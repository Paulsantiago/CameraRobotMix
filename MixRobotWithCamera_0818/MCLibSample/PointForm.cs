using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using RTIOMSMC_Name;

namespace MCLibSample
{
    
    public partial class PointForm : Form
    {
        
        public Form1 refMainfrm;
        public DataGridViewRowCollection rows;
        public Tech_InfoCaudal InfoDlg = new Tech_InfoCaudal();
        public JointInf jStandby;
        //public JointInf JposStaby;
        
        public PointInf pVisionCenter;
        public PointInf pP1;
        public PointInf pP2;
        public PointInf pP3;
        public PointInf pP4;
        public PointInf pP5;
        public PointInf pP6;
        public PointInf pP7;
        public PointInf pP8;
        // Get Glass
        public PointInf pP9;
        ////////////////
        
       
        // Stand By Joint
        public JointInf JposStaby;

        public PointInf pP11;
        public PointInf pP12;
        static PointInf[] _ShakePoints = new PointInf[4];
        static PointInf[] _globalValue = new PointInf[14];
        static double speed;
        public static double Parameters
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        
        }

        public static PointInf[] GlobalValue
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        
        }    

        public PointForm()
        {
            InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void PointForm_Load(object sender, EventArgs e)
        {
            rows = dataGridView1.Rows;
        }

        private void PointForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btn_GripCatch_Click(object sender, EventArgs e)
        {
            refMainfrm.GripperCatch();
        }

        private void btn_GripRelease_Click(object sender, EventArgs e)
        {
            refMainfrm.GripperRelease();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            PointInf temp;
            JointInf jTemp;
            rows.Clear();
            jTemp = jStandby;
            //rows.Add("等待點", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            rows.Add("等待點", Convert.ToString(jTemp.theta1), Convert.ToString(jTemp.theta2), Convert.ToString(jTemp.theta3), Convert.ToString(jTemp.theta4), Convert.ToString(jTemp.theta5), Convert.ToString(jTemp.theta6));
 

            temp = pVisionCenter;
            rows.Add("影像中心", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP1;
            rows.Add("放置點1", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP2;
            rows.Add("放置點2", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP3;
            rows.Add("放置點3", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP4;
            rows.Add("放置點4", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP5;
            rows.Add("放置點5", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP6;
            rows.Add("放置點6", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP7;
            rows.Add("放置點7", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP8;
            rows.Add("放置點8", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            
            temp = pP9;
            rows.Add("stb2", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));

            jTemp = JposStaby;
            //rows.Add("等待點", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            rows.Add("JposStaby10", Convert.ToString(jTemp.theta1), Convert.ToString(jTemp.theta2), Convert.ToString(jTemp.theta3), Convert.ToString(jTemp.theta4), Convert.ToString(jTemp.theta5), Convert.ToString(jTemp.theta6));
 
            temp = pP11;
            rows.Add("放置點11", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));
            temp = pP12;
            rows.Add("放置點12", Convert.ToString(temp.x), Convert.ToString(temp.y), Convert.ToString(temp.z), Convert.ToString(temp.a), Convert.ToString(temp.b), Convert.ToString(temp.c));

            //rows.Add("等待點", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            //temp = pVisionCenter;
            //rows.Add("影像中心", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            //temp = pP1;
            //rows.Add("放置點1", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            //temp = pP2;
            //rows.Add("放置點2", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            //temp = pP3;
            //rows.Add("放置點3", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            //temp = pP4;
            //rows.Add("放置點4", Convert.ToString(temp.theta1), Convert.ToString(temp.theta2), Convert.ToString(temp.theta3), Convert.ToString(temp.theta4), Convert.ToString(temp.theta5), Convert.ToString(temp.theta6));
            
            _globalValue[0] = pP1;
            _globalValue[1] = pP2;
            _globalValue[2] = pP3;
            _globalValue[3] = pP4;

            _globalValue[4] = pP5;
            _globalValue[5] = pP6;
            _globalValue[6] = pP7;
            _globalValue[7] = pP8;

            //_globalValue[8] = jStandby;
            _globalValue[9] = pVisionCenter;

            speed = Convert.ToDouble(tb_Speed.Text);

            _globalValue[10] = pP9;
            
            _globalValue[12] = pP11;
            _globalValue[13] = pP12;
            refMainfrm.speedBart = Convert.ToDouble(tb_Speed.Text);
          
            //refMainfrm.My_ShakePoints(_ShakePoints[0], _ShakePoints[1], _ShakePoints[2], Convert.ToDouble(tb_Speed.Text));
            refMainfrm.Wait_Time = Convert.ToDouble(tb_WaitTime.Text);

        }
        public double DrinkTime ;
        public void Time()
        {
            DrinkTime = Convert.ToDouble(tb_WaitTime.Text);
        }
        private void btn_Here_Click(object sender, EventArgs e)
        {
            //    JointInf temp;

            //    temp.theta1 = Convert.ToDouble(refMainfrm.tb_Axis1.Text);
            //    temp.theta2 = Convert.ToDouble(refMainfrm.tb_Axis2.Text);
            //    temp.theta3 = Convert.ToDouble(refMainfrm.tb_Axis3.Text);
            //    temp.theta4 = Convert.ToDouble(refMainfrm.tb_Axis4.Text);
            //    temp.theta5 = Convert.ToDouble(refMainfrm.tb_Axis5.Text);
            //    temp.theta6 = Convert.ToDouble(refMainfrm.tb_Axis6.Text);

            PointInf temp;
            JointInf jTemp;

            temp.x = Convert.ToDouble(refMainfrm.tb_posX.Text);
            temp.y = Convert.ToDouble(refMainfrm.tb_posY.Text);
            temp.z = Convert.ToDouble(refMainfrm.tb_posZ.Text);
            temp.a = Convert.ToDouble(refMainfrm.tb_posA.Text);
            temp.b = Convert.ToDouble(refMainfrm.tb_posB.Text);
            temp.c = Convert.ToDouble(refMainfrm.tb_posC.Text);

            jTemp.theta1 = Convert.ToDouble(refMainfrm.tb_Axis1.Text);
            jTemp.theta2 = Convert.ToDouble(refMainfrm.tb_Axis2.Text);
            jTemp.theta3 = Convert.ToDouble(refMainfrm.tb_Axis3.Text);
            jTemp.theta4 = Convert.ToDouble(refMainfrm.tb_Axis4.Text);
            jTemp.theta5 = Convert.ToDouble(refMainfrm.tb_Axis5.Text);
            jTemp.theta6 = Convert.ToDouble(refMainfrm.tb_Axis6.Text);
            jTemp.theta7 = 0;

            //temp.theta7 = 0;
            if (ratioBtn_Wait.Checked)
            {
                jStandby = jTemp;
                refMainfrm.jMyStandBy = jStandby;
            }
            else if (ratioBtn_VisionCenter.Checked)
            {
                pVisionCenter = temp;
                refMainfrm.myPnP.VisionCenter = temp;
            }
            else if (ratioBtn_P1.Checked)
            {
                pP1 = temp;
                refMainfrm.pDrinkSource[0] = temp;
            }
            else if (ratioBtn_P2.Checked)
            {
                pP2 = temp;
                refMainfrm.pDrinkSource[1] = temp;
            }
            else if (ratioBtn_P3.Checked)
            {
                pP3 = temp;
                refMainfrm.pDrinkSource[2] = temp;
            }
            else if (ratioBtn_P4.Checked)
            {
                pP4 = temp;
                refMainfrm.pDrinkSource[3] = temp;
            }
            else if (ratioBtn_P5.Checked)
            {
                pP5 = temp;
                refMainfrm.pDrinkSource[4] = temp;
            }
            else if (ratioBtn_P6.Checked)
            {
                pP6 = temp;
                refMainfrm.pDrinkSource[5] = temp;
            }
            else if (ratioBtn_P7.Checked)
            {
                pP7 = temp;
                refMainfrm.pDrinkSource[6] = temp;
            }
            else if (ratioBtn_P8.Checked)
                pP8 = temp;
            else if (ratioBtn_P9.Checked)
            {
                pP9 = temp;
                refMainfrm.pMyStandBy2 = temp;
                
            }

            else if (ratioBtn_P10.Checked)
            {
                JposStaby = jTemp;
               // refMainfrm.jMyStandBy2 = JposStaby;
            }
            else if (ratioBtn_P11.Checked)
                pP11 = temp;
            else if (ratioBtn_P12.Checked)
                pP12 = temp;

        }

        private int PnP_Load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "文字檔|*.txt";
            openFileDialog1.Title = "Load File";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                //string Data;
                char[] delimiterChars = { '\t', '\n' };
                //Data = System.IO.File.ReadAllText(openFileDialog1.FileName);
                string[] Data = System.IO.File.ReadAllText(openFileDialog1.FileName).Split(delimiterChars);


                //pStandby.theta1 = Convert.ToDouble(Data[0]);
                //pStandby.theta2 = Convert.ToDouble(Data[1]);
                //pStandby.theta3 = Convert.ToDouble(Data[2]);
                //pStandby.theta4 = Convert.ToDouble(Data[3]);
                //pStandby.theta5 = Convert.ToDouble(Data[4]);
                //pStandby.theta6 = Convert.ToDouble(Data[5]);

                //pVisionCenter.theta1 = Convert.ToDouble(Data[6]);
                //pVisionCenter.theta2 = Convert.ToDouble(Data[7]);
                //pVisionCenter.theta3 = Convert.ToDouble(Data[8]);
                //pVisionCenter.theta4 = Convert.ToDouble(Data[9]);
                //pVisionCenter.theta5 = Convert.ToDouble(Data[10]);
                //pVisionCenter.theta6 = Convert.ToDouble(Data[11]);

                //pP1.theta1 = Convert.ToDouble(Data[12]);
                //pP1.theta2 = Convert.ToDouble(Data[13]);
                //pP1.theta3 = Convert.ToDouble(Data[14]);
                //pP1.theta4 = Convert.ToDouble(Data[15]);
                //pP1.theta5 = Convert.ToDouble(Data[16]);
                //pP1.theta6 = Convert.ToDouble(Data[17]);

                //pP2.theta1 = Convert.ToDouble(Data[18]);
                //pP2.theta2 = Convert.ToDouble(Data[19]);
                //pP2.theta3 = Convert.ToDouble(Data[20]);
                //pP2.theta4 = Convert.ToDouble(Data[21]);
                //pP2.theta5 = Convert.ToDouble(Data[22]);
                //pP2.theta6 = Convert.ToDouble(Data[23]);

                //pP3.theta1 = Convert.ToDouble(Data[24]);
                //pP3.theta2 = Convert.ToDouble(Data[25]);
                //pP3.theta3 = Convert.ToDouble(Data[26]);
                //pP3.theta4 = Convert.ToDouble(Data[27]);
                //pP3.theta5 = Convert.ToDouble(Data[28]);
                //pP3.theta6 = Convert.ToDouble(Data[29]);

                //pP4.theta1 = Convert.ToDouble(Data[30]);
                //pP4.theta2 = Convert.ToDouble(Data[31]);
                //pP4.theta3 = Convert.ToDouble(Data[32]);
                //pP4.theta4 = Convert.ToDouble(Data[33]);
                //pP4.theta5 = Convert.ToDouble(Data[34]);
                //pP4.theta6 = Convert.ToDouble(Data[35]);



                //pStandby.x = Convert.ToDouble(Data[0]);
                //pStandby.y = Convert.ToDouble(Data[1]);
                //pStandby.z = Convert.ToDouble(Data[2]);
                //pStandby.a = Convert.ToDouble(Data[3]);
                //pStandby.b = Convert.ToDouble(Data[4]);
                //pStandby.c = Convert.ToDouble(Data[5]);


                jStandby.theta1 = Convert.ToDouble(Data[0]);
                jStandby.theta2 = Convert.ToDouble(Data[1]);
                jStandby.theta3 = Convert.ToDouble(Data[2]);
                jStandby.theta4 = Convert.ToDouble(Data[3]);
                jStandby.theta5 = Convert.ToDouble(Data[4]);
                jStandby.theta6 = Convert.ToDouble(Data[5]);
                jStandby.theta7 = 0;

                refMainfrm.jMyStandBy = jStandby;
               
                pVisionCenter.x = Convert.ToDouble(Data[6]);
                pVisionCenter.y = Convert.ToDouble(Data[7]);
                pVisionCenter.z = Convert.ToDouble(Data[8]);
                pVisionCenter.a = Convert.ToDouble(Data[9]);
                pVisionCenter.b = Convert.ToDouble(Data[10]);
                pVisionCenter.c = Convert.ToDouble(Data[11]);
                //refMainfrm.pMyPlace = pVisionCenter;
                refMainfrm.myPnP.VisionCenter = pVisionCenter;

                pP1.x = Convert.ToDouble(Data[12]);
                pP1.y = Convert.ToDouble(Data[13]);
                pP1.z = Convert.ToDouble(Data[14]);
                pP1.a = Convert.ToDouble(Data[15]);
                pP1.b = Convert.ToDouble(Data[16]);
                pP1.c = Convert.ToDouble(Data[17]);

                refMainfrm.pDrinkSource[0] = pP1;

                pP2.x = Convert.ToDouble(Data[18]);
                pP2.y = Convert.ToDouble(Data[19]);
                pP2.z = Convert.ToDouble(Data[20]);
                pP2.a = Convert.ToDouble(Data[21]);
                pP2.b = Convert.ToDouble(Data[22]);
                pP2.c = Convert.ToDouble(Data[23]);

                refMainfrm.pDrinkSource[1] = pP2;

                pP3.x = Convert.ToDouble(Data[24]);
                pP3.y = Convert.ToDouble(Data[25]);
                pP3.z = Convert.ToDouble(Data[26]);
                pP3.a = Convert.ToDouble(Data[27]);
                pP3.b = Convert.ToDouble(Data[28]);
                pP3.c = Convert.ToDouble(Data[29]);

                refMainfrm.pDrinkSource[2] = pP3;

                pP4.x = Convert.ToDouble(Data[30]);
                pP4.y = Convert.ToDouble(Data[31]);
                pP4.z = Convert.ToDouble(Data[32]);
                pP4.a = Convert.ToDouble(Data[33]);
                pP4.b = Convert.ToDouble(Data[34]);
                pP4.c = Convert.ToDouble(Data[35]);

                refMainfrm.pDrinkSource[3] = pP4;

                pP5.x = Convert.ToDouble(Data[36]);
                pP5.y = Convert.ToDouble(Data[37]);
                pP5.z = Convert.ToDouble(Data[38]);
                pP5.a = Convert.ToDouble(Data[39]);
                pP5.b = Convert.ToDouble(Data[40]);
                pP5.c = Convert.ToDouble(Data[41]);

                refMainfrm.pDrinkSource[4] = pP5;

                pP6.x = Convert.ToDouble(Data[42]);
                pP6.y = Convert.ToDouble(Data[43]);
                pP6.z = Convert.ToDouble(Data[44]);
                pP6.a = Convert.ToDouble(Data[45]);
                pP6.b = Convert.ToDouble(Data[46]);
                pP6.c = Convert.ToDouble(Data[47]);

                refMainfrm.pDrinkSource[5] = pP6;

                pP7.x = Convert.ToDouble(Data[48]);
                pP7.y = Convert.ToDouble(Data[49]);
                pP7.z = Convert.ToDouble(Data[50]);
                pP7.a = Convert.ToDouble(Data[51]);
                pP7.b = Convert.ToDouble(Data[52]);
                pP7.c = Convert.ToDouble(Data[53]);

                refMainfrm.pDrinkSource[6] = pP7;

                
                pP8.x = Convert.ToDouble(Data[54]);
                pP8.y = Convert.ToDouble(Data[55]);
                pP8.z = Convert.ToDouble(Data[56]);
                pP8.a = Convert.ToDouble(Data[57]);
                pP8.b = Convert.ToDouble(Data[58]);
                pP8.c = Convert.ToDouble(Data[59]);


                refMainfrm.pDrinkSource[7] = pP8;

                pP9.x = Convert.ToDouble(Data[60]);
                pP9.y = Convert.ToDouble(Data[61]);
                pP9.z = Convert.ToDouble(Data[62]);
                pP9.a = Convert.ToDouble(Data[63]);
                pP9.b = Convert.ToDouble(Data[64]);
                pP9.c = Convert.ToDouble(Data[65]);
                refMainfrm.pMyStandBy2 = pP9;
                //refMainfrm.pDrinkSource[8] = pP9;
                

                JposStaby.theta1 = Convert.ToDouble(Data[66]);
                JposStaby.theta2 = Convert.ToDouble(Data[67]);
                JposStaby.theta3 = Convert.ToDouble(Data[68]);
                JposStaby.theta4 = Convert.ToDouble(Data[69]);
                JposStaby.theta5 = Convert.ToDouble(Data[70]);
                JposStaby.theta6 = Convert.ToDouble(Data[71]);

                
                pP11.x = Convert.ToDouble(Data[72]);
                pP11.y = Convert.ToDouble(Data[73]);
                pP11.z = Convert.ToDouble(Data[74]);
                pP11.a = Convert.ToDouble(Data[75]);
                pP11.b = Convert.ToDouble(Data[76]);
                pP11.c = Convert.ToDouble(Data[77]);

                pP12.x = Convert.ToDouble(Data[78]);
                pP12.y = Convert.ToDouble(Data[79]);
                pP12.z = Convert.ToDouble(Data[80]);
                pP12.a = Convert.ToDouble(Data[81]);
                pP12.b = Convert.ToDouble(Data[82]);
                pP12.c = Convert.ToDouble(Data[83]);




            }
            return 0;
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            PnP_Load();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "文字檔|*.txt";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();






                //string str_pStandby = pStandby.theta1.ToString() + "\t" + pStandby.theta2.ToString() + "\t" + pStandby.theta3.ToString() + "\t" +
                //                            pStandby.theta4.ToString() + "\t" + pStandby.theta5.ToString() + "\t" + pStandby.theta6.ToString() + "\n";


                //string str_pVisionCenter = pVisionCenter.theta1.ToString() + "\t" + pVisionCenter.theta2.ToString() + "\t" + pVisionCenter.theta3.ToString() + "\t" +
                //                            pVisionCenter.theta4.ToString() + "\t" + pVisionCenter.theta5.ToString() + "\t" + pVisionCenter.theta6.ToString() + "\n";

                //string str_pP1 = pP1.theta1.ToString() + "\t" + pP1.theta2.ToString() + "\t" + pP1.theta3.ToString() + "\t" +
                //                            pP1.theta4.ToString() + "\t" + pP1.theta5.ToString() + "\t" + pP1.theta6.ToString() + "\n";

                //string str_pP2 = pP2.theta1.ToString() + "\t" + pP2.theta2.ToString() + "\t" + pP2.theta3.ToString() + "\t" +
                //                            pP2.theta4.ToString() + "\t" + pP2.theta5.ToString() + "\t" + pP2.theta6.ToString() + "\n";

                //string str_pP3 = pP3.theta1.ToString() + "\t" + pP3.theta2.ToString() + "\t" + pP3.theta3.ToString() + "\t" +
                //                            pP3.theta4.ToString() + "\t" + pP3.theta5.ToString() + "\t" + pP3.theta6.ToString() + "\n";

                //string str_pP4 = pP4.theta1.ToString() + "\t" + pP4.theta2.ToString() + "\t" + pP4.theta3.ToString() + "\t" +
                //                            pP4.theta4.ToString() + "\t" + pP4.theta5.ToString() + "\t" + pP4.theta6.ToString() + "\n";



                //string str_pStandby = pStandby.x.ToString() + "\t" + pStandby.y.ToString() + "\t" + pStandby.z.ToString() + "\t" +
                //                            pStandby.a.ToString() + "\t" + pStandby.b.ToString() + "\t" + pStandby.c.ToString() + "\n";

                string str_jStandby = jStandby.theta1.ToString() + "\t" + jStandby.theta2.ToString() + "\t" + jStandby.theta3.ToString() + "\t" +
                                            jStandby.theta4.ToString() + "\t" + jStandby.theta5.ToString() + "\t" + jStandby.theta6.ToString() + "\n";

                string str_pVisionCenter = pVisionCenter.x.ToString() + "\t" + pVisionCenter.y.ToString() + "\t" + pVisionCenter.z.ToString() + "\t" +
                                            pVisionCenter.a.ToString() + "\t" + pVisionCenter.b.ToString() + "\t" + pVisionCenter.c.ToString() + "\n";

                string str_pP1 = pP1.x.ToString() + "\t" + pP1.y.ToString() + "\t" + pP1.z.ToString() + "\t" +
                                            pP1.a.ToString() + "\t" + pP1.b.ToString() + "\t" + pP1.c.ToString() + "\n";

                string str_pP2 = pP2.x.ToString() + "\t" + pP2.y.ToString() + "\t" + pP2.z.ToString() + "\t" +
                                            pP2.a.ToString() + "\t" + pP2.b.ToString() + "\t" + pP2.c.ToString() + "\n";

                string str_pP3 = pP3.x.ToString() + "\t" + pP3.y.ToString() + "\t" + pP3.z.ToString() + "\t" +
                                            pP3.a.ToString() + "\t" + pP3.b.ToString() + "\t" + pP3.c.ToString() + "\n";

                string str_pP4 = pP4.x.ToString() + "\t" + pP4.y.ToString() + "\t" + pP4.z.ToString() + "\t" +
                                            pP4.a.ToString() + "\t" + pP4.b.ToString() + "\t" + pP4.c.ToString() + "\n";
                string str_pP5 = pP5.x.ToString() + "\t" + pP5.y.ToString() + "\t" + pP5.z.ToString() + "\t" +
                                            pP5.a.ToString() + "\t" + pP5.b.ToString() + "\t" + pP5.c.ToString() + "\n";
                string str_pP6 = pP6.x.ToString() + "\t" + pP6.y.ToString() + "\t" + pP6.z.ToString() + "\t" +
                                            pP6.a.ToString() + "\t" + pP6.b.ToString() + "\t" + pP6.c.ToString() + "\n";
                string str_pP7 = pP7.x.ToString() + "\t" + pP7.y.ToString() + "\t" + pP7.z.ToString() + "\t" +
                                            pP7.a.ToString() + "\t" + pP7.b.ToString() + "\t" + pP7.c.ToString() + "\n";
                string str_pP8 = pP8.x.ToString() + "\t" + pP8.y.ToString() + "\t" + pP8.z.ToString() + "\t" +
                                            pP8.a.ToString() + "\t" + pP8.b.ToString() + "\t" + pP8.c.ToString() + "\n";


                string str_pP9 = pP9.x.ToString() + "\t" + pP9.y.ToString() + "\t" + pP9.z.ToString() + "\t" +
                                            pP9.a.ToString() + "\t" + pP9.b.ToString() + "\t" + pP9.c.ToString() + "\n";

                
                
                string str_jStandby2 = JposStaby.theta1.ToString() + "\t" + JposStaby.theta2.ToString() + "\t" + JposStaby.theta3.ToString() + "\t" +
                                            JposStaby.theta4.ToString() + "\t" + JposStaby.theta5.ToString() + "\t" + JposStaby.theta6.ToString() + "\n";
                
                
                
                
                string str_pP11 = pP11.x.ToString() + "\t" + pP11.y.ToString() + "\t" + pP11.z.ToString() + "\t" +
                                            pP11.a.ToString() + "\t" + pP11.b.ToString() + "\t" + pP11.c.ToString() + "\n";
                string str_pP12 = pP12.x.ToString() + "\t" + pP12.y.ToString() + "\t" + pP12.z.ToString() + "\t" +
                                            pP12.a.ToString() + "\t" + pP12.b.ToString() + "\t" + pP12.c.ToString() + "\n";



                char[] CharArray = str_jStandby.ToCharArray();
                char[] CharArray2 = str_pVisionCenter.ToCharArray();
                char[] CharArray3 = str_pP1.ToCharArray();
                char[] CharArray4 = str_pP2.ToCharArray();
                char[] CharArray5 = str_pP3.ToCharArray();
                char[] CharArray6 = str_pP4.ToCharArray();
                char[] CharArray7 = str_pP5.ToCharArray();
                char[] CharArray8 = str_pP6.ToCharArray();
                char[] CharArray9 = str_pP7.ToCharArray();
                char[] CharArray10 = str_pP8.ToCharArray();
                char[] CharArray11 = str_pP9.ToCharArray();
                char[] CharArray12 = str_jStandby2.ToCharArray();
                char[] CharArray13 = str_pP11.ToCharArray();
                char[] CharArray14 = str_pP12.ToCharArray();



                byte[] ByteArray = new byte[CharArray.Length];
                byte[] ByteArray2 = new byte[CharArray2.Length];
                byte[] ByteArray3 = new byte[CharArray3.Length];
                byte[] ByteArray4 = new byte[CharArray4.Length];
                byte[] ByteArray5 = new byte[CharArray5.Length];
                byte[] ByteArray6 = new byte[CharArray6.Length];

                byte[] ByteArray7 = new byte[CharArray7.Length];
                byte[] ByteArray8 = new byte[CharArray8.Length];
                byte[] ByteArray9 = new byte[CharArray9.Length];
                byte[] ByteArray10 = new byte[CharArray10.Length];

                byte[] ByteArray11 = new byte[CharArray11.Length];
                byte[] ByteArray12 = new byte[CharArray12.Length];
                byte[] ByteArray13 = new byte[CharArray13.Length];
                byte[] ByteArray14 = new byte[CharArray14.Length];


                for (int i = 0; i < CharArray.Length; i++)
                    ByteArray[i] = (byte)CharArray[i];
                for (int i = 0; i < CharArray2.Length; i++)
                    ByteArray2[i] = (byte)CharArray2[i];
                for (int i = 0; i < CharArray3.Length; i++)
                    ByteArray3[i] = (byte)CharArray3[i];
                for (int i = 0; i < CharArray4.Length; i++)
                    ByteArray4[i] = (byte)CharArray4[i];
                for (int i = 0; i < CharArray5.Length; i++)
                    ByteArray5[i] = (byte)CharArray5[i];
                for (int i = 0; i < CharArray6.Length; i++)
                    ByteArray6[i] = (byte)CharArray6[i];

                for (int i = 0; i < CharArray7.Length; i++)
                    ByteArray7[i] = (byte)CharArray7[i];
                for (int i = 0; i < CharArray8.Length; i++)
                    ByteArray8[i] = (byte)CharArray8[i];
                for (int i = 0; i < CharArray9.Length; i++)
                    ByteArray9[i] = (byte)CharArray9[i];
                for (int i = 0; i < CharArray10.Length; i++)
                    ByteArray10[i] = (byte)CharArray10[i];

                for (int i = 0; i < CharArray11.Length; i++)
                    ByteArray11[i] = (byte)CharArray11[i];
                for (int i = 0; i < CharArray12.Length; i++)
                    ByteArray12[i] = (byte)CharArray12[i];
                for (int i = 0; i < CharArray13.Length; i++)
                    ByteArray13[i] = (byte)CharArray13[i];
                for (int i = 0; i < CharArray14.Length; i++)
                    ByteArray14[i] = (byte)CharArray14[i];

                fs.Write(ByteArray, 0, ByteArray.Length);
                fs.Write(ByteArray2, 0, ByteArray2.Length);
                fs.Write(ByteArray3, 0, ByteArray3.Length);
                fs.Write(ByteArray4, 0, ByteArray4.Length);
                fs.Write(ByteArray5, 0, ByteArray5.Length);
                fs.Write(ByteArray6, 0, ByteArray6.Length);

                fs.Write(ByteArray7, 0, ByteArray7.Length);
                fs.Write(ByteArray8, 0, ByteArray8.Length);
                fs.Write(ByteArray9, 0, ByteArray9.Length);
                fs.Write(ByteArray10, 0, ByteArray10.Length);

                fs.Write(ByteArray11, 0, ByteArray11.Length);
                fs.Write(ByteArray12, 0, ByteArray12.Length);
                fs.Write(ByteArray13, 0, ByteArray13.Length);
                fs.Write(ByteArray14, 0, ByteArray14.Length);

                fs.Close();
            }
        }

        private void btn_PTP_Click(object sender, EventArgs e)
        {
            PointInf temp = new PointInf();
            JointInf jTemp = new JointInf();
            bool isJoint = false;


            if (ratioBtn_Wait.Checked)
            {
                jTemp = jStandby;
                isJoint = true;
            }
            else if (ratioBtn_VisionCenter.Checked)
                temp = pVisionCenter;
            else if (ratioBtn_P1.Checked)
                temp = pP1;
            else if (ratioBtn_P2.Checked)
                temp = pP2;
            else if (ratioBtn_P3.Checked)
                temp = pP3;
            else if (ratioBtn_P4.Checked)
                temp = pP4;
            else if (ratioBtn_P5.Checked)
                temp = pP5;
            else if (ratioBtn_P6.Checked)
                temp = pP6;
            else if (ratioBtn_P7.Checked)
                temp = pP7;
            else if (ratioBtn_P8.Checked)
                temp = pP8;

            else if (ratioBtn_P9.Checked)
                temp = pP9;


            else if (ratioBtn_P10.Checked)
                jTemp = JposStaby;


            else if (ratioBtn_P11.Checked)
                temp = pP11;
            else if (ratioBtn_P12.Checked)
                temp = pP12;


            if (isJoint)
            {
                refMainfrm.Move_J(jTemp, Convert.ToDouble(tb_Speed.Text));
            }
            else
            {
                refMainfrm.Move_P(temp, Convert.ToDouble(tb_Speed.Text));
            }

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {

            //refMainfrm.Move_MyMotionP(pStandby, pP1, pP2, pP3, pP4, Convert.ToDouble(tb_Speed.Text));

            //Convert.ToDouble(tb_WaitTime.Text);
           // refMainfrm.bStartMix = true;
            
        }

        private void ratioBtn_P1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            refMainfrm.Move_Shake(pP1, pP2, Convert.ToDouble(tb_Speed.Text));
        }
        public void nothing()
        {
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoDlg.refPointForm = this;
            InfoDlg.Show();
            

        }

        private void ratioBtn_VisionCenter_CheckedChanged(object sender, EventArgs e)
        {

        }


          }
}

