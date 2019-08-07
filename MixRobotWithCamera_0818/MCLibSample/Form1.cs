using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using ModbusGripper;
using Warp_Csharp;
using SlimDX_Joystick;

using RTIOMSMC_Name;

public struct Motion_Struct
{
    public int Type;
    public JointInf Joint1;
    public JointInf Joint2;
    public JointInf Joint3;
    public JointInf Joint4;
    public JointInf Joint5;
    public JointInf Joint6;

   
    public PointInf Point;
    public PointInf Point2;
    public PointInf Point3;
    public PointInf Point4;
    public PointInf Point5;
    public PointInf Point6;

    public PointInf Point7;
    public PointInf Point8;
    public PointInf Point9;

    public double center_x;
    public double center_y;
    public double center_z;

    public double Vel;
    public double CTtime;
}

public struct PnP_Struct
{
    public PointInf VisionCenter;
    public PointInf add(PointInf A, PointInf B)
    {
        PointInf ans = new PointInf();
        ans.x = A.x + B.x;
        ans.y = A.y + B.y;
        ans.z = A.z + B.z;
        ans.a = A.a + B.a;
        ans.b = A.b + B.b;
        ans.c = A.c + B.c;
        return ans;
    }
}


namespace MCLibSample
{
    public partial class Form1 : Form
    {

        //Define Motion Type
        public const int MOTION_TYPE_NOCMD = 0;
        public const int MOTION_TYPE_MOVEJ = 1;
        public const int MOTION_TYPE_MOVEP = 2;
        public const int MOTION_TYPE_MOVECIRCLE = 3;
        public const int MOTION_TYPE_PICKPLACE = 4;
        public const int MOTION_TYPE_GRIPEERCATCH = 5;
        public const int MOTION_TYPE_GRIPEERRELEASE = 6;
        public const int MOTION_TYPE_CYCLETEST = 7;
        public const int MOTION_TYPE_GETDRINK_J = 8;
        public const int MOTION_TYPE_PLACECUP_J = 9;
        public const int MOTION_TYPE_GETDRINK_P = 10;
        public const int MOTION_TYPE_PLACECUP_P = 11;

        

        public const int MOTION_TYPE_MYMOTIONJ = 12;
        public const int MOTION_TYPE_MYMOTIONP = 13;
        public const int MOTION_TYPE_GRIPEERCATCHING = 14;
        public const int MOTION_TYPE_MYSHAKE= 15;

        public const int CHECKCUP = 16;
       
        public const int MOTION_TYPE_GETDRINK_HOME2 = 20;
        public const int MOTION_STANBYP_2 = 21;

        public const int CONTROLMODE_FREE = 0;
        public const int CONTROLMODE_TELEOP = 1;
        public const int CONTROLMODE_AUTO = 2;

        public const int RCS_CONNECT_OK = 0;
        public const int RCS_CONNECT_ERR = 1;

        public bool unitchange = true;
        //Controller communication
        public E_MCLib_Error MCLiberr;
        public JointInf m_jointsinf;
        public double[] m_motorsinf;
        public PointInf m_casrteseaninf;
        public bool initialGripper = false;
        public IntPtr MCPTR;
        public int [] DigitialInput = new int[64];
        public double[] AnalogInput = new double[8];
        //public double[] Voltage = new double[8];

        public int[] AnalogVal = new int[8];
        /* Robotiq Gripper */
        RobotiqGripperInf Gripper = new RobotiqGripperInf();
        MyGripper MyModusGripper = new MyGripper();
        //RobotiqGripperInf Gripper2 = new RobotiqGripperInf();
        ForceSensorInf ForceSensor = new ForceSensorInf();
        public PnP_Struct myPnP = new PnP_Struct();


        /*Mutex && Thread && Delegate*/
        public static Mutex Motion_mut = new Mutex();
        public Thread GetControllerThread;
        public Thread UpdateUIThread;
        public Thread MotionThread;
        public Thread MixdrinkThread;

        public delegate void DelegateTextBox(TextBox ctl, string myStr);
        public delegate void DelegateTrackBar(TrackBar ctl, int val);


        public delegate void DelegateButton(Button ct1, bool show);
        public delegate void DelegateLabel(Label ct1, bool show);
        
        
        //public Drinking dsgf = new Drinking();
        #region variable For Dialog

        public PointForm PointFormDlg = new PointForm();
        public choosing chooseDlg = new choosing();
        public ImageMainfrm ImageFrm;// = new ImageMainfrm();
        public int SerialNum;
        public bool MyFlag;
        //public Drinking1

        
        public bool bStartMotion = false;

        #endregion



        //Others
        public bool Initial = false;
        public int radioButtonState = 0;
        public bool bWorking = false;
        public bool bPicking = false;
        public int GetObj = 0;


        public int temp = 0;


        public int ControlMode = 0;


        Motion_Struct myCmd = new Motion_Struct();

        System.Diagnostics.Process ConnectServer = new System.Diagnostics.Process();
        System.Diagnostics.Process ConnectManger = new System.Diagnostics.Process();

        //



        //Drink Point Information
        static bool FinalPos;
        public static bool FlagFinalPos
        {
            get
            {
                return FinalPos;
            }
            set
            {
                FinalPos = value;
            }

        }

        public choosing Functions ;
        public PointInf[] pDrinkSource = new PointInf[9];
        public int[] iChoose = new int[3];
        public double[] dGetDrinkTime = new double[8]; 
        public bool bStartMix = false;
        public double speedBart;
        public JointInf jMyStandBy = new JointInf();
        public PointInf pMyStandBy2 = new PointInf();
        public PointInf pMyPlace = new PointInf();
        public double Wait_Time;
        public int NumberofBeberages;
        public double DdrinkingTime;
        public bool finish_flag=false;

        public double[] VisionData = new double[7];

        
        //Joystick
        SlimDX.DirectInput.JoystickState state;
        MyJoystick MyJoy;
        public bool HaveJoystick;
        public bool useJoystick;
        string Joyinfo;
        public double[] limit = { -170, 170, -100, 135, -75, 100, -190, 190, -115, 115, -180, 180 };



/*
*********************************************************************************************************
*                          Form Function
*******************************************f**************************************************************
*/

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Initial)
            {
                MyJoy = new MyJoystick();
                HaveJoystick = !Convert.ToBoolean(MyJoy.InitDevices());

                //flag = true;
                HaveJoystick = false;
                bStartMix = false;

                m_motorsinf = new double[7];

                ConnectServer.StartInfo.FileName = @"ext\\LYSONMotionServer.exe";
                ConnectServer.Start();
                Thread.Sleep(100);
                ConnectManger.StartInfo.FileName = @"ext\\LYSONMotionManagement.exe";
                ConnectManger.Start();
                Thread.Sleep(100);

                radioButtonState = 0;

                // Setup RCS connect 
                MCLiberr = Set_RCS();

                if (MCLiberr != E_MCLib_Error.CONNECT_OK)
                    MessageBox.Show(MCLiberr.ToString());

                GetControllerThread = new Thread(new ThreadStart(tGetControllerThread));
                GetControllerThread.Priority = ThreadPriority.Highest;
                GetControllerThread.Name = String.Format("GetControllerThread");
                GetControllerThread.Start();

                MotionThread = new Thread(new ThreadStart(tMotionThreadFunc));
                MotionThread.Priority = ThreadPriority.AboveNormal;
                MotionThread.Name = String.Format("MotionThread");
                MotionThread.Start();

                UpdateUIThread = new Thread(new ThreadStart(tUpdateUI));
                UpdateUIThread.Priority = ThreadPriority.Lowest;
                UpdateUIThread.Name = String.Format("UpdateUI");
                UpdateUIThread.Start();

                MixdrinkThread = new Thread(new ThreadStart(tMixDrink));
                MixdrinkThread.Priority = ThreadPriority.Normal;
                MixdrinkThread.Name = String.Format("MixDrink");
                MixdrinkThread.Start();
                
                initialGripper = MyModusGripper.initial();


                btn_ServoOn.BackColor = System.Drawing.Color.Gray;

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                Thread.Sleep(100);
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);

                ControlMode = CONTROLMODE_FREE;

