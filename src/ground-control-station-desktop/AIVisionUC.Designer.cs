namespace AutonomousControlStation
{
    partial class AIVisionUC
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
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlCameraBorder = new Guna.UI2.WinForms.Guna2Panel();
            this.picCameraStream = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnlAiData = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnToggleStream = new Guna.UI2.WinForms.Guna2Button();
            this.btnSnapshot = new Guna.UI2.WinForms.Guna2Button();
            this.btnToggleBBox = new Guna.UI2.WinForms.Guna2Button();
            this.pnlCameraBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCameraStream)).BeginInit();
            this.pnlAiData.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(9, 7);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(110, 31);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "AI VISION";
            // 
            // pnlCameraBorder
            // 
            this.pnlCameraBorder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.pnlCameraBorder.BorderRadius = 10;
            this.pnlCameraBorder.BorderThickness = 2;
            this.pnlCameraBorder.Controls.Add(this.picCameraStream);
            this.pnlCameraBorder.FillColor = System.Drawing.Color.Transparent;
            this.pnlCameraBorder.Location = new System.Drawing.Point(9, 44);
            this.pnlCameraBorder.Name = "pnlCameraBorder";
            this.pnlCameraBorder.Size = new System.Drawing.Size(720, 630);
            this.pnlCameraBorder.TabIndex = 1;
            // 
            // picCameraStream
            // 
            this.picCameraStream.BorderRadius = 8;
            this.picCameraStream.ImageRotate = 0F;
            this.picCameraStream.Location = new System.Drawing.Point(5, 5);
            this.picCameraStream.Name = "picCameraStream";
            this.picCameraStream.Padding = new System.Windows.Forms.Padding(2);
            this.picCameraStream.Size = new System.Drawing.Size(710, 620);
            this.picCameraStream.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCameraStream.TabIndex = 0;
            this.picCameraStream.TabStop = false;
            // 
            // pnlAiData
            // 
            this.pnlAiData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(85)))));
            this.pnlAiData.BorderRadius = 15;
            this.pnlAiData.BorderThickness = 2;
            this.pnlAiData.Controls.Add(this.btnToggleBBox);
            this.pnlAiData.Controls.Add(this.btnSnapshot);
            this.pnlAiData.Controls.Add(this.btnToggleStream);
            this.pnlAiData.Controls.Add(this.listBox1);
            this.pnlAiData.Controls.Add(this.guna2HtmlLabel4);
            this.pnlAiData.Controls.Add(this.guna2HtmlLabel3);
            this.pnlAiData.Controls.Add(this.guna2HtmlLabel2);
            this.pnlAiData.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.pnlAiData.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.pnlAiData.Location = new System.Drawing.Point(735, 44);
            this.pnlAiData.Name = "pnlAiData";
            this.pnlAiData.Size = new System.Drawing.Size(292, 630);
            this.pnlAiData.TabIndex = 2;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(85)))));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(12, 24);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(159, 26);
            this.guna2HtmlLabel2.TabIndex = 0;
            this.guna2HtmlLabel2.Text = "AI ENGINE: YOLO";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2HtmlLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(12, 56);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(272, 26);
            this.guna2HtmlLabel3.TabIndex = 0;
            this.guna2HtmlLabel3.Text = "HARDWARE: RASPBERRY PI 5";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(12, 88);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(69, 26);
            this.guna2HtmlLabel4.TabIndex = 0;
            this.guna2HtmlLabel4.Text = "FPS: 30";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Items.AddRange(new object[] {
            "Person - 85%",
            "Vehicle - 92%"});
            this.listBox1.Location = new System.Drawing.Point(30, 143);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 240);
            this.listBox1.TabIndex = 2;
            // 
            // btnToggleStream
            // 
            this.btnToggleStream.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnToggleStream.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnToggleStream.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnToggleStream.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnToggleStream.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
            this.btnToggleStream.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnToggleStream.ForeColor = System.Drawing.Color.Black;
            this.btnToggleStream.Location = new System.Drawing.Point(12, 438);
            this.btnToggleStream.Name = "btnToggleStream";
            this.btnToggleStream.Size = new System.Drawing.Size(263, 45);
            this.btnToggleStream.TabIndex = 3;
            this.btnToggleStream.Text = "START STREAM";
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnSnapshot.BorderThickness = 1;
            this.btnSnapshot.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSnapshot.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSnapshot.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSnapshot.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSnapshot.FillColor = System.Drawing.Color.Transparent;
            this.btnSnapshot.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnSnapshot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnSnapshot.Location = new System.Drawing.Point(12, 499);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(263, 45);
            this.btnSnapshot.TabIndex = 3;
            this.btnSnapshot.Text = "CAPTURE";
            // 
            // btnToggleBBox
            // 
            this.btnToggleBBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(85)))));
            this.btnToggleBBox.BorderThickness = 1;
            this.btnToggleBBox.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnToggleBBox.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnToggleBBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnToggleBBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnToggleBBox.FillColor = System.Drawing.Color.Transparent;
            this.btnToggleBBox.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnToggleBBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(85)))));
            this.btnToggleBBox.Location = new System.Drawing.Point(12, 560);
            this.btnToggleBBox.Name = "btnToggleBBox";
            this.btnToggleBBox.Size = new System.Drawing.Size(263, 45);
            this.btnToggleBBox.TabIndex = 3;
            this.btnToggleBBox.Text = "TOGGLE BBOX";
            // 
            // AIVisionUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.pnlAiData);
            this.Controls.Add(this.pnlCameraBorder);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Name = "AIVisionUC";
            this.Size = new System.Drawing.Size(1030, 680);
            this.Load += new System.EventHandler(this.AIVisionUC_Load);
            this.pnlCameraBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCameraStream)).EndInit();
            this.pnlAiData.ResumeLayout(false);
            this.pnlAiData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel pnlCameraBorder;
        private Guna.UI2.WinForms.Guna2PictureBox picCameraStream;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlAiData;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private System.Windows.Forms.ListBox listBox1;
        private Guna.UI2.WinForms.Guna2Button btnToggleBBox;
        private Guna.UI2.WinForms.Guna2Button btnSnapshot;
        private Guna.UI2.WinForms.Guna2Button btnToggleStream;
    }
}
