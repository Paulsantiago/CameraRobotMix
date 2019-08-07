using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using MCLibSample;

using System.Threading;
using MiM_iVision;
using MVSDK;
using CameraHandle = System.Int32;
using MvApi = MVSDK.MvApi;

using ZXing;                  // for BarcodeWriter
using ZXing.QrCode;           // for QRCode Engine
using ZXing.Common;

using System.IO;

namespace Warp_Csharp
{
    public partial class ImageMainfrm : Form
    {

        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);
        public long ctr1 = 0, ctr2 = 0, freq = 0;
        public Form1 refMainfrm;
        public double pixel_to_mm = 0.76;

        iMatchDialog ncc = new iMatchDialog();
        public IntPtr GrayImg = iImage.CreateGrayiImage();
        public IntPtr ColorImg = iImage.CreateColoriImage();
        public IntPtr GrayROIImg = iImage.CreateGrayiImage();
        public IntPtr ColorROIImg = iImage.CreateColoriImage();
        public IntPtr CalbColorImg = iImage.CreateColoriImage();
        public IntPtr GrabImg = iImage.CreateGrayiImage();
        //public IntPtr SnapGrayImg = iImage.CreateGrayiImage();        
        public IntPtr hbitmap;
        public IntPtr NCCmodel = iMatch.CreateNCCMatch();
        public bool UsingColor = false;

        //建立六個QR code圖檔
        public IntPtr GrayImg1 = iImage.CreateGrayiImage();
        public IntPtr GrayImg2 = iImage.CreateGrayiImage();
        public IntPtr GrayImg3 = iImage.CreateGrayiImage();
        public IntPtr GrayImg4 = iImage.CreateGrayiImage();
        public IntPtr GrayImg5 = iImage.CreateGrayiImage();
        public IntPtr GrayImg6 = iImage.CreateGrayiImage();

        public IntPtr m_ImageBufferSnapshot; // 抓拍通道RGB圖像緩存

        //ROI
        public bool StartROI = false;
        public bool SettingROI = false;
        public mRect ROI;

        //For show ROI
        public Graphics g_ROI;
        public choosing Mychoose;
        delegate void UpdataPictureBox_delegate();
       
        static Mutex wait_hbitmap;
        E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;

        #region variable For Camera

        public CameraHandle m_hCamera = 0;
        public tSdkCameraCapbility tCameraCapability;
        public tSdkFrameHead m_tFrameHead;

        public IntPtr m_ImageBuffer;

        public Thread m_tCaptureThread = null;

        public bool m_bExitCaptureThread = false;

        public bool m_bRefresh = false;
        public bool m_bPause = true;
        public bool m_bRT_iMatch = false;

        public int m_MaxCCDCount = 4;
        public int m_iDisplayedFrames = 0;

        #endregion

        #region variable For iMatch
        //E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
        delegate void UpdateDataGridView_delegate();
        delegate void ClearDataGridView_delegate();
        delegate void UpdateiMatchResults_delegate();
        int findnum = 0;
        string[] str = new string[6];
        double Execute_time = 0;
        Pen RedPen = new Pen(Color.Red);
        //Graphics g = refMainfrm.Picbox.CreateGraphics();
        double[] rAng = new double[4];
        Point[] RegionPoint = new Point[4];
        NCCFind objfind;
        public Graphics g_iMatchResults;
        #endregion

        
        public ImageMainfrm()
        {
            InitializeComponent();
        }

        private void openNCCDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ncc.refMainfrm = this;
            ncc.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void keyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int serial = 0;
            E_iVision_ERRORS err = iVision.iGetKeySerial(ref serial);

