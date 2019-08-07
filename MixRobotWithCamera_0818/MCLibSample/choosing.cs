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

using RTIOMSMC_Name;
using System.Drawing.Imaging;
namespace MCLibSample
{
    public partial class choosing : Form
    {
        //choosing frm2 = new choosing(this)
        public Form1 refMainfrm;
        //public ImageMainfrm dfs;
        public PointForm myPointForm = new PointForm();
        public PointForm refPointForm;
        public PointInf[] Rec_Points;
          
        /// <summary>
        /// //////////
        /// </summary>
        public PointInf pP1;
        public int Mode_state = 0;
        public double[] BeveragePosition = new double[3];

        public int[] itemvalue = new int[12];
        public bool start_flag = false;    
        public bool OrderState = false;
        public bool State;
        public int drinknumber = 0;
        public int drinknumber2 = 0;

        public int[] check = new int[15];
        public int[] beve = new int[15];
        public int[] Positions = new int[8];
        public int vodkayesno = 0;
        public int whiskyyesno = 0;
        public int tequilayesno = 0;
        public int limeyesno = 0;
        public int wateryesno = 0;
        public int orangejuiceyesno = 0;
        public int scothyesno = 0;
        public int Lemmonyesno = 0;
        public int bloodmaryesno = 0;
        public int cubalibreyesno = 0;
        public int caosyesno = 0;
        public int erickyesno = 0;
        public int rayyesno = 0;

        public int margaritayesno = 0;

        public bool newtime;

        public bool Shining = false;
        public bool flagfinish = false;
        public DateTime t;
        static bool flagtoTech;
        static double[] exportBeveragesVals = new double[3];


        public static bool FlagParameters
        {
            get
            {
                return flagtoTech;
            }
            set
            {
                flagtoTech = value;
            }

        }


        public static double[] ExportBeverages
        {
            get
            {
                return exportBeveragesVals;
            }
            set
            {
                exportBeveragesVals = value;
            }

        }

