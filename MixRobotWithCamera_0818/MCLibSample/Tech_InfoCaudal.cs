using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MCLibSample
{
    public partial class Tech_InfoCaudal : Form
    {
        public PointForm  refPointForm;
        //private choosing refChoosing = new choosing();
     
        double Time;
        double[] BottleNumber = new double[3];

        double area;
        double area1;
        double velocity;
        double[] ActualVolume = new double[8];

        double height;
        double Diameter;
        double TotaL_Volume;
        bool SState=false;

       

        public Tech_InfoCaudal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Tech_InfoCaudal_Load(object sender, EventArgs e)
        {
           pb_bottle.Image = Properties.Resources.bottle3;
            pb_Area.Image = Properties.Resources.areaequation;
           pb_Diameter.Image = Properties.Resources.Diameter3;
           PB_FinalEquations.Image = Properties.Resources.Finalequation;
           pb_FloweQUATIONS.Image = Properties.Resources.Flow_Equations;
           pb_Speed.Image = Properties.Resources.torriccelli;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void DataView()
        {
            SState = choosing.FlagParameters;//refChoosing.Fstate();
            BottleNumber = choosing.ExportBeverages;
            // for (int i = 0; i < refChoosing.exportBeveragesVals.Length; i++)
            //     BottleNumber[i] = refChoosing.exportBeveragesVals[i];

            if (SState == true)
            {
                //tb_drinkTime.Text = Convert.ToString(BottleNumber[0]) + "   qsee";
                for (int i = 0; i < BottleNumber.Length; i++)
                {
                    if (BottleNumber[i] == 1)
                    {
                        ActualVolume[0] = ActualVolume[0] - (velocity * area * Time * 1000000);
                        if (ActualVolume[0] <= 0)
                            ActualVolume[0] = 0;
                    }
                    if (BottleNumber[i] == 2)
                    {
                        ActualVolume[1] = ActualVolume[1] - (velocity * area * Time * 1000000);
                        if (ActualVolume[1] <= 0)
                            ActualVolume[1] = 0;
                    }
                    if (BottleNumber[i] == 3)
                    {
                        ActualVolume[2] = ActualVolume[2] - (velocity * area * Time * 1000000);
                        if (ActualVolume[2] <= 0)
                            ActualVolume[2] = 0;
                    }
                    if (BottleNumber[i] == 4)
                    {
                        ActualVolume[3] = ActualVolume[3] - (velocity * area * Time * 1000000);
                        if (ActualVolume[3] <= 0)
                            ActualVolume[3] = 0;
                    }
                    if (BottleNumber[i] == 5)
                    {
                        ActualVolume[4] = ActualVolume[4] - (velocity * area * Time * 1000000);
                        if (ActualVolume[4] <= 0)
                            ActualVolume[4] = 0;
                    }
                    if (BottleNumber[i] == 6)
                    {
                        ActualVolume[5] = ActualVolume[5] - (velocity * area * Time * 1000000);
                        if (ActualVolume[5] <= 0)
                            ActualVolume[5] = 0;
                    }
                    if (BottleNumber[i] == 7)
                    {
                        ActualVolume[6] = ActualVolume[6] - (velocity * area * Time * 1000000);
                        if (ActualVolume[6] <= 0)
                            ActualVolume[6] = 0;
                    }
                    if (BottleNumber[i] == 8)
                    {
                        ActualVolume[7] = ActualVolume[7] - (velocity * area * Time * 1000000);
                        if (ActualVolume[7] <= 0)
                            ActualVolume[7] = 0;
                    }
                }
            }
            for (int i = 0; i < BottleNumber.Length; i++)
                BottleNumber[i] = 0;
            for (int i = 0; i < choosing.ExportBeverages.Length; i++)
                choosing.ExportBeverages[i] = 0;


            tb_vodka.Text = (Convert.ToString(ActualVolume[0].ToString("N3")) + "  cc");
            tb_whisky.Text = Convert.ToString(ActualVolume[1]) + "  cc";
            tb_tequila.Text = Convert.ToString(ActualVolume[2]) + "  cc";
            tb_lime.Text = Convert.ToString(ActualVolume[3]) + "  cc";
            tb_orangeJuice.Text = Convert.ToString(ActualVolume[4]) + "  cc";
            tb_Water.Text = Convert.ToString(ActualVolume[5]) + "  cc";
            tb_scotch.Text = Convert.ToString(ActualVolume[6]) + "  cc";
           
            String.Format("{0:0.00}", tb_vodka.Text);

            choosing.FlagParameters = false;
         

        
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
           // choosing refChoosing = new choosing();
            Random rnd = new Random();
           
            SState = choosing.FlagParameters;//refChoosing.Fstate();
            BottleNumber = choosing.ExportBeverages;
           
           /* for (int i = 0; i < 3; i++)
                BottleNumber[i] = rnd.Next(1, 8);*/

           
                //tb_drinkTime.Text = Convert.ToString(BottleNumber[0]) + "   qsee";
                for (int i = 0; i < BottleNumber.Length; i++)
                {
                    if (BottleNumber[i] == 1)
                    {
                        ActualVolume[0] = ActualVolume[0] - 45;
                        if (ActualVolume[0] <= 0)
                            ActualVolume[0] = 0;
                    }
                    if (BottleNumber[i] == 2)
                    {
                        ActualVolume[1] = ActualVolume[1] - 45;
                        if (ActualVolume[1] <= 0)
                            ActualVolume[1] = 0;
                    }
                    if (BottleNumber[i] == 3)
                    {
                        ActualVolume[2] = ActualVolume[2] - 45;
                        if (ActualVolume[2] <= 0)
                            ActualVolume[2] = 0;
                    }
                    if (BottleNumber[i] == 4)
                    {
                        ActualVolume[3] = ActualVolume[3] - 45;
                        if (ActualVolume[3] <= 0)
                            ActualVolume[3] = 0;
                    }
                    if (BottleNumber[i] == 5)
                    {
                        ActualVolume[4] = ActualVolume[4] - 45;
                        if (ActualVolume[4] <= 0)
                            ActualVolume[4] = 0;
                    }
                    if (BottleNumber[i] == 6)
                    {
                        ActualVolume[5] = ActualVolume[5] - 45;
                        if (ActualVolume[5] <= 0)
                            ActualVolume[5] = 0;
                    }
                    if (BottleNumber[i] == 7)
                    {
                        ActualVolume[6] = ActualVolume[6] - 45;
                        if (ActualVolume[6] <= 0)
                            ActualVolume[6] = 0;
                    }
                    if (BottleNumber[i] == 8)
                    {
                        ActualVolume[7] = ActualVolume[7] - 45;
                        if (ActualVolume[7] <= 0)
                            ActualVolume[7] = 0;
                    }
                }
            
            for (int i = 0; i < BottleNumber.Length; i++)
                BottleNumber[i] = 0;
            for (int i = 0; i < choosing.ExportBeverages.Length; i++)
                choosing.ExportBeverages[i] = 0;


            tb_vodka.Text = (Convert.ToString(ActualVolume[0].ToString("N3")) + "  cc");
            tb_whisky.Text = Convert.ToString(ActualVolume[1]) + "  cc";
            tb_tequila.Text = Convert.ToString(ActualVolume[2]) + "  cc";
            tb_lime.Text = Convert.ToString(ActualVolume[3]) + "  cc";
            tb_orangeJuice.Text = Convert.ToString(ActualVolume[4]) + "  cc";
            tb_Water.Text = Convert.ToString(ActualVolume[5]) + "  cc";
            tb_scotch.Text = Convert.ToString(ActualVolume[6]) + "  cc";
            
            String.Format("{0:0.00}", tb_vodka.Text);
            
            choosing.FlagParameters = false;
            //refChoosing.State = false;
           
           /* refChoosing.OrderState = false;
            refChoosing.state2();*/
            
            
           
            
           // if(numberofShots == 1 )



        }

        private void button2_Click(object sender, EventArgs e)
        {
            height = Convert.ToDouble(tb_Height.Text);
            Diameter = Convert.ToDouble(tb_Diameter.Text) ;
            velocity = Math.Sqrt(2 * 9.8 * (height/1000));//m/s
            refPointForm.Time();
            Time = Convert.ToDouble(refPointForm.DrinkTime)/1000;
            area1 = ((Diameter / 1000) / 2) * ((Diameter / 1000) / 2) * Math.PI;
            double  timecal;
            
            area = ((10 / 1000) / 2) * ((10 / 1000) / 2) * Math.PI;// m^2
            timecal = 0.0002/(velocity*area1);
            TotaL_Volume = (velocity * (area1-area) * 30) * 1000000;//need 90 seconds to get empty  //m^3
            for (int i = 0; i < ActualVolume.Length; i++)
                ActualVolume[i] = TotaL_Volume;
            for (int i = 0; i < ActualVolume.Length; i++)
                ActualVolume[i] = 200;
            TB_TotalVolum.Text = Convert.ToString(TotaL_Volume.ToString("N3")) + "  cc";
            tb_drinkTime.Text = Convert.ToString(Time) + "  s";
            tb_velocity.Text = Convert.ToString(velocity)+ "  m/s";
        }

       
    }
}
