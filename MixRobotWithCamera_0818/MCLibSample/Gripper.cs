using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FtdAdapter;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using System.IO.Ports;
using RTIOMSMC_Name;

namespace ModbusGripper
{

    public class MyGripper
    {
        private IModbusSerialMaster master = null;
        private SerialPort port = new SerialPort("COM3"); 
         // gripper

        private bool open_port(ref SerialPort port, ref IModbusSerialMaster master)
        {


            bool flag;
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.Open();
            master = ModbusSerialMaster.CreateRtu(port);
            flag = port.IsOpen;

            return flag;

        }

        public void close()
        {

            if (this.port != null)
                this.port.Dispose();
            this.port = null;
        }

        public bool initial()
        {
            this.master = ModbusSerialMaster.CreateRtu(this.port);
            bool flag = false;

            flag = open_port(ref this.port, ref this.master);

            if (!flag)
                return false;

            byte slaveId = 09;
            ushort startAddress = 1000;//03E8
            ushort startAddress2 = 2000;//07D0
            ushort[] registers1 = new ushort[] { 0, 0, 0 };
            ushort[] registers2 = new ushort[] { 256, 0, 0 };
            ushort[] registers3 = new ushort[] { 2304, 255, 5000 }; // register, position 0/255, speed and force
            //ushort[] registers4 = new ushort[] { 2304, 0, 65535 };// Final Point

            // write three registers
            // 4372 in  progress
            //4900 
            //63744
            //12544  hex 3100
            master.WriteMultipleRegisters(slaveId, startAddress, registers1); //Activation 
            var status = master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];//Read Gripper status until the activation is completed
            Console.WriteLine(status);



            while (status != 12544)
            {
                master.WriteMultipleRegisters(slaveId, startAddress, registers2);//Request is (set rAct):
                status = master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];//Read Gripper status until the activation is completed
                // Console.WriteLine(status);
            }

            while (status != 63744)
            {
                master.WriteMultipleRegisters(slaveId, startAddress, registers3);//Close the Gripper at full speed and full force
                status = master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];//Read Gripper status until the grip is completed
                //Console.WriteLine(status);
            }
            //while (status1 != 63744)
            //{
            //    master.WriteMultipleRegisters(slaveId, startAddress, registers4);
            //    status1 = master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];
            //    //Console.WriteLine(status1);
            //}

            return true;
        }
        public int ReadStatus(ref RobotiqGripperInf Gripper)
        {
            byte slaveId = 09;
            ushort startAddress2 = 2000;//07D0
            ushort StatError, ActualPosCurrent;
           // string status1, status2;
           
            //auxStatus= Convert.ToString((this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[0]),16);//Read Gripper status until the activation is completed
            //auxError = Convert.ToString((this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[1]), 16);
            //AuxPosition = Convert.ToString((this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[2]),16);
            //status1 = auxStatus.Substring(auxStatus.Length - 2);
            //status2 = auxStatus.Substring(0, 2);
          
            
            /*
            if (auxError.Length > 2)
            {
                error1 = auxError.Substring(auxError.Length - 2);
            }
            else
                error1 ="0";
            error2 = auxError.Substring(0, 2);
            position1 = AuxPosition.Substring(AuxPosition.Length - 2);
            positio2 = AuxPosition.Substring(0, 2);
            //if (auxStatus.Length == 4)
           // {
                Gripper.Status = Convert.ToInt32(status2, 16);
                Gripper.Error = Convert.ToInt32(error2, 16);
                Gripper.PosReq = Convert.ToInt32(error1, 16);
                Gripper.ActualPos = Convert.ToInt32(positio2,16);
                Gripper.Current = Convert.ToInt32(position1, 16);
           // }
           // Console.Write(auxStatus);

            //string words = auxStatus.Substring(auxStatus.Length - 2) ;
            //Gripper.Status = Convert.ToInt32(auxStatus);
            //Gripper.Error =Convert.ToInt32(auxStatus);*/
            
           //Gripper.Status = (int)this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[0];//Read Gripper status until the activation is completed
           /* if (auxStatus.Length > 1)
            {
                Gripper.Status = Convert.ToInt32(status2, 10);
            }
            else*/
            string StErr = "";
            string PosCurr = "";
            int detection;
            Gripper.PosReq = (int)this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[1];
            StatError=this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[0];
            ActualPosCurrent = this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[2];
            StErr = Transform(StatError);
            PosCurr = Transform(ActualPosCurrent);
            Gripper.Status = Convert.ToInt32(StErr.Substring(0, 8), 2);
            Gripper.Error = Convert.ToInt32(StErr.Substring(8, 8), 2);
            Gripper.ActualPos = (Convert.ToInt32(PosCurr.Substring(0, 8), 2))*248/255;
            Gripper.Current = Convert.ToInt32(PosCurr.Substring(8, 8), 2);
            detection= (int)this.master.ReadHoldingRegisters(slaveId, startAddress2, 3)[0];


            if (detection == 47360)
            {
                Gripper.objDetect = 1;
            }
            else
                Gripper.objDetect = 0;

            
            return 0;

        }
        public int GoToReqPos(RobotiqGripperInf Gripper)
        {
            //Gripper.Cmd = 2304;

            byte slaveId = 09;

            ushort startAddress = 1000;//03E8
            ushort startAddress2 = 2000;//07D0
            ushort newVelFor = (ushort)convertion(Gripper);
            ushort[] registers = new ushort[] { (ushort)2304, (ushort)Gripper.PosCmd, newVelFor };
            var status = 0;
            while ((status != 63744))
            {
                this.master.WriteMultipleRegisters(slaveId, startAddress, registers); //Activation 
                Gripper.Status=status = this.master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];//Read Gripper status until the activation is completed
                //Gripper.ActualPos
                if (status == 47360)
                {
                    Gripper.objDetect = 1;
                    break;
                }
                else
                    Gripper.objDetect = 10;
                Console.WriteLine(status);
            }
            Gripper.Error = this.master.ReadHoldingRegisters(slaveId, startAddress2, 1)[0];
            //Gripper.PosReq = this.master.ReadHoldingRegisters(slaveId, startAddress2, 1)[1];
            //Gripper.ActualPos = this.master.ReadHoldingRegisters(slaveId, startAddress2, 1)[2];
            return 0;

        }
        private int convertion(RobotiqGripperInf Gripper)
        {

            string binary1 = Convert.ToString(Gripper.VelCmd, 16);
            string binary2 = Convert.ToString(Gripper.ForceCmd, 16);
            string newNumber = Convert.ToString(binary1.ToString() + binary2.ToString());

            newNumber = Convert.ToString(Convert.ToInt32(newNumber, 16), 10);
            int result = Convert.ToInt32(newNumber);

            return result;

        }
        public static string Transform(ushort number)
        {
            string strNum;
            strNum = Binary(number);
            strNum = Reverse(strNum);
            return strNum;

        }
        public static string Binary(ushort number)
        {
            string[] aux = new string[16];
            string aux2 = "";
            int numbers = number;
            for (int i = 0; i <= 15; i++)
            {
                aux[i] = Convert.ToString(numbers % 2);
                aux2 = aux2 + aux[i];
                numbers = numbers / 2;
            }
            return aux2;

        }
        public static string Reverse(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }
    }
}
