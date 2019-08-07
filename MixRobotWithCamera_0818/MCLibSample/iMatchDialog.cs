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
    public partial class iMatchDialog :Form 
    {
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);
        long ctr1 = 0, ctr2 = 0, freq = 0;

        public ImageMainfrm refMainfrm;
        string path;

        //For matching
        bool UsingMask;
        bool UsingRotation;
        bool UsingScale;
        int  Max_objs;
        double MaxAng, MinAng;
        double MaxScale, MinScale;
        double MinScore;
        int DontCarevalue;
        int MinReduceArea;
        NCCFind objfind;

        bool TrainingFromROI = false;

        public iMatchDialog()
        {
            InitializeComponent();

            UsingMask = false;
            UsingRotation = false;
            UsingScale = false;
            cbx_rotation.CheckState = CheckState.Unchecked;
            cbx_scale.CheckState = CheckState.Unchecked;
            cbx_dontcare.CheckState = CheckState.Unchecked;

            Max_objs = 6;
            MaxAng = 15; MinAng = -15;
            MaxScale = 1.1; MinScale = 0.9;
            MinScore = 0.6;
            DontCarevalue = 0;
            MinReduceArea = 64;

            tbx_objnums.Text = Max_objs.ToString();
            tbx_maxang.Text = MaxAng.ToString();
            tbx_minang.Text = MinAng.ToString();
            tbx_maxscale.Text = MaxScale.ToString();
            tbx_minscale.Text = MinScale.ToString();
            tbx_minscore.Text = MinScore.ToString();
            tbx_dontcarethreshold.Text = DontCarevalue.ToString();
            tbx_MinReduceArea.Text = MinReduceArea.ToString();
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            refMainfrm.openFileDialog1.Filter = "BMP file |*.bmp";
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
            if (refMainfrm.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = refMainfrm.openFileDialog1.FileName;
                err = iImage.iReadImage(refMainfrm.ColorImg, path);
                if (err == E_iVision_ERRORS.E_OK)
                {
                    refMainfrm.hbitmap = iImage.iGetBitmapAddress(refMainfrm.ColorImg);
                    if (refMainfrm.Picbox.Image != null)
                        refMainfrm.Picbox.Image.Dispose();
                    refMainfrm.Picbox.Image = System.Drawing.Image.FromHbitmap(refMainfrm.hbitmap);
                    refMainfrm.UsingColor = true;
                    refMainfrm.Picbox.Refresh();
                    refMainfrm.g_ROI = refMainfrm.Picbox.CreateGraphics();
                }
                else
                {
                    MessageBox.Show(err.ToString(), "Error");
                    label2.Text = "Error";
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_LoadGrayImg_Click(object sender, EventArgs e)
        {
            refMainfrm.openFileDialog1.Filter = "BMP file |*.bmp";
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
            if (refMainfrm.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = refMainfrm.openFileDialog1.FileName;
                err = iImage.iReadImage(refMainfrm.GrayImg, path);
                if (err == E_iVision_ERRORS.E_OK)
                {
                    refMainfrm.hbitmap = iImage.iGetBitmapAddress(refMainfrm.GrayImg);
                    if (refMainfrm.Picbox.Image != null)
                        refMainfrm.Picbox.Image.Dispose();
                    refMainfrm.Picbox.Image = System.Drawing.Image.FromHbitmap(refMainfrm.hbitmap);
                    refMainfrm.UsingColor = false;
                    refMainfrm.Picbox.Refresh();
                    refMainfrm.g_ROI = refMainfrm.Picbox.CreateGraphics();
                }
                else
                {
                    MessageBox.Show(err.ToString(), "Error");
                    label2.Text = "Error";
                }
            }

        }

        private void btn_NCCtraining_Click(object sender, EventArgs e)
        {
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;

            UpData_NCC_Parameter();

            MinReduceArea = Convert.ToInt32(tbx_MinReduceArea.Text);
            err = iMatch.iSetMinReduceArea(refMainfrm.NCCmodel, MinReduceArea);
            if (err != E_iVision_ERRORS.E_OK)
            { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
            else label2.Text = "E_OK";

            err = iMatch.iSetDontCareThreshold(refMainfrm.NCCmodel, DontCarevalue);
            if (err != E_iVision_ERRORS.E_OK)
            { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
            else label2.Text = "E_OK";

            if (TrainingFromROI)
            {
                if (refMainfrm.UsingColor)
                {
                    err = iImage.iImageResize(refMainfrm.ColorROIImg, refMainfrm.ROI.right - refMainfrm.ROI.left + 1,
                                                                    refMainfrm.ROI.down - refMainfrm.ROI.top + 1);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show("iImageResize Err" + err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";

                    err = iImage.GetSubiImage(refMainfrm.ColorROIImg, refMainfrm.ColorImg, refMainfrm.ROI);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show("GetSubiImage Err" + err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";

                    err = iMatch.CreateNCCModel(refMainfrm.ColorROIImg, refMainfrm.NCCmodel, UsingMask);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";
                }
                else
                {
                    err = iImage.iImageResize(refMainfrm.GrayROIImg, refMainfrm.ROI.right - refMainfrm.ROI.left + 1,
                                                                    refMainfrm.ROI.down - refMainfrm.ROI.top + 1);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show("iImageResize Err" + err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";

                    err = iImage.GetSubiImage(refMainfrm.GrayROIImg, refMainfrm.GrayImg, refMainfrm.ROI);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show("GetSubiImage Err" + err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";

                    err = iMatch.CreateNCCModel(refMainfrm.GrayROIImg, refMainfrm.NCCmodel, UsingMask);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";
                }
            }
            else
            {
                if (refMainfrm.UsingColor)
                {
                    err = iMatch.CreateNCCModel(refMainfrm.ColorImg, refMainfrm.NCCmodel, UsingMask);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";
                }
                else
                {
                    err = iMatch.CreateNCCModel(refMainfrm.GrayImg, refMainfrm.NCCmodel, UsingMask);
                    if (err != E_iVision_ERRORS.E_OK)
                    { MessageBox.Show(err.ToString(), "Error"); label2.Text = "Error"; }
                    else label2.Text = "E_OK";
                }
            }
        }

        private void NCCDialog_Load(object sender, EventArgs e)
        {
            label2.Text = "NULL";
            dataGridView1.Rows.Clear();
        }

        void UpData_NCC_Parameter()
        {
            if (cbx_rotation.CheckState == CheckState.Checked) 
                UsingRotation = true;
            else 
                UsingRotation = false;

            if (cbx_scale.CheckState == CheckState.Checked)
                UsingScale = true;
            else
                UsingScale = false;

            if (cbx_dontcare.CheckState == CheckState.Checked)
                UsingMask = true;
            else
                UsingMask = false;

            if (chk_TrainingFromROI.CheckState == CheckState.Checked)
                TrainingFromROI = true;
             else
                TrainingFromROI = false;

            Max_objs = Convert.ToInt32(tbx_objnums.Text);
            MaxAng = Convert.ToDouble(tbx_maxang.Text);
            MinAng = Convert.ToDouble(tbx_minang.Text);
            MaxScale = Convert.ToDouble(tbx_maxscale.Text);
            MinScale = Convert.ToDouble(tbx_minscale.Text);
            MinScore = Convert.ToDouble(tbx_minscore.Text);
            DontCarevalue = Convert.ToInt32(tbx_dontcarethreshold.Text); 
        }

        private void btn_NCCmatching_Click(object sender, EventArgs e)
        {
            if (refMainfrm.m_bPause)
            {
                Do_iMatchProcess();
            }
            else
            {
                QueryPerformanceFrequency(ref refMainfrm.freq);
                if (refMainfrm.m_bRT_iMatch)
                {
                    this.btn_NCCmatching.Text = "Matching";
                    refMainfrm.m_bRT_iMatch = false;
                }
                else
                {
                    UpData_NCC_Parameter();
                    iMatch.iSetMinScore(refMainfrm.NCCmodel, MinScore);
                    iMatch.iSetIsRotated(refMainfrm.NCCmodel, UsingRotation);
                    iMatch.iSetIsScaled(refMainfrm.NCCmodel, UsingScale);
                    iMatch.iSetOccurrence(refMainfrm.NCCmodel, Max_objs);
                    iMatch.iSetAngle(refMainfrm.NCCmodel, MaxAng, MinAng);
                    iMatch.iSetScale(refMainfrm.NCCmodel, MaxScale, MinScale);
                    iMatch.iSetDontCareThreshold(refMainfrm.NCCmodel, DontCarevalue);
                    this.btn_NCCmatching.Text = "Stop";
                    refMainfrm.m_bRT_iMatch = true;
                }

            }


        }

        private void Do_iMatchProcess()
        {
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
            int findnum = 0;
            string[] str = new string[6];
            double Execute_time = 0;
            QueryPerformanceFrequency(ref freq);
            dataGridView1.Rows.Clear();
            refMainfrm.Picbox.Refresh();

            Pen RedPen = new Pen(Color.Red);
            Graphics g = refMainfrm.Picbox.CreateGraphics();
            double[] rAng = new double[4];
            Point[] RegionPoint = new Point[4];

            UpData_NCC_Parameter();

            iMatch.iSetMinScore(refMainfrm.NCCmodel, MinScore);
            iMatch.iSetIsRotated(refMainfrm.NCCmodel, UsingRotation);
            iMatch.iSetIsScaled(refMainfrm.NCCmodel, UsingScale);
            iMatch.iSetOccurrence(refMainfrm.NCCmodel, Max_objs);
            iMatch.iSetAngle(refMainfrm.NCCmodel, MaxAng, MinAng);
            iMatch.iSetScale(refMainfrm.NCCmodel, MaxScale, MinScale);
            iMatch.iSetDontCareThreshold(refMainfrm.NCCmodel, DontCarevalue);


            err = iMatch.iIsPatternLearn(refMainfrm.NCCmodel);
            if (err != E_iVision_ERRORS.E_TRUE)
            {
                MessageBox.Show(err.ToString(), "Error");
                label2.Text = "Error";
                return;
            }

            if (refMainfrm.UsingColor)
            {
                if (QueryPerformanceCounter(ref ctr1) != 0)
                {
                    err = iMatch.MatchNCCModel(refMainfrm.ColorImg, refMainfrm.NCCmodel);
                    QueryPerformanceCounter(ref ctr2);
                    Execute_time = (ctr2 - ctr1) * 1000.0 / freq;
                }
            }
            else
            {
                if (QueryPerformanceCounter(ref ctr1) != 0)
                {
                    err = iMatch.MatchNCCModel(refMainfrm.GrayImg, refMainfrm.NCCmodel);
                    QueryPerformanceCounter(ref ctr2);
                    Execute_time = (ctr2 - ctr1) * 1000.0 / freq;
                }
            }


            if (err != E_iVision_ERRORS.E_OK)
            {
                MessageBox.Show(err.ToString(), "Error");
                label2.Text = "Error";
                return;
            }
            else label2.Text = E_iVision_ERRORS.E_OK.ToString();

            err = iMatch.iDrawiMatchResults(refMainfrm.NCCmodel, g.GetHdc(), 1);

            err = iMatch.iGetNCCMatchNum(refMainfrm.NCCmodel, ref findnum);
            if (err != E_iVision_ERRORS.E_OK)
            {
                MessageBox.Show(err.ToString(), "Error");
                label2.Text = "Error";
                return;
            }

            Execute_time /= findnum;
            for (int i = 0; i < findnum; i++)
            {
                err = iMatch.iGetNCCMatchResults(refMainfrm.NCCmodel, i, ref objfind);

                str[0] = objfind.Score.ToString("F4", CultureInfo.InvariantCulture);
                str[1] = objfind.CX.ToString("F4", CultureInfo.InvariantCulture);
                str[2] = objfind.CY.ToString("F4", CultureInfo.InvariantCulture);
                str[3] = objfind.Angle.ToString("F4", CultureInfo.InvariantCulture);
                str[4] = objfind.Scale.ToString("F4", CultureInfo.InvariantCulture);
                str[5] = Execute_time.ToString("F4", CultureInfo.InvariantCulture);

                dataGridView1.Rows.Add(str[0], str[1], str[2], str[3], str[4], str[5]);
            }
        }

        private void btn_ReadModel_Click(object sender, EventArgs e)
        {
            refMainfrm.openFileDialog1.Filter = "iMatchModel file |*.imm";
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
            if (refMainfrm.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = refMainfrm.openFileDialog1.FileName;

                err = iMatch.LoadiMatchModel(refMainfrm.NCCmodel, path);

                if (err == E_iVision_ERRORS.E_OK)
                {
                    //更新參數到介面
                    iMatch.iGetOccurrence(refMainfrm.NCCmodel,ref Max_objs);
                    iMatch.iGetAngle(refMainfrm.NCCmodel, ref MaxAng, ref MinAng);
                    iMatch.iGetScale(refMainfrm.NCCmodel, ref MaxScale, ref MinScale);
                    iMatch.iGetMinScore(refMainfrm.NCCmodel, ref MinScore);
                    iMatch.iGetDontCareThreshold(refMainfrm.NCCmodel, ref DontCarevalue);
                    iMatch.iGetMinReduceArea(refMainfrm.NCCmodel, ref MinReduceArea);

                    iMatch.iGetIsRotated(refMainfrm.NCCmodel,ref UsingRotation);
                    iMatch.iGetIsScaled(refMainfrm.NCCmodel,ref UsingScale);
                    iMatch.iGetIsDontArea(refMainfrm.NCCmodel, ref UsingMask);

                    tbx_objnums.Text = Max_objs.ToString();
                    tbx_maxang.Text = MaxAng.ToString();
                    tbx_minang.Text = MinAng.ToString();
                    tbx_maxscale.Text = MaxScale.ToString();
                    tbx_minscale.Text = MinScale.ToString();
                    tbx_minscore.Text = MinScore.ToString();
                    tbx_dontcarethreshold.Text = DontCarevalue.ToString();
                    tbx_MinReduceArea.Text = MinReduceArea.ToString();

                    if (UsingRotation)
                        cbx_rotation.CheckState = CheckState.Checked;
                    else
                        cbx_rotation.CheckState = CheckState.Unchecked;

                    if (UsingScale)
                        cbx_scale.CheckState = CheckState.Checked;
                    else
                        cbx_scale.CheckState = CheckState.Unchecked;

                    if (UsingMask)
                        cbx_dontcare.CheckState = CheckState.Checked;
                    else
                        cbx_dontcare.CheckState = CheckState.Unchecked;

                    label2.Text = "OK";
                }
                else
                {
                    MessageBox.Show(err.ToString(), "Error");
                    label2.Text = "Error";
                }
            }
        }

        private void btn_SaveModel_Click(object sender, EventArgs e)
        {
            refMainfrm.saveFileDialog1.Filter = "iMatchModel file |*.imm";
            refMainfrm.saveFileDialog1.AddExtension = true;
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;
 
            UpData_NCC_Parameter();

            iMatch.iSetMinReduceArea(refMainfrm.NCCmodel, MinReduceArea);
            iMatch.iSetMinScore(refMainfrm.NCCmodel, MinScore);
            iMatch.iSetIsRotated(refMainfrm.NCCmodel, UsingRotation);
            iMatch.iSetIsScaled(refMainfrm.NCCmodel, UsingScale);
            iMatch.iSetIsDontArea(refMainfrm.NCCmodel,UsingMask);
            iMatch.iSetOccurrence(refMainfrm.NCCmodel, Max_objs);
            iMatch.iSetAngle(refMainfrm.NCCmodel, MaxAng, MinAng);
            iMatch.iSetScale(refMainfrm.NCCmodel, MaxScale, MinScale);
            iMatch.iSetDontCareThreshold(refMainfrm.NCCmodel, DontCarevalue);

            if (refMainfrm.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = refMainfrm.saveFileDialog1.FileName;

                //if (refMainfrm.UsingColor)
                //{
                //    err = iImage.iImageIsNULL(refMainfrm.ColorImg);
                //    if (err == E_iVision_ERRORS.E_TRUE)
                //    {
                //        MessageBox.Show(err.ToString(), "Error");
                //        label2.Text = "Error";
                //        return;
                //    }
                //}
                //else
                {
                    err = iImage.iImageIsNULL(refMainfrm.GrayImg);
                    if (err == E_iVision_ERRORS.E_TRUE)
                    {
                        MessageBox.Show(err.ToString(), "Error");
                        label2.Text = "Error";
                        return;
                    }
                }

                err = iMatch.SaveiMatchModel(refMainfrm.NCCmodel, path);
                if (err != E_iVision_ERRORS.E_OK)
                {
                    MessageBox.Show(err.ToString(), "Error");
                    label2.Text = "Error";
                }
                else
                    label2.Text = "OK";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            E_iVision_ERRORS err = E_iVision_ERRORS.E_NULL;

            err = iMatch.iVisitingKey();
            if (err != E_iVision_ERRORS.E_OK)
            {
                MessageBox.Show(err.ToString(), "Error");
                label2.Text = "Error";
            }
            else
                label2.Text = "OK";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (btn_Play.Text == "Play")
            //{
            //    MvApi.CameraPlay(refMainfrm.m_hCamera);
            //    btn_Play.Text = "Pause";
            //    refMainfrm.m_bPause = false;
            //}
            //else
            //{
            //    MvApi.CameraPause(refMainfrm.m_hCamera);
            //    btn_Play.Text = "Play";
            //    refMainfrm.m_bPause = true;
            //}

            refMainfrm.StartROI = true;
            refMainfrm.Picbox.Refresh();
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (refMainfrm.m_hCamera < 1) //還未初始化相機
            {
                if (refMainfrm.initializeCamera() == true)
                {
                    MvApi.CameraPlay(refMainfrm.m_hCamera);
                    btn_Play.Text = "Pause";
                    refMainfrm.m_bPause = false;
                }
            }
            else
            {
                if (btn_Play.Text == "Play")
                {
                    MvApi.CameraPlay(refMainfrm.m_hCamera);
                    btn_Play.Text = "Pause";
                    refMainfrm.m_bPause = false;
                }
                else
                {
                    MvApi.CameraPause(refMainfrm.m_hCamera);
                    btn_Play.Text = "Play";
                    refMainfrm.m_bPause = true;
                }
            }
        }

        private void btn_CameraSetting_Click(object sender, EventArgs e)
        {
            if (refMainfrm.m_hCamera > 0)
            {
                MvApi.CameraShowSettingPage(refMainfrm.m_hCamera, 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int findnum = 0;
            int Model_wid = 0;
            E_iVision_ERRORS err = iMatch.iGetModelWidth(refMainfrm.NCCmodel, ref Model_wid);
            int Model_hei = 0;
            err = iMatch.iGetModelHeight(refMainfrm.NCCmodel, ref Model_hei);
            err = iMatch.iGetNCCMatchNum(refMainfrm.NCCmodel, ref findnum);

            mRect trainingmodel_size ;

            for (int i = 0; i < findnum; i++)
            {
                err = iMatch.iGetNCCMatchResults(refMainfrm.NCCmodel, i, ref objfind);
                err = iImage.iImageResize(refMainfrm.GrayImg1, Model_wid, Model_hei);
                trainingmodel_size.left = (int)objfind.CX - Model_wid / 2;
                trainingmodel_size.top = (int)objfind.CY - Model_hei / 2;
                trainingmodel_size.right = (int)objfind.CX + Model_wid / 2;
                trainingmodel_size.down = (int)objfind.CY + Model_hei / 2;
                err = iImage.GetSubiImage(refMainfrm.GrayImg1, refMainfrm.GrayImg, trainingmodel_size);

                IntPtr Training_Modelbitmap = iImage.iGetBitmapAddress(refMainfrm.GrayImg1);
                Image Image_Decode = System.Drawing.Image.FromHbitmap(Training_Modelbitmap);
                //refMainfrm.Picbox.Refresh();

                QRCodeReader qrRead = new QRCodeReader();
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(Image_Decode, Image_Decode.Size);
                Bitmap bitmap = new Bitmap(image);
                BitmapLuminanceSource source = new BitmapLuminanceSource(bitmap);
                HybridBinarizer binarizer = new HybridBinarizer(source);
                BinaryBitmap binaryBit = new BinaryBitmap(binarizer);//binaryBit解碼圖
                Result results_1;
                try
                {
                    results_1 = qrRead.decode(binaryBit);
                    MessageBox.Show(results_1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.GetType() + ":" + ex.Message);
                }          
            }

            //IntPtr mybitmap = iImage.iGetBitmapAddress(refMainfrm.GrayImg1);
            //Image myImage = System.Drawing.Image.FromHbitmap(mybitmap);
            ////refMainfrm.Picbox.Refresh();
     

            //QRCodeReader qrRead = new QRCodeReader();
            //System.Drawing.Bitmap image = new System.Drawing.Bitmap(myImage, myImage.Size);
            //Bitmap bitmap = new Bitmap(image);
            //BitmapLuminanceSource source = new BitmapLuminanceSource(bitmap);
            //HybridBinarizer binarizer = new HybridBinarizer(source);
            //BinaryBitmap binaryBit = new BinaryBitmap(binarizer);//binaryBit解碼圖
            //Result results_1;
            //try
            //{
            //    results_1 = qrRead.decode(binaryBit);
            //    MessageBox.Show(results_1.Text);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error:" + ex.GetType() + ":" + ex.Message);
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MvApi.CameraSetTriggerMode(refMainfrm.m_hCamera,0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // MvApi.CameraSetTriggerMode(refMainfrm.m_hCamera, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // MvApi.CameraSoftTrigger(refMainfrm.m_hCamera);
        }

        

       
    }
}
