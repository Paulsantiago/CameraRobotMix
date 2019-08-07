namespace Warp_Csharp
{
    partial class ImageMainfrm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nCCMatchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNCCDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaitonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Picbox = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nCCMatchingToolStripMenuItem,
            this.informaitonToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1105, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nCCMatchingToolStripMenuItem
            // 
            this.nCCMatchingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNCCDialogToolStripMenuItem});
            this.nCCMatchingToolStripMenuItem.Name = "nCCMatchingToolStripMenuItem";
            this.nCCMatchingToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.nCCMatchingToolStripMenuItem.Text = "NCC Matching";
            this.nCCMatchingToolStripMenuItem.Click += new System.EventHandler(this.nCCMatchingToolStripMenuItem_Click);
            // 
            // openNCCDialogToolStripMenuItem
            // 
            this.openNCCDialogToolStripMenuItem.Name = "openNCCDialogToolStripMenuItem";
            this.openNCCDialogToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openNCCDialogToolStripMenuItem.Text = "Open NCC Dialog";
            this.openNCCDialogToolStripMenuItem.Click += new System.EventHandler(this.openNCCDialogToolStripMenuItem_Click);
            // 
            // informaitonToolStripMenuItem
            // 
            this.informaitonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getVersionToolStripMenuItem,
            this.keyToolStripMenuItem});
            this.informaitonToolStripMenuItem.Name = "informaitonToolStripMenuItem";
            this.informaitonToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.informaitonToolStripMenuItem.Text = "Informaiton";
            // 
            // getVersionToolStripMenuItem
            // 
            this.getVersionToolStripMenuItem.Name = "getVersionToolStripMenuItem";
            this.getVersionToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.getVersionToolStripMenuItem.Text = "GetVersion";
            this.getVersionToolStripMenuItem.Click += new System.EventHandler(this.getVersionToolStripMenuItem_Click);
            // 
            // keyToolStripMenuItem
            // 
            this.keyToolStripMenuItem.Name = "keyToolStripMenuItem";
            this.keyToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.keyToolStripMenuItem.Text = "Key";
            this.keyToolStripMenuItem.Click += new System.EventHandler(this.keyToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Picbox
            // 
            this.Picbox.Location = new System.Drawing.Point(10, 30);
            this.Picbox.Name = "Picbox";
            this.Picbox.Size = new System.Drawing.Size(800, 600);
            this.Picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Picbox.TabIndex = 1;
            this.Picbox.TabStop = false;
            this.Picbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picbox_MouseDown);
            this.Picbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Picbox_MouseMove);
            this.Picbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picbox_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 633);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // ImageMainfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 750);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Picbox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImageMainfrm";
            this.Text = "iVision Demo_x64";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mainfrm_FormClosed);
            this.Load += new System.EventHandler(this.Mainfrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nCCMatchingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNCCDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.PictureBox Picbox;
        private System.Windows.Forms.ToolStripMenuItem informaitonToolStripMenuItem;
        public System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem getVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

