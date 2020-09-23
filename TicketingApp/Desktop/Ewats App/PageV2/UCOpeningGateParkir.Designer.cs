namespace Ewats_App.PageV2
{
    partial class UCOpeningGateParkir
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCOpeningGateParkir));
            this.panelOpening = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelesai = new System.Windows.Forms.Button();
            this.picShootKamera = new System.Windows.Forms.PictureBox();
            this.lblTimerOpening = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TimerOpening = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAction = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelOpening.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShootKamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOpening
            // 
            this.panelOpening.BackColor = System.Drawing.SystemColors.Control;
            this.panelOpening.Controls.Add(this.panel2);
            this.panelOpening.Controls.Add(this.lblAction);
            this.panelOpening.Controls.Add(this.panel1);
            this.panelOpening.Controls.Add(this.btnSelesai);
            this.panelOpening.Controls.Add(this.lblTimerOpening);
            this.panelOpening.Controls.Add(this.label1);
            this.panelOpening.Controls.Add(this.pictureBox1);
            this.panelOpening.Location = new System.Drawing.Point(420, 195);
            this.panelOpening.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelOpening.Name = "panelOpening";
            this.panelOpening.Size = new System.Drawing.Size(1034, 706);
            this.panelOpening.TabIndex = 74;
            this.panelOpening.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOpening_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 8);
            this.panel1.TabIndex = 81;
            // 
            // btnSelesai
            // 
            this.btnSelesai.BackColor = System.Drawing.Color.Tomato;
            this.btnSelesai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelesai.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelesai.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelesai.Location = new System.Drawing.Point(398, 620);
            this.btnSelesai.Name = "btnSelesai";
            this.btnSelesai.Size = new System.Drawing.Size(280, 70);
            this.btnSelesai.TabIndex = 76;
            this.btnSelesai.Text = "Selesai";
            this.btnSelesai.UseVisualStyleBackColor = false;
            this.btnSelesai.Click += new System.EventHandler(this.btnSelesai_Click);
            // 
            // picShootKamera
            // 
            this.picShootKamera.Location = new System.Drawing.Point(27, 19);
            this.picShootKamera.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picShootKamera.Name = "picShootKamera";
            this.picShootKamera.Size = new System.Drawing.Size(390, 390);
            this.picShootKamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShootKamera.TabIndex = 75;
            this.picShootKamera.TabStop = false;
            // 
            // lblTimerOpening
            // 
            this.lblTimerOpening.AutoSize = true;
            this.lblTimerOpening.BackColor = System.Drawing.Color.Transparent;
            this.lblTimerOpening.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerOpening.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTimerOpening.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTimerOpening.Location = new System.Drawing.Point(27, 625);
            this.lblTimerOpening.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimerOpening.Name = "lblTimerOpening";
            this.lblTimerOpening.Size = new System.Drawing.Size(85, 50);
            this.lblTimerOpening.TabIndex = 73;
            this.lblTimerOpening.Text = "600";
            this.lblTimerOpening.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(306, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(415, 39);
            this.label1.TabIndex = 51;
            this.label1.Text = "Simulasi Barrier Gate Opening";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 157);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(539, 428);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 72;
            this.pictureBox1.TabStop = false;
            // 
            // TimerOpening
            // 
            this.TimerOpening.Interval = 1000;
            this.TimerOpening.Tick += new System.EventHandler(this.TimerOpening_Tick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1973, 8);
            this.panel4.TabIndex = 81;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.BackColor = System.Drawing.Color.White;
            this.lblAction.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAction.Location = new System.Drawing.Point(168, 524);
            this.lblAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(212, 39);
            this.lblAction.TabIndex = 52;
            this.lblAction.Text = "Silahkan masuk";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.panel2.Controls.Add(this.picShootKamera);
            this.panel2.Location = new System.Drawing.Point(542, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 428);
            this.panel2.TabIndex = 83;
            // 
            // UCOpeningGateParkir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelOpening);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UCOpeningGateParkir";
            this.Size = new System.Drawing.Size(1973, 1078);
            this.Load += new System.EventHandler(this.UCOpeningGateParkir_Load);
            this.panelOpening.ResumeLayout(false);
            this.panelOpening.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShootKamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOpening;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Timer TimerOpening;
        private System.Windows.Forms.PictureBox picShootKamera;
        public System.Windows.Forms.Label lblTimerOpening;
        private System.Windows.Forms.Button btnSelesai;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Panel panel2;
    }
}