                label49.Visible = false;
                label50.Visible = false;
                label51.Visible = false;
                tb_Control7.Visible = false;
                tb_Control8.Visible = false;
                tb_Control9.Visible = false;
                SerialNum = 0;
                /*
                label58.Visible = false;
                tb_ControlPlat.Visible = false;
                btn_J7_Back.Visible = false;
                btn_J7_For.Visible = false;
                */

                bStartMotion = false;


                Initial = true;

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


            chooseDlg.Close();
            PointFormDlg.Close();

            //DrinkDlg.Close();

            if (!ConnectManger.HasExited)
            ConnectManger.Kill();
            Thread.Sleep(50);

            if (!ConnectServer.HasExited)
            ConnectServer.Kill();
            Thread.Sleep(50);

            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.DoMachineOff(MCPTR);

            if (UpdateUIThread != null && UpdateUIThread.IsAlive)
                UpdateUIThread.Abort();
            if (MotionThread != null && MotionThread.IsAlive)
                MotionThread.Abort();
            if (GetControllerThread != null && GetControllerThread.IsAlive)
                GetControllerThread.Abort();
            if (MixdrinkThread != null && MixdrinkThread.IsAlive)
                MixdrinkThread.Abort();

            MyModusGripper.close();


            if (MCLiberr == E_MCLib_Error.CONNECT_OK)
                MCLib.DoDisconnectFromController(MCPTR);

            if (MCPTR != null)
                MCLib.Destroy_MC(MCPTR);
        }

