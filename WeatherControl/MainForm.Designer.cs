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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTimerControl = new System.Windows.Forms.Button();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.btnWriteNow = new System.Windows.Forms.Button();
            this.timerBolwoodWrite = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.label27 = new System.Windows.Forms.Label();
            this.txtLastWritten = new System.Windows.Forms.TextBox();
            this.chkSaveConditions = new System.Windows.Forms.CheckBox();
            this.chkGoodConditions = new System.Windows.Forms.CheckBox();
            this.chkBadConditions = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFileDialog = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(1109, 588);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(200, 60);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1343, 588);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 60);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTimerControl
            // 
            this.btnTimerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTimerControl.Location = new System.Drawing.Point(284, 588);
            this.btnTimerControl.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimerControl.Name = "btnTimerControl";
            this.btnTimerControl.Size = new System.Drawing.Size(237, 60);
            this.btnTimerControl.TabIndex = 4;
            this.btnTimerControl.Text = "Старт автозапись";
            this.btnTimerControl.UseVisualStyleBackColor = true;
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
            this.btnWriteNow.Location = new System.Drawing.Point(548, 588);
            this.btnWriteNow.Margin = new System.Windows.Forms.Padding(4);
            this.btnWriteNow.Name = "btnWriteNow";
            this.btnWriteNow.Size = new System.Drawing.Size(237, 60);
            this.btnWriteNow.TabIndex = 4;
            this.btnWriteNow.Text = "Записать сейчас";
            this.btnWriteNow.UseVisualStyleBackColor = true;
            this.btnWriteNow.Click += new System.EventHandler(this.btnWriteNow_Click);
            // 
            // timerBolwoodWrite
            // 
            this.timerBolwoodWrite.Interval = 5000;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(149, 602);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
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
            this.numericUpDown1.Size = new System.Drawing.Size(99, 31);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(16, 605);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 25);
            this.label22.TabIndex = 6;
            this.label22.Text = "Интервал";
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1519, 221);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Boltwood Cloud Sensor II Format";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(1035, 58);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(31, 25);
            this.label24.TabIndex = 49;
            this.label24.Text = "%";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(804, 58);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 25);
            this.label23.TabIndex = 48;
            this.label23.Text = "%";
            // 
            // comboBoxWetFlag
            // 
            this.comboBoxWetFlag.FormattingEnabled = true;
            this.comboBoxWetFlag.Location = new System.Drawing.Point(1291, 54);
            this.comboBoxWetFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxWetFlag.Name = "comboBoxWetFlag";
            this.comboBoxWetFlag.Size = new System.Drawing.Size(196, 33);
            this.comboBoxWetFlag.TabIndex = 40;
            // 
            // comboBoxAlertFlag
            // 
            this.comboBoxAlertFlag.FormattingEnabled = true;
            this.comboBoxAlertFlag.Location = new System.Drawing.Point(1367, 159);
            this.comboBoxAlertFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAlertFlag.Name = "comboBoxAlertFlag";
            this.comboBoxAlertFlag.Size = new System.Drawing.Size(120, 33);
            this.comboBoxAlertFlag.TabIndex = 41;
            // 
            // comboBoxRoofCloseFlag
            // 
            this.comboBoxRoofCloseFlag.FormattingEnabled = true;
            this.comboBoxRoofCloseFlag.Location = new System.Drawing.Point(1223, 159);
            this.comboBoxRoofCloseFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxRoofCloseFlag.Name = "comboBoxRoofCloseFlag";
            this.comboBoxRoofCloseFlag.Size = new System.Drawing.Size(135, 33);
            this.comboBoxRoofCloseFlag.TabIndex = 42;
            // 
            // comboBoxDaylightCond
            // 
            this.comboBoxDaylightCond.FormattingEnabled = true;
            this.comboBoxDaylightCond.Location = new System.Drawing.Point(1017, 159);
            this.comboBoxDaylightCond.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDaylightCond.Name = "comboBoxDaylightCond";
            this.comboBoxDaylightCond.Size = new System.Drawing.Size(168, 33);
            this.comboBoxDaylightCond.TabIndex = 47;
            // 
            // comboBoxRainCond
            // 
            this.comboBoxRainCond.FormattingEnabled = true;
            this.comboBoxRainCond.Location = new System.Drawing.Point(843, 159);
            this.comboBoxRainCond.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxRainCond.Name = "comboBoxRainCond";
            this.comboBoxRainCond.Size = new System.Drawing.Size(165, 33);
            this.comboBoxRainCond.TabIndex = 44;
            // 
            // comboBoxWindCond
            // 
            this.comboBoxWindCond.FormattingEnabled = true;
            this.comboBoxWindCond.Location = new System.Drawing.Point(615, 159);
            this.comboBoxWindCond.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxWindCond.Name = "comboBoxWindCond";
            this.comboBoxWindCond.Size = new System.Drawing.Size(219, 33);
            this.comboBoxWindCond.TabIndex = 45;
            // 
            // comboBoxCloudCond
            // 
            this.comboBoxCloudCond.FormattingEnabled = true;
            this.comboBoxCloudCond.Location = new System.Drawing.Point(405, 159);
            this.comboBoxCloudCond.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCloudCond.Name = "comboBoxCloudCond";
            this.comboBoxCloudCond.Size = new System.Drawing.Size(200, 33);
            this.comboBoxCloudCond.TabIndex = 46;
            // 
            // comboBoxRainFlag
            // 
            this.comboBoxRainFlag.FormattingEnabled = true;
            this.comboBoxRainFlag.Location = new System.Drawing.Point(1069, 54);
            this.comboBoxRainFlag.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxRainFlag.Name = "comboBoxRainFlag";
            this.comboBoxRainFlag.Size = new System.Drawing.Size(197, 33);
            this.comboBoxRainFlag.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "C";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1293, 25);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 25);
            this.label13.TabIndex = 36;
            this.label13.Text = "Wet";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1083, 29);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 25);
            this.label12.TabIndex = 35;
            this.label12.Text = "ДождьF";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(156, 134);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 25);
            this.label15.TabIndex = 34;
            this.label15.Text = "Now()";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1361, 130);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(85, 25);
            this.label21.TabIndex = 33;
            this.label21.Text = "Alerting";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1217, 134);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(112, 25);
            this.label20.TabIndex = 32;
            this.label20.Text = "RoofClose";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1017, 134);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 25);
            this.label19.TabIndex = 31;
            this.label19.Text = "Daylight";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(843, 134);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 25);
            this.label18.TabIndex = 30;
            this.label18.Text = "Rain";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(615, 134);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 25);
            this.label17.TabIndex = 39;
            this.label17.Text = "Wind";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(401, 134);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 25);
            this.label16.TabIndex = 29;
            this.label16.Text = "Cloud";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 134);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 25);
            this.label14.TabIndex = 19;
            this.label14.Text = "Секунд";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(989, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 25);
            this.label11.TabIndex = 27;
            this.label11.Text = "Нагрев";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(861, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 25);
            this.label10.TabIndex = 26;
            this.label10.Text = "Точка росы";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(737, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 25);
            this.label9.TabIndex = 25;
            this.label9.Text = "Влажность";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(633, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 25);
            this.label8.TabIndex = 24;
            this.label8.Text = "Wind";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(541, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 25);
            this.label7.TabIndex = 23;
            this.label7.Text = "Sensor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(467, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Ambient";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 25);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Sky";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Время";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "Дата";
            // 
            // txtVBANow
            // 
            this.txtVBANow.Location = new System.Drawing.Point(161, 159);
            this.txtVBANow.Margin = new System.Windows.Forms.Padding(4);
            this.txtVBANow.Name = "txtVBANow";
            this.txtVBANow.ReadOnly = true;
            this.txtVBANow.Size = new System.Drawing.Size(189, 31);
            this.txtVBANow.TabIndex = 9;
            // 
            // txtSecondsSinceWrite
            // 
            this.txtSecondsSinceWrite.Location = new System.Drawing.Point(20, 159);
            this.txtSecondsSinceWrite.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecondsSinceWrite.Name = "txtSecondsSinceWrite";
            this.txtSecondsSinceWrite.ReadOnly = true;
            this.txtSecondsSinceWrite.Size = new System.Drawing.Size(132, 31);
            this.txtSecondsSinceWrite.TabIndex = 10;
            // 
            // txtHeating
            // 
            this.txtHeating.Location = new System.Drawing.Point(991, 54);
            this.txtHeating.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeating.Name = "txtHeating";
            this.txtHeating.Size = new System.Drawing.Size(37, 31);
            this.txtHeating.TabIndex = 11;
            this.txtHeating.Text = "30";
            // 
            // txtDewPoint
            // 
            this.txtDewPoint.Location = new System.Drawing.Point(867, 54);
            this.txtDewPoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtDewPoint.Name = "txtDewPoint";
            this.txtDewPoint.ReadOnly = true;
            this.txtDewPoint.Size = new System.Drawing.Size(100, 31);
            this.txtDewPoint.TabIndex = 12;
            // 
            // txtHumidity
            // 
            this.txtHumidity.Location = new System.Drawing.Point(743, 54);
            this.txtHumidity.Margin = new System.Windows.Forms.Padding(4);
            this.txtHumidity.Name = "txtHumidity";
            this.txtHumidity.Size = new System.Drawing.Size(52, 31);
            this.txtHumidity.TabIndex = 18;
            this.txtHumidity.Text = "70";
            // 
            // txtWindSpeed
            // 
            this.txtWindSpeed.Location = new System.Drawing.Point(632, 54);
            this.txtWindSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.txtWindSpeed.Name = "txtWindSpeed";
            this.txtWindSpeed.Size = new System.Drawing.Size(81, 31);
            this.txtWindSpeed.TabIndex = 14;
            this.txtWindSpeed.Text = "2";
            // 
            // txtSensorTemp
            // 
            this.txtSensorTemp.Location = new System.Drawing.Point(547, 54);
            this.txtSensorTemp.Margin = new System.Windows.Forms.Padding(4);
            this.txtSensorTemp.Name = "txtSensorTemp";
            this.txtSensorTemp.Size = new System.Drawing.Size(59, 31);
            this.txtSensorTemp.TabIndex = 15;
            this.txtSensorTemp.Text = "10";
            // 
            // txtAmbTemp
            // 
            this.txtAmbTemp.Location = new System.Drawing.Point(476, 54);
            this.txtAmbTemp.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmbTemp.Name = "txtAmbTemp";
            this.txtAmbTemp.Size = new System.Drawing.Size(59, 31);
            this.txtAmbTemp.TabIndex = 16;
            this.txtAmbTemp.Text = "10";
            // 
            // txtSkyTemp
            // 
            this.txtSkyTemp.Location = new System.Drawing.Point(405, 54);
            this.txtSkyTemp.Margin = new System.Windows.Forms.Padding(4);
            this.txtSkyTemp.Name = "txtSkyTemp";
            this.txtSkyTemp.Size = new System.Drawing.Size(59, 31);
            this.txtSkyTemp.TabIndex = 17;
            this.txtSkyTemp.Text = "-10";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(175, 54);
            this.txtTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(132, 31);
            this.txtTime.TabIndex = 13;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(20, 54);
            this.txtDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(132, 31);
            this.txtDate.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFileDialog);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.txtFilePath);
            this.groupBox2.Controls.Add(this.txtLastWritten);
            this.groupBox2.Location = new System.Drawing.Point(16, 244);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(858, 192);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вспомогательные поля";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 70);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(195, 25);
            this.label27.TabIndex = 6;
            this.label27.Text = "Последняя запись";
            // 
            // txtLastWritten
            // 
            this.txtLastWritten.Location = new System.Drawing.Point(251, 66);
            this.txtLastWritten.Margin = new System.Windows.Forms.Padding(4);
            this.txtLastWritten.Name = "txtLastWritten";
            this.txtLastWritten.ReadOnly = true;
            this.txtLastWritten.Size = new System.Drawing.Size(215, 31);
            this.txtLastWritten.TabIndex = 4;
            // 
            // chkSaveConditions
            // 
            this.chkSaveConditions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSaveConditions.AutoSize = true;
            this.chkSaveConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSaveConditions.Location = new System.Drawing.Point(903, 289);
            this.chkSaveConditions.Margin = new System.Windows.Forms.Padding(4);
            this.chkSaveConditions.Name = "chkSaveConditions";
            this.chkSaveConditions.Size = new System.Drawing.Size(115, 35);
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
            this.chkGoodConditions.Location = new System.Drawing.Point(1053, 264);
            this.chkGoodConditions.Margin = new System.Windows.Forms.Padding(4);
            this.chkGoodConditions.Name = "chkGoodConditions";
            this.chkGoodConditions.Size = new System.Drawing.Size(231, 88);
            this.chkGoodConditions.TabIndex = 11;
            this.chkGoodConditions.Text = "Благоприятные условия";
            this.chkGoodConditions.UseVisualStyleBackColor = true;
            this.chkGoodConditions.CheckedChanged += new System.EventHandler(this.chkGoodConditions_CheckedChanged);
            // 
            // chkBadConditions
            // 
            this.chkBadConditions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBadConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBadConditions.Location = new System.Drawing.Point(1304, 264);
            this.chkBadConditions.Margin = new System.Windows.Forms.Padding(4);
            this.chkBadConditions.Name = "chkBadConditions";
            this.chkBadConditions.Size = new System.Drawing.Size(231, 88);
            this.chkBadConditions.TabIndex = 11;
            this.chkBadConditions.Text = "Неблагоприятные условия";
            this.chkBadConditions.UseVisualStyleBackColor = true;
            this.chkBadConditions.CheckedChanged += new System.EventHandler(this.chkBadConditions_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(15, 109);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(157, 25);
            this.label25.TabIndex = 6;
            this.label25.Text = "Пусть к файлу";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(251, 106);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(490, 31);
            this.txtFilePath.TabIndex = 4;
            // 
            // btnFileDialog
            // 
            this.btnFileDialog.Location = new System.Drawing.Point(760, 102);
            this.btnFileDialog.Name = "btnFileDialog";
            this.btnFileDialog.Size = new System.Drawing.Size(75, 38);
            this.btnFileDialog.TabIndex = 7;
            this.btnFileDialog.Text = "...";
            this.btnFileDialog.UseVisualStyleBackColor = true;
            this.btnFileDialog.Click += new System.EventHandler(this.btnFileDialog_Click);
            // 
            // FormWeatherFileControl
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1559, 662);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormWeatherFileControl";
            this.Text = "Запись файла Boltwood Clound Sensor II";
            this.Load += new System.EventHandler(this.FormWeatherFileControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}

