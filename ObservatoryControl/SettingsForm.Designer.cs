namespace ObservatoryCenter
{
    partial class SettingsForm
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
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageSwitch = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtCielPath = new System.Windows.Forms.TextBox();
            this.txtCCDAPPath = new System.Windows.Forms.TextBox();
            this.txtMaximPath = new System.Windows.Forms.TextBox();
            this.txtSwitchRoofSwitchPort = new System.Windows.Forms.TextBox();
            this.txtSwitchRoofPowerPort = new System.Windows.Forms.TextBox();
            this.txtSwitchFocuserPort = new System.Windows.Forms.TextBox();
            this.txtSwitchCameraPort = new System.Windows.Forms.TextBox();
            this.txtSwitchMountPort = new System.Windows.Forms.TextBox();
            this.numUDRoofDuration = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConnectSwitchSettings = new System.Windows.Forms.Button();
            this.btnChooseSwitch = new System.Windows.Forms.Button();
            this.tabPageTelescope = new System.Windows.Forms.TabPage();
            this.tabPageDome = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnConnectDomeSettings = new System.Windows.Forms.Button();
            this.btnChooseDome = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConnectTelescopeSettings = new System.Windows.Forms.Button();
            this.btnChooseTelescope = new System.Windows.Forms.Button();
            this.txtDomeDriverId = new System.Windows.Forms.TextBox();
            this.txtSwitchDriverId = new System.Windows.Forms.TextBox();
            this.txtTelescopeDriverId = new System.Windows.Forms.TextBox();
            this.tabSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageSwitch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRoofDuration)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPageTelescope.SuspendLayout();
            this.tabPageDome.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabPageGeneral);
            this.tabSettings.Controls.Add(this.tabPageSwitch);
            this.tabSettings.Controls.Add(this.tabPageDome);
            this.tabSettings.Controls.Add(this.tabPageTelescope);
            this.tabSettings.Controls.Add(this.tabPageMisc);
            this.tabSettings.Location = new System.Drawing.Point(1, 2);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(447, 321);
            this.tabSettings.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.txtCielPath);
            this.tabPageGeneral.Controls.Add(this.txtCCDAPPath);
            this.tabPageGeneral.Controls.Add(this.txtMaximPath);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Controls.Add(this.label1);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageGeneral.Size = new System.Drawing.Size(439, 295);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cartes Du Ciel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "CCDAP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maxim DL";
            // 
            // tabPageSwitch
            // 
            this.tabPageSwitch.Controls.Add(this.groupBox2);
            this.tabPageSwitch.Controls.Add(this.groupBox1);
            this.tabPageSwitch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSwitch.Name = "tabPageSwitch";
            this.tabPageSwitch.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageSwitch.Size = new System.Drawing.Size(439, 295);
            this.tabPageSwitch.TabIndex = 1;
            this.tabPageSwitch.Text = "Switch";
            this.tabPageSwitch.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSwitchRoofSwitchPort);
            this.groupBox1.Controls.Add(this.txtSwitchRoofPowerPort);
            this.groupBox1.Controls.Add(this.txtSwitchFocuserPort);
            this.groupBox1.Controls.Add(this.txtSwitchCameraPort);
            this.groupBox1.Controls.Add(this.txtSwitchMountPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(7, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Switch";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mount port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Roof switch port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Roof power port";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Focuser port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Camera port";
            // 
            // tabPageMisc
            // 
            this.tabPageMisc.Controls.Add(this.numUDRoofDuration);
            this.tabPageMisc.Controls.Add(this.label10);
            this.tabPageMisc.Controls.Add(this.label9);
            this.tabPageMisc.Location = new System.Drawing.Point(4, 22);
            this.tabPageMisc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageMisc.Size = new System.Drawing.Size(439, 295);
            this.tabPageMisc.TabIndex = 2;
            this.tabPageMisc.Text = "Misc";
            this.tabPageMisc.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(177, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "sec";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Open/Close duration";
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreDefaults.Location = new System.Drawing.Point(5, 330);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreDefaults.TabIndex = 1;
            this.btnRestoreDefaults.Text = "Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(275, 330);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtCielPath
            // 
            this.txtCielPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "CartesPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCielPath.Location = new System.Drawing.Point(103, 59);
            this.txtCielPath.Name = "txtCielPath";
            this.txtCielPath.Size = new System.Drawing.Size(279, 20);
            this.txtCielPath.TabIndex = 1;
            this.txtCielPath.Text = global::ObservatoryCenter.Properties.Settings.Default.CartesPath;
            // 
            // txtCCDAPPath
            // 
            this.txtCCDAPPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "CCDAPPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCCDAPPath.Location = new System.Drawing.Point(103, 33);
            this.txtCCDAPPath.Name = "txtCCDAPPath";
            this.txtCCDAPPath.Size = new System.Drawing.Size(279, 20);
            this.txtCCDAPPath.TabIndex = 1;
            this.txtCCDAPPath.Text = global::ObservatoryCenter.Properties.Settings.Default.CCDAPPath;
            // 
            // txtMaximPath
            // 
            this.txtMaximPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "MaximDLPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtMaximPath.Location = new System.Drawing.Point(103, 11);
            this.txtMaximPath.Name = "txtMaximPath";
            this.txtMaximPath.Size = new System.Drawing.Size(279, 20);
            this.txtMaximPath.TabIndex = 1;
            this.txtMaximPath.Text = global::ObservatoryCenter.Properties.Settings.Default.MaximDLPath;
            // 
            // txtSwitchRoofSwitchPort
            // 
            this.txtSwitchRoofSwitchPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "txtSwitchRoofSwitchPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchRoofSwitchPort.Location = new System.Drawing.Point(96, 109);
            this.txtSwitchRoofSwitchPort.Name = "txtSwitchRoofSwitchPort";
            this.txtSwitchRoofSwitchPort.Size = new System.Drawing.Size(44, 20);
            this.txtSwitchRoofSwitchPort.TabIndex = 2;
            this.txtSwitchRoofSwitchPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchRoofSwitchPort;
            // 
            // txtSwitchRoofPowerPort
            // 
            this.txtSwitchRoofPowerPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchRoofPowerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchRoofPowerPort.Location = new System.Drawing.Point(95, 86);
            this.txtSwitchRoofPowerPort.Name = "txtSwitchRoofPowerPort";
            this.txtSwitchRoofPowerPort.Size = new System.Drawing.Size(44, 20);
            this.txtSwitchRoofPowerPort.TabIndex = 2;
            this.txtSwitchRoofPowerPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchRoofPowerPort;
            // 
            // txtSwitchFocuserPort
            // 
            this.txtSwitchFocuserPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchFocuserPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchFocuserPort.Location = new System.Drawing.Point(96, 60);
            this.txtSwitchFocuserPort.Name = "txtSwitchFocuserPort";
            this.txtSwitchFocuserPort.Size = new System.Drawing.Size(44, 20);
            this.txtSwitchFocuserPort.TabIndex = 2;
            this.txtSwitchFocuserPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchFocuserPort;
            // 
            // txtSwitchCameraPort
            // 
            this.txtSwitchCameraPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchCameraPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchCameraPort.Location = new System.Drawing.Point(96, 38);
            this.txtSwitchCameraPort.Name = "txtSwitchCameraPort";
            this.txtSwitchCameraPort.Size = new System.Drawing.Size(44, 20);
            this.txtSwitchCameraPort.TabIndex = 2;
            this.txtSwitchCameraPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchCameraPort;
            // 
            // txtSwitchMountPort
            // 
            this.txtSwitchMountPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchMountPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchMountPort.Location = new System.Drawing.Point(96, 16);
            this.txtSwitchMountPort.Name = "txtSwitchMountPort";
            this.txtSwitchMountPort.Size = new System.Drawing.Size(44, 20);
            this.txtSwitchMountPort.TabIndex = 2;
            this.txtSwitchMountPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchMountPort;
            // 
            // numUDRoofDuration
            // 
            this.numUDRoofDuration.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ObservatoryCenter.Properties.Settings.Default, "RoofDuration", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numUDRoofDuration.Location = new System.Drawing.Point(114, 9);
            this.numUDRoofDuration.Margin = new System.Windows.Forms.Padding(2);
            this.numUDRoofDuration.Name = "numUDRoofDuration";
            this.numUDRoofDuration.Size = new System.Drawing.Size(58, 20);
            this.numUDRoofDuration.TabIndex = 1;
            this.numUDRoofDuration.Value = global::ObservatoryCenter.Properties.Settings.Default.RoofDuration;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSwitchDriverId);
            this.groupBox2.Controls.Add(this.btnConnectSwitchSettings);
            this.groupBox2.Controls.Add(this.btnChooseSwitch);
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 57);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose driver";
            // 
            // btnConnectSwitchSettings
            // 
            this.btnConnectSwitchSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectSwitchSettings.Location = new System.Drawing.Point(348, 17);
            this.btnConnectSwitchSettings.Name = "btnConnectSwitchSettings";
            this.btnConnectSwitchSettings.Size = new System.Drawing.Size(72, 23);
            this.btnConnectSwitchSettings.TabIndex = 8;
            this.btnConnectSwitchSettings.Text = "Connect";
            this.btnConnectSwitchSettings.UseVisualStyleBackColor = true;
            // 
            // btnChooseSwitch
            // 
            this.btnChooseSwitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseSwitch.Location = new System.Drawing.Point(270, 17);
            this.btnChooseSwitch.Name = "btnChooseSwitch";
            this.btnChooseSwitch.Size = new System.Drawing.Size(72, 23);
            this.btnChooseSwitch.TabIndex = 7;
            this.btnChooseSwitch.Text = "Choose";
            this.btnChooseSwitch.UseVisualStyleBackColor = true;
            this.btnChooseSwitch.Click += new System.EventHandler(this.btnChooseSwitch_Click);
            // 
            // tabPageTelescope
            // 
            this.tabPageTelescope.Controls.Add(this.groupBox4);
            this.tabPageTelescope.Location = new System.Drawing.Point(4, 22);
            this.tabPageTelescope.Name = "tabPageTelescope";
            this.tabPageTelescope.Size = new System.Drawing.Size(439, 295);
            this.tabPageTelescope.TabIndex = 3;
            this.tabPageTelescope.Text = "Telescope";
            this.tabPageTelescope.UseVisualStyleBackColor = true;
            // 
            // tabPageDome
            // 
            this.tabPageDome.Controls.Add(this.groupBox3);
            this.tabPageDome.Location = new System.Drawing.Point(4, 22);
            this.tabPageDome.Name = "tabPageDome";
            this.tabPageDome.Size = new System.Drawing.Size(439, 295);
            this.tabPageDome.TabIndex = 4;
            this.tabPageDome.Text = "Dome";
            this.tabPageDome.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtDomeDriverId);
            this.groupBox3.Controls.Add(this.btnConnectDomeSettings);
            this.groupBox3.Controls.Add(this.btnChooseDome);
            this.groupBox3.Location = new System.Drawing.Point(7, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(426, 57);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose driver";
            // 
            // btnConnectDomeSettings
            // 
            this.btnConnectDomeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectDomeSettings.Location = new System.Drawing.Point(348, 17);
            this.btnConnectDomeSettings.Name = "btnConnectDomeSettings";
            this.btnConnectDomeSettings.Size = new System.Drawing.Size(72, 23);
            this.btnConnectDomeSettings.TabIndex = 7;
            this.btnConnectDomeSettings.Text = "Connect";
            this.btnConnectDomeSettings.UseVisualStyleBackColor = true;
            // 
            // btnChooseDome
            // 
            this.btnChooseDome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseDome.Location = new System.Drawing.Point(270, 17);
            this.btnChooseDome.Name = "btnChooseDome";
            this.btnChooseDome.Size = new System.Drawing.Size(72, 23);
            this.btnChooseDome.TabIndex = 6;
            this.btnChooseDome.Text = "Choose";
            this.btnChooseDome.UseVisualStyleBackColor = true;
            this.btnChooseDome.Click += new System.EventHandler(this.btnChooseDome_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTelescopeDriverId);
            this.groupBox4.Controls.Add(this.btnConnectTelescopeSettings);
            this.groupBox4.Controls.Add(this.btnChooseTelescope);
            this.groupBox4.Location = new System.Drawing.Point(7, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(426, 57);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose driver";
            // 
            // btnConnectTelescopeSettings
            // 
            this.btnConnectTelescopeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectTelescopeSettings.Location = new System.Drawing.Point(348, 17);
            this.btnConnectTelescopeSettings.Name = "btnConnectTelescopeSettings";
            this.btnConnectTelescopeSettings.Size = new System.Drawing.Size(72, 23);
            this.btnConnectTelescopeSettings.TabIndex = 7;
            this.btnConnectTelescopeSettings.Text = "Connect";
            this.btnConnectTelescopeSettings.UseVisualStyleBackColor = true;
            // 
            // btnChooseTelescope
            // 
            this.btnChooseTelescope.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseTelescope.Location = new System.Drawing.Point(270, 17);
            this.btnChooseTelescope.Name = "btnChooseTelescope";
            this.btnChooseTelescope.Size = new System.Drawing.Size(72, 23);
            this.btnChooseTelescope.TabIndex = 6;
            this.btnChooseTelescope.Text = "Choose";
            this.btnChooseTelescope.UseVisualStyleBackColor = true;
            this.btnChooseTelescope.Click += new System.EventHandler(this.btnChooseTelescope_Click);
            // 
            // txtDomeDriverId
            // 
            this.txtDomeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "DomeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomeDriverId.Location = new System.Drawing.Point(6, 19);
            this.txtDomeDriverId.Name = "txtDomeDriverId";
            this.txtDomeDriverId.ReadOnly = true;
            this.txtDomeDriverId.Size = new System.Drawing.Size(250, 20);
            this.txtDomeDriverId.TabIndex = 9;
            this.txtDomeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.DomeDriverId;
            // 
            // txtSwitchDriverId
            // 
            this.txtSwitchDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchDriverId.Location = new System.Drawing.Point(6, 19);
            this.txtSwitchDriverId.Name = "txtSwitchDriverId";
            this.txtSwitchDriverId.ReadOnly = true;
            this.txtSwitchDriverId.Size = new System.Drawing.Size(251, 20);
            this.txtSwitchDriverId.TabIndex = 9;
            this.txtSwitchDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchDriverId;
            // 
            // txtTelescopeDriverId
            // 
            this.txtTelescopeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "TelescopeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTelescopeDriverId.Location = new System.Drawing.Point(6, 19);
            this.txtTelescopeDriverId.Name = "txtTelescopeDriverId";
            this.txtTelescopeDriverId.ReadOnly = true;
            this.txtTelescopeDriverId.Size = new System.Drawing.Size(250, 20);
            this.txtTelescopeDriverId.TabIndex = 9;
            this.txtTelescopeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.TelescopeDriverId;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(451, 362);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageSwitch.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageMisc.ResumeLayout(false);
            this.tabPageMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRoofDuration)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageTelescope.ResumeLayout(false);
            this.tabPageDome.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TextBox txtMaximPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageSwitch;
        private System.Windows.Forms.TextBox txtCCDAPPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCielPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSwitchMountPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSwitchCameraPort;
        private System.Windows.Forms.TextBox txtSwitchRoofPowerPort;
        private System.Windows.Forms.TextBox txtSwitchRoofSwitchPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSwitchFocuserPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPageMisc;
        private System.Windows.Forms.NumericUpDown numUDRoofDuration;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConnectSwitchSettings;
        private System.Windows.Forms.Button btnChooseSwitch;
        private System.Windows.Forms.TabPage tabPageDome;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPageTelescope;
        private System.Windows.Forms.Button btnConnectDomeSettings;
        private System.Windows.Forms.Button btnChooseDome;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnConnectTelescopeSettings;
        private System.Windows.Forms.Button btnChooseTelescope;
        private System.Windows.Forms.TextBox txtSwitchDriverId;
        private System.Windows.Forms.TextBox txtDomeDriverId;
        private System.Windows.Forms.TextBox txtTelescopeDriverId;
    }
}