/*
*********************************************************************************************************
*                          User Function
*********************************************************************************************************
*/

        private void Get_Cmd(ref JointInf Joint)
        {
            Joint.theta1 = Convert.ToSingle(tb_Control1.Text);
            Joint.theta2 = Convert.ToSingle(tb_Control2.Text);
            Joint.theta3 = Convert.ToSingle(tb_Control3.Text);
            Joint.theta4 = Convert.ToSingle(tb_Control4.Text);
            Joint.theta5 = Convert.ToSingle(tb_Control5.Text);
            Joint.theta6 = Convert.ToSingle(tb_Control6.Text);
            

        }
        private void Get_Cmd(ref PointInf Point)
        {
            Point.x = Convert.ToSingle(tb_Control1.Text);
            Point.y = Convert.ToSingle(tb_Control2.Text);
            Point.z = Convert.ToSingle(tb_Control3.Text);
            Point.a = Convert.ToSingle(tb_Control4.Text);
            Point.b = Convert.ToSingle(tb_Control5.Text);
            Point.c = Convert.ToSingle(tb_Control6.Text);
        }
        private void Show_Pos(PointInf Point, double Yoffset)
        {
            Show_TextBox(tb_posX, Convert.ToString(Math.Round(Point.x, 3)));
            Show_TextBox(tb_posY, Convert.ToString(Math.Round(Point.y , 3)));
            Show_TextBox(tb_posZ, Convert.ToString(Math.Round(Point.z, 3)));
            Show_TextBox(tb_posA, Convert.ToString(Math.Round(Point.a, 3)));
            Show_TextBox(tb_posB, Convert.ToString(Math.Round(Point.b, 3)));
            Show_TextBox(tb_posC, Convert.ToString(Math.Round(Point.c, 3)));
        }

        private void Show_Pos(PointInf Point)
        {
            Show_TextBox(tb_posX, Convert.ToString(Math.Round(Point.x, 3)));
            Show_TextBox(tb_posY, Convert.ToString(Math.Round(Point.y, 3)));
            Show_TextBox(tb_posZ, Convert.ToString(Math.Round(Point.z, 3)));
            Show_TextBox(tb_posA, Convert.ToString(Math.Round(Point.a, 3)));
            Show_TextBox(tb_posB, Convert.ToString(Math.Round(Point.b, 3)));
            Show_TextBox(tb_posC, Convert.ToString(Math.Round(Point.c, 3)));
        }

        private void Show_Joint(JointInf Joint)
        {
            Show_TextBox(tb_Axis1, Convert.ToString(Math.Round(Joint.theta1, 3)));
            Show_TextBox(tb_Axis2, Convert.ToString(Math.Round(Joint.theta2, 3)));
            Show_TextBox(tb_Axis3, Convert.ToString(Math.Round(Joint.theta3, 3)));
            Show_TextBox(tb_Axis4, Convert.ToString(Math.Round(Joint.theta4, 3)));
            Show_TextBox(tb_Axis5, Convert.ToString(Math.Round(Joint.theta5, 3)));
            Show_TextBox(tb_Axis6, Convert.ToString(Math.Round(Joint.theta6, 3)));
        }

        private void Show_Joint(double[] motor)
        {
            Show_TextBox(tb_Axis1, Convert.ToString(Math.Round(motor[0], 3)));
            Show_TextBox(tb_Axis2, Convert.ToString(Math.Round(motor[1], 3)));
            Show_TextBox(tb_Axis3, Convert.ToString(Math.Round(motor[2], 3)));
            Show_TextBox(tb_Axis4, Convert.ToString(Math.Round(motor[3], 3)));
            Show_TextBox(tb_Axis5, Convert.ToString(Math.Round(motor[4], 3)));
            Show_TextBox(tb_Axis6, Convert.ToString(Math.Round(motor[5], 3)));

        }

        private void Show_TextBox(TextBox ctl, string myStr)
        {
            if (this.InvokeRequired)
            {
                DelegateTextBox UpdateTextBox = new DelegateTextBox(Show_TextBox);
                this.Invoke(UpdateTextBox, ctl, myStr);
            }
            else
                ctl.Text = myStr;
        }

        private void Show_TrackBar(TrackBar ctl, int val)
        {
            if (this.InvokeRequired)
            {
                DelegateTrackBar UpdateTrackBar = new DelegateTrackBar(Show_TrackBar);
                this.Invoke(UpdateTrackBar, ctl, val);
            }
            else
                ctl.Value = val;
        }


        private void UpdateIO()
        {
            temp = 1;
            for (int iindex = 0; iindex < 16; iindex++)
                MCLib.GetDigitialInput(MCPTR, iindex, ref DigitialInput[iindex]);


            MCLib.GetForceSensor(MCPTR, ref ForceSensor);
            AnalogInput[0] = ForceSensor.Fx;
            AnalogInput[1] = ForceSensor.Fy;
            AnalogInput[2] = ForceSensor.Fz;


            //for (int i = 0; i < 8; i++)
            //{
            //    MCLib.GetAnalogInput(MCPTR, i, ref AnalogInput[i]);
            //    AnalogVal[i] = Convert.ToInt32(AnalogInput[i] * 10);
            //}
            AnalogVal[0] = Convert.ToInt32(ForceSensor.Fx / 10);
            AnalogVal[1] = Convert.ToInt32(ForceSensor.Fy / 10);
            AnalogVal[2] = Convert.ToInt32(ForceSensor.Fz / 10);
            AnalogVal[3] = Convert.ToInt32(ForceSensor.Tx / 10);
            AnalogVal[4] = Convert.ToInt32(ForceSensor.Ty / 10);
            AnalogVal[5] = Convert.ToInt32(ForceSensor.Tz / 10);



            Show_TextBox(tB_DI0, DigitialInput[0].ToString());
            Show_TextBox(tB_DI1, DigitialInput[1].ToString());
            Show_TextBox(tB_DI2, DigitialInput[2].ToString());
            Show_TextBox(tB_DI3, DigitialInput[3].ToString());
            Show_TextBox(tB_DI4, DigitialInput[4].ToString());
            Show_TextBox(tB_DI5, DigitialInput[5].ToString());
            Show_TextBox(tB_DI6, DigitialInput[6].ToString());
            Show_TextBox(tB_DI7, DigitialInput[7].ToString());
            Show_TextBox(tB_DI8, DigitialInput[8].ToString());
            Show_TextBox(tB_DI9, DigitialInput[9].ToString());
            Show_TextBox(tB_DI10, DigitialInput[10].ToString());
            Show_TextBox(tB_DI11, DigitialInput[11].ToString());
            Show_TextBox(tB_DI12, DigitialInput[12].ToString());
            Show_TextBox(tB_DI13, DigitialInput[13].ToString());
            Show_TextBox(tB_DI14, DigitialInput[14].ToString());
            Show_TextBox(tB_DI15, DigitialInput[15].ToString());



        }


        //private void GetEnc(ref Joint_Info Joint)
        //{
        //    Joint.t1 = m_jointsinf.JointsRes[0];
        //    Joint.t2 = m_jointsinf.JointsRes[1];
        //    Joint.t3 = m_jointsinf.JointsRes[2];
        //    Joint.t4 = m_jointsinf.JointsRes[3];
        //    Joint.t5 = m_jointsinf.JointsRes[4];
        //    Joint.t6 = m_jointsinf.JointsRes[5];
        //}

        private void Slide_Override_Scroll(object sender, EventArgs e)
        {
            MCLib.SetFeedScale(MCPTR, Slide_Override.Value);
            tb_Override.Text = Convert.ToString(Slide_Override.Value);
        }

        public int Move_CYCLE(JointInf Joint1, JointInf Joint2, JointInf Joint3, JointInf Joint4, double Vel)
        {
            Motion_mut.WaitOne();
            myCmd.Joint1 = Joint1;
            myCmd.Joint2 = Joint2;
            myCmd.Joint3 = Joint3;
            myCmd.Joint4 = Joint4;
            myCmd.Vel = Vel;
            myCmd.CTtime = 10;
            myCmd.Type = MOTION_TYPE_CYCLETEST;
            Motion_mut.ReleaseMutex();
            return 0;
        }
        public int Move_J(JointInf Joint, double Vel)
        {
            Motion_mut.WaitOne();
            myCmd.Joint1 = Joint;
            myCmd.Vel = Vel;
            myCmd.Type = MOTION_TYPE_MOVEJ;
            Motion_mut.ReleaseMutex();
            return 0;
        }

        public int Move_P(PointInf Cmd, double Vel)
        {

            Motion_mut.WaitOne();
            myCmd.Point = Cmd;
            myCmd.Vel = Vel;
            myCmd.Type = MOTION_TYPE_MOVEP;

            Motion_mut.ReleaseMutex();

            return 0;
        }

        public int Move_Circle(PointInf Cmd, double Vel, double center_x, double center_y, double center_z)
        {

            Motion_mut.WaitOne();
            myCmd.Point = Cmd;
            myCmd.Vel = Vel;
            myCmd.center_x = center_x;
            myCmd.center_y = center_y;
            myCmd.center_z = center_z;
            myCmd.Type = MOTION_TYPE_MOVECIRCLE;

            Motion_mut.ReleaseMutex();

            return 0;
        }

        public void GripperCatch()
        {
            Motion_mut.WaitOne();
            myCmd.Type = MOTION_TYPE_GRIPEERCATCH;
            Motion_mut.ReleaseMutex();
        }

        public void GripperRelease()
        {
            Motion_mut.WaitOne();
            myCmd.Type = MOTION_TYPE_GRIPEERRELEASE;
            Motion_mut.ReleaseMutex();
        }

        public int Move_ToDrink_P(PointInf pDrink, double Vel, double WaitTime)
        {
            Motion_mut.WaitOne();
            myCmd.Point = pDrink;
            myCmd.Vel = Vel;
            myCmd.CTtime = WaitTime;
            myCmd.Type = MOTION_TYPE_GETDRINK_P;

            Motion_mut.ReleaseMutex();

            return 0;
        }
        public int MoveToHome2(JointInf joint, double Vel, double WaitTime)
        {
            Motion_mut.WaitOne();
            myCmd.Joint1 = joint;
            myCmd.Vel = Vel;
            myCmd.CTtime = WaitTime;
            myCmd.Type = MOTION_TYPE_GETDRINK_HOME2;

            Motion_mut.ReleaseMutex();

            return 0;
        }

       public int Move_ToDrink_J(JointInf jDrink, JointInf jStandby, double Vel, int WaitTime)
        {
            Motion_mut.WaitOne();

            myCmd.Joint1 = jStandby;
            myCmd.Joint2 = jDrink;

            myCmd.Vel = Vel;
            myCmd.CTtime = WaitTime;
            myCmd.Type = MOTION_TYPE_GETDRINK_J;

            Motion_mut.ReleaseMutex();

            return 0;
        }

       
        public int Move_ToPlace_P(PointInf pPlace, JointInf jStandby,PointInf PointInfpMyStandBy2, double Vel)
        {
            PointInf realplace = new PointInf();
            realplace = myPnP.add(pPlace, myPnP.VisionCenter);

            Motion_mut.WaitOne();
            myCmd.Point = realplace;
            myCmd.Point2 = PointInfpMyStandBy2;
            myCmd.Joint1 = jStandby;
            myCmd.Vel = Vel;
            myCmd.Type = MOTION_TYPE_PLACECUP_P;

            Motion_mut.ReleaseMutex();

            return 0;
        }
        
       

        public int Move_ToPlace_J(JointInf jPlace, JointInf jStandby, double Vel)
        {
            Motion_mut.WaitOne();
            myCmd.Joint1 = jStandby;
            myCmd.Joint2 = jPlace;
            myCmd.Vel = Vel;
            myCmd.Type = MOTION_TYPE_PLACECUP_J;

            Motion_mut.ReleaseMutex();

            return 0;
        }

        public void Move_MyMotionJ(JointInf jStandby, JointInf jP1, JointInf jP2, JointInf jP3, JointInf jP4, double vel)
        { 

            Motion_mut.WaitOne();
            myCmd.Joint1 = jStandby;
            myCmd.Joint2 = jP1;
            myCmd.Joint3 = jP2;
            myCmd.Joint4 = jP3;
            myCmd.Joint5 = jP4;
            myCmd.Vel = vel;
            myCmd.Type = MOTION_TYPE_MYMOTIONJ;
            Motion_mut.ReleaseMutex();


        }
       
        public void Move_MyMotionP(PointInf pStandby, PointInf pP1, PointInf pP2, PointInf pP3, PointInf pPlace, double vel)
        {

            Motion_mut.WaitOne();
            myCmd.Point = pStandby;
            myCmd.Point2 = pP1;
            myCmd.Point3 = pP2;
            myCmd.Point4 = pP3;
            myCmd.Point5 = pPlace;
            myCmd.Vel = vel;
            myCmd.Type = MOTION_TYPE_MYMOTIONP;
            Motion_mut.ReleaseMutex();


        }
        public void Move_Shake(PointInf pP1, PointInf pP2, double vel)
        {
            Motion_mut.WaitOne();
            
            myCmd.Point = pP1;
            myCmd.Point3 = pP2;
            
            myCmd.Vel = vel;
            myCmd.Type = MOTION_TYPE_MYSHAKE;
            Motion_mut.ReleaseMutex();
        
        }

        public void Move_Shake1(PointInf pP1, PointInf pP2, double vel)
        {
            Motion_mut.WaitOne();

            myCmd.Point2 = pP1;
            myCmd.Point3 = pP2;

            myCmd.Vel = vel;
            myCmd.Type = MOTION_TYPE_MYSHAKE;
            Motion_mut.ReleaseMutex();

        }

      
        

        public void Move_Order(double[] BeveragePosition)
        {
            Motion_mut.WaitOne();

           
            Motion_mut.ReleaseMutex();
        
        }
