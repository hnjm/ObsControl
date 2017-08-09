namespace WeatherControl
{
    partial class FormWeatherFileControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWeatherFileControl));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTimerControl = new System.Windows.Forms.Button();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.btnWriteNow = new System.Windows.Forms.Button();
            this.timerBolwoodWrite = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSinceLastRain = new System.Windows.Forms.TextBox();
            this.txtSinceLastWet = new System.Windows.Forms.TextBox();
            this.chkWetNow = new System.Windows.Forms.CheckBox();
            this.chkRainNow = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.comboBoxWetFlag = new System.Windows.Forms.ComboBox();
            this.comboBoxAlertFlag = new System.Windows.Forms.ComboBox();
            this.comboBoxRoofCloseFlag = new System.Windows.Forms.ComboBox();
            this.comboBoxDaylightCond = new System.Windows.Forms.ComboBox();
            this.comboBoxRainCond = new System.Windows.Forms.ComboBox();
            this.comboBoxWindCond = new System.Windows.Forms.ComboBox();
            this.comboBoxCloudCond = new System.Windows.Forms.ComboBox();
            this.comboBoxRainFlag = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVBANow = new System.Windows.Forms.TextBox();
            this.txtSecondsSinceWrite = new System.Windows.Forms.TextBox();
            this.txtHeating = new System.Windows.Forms.TextBox();
            this.txtDewPoint = new System.Windows.Forms.TextBox();
            this.txtHumidity = new System.Windows.Forms.TextBox();
            this.txtWindSpeed = new System.Windows.Forms.TextBox();
            this.txtSensorTemp = new System.Windows.Forms.TextBox();
            this.txtAmbTemp = new System.Windows.Forms.TextBox();
            this.txtSkyTemp = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.chkUseSmartLogic = new System.Windows.Forms.CheckBox();
            this.btnFileDialog = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBoxDecimalSeparator = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtLastWritten = new System.Windows.Forms.TextBox();
            this.chkSaveConditions = new System.Windows.Forms.CheckBox();
            this.chkGoodConditions = new System.Windows.Forms.CheckBox();
            this.chkBadConditions = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtDebug = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(828, 426);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 48);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1006, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 48);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTimerControl
            // 
            this.btnTimerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTimerControl.Location = new System.Drawing.Point(213, 427);
            this.btnTimerControl.Name = "btnTimerControl";
            this.btnTimerControl.Size = new System.Drawing.Size(178, 48);
            this.btnTimerControl.TabIndex = 4;
            this.btnTimerControl.Text = "Старт автозапись";
            this.btnTimerControl.UseVisualStyleBackColor = true;
            this.btnTimerControl.Click += new System.EventHandler(this.btnTimerControl_Click);
            // 
            // timerTime
            // 
            this.timerTime.Enabled = true;
            this.timerTime.Interval = 1000;
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // btnWriteNow
            // 
            this.btnWriteNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWriteNow.Location = new System.Drawing.Point(411, 427);
            this.btnWriteNow.Name = "btnWriteNow";
            this.btnWriteNow.Size = new System.Drawing.Size(178, 48);
            this.btnWriteNow.TabIndex = 4;
            this.btnWriteNow.Text = "Записать сейчас";
            this.btnWriteNow.UseVisualStyleBackColor = true;
            this.btnWriteNow.Click += new System.EventHandler(this.btnWriteNow_Click);
            // 
            // timerBolwoodWrite
            // 
            this.timerBolwoodWrite.Interval = 5000;
            this.timerBolwoodWrite.Tick += new System.EventHandler(this.timerBolwoodWrite_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(112, 438);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 26);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 440);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(84, 20);
            this.label22.TabIndex = 6;
            this.label22.Text = "Интервал";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.comboBoxWetFlag);
            this.groupBox1.Controls.Add(this.comboBoxAlertFlag);
            this.groupBox1.Controls.Add(this.comboBoxRoofCloseFlag);
            this.groupBox1.Controls.Add(this.comboBoxDaylightCond);
            this.groupBox1.Controls.Add(this.comboBoxRainCond);
            this.groupBox1.Controls.Add(this.comboBoxWindCond);
            this.groupBox1.Controls.Add(this.comboBoxCloudCond);
            this.groupBox1.Controls.Add(this.comboBoxRainFlag);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtVBANow);
            this.groupBox1.Controls.Add(this.txtSecondsSinceWrite);
            this.groupBox1.Controls.Add(this.txtHeating);
            this.groupBox1.Controls.Add(this.txtDewPoint);
            this.groupBox1.Controls.Add(this.txtHumidity);
            this.groupBox1.Controls.Add(this.txtWindSpeed);
            this.groupBox1.Controls.Add(this.txtSensorTemp);
            this.groupBox1.Controls.Add(this.txtAmbTemp);
            this.groupBox1.Controls.Add(this.txtSkyTemp);
            this.groupBox1.Controls.Add(this.txtTime);
            this.groupBox1.Controls.Add(this.txtDate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1139, 177);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Boltwood Cloud Sensor II Format";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSinceLastRain);
            this.groupBox3.Controls.Add(this.txtSinceLastWet);
            this.groupBox3.Controls.Add(this.chkWetNow);
            this.groupBox3.Controls.Add(this.chkRainNow);
            this.groupBox3.Location = new System.Drawing.Point(879, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 113);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            this.groupBox3.Visible = false;
            // 
            // txtSinceLastRain
            // 
            this.txtSinceLastRain.Location = new System.Drawing.Point(102, 29);
            this.txtSinceLastRain.Margin = new System.Windows.Forms.Padding(2);
            this.txtSinceLastRain.Name = "txtSinceLastRain";
            this.txtSinceLastRain.ReadOnly = true;
            this.txtSinceLastRain.Size = new System.Drawing.Size(131, 26);
            this.txtSinceLastRain.TabIndex = 19;
            // 
            // txtSinceLastWet
            // 
            this.txtSinceLastWet.Location = new System.Drawing.Point(102, 57);
            this.txtSinceLastWet.Margin = new System.Windows.Forms.Padding(2);
            this.txtSinceLastWet.Name = "txtSinceLastWet";
            this.txtSinceLastWet.ReadOnly = true;
            this.txtSinceLastWet.Size = new System.Drawing.Size(131, 26);
            this.txtSinceLastWet.TabIndex = 20;
            // 
            // chkWetNow
            // 
            this.chkWetNow.AutoSize = true;
            this.chkWetNow.Enabled = false;
            this.chkWetNow.Location = new System.Drawing.Point(-4, 59);
            this.chkWetNow.Margin = new System.Windows.Forms.Padding(2);
            this.chkWetNow.Name = "chkWetNow";
            this.chkWetNow.Size = new System.Drawing.Size(95, 24);
            this.chkWetNow.TabIndex = 17;
            this.chkWetNow.Text = "WetNow";
            this.chkWetNow.UseVisualStyleBackColor = true;
            // 
            // chkRainNow
            // 
            this.chkRainNow.AutoSize = true;
            this.chkRainNow.Enabled = false;
            this.chkRainNow.Location = new System.Drawing.Point(-4, 31);
            this.chkRainNow.Margin = new System.Windows.Forms.Padding(2);
            this.chkRainNow.Name = "chkRainNow";
            this.chkRainNow.Size = new System.Drawing.Size(99, 24);
            this.chkRainNow.TabIndex = 18;
            this.chkRainNow.Text = "RainNow";
            this.chkRainNow.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(776, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 20);
            this.label24.TabIndex = 49;
            this.label24.Text = "%";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(603, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 20);
            this.label23.TabIndex = 48;
            this.label23.Text = "%";
            // 
            // comboBoxWetFlag
            // 
            this.comboBoxWetFlag.FormattingEnabled = true;
            this.comboBoxWetFlag.Location = new System.Drawing.Point(968, 43);
            this.comboBoxWetFlag.Name = "comboBoxWetFlag";
            this.comboBoxWetFlag.Size = new System.Drawing.Size(148, 28);
            this.comboBoxWetFlag.TabIndex = 40;
            this.comboBoxWetFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxWetFlag_SelectedIndexChanged);
            // 
            // comboBoxAlertFlag
            // 
            this.comboBoxAlertFlag.FormattingEnabled = true;
            this.comboBoxAlertFlag.Location = new System.Drawing.Point(1025, 127);
            this.comboBoxAlertFlag.Name = "comboBoxAlertFlag";
            this.comboBoxAlertFlag.Size = new System.Drawing.Size(91, 28);
            this.comboBoxAlertFlag.TabIndex = 41;
            this.comboBoxAlertFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // comboBoxRoofCloseFlag
            // 
            this.comboBoxRoofCloseFlag.FormattingEnabled = true;
            this.comboBoxRoofCloseFlag.Location = new System.Drawing.Point(917, 127);
            this.comboBoxRoofCloseFlag.Name = "comboBoxRoofCloseFlag";
            this.comboBoxRoofCloseFlag.Size = new System.Drawing.Size(102, 28);
            this.comboBoxRoofCloseFlag.TabIndex = 42;
            this.comboBoxRoofCloseFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // comboBoxDaylightCond
            // 
            this.comboBoxDaylightCond.FormattingEnabled = true;
            this.comboBoxDaylightCond.Location = new System.Drawing.Point(763, 127);
            this.comboBoxDaylightCond.Name = "comboBoxDaylightCond";
            this.comboBoxDaylightCond.Size = new System.Drawing.Size(127, 28);
            this.comboBoxDaylightCond.TabIndex = 47;
            this.comboBoxDaylightCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // comboBoxRainCond
            // 
            this.comboBoxRainCond.FormattingEnabled = true;
            this.comboBoxRainCond.Location = new System.Drawing.Point(632, 127);
            this.comboBoxRainCond.Name = "comboBoxRainCond";
            this.comboBoxRainCond.Size = new System.Drawing.Size(125, 28);
            this.comboBoxRainCond.TabIndex = 44;
            this.comboBoxRainCond.SelectedIndexChanged += new System.EventHandler(this.comboBoxRainCond_SelectedIndexChanged);
            // 
            // comboBoxWindCond
            // 
            this.comboBoxWindCond.FormattingEnabled = true;
            this.comboBoxWindCond.Location = new System.Drawing.Point(461, 127);
            this.comboBoxWindCond.Name = "comboBoxWindCond";
            this.comboBoxWindCond.Size = new System.Drawing.Size(165, 28);
            this.comboBoxWindCond.TabIndex = 45;
            this.comboBoxWindCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // comboBoxCloudCond
            // 
            this.comboBoxCloudCond.FormattingEnabled = true;
            this.comboBoxCloudCond.Location = new System.Drawing.Point(304, 127);
            this.comboBoxCloudCond.Name = "comboBoxCloudCond";
            this.comboBoxCloudCond.Size = new System.Drawing.Size(151, 28);
            this.comboBoxCloudCond.TabIndex = 46;
            this.comboBoxCloudCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // comboBoxRainFlag
            // 
            this.comboBoxRainFlag.FormattingEnabled = true;
            this.comboBoxRainFlag.Location = new System.Drawing.Point(802, 43);
            this.comboBoxRainFlag.Name = "comboBoxRainFlag";
            this.comboBoxRainFlag.Size = new System.Drawing.Size(149, 28);
            this.comboBoxRainFlag.TabIndex = 43;
            this.comboBoxRainFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxRainFlag_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "C";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(970, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 20);
            this.label13.TabIndex = 36;
            this.label13.Text = "Wet";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(812, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 20);
            this.label12.TabIndex = 35;
            this.label12.Text = "ДождьF";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(117, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 20);
            this.label15.TabIndex = 34;
            this.label15.Text = "Now()";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1021, 104);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 20);
            this.label21.TabIndex = 33;
            this.label21.Text = "Alerting";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(913, 107);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 20);
            this.label20.TabIndex = 32;
            this.label20.Text = "RoofClose";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(763, 107);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 20);
            this.label19.TabIndex = 31;
            this.label19.Text = "Daylight";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(632, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 20);
            this.label18.TabIndex = 30;
            this.label18.Text = "Rain";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(461, 107);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 20);
            this.label17.TabIndex = 39;
            this.label17.Text = "Wind";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(301, 107);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 20);
            this.label16.TabIndex = 29;
            this.label16.Text = "Cloud";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 20);
            this.label14.TabIndex = 19;
            this.label14.Text = "Секунд";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(742, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 20);
            this.label11.TabIndex = 27;
            this.label11.Text = "Нагрев";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(646, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 20);
            this.label10.TabIndex = 26;
            this.label10.Text = "Точка росы";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(553, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 25;
            this.label9.Text = "Влажность";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(475, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "Wind";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(406, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Sensor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(350, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Ambient";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Sky";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Время";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Дата";
            // 
            // txtVBANow
            // 
            this.txtVBANow.Location = new System.Drawing.Point(121, 127);
            this.txtVBANow.Name = "txtVBANow";
            this.txtVBANow.ReadOnly = true;
            this.txtVBANow.Size = new System.Drawing.Size(143, 26);
            this.txtVBANow.TabIndex = 9;
            // 
            // txtSecondsSinceWrite
            // 
            this.txtSecondsSinceWrite.Location = new System.Drawing.Point(15, 127);
            this.txtSecondsSinceWrite.Name = "txtSecondsSinceWrite";
            this.txtSecondsSinceWrite.ReadOnly = true;
            this.txtSecondsSinceWrite.Size = new System.Drawing.Size(100, 26);
            this.txtSecondsSinceWrite.TabIndex = 10;
            // 
            // txtHeating
            // 
            this.txtHeating.Location = new System.Drawing.Point(743, 43);
            this.txtHeating.Name = "txtHeating";
            this.txtHeating.Size = new System.Drawing.Size(29, 26);
            this.txtHeating.TabIndex = 11;
            this.txtHeating.Text = "30";
            this.txtHeating.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtDewPoint
            // 
            this.txtDewPoint.Location = new System.Drawing.Point(650, 43);
            this.txtDewPoint.Name = "txtDewPoint";
            this.txtDewPoint.ReadOnly = true;
            this.txtDewPoint.Size = new System.Drawing.Size(76, 26);
            this.txtDewPoint.TabIndex = 12;
            // 
            // txtHumidity
            // 
            this.txtHumidity.Location = new System.Drawing.Point(557, 43);
            this.txtHumidity.Name = "txtHumidity";
            this.txtHumidity.Size = new System.Drawing.Size(40, 26);
            this.txtHumidity.TabIndex = 18;
            this.txtHumidity.Text = "70";
            this.txtHumidity.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtWindSpeed
            // 
            this.txtWindSpeed.Location = new System.Drawing.Point(474, 43);
            this.txtWindSpeed.Name = "txtWindSpeed";
            this.txtWindSpeed.Size = new System.Drawing.Size(62, 26);
            this.txtWindSpeed.TabIndex = 14;
            this.txtWindSpeed.Text = "2";
            this.txtWindSpeed.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtSensorTemp
            // 
            this.txtSensorTemp.Location = new System.Drawing.Point(410, 43);
            this.txtSensorTemp.Name = "txtSensorTemp";
            this.txtSensorTemp.Size = new System.Drawing.Size(45, 26);
            this.txtSensorTemp.TabIndex = 15;
            this.txtSensorTemp.Text = "10";
            this.txtSensorTemp.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtAmbTemp
            // 
            this.txtAmbTemp.Location = new System.Drawing.Point(357, 43);
            this.txtAmbTemp.Name = "txtAmbTemp";
            this.txtAmbTemp.Size = new System.Drawing.Size(45, 26);
            this.txtAmbTemp.TabIndex = 16;
            this.txtAmbTemp.Text = "10";
            this.txtAmbTemp.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtSkyTemp
            // 
            this.txtSkyTemp.Location = new System.Drawing.Point(304, 43);
            this.txtSkyTemp.Name = "txtSkyTemp";
            this.txtSkyTemp.Size = new System.Drawing.Size(45, 26);
            this.txtSkyTemp.TabIndex = 17;
            this.txtSkyTemp.Text = "-10";
            this.txtSkyTemp.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(131, 43);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(100, 26);
            this.txtTime.TabIndex = 13;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(15, 43);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(100, 26);
            this.txtDate.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.chkUseSmartLogic);
            this.groupBox2.Controls.Add(this.btnFileDialog);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.comboBoxDecimalSeparator);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.txtFilePath);
            this.groupBox2.Controls.Add(this.txtLastWritten);
            this.groupBox2.Location = new System.Drawing.Point(12, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 163);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вспомогательные поля";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(15, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(149, 20);
            this.label26.TabIndex = 17;
            this.label26.Text = "Десятичная точка";
            // 
            // chkUseSmartLogic
            // 
            this.chkUseSmartLogic.AutoSize = true;
            this.chkUseSmartLogic.Checked = global::WeatherControl.Properties.Settings.Default.UseBoltwoodLogic;
            this.chkUseSmartLogic.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::WeatherControl.Properties.Settings.Default, "UseBoltwoodLogic", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkUseSmartLogic.Location = new System.Drawing.Point(15, 125);
            this.chkUseSmartLogic.Margin = new System.Windows.Forms.Padding(2);
            this.chkUseSmartLogic.Name = "chkUseSmartLogic";
            this.chkUseSmartLogic.Size = new System.Drawing.Size(413, 24);
            this.chkUseSmartLogic.TabIndex = 8;
            this.chkUseSmartLogic.Text = "Использовать логику в комбинации разных полей";
            this.chkUseSmartLogic.UseVisualStyleBackColor = true;
            this.chkUseSmartLogic.CheckedChanged += new System.EventHandler(this.chkUseSmartLogic_CheckedChanged);
            // 
            // btnFileDialog
            // 
            this.btnFileDialog.Location = new System.Drawing.Point(570, 82);
            this.btnFileDialog.Margin = new System.Windows.Forms.Padding(2);
            this.btnFileDialog.Name = "btnFileDialog";
            this.btnFileDialog.Size = new System.Drawing.Size(56, 30);
            this.btnFileDialog.TabIndex = 7;
            this.btnFileDialog.Text = "...";
            this.btnFileDialog.UseVisualStyleBackColor = true;
            this.btnFileDialog.Click += new System.EventHandler(this.btnFileDialog_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(11, 87);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(120, 20);
            this.label25.TabIndex = 6;
            this.label25.Text = "Пусть к файлу";
            // 
            // comboBoxDecimalSeparator
            // 
            this.comboBoxDecimalSeparator.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WeatherControl.Properties.Settings.Default, "decimalPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBoxDecimalSeparator.FormattingEnabled = true;
            this.comboBoxDecimalSeparator.Location = new System.Drawing.Point(188, 24);
            this.comboBoxDecimalSeparator.Name = "comboBoxDecimalSeparator";
            this.comboBoxDecimalSeparator.Size = new System.Drawing.Size(163, 28);
            this.comboBoxDecimalSeparator.TabIndex = 46;
            this.comboBoxDecimalSeparator.Text = global::WeatherControl.Properties.Settings.Default.decimalPoint;
            this.comboBoxDecimalSeparator.SelectedIndexChanged += new System.EventHandler(this.comboBoxDecimalSeparator_SelectedIndexChanged);
            this.comboBoxDecimalSeparator.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(11, 56);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(151, 20);
            this.label27.TabIndex = 6;
            this.label27.Text = "Последняя запись";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(188, 85);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(368, 26);
            this.txtFilePath.TabIndex = 4;
            // 
            // txtLastWritten
            // 
            this.txtLastWritten.Location = new System.Drawing.Point(188, 53);
            this.txtLastWritten.Name = "txtLastWritten";
            this.txtLastWritten.ReadOnly = true;
            this.txtLastWritten.Size = new System.Drawing.Size(162, 26);
            this.txtLastWritten.TabIndex = 4;
            // 
            // chkSaveConditions
            // 
            this.chkSaveConditions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSaveConditions.AutoSize = true;
            this.chkSaveConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSaveConditions.Location = new System.Drawing.Point(677, 231);
            this.chkSaveConditions.Name = "chkSaveConditions";
            this.chkSaveConditions.Size = new System.Drawing.Size(92, 30);
            this.chkSaveConditions.TabIndex = 11;
            this.chkSaveConditions.Text = "Записать";
            this.chkSaveConditions.UseVisualStyleBackColor = true;
            this.chkSaveConditions.CheckedChanged += new System.EventHandler(this.chkSaveConditions_CheckedChanged);
            // 
            // chkGoodConditions
            // 
            this.chkGoodConditions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGoodConditions.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGoodConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGoodConditions.Location = new System.Drawing.Point(790, 211);
            this.chkGoodConditions.Name = "chkGoodConditions";
            this.chkGoodConditions.Size = new System.Drawing.Size(173, 70);
            this.chkGoodConditions.TabIndex = 11;
            this.chkGoodConditions.Text = "Благоприятные условия";
            this.chkGoodConditions.UseVisualStyleBackColor = true;
            this.chkGoodConditions.CheckedChanged += new System.EventHandler(this.chkGoodConditions_CheckedChanged);
            // 
            // chkBadConditions
            // 
            this.chkBadConditions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBadConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBadConditions.Location = new System.Drawing.Point(978, 211);
            this.chkBadConditions.Name = "chkBadConditions";
            this.chkBadConditions.Size = new System.Drawing.Size(173, 70);
            this.chkBadConditions.TabIndex = 11;
            this.chkBadConditions.Text = "Неблагоприятные условия";
            this.chkBadConditions.UseVisualStyleBackColor = true;
            this.chkBadConditions.CheckedChanged += new System.EventHandler(this.chkBadConditions_CheckedChanged);
            // 
            // txtDebug
            // 
            this.txtDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebug.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDebug.Location = new System.Drawing.Point(16, 384);
            this.txtDebug.Margin = new System.Windows.Forms.Padding(2);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ReadOnly = true;
            this.txtDebug.Size = new System.Drawing.Size(1140, 34);
            this.txtDebug.TabIndex = 12;
            // 
            // FormWeatherFileControl
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1169, 486);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.chkBadConditions);
            this.Controls.Add(this.chkGoodConditions);
            this.Controls.Add(this.chkSaveConditions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnWriteNow);
            this.Controls.Add(this.btnTimerControl);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormWeatherFileControl";
            this.Text = "Эмуляция файла Boltwood Clound Sensor II";
            this.Load += new System.EventHandler(this.FormWeatherFileControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnTimerControl;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Button btnWriteNow;
        private System.Windows.Forms.Timer timerBolwoodWrite;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox comboBoxWetFlag;
        private System.Windows.Forms.ComboBox comboBoxAlertFlag;
        private System.Windows.Forms.ComboBox comboBoxRoofCloseFlag;
        private System.Windows.Forms.ComboBox comboBoxDaylightCond;
        private System.Windows.Forms.ComboBox comboBoxRainCond;
        private System.Windows.Forms.ComboBox comboBoxWindCond;
        private System.Windows.Forms.ComboBox comboBoxCloudCond;
        private System.Windows.Forms.ComboBox comboBoxRainFlag;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVBANow;
        private System.Windows.Forms.TextBox txtSecondsSinceWrite;
        private System.Windows.Forms.TextBox txtHeating;
        private System.Windows.Forms.TextBox txtDewPoint;
        private System.Windows.Forms.TextBox txtHumidity;
        private System.Windows.Forms.TextBox txtWindSpeed;
        private System.Windows.Forms.TextBox txtSensorTemp;
        private System.Windows.Forms.TextBox txtAmbTemp;
        private System.Windows.Forms.TextBox txtSkyTemp;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtLastWritten;
        private System.Windows.Forms.CheckBox chkSaveConditions;
        private System.Windows.Forms.CheckBox chkGoodConditions;
        private System.Windows.Forms.CheckBox chkBadConditions;
        private System.Windows.Forms.Button btnFileDialog;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.CheckBox chkUseSmartLogic;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBoxDecimalSeparator;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSinceLastRain;
        private System.Windows.Forms.TextBox txtSinceLastWet;
        private System.Windows.Forms.CheckBox chkWetNow;
        private System.Windows.Forms.CheckBox chkRainNow;
    }
}

