using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RTIOMSMC_Name
{
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JointInf
    {
        public double theta1;
        public double theta2;
        public double theta3;
        public double theta4;
        public double theta5;
        public double theta6;
        public double theta7;

    } 
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PointInf
    {
        public double x;
        public double y;
        public double z;
        public double a;
        public double b;
        public double c;
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct MotorInf
    {
        public double[] Motors;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RobotiqGripperInf
    {
        public int Cmd;
        public int PosCmd;
        public int VelCmd;
        public int ForceCmd;

        public int Status;
        public int Error;
        public int PosReq;
        public int ActualPos;
        public int Current;

        public int objDetect;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ForceSensorInf
    {
        public double Fx;
        public double Fy;
        public double Fz;
        public double Tx;
        public double Ty;
        public double Tz;

    }

    public class MCLib
    {

/*
*********************************************************************************************************
*                           Basic Function
*********************************************************************************************************
*/

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "Create_MC")]
        public extern static IntPtr Create_MC();
        //RTIOMSMC_API LONG_PTR __cdecl Create_MC();

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "Destroy_MC")]
        public extern static void Destroy_MC(IntPtr MC);
		//RTIOMSMC_API void __cdecl DestoryMC(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "UpdateStatus")]
        public extern static int UpdateStatus(IntPtr MC);
		//RTIOMSMC_API int __cdecl RTIOMSMC_UpdateStatus(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "UpdateError")]
        public extern static int UpdateError(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_UpdateError(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoConnectToController")]
        public extern static E_MCLib_Error DoConnectToController(IntPtr MC, char[] processname);
        //RTIOMSMC_API E_MCLib_Error __cdecl RTIOMSMC_DoConnectToController(LONG_PTR MC, char *processname);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoDisconnectFromController")]
        public extern static void DoDisconnectFromController(IntPtr MC);
        //RTIOMSMC_API void __cdecl RTIOMSMC_DoDisconnectFromController(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoCloseSystem")]
        public extern static void DoCloseSystem(IntPtr MC);
        //RTIOMSMC_API void __cdecl RTIOMSMC_DoCloseSystem(LONG_PTR MC);


/*
*********************************************************************************************************
*                           Response Function
*********************************************************************************************************
*/

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetJointsPosCmd")]
        public extern static int GetJointsPosCmd(IntPtr MC, ref JointInf JointsCmd);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetJointsPosCmd(LONG_PTR MC, JointPosInf *joints);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetJointsPosRes")]
        public extern static int GetJointsPosRes(IntPtr MC, ref JointInf JointsRes);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetJointsPosRes(LONG_PTR MC, JointPosInf *joints);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetMotorsCmd")]
        public extern static int GetMotorsCmd(IntPtr MC, ref MotorInf MotorsCmd);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetMotorsCmd(LONG_PTR MC, MotorPosInf *motors);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetMotorsEncoder")]
        public extern static int GetMotorsEncoder(IntPtr MC, int axis, ref double MotorsRes);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetMotorsEncoder(LONG_PTR MC, MotorPosInf *motors);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetCartesianPosCmd")]
        public extern static int GetCartesianPosCmd(IntPtr MC, ref PointInf CartPosCmd);
        //RTIOMSMC_API int __cdecl GetCartesianPosCmd(LONG_PTR MC, CarteseanPosInf *CartPosCmd);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetCartesianPosRes")]
        public extern static int GetCartesianPosRes(IntPtr MC, ref PointInf CartPosRes);
        //RTIOMSMC_API int __cdecl GetCartesianPosRes(LONG_PTR MC, CarteseanPosInf *CartPosRes);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetAxisEnable")]
        public extern static int GetAxisEnable(IntPtr MC, int axis);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetAxisEnable(LONG_PTR MC, int axis);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetMotionStatus")]
        public extern static int GetMotionStatus(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetMotionStatus(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetAxisMotionStatus")]
        public extern static int GetAxisMotionStatus(IntPtr MC, int axis);
        //RTIOMSMC_API int __cdecl RTIOMSMC_GetAxisMotionStatus(LONG_PTR MC, int axis);


/*
*********************************************************************************************************
*                           Motion Function
*********************************************************************************************************
*/

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoMachineOn")]
        public extern static int DoMachineOn(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_DoMachineOn(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoMachineOff")]
        public extern static int DoMachineOff(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_DoMachineOff(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetAxisEnable")]
        public extern static int SetAxisEnable(IntPtr MC, int axis);
        //RTIOMSMC_API int __cdecl RTIOMSMC_SetAxisEnable(LONG_PTR MC, int axis);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetAxisDisable")]
        public extern static int SetAxisDisable(IntPtr MC, int axis);
        //RTIOMSMC_API int __cdecl RTIOMSMC_SetAxisDisable(LONG_PTR MC, int axis);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJogStop")]
        public extern static int MoveJogStop(IntPtr MC, int axis);
        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveJogStop(LONG_PTR MC, int axis);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJogAllStop")]
        public extern static int MoveJogAllStop(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveJogAllStop(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJogCont")]
        public extern static int MoveJogCont(IntPtr MC, int axis, double speed);
        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveJogCont(LONG_PTR MC, int axis, double speed);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJogIncr")]
        public extern static int MoveJogIncr(IntPtr MC, int axis, double speed, double incr);
        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveJogIncr(LONG_PTR MC, int axis, double speed, double incr);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJogAbs")]
        public extern static int MoveJogAbs(IntPtr MC, int axis, double speed, double abspos);
        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveJogAbs(LONG_PTR MC, int axis, double speed, double abspos);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJoyStickCont")]
        public extern static int MoveJoyStickCont(IntPtr MC, double nominalx, double nominaly, double nominalz, double nominala, double nominalb, double nominalc, double speed);
        //int RTIOMS_MoveJoyStickCont(double nominalx,double nominaly,double nominalz,double nominala,double nominalb,double nominalc,double speed);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveJoyStickStop")]
        public extern static int MoveJoyStickStop(IntPtr MC);
        //int RTIOMS_MoveJoyStickStop(void);



        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveABSJointsSyn")]
        public extern static int MoveABSJointsSyn(IntPtr MC, JointInf Cmd, double vel);

        //RTIOMSMC_API int __cdecl RTIOMSMC_MoveABSJointsSyn(LONG_PTR MC, SynJointCmd cmd);
        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveLineAbs")]
        public extern static int MoveLineAbs(IntPtr MC, PointInf Cmd, double speed);
        //RTIOMSMC_API int __cdecl MoveLineAbs(LONG_PTR MC, PointInf Point, double speed);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveCircleAbs")]
        public extern static int MoveCircleAbs(IntPtr MC, PointInf Cmd, double center_x, double center_y, double center_z 
                                                        , double nominalx, double nominaly, double nominalz, double speed);
       	//RTIOMSMC_API int __cdecl MoveCircleAbs(LONG_PTR MC, PointInf Target_Point, double center_x,double center_y, double center_z
		//											, double nominalx, double nominaly, double nominalz, double speed);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveTrajAbort")]
        public extern static int MoveTrajAbort(IntPtr MC);
		//RTIOMSMC_API int __cdecl MoveTrajAbort(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveTrajPause")]
        public extern static int MoveTrajPause(IntPtr MC);
		//RTIOMSMC_API int __cdecl MoveTrajPause(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "MoveTrajResume")]
        public extern static int MoveTrajResume(IntPtr MC);
		//RTIOMSMC_API int __cdecl MoveTrajResume(LONG_PTR MC);


        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetForwardKine")]
        public extern static int GetForwardKine(IntPtr MC, JointInf Joint, ref PointInf Point);
        //RTIOMSMC_API int __cdecl GetForwardKine(LONG_PTR MC, JointInf Joint, PointInf *Point);




/*
*********************************************************************************************************
*                           Setting Function
*********************************************************************************************************
*/

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetControlMode")]
        public extern static int SetControlMode(IntPtr MC, E_CONTROL_MODE mode);
        //RTIOMSMC_API int __cdecl RTIOMS_SetControlMode(LONG_PTR MC, E_CONTROL_MODE mode)

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetMANUALOpMode")]
        public extern static int SetMANUALOpMode(IntPtr MC, E_OPERATION_MODE mode);
        //RTIOMSMC_API int __cdecl RTIOMS_SetMANUALOpMode(LONG_PTR MC, E_OPERATION_MODE mode)

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetTool")]
        public extern static int SetTool(IntPtr MC, double DisX, double DisY, double DisZ, double RotX, double RotY, double RotZ);
        //RTIOMSMC_API int __cdecl SetTool(LONG_PTR MC, double DisX, double DisY, double DisZ, double RotX, double RotY, double RotZ);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SetFeedScale")]
        public extern static int SetFeedScale(IntPtr MC, double scale);
        //RTIOMSMC_API int __cdecl RTIOMSMC_SetFeedScale(LONG_PTR MC, double scale);


/*
*********************************************************************************************************
*                           I/O Function
*********************************************************************************************************
*/

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoJawsOn")]
        public extern static int DoJawsOn(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_DoJawsOn(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "DoJawsOff")]
        public extern static int DoJawsOff(IntPtr MC);
        //RTIOMSMC_API int __cdecl RTIOMSMC_DoJawsOff(LONG_PTR MC);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "RobotiqGripperCmd")]
        public extern static int RobotiqGripperCmd(IntPtr MC, RobotiqGripperInf Gripper);
        //RTIOMSMC_API int __cdecl RobotiqGripperCmd(LONG_PTR MC, RobotiqGripperInf *Gripper);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "RobotiqGripperStat")]
        public extern static int RobotiqGripperStat(IntPtr MC, ref RobotiqGripperInf Gripper);
        //RTIOMSMC_API int __cdecl RobotiqGripperStat(LONG_PTR MC, RobotiqGripperInf *Gripper);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "RobotiqGripperForceControl")]
        public extern static int RobotiqGripperForceControl(IntPtr MC, RobotiqGripperInf Gripper, int max_current);
        //RTIOMSMC_API int __cdecl RobotiqGripperStat(LONG_PTR MC, RobotiqGripperInf *Gripper);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetDigitialInput")]
        public extern static int GetDigitialInput(IntPtr MC, int bits, ref int data);
        //RTIOMSMC_API int __cdecl GetDigitialInput(LONG_PTR MC, int bits, int *data);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetAnalogInput")]
        public extern static int GetAnalogInput(IntPtr MC, int bits, ref double data);
        //RTIOMSMC_API int __cdecl GetAnalogInput(LONG_PTR MC, int bits, short *data);

        [DllImport("RTIOMSMC.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "GetForceSensor")]
        public extern static int GetForceSensor(IntPtr MC, ref ForceSensorInf Gripper);
        //RTIOMSMC_API int __cdecl GetForceSensor(LONG_PTR MC, ForceSensor *Force);

    }
}