/*
*********************************************************************************************************
*                          Thread Functions
*********************************************************************************************************
*/
        private void tGetControllerThread()
        {

            while (true)
            {

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.UpdateStatus(MCPTR);                 //  Update Status, must call this function 
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.UpdateError(MCPTR);
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) bWorking = !(Convert.ToBoolean(MCLib.GetMotionStatus(MCPTR)));

                UpdateIO();

                if (HaveJoystick) JoystickButton();
                if ( useJoystick ) JoystickControl();

                //GripperControl();

                //ForceEmgStop();

                Thread.Sleep(10);
            }
        }

        private void tUpdateUI()
        {
            while (true)
            {

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.GetJointsPosRes(MCPTR, ref m_jointsinf);
                for (int i = 0; i < 7; i++)
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.GetMotorsEncoder(MCPTR, i, ref m_motorsinf[i]);
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.GetCartesianPosRes(MCPTR, ref m_casrteseaninf);

                if(unitchange)
                    Show_Joint(m_jointsinf);
                else
                    Show_Joint(m_motorsinf);

                Show_Pos(m_casrteseaninf, m_jointsinf.theta7);
                //Show_Pos(m_casrteseaninf);

                if (bWorking)
                    Show_TextBox(tb_RobotStatus, "Moving...");
                else
                    Show_TextBox(tb_RobotStatus, "In Position");

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.RobotiqGripperStat(MCPTR, ref Gripper);
                if (initialGripper)
                {
                    MyModusGripper.ReadStatus(ref Gripper);
                }
                else
                    initialGripper = false;
                Show_TextBox(tb_GripStatus, Convert.ToString(Gripper.Status));
                Show_TextBox(tb_GripErr, Convert.ToString(Gripper.Error));
                Show_TextBox(tb_GripPosReq, Convert.ToString(Gripper.PosReq));
                Show_TextBox(tb_GripAP, Convert.ToString(Gripper.ActualPos));
                Show_TextBox(tb_GripCurrent, Convert.ToString(Gripper.Current));
                Show_TextBox(tb_GripObjDet, Convert.ToString(Gripper.objDetect));

 

                Thread.Sleep(100);
            }
        }
        public void myExternalFunction()
        {
            if (System.Windows.Forms.Application.OpenForms["choosing"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["choosing"] as choosing).Myfunction();
            }
        
        }
        public int checkcup(PointInf pcup_place, JointInf jMyStandBy, double Vel)
        {
            Motion_mut.WaitOne();
            myCmd.Point = pcup_place;
            myCmd.Vel = Vel;
            myCmd.Joint1 = jMyStandBy;
            //myCmd.CTtime = WaitTime;
            myCmd.Type = CHECKCUP;

            Motion_mut.ReleaseMutex();

            return 0;
        }


        private void tMixDrink()
        {
            int iGetDrinkPoint;
            for (int i = 0; i < dGetDrinkTime.Length; i++)
                dGetDrinkTime[i] = 2000;
            //bool home2 = true;
           // double vel = 4000;
            double total_Time = 0;
           // double total_Time = 6000.00 * 1;
           // DdrinkingTime = 4000;
           
            while (true)
            {
                if (bStartMix)
                {
                    if (Convert.ToInt16(tb_GripObjDet.Text) == 0)
                    {
                        ReleaseObject(255, 0);
                        checkcup(pMyStandBy2, jMyStandBy, speedBart);
                        Thread.Sleep(500);
                    }
                     total_Time = Wait_Time * 3;
                     DdrinkingTime = (double)total_Time / NumberofBeberages;
                    
                     for (int i = 0; i < 3; i++)
                        {
                            iGetDrinkPoint = iChoose[i];
                            if (iGetDrinkPoint != 0)
                            {
                                iChoose[i] = 0;
                                Move_ToDrink_P(pDrinkSource[iGetDrinkPoint - 1], speedBart, DdrinkingTime);

                                Thread.Sleep(500);
                                while (bStartMotion)
                                    Thread.Sleep(100);
                            }
                        }
                     
                     MyFlag = true;
                     ImageFrm.softwarecontinumode();
                     Thread.Sleep(500);
                    
                    
                    
                     while (VisionData[0] == 0)
                     Thread.Sleep(100);                    

                     pMyPlace.x = VisionData[1];
                     pMyPlace.y = VisionData[2];
                     pMyPlace.z = VisionData[3];
                     pMyPlace.a = VisionData[4];
                     pMyPlace.b = VisionData[5];
                     pMyPlace.c = VisionData[6];



                     //if (pMyPlace.x != 0 && pMyPlace.y != 0 && pMyPlace.z != 0 && pMyPlace.a != 0 && pMyPlace.b != 0 && pMyPlace.c != 0)
                     //{
                         
                     //}
                     ImageFrm.softwaretriggermode();
                     Move_ToPlace_P(pMyPlace, jMyStandBy, pMyStandBy2, speedBart);

                     Thread.Sleep(500);
                     while (bStartMotion)
                         Thread.Sleep(100);
                     for (int i = 0; i < 7; i++)
                         VisionData[i] = 0;
                    // Move_To_StanyPoint_2(pMyStandBy2, speedBart);
                     Thread.Sleep(500);
                     //SerialNum = 0;
                     ScreenReset();
                     //SerialNum++;
                     bStartMix = false;
                     //ImageFrm.softwarecontinumode();
                     
                    
                    }
                }
            
        }
       
        private void ButtonStatus(Button ctl, bool show)
        {
            if (this.InvokeRequired)
            {
                DelegateButton UpdateButton = new DelegateButton(ButtonStatus);
                this.Invoke(UpdateButton, ctl, show);
            }
            else
            {
                if (show)
                {
                    if (!ctl.Visible)
                    {
                        ctl.Visible = true;
                    }
                }
                else
                {
                    if (ctl.Visible)
                    {
                        ctl.Visible = false;
                    }
                }
            }

        }




        private void LabelStatus(Label ctl, bool show)
        {
            if (this.InvokeRequired)
            {
                DelegateLabel UpdateLabel = new DelegateLabel(LabelStatus);
                this.Invoke(UpdateLabel, ctl, show);
            }
            else
            {
                if (show)
                {
                    if (!ctl.Visible)
                    {
                        ctl.Visible = true;
                    }
                }
                else
                {
                    if (ctl.Visible)
                    {
                        ctl.Visible = false;
                    }
                }
            }

        }
        private void ScreenReset()
        {
         //   chooseDlg.flagfinish = true;
            ButtonStatus(chooseDlg.bPrepare_By_Me, true);
            ButtonStatus(chooseDlg.bpredefine, true);
            ButtonStatus(chooseDlg.bFinish, false);
            LabelStatus(chooseDlg.lwait,false);
            //chooseDlg.bPrepare_By_Me.Show();
            //chooseDlg.bpredefine.Show();
            //chooseDlg.bFinish.Hide();
            chooseDlg.State = false; 
        }

        private void tMotionThreadFunc()
        {

            Motion_Struct MotionCmd = new Motion_Struct();

            while (true)
            {
                Motion_mut.WaitOne();
                MotionCmd = myCmd;
                myCmd.Type = 0;
                Motion_mut.ReleaseMutex();
                switch (MotionCmd.Type)
                {
                    case MOTION_TYPE_NOCMD:
                        //No Cmd
                        break;
                    case MOTION_TYPE_MOVEJ:
                        //Joint
                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                            Thread.Sleep(200);
                        }

                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);
                        MotionWait();

                        break;
                    case MOTION_TYPE_MOVEP:
                        //Joint
                        //MoveFunc(MotionCmd.Joint1);
                       // MCLib
                        //MotionWait();
                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(200);
                        }
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);

                        break;
                    case MOTION_TYPE_MOVECIRCLE:
                        MCLib.MoveCircleAbs(MCPTR, MotionCmd.Point, MotionCmd.center_x, MotionCmd.center_y, MotionCmd.center_z,
                                             0, 0, 1, MotionCmd.Vel);
                        break;

                    case MOTION_TYPE_CYCLETEST:

                        for (int i = 0; i < MotionCmd.CTtime; i++)
                        {
                            MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);
                            MotionWait();

                            MoveFunc(MotionCmd.Joint2, MotionCmd.Vel);
                            MotionWait();

                            MoveFunc(MotionCmd.Joint3, MotionCmd.Vel);
                            MotionWait();

                            MoveFunc(MotionCmd.Joint4, MotionCmd.Vel);
                            MotionWait();
                        }

                        break;

                    case MOTION_TYPE_PICKPLACE:

                        bPicking = true;
                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);      //移到Pick點上方
                        MotionWait();

                        MoveFunc(MotionCmd.Joint2, MotionCmd.Vel);      //移到Pick點
                        MotionWait();

                        //取物
                        GetObj = CatchObject(150, 0);
                        //Air_Ctrl(true);
                        //Thread.Sleep(500);
                        if (GetObj == 0)
                        {
                            MoveFunc(MotionCmd.Joint3, MotionCmd.Vel);      //移到Pick點上空
                            MotionWait();

                            MoveFunc(MotionCmd.Joint4, MotionCmd.Vel);      //移到Place點上空
                            MotionWait();

                            MoveFunc(MotionCmd.Joint5, MotionCmd.Vel);      //移到Place點
                            MotionWait();
                        }
                        //置放物件
                        //Air_Ctrl(false);
                        //Thread.Sleep(500);
                        ReleaseObject(255, 0);

                        if (GetObj == 0)
                        {
                            MoveFunc(MotionCmd.Joint6, MotionCmd.Vel);      //移到Place點上空
                            MotionWait();
                        }
                        else
                        {
                            MoveFunc(MotionCmd.Joint3, MotionCmd.Vel);      //移到Pick點上空
                            MotionWait();
                        }
                        /*
                        MoveFunc(MotionCmd.Joint6, MotionCmd.Vol);      //移到standby點
                        MotionWait();
                        */


                        bPicking = false;

                        break;

                    case MOTION_TYPE_GRIPEERCATCH:
                        CatchObject(150, 0);
                        break;

                    case MOTION_TYPE_GRIPEERRELEASE:
                        ReleaseObject(255, 0);
                        break;
                    case MOTION_TYPE_GRIPEERCATCHING:
                        catching();
                        
                        break;
                    case MOTION_TYPE_MYSHAKE:
                      
                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(100);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                            //Thread.Sleep(50);
                            MotionWait();

                            MCLib.MoveLineAbs(MCPTR, MotionCmd.Point3, MotionCmd.Vel);
                            MotionWait();
                        }
                            // Shake();
                                           
                        
                        break;

                   
                    case CHECKCUP :
                        bStartMotion = true;
                        
                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(100);
                        }

                        
                        MotionCmd.Point.z += 100;
                        MotionWait();
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        MotionCmd.Point.z -= 100; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();
                        CatchObject(255, 255);

                        MotionWait();
                        MotionCmd.Point.z += 100; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        MotionWait();

                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                            Thread.Sleep(200);
                        }

                        MCLib.MoveABSJointsSyn(MCPTR, MotionCmd.Joint1, MotionCmd.Vel);
                        Thread.Sleep(100);
                        MotionWait();


                        bStartMotion = false;

                        break;





                    case MOTION_TYPE_GETDRINK_P:

                        bStartMotion = true;
                        
                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(100);
                        }

                        
                        MotionCmd.Point.z -= 100;
                        MotionWait();
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        MotionCmd.Point.z += 100; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);

                        Thread.Sleep(Convert.ToInt32(MotionCmd.CTtime));     //取酒等待時間

                        MotionWait();
                        MotionCmd.Point.z -= 100; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        MotionWait();

                        bStartMotion = false;

                        break;
                    case MOTION_STANBYP_2:

                        bStartMotion = true;

                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(100);
                        }


                        MotionCmd.Point.z -= 100;
                        MotionWait();
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        MotionCmd.Point.z += 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        CatchObject(100, 100);
                        Thread.Sleep(Convert.ToInt32(MotionCmd.CTtime));     //取酒等待時間

                        MotionWait();
                        MotionCmd.Point.z -= 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        MotionWait();

                        bStartMotion = false;

                        break;


                    case MOTION_TYPE_GETDRINK_HOME2:
                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                        }

                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);      //移到StandBy 
                        MotionWait();
                        break;
                    case MOTION_TYPE_PLACECUP_P:

                        bStartMotion = true;

                        //FREE_MODE

                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                        }

                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);      //移到StandBy 
                        MotionWait();
                        double aux = MotionCmd.Joint1.theta1;
                        double aux1 = MotionCmd.Joint1.theta2;
                        //MotionCmd.Joint1.theta1 =0;
                        MotionCmd.Joint1.theta4 -= 15;
                        //MotionCmd.Joint1.theta2 -= 15;
                        MotionCmd.Joint1.theta6 -= 15;
                        //MotionCmd.Joint1.theta2 = 1;
                        //MotionCmd.Joint1.theta3 = 55;
                        //MotionCmd.Joint1.theta4 = 7;
                        //MotionCmd.Joint1.theta5 = 74;
                        //MotionCmd.Joint1.theta6 = 3;
                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel- 300);     
                        MotionWait();

                        //MotionCmd.Joint1.theta4 -= 15;
                        //MotionCmd.Joint1.theta6 -= 15;
                        //MotionWait();
                      //  MotionCmd.Joint1.theta2 -= 10;
                        
                        for (int i = 0; i < 2; i++)
                        {
                            MotionCmd.Joint1.theta4 += 30;
                            MotionCmd.Joint1.theta6 += 30;
                            //MotionCmd.Joint1.theta1 -= 8;
                            //MotionCmd.Joint1.theta2 += 3;
                            MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);      //移到StandBy 
                            MotionWait();

                            MotionCmd.Joint1.theta4 -= 30;
                            MotionCmd.Joint1.theta6 -= 30;
                           // MotionCmd.Joint1.theta2 -= 5;
                            MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);      //移到StandBy 
                            MotionWait();

                        }
                        MotionCmd.Joint1.theta4 += 15;
                        MotionCmd.Joint1.theta6 += 15;
                        //MotionCmd.Joint1.theta1 = aux;
                        //MotionCmd.Joint1.theta2 = aux1;
                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel);
                            //AUTO MODE
                        MotionWait();
                         
                            if (ControlMode != CONTROLMODE_AUTO)
                            {
                                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                                ControlMode = CONTROLMODE_AUTO;
                                Thread.Sleep(1000);
                            }

                        MotionCmd.Point.z += 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        MotionCmd.Point.z -= 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        ReleaseObject(255, 0);
                        Thread.Sleep(1000); 

                        MotionCmd.Point.z += 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();

                        //////////////////////////////////////////////////////////////
                        MotionCmd.Point2.z += 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();
                        MotionCmd.Point2.z -= 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();
                        CatchObject(255, 255);
                         MotionCmd.Point2.z += 100;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        Thread.Sleep(50);
                        MotionWait();
                        //FREE_MODE

                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                        }

                        MotionWait();

                        MoveFunc(MotionCmd.Joint1, MotionCmd.Vel); 
                      
                        MotionWait();
                         
                        bStartMotion = false;
                        
                        break;

                    case MOTION_TYPE_MYMOTIONJ:

                        if (ControlMode != CONTROLMODE_FREE)
                        {
                            MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                            Thread.Sleep(50);
                            MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                            ControlMode = CONTROLMODE_FREE;
                        }

                        MoveFunc(MotionCmd.Joint2, MotionCmd.Vel);      //移到取酒前點
                        MotionWait();

                        MoveFunc(MotionCmd.Joint3, MotionCmd.Vel/2.0);      //移到取酒點
                        MotionWait();

                        Thread.Sleep(2000);                             //取酒等待時間

                        MoveFunc(MotionCmd.Joint2, MotionCmd.Vel);      //移到取酒前點
                        MotionWait();

                        MoveFunc(MotionCmd.Joint4, MotionCmd.Vel);      //移到放置前點
                        MotionWait();

                        MoveFunc(MotionCmd.Joint5, MotionCmd.Vel/2.0);      //移到放置點
                        MotionWait();

                        Thread.Sleep(2000);                             //放酒等待時間

                        MoveFunc(MotionCmd.Joint4, MotionCmd.Vel);      //移到放置前點
                        MotionWait();

                        MoveFunc(MotionCmd.Joint5, MotionCmd.Vel);      //移到等待點
                        MotionWait();



                        break;
                    case MOTION_TYPE_MYMOTIONP:

                        bStartMotion = true;
                        //AUTO MODE

                        if (ControlMode != CONTROLMODE_AUTO)
                        {
                            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                            ControlMode = CONTROLMODE_AUTO;
                            Thread.Sleep(100);
                        }

                        ///
                        ///First Drink
                        ///
                        MotionCmd.Point2.z -= 50; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        //Thread.Sleep(50);
                        MotionWait();
                        //MotionWait();

                        MotionCmd.Point2.z += 50; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        MotionWait();

                        Thread.Sleep(Convert.ToInt32(MotionCmd.CTtime));     //取酒等待時間

                        //MotionWait();
                        MotionCmd.Point2.z -= 70; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point2, MotionCmd.Vel);
                        MotionWait();


                        ///
                        ///Second Drink
                        ///
                        MotionCmd.Point3.z -= 50; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point3, MotionCmd.Vel);
                        MotionWait();
                        //MotionWait();

                        MotionCmd.Point3.z += 50; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point3, MotionCmd.Vel);
                        MotionWait();

                        Thread.Sleep(Convert.ToInt32(MotionCmd.CTtime));     //取酒等待時間

                        //MotionWait();
                        MotionCmd.Point3.z -= 60; 
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point3, MotionCmd.Vel);
                        MotionWait();


                        ///
                        ///Third Drink
                        ///
                        MotionCmd.Point4.z -= 50;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point4, MotionCmd.Vel);
                        MotionWait();
                        //MotionWait();

                        MotionCmd.Point4.z += 50;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point4, MotionCmd.Vel);
                        MotionWait();

                        Thread.Sleep(Convert.ToInt32(MotionCmd.CTtime));     //取酒等待時間

                        //MotionWait();
                        MotionCmd.Point4.z -= 60;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point4, MotionCmd.Vel);
                        MotionWait();


                        ///
                        /// Go To Place
                        /// 
                        MotionCmd.Point5.z += 50;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point5, MotionCmd.Vel);
                        MotionWait();
                        //MotionWait();

                        MotionCmd.Point5.z -= 60;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point5, MotionCmd.Vel);
                        MotionWait();

                        ReleaseObject(255, 0);
                        Thread.Sleep(1000);         //放下的延遲


                        MotionCmd.Point5.z += 70;
                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point5, MotionCmd.Vel);
                        MotionWait();


                        MCLib.MoveLineAbs(MCPTR, MotionCmd.Point, MotionCmd.Vel);
                        MotionWait();

                        bStartMotion = false;
                        break;




                }

                Thread.Sleep(5);

            }

        }


        private void GripperControl()
        {
            if (DigitialInput[1] == 1)
            {
                Gripper.PosCmd += 10;
                if (Gripper.PosCmd > 255)
                    Gripper.PosCmd = 255;
                Thread.Sleep(10);
                //MCLib.RobotiqGripperCmd(MCPTR, Gripper);
                MyModusGripper.GoToReqPos(Gripper);
            }
            else if (DigitialInput[2] == 1)
            {
                Gripper.PosCmd -= 10;
                if (Gripper.PosCmd < 0)
                    Gripper.PosCmd = 0;
                Thread.Sleep(10);
                MyModusGripper.GoToReqPos( Gripper);
               // MCLib.RobotiqGripperCmd(MCPTR, Gripper);
            }        

        }
       
        private void ForceEmgStop()
        { 
            //oldForce[0
        
        }

        private E_MCLib_Error Set_RCS()
        {
            string temp2 = "opintf\0";
            char[] temp = temp2.ToCharArray();

            E_MCLib_Error err = E_MCLib_Error.CONNECT_ERROR;
            MCPTR = MCLib.Create_MC();
            err = MCLib.DoConnectToController(MCPTR, temp);
            MCLib.SetTool(MCPTR, 0, 0, 250, 0, 0, 0);
            return err;
        }



        private int GripperInit()
        {
            Gripper.Cmd = 0;
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.RobotiqGripperCmd(MCPTR, Gripper);
            Thread.Sleep(50);
            Gripper.Cmd = 9;
            Gripper.PosCmd = 0;
            Gripper.VelCmd = 255;
            Gripper.ForceCmd = 255;
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.RobotiqGripperCmd(MCPTR, Gripper);

            return 0;
        }
        private int catching()
        {
            Gripper.Cmd = Convert.ToInt32(tb_GripCmd.Text);
            Gripper.PosCmd = Convert.ToInt32(tb_GripPos.Text);
            Gripper.VelCmd = Convert.ToInt32(tb_GripVel.Text);
            Gripper.ForceCmd = Convert.ToInt32(tb_GripForce.Text);
            MyModusGripper.GoToReqPos(Gripper);
            Thread.Sleep(1000);
        return 0;
        }
        private int CatchObject(int vel, int force)
        {
            int err;
            Gripper.PosCmd = 255;
            Gripper.VelCmd = vel;
            Gripper.ForceCmd = force;
            //MCLib.RobotiqGripperCmd(MCPTR, Gripper);
            ////Thread.Sleep(100);
            //do
            //{
            //    Thread.Sleep(100);
            //    MCLib.RobotiqGripperStat(MCPTR, ref Gripper);
            //} while (Gripper.Status == 57);

            //Thread.Sleep(100);

            //MCLib.RobotiqGripperStat(MCPTR, ref Gripper);
            //if (Gripper.objDetect == 0)
            //{
            //    return 1;
            //    //err = 1;
            //}
            //else
            //    return 0;

            MyModusGripper.GoToReqPos(Gripper);
            Thread.Sleep(1000);

            err = 0;
            return err;
        }
        private int ShakeMotion()
        {


            return 0;
        }
        private int ReleaseObject(int vel, int force)
        {
            int err;
            Gripper.PosCmd = 0;
            Gripper.VelCmd = vel;
            Gripper.ForceCmd = force;

            //MCLib.RobotiqGripperCmd(MCPTR, Gripper);
            MyModusGripper.GoToReqPos(Gripper);
            Thread.Sleep(1000);

            //Thread.Sleep(100);
            //do
            //{
            //    Thread.Sleep(100);
            //    MCLib.RobotiqGripperStat(MCPTR, ref Gripper);
            //} while (Gripper.Status == 57);





            err = 0;

            return err;

        }

        private int MoveFunc(JointInf cmd, double Vel)
        {
            MCLib.MoveABSJointsSyn(MCPTR, cmd, Vel);

            Thread.Sleep(100);
            return 0;
        }

        private int MotionWait()
        {
            Thread.Sleep(100);
            while (bWorking)
                Thread.Sleep(500);

            return 0;
        }