            MessageBox.Show("Key State:" + err.ToString() + " Serial:" + serial.ToString(), "Information");
        }

        private void getVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = iVision.iGetiMatchVersion();
            string strdate = iVision.iGetiMatchVersionDate();
            MessageBox.Show(str.ToString()+ "   "+ strdate.ToString(), "Information");
        }

        private void Mainfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_tCaptureThread.IsAlive && m_tCaptureThread != null)
                m_tCaptureThread.Abort();

            iMatch.DestroyNCCMatch(NCCmodel);
            iImage.DestroyiImage(ColorImg);
            iImage.DestroyiImage(GrayImg);
            if (m_hCamera > 0)
            {
                m_bExitCaptureThread = true;

                while (m_tCaptureThread.IsAlive)
                {
                    MvApi.CameraPause(m_hCamera);
                    Thread.Sleep(10);
                }

                MvApi.CameraUnInit(m_hCamera);
                Marshal.FreeHGlobal(m_ImageBuffer);

                m_hCamera = 0;
            }
        }
        
        private void Picbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (StartROI)
            {
                ROI.top = e.Y;
                ROI.left = e.X;
                SettingROI = true;
                StartROI = false;
                
            }
        }

        private void Picbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (SettingROI)
            {
                ROI.right = e.X;
                ROI.down = e.Y;
                RefreshImageBox();
                //Picbox.Refresh();
                g_ROI.DrawRectangle(Pens.Red, ROI.left, ROI.top, ROI.right - ROI.left + 1, ROI.down - ROI.top + 1);
            }
        }

        private void Picbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (SettingROI)
            {
                ROI.right = e.X;
                ROI.down = e.Y;
                RefreshImageBox();
                //Picbox.Refresh();
                g_ROI.DrawRectangle(Pens.Red, ROI.left, ROI.top, ROI.right - ROI.left + 1, ROI.down - ROI.top + 1);
                SettingROI = false;
            }
        }

        public bool initializeCamera()
        {
            tSdkCameraDevInfo[] tCameraDevInfoList = new tSdkCameraDevInfo[m_MaxCCDCount];
            IntPtr ptr;
            int i;

            ptr = Marshal.AllocHGlobal(Marshal.SizeOf(new tSdkCameraDevInfo()) * m_MaxCCDCount);
            int iCameraCounts = m_MaxCCDCount;//如果有多個相機時，表示最大只獲取最多12個相機的信息列表。該變量必須初始化，并且大于1

            if (m_hCamera > 0) return true;  //已經初始化過，直接返回 true

            if (MvApi.CameraEnumerateDevice(ptr, ref iCameraCounts) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                for (i = 0; i < m_MaxCCDCount; i++)
                {
                    tCameraDevInfoList[i] = (tSdkCameraDevInfo)Marshal.PtrToStructure((IntPtr)((int)ptr + i * Marshal.SizeOf(new tSdkCameraDevInfo())), typeof(tSdkCameraDevInfo));
                }
                Marshal.FreeHGlobal(ptr);

                if (iCameraCounts >= 1)//此時iCameraCounts返回了實際連接的相機個數。如果大于1，則初始化第一個相機
                {
                    if (MvApi.CameraInit(ref tCameraDevInfoList[0], -1, -1, ref m_hCamera) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                    {
                        //獲得相機特性描述
                        ptr = Marshal.AllocHGlobal(Marshal.SizeOf(new tSdkCameraCapbility()));
                        MvApi.CameraGetCapability(m_hCamera, ptr);
                        tCameraCapability = (tSdkCameraCapbility)Marshal.PtrToStructure(ptr, typeof(tSdkCameraCapbility));
                        Marshal.FreeHGlobal(ptr);

                        //設置抓拍通道的分辨率。
                        tSdkImageResolution tResolution;
                        tResolution.uSkipMode = 0;
                        tResolution.uBinAverageMode = 0;
                        tResolution.uBinSumMode = 0;
                        tResolution.uResampleMask = 0;
                        tResolution.iVOffsetFOV = 0;
                        tResolution.iHOffsetFOV = 0;
                        tResolution.iWidthFOV = tCameraCapability.sResolutionRange.iWidthMax;
                        tResolution.iHeightFOV = tCameraCapability.sResolutionRange.iHeightMax;
                        tResolution.iWidth = tResolution.iWidthFOV;
                        tResolution.iHeight = tResolution.iHeightFOV;
                        tResolution.iIndex = 0xff;
                        tResolution.acDescription = new byte[32];
                        tResolution.iWidthZoomHd = 0;
                        tResolution.iHeightZoomHd = 0;
                        tResolution.iWidthZoomSw = 0;
                        tResolution.iHeightZoomSw = 0;

                        MvApi.CameraSetResolutionForSnap(m_hCamera, ref tResolution);
                        MvApi.CameraGetImageResolution(m_hCamera, ref tResolution);

                        iImage.iImageResize(GrayImg, tResolution.iWidth, tResolution.iHeight);

                        m_ImageBuffer = Marshal.AllocHGlobal(tCameraCapability.sResolutionRange.iWidthMax * tCameraCapability.sResolutionRange.iHeightMax * 3 + 1024);

                        if (tCameraCapability.sIspCapacity.bMonoSensor == 1)  //灰階相機
                        {
                            MvApi.CameraSetMonochrome(m_hCamera, 1);
                            MvApi.CameraSetIspOutFormat(m_hCamera, Convert.ToUInt32(MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8));

                            hbitmap = iImage.iGetBitmapAddress(GrayImg);
                        }
                        else
                        {
                            hbitmap = iImage.iGetBitmapAddress(GrayImg);
                        }
                        //return false;

                        //讓SDK來根據相機的型號動態創建該相機的配置窗口。
                        MvApi.CameraCreateSettingPage(m_hCamera, this.Handle, tCameraDevInfoList[0].acFriendlyName,/*SettingPageMsgCalBack*/null,/*m_iSettingPageMsgCallbackCtx*/(IntPtr)null, 0);

                        //兩種方式來獲得預覽圖像，設置回調函數或者使用定時器或者獨立線程的方式，
                        //主動調用CameraGetImageBuffer接口來抓圖。
                        //本例中僅演示了兩種的方式,注意，兩種方式也可以同時使用，但是在回調函數中，
                        //不要使用CameraGetImageBuffer，否則會造成死鎖現象。

                        m_bExitCaptureThread = false;
                        m_tCaptureThread = new Thread(new ThreadStart(CaptureThreadProc));
                        m_tCaptureThread.Start();
                        label1.Text = "Camera Init Successful";
                        return true;
                    }
                    else
                    {
                        m_hCamera = 0;
                        label1.Text = "Camera Init error";
                        //MessageBox.Show("Camera error");
                        return false;
                    }
                }
            }
            label1.Text = "Camera Init Successful";
            //MessageBox.Show("Camera OK");
            return true;
        }

        public void CaptureThreadProc()
        {
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            uint uRawBuffer;       //rawbuffer由SDK內部申請。應用層不要調用delete之類的釋放函數
            string strinresults_value;
            int results_value = 0;
            bool flag = false;
            /////////////////Define variables image

            IntPtr Training_Modelbitmap;
            Image Image_Decode;
            System.Drawing.Bitmap image;
            Bitmap bitmap ;
            BitmapLuminanceSource source;
            HybridBinarizer binarizer;
            BinaryBitmap binaryBit;

            mRect trainingmodel_size;
           
            QRCodeReader qrRead = new QRCodeReader();
            
            Result QRresults;
           
            while (m_bExitCaptureThread == false)
           // while (flag == false)
            {
                
                Thread.BeginCriticalRegion();
                //500毫秒超時,圖像沒捕獲到前，線程會被掛起,釋放CPU，所以該線程中無需調用sleep

               
                eStatus = MvApi.CameraGetImageBuffer(m_hCamera, out FrameHead, out uRawBuffer, 500); // here I recive the trigger signal
                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS)//如果是觸發模式，則有可能超時
                {
                   
                    MvApi.CameraImageProcess(m_hCamera, uRawBuffer, m_ImageBuffer, ref FrameHead);
                    wait_hbitmap.WaitOne();
                    iImage.iPointerToiImage(GrayImg, m_ImageBuffer, FrameHead.iWidth, FrameHead.iHeight);
                    m_tFrameHead = FrameHead;
                    m_iDisplayedFrames++;
                    ClearDataGrid();


                    
                    if (refMainfrm.MyFlag)
                    {
                        
                        if (m_bRT_iMatch)
                        {
                           
                            ////////////////////////////////////////////////START  /////////////////////
                           

                           if (QueryPerformanceCounter(ref ctr1) != 0)
                           {
                                err = iMatch.MatchNCCModel(GrayImg, NCCmodel);
                                QueryPerformanceCounter(ref ctr2);
                                Execute_time = (ctr2 - ctr1) * 1000.0 / freq;
                           }

                                if (err != E_iVision_ERRORS.E_OK)
                                {
                                    MessageBox.Show(err.ToString(), "Error");
                                    //ncc.label2.Text = "Error";
                                    return;
                                }
                                else
                                {
                                    //ncc.label2.Text = E_iVision_ERRORS.E_OK.ToString();
                                }

                                err = iMatch.iGetNCCMatchNum(NCCmodel, ref findnum);

                                if (err != E_iVision_ERRORS.E_OK)
                                {
                                    MessageBox.Show(err.ToString(), "Error");
                                    //ncc.label2.Text = "Error";
                                    return;
                                }

                                if (findnum != 0)
                                        Execute_time /= findnum;

                                for (int i = 0; i < findnum; i++)
                                {

                                    err = iMatch.iGetNCCMatchResults(NCCmodel, i, ref objfind);
                                    //QR  code
                                    int Model_hei = 0;
                                    int Model_wid = 0;
                                    err = iMatch.iGetModelWidth(NCCmodel, ref Model_wid);
                                    err = iMatch.iGetModelHeight(NCCmodel, ref Model_hei);
                                    err = iImage.iImageResize(GrayImg1, Model_wid, Model_hei);
                                    trainingmodel_size.left = (int)objfind.CX - Model_wid / 2;
                                    trainingmodel_size.top = (int)objfind.CY - Model_hei / 2;
                                    trainingmodel_size.right = (int)objfind.CX + Model_wid / 2;
                                    trainingmodel_size.down = (int)objfind.CY + Model_hei / 2;

                                    if (trainingmodel_size.left <= 0 || trainingmodel_size.top <= 0 || trainingmodel_size.right <= 0 || trainingmodel_size.down <= 0)
                                    {
                                        findnum = 0;
                                    }
                                    else
                                    {
                                        if (findnum != 0)
                                        {

                                             err = iImage.GetSubiImage(GrayImg1, GrayImg, trainingmodel_size);
                                             Training_Modelbitmap = iImage.iGetBitmapAddress(GrayImg1);
                                             Image_Decode = System.Drawing.Image.FromHbitmap(Training_Modelbitmap);
                                             image = new System.Drawing.Bitmap(Image_Decode, Image_Decode.Size);
                                             bitmap = new Bitmap(image);
                                             source = new BitmapLuminanceSource(bitmap);
                                             binarizer = new HybridBinarizer(source);
                                             binaryBit = new BinaryBitmap(binarizer);//binaryBit解碼圖

                                      
                                             QRresults = qrRead.decode(binaryBit);
                                             if (QRresults != null)
                                                {

                                                    results_value = Convert.ToInt32(QRresults.Text);

                                                    if ((results_value > 0) && (results_value < 7))
                                                    {

                                                        strinresults_value = Convert.ToString(results_value);
                                                        if (results_value == refMainfrm.SerialNum && refMainfrm.VisionData[0] == 0)
                                                        {
                                                            refMainfrm.VisionData[1] = (-(objfind.CY - 240) * pixel_to_mm);
                                                            refMainfrm.VisionData[2] = (-(objfind.CX - 376) * pixel_to_mm);
                                                            refMainfrm.VisionData[3] = 0;
                                                            refMainfrm.VisionData[4] = 0;
                                                            refMainfrm.VisionData[5] = 0;
                                                            refMainfrm.VisionData[6] = 0;
                                                            refMainfrm.VisionData[0] = 1;
                                                            refMainfrm.MyFlag = false;

                                                            if (refMainfrm.SerialNum==6)
                                                                refMainfrm.SerialNum = 0;
                                                            //flag = true;
                                                            i = findnum;

                                                        }
                                                        else
                                                        {
                                                            i = findnum;
                                                            strinresults_value = "nothing";
                                                        }
                                                    }
                                                   
                                                }
                                               
                                            
                                        }

                                        
                                      


                                        str[0] = objfind.Score.ToString("F4", CultureInfo.InvariantCulture);
                                        str[1] = objfind.CX.ToString("F4", CultureInfo.InvariantCulture);
                                        str[2] = objfind.CY.ToString("F4", CultureInfo.InvariantCulture);
                                        str[3] = objfind.Angle.ToString("F4", CultureInfo.InvariantCulture);
                                        str[4] = objfind.Scale.ToString("F4", CultureInfo.InvariantCulture);
                                        str[5] = Execute_time.ToString("F4", CultureInfo.InvariantCulture);

                                        RefreshDataGrid();
                                        RefreshiMatchResults();
                                    }
                                }

                                //ncc.dataGridView1.Rows.Add(str[0], str[1], str[2], str[3], str[4], str[5]);





                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////END ////////////////////
                            }
                        
                        
                            
                        //refMainfrm.MyFlag = false;
                    }
                    




                    //成功調用CameraGetImageBuffer后必須釋放，下次才能繼續調用CameraGetImageBuffer捕獲圖像。
                    MvApi.CameraReleaseImageBuffer(m_hCamera, uRawBuffer);

                    
                    //更新畫面
                    RefreshImageBox();


//this.g_ROI = this.Picbox.CreateGraphics();
//if (findnum > 0)
//    err = iMatch.iDrawiMatchResults(NCCmodel, g_ROI.GetHdc(), 1);
//else
//    this.Picbox.Refresh();


                    m_bRefresh = true;
                    wait_hbitmap.ReleaseMutex();
                }
                Thread.EndCriticalRegion();
            }
        }
        private void ClearDataGrid()
        { 
            if (ncc.dataGridView1.InvokeRequired)
            {
                ClearDataGridView_delegate d = new ClearDataGridView_delegate(ClearDataGrid);
                this.Invoke(d, new object[] { });
            }
            else
            {
                ncc.dataGridView1.Rows.Clear();
            }
            
        }

        private void RefreshDataGrid()
        {
            if (ncc.dataGridView1.InvokeRequired)
            {
                UpdateDataGridView_delegate d = new UpdateDataGridView_delegate(RefreshDataGrid);
                this.Invoke(d, new object[] { });
            }
            else
            {
                 ncc.dataGridView1.Rows.Add(str[0], str[1], str[2], str[3], str[4], str[5]);
                   
            }
        }


        private void RefreshiMatchResults()
        {
            if (this.Picbox.InvokeRequired)
            {
                UpdataPictureBox_delegate d = new UpdataPictureBox_delegate(RefreshiMatchResults);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.g_iMatchResults = this.Picbox.CreateGraphics();
                err = iMatch.iDrawiMatchResults(NCCmodel, g_iMatchResults.GetHdc(), 1);

            }
        }

        private void RefreshImageBox()
        {
            if (this.Picbox.InvokeRequired)
            {
                UpdataPictureBox_delegate d = new UpdataPictureBox_delegate(RefreshImageBox);
                this.Invoke(d, new object[] { });
            }
            else
            {
                if (this.Picbox.Image != null)
                    this.Picbox.Image.Dispose();

                this.Picbox.Image = System.Drawing.Image.FromHbitmap(hbitmap);
                this.Picbox.Refresh();
                this.g_ROI = this.Picbox.CreateGraphics();
            }
        }
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            //Picbox.Location = new Point(10, 10);

            wait_hbitmap = new Mutex(false);

            initializeCamera();
        }

        private void nCCMatchingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void softwaretrigger()
        {
            MvApi.CameraSoftTrigger(m_hCamera);
        }
        public void softwarecontinumode()
        {
            MvApi.CameraSetTriggerMode(m_hCamera, 0);
        }
        public void softwaretriggermode()
        {
            MvApi.CameraSetTriggerMode(m_hCamera, 1);
        }

       
    }
}

