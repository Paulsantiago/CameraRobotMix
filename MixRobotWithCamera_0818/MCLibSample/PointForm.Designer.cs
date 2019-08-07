namespace MCLibSample
{
    partial class PointForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Close = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Here = new System.Windows.Forms.Button();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ratioBtn_P12 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P11 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P10 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P9 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P8 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P7 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P6 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P5 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_VisionCenter = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P3 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P2 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P4 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_P1 = new System.Windows.Forms.RadioButton();
            this.ratioBtn_Wait = new System.Windows.Forms.RadioButton();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_PTP = new System.Windows.Forms.Button();
            this.btn_GripRelease = new System.Windows.Forms.Button();
            this.btn_GripCatch = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.tb_Speed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_WaitTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(735, 336);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(126, 47);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column2,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(312, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(602, 294);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "J1";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "J2";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "J3";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "J4";
            this.Column6.Name = "Column6";
            this.Column6.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "J5";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "J6";
            this.Column7.Name = "Column7";
            this.Column7.Width = 80;
            // 
            // btn_Here
            // 
            this.btn_Here.Location = new System.Drawing.Point(35, 226);
            this.btn_Here.Name = "btn_Here";
            this.btn_Here.Size = new System.Drawing.Size(80, 47);
            this.btn_Here.TabIndex = 2;
            this.btn_Here.Text = "Here";
            this.btn_Here.UseVisualStyleBackColor = true;
            this.btn_Here.Click += new System.EventHandler(this.btn_Here_Click);
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(30, 385);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(91, 47);
            this.btn_Load.TabIndex = 3;
            this.btn_Load.Text = "Load";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(127, 385);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(85, 47);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ratioBtn_P12);
            this.groupBox1.Controls.Add(this.ratioBtn_P11);
            this.groupBox1.Controls.Add(this.ratioBtn_P10);
            this.groupBox1.Controls.Add(this.ratioBtn_P9);
            this.groupBox1.Controls.Add(this.ratioBtn_P8);
            this.groupBox1.Controls.Add(this.ratioBtn_P7);
            this.groupBox1.Controls.Add(this.ratioBtn_P6);
            this.groupBox1.Controls.Add(this.ratioBtn_P5);
            this.groupBox1.Controls.Add(this.ratioBtn_VisionCenter);
            this.groupBox1.Controls.Add(this.ratioBtn_P3);
            this.groupBox1.Controls.Add(this.ratioBtn_P2);
            this.groupBox1.Controls.Add(this.ratioBtn_P4);
            this.groupBox1.Controls.Add(this.ratioBtn_P1);
            this.groupBox1.Controls.Add(this.ratioBtn_Wait);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 173);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Point";
            // 
            // ratioBtn_P12
            // 
            this.ratioBtn_P12.AutoSize = true;
            this.ratioBtn_P12.Location = new System.Drawing.Point(129, 87);
            this.ratioBtn_P12.Name = "ratioBtn_P12";
            this.ratioBtn_P12.Size = new System.Drawing.Size(41, 16);
            this.ratioBtn_P12.TabIndex = 13;
            this.ratioBtn_P12.TabStop = true;
            this.ratioBtn_P12.Text = "P12";
            this.ratioBtn_P12.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P11
            // 
            this.ratioBtn_P11.AutoSize = true;
            this.ratioBtn_P11.Location = new System.Drawing.Point(129, 65);
            this.ratioBtn_P11.Name = "ratioBtn_P11";
            this.ratioBtn_P11.Size = new System.Drawing.Size(76, 16);
            this.ratioBtn_P11.TabIndex = 12;
            this.ratioBtn_P11.TabStop = true;
            this.ratioBtn_P11.Text = "P2 to shake";
            this.ratioBtn_P11.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P10
            // 
            this.ratioBtn_P10.AutoSize = true;
            this.ratioBtn_P10.Location = new System.Drawing.Point(129, 43);
            this.ratioBtn_P10.Name = "ratioBtn_P10";
            this.ratioBtn_P10.Size = new System.Drawing.Size(80, 16);
            this.ratioBtn_P10.TabIndex = 11;
            this.ratioBtn_P10.TabStop = true;
            this.ratioBtn_P10.Text = "P1 To shake";
            this.ratioBtn_P10.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P9
            // 
            this.ratioBtn_P9.AutoSize = true;
            this.ratioBtn_P9.Location = new System.Drawing.Point(129, 21);
            this.ratioBtn_P9.Name = "ratioBtn_P9";
            this.ratioBtn_P9.Size = new System.Drawing.Size(47, 16);
            this.ratioBtn_P9.TabIndex = 10;
            this.ratioBtn_P9.TabStop = true;
            this.ratioBtn_P9.Text = "St by";
            this.ratioBtn_P9.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P8
            // 
            this.ratioBtn_P8.AutoSize = true;
            this.ratioBtn_P8.Location = new System.Drawing.Point(71, 86);
            this.ratioBtn_P8.Name = "ratioBtn_P8";
            this.ratioBtn_P8.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P8.TabIndex = 9;
            this.ratioBtn_P8.TabStop = true;
            this.ratioBtn_P8.Text = "P8";
            this.ratioBtn_P8.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P7
            // 
            this.ratioBtn_P7.AutoSize = true;
            this.ratioBtn_P7.Location = new System.Drawing.Point(71, 65);
            this.ratioBtn_P7.Name = "ratioBtn_P7";
            this.ratioBtn_P7.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P7.TabIndex = 8;
            this.ratioBtn_P7.TabStop = true;
            this.ratioBtn_P7.Text = "P7";
            this.ratioBtn_P7.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P6
            // 
            this.ratioBtn_P6.AutoSize = true;
            this.ratioBtn_P6.Location = new System.Drawing.Point(71, 43);
            this.ratioBtn_P6.Name = "ratioBtn_P6";
            this.ratioBtn_P6.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P6.TabIndex = 7;
            this.ratioBtn_P6.TabStop = true;
            this.ratioBtn_P6.Text = "P6";
            this.ratioBtn_P6.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P5
            // 
            this.ratioBtn_P5.AutoSize = true;
            this.ratioBtn_P5.Location = new System.Drawing.Point(71, 21);
            this.ratioBtn_P5.Name = "ratioBtn_P5";
            this.ratioBtn_P5.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P5.TabIndex = 6;
            this.ratioBtn_P5.TabStop = true;
            this.ratioBtn_P5.Text = "P5";
            this.ratioBtn_P5.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_VisionCenter
            // 
            this.ratioBtn_VisionCenter.AutoSize = true;
            this.ratioBtn_VisionCenter.Location = new System.Drawing.Point(16, 109);
            this.ratioBtn_VisionCenter.Name = "ratioBtn_VisionCenter";
            this.ratioBtn_VisionCenter.Size = new System.Drawing.Size(71, 16);
            this.ratioBtn_VisionCenter.TabIndex = 5;
            this.ratioBtn_VisionCenter.TabStop = true;
            this.ratioBtn_VisionCenter.Text = "影像中心";
            this.ratioBtn_VisionCenter.UseVisualStyleBackColor = true;
            this.ratioBtn_VisionCenter.CheckedChanged += new System.EventHandler(this.ratioBtn_VisionCenter_CheckedChanged);
            // 
            // ratioBtn_P3
            // 
            this.ratioBtn_P3.AutoSize = true;
            this.ratioBtn_P3.Location = new System.Drawing.Point(16, 65);
            this.ratioBtn_P3.Name = "ratioBtn_P3";
            this.ratioBtn_P3.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P3.TabIndex = 3;
            this.ratioBtn_P3.TabStop = true;
            this.ratioBtn_P3.Text = "P3";
            this.ratioBtn_P3.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P2
            // 
            this.ratioBtn_P2.AutoSize = true;
            this.ratioBtn_P2.Location = new System.Drawing.Point(16, 43);
            this.ratioBtn_P2.Name = "ratioBtn_P2";
            this.ratioBtn_P2.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P2.TabIndex = 2;
            this.ratioBtn_P2.TabStop = true;
            this.ratioBtn_P2.Text = "P2";
            this.ratioBtn_P2.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P4
            // 
            this.ratioBtn_P4.AutoSize = true;
            this.ratioBtn_P4.Location = new System.Drawing.Point(16, 87);
            this.ratioBtn_P4.Name = "ratioBtn_P4";
            this.ratioBtn_P4.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P4.TabIndex = 4;
            this.ratioBtn_P4.TabStop = true;
            this.ratioBtn_P4.Text = "P4";
            this.ratioBtn_P4.UseVisualStyleBackColor = true;
            // 
            // ratioBtn_P1
            // 
            this.ratioBtn_P1.AutoSize = true;
            this.ratioBtn_P1.Location = new System.Drawing.Point(16, 21);
            this.ratioBtn_P1.Name = "ratioBtn_P1";
            this.ratioBtn_P1.Size = new System.Drawing.Size(35, 16);
            this.ratioBtn_P1.TabIndex = 1;
            this.ratioBtn_P1.TabStop = true;
            this.ratioBtn_P1.Text = "P1";
            this.ratioBtn_P1.UseVisualStyleBackColor = true;
            this.ratioBtn_P1.CheckedChanged += new System.EventHandler(this.ratioBtn_P1_CheckedChanged);
            // 
            // ratioBtn_Wait
            // 
            this.ratioBtn_Wait.AutoSize = true;
            this.ratioBtn_Wait.Location = new System.Drawing.Point(16, 131);
            this.ratioBtn_Wait.Name = "ratioBtn_Wait";
            this.ratioBtn_Wait.Size = new System.Drawing.Size(59, 16);
            this.ratioBtn_Wait.TabIndex = 0;
            this.ratioBtn_Wait.TabStop = true;
            this.ratioBtn_Wait.Text = "等待點";
            this.ratioBtn_Wait.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(644, 336);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(85, 47);
            this.btn_Update.TabIndex = 6;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_PTP
            // 
            this.btn_PTP.Location = new System.Drawing.Point(121, 226);
            this.btn_PTP.Name = "btn_PTP";
            this.btn_PTP.Size = new System.Drawing.Size(80, 47);
            this.btn_PTP.TabIndex = 7;
            this.btn_PTP.Text = "PTP";
            this.btn_PTP.UseVisualStyleBackColor = true;
            this.btn_PTP.Click += new System.EventHandler(this.btn_PTP_Click);
            // 
            // btn_GripRelease
            // 
            this.btn_GripRelease.Location = new System.Drawing.Point(121, 279);
            this.btn_GripRelease.Name = "btn_GripRelease";
            this.btn_GripRelease.Size = new System.Drawing.Size(58, 41);
            this.btn_GripRelease.TabIndex = 55;
            this.btn_GripRelease.Text = "Release";
            this.btn_GripRelease.UseVisualStyleBackColor = true;
            this.btn_GripRelease.Click += new System.EventHandler(this.btn_GripRelease_Click);
            // 
            // btn_GripCatch
            // 
            this.btn_GripCatch.Location = new System.Drawing.Point(57, 279);
            this.btn_GripCatch.Name = "btn_GripCatch";
            this.btn_GripCatch.Size = new System.Drawing.Size(58, 41);
            this.btn_GripCatch.TabIndex = 54;
            this.btn_GripCatch.Text = "Catch";
            this.btn_GripCatch.UseVisualStyleBackColor = true;
            this.btn_GripCatch.Click += new System.EventHandler(this.btn_GripCatch_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(381, 336);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(113, 47);
            this.btn_Start.TabIndex = 56;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // tb_Speed
            // 
            this.tb_Speed.Location = new System.Drawing.Point(112, 341);
            this.tb_Speed.Name = "tb_Speed";
            this.tb_Speed.Size = new System.Drawing.Size(100, 22);
            this.tb_Speed.TabIndex = 57;
            this.tb_Speed.Text = "5000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "Speed";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(516, 336);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 47);
            this.button1.TabIndex = 59;
            this.button1.Text = "Shake";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_WaitTime
            // 
            this.tb_WaitTime.Location = new System.Drawing.Point(332, 414);
            this.tb_WaitTime.Name = "tb_WaitTime";
            this.tb_WaitTime.Size = new System.Drawing.Size(74, 22);
            this.tb_WaitTime.TabIndex = 60;
            this.tb_WaitTime.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "WaitingTime";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(942, 25);
            this.toolStrip1.TabIndex = 62;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::MCLibSample.Properties.Resources.school;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.informationToolStripMenuItem.Text = "Information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // PointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 474);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_WaitTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Speed);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_GripRelease);
            this.Controls.Add(this.btn_GripCatch);
            this.Controls.Add(this.btn_PTP);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.btn_Here);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Close);
            this.Name = "PointForm";
            this.Text = "PointForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PointForm_FormClosing);
            this.Load += new System.EventHandler(this.PointForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Here;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ratioBtn_VisionCenter;
        private System.Windows.Forms.RadioButton ratioBtn_P4;
        private System.Windows.Forms.RadioButton ratioBtn_P3;
        private System.Windows.Forms.RadioButton ratioBtn_P2;
        private System.Windows.Forms.RadioButton ratioBtn_P1;
        private System.Windows.Forms.RadioButton ratioBtn_Wait;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_PTP;
        private System.Windows.Forms.Button btn_GripRelease;
        private System.Windows.Forms.Button btn_GripCatch;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.TextBox tb_Speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton ratioBtn_P12;
        private System.Windows.Forms.RadioButton ratioBtn_P11;
        private System.Windows.Forms.RadioButton ratioBtn_P10;
        private System.Windows.Forms.RadioButton ratioBtn_P9;
        private System.Windows.Forms.RadioButton ratioBtn_P8;
        private System.Windows.Forms.RadioButton ratioBtn_P7;
        private System.Windows.Forms.RadioButton ratioBtn_P6;
        private System.Windows.Forms.RadioButton ratioBtn_P5;
        private System.Windows.Forms.TextBox tb_WaitTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
    }
}