/*
*********************************************************************************************************
*                          Button Click
*********************************************************************************************************
*/
      
        private void btn_ServoOn_Click(object sender, EventArgs e)
        {
            if (btn_ServoOn.Text == "ServoOn")
            {
                //DO ServoOn
                btn_ServoOn.Text = "ServoOff";
                btn_ServoOn.BackColor = System.Drawing.Color.Green;
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.DoMachineOn(MCPTR);
                Thread.Sleep(50);
                //GripperInit();
                //initialGripper= MyModusGripper.initial();
            }
            else
            {
                // DO "ServoOFF"
                btn_ServoOn.Text = "ServoOn";
                btn_ServoOn.BackColor = System.Drawing.Color.Gray;
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                Thread.Sleep(50);
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.DoMachineOff(MCPTR);
            }

        }

        private void btn_emg_Click(object sender, EventArgs e)
        {
            MCLib.MoveJogAllStop(MCPTR);
            MCLib.DoMachineOff(MCPTR); 
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            MCLib.DoMachineOff(MCPTR);
            Thread.Sleep(500);
            MCLib.DoMachineOn(MCPTR);
            Thread.Sleep(100);
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
            Thread.Sleep(100);
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR,E_OPERATION_MODE.FREE_MODE);
            ControlMode = 0;
            btn_ServoOn.Text = "ServoOn";
        }

        private void btn_For_MouseDown(object sender, MouseEventArgs e)
        {
            if (ControlMode == CONTROLMODE_AUTO)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                Thread.Sleep(100);
                if (radioButton_Joint.Checked == true)
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                    ControlMode = CONTROLMODE_FREE;
                }
                else
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.TELEOP_MODE);
                    ControlMode = CONTROLMODE_TELEOP;
                }
            }
            if (sender.Equals(btn_J1_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 0, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J2_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 1, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J3_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 2, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J4_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 3, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J5_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 4, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J6_For) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 5, Convert.ToDouble(tb_MotionSpeed.Text));
            }

        }

        private void btn_Back_MouseDown(object sender, MouseEventArgs e)
        {
            if (ControlMode == CONTROLMODE_AUTO)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                Thread.Sleep(100);
                if (radioButton_Joint.Checked == true)
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                    ControlMode = CONTROLMODE_FREE;
                }
                else
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.TELEOP_MODE);
                    ControlMode = CONTROLMODE_TELEOP;
                }
            }
            if (sender.Equals(btn_J1_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 0, -Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J2_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 1, -Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J3_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 2, -Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J4_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 3, -Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J5_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 4, -Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (sender.Equals(btn_J6_Back) == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogCont(MCPTR, 5, -Convert.ToDouble(tb_MotionSpeed.Text));
            }

        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if ((sender.Equals(btn_J1_Back) == true) || (sender.Equals(btn_J1_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 0);
            }
            else if ((sender.Equals(btn_J2_Back) == true) || (sender.Equals(btn_J2_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 1);
            }
            else if ((sender.Equals(btn_J3_Back) == true) || (sender.Equals(btn_J3_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 2);
            }
            else if ((sender.Equals(btn_J4_Back) == true) || (sender.Equals(btn_J4_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 3);
            }
            else if ((sender.Equals(btn_J5_Back) == true) || (sender.Equals(btn_J5_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 4);
            }
            else if ((sender.Equals(btn_J6_Back) == true) || (sender.Equals(btn_J6_For) == true))
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogStop(MCPTR, 5);
            }



        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Joint.Checked == true && radioButtonState != 0)
            {
                btn_SendCmd.Text = "SendJoint";
                label14.Text = "J1";
                label15.Text = "J2";
                label16.Text = "J3";
                label17.Text = "J4";
                label18.Text = "J5";
                label19.Text = "J6";
                radioButtonState = 0;

                if (label49.Visible == true)
                {
                    label49.Visible = false;
                    label50.Visible = false;
                    label51.Visible = false;
                    tb_Control7.Visible = false;
                    tb_Control8.Visible = false;
                    tb_Control9.Visible = false;
                }
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                Thread.Sleep(50);
                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                ControlMode = CONTROLMODE_FREE;
            }
            if (radioButton_Point.Checked == true && radioButtonState != 1)
            {
                btn_SendCmd.Text = "SendPosition";
                label14.Text = "X";
                label15.Text = "Y";
                label16.Text = "Z";
                label17.Text = "A";
                label18.Text = "B";
                label19.Text = "C";
                radioButtonState = 1;

                if (label49.Visible == true)
                {
                    label49.Visible = false;
                    label50.Visible = false;
                    label51.Visible = false;
                    tb_Control7.Visible = false;
                    tb_Control8.Visible = false;
                    tb_Control9.Visible = false;
                }

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.TELEOP_MODE);
                ControlMode = CONTROLMODE_TELEOP;
            }
            if (radioButton_Circle.Checked == true && radioButtonState != 2)
            {
                btn_SendCmd.Text = "SendCircle";
                label14.Text = "X";
                label15.Text = "Y";
                label16.Text = "Z";
                label17.Text = "A";
                label18.Text = "B";
                label19.Text = "C";
                radioButtonState = 2;

                if (label49.Visible == false)
                {
                    label49.Visible = true;
                    label50.Visible = true;
                    label51.Visible = true;
                    tb_Control7.Visible = true;
                    tb_Control8.Visible = true;
                    tb_Control9.Visible = true;
                }

                if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.TELEOP_MODE);
                ControlMode = CONTROLMODE_TELEOP;
            }
            tb_Control1.Text = "0";
            tb_Control2.Text = "0";
            tb_Control3.Text = "0";
            tb_Control4.Text = "0";
            tb_Control5.Text = "0";
            tb_Control6.Text = "0";
        }

        private void btn_SendCmd_Click(object sender, EventArgs e)
        {
            //int err;
            /*
            Point_Info pWork = new Point_Info();
            Joint_Info jOld = new Joint_Info();
            Joint_Info jWork = new Joint_Info();
             * */
            
            JointInf jWork = new JointInf();
            PointInf pWork = new PointInf();
            if (btn_SendCmd.Text == "SendJoint")
            {
                Get_Cmd(ref jWork);
                if (ControlMode != CONTROLMODE_FREE)
                {
                    MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.MANUAL_MODE);
                    Thread.Sleep(50);
                    MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.FREE_MODE);
                    ControlMode = CONTROLMODE_FREE;
                }
                Move_J(jWork, Convert.ToDouble(tb_MotionSpeed.Text));
            }
            else if (btn_SendCmd.Text == "SendPosition")
            {
                Get_Cmd(ref pWork);
                if (ControlMode != CONTROLMODE_AUTO)
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                    ControlMode = CONTROLMODE_AUTO;
                    Thread.Sleep(100);
                }
                Move_P(pWork, Convert.ToDouble(tb_MotionSpeed.Text));
                //Move_Circle(pWork, Convert.ToDouble(tb_MotionSpeed.Text), Convert.ToDouble(tb_Control7.Text),
                //    Convert.ToDouble(tb_Control8.Text), Convert.ToInt32(tb_Control9.Text));
            }
            else
            {
                Get_Cmd(ref pWork);
                if (ControlMode != CONTROLMODE_AUTO)
                {
                    if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.SetControlMode(MCPTR, E_CONTROL_MODE.AUTO_MODE);
                    ControlMode = CONTROLMODE_AUTO;
                    Thread.Sleep(100);
                }
                //Move_P(pWork, Convert.ToDouble(tb_MotionSpeed.Text));
                Move_Circle(pWork, Convert.ToDouble(tb_MotionSpeed.Text), Convert.ToDouble(tb_Control7.Text),
                    Convert.ToDouble(tb_Control8.Text), Convert.ToDouble(tb_Control9.Text));
            }

        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.MoveJogAllStop(MCPTR);
        }

        private void btn_Here_Click(object sender, EventArgs e)
        {
            if (radioButton_Joint.Checked == true)
            {
                tb_Control1.Text = tb_Axis1.Text;
                tb_Control2.Text = tb_Axis2.Text;
                tb_Control3.Text = tb_Axis3.Text;
                tb_Control4.Text = tb_Axis4.Text;
                tb_Control5.Text = tb_Axis5.Text;
                tb_Control6.Text = tb_Axis6.Text;
            }
            if (radioButton_Point.Checked == true)
            {
                tb_Control1.Text = tb_posX.Text;
                tb_Control2.Text = tb_posY.Text;
                tb_Control3.Text = tb_posZ.Text;
                tb_Control4.Text = tb_posA.Text;
                tb_Control5.Text = tb_posB.Text;
                tb_Control6.Text = tb_posC.Text;
            }
        }

        private void btn_SendGripperCmd_Click(object sender, EventArgs e)
        {
            
           
           
            /*Gripper.Cmd = Convert.ToInt32(tb_GripCmd.Text);
            Gripper.PosCmd = Convert.ToInt32(tb_GripPos.Text);
            Gripper.VelCmd = Convert.ToInt32(tb_GripVel.Text);
            Gripper.ForceCmd = Convert.ToInt32(tb_GripForce.Text);*/
            //if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.RobotiqGripperCmd(MCPTR, Gripper);

            Motion_mut.WaitOne();
            myCmd.Type = MOTION_TYPE_GRIPEERCATCHING;
            Motion_mut.ReleaseMutex();
        }

        private void btn_GripperInitial_Click(object sender, EventArgs e)
        {
            GripperInit();
        }

        private void btn_GripCatch_Click(object sender, EventArgs e)
        {

            //CatchObject(100, 0);
            Motion_mut.WaitOne();
            myCmd.Type = MOTION_TYPE_GRIPEERCATCH;
            Motion_mut.ReleaseMutex();
        }

        private void btn_GripRelease_Click(object sender, EventArgs e)
        {
            //ReleaseObject(255, 0);
            Motion_mut.WaitOne();
            myCmd.Type = MOTION_TYPE_GRIPEERRELEASE;
            Motion_mut.ReleaseMutex();
        }




        private void JoystickButton()
        {
            bool[] btnstate;
            MyJoy.GetMyJoystickState(ref state, ref Joyinfo);
            btnstate = state.GetButtons();

            if (btnstate[5] == true)
            {
                Gripper.PosCmd += 20;
                if (Gripper.PosCmd > 255)
                    Gripper.PosCmd = 255;
                Thread.Sleep(10);
                //MCLib.RobotiqGripperCmd(MCPTR, Gripper);
                MyModusGripper.GoToReqPos(Gripper);
            }
            else if (btnstate[4] == true)
            {
                Gripper.PosCmd -= 20;
                if (Gripper.PosCmd < 0)
                    Gripper.PosCmd = 0;
                Thread.Sleep(10);
                //MCLib.RobotiqGripperCmd(MCPTR, Gripper);
                MyModusGripper.GoToReqPos(Gripper);
            }
            else if (btnstate[0] == true)
            {
                //btn_ServoOn.Text = "ServoOff";
                MCLib.DoMachineOn(MCPTR);
            }
            else if (btnstate[1] == true)
            {
                //btn_ServoOn.Text = "ServoOn";
                //btn_ServoOn_Click(0, 0);
                MCLib.DoMachineOff(MCPTR);
            }
        }

        private int JoystickControl()
        {
            int err;

            double dx, dy, ry, rx, dspeed, vel;
            double cmdX, cmdY, cmdZ, cmdA, cmdB, cmdC;
            int[] pov = new int[4];
            int Dis;
            JointInf encJoint;

            pov[0] = 0;
            MyJoy.GetMyJoystickState(ref state, ref Joyinfo);
            dx = Math.Round(Convert.ToDouble(state.X) / 65535 * 2 - 1, 4);
            dy = Math.Round(Convert.ToDouble(state.Y) / 65535 * 2 - 1, 4);
            rx = Math.Round(Convert.ToDouble(state.RotationX) / 65535 * 2 - 1, 4);
            ry = -Math.Round(Convert.ToDouble(state.RotationY) / 65535 * 2 - 1, 4);
            dspeed = Math.Round(Convert.ToDouble(state.Z) / 65535 * 2 - 1, 1);
            pov = state.GetPointOfViewControllers();

            if (Math.Abs(dx) <= 0.2)
                dx = 0;
            if (Math.Abs(dy) <= 0.2)
                dy = 0;
            if (Math.Abs(ry) <= 0.2)
                ry = 0;
            if (Math.Abs(rx) <= 0.5)
                rx = 0;
            if (Math.Abs(dspeed) <= 0.2)
                dspeed = 0;
            cmdA = 0;
            cmdB = 0;
            cmdC = 0;



            vel = Convert.ToDouble(tb_ControlSpeed.Text);
            
            if (dspeed != 0)
                vel = vel + dspeed * 50.0;
            if (vel < 100)
                vel = 100;
            if (vel > 5000)
                vel = 5000;

            tb_ControlSpeed.Text = Convert.ToString(vel);
            if (radioButton_d0.Checked == true)
            {
                cmdX = dy;
                cmdY = dx;
                cmdZ = ry;
            }
            else if (radioButton_d90.Checked == true)
            {
                cmdX = -dx;
                cmdY = dy;
                cmdZ = ry;

            }
            else if (radioButton_d180.Checked == true)
            {
                cmdX = -dy;
                cmdY = -dx;
                cmdZ = ry;
            }
            else    //(radioButton_d270.Checked == true)
            {
                cmdX = dx;
                cmdY = -dy;
                cmdZ = ry;
            }

            if (checkBox2.Checked)
            {
                cmdC = rx;
                switch (pov[0])
                {
                    case -1:
                        cmdA = 0;
                        cmdB = 0;
                        break;
                    case 0:
                        cmdA = 0;
                        cmdB = 1;
                        break;
                    case 9000:
                        cmdA = 1;
                        cmdB = 0;
                        break;
                    case 18000:
                        cmdA = 0;
                        cmdB = -1;
                        break;
                    case 27000:
                        cmdA = -1;
                        cmdB = 0;
                        break;
                    case 4500:
                        cmdA = 1;
                        cmdB = 1;
                        break;
                    case 13500:
                        cmdA = 1;
                        cmdB = -1;
                        break;
                    case 22500:
                        cmdA = -1;
                        cmdB = -1;
                        break;
                    case 31500:
                        cmdA = 1;
                        cmdB = -1;
                        break;
                    default:
                        cmdA = 0;
                        cmdB = 0;
                        break;
                }
            }

            MCLib.MoveJoyStickCont(MCPTR, cmdX, cmdY, cmdZ, cmdA, cmdB, cmdC, vel);


            /*
        else
        {
            cmdX = dx;
            cmdY = -dy;
            cmdZ = -rx;
            MCLib.MoveJoyStickCont(MCPTR, 0.0, 0.0, 0.0, cmdX, cmdY, cmdZ, Convert.ToDouble(textMotionVelocity.Text));
        }
        */


           
            if (MCLiberr == E_MCLib_Error.CONNECT_OK) MCLib.GetJointsPosRes(MCPTR, ref m_jointsinf);

            encJoint.theta1 = m_jointsinf.theta1;
            encJoint.theta2 = m_jointsinf.theta2;
            encJoint.theta3 = m_jointsinf.theta3;
            encJoint.theta4 = m_jointsinf.theta4;
            encJoint.theta5 = m_jointsinf.theta5;
            encJoint.theta6 = m_jointsinf.theta6;
            encJoint.theta7 = m_jointsinf.theta7;

            Dis = CheckLimitDis(limit, ref encJoint);

            MyJoy.Vibration(Dis);

            //Show_TextBox(tBTemp, Convert.ToString(dx) + "\t" + Convert.ToString(dy) + "\t" + Convert.ToString(ry));
            //Show_TextBox(textMotionVelocity, Convert.ToString(vel));
            err = 0;
            return err;
        }


        private int CheckLimitDis(double[] limit, ref JointInf Joint)
        {
            double[] temp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double max = 0;


            if (Math.Abs(Joint.theta1 - limit[0]) < 5)
                temp[0] = 5 - Math.Abs(Joint.theta1 - limit[0]);
            if (Math.Abs(Joint.theta1 - limit[1]) < 5)
                temp[1] = 5 - Math.Abs(Joint.theta1 - limit[1]);

            if (Math.Abs(Joint.theta2 - limit[2]) < 5)
                temp[2] = 5 - Math.Abs(Joint.theta2 - limit[2]);
            if (Math.Abs(Joint.theta2 - limit[3]) < 5)
                temp[3] = 5 - Math.Abs(Joint.theta2 - limit[3]);

            if (Math.Abs(Joint.theta3 - limit[4]) < 5)
                temp[4] = 5 - Math.Abs(Joint.theta3 - limit[4]);
            if (Math.Abs(Joint.theta3 - limit[5]) < 5)
                temp[5] = 5 - Math.Abs(Joint.theta3 - limit[5]);

            if (Math.Abs(Joint.theta4 - limit[6]) < 5)
                temp[6] = 5 - Math.Abs(Joint.theta4 - limit[6]);
            if (Math.Abs(Joint.theta4 - limit[7]) < 5)
                temp[7] = 5 - Math.Abs(Joint.theta4 - limit[7]);

            if (Math.Abs(Joint.theta5 - limit[8]) < 5)
                temp[8] = 5 - Math.Abs(Joint.theta5 - limit[8]);
            if (Math.Abs(Joint.theta5 - limit[9]) < 5)
                temp[9] = 5 - Math.Abs(Joint.theta5 - limit[9]);

            if (Math.Abs(Joint.theta6 - limit[10]) < 5)
                temp[10] = 5 - Math.Abs(Joint.theta6 - limit[10]);
            if (Math.Abs(Joint.theta6 - limit[11]) < 5)
                temp[11] = 5 - Math.Abs(Joint.theta6 - limit[11]);

            for (int i = 0; i < 12; i++)
            {
                if (temp[i] > max)
                    max = temp[i];
            }

            max = max * 100.0 / 5.0;

            return Convert.ToInt32(max);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!HaveJoystick)
                HaveJoystick = !Convert.ToBoolean(MyJoy.InitDevices());
            if (checkBox1.Checked == true && HaveJoystick == true)
            {
                if (MCLiberr == E_MCLib_Error.CONNECT_OK  && ControlMode != CONTROLMODE_TELEOP ) MCLib.SetMANUALOpMode(MCPTR, E_OPERATION_MODE.TELEOP_MODE);
                useJoystick = true;
            }

            else
                useJoystick = false;   
        }


        private void btn_unitChange_Click(object sender, EventArgs e)
        {
            unitchange = !unitchange;
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PointFormDlg.refMainfrm = this;
            PointFormDlg.Show();
        }
       
       


        private void button2_Click(object sender, EventArgs e)
        {
            Gripper.PosCmd = 200;
            Gripper.VelCmd = 150;
            Gripper.ForceCmd = 150;
            //if (flag2 == 0)
            //MyModusGripper.GoToReqPos(Gripper);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gripper.PosCmd = 20;
            Gripper.VelCmd = 150;
            Gripper.ForceCmd = 150;
            //if (flag2 == 0)
            //MyModusGripper.GoToReqPos(Gripper);
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageFrm = new ImageMainfrm();
            ImageFrm.refMainfrm = this;
            ImageFrm.Show();
        }

        private void drinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chooseDlg.refMainfrm = this;
            chooseDlg.Show();

          


        }
    
    
    }
}
