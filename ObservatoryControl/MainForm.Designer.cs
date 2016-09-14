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
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_Switch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Dome = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Telescope = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Focuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Camera = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Connection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownLogLevel = new System.Windows.Forms.ToolStripDropDownButton();
            this.animateRoofTimer = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxRoof = new System.Windows.Forms.GroupBox();
            this.btnStopRoof = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkPHDBroker = new System.Windows.Forms.LinkLabel();
            this.linkTelescopeTempControl = new System.Windows.Forms.LinkLabel();
            this.linkTest = new System.Windows.Forms.LinkLabel();
            this.linkWeatherStation = new System.Windows.Forms.LinkLabel();
            this.linkFocusMax = new System.Windows.Forms.LinkLabel();
            this.linkCdC = new System.Windows.Forms.LinkLabel();
            this.linkPHD2 = new System.Windows.Forms.LinkLabel();
            this.linkCCDAP = new System.Windows.Forms.LinkLabel();
            this.linkMaximDL = new System.Windows.Forms.LinkLabel();
            this.btnBeforeImaging = new System.Windows.Forms.Button();
            this.btnMaximStart = new System.Windows.Forms.Button();
            this.mainTimer_short = new System.Windows.Forms.Timer(this.components);
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
            this.mainTimer_Long = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker_runprograms = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPageControl.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusBar.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(259, 241);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 374);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageControl
            // 
            this.tabPageControl.Controls.Add(this.txtLog);
            this.tabPageControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageControl.Size = new System.Drawing.Size(750, 348);
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
            this.txtLog.Location = new System.Drawing.Point(2, 2);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(745, 343);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // tabPageWeather
            // 
            this.tabPageWeather.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeather.Name = "tabPageWeather";
            this.tabPageWeather.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeather.Size = new System.Drawing.Size(750, 348);
            this.tabPageWeather.TabIndex = 1;
            this.tabPageWeather.Text = "Weather";
            this.tabPageWeather.UseVisualStyleBackColor = true;
            // 
            // tabPageAllsky
            // 
            this.tabPageAllsky.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllsky.Name = "tabPageAllsky";
            this.tabPageAllsky.Size = new System.Drawing.Size(750, 348);
            this.tabPageAllsky.TabIndex = 2;
            this.tabPageAllsky.Text = "AllSky";
            this.tabPageAllsky.UseVisualStyleBackColor = true;
            // 
            // tabPageCameras
            // 
            this.tabPageCameras.Location = new System.Drawing.Point(4, 22);
            this.tabPageCameras.Name = "tabPageCameras";
            this.tabPageCameras.Size = new System.Drawing.Size(750, 348);
            this.tabPageCameras.TabIndex = 3;
            this.tabPageCameras.Text = "Cameras";
            this.tabPageCameras.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.btnSettings);
            this.tabPageSettings.Controls.Add(this.groupBox6);
            this.tabPageSettings.Controls.Add(this.groupBox4);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(750, 348);
            this.tabPageSettings.TabIndex = 4;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(359, 11);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(154, 62);
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
            this.groupBox6.Location = new System.Drawing.Point(6, 104);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(334, 75);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Maxim";
            // 
            // txtSet_Maxim_Camera2
            // 
            this.txtSet_Maxim_Camera2.Location = new System.Drawing.Point(97, 42);
            this.txtSet_Maxim_Camera2.Margin = new System.Windows.Forms.Padding(2);
            this.txtSet_Maxim_Camera2.Name = "txtSet_Maxim_Camera2";
            this.txtSet_Maxim_Camera2.ReadOnly = true;
            this.txtSet_Maxim_Camera2.Size = new System.Drawing.Size(223, 20);
            this.txtSet_Maxim_Camera2.TabIndex = 7;
            // 
            // txtSet_Maxim_Camera1
            // 
            this.txtSet_Maxim_Camera1.Location = new System.Drawing.Point(97, 18);
            this.txtSet_Maxim_Camera1.Margin = new System.Windows.Forms.Padding(2);
            this.txtSet_Maxim_Camera1.Name = "txtSet_Maxim_Camera1";
            this.txtSet_Maxim_Camera1.ReadOnly = true;
            this.txtSet_Maxim_Camera1.Size = new System.Drawing.Size(223, 20);
            this.txtSet_Maxim_Camera1.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Camera1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
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
            this.groupBox4.Location = new System.Drawing.Point(6, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(334, 98);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ASCOM";
            // 
            // txtSet_Telescope
            // 
            this.txtSet_Telescope.Location = new System.Drawing.Point(97, 66);
            this.txtSet_Telescope.Margin = new System.Windows.Forms.Padding(2);
            this.txtSet_Telescope.Name = "txtSet_Telescope";
            this.txtSet_Telescope.ReadOnly = true;
            this.txtSet_Telescope.Size = new System.Drawing.Size(223, 20);
            this.txtSet_Telescope.TabIndex = 7;
            // 
            // txtSet_Dome
            // 
            this.txtSet_Dome.Location = new System.Drawing.Point(97, 41);
            this.txtSet_Dome.Margin = new System.Windows.Forms.Padding(2);
            this.txtSet_Dome.Name = "txtSet_Dome";
            this.txtSet_Dome.ReadOnly = true;
            this.txtSet_Dome.Size = new System.Drawing.Size(223, 20);
            this.txtSet_Dome.TabIndex = 7;
            // 
            // txtSet_Switch
            // 
            this.txtSet_Switch.Location = new System.Drawing.Point(97, 18);
            this.txtSet_Switch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSet_Switch.Name = "txtSet_Switch";
            this.txtSet_Switch.ReadOnly = true;
            this.txtSet_Switch.Size = new System.Drawing.Size(223, 20);
            this.txtSet_Switch.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Telescope";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Switch";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
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
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(750, 348);
            this.tabPageAbout.TabIndex = 5;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(3, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(168, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "(C) 2014-2015 by Emchenko Boris";
            // 
            // linkAstromania
            // 
            this.linkAstromania.AutoSize = true;
            this.linkAstromania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkAstromania.Location = new System.Drawing.Point(3, 140);
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.Size = new System.Drawing.Size(105, 13);
            this.linkAstromania.TabIndex = 6;
            this.linkAstromania.TabStop = true;
            this.linkAstromania.Text = "www.astromania.info";
            this.linkAstromania.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAstromania_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(239, 38);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(227, 75);
            this.lblVersion.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(239, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(352, 30);
            this.label14.TabIndex = 4;
            this.label14.Text = "Observatory Control automation software";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ObservatoryCenter.Properties.Resources.logo;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 112);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Power";
            // 
            // btnPowerAll
            // 
            this.btnPowerAll.Location = new System.Drawing.Point(7, 82);
            this.btnPowerAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnPowerAll.Name = "btnPowerAll";
            this.btnPowerAll.Size = new System.Drawing.Size(236, 23);
            this.btnPowerAll.TabIndex = 7;
            this.btnPowerAll.Text = "Power all";
            this.btnPowerAll.UseVisualStyleBackColor = true;
            this.btnPowerAll.Click += new System.EventHandler(this.btnPowerAll_Click);
            // 
            // btnHeating
            // 
            this.btnHeating.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHeating.Location = new System.Drawing.Point(65, 109);
            this.btnHeating.Margin = new System.Windows.Forms.Padding(2);
            this.btnHeating.Name = "btnHeating";
            this.btnHeating.Size = new System.Drawing.Size(110, 23);
            this.btnHeating.TabIndex = 6;
            this.btnHeating.Text = "Heating";
            this.btnHeating.UseVisualStyleBackColor = false;
            // 
            // btnCameraPower
            // 
            this.btnCameraPower.BackColor = System.Drawing.Color.Tomato;
            this.btnCameraPower.Location = new System.Drawing.Point(130, 50);
            this.btnCameraPower.Margin = new System.Windows.Forms.Padding(2);
            this.btnCameraPower.Name = "btnCameraPower";
            this.btnCameraPower.Size = new System.Drawing.Size(110, 28);
            this.btnCameraPower.TabIndex = 6;
            this.btnCameraPower.Text = "Camera power";
            this.btnCameraPower.UseVisualStyleBackColor = false;
            this.btnCameraPower.Click += new System.EventHandler(this.btnCameraPower_Click);
            // 
            // btnRoofPower
            // 
            this.btnRoofPower.BackColor = System.Drawing.Color.Tomato;
            this.btnRoofPower.Location = new System.Drawing.Point(130, 18);
            this.btnRoofPower.Margin = new System.Windows.Forms.Padding(2);
            this.btnRoofPower.Name = "btnRoofPower";
            this.btnRoofPower.Size = new System.Drawing.Size(110, 28);
            this.btnRoofPower.TabIndex = 6;
            this.btnRoofPower.Text = "Roof power";
            this.btnRoofPower.UseVisualStyleBackColor = false;
            this.btnRoofPower.Click += new System.EventHandler(this.btnRoofPower_Click);
            // 
            // btnFocuserPower
            // 
            this.btnFocuserPower.BackColor = System.Drawing.Color.Tomato;
            this.btnFocuserPower.Location = new System.Drawing.Point(5, 50);
            this.btnFocuserPower.Margin = new System.Windows.Forms.Padding(2);
            this.btnFocuserPower.Name = "btnFocuserPower";
            this.btnFocuserPower.Size = new System.Drawing.Size(110, 28);
            this.btnFocuserPower.TabIndex = 6;
            this.btnFocuserPower.Text = "Focuser";
            this.btnFocuserPower.UseVisualStyleBackColor = false;
            this.btnFocuserPower.Click += new System.EventHandler(this.btnFocuserPower_Click);
            // 
            // btnTelescopePower
            // 
            this.btnTelescopePower.BackColor = System.Drawing.Color.Tomato;
            this.btnTelescopePower.Location = new System.Drawing.Point(5, 18);
            this.btnTelescopePower.Margin = new System.Windows.Forms.Padding(2);
            this.btnTelescopePower.Name = "btnTelescopePower";
            this.btnTelescopePower.Size = new System.Drawing.Size(110, 28);
            this.btnTelescopePower.TabIndex = 6;
            this.btnTelescopePower.Text = "Mount power";
            this.btnTelescopePower.UseVisualStyleBackColor = false;
            this.btnTelescopePower.Click += new System.EventHandler(this.btnTelescopePower_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Location = new System.Drawing.Point(8, 19);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(147, 48);
            this.btnStartAll.TabIndex = 3;
            this.btnStartAll.Text = "Prepare";
            this.toolTip1.SetToolTip(this.btnStartAll, resources.GetString("btnStartAll.ToolTip"));
            this.btnStartAll.UseVisualStyleBackColor = true;
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // btnOpenRoof
            // 
            this.btnOpenRoof.Enabled = false;
            this.btnOpenRoof.Location = new System.Drawing.Point(161, 105);
            this.btnOpenRoof.Name = "btnOpenRoof";
            this.btnOpenRoof.Size = new System.Drawing.Size(70, 23);
            this.btnOpenRoof.TabIndex = 2;
            this.btnOpenRoof.Text = "Open roof";
            this.btnOpenRoof.UseVisualStyleBackColor = true;
            this.btnOpenRoof.Click += new System.EventHandler(this.btnOpenRoof_Click);
            // 
            // btnCloseRoof
            // 
            this.btnCloseRoof.Enabled = false;
            this.btnCloseRoof.Location = new System.Drawing.Point(8, 105);
            this.btnCloseRoof.Name = "btnCloseRoof";
            this.btnCloseRoof.Size = new System.Drawing.Size(70, 24);
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
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_Switch,
            this.toolStripStatus_Dome,
            this.toolStripStatus_Telescope,
            this.toolStripStatus_Focuser,
            this.toolStripStatus_Camera,
            this.toolStripStatus_Connection,
            this.toolStripDropDownLogLevel});
            this.statusBar.Location = new System.Drawing.Point(0, 619);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(1018, 25);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusBar";
            // 
            // toolStripStatus_Switch
            // 
            this.toolStripStatus_Switch.DoubleClickEnabled = true;
            this.toolStripStatus_Switch.Name = "toolStripStatus_Switch";
            this.toolStripStatus_Switch.Size = new System.Drawing.Size(51, 20);
            this.toolStripStatus_Switch.Text = "SWITCH";
            this.toolStripStatus_Switch.DoubleClick += new System.EventHandler(this.toolStripStatus_Switch_DoubleClick);
            // 
            // toolStripStatus_Dome
            // 
            this.toolStripStatus_Dome.DoubleClickEnabled = true;
            this.toolStripStatus_Dome.Name = "toolStripStatus_Dome";
            this.toolStripStatus_Dome.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatus_Dome.Text = "DOME";
            this.toolStripStatus_Dome.Click += new System.EventHandler(this.toolStripStatus_Dome_Click);
            // 
            // toolStripStatus_Telescope
            // 
            this.toolStripStatus_Telescope.Name = "toolStripStatus_Telescope";
            this.toolStripStatus_Telescope.Size = new System.Drawing.Size(68, 20);
            this.toolStripStatus_Telescope.Text = "TELESCOPE";
            this.toolStripStatus_Telescope.ToolTipText = "test";
            // 
            // toolStripStatus_Focuser
            // 
            this.toolStripStatus_Focuser.Name = "toolStripStatus_Focuser";
            this.toolStripStatus_Focuser.Size = new System.Drawing.Size(57, 20);
            this.toolStripStatus_Focuser.Text = "FOCUSER";
            // 
            // toolStripStatus_Camera
            // 
            this.toolStripStatus_Camera.Name = "toolStripStatus_Camera";
            this.toolStripStatus_Camera.Size = new System.Drawing.Size(55, 20);
            this.toolStripStatus_Camera.Text = "CAMERA";
            // 
            // toolStripStatus_Connection
            // 
            this.toolStripStatus_Connection.AutoSize = false;
            this.toolStripStatus_Connection.Name = "toolStripStatus_Connection";
            this.toolStripStatus_Connection.Size = new System.Drawing.Size(200, 20);
            this.toolStripStatus_Connection.Text = "CONNECTIONS: 0";
            // 
            // toolStripDropDownLogLevel
            // 
            this.toolStripDropDownLogLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownLogLevel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownLogLevel.Image")));
            this.toolStripDropDownLogLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownLogLevel.Name = "toolStripDropDownLogLevel";
            this.toolStripDropDownLogLevel.Size = new System.Drawing.Size(169, 23);
            this.toolStripDropDownLogLevel.Text = "toolStripDropDownLogLevel";
            this.toolStripDropDownLogLevel.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDropDownLogLevel_DropDownItemClicked);
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
            this.panel4.Location = new System.Drawing.Point(3, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(256, 560);
            this.panel4.TabIndex = 2;
            // 
            // groupBoxRoof
            // 
            this.groupBoxRoof.Controls.Add(this.btnStopRoof);
            this.groupBoxRoof.Controls.Add(this.btnCloseRoof);
            this.groupBoxRoof.Controls.Add(this.btnOpenRoof);
            this.groupBoxRoof.Controls.Add(this.shapeContainer2);
            this.groupBoxRoof.Location = new System.Drawing.Point(5, 3);
            this.groupBoxRoof.Name = "groupBoxRoof";
            this.groupBoxRoof.Size = new System.Drawing.Size(246, 138);
            this.groupBoxRoof.TabIndex = 3;
            this.groupBoxRoof.TabStop = false;
            this.groupBoxRoof.Text = "Roof";
            // 
            // btnStopRoof
            // 
            this.btnStopRoof.Enabled = false;
            this.btnStopRoof.Location = new System.Drawing.Point(97, 105);
            this.btnStopRoof.Name = "btnStopRoof";
            this.btnStopRoof.Size = new System.Drawing.Size(46, 24);
            this.btnStopRoof.TabIndex = 1;
            this.btnStopRoof.Text = "Stop";
            this.btnStopRoof.UseVisualStyleBackColor = true;
            this.btnStopRoof.Click += new System.EventHandler(this.btnStopRoof_Click);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1,
            this.rectRoof,
            this.rectBase});
            this.shapeContainer2.Size = new System.Drawing.Size(240, 119);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkPHDBroker);
            this.groupBox2.Controls.Add(this.linkTelescopeTempControl);
            this.groupBox2.Controls.Add(this.linkTest);
            this.groupBox2.Controls.Add(this.linkWeatherStation);
            this.groupBox2.Controls.Add(this.linkFocusMax);
            this.groupBox2.Controls.Add(this.linkCdC);
            this.groupBox2.Controls.Add(this.linkPHD2);
            this.groupBox2.Controls.Add(this.linkCCDAP);
            this.groupBox2.Controls.Add(this.linkMaximDL);
            this.groupBox2.Controls.Add(this.btnBeforeImaging);
            this.groupBox2.Controls.Add(this.btnMaximStart);
            this.groupBox2.Controls.Add(this.btnStartAll);
            this.groupBox2.Location = new System.Drawing.Point(5, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 213);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scenarios";
            // 
            // linkPHDBroker
            // 
            this.linkPHDBroker.AutoSize = true;
            this.linkPHDBroker.LinkColor = System.Drawing.Color.Blue;
            this.linkPHDBroker.Location = new System.Drawing.Point(54, 140);
            this.linkPHDBroker.Name = "linkPHDBroker";
            this.linkPHDBroker.Size = new System.Drawing.Size(61, 13);
            this.linkPHDBroker.TabIndex = 4;
            this.linkPHDBroker.TabStop = true;
            this.linkPHDBroker.Text = "PHDBroker";
            this.linkPHDBroker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPHDBroker_LinkClicked);
            // 
            // linkTelescopeTempControl
            // 
            this.linkTelescopeTempControl.AutoSize = true;
            this.linkTelescopeTempControl.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.linkTelescopeTempControl.Location = new System.Drawing.Point(93, 164);
            this.linkTelescopeTempControl.Name = "linkTelescopeTempControl";
            this.linkTelescopeTempControl.Size = new System.Drawing.Size(117, 13);
            this.linkTelescopeTempControl.TabIndex = 4;
            this.linkTelescopeTempControl.TabStop = true;
            this.linkTelescopeTempControl.Text = "TelescopeTempControl";
            // 
            // linkTest
            // 
            this.linkTest.AutoSize = true;
            this.linkTest.Location = new System.Drawing.Point(6, 186);
            this.linkTest.Name = "linkTest";
            this.linkTest.Size = new System.Drawing.Size(28, 13);
            this.linkTest.TabIndex = 4;
            this.linkTest.TabStop = true;
            this.linkTest.Text = "Test";
            this.linkTest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTest_LinkClicked);
            // 
            // linkWeatherStation
            // 
            this.linkWeatherStation.AutoSize = true;
            this.linkWeatherStation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.linkWeatherStation.Location = new System.Drawing.Point(6, 164);
            this.linkWeatherStation.Name = "linkWeatherStation";
            this.linkWeatherStation.Size = new System.Drawing.Size(81, 13);
            this.linkWeatherStation.TabIndex = 4;
            this.linkWeatherStation.TabStop = true;
            this.linkWeatherStation.Text = "WeatherStation";
            // 
            // linkFocusMax
            // 
            this.linkFocusMax.AutoSize = true;
            this.linkFocusMax.LinkColor = System.Drawing.Color.Blue;
            this.linkFocusMax.Location = new System.Drawing.Point(121, 140);
            this.linkFocusMax.Name = "linkFocusMax";
            this.linkFocusMax.Size = new System.Drawing.Size(56, 13);
            this.linkFocusMax.TabIndex = 4;
            this.linkFocusMax.TabStop = true;
            this.linkFocusMax.Text = "FocusMax";
            // 
            // linkCdC
            // 
            this.linkCdC.AutoSize = true;
            this.linkCdC.Location = new System.Drawing.Point(107, 117);
            this.linkCdC.Name = "linkCdC";
            this.linkCdC.Size = new System.Drawing.Size(27, 13);
            this.linkCdC.TabIndex = 4;
            this.linkCdC.TabStop = true;
            this.linkCdC.Text = "CdC";
            this.linkCdC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCdC_LinkClicked);
            // 
            // linkPHD2
            // 
            this.linkPHD2.AutoSize = true;
            this.linkPHD2.Location = new System.Drawing.Point(63, 117);
            this.linkPHD2.Name = "linkPHD2";
            this.linkPHD2.Size = new System.Drawing.Size(36, 13);
            this.linkPHD2.TabIndex = 4;
            this.linkPHD2.TabStop = true;
            this.linkPHD2.Text = "PHD2";
            this.linkPHD2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPHD2_LinkClicked);
            // 
            // linkCCDAP
            // 
            this.linkCCDAP.AutoSize = true;
            this.linkCCDAP.LinkColor = System.Drawing.Color.Blue;
            this.linkCCDAP.Location = new System.Drawing.Point(6, 140);
            this.linkCCDAP.Name = "linkCCDAP";
            this.linkCCDAP.Size = new System.Drawing.Size(43, 13);
            this.linkCCDAP.TabIndex = 4;
            this.linkCCDAP.TabStop = true;
            this.linkCCDAP.Text = "CCDAP";
            this.linkCCDAP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCCDAP_LinkClicked);
            // 
            // linkMaximDL
            // 
            this.linkMaximDL.AutoSize = true;
            this.linkMaximDL.Location = new System.Drawing.Point(6, 117);
            this.linkMaximDL.Name = "linkMaximDL";
            this.linkMaximDL.Size = new System.Drawing.Size(51, 13);
            this.linkMaximDL.TabIndex = 4;
            this.linkMaximDL.TabStop = true;
            this.linkMaximDL.Text = "MaximDL";
            this.linkMaximDL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMaximDL_LinkClicked);
            // 
            // btnBeforeImaging
            // 
            this.btnBeforeImaging.Location = new System.Drawing.Point(161, 19);
            this.btnBeforeImaging.Name = "btnBeforeImaging";
            this.btnBeforeImaging.Size = new System.Drawing.Size(79, 48);
            this.btnBeforeImaging.TabIndex = 3;
            this.btnBeforeImaging.Text = "Preapare Imaging";
            this.toolTip1.SetToolTip(this.btnBeforeImaging, "Предсъемочный цикл:\r\n1.\tОткрыть крышу\r\n2.\tПодвигать фокусером туда сюда (300 ед)\r" +
        "\n3.\tВыключить ИК подсветку камеры\r\n4.\tЗапустить CCDAP\r\n");
            this.btnBeforeImaging.UseVisualStyleBackColor = true;
            this.btnBeforeImaging.Click += new System.EventHandler(this.btnBeforeImaging_Click);
            // 
            // btnMaximStart
            // 
            this.btnMaximStart.Location = new System.Drawing.Point(8, 73);
            this.btnMaximStart.Name = "btnMaximStart";
            this.btnMaximStart.Size = new System.Drawing.Size(85, 29);
            this.btnMaximStart.TabIndex = 3;
            this.btnMaximStart.Text = "Maxim start";
            this.btnMaximStart.UseVisualStyleBackColor = true;
            this.btnMaximStart.Click += new System.EventHandler(this.btnMaximStart_Click);
            // 
            // mainTimer_short
            // 
            this.mainTimer_short.Interval = 1000;
            this.mainTimer_short.Tick += new System.EventHandler(this.mainTimerShort_Tick);
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
            this.groupBox5.Location = new System.Drawing.Point(260, 1);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(474, 138);
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
            this.groupBox3.Location = new System.Drawing.Point(358, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(111, 85);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cooling";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Setpoint:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Power:";
            // 
            // txtCameraSetPoint
            // 
            this.txtCameraSetPoint.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraSetPoint.Location = new System.Drawing.Point(62, 17);
            this.txtCameraSetPoint.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraSetPoint.Name = "txtCameraSetPoint";
            this.txtCameraSetPoint.ReadOnly = true;
            this.txtCameraSetPoint.Size = new System.Drawing.Size(35, 20);
            this.txtCameraSetPoint.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Temp:";
            // 
            // txtCameraTemp
            // 
            this.txtCameraTemp.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraTemp.Location = new System.Drawing.Point(62, 37);
            this.txtCameraTemp.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraTemp.Name = "txtCameraTemp";
            this.txtCameraTemp.ReadOnly = true;
            this.txtCameraTemp.Size = new System.Drawing.Size(35, 20);
            this.txtCameraTemp.TabIndex = 0;
            // 
            // txtCameraCoolerPower
            // 
            this.txtCameraCoolerPower.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraCoolerPower.Location = new System.Drawing.Point(62, 57);
            this.txtCameraCoolerPower.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraCoolerPower.Name = "txtCameraCoolerPower";
            this.txtCameraCoolerPower.ReadOnly = true;
            this.txtCameraCoolerPower.Size = new System.Drawing.Size(35, 20);
            this.txtCameraCoolerPower.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(137, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Bin";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Filter";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Camera:";
            // 
            // txtCameraStatus
            // 
            this.txtCameraStatus.Location = new System.Drawing.Point(5, 65);
            this.txtCameraStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraStatus.Name = "txtCameraStatus";
            this.txtCameraStatus.Size = new System.Drawing.Size(246, 20);
            this.txtCameraStatus.TabIndex = 1;
            // 
            // txtCameraBinMode
            // 
            this.txtCameraBinMode.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraBinMode.Location = new System.Drawing.Point(164, 38);
            this.txtCameraBinMode.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraBinMode.Name = "txtCameraBinMode";
            this.txtCameraBinMode.ReadOnly = true;
            this.txtCameraBinMode.Size = new System.Drawing.Size(37, 20);
            this.txtCameraBinMode.TabIndex = 0;
            // 
            // txtFilterName
            // 
            this.txtFilterName.BackColor = System.Drawing.Color.Tomato;
            this.txtFilterName.Location = new System.Drawing.Point(56, 38);
            this.txtFilterName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.ReadOnly = true;
            this.txtFilterName.Size = new System.Drawing.Size(76, 20);
            this.txtFilterName.TabIndex = 0;
            // 
            // txtCameraName
            // 
            this.txtCameraName.BackColor = System.Drawing.Color.Tomato;
            this.txtCameraName.Location = new System.Drawing.Point(56, 13);
            this.txtCameraName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCameraName.Name = "txtCameraName";
            this.txtCameraName.ReadOnly = true;
            this.txtCameraName.Size = new System.Drawing.Size(145, 20);
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
            this.Guider.Location = new System.Drawing.Point(263, 144);
            this.Guider.Name = "Guider";
            this.Guider.Size = new System.Drawing.Size(471, 83);
            this.Guider.TabIndex = 4;
            this.Guider.TabStop = false;
            this.Guider.Text = "Guider";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(141, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(12, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "s";
            // 
            // txtGuiderError
            // 
            this.txtGuiderError.Location = new System.Drawing.Point(364, 9);
            this.txtGuiderError.Name = "txtGuiderError";
            this.txtGuiderError.Size = new System.Drawing.Size(102, 68);
            this.txtGuiderError.TabIndex = 3;
            this.txtGuiderError.Text = "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(75, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Agg Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 53);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Agg X";
            // 
            // txtGuiderExposure
            // 
            this.txtGuiderExposure.Location = new System.Drawing.Point(107, 21);
            this.txtGuiderExposure.Margin = new System.Windows.Forms.Padding(2);
            this.txtGuiderExposure.Name = "txtGuiderExposure";
            this.txtGuiderExposure.Size = new System.Drawing.Size(32, 20);
            this.txtGuiderExposure.TabIndex = 1;
            // 
            // txtGuiderLastErrSt
            // 
            this.txtGuiderLastErrSt.Location = new System.Drawing.Point(189, 21);
            this.txtGuiderLastErrSt.Margin = new System.Windows.Forms.Padding(2);
            this.txtGuiderLastErrSt.Name = "txtGuiderLastErrSt";
            this.txtGuiderLastErrSt.Size = new System.Drawing.Size(135, 20);
            this.txtGuiderLastErrSt.TabIndex = 1;
            // 
            // txtGuider_AggY
            // 
            this.txtGuider_AggY.Location = new System.Drawing.Point(116, 50);
            this.txtGuider_AggY.Margin = new System.Windows.Forms.Padding(2);
            this.txtGuider_AggY.Name = "txtGuider_AggY";
            this.txtGuider_AggY.Size = new System.Drawing.Size(23, 20);
            this.txtGuider_AggY.TabIndex = 1;
            // 
            // txtGuider_AggX
            // 
            this.txtGuider_AggX.Location = new System.Drawing.Point(47, 50);
            this.txtGuider_AggX.Margin = new System.Windows.Forms.Padding(2);
            this.txtGuider_AggX.Name = "txtGuider_AggX";
            this.txtGuider_AggX.Size = new System.Drawing.Size(23, 20);
            this.txtGuider_AggX.TabIndex = 1;
            // 
            // btnGuider
            // 
            this.btnGuider.Location = new System.Drawing.Point(8, 19);
            this.btnGuider.Name = "btnGuider";
            this.btnGuider.Size = new System.Drawing.Size(94, 23);
            this.btnGuider.TabIndex = 0;
            this.btnGuider.Text = "Guider";
            this.btnGuider.UseVisualStyleBackColor = true;
            this.btnGuider.Click += new System.EventHandler(this.btnGuider_Click);
            // 
            // logRefreshTimer
            // 
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
            this.groupBoxTelescope.Location = new System.Drawing.Point(739, 1);
            this.groupBoxTelescope.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxTelescope.Name = "groupBoxTelescope";
            this.groupBoxTelescope.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxTelescope.Size = new System.Drawing.Size(274, 226);
            this.groupBoxTelescope.TabIndex = 3;
            this.groupBoxTelescope.TabStop = false;
            this.groupBoxTelescope.Text = "Telescope";
            // 
            // btnConnectTelescope
            // 
            this.btnConnectTelescope.Location = new System.Drawing.Point(14, 167);
            this.btnConnectTelescope.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnectTelescope.Name = "btnConnectTelescope";
            this.btnConnectTelescope.Size = new System.Drawing.Size(101, 25);
            this.btnConnectTelescope.TabIndex = 7;
            this.btnConnectTelescope.Text = "Connect";
            this.btnConnectTelescope.UseVisualStyleBackColor = true;
            this.btnConnectTelescope.Click += new System.EventHandler(this.btnConnectTelescope_Click);
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(66, 117);
            this.btnTrack.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(49, 24);
            this.btnTrack.TabIndex = 7;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = true;
            // 
            // btnPark
            // 
            this.btnPark.Location = new System.Drawing.Point(14, 117);
            this.btnPark.Margin = new System.Windows.Forms.Padding(2);
            this.btnPark.Name = "btnPark";
            this.btnPark.Size = new System.Drawing.Size(49, 24);
            this.btnPark.TabIndex = 7;
            this.btnPark.Text = "Park";
            this.btnPark.UseVisualStyleBackColor = true;
            // 
            // txtPierSide
            // 
            this.txtPierSide.Location = new System.Drawing.Point(14, 145);
            this.txtPierSide.Margin = new System.Windows.Forms.Padding(2);
            this.txtPierSide.Name = "txtPierSide";
            this.txtPierSide.Size = new System.Drawing.Size(102, 20);
            this.txtPierSide.TabIndex = 6;
            // 
            // txtTelescopeDec
            // 
            this.txtTelescopeDec.Location = new System.Drawing.Point(40, 94);
            this.txtTelescopeDec.Margin = new System.Windows.Forms.Padding(2);
            this.txtTelescopeDec.Name = "txtTelescopeDec";
            this.txtTelescopeDec.Size = new System.Drawing.Size(76, 20);
            this.txtTelescopeDec.TabIndex = 6;
            // 
            // txtTelescopeAlt
            // 
            this.txtTelescopeAlt.Location = new System.Drawing.Point(40, 44);
            this.txtTelescopeAlt.Margin = new System.Windows.Forms.Padding(2);
            this.txtTelescopeAlt.Name = "txtTelescopeAlt";
            this.txtTelescopeAlt.Size = new System.Drawing.Size(76, 20);
            this.txtTelescopeAlt.TabIndex = 6;
            // 
            // txtTelescopeRA
            // 
            this.txtTelescopeRA.Location = new System.Drawing.Point(40, 73);
            this.txtTelescopeRA.Margin = new System.Windows.Forms.Padding(2);
            this.txtTelescopeRA.Name = "txtTelescopeRA";
            this.txtTelescopeRA.Size = new System.Drawing.Size(76, 20);
            this.txtTelescopeRA.TabIndex = 6;
            // 
            // txtTelescopeAz
            // 
            this.txtTelescopeAz.Location = new System.Drawing.Point(40, 23);
            this.txtTelescopeAz.Margin = new System.Windows.Forms.Padding(2);
            this.txtTelescopeAz.Name = "txtTelescopeAz";
            this.txtTelescopeAz.Size = new System.Drawing.Size(76, 20);
            this.txtTelescopeAz.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Dec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "RA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Az";
            // 
            // panelTelescopeH
            // 
            this.panelTelescopeH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTelescopeH.Location = new System.Drawing.Point(128, 128);
            this.panelTelescopeH.Margin = new System.Windows.Forms.Padding(2);
            this.panelTelescopeH.Name = "panelTelescopeH";
            this.panelTelescopeH.Size = new System.Drawing.Size(136, 65);
            this.panelTelescopeH.TabIndex = 4;
            this.panelTelescopeH.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTelescopeH_Paint);
            // 
            // panelTelescopeV
            // 
            this.panelTelescopeV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTelescopeV.Location = new System.Drawing.Point(128, 9);
            this.panelTelescopeV.Margin = new System.Windows.Forms.Padding(2);
            this.panelTelescopeV.Name = "panelTelescopeV";
            this.panelTelescopeV.Size = new System.Drawing.Size(134, 98);
            this.panelTelescopeV.TabIndex = 4;
            this.panelTelescopeV.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTelescopeV_Paint);
            // 
            // mainTimer_Long
            // 
            this.mainTimer_Long.Interval = 5000;
            this.mainTimer_Long.Tick += new System.EventHandler(this.mainTimer_Long_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 644);
            this.Controls.Add(this.Guider);
            this.Controls.Add(this.groupBoxTelescope);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1034, 682);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Observatory Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageControl.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBoxRoof.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusBar;
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
        private System.Windows.Forms.Timer mainTimer_short;
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
        private System.Windows.Forms.Timer mainTimer_Long;
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
        private System.Windows.Forms.LinkLabel linkPHDBroker;
        private System.Windows.Forms.LinkLabel linkTelescopeTempControl;
        private System.Windows.Forms.LinkLabel linkWeatherStation;
        private System.Windows.Forms.LinkLabel linkFocusMax;
        private System.Windows.Forms.LinkLabel linkCdC;
        private System.Windows.Forms.LinkLabel linkPHD2;
        private System.Windows.Forms.LinkLabel linkCCDAP;
        private System.Windows.Forms.LinkLabel linkMaximDL;
        private System.Windows.Forms.LinkLabel linkTest;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownLogLevel;
        private System.ComponentModel.BackgroundWorker backgroundWorker_runprograms;
    }
}

