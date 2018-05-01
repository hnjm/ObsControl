namespace ObservatoryCenter
{
    partial class TestEquipmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestEquipmentForm));
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.chkTestCdC_connect = new System.Windows.Forms.CheckBox();
            this.chkTestCdC_run = new System.Windows.Forms.CheckBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.chkTestFM_focusermove = new System.Windows.Forms.CheckBox();
            this.chkTestFM_run = new System.Windows.Forms.CheckBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.chkTestPHD_guiding = new System.Windows.Forms.CheckBox();
            this.chkTestPHD_connect = new System.Windows.Forms.CheckBox();
            this.chkTestPHD_run = new System.Windows.Forms.CheckBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.chkTestMaximDl_setcooling = new System.Windows.Forms.CheckBox();
            this.chkTestMaximDl_CameraShoot = new System.Windows.Forms.CheckBox();
            this.chkTestMaximDl_FilterWheel = new System.Windows.Forms.CheckBox();
            this.chkTestMaximDl_telescopeconnect = new System.Windows.Forms.CheckBox();
            this.chkTestMaximDl_cameraconnect = new System.Windows.Forms.CheckBox();
            this.chkTestMaximDl_Run = new System.Windows.Forms.CheckBox();
            this.btnRunObservatoryTest = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtTestFormLog = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker_test = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTTC_heating = new System.Windows.Forms.CheckBox();
            this.chkTTC_fan = new System.Windows.Forms.CheckBox();
            this.chkTTC_run = new System.Windows.Forms.CheckBox();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.chkTestCdC_connect);
            this.groupBox14.Controls.Add(this.chkTestCdC_run);
            this.groupBox14.Location = new System.Drawing.Point(12, 341);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(365, 94);
            this.groupBox14.TabIndex = 2;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Cartes du Ciel";
            // 
            // chkTestCdC_connect
            // 
            this.chkTestCdC_connect.AutoSize = true;
            this.chkTestCdC_connect.Checked = true;
            this.chkTestCdC_connect.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestCdC_connect.Location = new System.Drawing.Point(21, 59);
            this.chkTestCdC_connect.Name = "chkTestCdC_connect";
            this.chkTestCdC_connect.Size = new System.Drawing.Size(200, 24);
            this.chkTestCdC_connect.TabIndex = 0;
            this.chkTestCdC_connect.Text = "CdC telescope connect";
            this.chkTestCdC_connect.UseVisualStyleBackColor = true;
            // 
            // chkTestCdC_run
            // 
            this.chkTestCdC_run.AutoSize = true;
            this.chkTestCdC_run.Checked = true;
            this.chkTestCdC_run.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestCdC_run.Location = new System.Drawing.Point(21, 29);
            this.chkTestCdC_run.Name = "chkTestCdC_run";
            this.chkTestCdC_run.Size = new System.Drawing.Size(93, 24);
            this.chkTestCdC_run.TabIndex = 0;
            this.chkTestCdC_run.Text = "CdC run";
            this.chkTestCdC_run.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.chkTestFM_focusermove);
            this.groupBox15.Controls.Add(this.chkTestFM_run);
            this.groupBox15.Location = new System.Drawing.Point(12, 441);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(365, 96);
            this.groupBox15.TabIndex = 3;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "FocusMax";
            // 
            // chkTestFM_focusermove
            // 
            this.chkTestFM_focusermove.AutoSize = true;
            this.chkTestFM_focusermove.Checked = true;
            this.chkTestFM_focusermove.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestFM_focusermove.Location = new System.Drawing.Point(21, 59);
            this.chkTestFM_focusermove.Name = "chkTestFM_focusermove";
            this.chkTestFM_focusermove.Size = new System.Drawing.Size(157, 24);
            this.chkTestFM_focusermove.TabIndex = 0;
            this.chkTestFM_focusermove.Text = "FM focuser move";
            this.chkTestFM_focusermove.UseVisualStyleBackColor = true;
            // 
            // chkTestFM_run
            // 
            this.chkTestFM_run.AutoSize = true;
            this.chkTestFM_run.Checked = true;
            this.chkTestFM_run.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestFM_run.Location = new System.Drawing.Point(21, 29);
            this.chkTestFM_run.Name = "chkTestFM_run";
            this.chkTestFM_run.Size = new System.Drawing.Size(85, 24);
            this.chkTestFM_run.TabIndex = 0;
            this.chkTestFM_run.Text = "FM run";
            this.chkTestFM_run.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.chkTestPHD_guiding);
            this.groupBox13.Controls.Add(this.chkTestPHD_connect);
            this.groupBox13.Controls.Add(this.chkTestPHD_run);
            this.groupBox13.Location = new System.Drawing.Point(12, 211);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(365, 124);
            this.groupBox13.TabIndex = 4;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "PHD2";
            // 
            // chkTestPHD_guiding
            // 
            this.chkTestPHD_guiding.AutoSize = true;
            this.chkTestPHD_guiding.Checked = true;
            this.chkTestPHD_guiding.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestPHD_guiding.Location = new System.Drawing.Point(21, 89);
            this.chkTestPHD_guiding.Name = "chkTestPHD_guiding";
            this.chkTestPHD_guiding.Size = new System.Drawing.Size(133, 24);
            this.chkTestPHD_guiding.TabIndex = 0;
            this.chkTestPHD_guiding.Text = "PHD2 guiding";
            this.chkTestPHD_guiding.UseVisualStyleBackColor = true;
            // 
            // chkTestPHD_connect
            // 
            this.chkTestPHD_connect.AutoSize = true;
            this.chkTestPHD_connect.Checked = true;
            this.chkTestPHD_connect.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestPHD_connect.Location = new System.Drawing.Point(21, 59);
            this.chkTestPHD_connect.Name = "chkTestPHD_connect";
            this.chkTestPHD_connect.Size = new System.Drawing.Size(139, 24);
            this.chkTestPHD_connect.TabIndex = 0;
            this.chkTestPHD_connect.Text = "PHD2 connect";
            this.chkTestPHD_connect.UseVisualStyleBackColor = true;
            // 
            // chkTestPHD_run
            // 
            this.chkTestPHD_run.AutoSize = true;
            this.chkTestPHD_run.Checked = true;
            this.chkTestPHD_run.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestPHD_run.Location = new System.Drawing.Point(21, 29);
            this.chkTestPHD_run.Name = "chkTestPHD_run";
            this.chkTestPHD_run.Size = new System.Drawing.Size(105, 24);
            this.chkTestPHD_run.TabIndex = 0;
            this.chkTestPHD_run.Text = "PHD2 run";
            this.chkTestPHD_run.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.checkBox2);
            this.groupBox16.Controls.Add(this.checkBox9);
            this.groupBox16.Controls.Add(this.button1);
            this.groupBox16.Controls.Add(this.checkBox10);
            this.groupBox16.Controls.Add(this.checkBox11);
            this.groupBox16.Controls.Add(this.checkBox12);
            this.groupBox16.Controls.Add(this.checkBox13);
            this.groupBox16.Location = new System.Drawing.Point(12, 713);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(365, 203);
            this.groupBox16.TabIndex = 5;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Operation test";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox2.Location = new System.Drawing.Point(21, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(100, 24);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Focusing";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Checked = true;
            this.checkBox9.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox9.Location = new System.Drawing.Point(21, 174);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(90, 24);
            this.checkBox9.TabIndex = 0;
            this.checkBox9.Text = "Guiding";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Checked = true;
            this.checkBox10.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox10.Location = new System.Drawing.Point(21, 145);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(138, 24);
            this.checkBox10.TabIndex = 0;
            this.checkBox10.Text = "Pole Align Max";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Checked = true;
            this.checkBox11.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox11.Location = new System.Drawing.Point(21, 115);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(148, 24);
            this.checkBox11.TabIndex = 0;
            this.checkBox11.Text = "Solving PinPoint";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Checked = true;
            this.checkBox12.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox12.Location = new System.Drawing.Point(21, 85);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(174, 24);
            this.checkBox12.TabIndex = 0;
            this.checkBox12.Text = "Solving AstroTortilla";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Checked = true;
            this.checkBox13.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox13.Location = new System.Drawing.Point(21, 29);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(167, 24);
            this.checkBox13.TabIndex = 0;
            this.checkBox13.Text = "Moving mount to E";
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.chkTestMaximDl_setcooling);
            this.groupBox12.Controls.Add(this.chkTestMaximDl_CameraShoot);
            this.groupBox12.Controls.Add(this.chkTestMaximDl_FilterWheel);
            this.groupBox12.Controls.Add(this.chkTestMaximDl_telescopeconnect);
            this.groupBox12.Controls.Add(this.chkTestMaximDl_cameraconnect);
            this.groupBox12.Controls.Add(this.chkTestMaximDl_Run);
            this.groupBox12.Location = new System.Drawing.Point(12, 2);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(365, 203);
            this.groupBox12.TabIndex = 6;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Maxim DL";
            // 
            // chkTestMaximDl_setcooling
            // 
            this.chkTestMaximDl_setcooling.AutoSize = true;
            this.chkTestMaximDl_setcooling.Checked = true;
            this.chkTestMaximDl_setcooling.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_setcooling.Location = new System.Drawing.Point(21, 119);
            this.chkTestMaximDl_setcooling.Name = "chkTestMaximDl_setcooling";
            this.chkTestMaximDl_setcooling.Size = new System.Drawing.Size(238, 24);
            this.chkTestMaximDl_setcooling.TabIndex = 0;
            this.chkTestMaximDl_setcooling.Text = "MaximDL camera set cooling";
            this.chkTestMaximDl_setcooling.UseVisualStyleBackColor = true;
            // 
            // chkTestMaximDl_CameraShoot
            // 
            this.chkTestMaximDl_CameraShoot.AutoSize = true;
            this.chkTestMaximDl_CameraShoot.Checked = true;
            this.chkTestMaximDl_CameraShoot.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_CameraShoot.Location = new System.Drawing.Point(21, 149);
            this.chkTestMaximDl_CameraShoot.Name = "chkTestMaximDl_CameraShoot";
            this.chkTestMaximDl_CameraShoot.Size = new System.Drawing.Size(202, 24);
            this.chkTestMaximDl_CameraShoot.TabIndex = 0;
            this.chkTestMaximDl_CameraShoot.Text = "MaximDL camera shoot";
            this.chkTestMaximDl_CameraShoot.UseVisualStyleBackColor = true;
            // 
            // chkTestMaximDl_FilterWheel
            // 
            this.chkTestMaximDl_FilterWheel.AutoSize = true;
            this.chkTestMaximDl_FilterWheel.Checked = true;
            this.chkTestMaximDl_FilterWheel.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_FilterWheel.Location = new System.Drawing.Point(21, 179);
            this.chkTestMaximDl_FilterWheel.Name = "chkTestMaximDl_FilterWheel";
            this.chkTestMaximDl_FilterWheel.Size = new System.Drawing.Size(180, 24);
            this.chkTestMaximDl_FilterWheel.TabIndex = 0;
            this.chkTestMaximDl_FilterWheel.Text = "MaximDL filter wheel";
            this.chkTestMaximDl_FilterWheel.UseVisualStyleBackColor = true;
            // 
            // chkTestMaximDl_telescopeconnect
            // 
            this.chkTestMaximDl_telescopeconnect.AutoSize = true;
            this.chkTestMaximDl_telescopeconnect.Checked = true;
            this.chkTestMaximDl_telescopeconnect.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_telescopeconnect.Location = new System.Drawing.Point(21, 89);
            this.chkTestMaximDl_telescopeconnect.Name = "chkTestMaximDl_telescopeconnect";
            this.chkTestMaximDl_telescopeconnect.Size = new System.Drawing.Size(235, 24);
            this.chkTestMaximDl_telescopeconnect.TabIndex = 0;
            this.chkTestMaximDl_telescopeconnect.Text = "MaximDL telescope connect";
            this.chkTestMaximDl_telescopeconnect.UseVisualStyleBackColor = true;
            // 
            // chkTestMaximDl_cameraconnect
            // 
            this.chkTestMaximDl_cameraconnect.AutoSize = true;
            this.chkTestMaximDl_cameraconnect.Checked = true;
            this.chkTestMaximDl_cameraconnect.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_cameraconnect.Location = new System.Drawing.Point(21, 59);
            this.chkTestMaximDl_cameraconnect.Name = "chkTestMaximDl_cameraconnect";
            this.chkTestMaximDl_cameraconnect.Size = new System.Drawing.Size(219, 24);
            this.chkTestMaximDl_cameraconnect.TabIndex = 0;
            this.chkTestMaximDl_cameraconnect.Text = "MaximDL camera connect";
            this.chkTestMaximDl_cameraconnect.UseVisualStyleBackColor = true;
            // 
            // chkTestMaximDl_Run
            // 
            this.chkTestMaximDl_Run.AutoSize = true;
            this.chkTestMaximDl_Run.Checked = true;
            this.chkTestMaximDl_Run.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTestMaximDl_Run.Location = new System.Drawing.Point(21, 29);
            this.chkTestMaximDl_Run.Name = "chkTestMaximDl_Run";
            this.chkTestMaximDl_Run.Size = new System.Drawing.Size(128, 24);
            this.chkTestMaximDl_Run.TabIndex = 0;
            this.chkTestMaximDl_Run.Text = "MaximDL run";
            this.chkTestMaximDl_Run.UseVisualStyleBackColor = true;
            // 
            // btnRunObservatoryTest
            // 
            this.btnRunObservatoryTest.Location = new System.Drawing.Point(12, 670);
            this.btnRunObservatoryTest.Name = "btnRunObservatoryTest";
            this.btnRunObservatoryTest.Size = new System.Drawing.Size(178, 37);
            this.btnRunObservatoryTest.TabIndex = 8;
            this.btnRunObservatoryTest.Text = "Run test";
            this.btnRunObservatoryTest.UseVisualStyleBackColor = true;
            this.btnRunObservatoryTest.Click += new System.EventHandler(this.btnRunObservatoryTest_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 932);
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1258, 37);
            this.progressBar1.TabIndex = 7;
            // 
            // txtTestFormLog
            // 
            this.txtTestFormLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestFormLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTestFormLog.Location = new System.Drawing.Point(392, 12);
            this.txtTestFormLog.Name = "txtTestFormLog";
            this.txtTestFormLog.Size = new System.Drawing.Size(854, 904);
            this.txtTestFormLog.TabIndex = 9;
            this.txtTestFormLog.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 37);
            this.button1.TabIndex = 8;
            this.button1.Text = "Run operation test";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker_test
            // 
            this.backgroundWorker_test.WorkerReportsProgress = true;
            this.backgroundWorker_test.WorkerSupportsCancellation = true;
            this.backgroundWorker_test.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_test_DoWork);
            this.backgroundWorker_test.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_test_ProgressChanged_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTTC_heating);
            this.groupBox1.Controls.Add(this.chkTTC_fan);
            this.groupBox1.Controls.Add(this.chkTTC_run);
            this.groupBox1.Location = new System.Drawing.Point(12, 543);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 121);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Telescope Temp Control";
            // 
            // chkTTC_heating
            // 
            this.chkTTC_heating.AutoSize = true;
            this.chkTTC_heating.Checked = true;
            this.chkTTC_heating.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTTC_heating.Location = new System.Drawing.Point(21, 91);
            this.chkTTC_heating.Name = "chkTTC_heating";
            this.chkTTC_heating.Size = new System.Drawing.Size(146, 24);
            this.chkTTC_heating.TabIndex = 1;
            this.chkTTC_heating.Text = "Heating Control";
            this.chkTTC_heating.UseVisualStyleBackColor = true;
            // 
            // chkTTC_fan
            // 
            this.chkTTC_fan.AutoSize = true;
            this.chkTTC_fan.Checked = true;
            this.chkTTC_fan.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTTC_fan.Location = new System.Drawing.Point(21, 59);
            this.chkTTC_fan.Name = "chkTTC_fan";
            this.chkTTC_fan.Size = new System.Drawing.Size(118, 24);
            this.chkTTC_fan.TabIndex = 0;
            this.chkTTC_fan.Text = "Fan Control";
            this.chkTTC_fan.UseVisualStyleBackColor = true;
            // 
            // chkTTC_run
            // 
            this.chkTTC_run.AutoSize = true;
            this.chkTTC_run.Checked = true;
            this.chkTTC_run.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkTTC_run.Location = new System.Drawing.Point(21, 29);
            this.chkTTC_run.Name = "chkTTC_run";
            this.chkTTC_run.Size = new System.Drawing.Size(91, 24);
            this.chkTTC_run.TabIndex = 0;
            this.chkTTC_run.Text = "TTC run";
            this.chkTTC_run.UseVisualStyleBackColor = true;
            // 
            // TestEquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1258, 969);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTestFormLog);
            this.Controls.Add(this.btnRunObservatoryTest);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.groupBox15);
            this.Controls.Add(this.groupBox13);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.groupBox12);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 1024);
            this.Name = "TestEquipmentForm";
            this.Text = "Test Equipment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestEquipment_FormClosing);
            this.Load += new System.EventHandler(this.TestEquipment_Load);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.CheckBox chkTestCdC_connect;
        private System.Windows.Forms.CheckBox chkTestCdC_run;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.CheckBox chkTestFM_focusermove;
        private System.Windows.Forms.CheckBox chkTestFM_run;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckBox chkTestPHD_guiding;
        private System.Windows.Forms.CheckBox chkTestPHD_connect;
        private System.Windows.Forms.CheckBox chkTestPHD_run;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckBox chkTestMaximDl_setcooling;
        private System.Windows.Forms.CheckBox chkTestMaximDl_CameraShoot;
        private System.Windows.Forms.CheckBox chkTestMaximDl_FilterWheel;
        private System.Windows.Forms.CheckBox chkTestMaximDl_telescopeconnect;
        private System.Windows.Forms.CheckBox chkTestMaximDl_cameraconnect;
        private System.Windows.Forms.CheckBox chkTestMaximDl_Run;
        private System.Windows.Forms.Button btnRunObservatoryTest;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox txtTestFormLog;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker_test;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTTC_heating;
        private System.Windows.Forms.CheckBox chkTTC_fan;
        private System.Windows.Forms.CheckBox chkTTC_run;
    }
}