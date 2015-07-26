namespace ObservatoryCenter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.AGaugeLabel aGaugeLabel1 = new System.Windows.Forms.AGaugeLabel();
            System.Windows.Forms.AGaugeLabel aGaugeLabel2 = new System.Windows.Forms.AGaugeLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageControl = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tabPageWeather = new System.Windows.Forms.TabPage();
            this.tabPageAllsky = new System.Windows.Forms.TabPage();
            this.tabPageCameras = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.btnSettings = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSet_Maxim_Camera2 = new System.Windows.Forms.TextBox();
            this.txtSet_Maxim_Camera1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSet_Telescope = new System.Windows.Forms.TextBox();
            this.txtSet_Dome = new System.Windows.Forms.TextBox();
            this.txtSet_Switch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.linkAstromania = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPowerAll = new System.Windows.Forms.Button();
            this.btnHeating = new System.Windows.Forms.Button();
            this.btnCameraPower = new System.Windows.Forms.Button();
            this.btnRoofPower = new System.Windows.Forms.Button();
            this.btnFocuserPower = new System.Windows.Forms.Button();
            this.btnTelescopePower = new System.Windows.Forms.Button();
            this.btnStartAll = new System.Windows.Forms.Button();
            this.btnOpenRoof = new System.Windows.Forms.Button();
            this.btnCloseRoof = new System.Windows.Forms.Button();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.rectRoof = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectBase = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_Switch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Dome = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Telescope = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Focuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Camera = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Connection = new System.Windows.Forms.ToolStripStatusLabel();
            this.animateRoofTimer = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxRoof = new System.Windows.Forms.GroupBox();
            this.btnStopRoof = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBeforeImaging = new System.Windows.Forms.Button();
            this.btnMaximStart = new System.Windows.Forms.Button();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker_SocketServer = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCameraSetPoint = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCameraTemp = new System.Windows.Forms.TextBox();
            this.txtCameraCoolerPower = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCameraStatus = new System.Windows.Forms.TextBox();
            this.txtCameraBinMode = new System.Windows.Forms.TextBox();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.txtCameraName = new System.Windows.Forms.TextBox();
            this.Guider = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtGuiderError = new System.Windows.Forms.RichTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtGuiderExposure = new System.Windows.Forms.TextBox();
            this.txtGuiderLastErrSt = new System.Windows.Forms.TextBox();
            this.txtGuider_AggY = new System.Windows.Forms.TextBox();
            this.txtGuider_AggX = new System.Windows.Forms.TextBox();
            this.btnGuider = new System.Windows.Forms.Button();
            this.logRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBoxTelescope = new System.Windows.Forms.GroupBox();
            this.btnConnectTelescope = new System.Windows.Forms.Button();
            this.btnTrack = new System.Windows.Forms.Button();
            this.btnPark = new System.Windows.Forms.Button();
            this.txtPierSide = new System.Windows.Forms.TextBox();
            this.txtTelescopeDec = new System.Windows.Forms.TextBox();
            this.txtTelescopeAlt = new System.Windows.Forms.TextBox();
            this.txtTelescopeRA = new System.Windows.Forms.TextBox();
            this.txtTelescopeAz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTelescopeH = new System.Windows.Forms.Panel();
            this.panelTelescopeV = new System.Windows.Forms.Panel();
            this.mainTimer2 = new System.Windows.Forms.Timer(this.components);
            this.aGauge1 = new System.Windows.Forms.AGauge();
            this.tabControl1.SuspendLayout();
            this.tabPageControl.SuspendLayout();
            this.tabPageWeather.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBoxRoof.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Guider.SuspendLayout();
            this.groupBoxTelescope.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageControl);
            this.tabControl1.Controls.Add(this.tabPageWeather);
            this.tabControl1.Controls.Add(this.tabPageAllsky);
            this.tabControl1.Controls.Add(this.tabPageCameras);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Location = new System.Drawing.Point(345, 297);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1011, 460);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageControl
            // 
            this.tabPageControl.Controls.Add(this.txtLog);
            this.tabPageControl.Location = new System.Drawing.Point(4, 25);
            this.tabPageControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageControl.Size = new System.Drawing.Size(1003, 431);
            this.tabPageControl.TabIndex = 0;
            this.tabPageControl.Text = "Log";
            this.tabPageControl.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Location = new System.Drawing.Point(3, 2);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(992, 421);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // tabPageWeather
            // 
            this.tabPageWeather.Controls.Add(this.aGauge1);
            this.tabPageWeather.Location = new System.Drawing.Point(4, 25);
            this.tabPageWeather.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageWeather.Name = "tabPageWeather";
            this.tabPageWeather.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageWeather.Size = new System.Drawing.Size(1003, 431);
            this.tabPageWeather.TabIndex = 1;
            this.tabPageWeather.Text = "Weather";
            this.tabPageWeather.UseVisualStyleBackColor = true;
            // 
            // tabPageAllsky
            // 
            this.tabPageAllsky.Location = new System.Drawing.Point(4, 25);
            this.tabPageAllsky.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageAllsky.Name = "tabPageAllsky";
            this.tabPageAllsky.Size = new System.Drawing.Size(1003, 431);
            this.tabPageAllsky.TabIndex = 2;
            this.tabPageAllsky.Text = "AllSky";
            this.tabPageAllsky.UseVisualStyleBackColor = true;
            // 
            // tabPageCameras
            // 
            this.tabPageCameras.Location = new System.Drawing.Point(4, 25);
            this.tabPageCameras.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageCameras.Name = "tabPageCameras";
            this.tabPageCameras.Size = new System.Drawing.Size(1003, 431);
            this.tabPageCameras.TabIndex = 3;
            this.tabPageCameras.Text = "Cameras";
            this.tabPageCameras.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.btnSettings);
            this.tabPageSettings.Controls.Add(this.groupBox6);
            this.tabPageSettings.Controls.Add(this.groupBox4);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(1003, 431);
            this.tabPageSettings.TabIndex = 4;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(479, 14);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(205, 76);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSet_Maxim_Camera2);
            this.groupBox6.Controls.Add(this.txtSet_Maxim_Camera1);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(8, 128);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Size = new System.Drawing.Size(445, 92);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Maxim";
            // 
            // txtSet_Maxim_Camera2
            // 
            this.txtSet_Maxim_Camera2.Location = new System.Drawing.Point(129, 52);
            this.txtSet_Maxim_Camera2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSet_Maxim_Camera2.Name = "txtSet_Maxim_Camera2";
            this.txtSet_Maxim_Camera2.ReadOnly = true;
            this.txtSet_Maxim_Camera2.Size = new System.Drawing.Size(296, 22);
            this.txtSet_Maxim_Camera2.TabIndex = 7;
            // 
            // txtSet_Maxim_Camera1
            // 
            this.txtSet_Maxim_Camera1.Location = new System.Drawing.Point(129, 22);
            this.txtSet_Maxim_Camera1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSet_Maxim_Camera1.Name = "txtSet_Maxim_Camera1";
            this.txtSet_Maxim_Camera1.ReadOnly = true;
            this.txtSet_Maxim_Camera1.Size = new System.Drawing.Size(296, 22);
            this.txtSet_Maxim_Camera1.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 25);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 17);
            this.label15.TabIndex = 6;
            this.label15.Text = "Camera1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 55);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 17);
            this.label16.TabIndex = 6;
            this.label16.Text = "Camera2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtSet_Telescope);
            this.groupBox4.Controls.Add(this.txtSet_Dome);
            this.groupBox4.Controls.Add(this.txtSet_Switch);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(8, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(445, 121);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ASCOM";
            // 
            // txtSet_Telescope
            // 
            this.txtSet_Telescope.Location = new System.Drawing.Point(129, 81);
            this.txtSet_Telescope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSet_Telescope.Name = "txtSet_Telescope";
            this.txtSet_Telescope.ReadOnly = true;
            this.txtSet_Telescope.Size = new System.Drawing.Size(296, 22);
            this.txtSet_Telescope.TabIndex = 7;
            // 
            // txtSet_Dome
            // 
            this.txtSet_Dome.Location = new System.Drawing.Point(129, 50);
            this.txtSet_Dome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSet_Dome.Name = "txtSet_Dome";
            this.txtSet_Dome.ReadOnly = true;
            this.txtSet_Dome.Size = new System.Drawing.Size(296, 22);
            this.txtSet_Dome.TabIndex = 7;
            // 
            // txtSet_Switch
            // 
            this.txtSet_Switch.Location = new System.Drawing.Point(129, 22);
            this.txtSet_Switch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSet_Switch.Name = "txtSet_Switch";
            this.txtSet_Switch.ReadOnly = true;
            this.txtSet_Switch.Size = new System.Drawing.Size(296, 22);
            this.txtSet_Switch.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Telescope";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 25);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 17);
            this.label13.TabIndex = 6;
            this.label13.Text = "Switch";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Dome";
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.label17);
            this.tabPageAbout.Controls.Add(this.linkAstromania);
            this.tabPageAbout.Controls.Add(this.lblVersion);
            this.tabPageAbout.Controls.Add(this.label14);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 25);
            this.tabPageAbout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(1003, 431);
            this.tabPageAbout.TabIndex = 5;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(4, 155);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(225, 17);
            this.label17.TabIndex = 7;
            this.label17.Text = "(C) 2014-2015 by Emchenko Boris";
            // 
            // linkAstromania
            // 
            this.linkAstromania.AutoSize = true;
            this.linkAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAstromania.Location = new System.Drawing.Point(4, 172);
            this.linkAstromania.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.Size = new System.Drawing.Size(136, 17);
            this.linkAstromania.TabIndex = 6;
            this.linkAstromania.TabStop = true;
            this.linkAstromania.Text = "www.astromania.info";
            this.linkAstromania.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAstromania_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(319, 47);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(303, 92);
            this.lblVersion.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(319, 10);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(469, 37);
            this.label14.TabIndex = 4;
            this.label14.Text = "Observatory Control automation software";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ObservatoryCenter.Properties.Resources.logo;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(4, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(303, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPowerAll);
            this.groupBox1.Controls.Add(this.btnHeating);
            this.groupBox1.Controls.Add(this.btnCameraPower);
            this.groupBox1.Controls.Add(this.btnRoofPower);
            this.groupBox1.Controls.Add(this.btnFocuserPower);
            this.groupBox1.Controls.Add(this.btnTelescopePower);
            this.groupBox1.Location = new System.Drawing.Point(7, 182);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(328, 198);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Power";
            // 
            // btnPowerAll
            // 
            this.btnPowerAll.Location = new System.Drawing.Point(9, 101);
            this.btnPowerAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerAll.Name = "btnPowerAll";
            this.btnPowerAll.Size = new System.Drawing.Size(315, 28);
            this.btnPowerAll.TabIndex = 7;
            this.btnPowerAll.Text = "Power all";
            this.btnPowerAll.UseVisualStyleBackColor = true;
            this.btnPowerAll.Click += new System.EventHandler(this.btnPowerAll_Click);
            // 
            // btnHeating
            // 
            this.btnHeating.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHeating.Location = new System.Drawing.Point(87, 134);
            this.btnHeating.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHeating.Name = "btnHeating";
            this.btnHeating.Size = new System.Drawing.Size(147, 28);
            this.btnHeating.TabIndex = 6;
            this.btnHeating.Text = "Heating";
            this.btnHeating.UseVisualStyleBackColor = false;
            // 
            // btnCameraPower
            // 
            this.btnCameraPower.BackColor = System.Drawing.Color.Tomato;
            this.btnCameraPower.Location = new System.Drawing.Point(173, 62);
            this.btnCameraPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCameraPower.Name = "btnCameraPower";
            this.btnCameraPower.Size = new System.Drawing.Size(147, 34);
            this.btnCameraPower.TabIndex = 6;
            this.btnCameraPower.Text = "Camera power";
            this.btnCameraPower.UseVisualStyleBackColor = false;
            this.btnCameraPower.Click += new System.EventHandler(this.btnCameraPower_Click);
            // 
            // btnRoofPower
            // 
            this.btnRoofPower.BackColor = System.Drawing.Color.Tomato;
            this.btnRoofPower.Location = new System.Drawing.Point(173, 22);
            this.btnRoofPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRoofPower.Name = "btnRoofPower";
            this.btnRoofPower.Size = new System.Drawing.Size(147, 34);
            this.btnRoofPower.TabIndex = 6;
            this.btnRoofPower.Text = "Roof power";
            this.btnRoofPower.UseVisualStyleBackColor = false;
            this.btnRoofPower.Click += new System.EventHandler(this.btnRoofPower_Click);
            // 
            // btnFocuserPower
            // 
            this.btnFocuserPower.BackColor = System.Drawing.Color.Tomato;
            this.btnFocuserPower.Location = new System.Drawing.Point(7, 62);
            this.btnFocuserPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFocuserPower.Name = "btnFocuserPower";
            this.btnFocuserPower.Size = new System.Drawing.Size(147, 34);
            this.btnFocuserPower.TabIndex = 6;
            this.btnFocuserPower.Text = "Focuser";
            this.btnFocuserPower.UseVisualStyleBackColor = false;
            this.btnFocuserPower.Click += new System.EventHandler(this.btnFocuserPower_Click);
            // 
            // btnTelescopePower
            // 
            this.btnTelescopePower.BackColor = System.Drawing.Color.Tomato;
            this.btnTelescopePower.Location = new System.Drawing.Point(7, 22);
            this.btnTelescopePower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTelescopePower.Name = "btnTelescopePower";
            this.btnTelescopePower.Size = new System.Drawing.Size(147, 34);
            this.btnTelescopePower.TabIndex = 6;
            this.btnTelescopePower.Text = "Mount power";
            this.btnTelescopePower.UseVisualStyleBackColor = false;
            this.btnTelescopePower.Click += new System.EventHandler(this.btnTelescopePower_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Location = new System.Drawing.Point(11, 23);
            this.btnStartAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(196, 59);
            this.btnStartAll.TabIndex = 3;
            this.btnStartAll.Text = "Prepare";
            this.toolTip1.SetToolTip(this.btnStartAll, resources.GetString("btnStartAll.ToolTip"));
            this.btnStartAll.UseVisualStyleBackColor = true;
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // btnOpenRoof
            // 
            this.btnOpenRoof.Enabled = false;
            this.btnOpenRoof.Location = new System.Drawing.Point(215, 129);
            this.btnOpenRoof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenRoof.Name = "btnOpenRoof";
            this.btnOpenRoof.Size = new System.Drawing.Size(93, 28);
            this.btnOpenRoof.TabIndex = 2;
            this.btnOpenRoof.Text = "Open roof";
            this.btnOpenRoof.UseVisualStyleBackColor = true;
            this.btnOpenRoof.Click += new System.EventHandler(this.btnOpenRoof_Click);
            // 
            // btnCloseRoof
            // 
            this.btnCloseRoof.Enabled = false;
            this.btnCloseRoof.Location = new System.Drawing.Point(11, 129);
            this.btnCloseRoof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCloseRoof.Name = "btnCloseRoof";
            this.btnCloseRoof.Size = new System.Drawing.Size(93, 30);
            this.btnCloseRoof.TabIndex = 1;
            this.btnCloseRoof.Text = "Close roof";
            this.btnCloseRoof.UseVisualStyleBackColor = true;
            this.btnCloseRoof.Click += new System.EventHandler(this.btnCloseRoof_Click);
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 171;
            this.lineShape2.X2 = 171;
            this.lineShape2.Y1 = 26;
            this.lineShape2.Y2 = 74;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 94;
            this.lineShape1.X2 = 170;
            this.lineShape1.Y1 = 26;
            this.lineShape1.Y2 = 26;
            // 
            // rectRoof
            // 
            this.rectRoof.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rectRoof.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectRoof.Location = new System.Drawing.Point(5, 3);
            this.rectRoof.Name = "recRoof";
            this.rectRoof.Size = new System.Drawing.Size(93, 23);
            // 
            // rectBase
            // 
            this.rectBase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rectBase.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectBase.Location = new System.Drawing.Point(9, 26);
            this.rectBase.Name = "rectBase";
            this.rectBase.Size = new System.Drawing.Size(85, 46);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_Switch,
            this.toolStripStatus_Dome,
            this.toolStripStatus_Telescope,
            this.toolStripStatus_Focuser,
            this.toolStripStatus_Camera,
            this.toolStripStatus_Connection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 768);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1357, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus_Switch
            // 
            this.toolStripStatus_Switch.DoubleClickEnabled = true;
            this.toolStripStatus_Switch.Name = "toolStripStatus_Switch";
            this.toolStripStatus_Switch.Size = new System.Drawing.Size(63, 20);
            this.toolStripStatus_Switch.Text = "SWITCH";
            this.toolStripStatus_Switch.DoubleClick += new System.EventHandler(this.toolStripStatus_Switch_DoubleClick);
            // 
            // toolStripStatus_Dome
            // 
            this.toolStripStatus_Dome.DoubleClickEnabled = true;
            this.toolStripStatus_Dome.Name = "toolStripStatus_Dome";
            this.toolStripStatus_Dome.Size = new System.Drawing.Size(52, 20);
            this.toolStripStatus_Dome.Text = "DOME";
            this.toolStripStatus_Dome.Click += new System.EventHandler(this.toolStripStatus_Dome_Click);
            // 
            // toolStripStatus_Telescope
            // 
            this.toolStripStatus_Telescope.Name = "toolStripStatus_Telescope";
            this.toolStripStatus_Telescope.Size = new System.Drawing.Size(84, 20);
            this.toolStripStatus_Telescope.Text = "TELESCOPE";
            this.toolStripStatus_Telescope.ToolTipText = "test";
            // 
            // toolStripStatus_Focuser
            // 
            this.toolStripStatus_Focuser.Name = "toolStripStatus_Focuser";
            this.toolStripStatus_Focuser.Size = new System.Drawing.Size(71, 20);
            this.toolStripStatus_Focuser.Text = "FOCUSER";
            // 
            // toolStripStatus_Camera
            // 
            this.toolStripStatus_Camera.Name = "toolStripStatus_Camera";
            this.toolStripStatus_Camera.Size = new System.Drawing.Size(68, 20);
            this.toolStripStatus_Camera.Text = "CAMERA";
            // 
            // toolStripStatus_Connection
            // 
            this.toolStripStatus_Connection.AutoSize = false;
            this.toolStripStatus_Connection.Name = "toolStripStatus_Connection";
            this.toolStripStatus_Connection.Size = new System.Drawing.Size(200, 20);
            this.toolStripStatus_Connection.Text = "CONNECTIONS: 0";
            // 
            // animateRoofTimer
            // 
            this.animateRoofTimer.Interval = 1000;
            this.animateRoofTimer.Tick += new System.EventHandler(this.animateRoof_Tick);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.Controls.Add(this.groupBoxRoof);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Location = new System.Drawing.Point(4, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 689);
            this.panel4.TabIndex = 2;
            // 
            // groupBoxRoof
            // 
            this.groupBoxRoof.Controls.Add(this.btnStopRoof);
            this.groupBoxRoof.Controls.Add(this.btnCloseRoof);
            this.groupBoxRoof.Controls.Add(this.btnOpenRoof);
            this.groupBoxRoof.Controls.Add(this.shapeContainer2);
            this.groupBoxRoof.Location = new System.Drawing.Point(7, 4);
            this.groupBoxRoof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRoof.Name = "groupBoxRoof";
            this.groupBoxRoof.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxRoof.Size = new System.Drawing.Size(328, 170);
            this.groupBoxRoof.TabIndex = 3;
            this.groupBoxRoof.TabStop = false;
            this.groupBoxRoof.Text = "Roof";
            // 
            // btnStopRoof
            // 
            this.btnStopRoof.Enabled = false;
            this.btnStopRoof.Location = new System.Drawing.Point(129, 129);
            this.btnStopRoof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStopRoof.Name = "btnStopRoof";
            this.btnStopRoof.Size = new System.Drawing.Size(61, 30);
            this.btnStopRoof.TabIndex = 1;
            this.btnStopRoof.Text = "Stop";
            this.btnStopRoof.UseVisualStyleBackColor = true;
            this.btnStopRoof.Click += new System.EventHandler(this.btnStopRoof_Click);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(4, 19);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1,
            this.rectRoof,
            this.rectBase});
            this.shapeContainer2.Size = new System.Drawing.Size(320, 147);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBeforeImaging);
            this.groupBox2.Controls.Add(this.btnMaximStart);
            this.groupBox2.Controls.Add(this.btnStartAll);
            this.groupBox2.Location = new System.Drawing.Point(7, 388);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(328, 262);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scenarios";
            // 
            // btnBeforeImaging
            // 
            this.btnBeforeImaging.Location = new System.Drawing.Point(215, 23);
            this.btnBeforeImaging.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBeforeImaging.Name = "btnBeforeImaging";
            this.btnBeforeImaging.Size = new System.Drawing.Size(105, 59);
            this.btnBeforeImaging.TabIndex = 3;
            this.btnBeforeImaging.Text = "Preapare Imaging";
            this.toolTip1.SetToolTip(this.btnBeforeImaging, "Предсъемочный цикл:\r\n1.\tОткрыть крышу\r\n2.\tПодвигать фокусером туда сюда (300 ед)\r" +
        "\n3.\tВыключить ИК подсветку камеры\r\n4.\tЗапустить CCDAP\r\n");
            this.btnBeforeImaging.UseVisualStyleBackColor = true;
            this.btnBeforeImaging.Click += new System.EventHandler(this.btnBeforeImaging_Click);
            // 
            // btnMaximStart
            // 
            this.btnMaximStart.Location = new System.Drawing.Point(11, 90);
            this.btnMaximStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMaximStart.Name = "btnMaximStart";
            this.btnMaximStart.Size = new System.Drawing.Size(113, 36);
            this.btnMaximStart.TabIndex = 3;
            this.btnMaximStart.Text = "Maxim start";
            this.btnMaximStart.UseVisualStyleBackColor = true;
            this.btnMaximStart.Click += new System.EventHandler(this.btnMaximStart_Click);
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 1000;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // backgroundWorker_SocketServer
            // 
            this.backgroundWorker_SocketServer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtCameraStatus);
            this.groupBox5.Controls.Add(this.txtCameraBinMode);
            this.groupBox5.Controls.Add(this.txtFilterName);
            this.groupBox5.Controls.Add(this.txtCameraName);
            this.groupBox5.Location = new System.Drawing.Point(347, 1);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(632, 170);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Camera";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtCameraSetPoint);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtCameraTemp);
            this.groupBox3.Controls.Add(this.txtCameraCoolerPower);
            this.groupBox3.Location = new System.Drawing.Point(477, 11);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(148, 105);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cooling";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Setpoint:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 74);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Power:";
            // 
            // txtCameraSetPoint
            // 
            this.txtCameraSetPoint.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraSetPoint.Location = new System.Drawing.Point(83, 21);
            this.txtCameraSetPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraSetPoint.Name = "txtCameraSetPoint";
            this.txtCameraSetPoint.ReadOnly = true;
            this.txtCameraSetPoint.Size = new System.Drawing.Size(45, 22);
            this.txtCameraSetPoint.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Temp:";
            // 
            // txtCameraTemp
            // 
            this.txtCameraTemp.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraTemp.Location = new System.Drawing.Point(83, 46);
            this.txtCameraTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraTemp.Name = "txtCameraTemp";
            this.txtCameraTemp.ReadOnly = true;
            this.txtCameraTemp.Size = new System.Drawing.Size(45, 22);
            this.txtCameraTemp.TabIndex = 0;
            // 
            // txtCameraCoolerPower
            // 
            this.txtCameraCoolerPower.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraCoolerPower.Location = new System.Drawing.Point(83, 70);
            this.txtCameraCoolerPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraCoolerPower.Name = "txtCameraCoolerPower";
            this.txtCameraCoolerPower.ReadOnly = true;
            this.txtCameraCoolerPower.Size = new System.Drawing.Size(45, 22);
            this.txtCameraCoolerPower.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(183, 50);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Bin";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 50);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Filter";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "Camera:";
            // 
            // txtCameraStatus
            // 
            this.txtCameraStatus.Location = new System.Drawing.Point(7, 80);
            this.txtCameraStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraStatus.Name = "txtCameraStatus";
            this.txtCameraStatus.Size = new System.Drawing.Size(327, 22);
            this.txtCameraStatus.TabIndex = 1;
            // 
            // txtCameraBinMode
            // 
            this.txtCameraBinMode.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraBinMode.Location = new System.Drawing.Point(219, 47);
            this.txtCameraBinMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraBinMode.Name = "txtCameraBinMode";
            this.txtCameraBinMode.ReadOnly = true;
            this.txtCameraBinMode.Size = new System.Drawing.Size(48, 22);
            this.txtCameraBinMode.TabIndex = 0;
            // 
            // txtFilterName
            // 
            this.txtFilterName.BackColor = System.Drawing.Color.Tomato;
            this.txtFilterName.Location = new System.Drawing.Point(75, 47);
            this.txtFilterName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.ReadOnly = true;
            this.txtFilterName.Size = new System.Drawing.Size(100, 22);
            this.txtFilterName.TabIndex = 0;
            // 
            // txtCameraName
            // 
            this.txtCameraName.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraName.Location = new System.Drawing.Point(75, 16);
            this.txtCameraName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCameraName.Name = "txtCameraName";
            this.txtCameraName.ReadOnly = true;
            this.txtCameraName.Size = new System.Drawing.Size(192, 22);
            this.txtCameraName.TabIndex = 0;
            // 
            // Guider
            // 
            this.Guider.Controls.Add(this.label20);
            this.Guider.Controls.Add(this.txtGuiderError);
            this.Guider.Controls.Add(this.label19);
            this.Guider.Controls.Add(this.label18);
            this.Guider.Controls.Add(this.txtGuiderExposure);
            this.Guider.Controls.Add(this.txtGuiderLastErrSt);
            this.Guider.Controls.Add(this.txtGuider_AggY);
            this.Guider.Controls.Add(this.txtGuider_AggX);
            this.Guider.Controls.Add(this.btnGuider);
            this.Guider.Location = new System.Drawing.Point(351, 177);
            this.Guider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Guider.Name = "Guider";
            this.Guider.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Guider.Size = new System.Drawing.Size(628, 102);
            this.Guider.TabIndex = 4;
            this.Guider.TabStop = false;
            this.Guider.Text = "Guider";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(188, 30);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 17);
            this.label20.TabIndex = 4;
            this.label20.Text = "s";
            // 
            // txtGuiderError
            // 
            this.txtGuiderError.Location = new System.Drawing.Point(485, 11);
            this.txtGuiderError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGuiderError.Name = "txtGuiderError";
            this.txtGuiderError.Size = new System.Drawing.Size(135, 83);
            this.txtGuiderError.TabIndex = 3;
            this.txtGuiderError.Text = "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(100, 65);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 17);
            this.label19.TabIndex = 2;
            this.label19.Text = "Agg Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 65);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 17);
            this.label18.TabIndex = 2;
            this.label18.Text = "Agg X";
            // 
            // txtGuiderExposure
            // 
            this.txtGuiderExposure.Location = new System.Drawing.Point(143, 26);
            this.txtGuiderExposure.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuiderExposure.Name = "txtGuiderExposure";
            this.txtGuiderExposure.Size = new System.Drawing.Size(41, 22);
            this.txtGuiderExposure.TabIndex = 1;
            // 
            // txtGuiderLastErrSt
            // 
            this.txtGuiderLastErrSt.Location = new System.Drawing.Point(252, 26);
            this.txtGuiderLastErrSt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuiderLastErrSt.Name = "txtGuiderLastErrSt";
            this.txtGuiderLastErrSt.Size = new System.Drawing.Size(179, 22);
            this.txtGuiderLastErrSt.TabIndex = 1;
            // 
            // txtGuider_AggY
            // 
            this.txtGuider_AggY.Location = new System.Drawing.Point(155, 62);
            this.txtGuider_AggY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuider_AggY.Name = "txtGuider_AggY";
            this.txtGuider_AggY.Size = new System.Drawing.Size(29, 22);
            this.txtGuider_AggY.TabIndex = 1;
            // 
            // txtGuider_AggX
            // 
            this.txtGuider_AggX.Location = new System.Drawing.Point(63, 62);
            this.txtGuider_AggX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuider_AggX.Name = "txtGuider_AggX";
            this.txtGuider_AggX.Size = new System.Drawing.Size(29, 22);
            this.txtGuider_AggX.TabIndex = 1;
            // 
            // btnGuider
            // 
            this.btnGuider.Location = new System.Drawing.Point(11, 23);
            this.btnGuider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuider.Name = "btnGuider";
            this.btnGuider.Size = new System.Drawing.Size(125, 28);
            this.btnGuider.TabIndex = 0;
            this.btnGuider.Text = "Guider";
            this.btnGuider.UseVisualStyleBackColor = true;
            this.btnGuider.Click += new System.EventHandler(this.btnGuider_Click);
            // 
            // logRefreshTimer
            // 
            this.logRefreshTimer.Enabled = true;
            this.logRefreshTimer.Interval = 500;
            this.logRefreshTimer.Tick += new System.EventHandler(this.logRefreshTimer_Tick);
            // 
            // groupBoxTelescope
            // 
            this.groupBoxTelescope.Controls.Add(this.btnConnectTelescope);
            this.groupBoxTelescope.Controls.Add(this.btnTrack);
            this.groupBoxTelescope.Controls.Add(this.btnPark);
            this.groupBoxTelescope.Controls.Add(this.txtPierSide);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeDec);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeAlt);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeRA);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeAz);
            this.groupBoxTelescope.Controls.Add(this.label4);
            this.groupBoxTelescope.Controls.Add(this.label3);
            this.groupBoxTelescope.Controls.Add(this.label2);
            this.groupBoxTelescope.Controls.Add(this.label1);
            this.groupBoxTelescope.Controls.Add(this.panelTelescopeH);
            this.groupBoxTelescope.Controls.Add(this.panelTelescopeV);
            this.groupBoxTelescope.Location = new System.Drawing.Point(985, 1);
            this.groupBoxTelescope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxTelescope.Name = "groupBoxTelescope";
            this.groupBoxTelescope.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxTelescope.Size = new System.Drawing.Size(365, 278);
            this.groupBoxTelescope.TabIndex = 3;
            this.groupBoxTelescope.TabStop = false;
            this.groupBoxTelescope.Text = "Telescope";
            // 
            // btnConnectTelescope
            // 
            this.btnConnectTelescope.Location = new System.Drawing.Point(19, 206);
            this.btnConnectTelescope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnectTelescope.Name = "btnConnectTelescope";
            this.btnConnectTelescope.Size = new System.Drawing.Size(135, 31);
            this.btnConnectTelescope.TabIndex = 7;
            this.btnConnectTelescope.Text = "Connect";
            this.btnConnectTelescope.UseVisualStyleBackColor = true;
            this.btnConnectTelescope.Click += new System.EventHandler(this.btnConnectTelescope_Click);
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(88, 144);
            this.btnTrack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(65, 30);
            this.btnTrack.TabIndex = 7;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = true;
            // 
            // btnPark
            // 
            this.btnPark.Location = new System.Drawing.Point(19, 144);
            this.btnPark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPark.Name = "btnPark";
            this.btnPark.Size = new System.Drawing.Size(65, 30);
            this.btnPark.TabIndex = 7;
            this.btnPark.Text = "Park";
            this.btnPark.UseVisualStyleBackColor = true;
            // 
            // txtPierSide
            // 
            this.txtPierSide.Location = new System.Drawing.Point(19, 178);
            this.txtPierSide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPierSide.Name = "txtPierSide";
            this.txtPierSide.Size = new System.Drawing.Size(135, 22);
            this.txtPierSide.TabIndex = 6;
            // 
            // txtTelescopeDec
            // 
            this.txtTelescopeDec.Location = new System.Drawing.Point(53, 116);
            this.txtTelescopeDec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTelescopeDec.Name = "txtTelescopeDec";
            this.txtTelescopeDec.Size = new System.Drawing.Size(100, 22);
            this.txtTelescopeDec.TabIndex = 6;
            // 
            // txtTelescopeAlt
            // 
            this.txtTelescopeAlt.Location = new System.Drawing.Point(53, 54);
            this.txtTelescopeAlt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTelescopeAlt.Name = "txtTelescopeAlt";
            this.txtTelescopeAlt.Size = new System.Drawing.Size(100, 22);
            this.txtTelescopeAlt.TabIndex = 6;
            // 
            // txtTelescopeRA
            // 
            this.txtTelescopeRA.Location = new System.Drawing.Point(53, 90);
            this.txtTelescopeRA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTelescopeRA.Name = "txtTelescopeRA";
            this.txtTelescopeRA.Size = new System.Drawing.Size(100, 22);
            this.txtTelescopeRA.TabIndex = 6;
            // 
            // txtTelescopeAz
            // 
            this.txtTelescopeAz.Location = new System.Drawing.Point(53, 28);
            this.txtTelescopeAz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTelescopeAz.Name = "txtTelescopeAz";
            this.txtTelescopeAz.Size = new System.Drawing.Size(100, 22);
            this.txtTelescopeAz.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Dec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "RA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Az";
            // 
            // panelTelescopeH
            // 
            this.panelTelescopeH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTelescopeH.Location = new System.Drawing.Point(171, 158);
            this.panelTelescopeH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTelescopeH.Name = "panelTelescopeH";
            this.panelTelescopeH.Size = new System.Drawing.Size(181, 80);
            this.panelTelescopeH.TabIndex = 4;
            this.panelTelescopeH.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTelescopeH_Paint);
            // 
            // panelTelescopeV
            // 
            this.panelTelescopeV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTelescopeV.Location = new System.Drawing.Point(171, 11);
            this.panelTelescopeV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTelescopeV.Name = "panelTelescopeV";
            this.panelTelescopeV.Size = new System.Drawing.Size(178, 120);
            this.panelTelescopeV.TabIndex = 4;
            this.panelTelescopeV.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTelescopeV_Paint);
            // 
            // mainTimer2
            // 
            this.mainTimer2.Enabled = true;
            this.mainTimer2.Interval = 5000;
            this.mainTimer2.Tick += new System.EventHandler(this.mainTimer2_Tick);
            // 
            // aGauge1
            // 
            this.aGauge1.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge1.BaseArcRadius = 33;
            this.aGauge1.BaseArcStart = 135;
            this.aGauge1.BaseArcSweep = 270;
            this.aGauge1.BaseArcWidth = 2;
            this.aGauge1.Center = new System.Drawing.Point(100, 100);
            aGaugeLabel1.Color = System.Drawing.SystemColors.WindowText;
            aGaugeLabel1.Name = "GaugeLabel1";
            aGaugeLabel1.Position = new System.Drawing.Point(0, 0);
            aGaugeLabel1.Text = "222";
            aGaugeLabel2.Color = System.Drawing.SystemColors.WindowText;
            aGaugeLabel2.Name = "GaugeLabel2";
            aGaugeLabel2.Position = new System.Drawing.Point(0, 0);
            aGaugeLabel2.Text = "333";
            this.aGauge1.GaugeLabels.Add(aGaugeLabel1);
            this.aGauge1.GaugeLabels.Add(aGaugeLabel2);
            this.aGauge1.Location = new System.Drawing.Point(399, 125);
            this.aGauge1.MaxValue = 400F;
            this.aGauge1.MinValue = -100F;
            this.aGauge1.Name = "aGauge1";
            this.aGauge1.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge1.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge1.NeedleRadius = 80;
            this.aGauge1.NeedleType = System.Windows.Forms.NeedleType.Simple;
            this.aGauge1.NeedleWidth = 2;
            this.aGauge1.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge1.ScaleLinesInterInnerRadius = 73;
            this.aGauge1.ScaleLinesInterOuterRadius = 80;
            this.aGauge1.ScaleLinesInterWidth = 1;
            this.aGauge1.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge1.ScaleLinesMajorInnerRadius = 70;
            this.aGauge1.ScaleLinesMajorOuterRadius = 80;
            this.aGauge1.ScaleLinesMajorStepValue = 50F;
            this.aGauge1.ScaleLinesMajorWidth = 2;
            this.aGauge1.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.aGauge1.ScaleLinesMinorInnerRadius = 75;
            this.aGauge1.ScaleLinesMinorOuterRadius = 80;
            this.aGauge1.ScaleLinesMinorTicks = 9;
            this.aGauge1.ScaleLinesMinorWidth = 1;
            this.aGauge1.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge1.ScaleNumbersFormat = null;
            this.aGauge1.ScaleNumbersRadius = 95;
            this.aGauge1.ScaleNumbersRotation = 0;
            this.aGauge1.ScaleNumbersStartScaleLine = 0;
            this.aGauge1.ScaleNumbersStepScaleLines = 1;
            this.aGauge1.Size = new System.Drawing.Size(205, 180);
            this.aGauge1.TabIndex = 5;
            this.aGauge1.Text = "aGauge1";
            this.aGauge1.Value = 333F;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 793);
            this.Controls.Add(this.Guider);
            this.Controls.Add(this.groupBoxTelescope);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1373, 829);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Observatory Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageControl.ResumeLayout(false);
            this.tabPageWeather.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBoxRoof.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Guider.ResumeLayout(false);
            this.Guider.PerformLayout();
            this.groupBoxTelescope.ResumeLayout(false);
            this.groupBoxTelescope.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageControl;
        private System.Windows.Forms.TabPage tabPageWeather;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabPageAllsky;
        private System.Windows.Forms.Button btnCloseRoof;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectRoof;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectBase;
        private System.Windows.Forms.Timer animateRoofTimer;
        private System.Windows.Forms.Button btnStartAll;
        private System.Windows.Forms.Button btnOpenRoof;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBoxRoof;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private System.Windows.Forms.Button btnStopRoof;
        private System.Windows.Forms.TabPage tabPageCameras;
        private System.Windows.Forms.Button btnTelescopePower;
        private System.Windows.Forms.Button btnRoofPower;
        private System.Windows.Forms.Button btnFocuserPower;
        private System.Windows.Forms.Button btnPowerAll;
        private System.Windows.Forms.Button btnHeating;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Switch;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Dome;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Telescope;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Focuser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Camera;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Button btnCameraPower;
        private System.Windows.Forms.Button btnSettings;
        private System.ComponentModel.BackgroundWorker backgroundWorker_SocketServer;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Connection;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBeforeImaging;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Timer logRefreshTimer;
        private System.Windows.Forms.TextBox txtCameraSetPoint;
        private System.Windows.Forms.TextBox txtCameraName;
        private System.Windows.Forms.TextBox txtCameraStatus;
        private System.Windows.Forms.TextBox txtCameraTemp;
        private System.Windows.Forms.TextBox txtCameraCoolerPower;
        private System.Windows.Forms.GroupBox groupBoxTelescope;
        private System.Windows.Forms.Panel panelTelescopeV;
        private System.Windows.Forms.Panel panelTelescopeH;
        private System.Windows.Forms.Button btnPark;
        private System.Windows.Forms.TextBox txtTelescopeDec;
        private System.Windows.Forms.TextBox txtTelescopeAlt;
        private System.Windows.Forms.TextBox txtTelescopeRA;
        private System.Windows.Forms.TextBox txtTelescopeAz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnectTelescope;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.TextBox txtPierSide;
        private System.Windows.Forms.Button btnMaximStart;
        private System.Windows.Forms.GroupBox Guider;
        private System.Windows.Forms.Button btnGuider;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCameraBinMode;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtSet_Maxim_Camera2;
        private System.Windows.Forms.TextBox txtSet_Maxim_Camera1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSet_Telescope;
        private System.Windows.Forms.TextBox txtSet_Dome;
        private System.Windows.Forms.TextBox txtSet_Switch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer mainTimer2;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.LinkLabel linkAstromania;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtGuider_AggY;
        private System.Windows.Forms.TextBox txtGuider_AggX;
        private System.Windows.Forms.RichTextBox txtGuiderError;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtGuiderExposure;
        private System.Windows.Forms.TextBox txtGuiderLastErrSt;
        private System.Windows.Forms.AGauge aGauge1;
    }
}

