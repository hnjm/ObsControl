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
            this.tabPageWeather = new System.Windows.Forms.TabPage();
            this.tabPageAllsky = new System.Windows.Forms.TabPage();
            this.tabPageCameras = new System.Windows.Forms.TabPage();
            this.annunciatorPanel1 = new ASCOM.Controls.AnnunciatorPanel();
            this.annunciator1 = new ASCOM.Controls.Annunciator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPowerAll = new System.Windows.Forms.Button();
            this.ledIndicator2 = new ASCOM.Controls.LedIndicator();
            this.ledIndicator1 = new ASCOM.Controls.LedIndicator();
            this.ledFocuserPower = new ASCOM.Controls.LedIndicator();
            this.ledTelescopeNCameraPower = new ASCOM.Controls.LedIndicator();
            this.btnHeating = new System.Windows.Forms.Button();
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
            this.animateRoof = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.ledIndicator3 = new ASCOM.Controls.LedIndicator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStopRoof = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.annunciatorPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(345, 1);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(641, 609);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageControl
            // 
            this.tabPageControl.Location = new System.Drawing.Point(4, 25);
            this.tabPageControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageControl.Size = new System.Drawing.Size(633, 580);
            this.tabPageControl.TabIndex = 0;
            this.tabPageControl.Text = "Control";
            this.tabPageControl.UseVisualStyleBackColor = true;
            // 
            // tabPageWeather
            // 
            this.tabPageWeather.Location = new System.Drawing.Point(4, 25);
            this.tabPageWeather.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageWeather.Name = "tabPageWeather";
            this.tabPageWeather.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageWeather.Size = new System.Drawing.Size(633, 580);
            this.tabPageWeather.TabIndex = 1;
            this.tabPageWeather.Text = "Weather";
            this.tabPageWeather.UseVisualStyleBackColor = true;
            // 
            // tabPageAllsky
            // 
            this.tabPageAllsky.Location = new System.Drawing.Point(4, 25);
            this.tabPageAllsky.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageAllsky.Name = "tabPageAllsky";
            this.tabPageAllsky.Size = new System.Drawing.Size(633, 580);
            this.tabPageAllsky.TabIndex = 2;
            this.tabPageAllsky.Text = "AllSky";
            this.tabPageAllsky.UseVisualStyleBackColor = true;
            // 
            // tabPageCameras
            // 
            this.tabPageCameras.Location = new System.Drawing.Point(4, 25);
            this.tabPageCameras.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageCameras.Name = "tabPageCameras";
            this.tabPageCameras.Size = new System.Drawing.Size(633, 580);
            this.tabPageCameras.TabIndex = 3;
            this.tabPageCameras.Text = "Cameras";
            this.tabPageCameras.UseVisualStyleBackColor = true;
            // 
            // annunciatorPanel1
            // 
            this.annunciatorPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.annunciatorPanel1.Controls.Add(this.annunciator1);
            this.annunciatorPanel1.Location = new System.Drawing.Point(11, 165);
            this.annunciatorPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.annunciatorPanel1.Name = "annunciatorPanel1";
            this.annunciatorPanel1.Size = new System.Drawing.Size(297, 31);
            this.annunciatorPanel1.TabIndex = 5;
            this.annunciatorPanel1.Click += new System.EventHandler(this.annunciatorPanel1_Click);
            // 
            // annunciator1
            // 
            this.annunciator1.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.annunciator1.AutoSize = true;
            this.annunciator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.annunciator1.Cadence = ASCOM.Controls.CadencePattern.SteadyOff;
            this.annunciator1.Font = new System.Drawing.Font("Consolas", 10F);
            this.annunciator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.annunciator1.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.annunciator1.Location = new System.Drawing.Point(4, 0);
            this.annunciator1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.annunciator1.Mute = false;
            this.annunciator1.Name = "annunciator1";
            this.annunciator1.Size = new System.Drawing.Size(117, 20);
            this.annunciator1.TabIndex = 4;
            this.annunciator1.Text = "annunciator1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPowerAll);
            this.groupBox1.Controls.Add(this.ledIndicator2);
            this.groupBox1.Controls.Add(this.ledIndicator1);
            this.groupBox1.Controls.Add(this.ledFocuserPower);
            this.groupBox1.Controls.Add(this.ledTelescopeNCameraPower);
            this.groupBox1.Controls.Add(this.btnHeating);
            this.groupBox1.Controls.Add(this.btnRoofPower);
            this.groupBox1.Controls.Add(this.btnFocuserPower);
            this.groupBox1.Controls.Add(this.btnTelescopePower);
            this.groupBox1.Location = new System.Drawing.Point(7, 214);
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
            this.btnPowerAll.Location = new System.Drawing.Point(257, 22);
            this.btnPowerAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPowerAll.Name = "btnPowerAll";
            this.btnPowerAll.Size = new System.Drawing.Size(67, 115);
            this.btnPowerAll.TabIndex = 7;
            this.btnPowerAll.Text = "Power all";
            this.btnPowerAll.UseVisualStyleBackColor = true;
            // 
            // ledIndicator2
            // 
            this.ledIndicator2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ledIndicator2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledIndicator2.LabelText = "";
            this.ledIndicator2.Location = new System.Drawing.Point(65, 166);
            this.ledIndicator2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ledIndicator2.Name = "ledIndicator2";
            this.ledIndicator2.Size = new System.Drawing.Size(21, 15);
            this.ledIndicator2.Status = ASCOM.Controls.TrafficLight.Red;
            this.ledIndicator2.TabIndex = 5;
            // 
            // ledIndicator1
            // 
            this.ledIndicator1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ledIndicator1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledIndicator1.LabelText = "";
            this.ledIndicator1.Location = new System.Drawing.Point(65, 113);
            this.ledIndicator1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ledIndicator1.Name = "ledIndicator1";
            this.ledIndicator1.Size = new System.Drawing.Size(21, 15);
            this.ledIndicator1.Status = ASCOM.Controls.TrafficLight.Red;
            this.ledIndicator1.TabIndex = 5;
            // 
            // ledFocuserPower
            // 
            this.ledFocuserPower.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ledFocuserPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledFocuserPower.LabelText = "";
            this.ledFocuserPower.Location = new System.Drawing.Point(65, 73);
            this.ledFocuserPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ledFocuserPower.Name = "ledFocuserPower";
            this.ledFocuserPower.Size = new System.Drawing.Size(21, 15);
            this.ledFocuserPower.Status = ASCOM.Controls.TrafficLight.Red;
            this.ledFocuserPower.TabIndex = 5;
            // 
            // ledTelescopeNCameraPower
            // 
            this.ledTelescopeNCameraPower.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ledTelescopeNCameraPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledTelescopeNCameraPower.LabelText = "";
            this.ledTelescopeNCameraPower.Location = new System.Drawing.Point(65, 32);
            this.ledTelescopeNCameraPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ledTelescopeNCameraPower.Name = "ledTelescopeNCameraPower";
            this.ledTelescopeNCameraPower.Size = new System.Drawing.Size(21, 15);
            this.ledTelescopeNCameraPower.Status = ASCOM.Controls.TrafficLight.Red;
            this.ledTelescopeNCameraPower.TabIndex = 5;
            // 
            // btnHeating
            // 
            this.btnHeating.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnHeating.Location = new System.Drawing.Point(43, 156);
            this.btnHeating.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHeating.Name = "btnHeating";
            this.btnHeating.Size = new System.Drawing.Size(189, 34);
            this.btnHeating.TabIndex = 6;
            this.btnHeating.Text = "Heating";
            this.btnHeating.UseVisualStyleBackColor = false;
            // 
            // btnRoofPower
            // 
            this.btnRoofPower.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRoofPower.Location = new System.Drawing.Point(43, 103);
            this.btnRoofPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRoofPower.Name = "btnRoofPower";
            this.btnRoofPower.Size = new System.Drawing.Size(189, 34);
            this.btnRoofPower.TabIndex = 6;
            this.btnRoofPower.Text = "Roof power";
            this.btnRoofPower.UseVisualStyleBackColor = false;
            // 
            // btnFocuserPower
            // 
            this.btnFocuserPower.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnFocuserPower.Location = new System.Drawing.Point(43, 63);
            this.btnFocuserPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFocuserPower.Name = "btnFocuserPower";
            this.btnFocuserPower.Size = new System.Drawing.Size(189, 34);
            this.btnFocuserPower.TabIndex = 6;
            this.btnFocuserPower.Text = "Focuser";
            this.btnFocuserPower.UseVisualStyleBackColor = false;
            // 
            // btnTelescopePower
            // 
            this.btnTelescopePower.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTelescopePower.Location = new System.Drawing.Point(43, 22);
            this.btnTelescopePower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTelescopePower.Name = "btnTelescopePower";
            this.btnTelescopePower.Size = new System.Drawing.Size(189, 34);
            this.btnTelescopePower.TabIndex = 6;
            this.btnTelescopePower.Text = "Main power";
            this.btnTelescopePower.UseVisualStyleBackColor = false;
            this.btnTelescopePower.Click += new System.EventHandler(this.btnTelescopePower_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.Location = new System.Drawing.Point(56, 37);
            this.btnStartAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(176, 41);
            this.btnStartAll.TabIndex = 3;
            this.btnStartAll.Text = "Start All";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(989, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // animateRoof
            // 
            this.animateRoof.Interval = 1000;
            this.animateRoof.Tick += new System.EventHandler(this.animateRoof_Tick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ledIndicator3);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Location = new System.Drawing.Point(4, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 608);
            this.panel4.TabIndex = 2;
            // 
            // ledIndicator3
            // 
            this.ledIndicator3.LabelText = "LED";
            this.ledIndicator3.Location = new System.Drawing.Point(61, 421);
            this.ledIndicator3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ledIndicator3.Name = "ledIndicator3";
            this.ledIndicator3.Size = new System.Drawing.Size(75, 20);
            this.ledIndicator3.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.annunciatorPanel1);
            this.groupBox3.Controls.Add(this.btnStopRoof);
            this.groupBox3.Controls.Add(this.btnCloseRoof);
            this.groupBox3.Controls.Add(this.btnOpenRoof);
            this.groupBox3.Controls.Add(this.shapeContainer2);
            this.groupBox3.Location = new System.Drawing.Point(7, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(328, 203);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Roof";
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
            this.shapeContainer2.Size = new System.Drawing.Size(320, 180);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStartAll);
            this.groupBox2.Location = new System.Drawing.Point(7, 492);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(328, 110);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Software";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.groupBox4);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(633, 580);
            this.tabPageSettings.TabIndex = 4;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelDriverId);
            this.groupBox4.Controls.Add(this.buttonConnect);
            this.groupBox4.Controls.Add(this.buttonChoose);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(605, 68);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Switch";
            // 
            // labelDriverId
            // 
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDriverId.Location = new System.Drawing.Point(7, 18);
            this.labelDriverId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(317, 25);
            this.labelDriverId.TabIndex = 5;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonConnect
            // 
            this.buttonConnect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonConnect.Location = new System.Drawing.Point(436, 16);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(96, 28);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            // 
            // buttonChoose
            // 
            this.buttonChoose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonChoose.Location = new System.Drawing.Point(332, 16);
            this.buttonChoose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(96, 28);
            this.buttonChoose.TabIndex = 3;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 645);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Observatory Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.annunciatorPanel1.ResumeLayout(false);
            this.annunciatorPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.Timer animateRoof;
        private System.Windows.Forms.Button btnStartAll;
        private System.Windows.Forms.Button btnOpenRoof;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private System.Windows.Forms.Button btnStopRoof;
        private ASCOM.Controls.AnnunciatorPanel annunciatorPanel1;
        private ASCOM.Controls.Annunciator annunciator1;
        private System.Windows.Forms.TabPage tabPageCameras;
        private ASCOM.Controls.LedIndicator ledTelescopeNCameraPower;
        private System.Windows.Forms.Button btnTelescopePower;
        private ASCOM.Controls.LedIndicator ledIndicator1;
        private ASCOM.Controls.LedIndicator ledFocuserPower;
        private System.Windows.Forms.Button btnRoofPower;
        private System.Windows.Forms.Button btnFocuserPower;
        private System.Windows.Forms.Button btnPowerAll;
        private ASCOM.Controls.LedIndicator ledIndicator2;
        private System.Windows.Forms.Button btnHeating;
        private ASCOM.Controls.LedIndicator ledIndicator3;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonChoose;
    }
}

