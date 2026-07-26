namespace AutonomousControlStation
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pnlSol = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.btnVision = new Guna.UI2.WinForms.Guna2Button();
            this.btnTelemetry = new Guna.UI2.WinForms.Guna2Button();
            this.btnSystemLogs = new Guna.UI2.WinForms.Guna2Button();
            this.btnControlPanel = new Guna.UI2.WinForms.Guna2Button();
            this.pnlUst = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlOrta = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.pnlSol.SuspendLayout();
            this.pnlUst.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(982, 3);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(931, 3);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox2.TabIndex = 0;
            // 
            // pnlSol
            // 
            this.pnlSol.Controls.Add(this.btnControlPanel);
            this.pnlSol.Controls.Add(this.btnSystemLogs);
            this.pnlSol.Controls.Add(this.btnTelemetry);
            this.pnlSol.Controls.Add(this.btnVision);
            this.pnlSol.Controls.Add(this.guna2Button1);
            this.pnlSol.Controls.Add(this.btnDashboard);
            this.pnlSol.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSol.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.pnlSol.Location = new System.Drawing.Point(0, 0);
            this.pnlSol.Name = "pnlSol";
            this.pnlSol.Size = new System.Drawing.Size(250, 720);
            this.pnlSol.TabIndex = 1;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BorderRadius = 10;
            this.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDashboard.FillColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(141)))), ((int)(((byte)(155)))));
            this.btnDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.btnDashboard.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.ImageSize = new System.Drawing.Size(40, 40);
            this.btnDashboard.Location = new System.Drawing.Point(3, 114);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(241, 45);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "DASHBOARD";
            this.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnVision
            // 
            this.btnVision.BorderRadius = 10;
            this.btnVision.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVision.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVision.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVision.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVision.FillColor = System.Drawing.Color.Transparent;
            this.btnVision.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnVision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(141)))), ((int)(((byte)(155)))));
            this.btnVision.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.btnVision.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnVision.Image = ((System.Drawing.Image)(resources.GetObject("btnVision.Image")));
            this.btnVision.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnVision.ImageSize = new System.Drawing.Size(40, 40);
            this.btnVision.Location = new System.Drawing.Point(3, 165);
            this.btnVision.Name = "btnVision";
            this.btnVision.Size = new System.Drawing.Size(241, 45);
            this.btnVision.TabIndex = 0;
            this.btnVision.Text = "AI VISION";
            this.btnVision.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnVision.Click += new System.EventHandler(this.btnVision_Click);
            // 
            // btnTelemetry
            // 
            this.btnTelemetry.BorderRadius = 10;
            this.btnTelemetry.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTelemetry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTelemetry.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTelemetry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTelemetry.FillColor = System.Drawing.Color.Transparent;
            this.btnTelemetry.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnTelemetry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(141)))), ((int)(((byte)(155)))));
            this.btnTelemetry.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.btnTelemetry.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnTelemetry.Image = ((System.Drawing.Image)(resources.GetObject("btnTelemetry.Image")));
            this.btnTelemetry.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTelemetry.ImageSize = new System.Drawing.Size(40, 40);
            this.btnTelemetry.Location = new System.Drawing.Point(3, 216);
            this.btnTelemetry.Name = "btnTelemetry";
            this.btnTelemetry.Size = new System.Drawing.Size(241, 45);
            this.btnTelemetry.TabIndex = 0;
            this.btnTelemetry.Text = "TELEMETRY";
            this.btnTelemetry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTelemetry.Click += new System.EventHandler(this.btnTelemetry_Click);
            // 
            // btnSystemLogs
            // 
            this.btnSystemLogs.BorderRadius = 10;
            this.btnSystemLogs.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSystemLogs.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSystemLogs.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSystemLogs.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSystemLogs.FillColor = System.Drawing.Color.Transparent;
            this.btnSystemLogs.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnSystemLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(141)))), ((int)(((byte)(155)))));
            this.btnSystemLogs.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.btnSystemLogs.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnSystemLogs.Image = ((System.Drawing.Image)(resources.GetObject("btnSystemLogs.Image")));
            this.btnSystemLogs.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSystemLogs.ImageSize = new System.Drawing.Size(40, 40);
            this.btnSystemLogs.Location = new System.Drawing.Point(3, 267);
            this.btnSystemLogs.Name = "btnSystemLogs";
            this.btnSystemLogs.Size = new System.Drawing.Size(241, 45);
            this.btnSystemLogs.TabIndex = 0;
            this.btnSystemLogs.Text = "SYSTEM LOGS";
            this.btnSystemLogs.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSystemLogs.Click += new System.EventHandler(this.btnSystemLogs_Click);
            // 
            // btnControlPanel
            // 
            this.btnControlPanel.BorderRadius = 10;
            this.btnControlPanel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnControlPanel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnControlPanel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnControlPanel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnControlPanel.FillColor = System.Drawing.Color.Transparent;
            this.btnControlPanel.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnControlPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(141)))), ((int)(((byte)(155)))));
            this.btnControlPanel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.btnControlPanel.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnControlPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnControlPanel.Image")));
            this.btnControlPanel.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnControlPanel.ImageSize = new System.Drawing.Size(40, 40);
            this.btnControlPanel.Location = new System.Drawing.Point(3, 318);
            this.btnControlPanel.Name = "btnControlPanel";
            this.btnControlPanel.Size = new System.Drawing.Size(241, 45);
            this.btnControlPanel.TabIndex = 0;
            this.btnControlPanel.Text = "CONTROL PANEL";
            this.btnControlPanel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnControlPanel.Click += new System.EventHandler(this.btnControlPanel_Click);
            // 
            // pnlUst
            // 
            this.pnlUst.Controls.Add(this.guna2ControlBox2);
            this.pnlUst.Controls.Add(this.guna2ControlBox1);
            this.pnlUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUst.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.pnlUst.Location = new System.Drawing.Point(250, 0);
            this.pnlUst.Name = "pnlUst";
            this.pnlUst.Size = new System.Drawing.Size(1030, 40);
            this.pnlUst.TabIndex = 2;
            // 
            // pnlOrta
            // 
            this.pnlOrta.BackColor = System.Drawing.Color.Transparent;
            this.pnlOrta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrta.Location = new System.Drawing.Point(250, 40);
            this.pnlOrta.Name = "pnlOrta";
            this.pnlOrta.Size = new System.Drawing.Size(1030, 680);
            this.pnlOrta.TabIndex = 3;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.guna2Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(43)))));
            this.guna2Button1.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.ImageSize = new System.Drawing.Size(60, 60);
            this.guna2Button1.Location = new System.Drawing.Point(3, 3);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(241, 94);
            this.guna2Button1.TabIndex = 0;
            this.guna2Button1.Text = "BALKAR GCS";
            this.guna2Button1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.pnlUst;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlOrta);
            this.Controls.Add(this.pnlUst);
            this.Controls.Add(this.pnlSol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSol.ResumeLayout(false);
            this.pnlUst.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2Panel pnlSol;
        private Guna.UI2.WinForms.Guna2Button btnControlPanel;
        private Guna.UI2.WinForms.Guna2Button btnSystemLogs;
        private Guna.UI2.WinForms.Guna2Button btnTelemetry;
        private Guna.UI2.WinForms.Guna2Button btnVision;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Panel pnlUst;
        private Guna.UI2.WinForms.Guna2Panel pnlOrta;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}

