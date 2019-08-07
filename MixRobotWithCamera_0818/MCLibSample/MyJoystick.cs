using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.DirectInput;
using SlimDX.XInput;

namespace SlimDX_Joystick
{
    public class MyJoystick
    {
        //List<DeviceInstance> directInputList = new List<DeviceInstance>();
        //DirectInput directInput = new DirectInput();
        //SlimDX.DirectInput.Joystick Joystick;
        //SlimDX.DirectInput.Joystick Joystick;
        //SlimDX.DirectInput.JoystickState state;

        
        public DirectInput directInput;
        public Guid joystickGuid;
        public Joystick Joystick;

        public UserIndex userIndex;
        private SlimDX.XInput.Controller XJoystick;
        private Vibration vibration;


        public int InitDevices()
        {
            int err = 0;


            this.directInput = new DirectInput();
            this.joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(SlimDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                this.joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (this.joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(SlimDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    this.joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (this.joystickGuid == Guid.Empty)
            {
                MessageBox.Show("No joystick/Gamepad found.");
                err = 1;
                //Console.ReadKey();
                //Environment.Exit(1);

            }
            else
            {

                this.Joystick = new Joystick(directInput, joystickGuid);
                var allEffects = this.Joystick.GetEffects();
                /**
                foreach (var effectInfo in allEffects)
                    MessageBox.Show("Effect available {0}", effectInfo.Name);
                */
                this.Joystick.Properties.BufferSize = 128;
                Joystick.Acquire();
                err = 0;

            }
            //Joystick.SetCooperativeLevel(null, CooperativeLevelFlags.Background | CooperativeLevelFlags.Exclusive); 

            this.userIndex = new UserIndex();


            this.XJoystick = new SlimDX.XInput.Controller(userIndex);

            this.vibration = new Vibration();




            //directInputList.AddRange(directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly));            
            //Joystick = new SlimDX.DirectInput.Joystick(directInput, directInputList[0].InstanceGuid);
            //var allEffects = this.Joystick.GetEffects();

            //if (Joystick == null)
            //{
            //    //Throw exception if joystick not found.
            //    //throw new Exception("No joystick found.");
            //    MessageBox.Show("No joystick found.");
            //}
            //else
            //    Joystick.Acquire();

            return err;


        }

        public int GetMyJoystickState(ref SlimDX.DirectInput.JoystickState state)
        {
            int err;
            if (Joystick != null)
            {
                state = Joystick.GetCurrentState();
                err = 0;
            }
            else
                err = 1;

            return err;
        }

        public int GetMyJoystickState(ref SlimDX.DirectInput.JoystickState state, ref string info)
        {
            int err;
            if (Joystick != null)
            {
                info = "Joystick: ";
                state = Joystick.GetCurrentState();
                //Capture Position.
                info += "X:" + state.X + " ";
                info += "Y:" + state.Y + " ";
                info += "Z:" + state.Z + " ";

                //Capture Buttons.

                bool[] buttons = state.GetButtons();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i] != false)
                    {
                        info += "Button:" + i + " ";
                    }
                }
                err = 0;
            }
            else
                err = 1;
            return err;
        }

        public int Vibration(int Speed)
        {
            ushort rSpeed, lSpeed;
            rSpeed = Convert.ToUInt16(Speed * 65535.0 / 100.0);
            lSpeed = rSpeed;
            this.vibration.RightMotorSpeed = rSpeed;
            this.vibration.LeftMotorSpeed = lSpeed;
            if (XJoystick.IsConnected) this.XJoystick.SetVibration(vibration);

            return 0;
        }

        public int DisConnectJoystick()
        {
            int err;
            if (Joystick != null)
            {
                Joystick.Unacquire();
                err = 0;
            }
            else
                err = 1;
            return err;
        }
    }
}