        public Thread Wait;
        private void choosing_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < itemvalue.Length; i++)
                itemvalue[i] = 0;
            this.AutoSize = false;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ///this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Text = "Drink Choose";
            pb_drink.Image = Properties.Resources.glass0;
            Wait = new Thread(new ThreadStart(Reset));
            Wait.Priority = ThreadPriority.Highest;
            Wait.Name = String.Format("GetControllerThread");
            Wait.Start();



        }
        private void choosing_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (Wait != null && Wait.IsAlive)
                Wait.Abort();
           
        }


        public choosing()
        {
            InitializeComponent();
            //ptBox.Image = Properties.Resources.water1;
            //btnOffset = this.Width - btn_Clear.Width;
           // timer1_Tick(this, null);
            timer1.Interval = 1000; // 設定每秒觸發一次
            timer1.Enabled = true; // 啟動 Timer
            ///////////////
            //this.TopMost = true;

            this.lwait.Hide();
            ///////////
            label1.Show();
            bFinish.Hide();

            lb_wait.Hide();
            btn_Clear.Hide();
            bOrder.Hide();
            bRestore.Hide();
            BtnLemmon.Hide();
            btn_tequila.Hide();
            btn_vodka.Hide();
            btn_whisky.Hide();
            btn_lime.Hide();
            btn_orange.Hide();
            btn_water.Hide();
            BtnScoth.Hide();
            btn_cubalibre.Hide();
            btn_chaos.Hide();
            btn_Erick.Hide();
            btn_ray.Hide();
            btn_bloodmary.Hide();
            btn_margarita.Hide();
            pb_drink.Hide();
            label_vodka.Hide();
            label_whisky.Hide();
            label_tequila.Hide();
            label_lime.Hide();
            label_orangejuice.Hide();
            label_water.Hide();
            label_bloodmary.Hide();
            label_margarita.Hide();
            label_scotch.Hide();
            label_lemmonjuice.Hide();
            label_cubalibre.Hide();
            label_erick.Hide();
            label_ray.Hide();
            
            label_chaos.Hide();

            BtnLemmon.FlatStyle = FlatStyle.Flat;
            btn_vodka.FlatStyle = FlatStyle.Flat;
            btn_tequila.FlatStyle = FlatStyle.Flat;
            btn_whisky.FlatStyle = FlatStyle.Flat;
            btn_lime.FlatStyle = FlatStyle.Flat;
            btn_orange.FlatStyle = FlatStyle.Flat;
            btn_water.FlatStyle = FlatStyle.Flat;
            BtnScoth.FlatStyle = FlatStyle.Flat;

            btn_cubalibre.FlatStyle = FlatStyle.Flat;
            btn_chaos.FlatStyle = FlatStyle.Flat;
            btn_Erick.FlatStyle = FlatStyle.Flat;
            btn_ray.FlatStyle = FlatStyle.Flat;
            btn_bloodmary.FlatStyle = FlatStyle.Flat;
            btn_margarita.FlatStyle = FlatStyle.Flat;
            ////////////////////////
           /* using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "8578";
                dlg.Filter = "bmp files (*.pnp)|*.pnp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureBox PictureBox1 = new PictureBox();
                    ptBox.Image = new Bitmap(dlg.FileName);

                    // Add the new control to its parent's controls collection
                    this.Controls.Add(PictureBox1);
                }
            }
            */
           



            //choosing_Load();

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.Menu = null;
            
        }

        

        private void bPrepare_By_Me_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < beve.Length; i++)
                beve[i] = 0;
            for (int i = 0; i < check.Length; i++)
                check[i] = 0;
            for (int i = 0; i < Positions.Length; i++)
                Positions[i] = 0;
            Mode_state = 1;

            
            bPrepare_By_Me.Hide();
            pb_drink.Show();
            bpredefine.Hide();
            
            //pictureBox1.Hide();


            //label1.Hide();
            BtnLemmon.Show();
            BtnScoth.Show();
            btn_tequila.Show();
            btn_vodka.Show();
            btn_whisky.Show();
            btn_lime.Show();
            btn_orange.Show();
            btn_water.Show();
            label_vodka.Show();
            label_whisky.Show();
            label_tequila.Show();
            label_lime.Show();
            label_orangejuice.Show();
            label_water.Show();
            label_scotch.Show();
            label_lemmonjuice.Show();
            btn_bloodmary.Hide();
            btn_margarita.Hide();

            bOrder.Show();
            bRestore.Show();
            btn_Clear.Hide();
            bOrder.Enabled = true;
            bRestore.Enabled = true;

            btn_vodka.Enabled = true;
            btn_vodka.FlatAppearance.BorderColor = Color.Black;
            btn_tequila.Enabled = true;
            btn_tequila.FlatAppearance.BorderColor = Color.Black;
            btn_whisky.Enabled = true;
            btn_whisky.FlatAppearance.BorderColor = Color.Black;
            btn_lime.Enabled = true;
            btn_lime.FlatAppearance.BorderColor = Color.Black;
            btn_orange.Enabled = true;
            btn_orange.FlatAppearance.BorderColor = Color.Black;
            btn_water.Enabled = true;
            btn_water.FlatAppearance.BorderColor = Color.Black;
            BtnLemmon.Enabled = true;
            BtnLemmon.FlatAppearance.BorderColor = Color.Black;
            BtnScoth.Enabled = true;
            BtnScoth.FlatAppearance.BorderColor = Color.Black;
            btn_cubalibre.Enabled = true;
            btn_cubalibre.FlatAppearance.BorderColor = Color.Black;
            btn_chaos.Enabled = true;
            btn_chaos.FlatAppearance.BorderColor = Color.Black;
            btn_Erick.Enabled = true;
            btn_Erick.FlatAppearance.BorderColor = Color.Black;

            btn_ray.Enabled = true;
            btn_ray.FlatAppearance.BorderColor = Color.Black;
            
            btn_bloodmary.Enabled = true;
            btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
            btn_margarita.Enabled = true;
            btn_margarita.FlatAppearance.BorderColor = Color.Black;


            /// QR CODE
            /// 

        }

        private void bpredefine_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < beve.Length; i++)
                beve[i] = 0;
            for (int i = 0; i < check.Length; i++)
                check[i] = 0;
            for (int i = 0; i < Positions.Length; i++)
                Positions[i] = 0;

            btn_cubalibre.Enabled = true;
            btn_cubalibre.FlatAppearance.BorderColor = Color.Black;
            btn_bloodmary.Enabled = true;
            btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
            btn_margarita.Enabled = true;
            btn_margarita.FlatAppearance.BorderColor = Color.Black;
            btn_chaos.Enabled = true;
            btn_chaos.FlatAppearance.BorderColor = Color.Black;
            btn_Erick.Enabled = true;
            btn_Erick.FlatAppearance.BorderColor = Color.Black;


            btn_ray.Enabled = true;
            btn_ray.FlatAppearance.BorderColor = Color.Black;

            //////
            pb_drink.Show();
            /////

            Mode_state = 2;

            bPrepare_By_Me.Hide();
            
            bpredefine.Hide();
            btn_Clear.Hide();
            
            //pictureBox1.Hide();

            bOrder.Enabled = true;
            bPrepare_By_Me.Enabled = true;


            //label1.Hide();

            btn_tequila.Hide();
            btn_vodka.Hide();
            btn_whisky.Hide();
            btn_lime.Hide();
            btn_orange.Hide();
            btn_water.Hide();
            BtnLemmon.Hide();
            
            btn_bloodmary.Show();
            btn_margarita.Show();
            btn_cubalibre.Show();
            btn_chaos.Show();
            btn_Erick.Show();
            btn_ray.Show();
            label_bloodmary.Show();
            label_margarita.Show();
            label_cubalibre.Show();
            label_ray.Show();
            //label_scotch.Show();
            label_chaos.Show();
            label_erick.Show();
            
            bOrder.Show();
            bRestore.Show();
            
        }
        public bool Fstate()
        {
            flagtoTech = State;
            return flagtoTech;
        }
        public void MyiMAGE()
        {
           if(drinknumber==0)
            pb_drink.Image= Properties.Resources.glass0;
           if (drinknumber == 1)
               pb_drink.Image = Properties.Resources.glass1;
           if (drinknumber == 2)
               pb_drink.Image = Properties.Resources.glass3;
           if (drinknumber == 3)
               pb_drink.Image = Properties.Resources.glass4;
        
          
        }
        public Tech_InfoCaudal MyInfo;
        public delegate void DelegateMyInfo(Tech_InfoCaudal MyInfo, bool show);

       

        private void bOrder_Click(object sender, EventArgs e)
        {
           

            flagtoTech = true;
           // Fstate(State);
            vodkayesno = 0;
            whiskyyesno = 0;
            tequilayesno = 0;
            limeyesno = 0;
            wateryesno = 0;
            orangejuiceyesno = 0;
            scothyesno = 0;
            Lemmonyesno = 0;
            bloodmaryesno = 0;
            cubalibreyesno = 0;
            caosyesno = 0;
            erickyesno = 0;
            rayyesno = 0;
            margaritayesno = 0;
            drinknumber = 0;
            drinknumber2 = 0;

            this.lwait.Show();
            this.lwait.Size = new System.Drawing.Size(1000, 1000);
            this.lwait.Font = new Font(this.lwait.Font.FontFamily, 100f, this.lwait.Font.Style);
            this.Controls.Add(this.lwait);
            this.lwait.AutoSize = true;

            this.lwait.Text = "Please put your \n cupholder in the \nTable ";
            this.lwait.TextAlign = ContentAlignment.MiddleRight;//MiddleRight;
           /* this.label_wait.AutoSize = false;
           //label_wait.Size = new System.Drawing.Size(100, 100); ;
            this.label_wait.TextAlign = ContentAlignment.MiddleCenter;//MiddleRight;
            this.label_wait.Font = new Font(this.label_wait.Font.FontFamily, 100f, this.label_wait.Font.Style);
            this.label_wait.TextAlign = ContentAlignment.MiddleCenter;//MiddleRight;
            this.label_wait.Location = new System.Drawing.Point(1000, 500);*/
            int [] choose_val = new int [12] ;
            for (int i = 0; i < choose_val.Length; i++)
                choose_val[i] = 0;
            
            Hide1();
           
           
            int count2 = 0;

            for (int i = 0; i < check.Length; i++)
                {
                    if (check[i] != 0)
                    {
                        BeveragePosition[count2] = check[i];
                        count2 += 1;
                    }
                }

               
 
            for (int i = 0; i < BeveragePosition.Length; i++)
            {
                if (BeveragePosition[i] != 0)
                    if (BeveragePosition[i] == 9)
                    {//bloddy mary
                        BeveragePosition[0] = 1;
                        BeveragePosition[1] = 2;
                        BeveragePosition[2] = 3;
                        count2 = 3;
                    }
                if (BeveragePosition[i] == 10)
                {//margarita
                    BeveragePosition[0] = 2;
                    BeveragePosition[1] = 3;
                    BeveragePosition[2] = 4;
                    count2 = 3;
                }
                if (BeveragePosition[i] == 11)
                {//cuba libre
                    BeveragePosition[0] = 4;
                    BeveragePosition[1] = 5;
                    BeveragePosition[2] = 6;
                    count2 = 3;
                }
                if (BeveragePosition[i] == 12)
                {//caos
                    BeveragePosition[0] = 1;
                    BeveragePosition[1] = 3;
                    BeveragePosition[2] = 5;
                    count2 = 3;
                }

                if (BeveragePosition[i] == 13)
                {//eric
                    BeveragePosition[0] = 2;
                    BeveragePosition[1] = 4;
                    BeveragePosition[2] = 6;
                    count2 = 3;
                }

                if (BeveragePosition[i] == 14)
                {//ray
                    BeveragePosition[0] = 1;
                    BeveragePosition[1] = 2;
                    BeveragePosition[2] = 6;
                    count2 = 3;
                }



            }






            refMainfrm.NumberofBeberages = (int)count2;
            
            for (int j = 1; j < choose_val.Length; j++)
                    for (int i = 0; i < BeveragePosition.Length; i++)
                        if (BeveragePosition[i]==j)    
                            choose_val[j] = 1;

                Array.Sort(BeveragePosition);

                refMainfrm.iChoose[0] = (int)BeveragePosition[0];
                refMainfrm.iChoose[1] = (int)BeveragePosition[1];
                refMainfrm.iChoose[2] = (int)BeveragePosition[2];
                
                SavePositionsVals(BeveragePosition);

                //if (this.InvokeRequired)
                //{
                //    Tech_InfoCaudal UpdateLabel = new Tech_InfoCaudal();
                //    this.Invoke(UpdateLabel, MyInfo.DataView(), true);
                //}
              

                // clear all
                for (int i = 0; i < itemvalue.Length; i++)
                    itemvalue[i] = 0;
                
                for (int i = 0; i < BeveragePosition.Length; i++)
                    BeveragePosition[i] = 0;

                for (int i = 0; i < beve.Length; i++)
                    beve[i] = 0;
                for (int i = 0; i < check.Length; i++)
                    check[i] = 0;
                for (int i = 0; i < Positions.Length; i++)
                    Positions[i] = 0;
            
            
            if (refMainfrm.SerialNum > 6)
                refMainfrm.SerialNum = 1;

           // refMainfrm.MyFlag = true;
           refMainfrm.SerialNum++;
            refMainfrm.bStartMix = true;

        }

        public void SavePositionsVals(double[] BeveragePosition)
        {
            for (int i = 0; i < BeveragePosition.Length; i++)
                exportBeveragesVals[i] = BeveragePosition[i];
        }

        public void Myfunction()
        {
            this.lb_wait.Text = "sdfsdfsdfsd";

            //this.lwait.Hide();
            //this.pictureBox1.Show();
            //start_flag = refMainfrm.finish_flag;
            
        }
        public void Hide1()
        {
            pb_drink.Hide();
            pictureBox1.Show();
            label1.Show();
            ////////////////
          

            bPrepare_By_Me.Hide();
            bpredefine.Hide();
            /////////////////////
            btn_tequila.Hide();
            btn_vodka.Hide();
            btn_whisky.Hide();
            btn_lime.Hide();
            btn_orange.Hide();
            btn_water.Hide();
            btn_bloodmary.Hide();
            btn_margarita.Hide();
            BtnLemmon.Hide();
            btn_cubalibre.Hide();
            btn_chaos.Hide();
            btn_ray.Hide();
            btn_Erick.Hide();
            BtnScoth.Hide();
            

            label_vodka.Hide();
            label_whisky.Hide();
            label_tequila.Hide();
            label_lime.Hide();
            label_orangejuice.Hide();
            label_water.Hide();
            label_bloodmary.Hide();
            label_margarita.Hide();
            label_scotch.Hide();
            label_lemmonjuice.Hide();
            label_cubalibre.Hide();
            label_erick.Hide();
            label_chaos.Hide();
            label_ray.Hide();

            btn_vodka.Enabled = true;
            btn_vodka.FlatAppearance.BorderColor = Color.Black;
            btn_tequila.Enabled = true;
            btn_tequila.FlatAppearance.BorderColor = Color.Black;
            btn_whisky.Enabled = true;
            btn_whisky.FlatAppearance.BorderColor = Color.Black;
            btn_lime.Enabled = true;
            btn_lime.FlatAppearance.BorderColor = Color.Black;
            btn_orange.Enabled = true;
            btn_orange.FlatAppearance.BorderColor = Color.Black;
            btn_water.Enabled = true;
            btn_water.FlatAppearance.BorderColor = Color.Black;
            BtnLemmon.Enabled = true;
            BtnLemmon.FlatAppearance.BorderColor = Color.Black;
            BtnScoth.Enabled = true;
            BtnScoth.FlatAppearance.BorderColor = Color.Black;
            btn_cubalibre.Enabled = true;
            btn_cubalibre.FlatAppearance.BorderColor = Color.Black;
            btn_chaos.Enabled = true;
            btn_chaos.FlatAppearance.BorderColor = Color.Black;
            btn_Erick.Enabled = true;
            btn_Erick.FlatAppearance.BorderColor = Color.Black;
            btn_ray.Enabled = true;
            btn_ray.FlatAppearance.BorderColor = Color.Black;

            
            btn_bloodmary.Enabled = true;
            btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
            btn_margarita.Enabled = true;
            btn_margarita.FlatAppearance.BorderColor = Color.Black;

            
            //drinknumber = 0;

            bpredefine.Enabled = true;
            bPrepare_By_Me.Enabled = true;

            bOrder.Hide();
            bRestore.Hide();
            btn_Clear.Hide();

            
            btn_bloodmary.FlatAppearance.BorderColor = Color.Black;

            label_vodka.ForeColor = System.Drawing.Color.Black;
            label_whisky.ForeColor = System.Drawing.Color.Black;
            label_tequila.ForeColor = System.Drawing.Color.Black;
            label_lime.ForeColor = System.Drawing.Color.Black;
            label_orangejuice.ForeColor = System.Drawing.Color.Black;
            label_water.ForeColor = System.Drawing.Color.Black;
            label_lemmonjuice.ForeColor = System.Drawing.Color.Black;
            label_scotch.ForeColor = System.Drawing.Color.Black;
            
            label_bloodmary.ForeColor = System.Drawing.Color.Black;
            label_margarita.ForeColor = System.Drawing.Color.Black;
            label_cubalibre.ForeColor = System.Drawing.Color.Black;
            label_chaos.ForeColor = System.Drawing.Color.Black;
            label_erick.ForeColor = System.Drawing.Color.Black;
            label_ray.ForeColor = System.Drawing.Color.Black;
        
        
        }
        public void Select_Order(double[] BeveragePosition, int count, PointInf[] Points_save)
        {

            int tester = 0 ;
            double tb_Speed = PointForm.Parameters;
            Form1 Function_pointer = new Form1();
            PointInf[] point; //= new PointInf[Points_save.Length];
            point = Points_save;
            PointInf[] Pptf = new PointInf[4];
            PointInf[] shakepoints=new PointInf[4]; ;
            shakepoints[0] = Points_save[10];
            shakepoints[1] = Points_save[11];
            shakepoints[2] = Points_save[12];
            shakepoints[3] = Points_save[13];
            
            for (int i = 1; i <= 12; i++)
            {
                    for (int j = 0; j < BeveragePosition.Length; j++)
                    {
                        if (BeveragePosition[j] < 9)
                        {
                            if (BeveragePosition[j] == i)
                                Pptf[j] = point[i - 1];
                        }
                        else if (BeveragePosition[j] == 9)
                            tester = 1;
                        else if (BeveragePosition[j] == 10)
                           tester = 2;
                        else if (BeveragePosition[j] == 11)
                            tester = 3;
               
                    }
           }
            
          
        }

        private void bRestore_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < beve.Length; i++)
                beve[i] = 0;
            for (int i = 0; i < check.Length; i++)
                check[i] = 0;
            for (int i = 0; i < Positions.Length; i++)
                Positions[i] = 0;
            btn_vodka.Enabled = true;
            btn_vodka.FlatAppearance.BorderColor = Color.Black;
            btn_tequila.Enabled = true;
            btn_tequila.FlatAppearance.BorderColor = Color.Black;
            btn_whisky.Enabled = true;
            btn_whisky.FlatAppearance.BorderColor = Color.Black;
            btn_lime.Enabled = true;
            btn_lime.FlatAppearance.BorderColor = Color.Black;
            btn_orange.Enabled = true;
            btn_orange.FlatAppearance.BorderColor = Color.Black;
            btn_water.Enabled = true;
            btn_water.FlatAppearance.BorderColor = Color.Black;
            BtnLemmon.Enabled = true;
            BtnLemmon.FlatAppearance.BorderColor = Color.Black;
            BtnScoth.Enabled = true;
            BtnScoth.FlatAppearance.BorderColor = Color.Black;
            btn_cubalibre.Enabled = true;
            btn_cubalibre.FlatAppearance.BorderColor = Color.Black;
            btn_bloodmary.Enabled = true;
            btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
            btn_margarita.Enabled = true;
            btn_margarita.FlatAppearance.BorderColor = Color.Black;
            
            drinknumber = 0;
            pictureBox1.Show();
            bPrepare_By_Me.Show();
            bpredefine.Show();
            btn_tequila.Hide();
            btn_vodka.Hide();
            btn_whisky.Hide();
            btn_lime.Hide();
            btn_orange.Hide();
            btn_water.Hide();
            btn_bloodmary.Hide();
            btn_margarita.Hide();
            BtnScoth.Hide();
            BtnLemmon.Hide();
            btn_cubalibre.Hide();
            btn_chaos.Hide();
            btn_ray.Hide();

            btn_Erick.Hide();
            label_vodka.Hide();
            label_whisky.Hide();
            label_tequila.Hide();
            label_lime.Hide();
            label_orangejuice.Hide();
            label_water.Hide();
            label_bloodmary.Hide();
            label_margarita.Hide();
            label_scotch.Hide();
            label_lemmonjuice.Hide();
            label_cubalibre.Hide();
            label_chaos.Hide();
            label_erick.Hide();
            label_ray.Hide();
            label1.Show();

            btn_vodka.Enabled = true;
            btn_tequila.Enabled = true;
            btn_whisky.Enabled = true;
            btn_lime.Enabled = true;
            btn_orange.Enabled = true;
            btn_water.Enabled = true;
            btn_bloodmary.Enabled = true;
            btn_margarita.Enabled = true;
            BtnLemmon.Enabled = true;
            BtnScoth.Enabled = true;
            //drinknumber = 0;

            bpredefine.Enabled = true;
            bPrepare_By_Me.Enabled = true;

            bOrder.Hide();
            bRestore.Hide();
            btn_Clear.Hide();

            label_vodka.ForeColor = System.Drawing.Color.Black;
            label_whisky.ForeColor = System.Drawing.Color.Black;
            label_tequila.ForeColor = System.Drawing.Color.Black;
            label_lime.ForeColor = System.Drawing.Color.Black;
            label_orangejuice.ForeColor = System.Drawing.Color.Black;
            label_water.ForeColor = System.Drawing.Color.Black;
            label_bloodmary.ForeColor = System.Drawing.Color.Black;
            label_margarita.ForeColor = System.Drawing.Color.Black;
            label_lemmonjuice.ForeColor = System.Drawing.Color.Black;
            label_cubalibre.ForeColor = System.Drawing.Color.Black;
            label_erick.ForeColor = System.Drawing.Color.Black;
            label_chaos.ForeColor = System.Drawing.Color.Black;
            label_ray.ForeColor = System.Drawing.Color.Black;
            label_scotch.ForeColor = System.Drawing.Color.Black;



            vodkayesno = 0;
            whiskyyesno = 0;
            tequilayesno = 0;
            limeyesno = 0;
            wateryesno = 0;
            orangejuiceyesno = 0;
            scothyesno = 0;
            Lemmonyesno = 0;
        }


        public void check_number()
        {
            if (drinknumber > 2)
            {
                drinknumber = 0;
                Disable_ever();
            }
            

        }
        public void check_number2()
        {
            if (drinknumber2 > 1)
            {
                drinknumber2 = 0;
                Disable_ever2();
            }
            

        }
        public void Disable_ever2()
        {
            btn_bloodmary.Enabled = false;
            btn_margarita.Enabled = false;
        }
        public void Disable_ever()
        {
            btn_vodka.Enabled = false;
            btn_whisky.Enabled = false;
            btn_tequila.Enabled = false;
            btn_lime.Enabled = false;
            btn_orange.Enabled = false;
            btn_water.Enabled = false;
            BtnLemmon.Enabled = false;
            BtnScoth.Enabled = false;
        }
        
        public void sort(ref int[] Actual)
        {
            int []newAr = new int[Actual.Length];

            int counter = 0;
            for (int j = 0; j < Actual.Length; j++)
            {
                if (Actual[j] != 0)
                {
                    newAr[counter] = Actual[j];
                    counter += 1;
                }
            }
            Actual = newAr;
        }
         public void checking(int actual_beverage, int state )
        {
            MyiMAGE();
            int before_val = actual_beverage;
            if (state == 1)
            {
                Positions[drinknumber-1] = before_val;
            }
            else if (state == 0)
            {
                for (int i = 0; i < Positions.Length; i++)
                    if (Positions[i] == actual_beverage)
                        Positions[i] = 0;
              
                
            }
            int lenght = 4;
            if (drinknumber > 3)
            {
                if (Positions[drinknumber - lenght] == 1)
                {
                    label_vodka.ForeColor = System.Drawing.Color.Black;
                    btn_vodka.FlatAppearance.BorderColor = Color.Black;
                    vodkayesno = 0;
                }

                if (Positions[drinknumber - lenght] == 2)
                {
                    label_whisky.ForeColor = System.Drawing.Color.Black;
                    btn_whisky.ForeColor = System.Drawing.Color.Black;
                    
                    whiskyyesno = 0;
                }

                if (Positions[drinknumber - lenght] == 3)
                {
                    label_tequila.ForeColor = System.Drawing.Color.Black;
                    btn_tequila.ForeColor = System.Drawing.Color.Black;
                    tequilayesno = 0;
                }

                if (Positions[drinknumber - lenght] == 4)
                 {
                     label_lime.ForeColor = System.Drawing.Color.Black;
                     btn_lime.ForeColor = System.Drawing.Color.Black;
                     limeyesno = 0;
                 }
                if (Positions[drinknumber - lenght] == 5)
                 {
                     label_orangejuice.ForeColor = System.Drawing.Color.Black;
                     btn_orange.ForeColor = System.Drawing.Color.Black;
                     orangejuiceyesno = 0;
                 }
                if (Positions[drinknumber - lenght] == 6)
                 {
                     label_water.ForeColor = System.Drawing.Color.Black;
                     btn_water.ForeColor = System.Drawing.Color.Black;
                     wateryesno = 0;
                 }
                if (Positions[drinknumber - lenght] == 7)
                 {
                     label_scotch.ForeColor = System.Drawing.Color.Black;
                     BtnScoth.ForeColor = System.Drawing.Color.Black;
                     scothyesno = 0;
                 }

                Positions[0] = 0;
                drinknumber = 3;
            }
            sort(ref Positions);


            //int counter = 0;
            for (int j = 0; j < Positions.Length; j++)
            {
                check[j] = Positions[j];
                
            }
                  
            int tester1 = 0;
            int tester2 = 0;
            int tester3 = 0;
            int tester4 = 0;
            int tester5 = 0;
            int tester6 = 0;
            int tester7 = 0;
            int tester8 = 0;
           for (int j = 0; j < check.Length; j++)
            {
                if (check[j] != 0)
                {
                    if (check[j] == 1)
                    {
                        label_vodka.ForeColor = System.Drawing.Color.Red;
                        btn_vodka.FlatAppearance.BorderColor = Color.Red;
                        tester1 = 1;
                        
                      

                    }
                    if (check[j] == 2)
                    {
                        label_whisky.ForeColor = System.Drawing.Color.Red;

                        btn_whisky.FlatAppearance.BorderColor = Color.Red;
                        tester2 = 1;
                       
                       
                         
                    }

                    if (check[j] == 3)
                    {
                        label_tequila.ForeColor = System.Drawing.Color.Red;
                        btn_tequila.FlatAppearance.BorderColor = Color.Red;
                        tester3 = 1;
                       
                       
                    }
                    if (check[j] == 4)
                    {
                        label_lime.ForeColor = System.Drawing.Color.Red;
                        btn_lime.FlatAppearance.BorderColor = Color.Red;
                        tester4 = 1;
                        
                    }
                    if (check[j] == 5)
                    {
                        label_orangejuice.ForeColor = System.Drawing.Color.Red;
                        btn_orange.FlatAppearance.BorderColor = Color.Red;
                        tester5 = 1;
                    }
                    if (check[j] == 6)
                    {
                        label_water.ForeColor = System.Drawing.Color.Red;

                        btn_water.FlatAppearance.BorderColor = Color.Red;
                        tester6 = 1;
                    }
                    if (check[j] == 7)
                    {
                        label_scotch.ForeColor = System.Drawing.Color.Red;

                        BtnScoth.FlatAppearance.BorderColor = Color.Red;
                        tester7 = 1;
                    }
                    if (check[j] == 8)
                    {
                        label_lemmonjuice.ForeColor = System.Drawing.Color.Red;
                        BtnLemmon.FlatAppearance.BorderColor = Color.Red;
                        tester8 = 1;
                    }
                }
              
                else 
                {
                    if (tester1 == 0)
                    {
                        label_vodka.ForeColor = System.Drawing.Color.Black;
                        btn_vodka.FlatAppearance.BorderColor = Color.Black;
                        vodkayesno = 0;
                       
                       
                    }

                    if (tester2 == 0)
                    {
                        label_whisky.ForeColor = System.Drawing.Color.Black;

                        btn_whisky.FlatAppearance.BorderColor = Color.Black;
                        whiskyyesno = 0;
                       
                       
                    }

                    if (tester3 == 0)
                    {
                        label_tequila.ForeColor = System.Drawing.Color.Black;
                        btn_tequila.FlatAppearance.BorderColor = Color.Black;
                        tequilayesno = 0;
                       
                       
                    }
                    if (tester4 == 0)
                    {
                        label_lime.ForeColor = System.Drawing.Color.Black;

                        btn_lime.FlatAppearance.BorderColor = Color.Black;
                        limeyesno = 0;
                    }
                    if (tester5 == 0)
                    {

                        label_orangejuice.ForeColor = System.Drawing.Color.Black;
                        btn_orange.FlatAppearance.BorderColor = Color.Black;
                        orangejuiceyesno = 0;
                        
                    }

                    if (tester6 == 0)
                    {
                        label_water.ForeColor = System.Drawing.Color.Black;

                        btn_water.FlatAppearance.BorderColor = Color.Black;
                        wateryesno = 0;
                    }

                    if (tester7 == 0)
                    {
                        label_scotch.ForeColor = System.Drawing.Color.Black;

                        BtnScoth.FlatAppearance.BorderColor = Color.Black;
                        scothyesno = 0;
                    }
                    if (tester8 == 0)
                    {
                        label_lemmonjuice.ForeColor = System.Drawing.Color.Black;

                        BtnLemmon.FlatAppearance.BorderColor = Color.Black;
                        Lemmonyesno = 0;
                    }
                }
                
            }
         
                    
                
               
            
        }

        
        private void btn_vodka_Click(object sender, EventArgs e)
        {
            beve[0] = 1;
            if (vodkayesno == 0)
            {
                drinknumber++;
                vodkayesno = 1;

               
            }
            else if (vodkayesno == 1)
            {

                
                vodkayesno = 0;
                drinknumber--;
            }
            checking(beve[0], vodkayesno);
        }
        private void btn_whisky_Click(object sender, EventArgs e)
        {

            beve[1] = 2;
            if (whiskyyesno == 0)
            {

                whiskyyesno = 1;
                drinknumber++;
                

            }
            else if (whiskyyesno == 1)
            {
                whiskyyesno = 0;
                drinknumber--;
               
            }
            checking(beve[1], whiskyyesno);
            
           
        }
        private void btn_tequila_Click(object sender, EventArgs e)
        {

            beve[2] = 3;
            if (tequilayesno == 0)
            {
                tequilayesno = 1;
                drinknumber++;
               
            }
            else if (tequilayesno == 1)
            {
                tequilayesno = 0;
                drinknumber--;
                
            }
            checking(beve[2], tequilayesno);

        }
        private void btn_lime_Click(object sender, EventArgs e)
        {
            beve[3] = 4;
            if (limeyesno == 0)
            {
                limeyesno = 1;
                drinknumber++;
                
            }
            else if (limeyesno == 1)
            {
                limeyesno = 0;
                drinknumber--;
                
            }
            checking(beve[3], limeyesno);
                      
        }
        private void btn_orange_Click(object sender, EventArgs e)
        {
            beve[4] = 5;
            if (orangejuiceyesno == 0)
            {
                orangejuiceyesno = 1;
                drinknumber++;
                
            }
            else if (orangejuiceyesno == 1)
            {
                orangejuiceyesno = 0;
                drinknumber--;
                
            }
            checking(beve[4], orangejuiceyesno);
           
           
        }
        private void btn_water_Click(object sender, EventArgs e)
        {
            beve[5] = 6;
            if (wateryesno == 0)
            {
                wateryesno = 1;
                drinknumber++;
               
            }
            else if (wateryesno == 1)
            {
                wateryesno = 0;
                drinknumber--;
                
            }
            checking(beve[5], wateryesno);
        }
        private void Scoth_Click(object sender, EventArgs e)
        {
            beve[6] = 7;
            if (scothyesno == 0)
            {
                scothyesno = 1;
                drinknumber++;

            }
            else if (scothyesno == 1)
            {
                scothyesno = 0;
                drinknumber--;
                
            }
            checking(beve[6], scothyesno);
          }
        private void BtnLemmon_Click(object sender, EventArgs e)
        {
            beve[7] = 8;
            if (Lemmonyesno == 0)
            {
                Lemmonyesno = 1;
                drinknumber++;
                
            }
            else if (Lemmonyesno == 1)
            {
                Lemmonyesno = 0;
                drinknumber--;
                
            }
            checking(beve[7], Lemmonyesno);

           
        }

        public void checking2(int actual_beverage, int state)
        {
            int before_val = actual_beverage;
            if (state == 1)
            {
                Positions[drinknumber2 - 1] = before_val;
            }
            else if (state == 0)
            {
                for (int i = 0; i < Positions.Length; i++)
                    if (Positions[i] == actual_beverage)
                        Positions[i] = 0;


            }
            int lenght = 2;
            if (drinknumber2 > 1)
            {
                if (Positions[drinknumber2 - lenght] == 9)
                {
                    label_bloodmary.ForeColor = System.Drawing.Color.Black;
                    btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
                    bloodmaryesno = 0;
                    
                }

                if (Positions[drinknumber2 - lenght] == 10)
                {

                    label_margarita.ForeColor = System.Drawing.Color.Black;
                    btn_margarita.ForeColor = System.Drawing.Color.Black;
                    margaritayesno = 0;

                    
                    
                }

                if (Positions[drinknumber2 - lenght] == 11)
                {
                    label_cubalibre.ForeColor = System.Drawing.Color.Black;
                    btn_cubalibre.ForeColor = System.Drawing.Color.Black;

                    cubalibreyesno = 0;
                    
                }
                if (Positions[drinknumber2 - lenght] == 12)
                {
                    label_chaos.ForeColor = System.Drawing.Color.Black;
                    btn_chaos.ForeColor = System.Drawing.Color.Black;

                    caosyesno = 0;

                }

                if (Positions[drinknumber2 - lenght] == 13)
                {
                    label_erick.ForeColor = System.Drawing.Color.Black;
                    btn_Erick.ForeColor = System.Drawing.Color.Black;

                    erickyesno = 0;

                }

                 if (Positions[drinknumber2 - lenght] == 14)
                {
                    label_ray.ForeColor = System.Drawing.Color.Black;
                    btn_ray.ForeColor = System.Drawing.Color.Black;

                    rayyesno = 0;

                }


                




                Positions[0] = 0;
                drinknumber2 = 1;
            }
            sort(ref Positions);


            //int counter = 0;
            for (int j = 0; j < Positions.Length; j++)
            {
                check[j] = Positions[j];

            }

            int tester1 = 0;
            int tester2 = 0;
            int tester3 = 0;
            int tester4 = 0;
            int tester5 = 0;
            int tester6 = 0;



            for (int j = 0; j < check.Length-10; j++)
            {
                if (check[j] != 0)
                {
                    if (check[j] == 9)
                    {
                        label_bloodmary.ForeColor = System.Drawing.Color.Red;
                        btn_bloodmary.FlatAppearance.BorderColor = Color.Red;
                        tester1 = 1;



                    }
                    if (check[j] == 10)
                    {
                        label_margarita.ForeColor = System.Drawing.Color.Red;
                        btn_margarita.FlatAppearance.BorderColor = Color.Red;
                        tester2 = 1;
                        
                      
                    }

                    if (check[j] == 11)
                    {
                        label_cubalibre.ForeColor = System.Drawing.Color.Red;
                        btn_cubalibre.FlatAppearance.BorderColor = Color.Red;
                        tester3 = 1;
                    }
                    if (check[j] == 12)
                    {
                        label_chaos.ForeColor = System.Drawing.Color.Red;
                        btn_chaos.FlatAppearance.BorderColor = Color.Red;
                        tester4 = 1;
                    }

                    if (check[j] == 13)
                    {
                        label_erick.ForeColor = System.Drawing.Color.Red;
                        btn_Erick.FlatAppearance.BorderColor = Color.Red;
                        tester5 = 1;
                    }

                    if (check[j] == 14)
                    {
                        label_ray.ForeColor = System.Drawing.Color.Red;
                        btn_ray.FlatAppearance.BorderColor = Color.Red;
                        tester6 = 1;
                    }



                    
                }

                else
                {
                    if (tester1 == 0)
                    {
                        label_bloodmary.ForeColor = System.Drawing.Color.Black;
                        btn_bloodmary.FlatAppearance.BorderColor = Color.Black;
                        bloodmaryesno = 0;


                    }

                    if (tester2 == 0)
                    {
                        label_margarita.ForeColor = System.Drawing.Color.Black;
                        btn_margarita.FlatAppearance.BorderColor = Color.Black;
                        margaritayesno = 0;
                    }

                    if (tester3 == 0)
                    {

                        label_cubalibre.ForeColor = System.Drawing.Color.Black;
                        btn_cubalibre.FlatAppearance.BorderColor = Color.Black;
                        cubalibreyesno = 0;


                    }
                    if (tester4 == 0)
                    {

                        label_chaos.ForeColor = System.Drawing.Color.Black;
                        btn_chaos.FlatAppearance.BorderColor = Color.Black;
                        caosyesno = 0;


                    }


                    if (tester5 == 0)
                    {

                        label_erick.ForeColor = System.Drawing.Color.Black;
                        btn_Erick.FlatAppearance.BorderColor = Color.Black;
                        erickyesno = 0;


                    }
                    

                   if (tester6 == 0)
                    {

                        label_ray.ForeColor = System.Drawing.Color.Black;
                        btn_ray.FlatAppearance.BorderColor = Color.Black;
                        rayyesno = 0;


                    }



                }

            }





        }


        private void btn_bloodmary_Click(object sender, EventArgs e)
        {

            beve[8] = 9;
            if (bloodmaryesno == 0)
            {
                bloodmaryesno = 1;
                drinknumber2++;

            }
            else if (bloodmaryesno == 1)
            {
                bloodmaryesno = 0;
                drinknumber2--;

            }
            checking2(beve[8], bloodmaryesno);


            
           
            //check_number2();

        }
        private void btn_cubalibre_Click(object sender, EventArgs e)
        {
             beve[10] = 11;
            if (cubalibreyesno == 0)
            {
                cubalibreyesno = 1;
                drinknumber2++;

            }
            else if (cubalibreyesno == 1)
            {
                cubalibreyesno = 0;
                drinknumber2--;

            }
            checking2(beve[10], cubalibreyesno);

           
        }
        private void btn_margarita_Click(object sender, EventArgs e)
        {
            beve[9] = 10;
            if (margaritayesno == 0)
            {
                margaritayesno = 1;
                drinknumber2++;

            }
            else if (margaritayesno == 1)
            {
                margaritayesno = 0;
                drinknumber2--;

            }
            checking2(beve[9], margaritayesno);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < itemvalue.Length; i++)
                itemvalue[i] = 0;
            btn_vodka.Enabled = true;
            btn_tequila.Enabled = true;
            btn_whisky.Enabled = true;
            btn_lime.Enabled = true;
            btn_orange.Enabled = true;
            btn_water.Enabled = true;
            btn_bloodmary.Enabled = true;
            btn_margarita.Enabled = true;
            BtnScoth.Enabled = true;
            btn_cubalibre.Enabled = true;
            btn_chaos.Enabled = true;
            btn_Erick.Enabled = true;
            btn_ray.Enabled = true;
            BtnLemmon.Enabled = true;
            drinknumber = 0;
            
            label_vodka.ForeColor = System.Drawing.Color.Black;
            label_whisky.ForeColor = System.Drawing.Color.Black;
            label_tequila.ForeColor = System.Drawing.Color.Black;
            label_lime.ForeColor = System.Drawing.Color.Black;
            label_orangejuice.ForeColor = System.Drawing.Color.Black;
            label_water.ForeColor = System.Drawing.Color.Black;
            label_bloodmary.ForeColor = System.Drawing.Color.Black;
            label_margarita.ForeColor = System.Drawing.Color.Black;
            label_scotch.ForeColor = System.Drawing.Color.Black;
            label_lemmonjuice.ForeColor = System.Drawing.Color.Black;
            label_cubalibre.ForeColor = System.Drawing.Color.Black;
            label_chaos.ForeColor = System.Drawing.Color.Black;
            label_erick.ForeColor = System.Drawing.Color.Black;
            label_ray.ForeColor = System.Drawing.Color.Black;




             vodkayesno = 0;
             whiskyyesno = 0;
             tequilayesno = 0;
             limeyesno = 0;
             wateryesno = 0;
             orangejuiceyesno = 0;
             scothyesno = 0;
             Lemmonyesno = 0;

        }
        private void bQR_Click(object sender, EventArgs e)
        {
            // Create a new form.

            choosing form2 = new choosing();
            // Set the text displayed in the caption.
            form2.Text = "My Form";
            // Set the opacity to 75%.
            form2.Opacity = .75;
            // Size the form to be 300 pixels in height and width.
            form2.Size = new Size(300, 300);
            // Display the form in the center of the screen.
            form2.StartPosition = FormStartPosition.CenterScreen;

            // Display the form as a modal dialog box.
            form2.ShowDialog();

          
        }
      
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            t = DateTime.Now;
            label3.Text = t.Year + "/" + t.Month + "/" + t.Day;
            label2.Text = t.Hour + ":" + t.Minute + ":" + t.Second; 
            
            label2.Font = new Font(label3.Font.FontFamily, 20f, label3.Font.Style);
            label2.TextAlign = ContentAlignment.MiddleCenter;//MiddleRight;

            label3.Font = new Font(label3.Font.FontFamily, 20f, label3.Font.Style);
            label3.TextAlign = ContentAlignment.MiddleCenter;//MiddleRight;

            if (Shining)
            {
                lwait.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lwait.ForeColor = Color.Black ;
            }
            Shining = !Shining;


        }
        private void Reset()
        {
            while (flagfinish)
            {
                this.lwait.Hide();
                bPrepare_By_Me.Show();
                bpredefine.Show();
                bFinish.Hide();
                State = false;
                flagfinish = !flagfinish;
            }
        
        }

        

        private void label_scotch_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_chaos_Click(object sender, EventArgs e)
        {
            beve[11] = 12;
            if (caosyesno == 0)
            {
                caosyesno = 1;
                drinknumber2++;

            }
            else if (caosyesno == 1)
            {
                caosyesno = 0;
                drinknumber2--;

            }
            checking2(beve[11], caosyesno);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            beve[13] = 14;
            if (rayyesno == 0)
            {
                rayyesno = 1;
                drinknumber2++;

            }
            else if (rayyesno == 1)
            {
                rayyesno = 0;
                drinknumber2--;

            }
            checking2(beve[13], rayyesno);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_Erick_Click(object sender, EventArgs e)
        {
            beve[12] = 13;
            if (erickyesno == 0)
            {
                erickyesno = 1;
                drinknumber2++;

            }
            else if (erickyesno == 1)
            {
                erickyesno = 0;
                drinknumber2--;

            }
            checking2(beve[12], erickyesno);
        }

        private void label_cubalibre_Click(object sender, EventArgs e)
        {

        }
        
        

        
        

       


       
      


    }
}
