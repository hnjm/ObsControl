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
            this.txtCielPath = new System.Windows.Forms.TextBox();
            this.txtCCDAPPath = new System.Windows.Forms.TextBox();
            this.txtMaximPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageSwitch = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSwitchDriverId = new System.Windows.Forms.TextBox();
            this.btnConnectSwitchSettings = new System.Windows.Forms.Button();
            this.btnChooseSwitch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSwitchRoofSwitchPort = new System.Windows.Forms.TextBox();
            this.txtSwitchRoofPowerPort = new System.Windows.Forms.TextBox();
            this.txtSwitchFocuserPort = new System.Windows.Forms.TextBox();
            this.txtSwitchCameraPort = new System.Windows.Forms.TextBox();
            this.txtSwitchMountPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageDome = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDomeDriverId = new System.Windows.Forms.TextBox();
            this.btnConnectDomeSettings = new System.Windows.Forms.Button();
            this.btnChooseDome = new System.Windows.Forms.Button();
            this.tabPageTelescope = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTelescopeDriverId = new System.Windows.Forms.TextBox();
            this.btnConnectTelescopeSettings = new System.Windows.Forms.Button();
            this.btnChooseTelescope = new System.Windows.Forms.Button();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.numUDRoofDuration = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPageProgramPath = new System.Windows.Forms.TabPage();
            this.dataGridConfig_programsPath = new System.Windows.Forms.DataGridView();
            this.tabPagescenarioMainParams = new System.Windows.Forms.TabPage();
            this.dataGridConfig_scenarioMainParams = new System.Windows.Forms.DataGridView();
            this.dataGridConfig_programsPath_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_programsPath_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_scenarioMainParams_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_scenarioMainParams_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageSwitch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageDome.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageTelescope.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRoofDuration)).BeginInit();
            this.tabPageProgramPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_programsPath)).BeginInit();
            this.tabPagescenarioMainParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_scenarioMainParams)).BeginInit();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabPageProgramPath);
            this.tabSettings.Controls.Add(this.tabPagescenarioMainParams);
            this.tabSettings.Controls.Add(this.tabPageGeneral);
            this.tabSettings.Controls.Add(this.tabPageSwitch);
            this.tabSettings.Controls.Add(this.tabPageDome);
            this.tabSettings.Controls.Add(this.tabPageTelescope);
            this.tabSettings.Controls.Add(this.tabPageMisc);
            this.tabSettings.Location = new System.Drawing.Point(2, 4);
            this.tabSettings.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(1166, 550);
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
            this.tabPageGeneral.Location = new System.Drawing.Point(8, 39);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageGeneral.Size = new System.Drawing.Size(1150, 503);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // txtCielPath
            // 
            this.txtCielPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "CartesPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCielPath.Location = new System.Drawing.Point(206, 112);
            this.txtCielPath.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCielPath.Name = "txtCielPath";
            this.txtCielPath.Size = new System.Drawing.Size(554, 31);
            this.txtCielPath.TabIndex = 1;
            this.txtCielPath.Text = global::ObservatoryCenter.Properties.Settings.Default.CartesPath;
            // 
            // txtCCDAPPath
            // 
            this.txtCCDAPPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "CCDAPPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCCDAPPath.Location = new System.Drawing.Point(206, 67);
            this.txtCCDAPPath.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCCDAPPath.Name = "txtCCDAPPath";
            this.txtCCDAPPath.Size = new System.Drawing.Size(554, 31);
            this.txtCCDAPPath.TabIndex = 1;
            this.txtCCDAPPath.Text = global::ObservatoryCenter.Properties.Settings.Default.CCDAPPath;
            // 
            // txtMaximPath
            // 
            this.txtMaximPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "MaximDLPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtMaximPath.Location = new System.Drawing.Point(206, 21);
            this.txtMaximPath.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMaximPath.Name = "txtMaximPath";
            this.txtMaximPath.Size = new System.Drawing.Size(554, 31);
            this.txtMaximPath.TabIndex = 1;
            this.txtMaximPath.Text = global::ObservatoryCenter.Properties.Settings.Default.MaximDLPath;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cartes Du Ciel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "CCDAP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maxim DL";
            // 
            // tabPageSwitch
            // 
            this.tabPageSwitch.Controls.Add(this.groupBox2);
            this.tabPageSwitch.Controls.Add(this.groupBox1);
            this.tabPageSwitch.Location = new System.Drawing.Point(8, 39);
            this.tabPageSwitch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageSwitch.Name = "tabPageSwitch";
            this.tabPageSwitch.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageSwitch.Size = new System.Drawing.Size(1150, 503);
            this.tabPageSwitch.TabIndex = 1;
            this.tabPageSwitch.Text = "Switch";
            this.tabPageSwitch.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSwitchDriverId);
            this.groupBox2.Controls.Add(this.btnConnectSwitchSettings);
            this.groupBox2.Controls.Add(this.btnChooseSwitch);
            this.groupBox2.Location = new System.Drawing.Point(14, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(852, 110);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose driver";
            // 
            // txtSwitchDriverId
            // 
            this.txtSwitchDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchDriverId.Location = new System.Drawing.Point(12, 37);
            this.txtSwitchDriverId.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchDriverId.Name = "txtSwitchDriverId";
            this.txtSwitchDriverId.ReadOnly = true;
            this.txtSwitchDriverId.Size = new System.Drawing.Size(498, 31);
            this.txtSwitchDriverId.TabIndex = 9;
            this.txtSwitchDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchDriverId;
            // 
            // btnConnectSwitchSettings
            // 
            this.btnConnectSwitchSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectSwitchSettings.Location = new System.Drawing.Point(696, 33);
            this.btnConnectSwitchSettings.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnectSwitchSettings.Name = "btnConnectSwitchSettings";
            this.btnConnectSwitchSettings.Size = new System.Drawing.Size(144, 44);
            this.btnConnectSwitchSettings.TabIndex = 8;
            this.btnConnectSwitchSettings.Text = "Connect";
            this.btnConnectSwitchSettings.UseVisualStyleBackColor = true;
            this.btnConnectSwitchSettings.Click += new System.EventHandler(this.btnConnectSwitchSettings_Click);
            // 
            // btnChooseSwitch
            // 
            this.btnChooseSwitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseSwitch.Location = new System.Drawing.Point(540, 33);
            this.btnChooseSwitch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnChooseSwitch.Name = "btnChooseSwitch";
            this.btnChooseSwitch.Size = new System.Drawing.Size(144, 44);
            this.btnChooseSwitch.TabIndex = 7;
            this.btnChooseSwitch.Text = "Choose";
            this.btnChooseSwitch.UseVisualStyleBackColor = true;
            this.btnChooseSwitch.Click += new System.EventHandler(this.btnChooseSwitch_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(14, 123);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(374, 279);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Switch";
            // 
            // txtSwitchRoofSwitchPort
            // 
            this.txtSwitchRoofSwitchPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchRoofSwitchPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchRoofSwitchPort.Location = new System.Drawing.Point(192, 210);
            this.txtSwitchRoofSwitchPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchRoofSwitchPort.Name = "txtSwitchRoofSwitchPort";
            this.txtSwitchRoofSwitchPort.Size = new System.Drawing.Size(84, 31);
            this.txtSwitchRoofSwitchPort.TabIndex = 2;
            this.txtSwitchRoofSwitchPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchRoofSwitchPort;
            // 
            // txtSwitchRoofPowerPort
            // 
            this.txtSwitchRoofPowerPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchRoofPowerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchRoofPowerPort.Location = new System.Drawing.Point(190, 165);
            this.txtSwitchRoofPowerPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchRoofPowerPort.Name = "txtSwitchRoofPowerPort";
            this.txtSwitchRoofPowerPort.Size = new System.Drawing.Size(84, 31);
            this.txtSwitchRoofPowerPort.TabIndex = 2;
            this.txtSwitchRoofPowerPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchRoofPowerPort;
            // 
            // txtSwitchFocuserPort
            // 
            this.txtSwitchFocuserPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchFocuserPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchFocuserPort.Location = new System.Drawing.Point(192, 115);
            this.txtSwitchFocuserPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchFocuserPort.Name = "txtSwitchFocuserPort";
            this.txtSwitchFocuserPort.Size = new System.Drawing.Size(84, 31);
            this.txtSwitchFocuserPort.TabIndex = 2;
            this.txtSwitchFocuserPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchFocuserPort;
            // 
            // txtSwitchCameraPort
            // 
            this.txtSwitchCameraPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchCameraPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchCameraPort.Location = new System.Drawing.Point(192, 73);
            this.txtSwitchCameraPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchCameraPort.Name = "txtSwitchCameraPort";
            this.txtSwitchCameraPort.Size = new System.Drawing.Size(84, 31);
            this.txtSwitchCameraPort.TabIndex = 2;
            this.txtSwitchCameraPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchCameraPort;
            // 
            // txtSwitchMountPort
            // 
            this.txtSwitchMountPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchMountPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchMountPort.Location = new System.Drawing.Point(192, 31);
            this.txtSwitchMountPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSwitchMountPort.Name = "txtSwitchMountPort";
            this.txtSwitchMountPort.Size = new System.Drawing.Size(84, 31);
            this.txtSwitchMountPort.TabIndex = 2;
            this.txtSwitchMountPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchMountPort;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mount port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 215);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 25);
            this.label7.TabIndex = 1;
            this.label7.Text = "Roof switch port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 165);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Roof power port";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 121);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "Focuser port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Camera port";
            // 
            // tabPageDome
            // 
            this.tabPageDome.Controls.Add(this.groupBox3);
            this.tabPageDome.Location = new System.Drawing.Point(8, 39);
            this.tabPageDome.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageDome.Name = "tabPageDome";
            this.tabPageDome.Size = new System.Drawing.Size(1150, 503);
            this.tabPageDome.TabIndex = 4;
            this.tabPageDome.Text = "Dome";
            this.tabPageDome.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtDomeDriverId);
            this.groupBox3.Controls.Add(this.btnConnectDomeSettings);
            this.groupBox3.Controls.Add(this.btnChooseDome);
            this.groupBox3.Location = new System.Drawing.Point(14, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(852, 110);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose driver";
            // 
            // txtDomeDriverId
            // 
            this.txtDomeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "DomeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomeDriverId.Location = new System.Drawing.Point(12, 37);
            this.txtDomeDriverId.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDomeDriverId.Name = "txtDomeDriverId";
            this.txtDomeDriverId.ReadOnly = true;
            this.txtDomeDriverId.Size = new System.Drawing.Size(496, 31);
            this.txtDomeDriverId.TabIndex = 9;
            this.txtDomeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.DomeDriverId;
            // 
            // btnConnectDomeSettings
            // 
            this.btnConnectDomeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectDomeSettings.Location = new System.Drawing.Point(696, 33);
            this.btnConnectDomeSettings.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnectDomeSettings.Name = "btnConnectDomeSettings";
            this.btnConnectDomeSettings.Size = new System.Drawing.Size(144, 44);
            this.btnConnectDomeSettings.TabIndex = 7;
            this.btnConnectDomeSettings.Text = "Connect";
            this.btnConnectDomeSettings.UseVisualStyleBackColor = true;
            this.btnConnectDomeSettings.Click += new System.EventHandler(this.btnConnectDomeSettings_Click);
            // 
            // btnChooseDome
            // 
            this.btnChooseDome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseDome.Location = new System.Drawing.Point(540, 33);
            this.btnChooseDome.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnChooseDome.Name = "btnChooseDome";
            this.btnChooseDome.Size = new System.Drawing.Size(144, 44);
            this.btnChooseDome.TabIndex = 6;
            this.btnChooseDome.Text = "Choose";
            this.btnChooseDome.UseVisualStyleBackColor = true;
            this.btnChooseDome.Click += new System.EventHandler(this.btnChooseDome_Click);
            // 
            // tabPageTelescope
            // 
            this.tabPageTelescope.Controls.Add(this.groupBox4);
            this.tabPageTelescope.Location = new System.Drawing.Point(8, 39);
            this.tabPageTelescope.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPageTelescope.Name = "tabPageTelescope";
            this.tabPageTelescope.Size = new System.Drawing.Size(1150, 503);
            this.tabPageTelescope.TabIndex = 3;
            this.tabPageTelescope.Text = "Telescope";
            this.tabPageTelescope.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTelescopeDriverId);
            this.groupBox4.Controls.Add(this.btnConnectTelescopeSettings);
            this.groupBox4.Controls.Add(this.btnChooseTelescope);
            this.groupBox4.Location = new System.Drawing.Point(14, 6);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(852, 110);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose driver";
            // 
            // txtTelescopeDriverId
            // 
            this.txtTelescopeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "TelescopeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTelescopeDriverId.Location = new System.Drawing.Point(12, 37);
            this.txtTelescopeDriverId.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtTelescopeDriverId.Name = "txtTelescopeDriverId";
            this.txtTelescopeDriverId.ReadOnly = true;
            this.txtTelescopeDriverId.Size = new System.Drawing.Size(496, 31);
            this.txtTelescopeDriverId.TabIndex = 9;
            this.txtTelescopeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.TelescopeDriverId;
            // 
            // btnConnectTelescopeSettings
            // 
            this.btnConnectTelescopeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectTelescopeSettings.Location = new System.Drawing.Point(696, 33);
            this.btnConnectTelescopeSettings.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnectTelescopeSettings.Name = "btnConnectTelescopeSettings";
            this.btnConnectTelescopeSettings.Size = new System.Drawing.Size(144, 44);
            this.btnConnectTelescopeSettings.TabIndex = 7;
            this.btnConnectTelescopeSettings.Text = "Connect";
            this.btnConnectTelescopeSettings.UseVisualStyleBackColor = true;
            this.btnConnectTelescopeSettings.Click += new System.EventHandler(this.btnConnectTelescopeSettings_Click);
            // 
            // btnChooseTelescope
            // 
            this.btnChooseTelescope.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseTelescope.Location = new System.Drawing.Point(540, 33);
            this.btnChooseTelescope.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnChooseTelescope.Name = "btnChooseTelescope";
            this.btnChooseTelescope.Size = new System.Drawing.Size(144, 44);
            this.btnChooseTelescope.TabIndex = 6;
            this.btnChooseTelescope.Text = "Choose";
            this.btnChooseTelescope.UseVisualStyleBackColor = true;
            this.btnChooseTelescope.Click += new System.EventHandler(this.btnChooseTelescope_Click);
            // 
            // tabPageMisc
            // 
            this.tabPageMisc.Controls.Add(this.numUDRoofDuration);
            this.tabPageMisc.Controls.Add(this.label10);
            this.tabPageMisc.Controls.Add(this.label9);
            this.tabPageMisc.Location = new System.Drawing.Point(8, 39);
            this.tabPageMisc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageMisc.Size = new System.Drawing.Size(1150, 503);
            this.tabPageMisc.TabIndex = 2;
            this.tabPageMisc.Text = "Misc";
            this.tabPageMisc.UseVisualStyleBackColor = true;
            // 
            // numUDRoofDuration
            // 
            this.numUDRoofDuration.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ObservatoryCenter.Properties.Settings.Default, "RoofDuration", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numUDRoofDuration.Location = new System.Drawing.Point(228, 17);
            this.numUDRoofDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numUDRoofDuration.Name = "numUDRoofDuration";
            this.numUDRoofDuration.Size = new System.Drawing.Size(116, 31);
            this.numUDRoofDuration.TabIndex = 1;
            this.numUDRoofDuration.Value = global::ObservatoryCenter.Properties.Settings.Default.RoofDuration;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(354, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "sec";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Open/Close duration";
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreDefaults.Location = new System.Drawing.Point(10, 568);
            this.btnRestoreDefaults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(150, 44);
            this.btnRestoreDefaults.TabIndex = 1;
            this.btnRestoreDefaults.Text = "Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(822, 568);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 44);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1018, 568);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 44);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPageProgramPath
            // 
            this.tabPageProgramPath.Controls.Add(this.dataGridConfig_programsPath);
            this.tabPageProgramPath.Location = new System.Drawing.Point(8, 39);
            this.tabPageProgramPath.Name = "tabPageProgramPath";
            this.tabPageProgramPath.Size = new System.Drawing.Size(1150, 503);
            this.tabPageProgramPath.TabIndex = 5;
            this.tabPageProgramPath.Text = "Programs Path";
            this.tabPageProgramPath.UseVisualStyleBackColor = true;
            // 
            // dataGridConfig_programsPath
            // 
            this.dataGridConfig_programsPath.AllowUserToAddRows = false;
            this.dataGridConfig_programsPath.AllowUserToDeleteRows = false;
            this.dataGridConfig_programsPath.AllowUserToResizeRows = false;
            this.dataGridConfig_programsPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridConfig_programsPath.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridConfig_programsPath.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridConfig_programsPath_name,
            this.dataGridConfig_programsPath_value});
            this.dataGridConfig_programsPath.Location = new System.Drawing.Point(6, 6);
            this.dataGridConfig_programsPath.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridConfig_programsPath.Name = "dataGridConfig_programsPath";
            this.dataGridConfig_programsPath.RowHeadersVisible = false;
            this.dataGridConfig_programsPath.RowHeadersWidth = 10;
            this.dataGridConfig_programsPath.Size = new System.Drawing.Size(1139, 491);
            this.dataGridConfig_programsPath.TabIndex = 3;
            // 
            // tabPagescenarioMainParams
            // 
            this.tabPagescenarioMainParams.Controls.Add(this.dataGridConfig_scenarioMainParams);
            this.tabPagescenarioMainParams.Location = new System.Drawing.Point(8, 39);
            this.tabPagescenarioMainParams.Name = "tabPagescenarioMainParams";
            this.tabPagescenarioMainParams.Size = new System.Drawing.Size(1150, 503);
            this.tabPagescenarioMainParams.TabIndex = 6;
            this.tabPagescenarioMainParams.Text = "Scenario Main Params";
            this.tabPagescenarioMainParams.UseVisualStyleBackColor = true;
            // 
            // dataGridConfig_scenarioMainParams
            // 
            this.dataGridConfig_scenarioMainParams.AllowUserToAddRows = false;
            this.dataGridConfig_scenarioMainParams.AllowUserToDeleteRows = false;
            this.dataGridConfig_scenarioMainParams.AllowUserToResizeRows = false;
            this.dataGridConfig_scenarioMainParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridConfig_scenarioMainParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridConfig_scenarioMainParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridConfig_scenarioMainParams_name,
            this.dataGridConfig_scenarioMainParams_value});
            this.dataGridConfig_scenarioMainParams.Location = new System.Drawing.Point(6, 6);
            this.dataGridConfig_scenarioMainParams.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridConfig_scenarioMainParams.Name = "dataGridConfig_scenarioMainParams";
            this.dataGridConfig_scenarioMainParams.RowHeadersVisible = false;
            this.dataGridConfig_scenarioMainParams.RowHeadersWidth = 10;
            this.dataGridConfig_scenarioMainParams.Size = new System.Drawing.Size(1139, 491);
            this.dataGridConfig_scenarioMainParams.TabIndex = 4;
            // 
            // dataGridConfig_programsPath_name
            // 
            this.dataGridConfig_programsPath_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridConfig_programsPath_name.FillWeight = 200F;
            this.dataGridConfig_programsPath_name.HeaderText = "Name";
            this.dataGridConfig_programsPath_name.MinimumWidth = 100;
            this.dataGridConfig_programsPath_name.Name = "dataGridConfig_programsPath_name";
            this.dataGridConfig_programsPath_name.ReadOnly = true;
            this.dataGridConfig_programsPath_name.Width = 113;
            // 
            // dataGridConfig_programsPath_value
            // 
            this.dataGridConfig_programsPath_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridConfig_programsPath_value.HeaderText = "Value";
            this.dataGridConfig_programsPath_value.MinimumWidth = 300;
            this.dataGridConfig_programsPath_value.Name = "dataGridConfig_programsPath_value";
            // 
            // dataGridConfig_scenarioMainParams_name
            // 
            this.dataGridConfig_scenarioMainParams_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridConfig_scenarioMainParams_name.FillWeight = 200F;
            this.dataGridConfig_scenarioMainParams_name.HeaderText = "Name";
            this.dataGridConfig_scenarioMainParams_name.MinimumWidth = 100;
            this.dataGridConfig_scenarioMainParams_name.Name = "dataGridConfig_scenarioMainParams_name";
            this.dataGridConfig_scenarioMainParams_name.ReadOnly = true;
            this.dataGridConfig_scenarioMainParams_name.Width = 113;
            // 
            // dataGridConfig_scenarioMainParams_value
            // 
            this.dataGridConfig_scenarioMainParams_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridConfig_scenarioMainParams_value.HeaderText = "Value";
            this.dataGridConfig_scenarioMainParams_value.MinimumWidth = 300;
            this.dataGridConfig_scenarioMainParams_value.Name = "dataGridConfig_scenarioMainParams_value";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1174, 629);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageDome.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageTelescope.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPageMisc.ResumeLayout(false);
            this.tabPageMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRoofDuration)).EndInit();
            this.tabPageProgramPath.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_programsPath)).EndInit();
            this.tabPagescenarioMainParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_scenarioMainParams)).EndInit();
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
        private System.Windows.Forms.TabPage tabPageProgramPath;
        private System.Windows.Forms.DataGridView dataGridConfig_programsPath;
        private System.Windows.Forms.TabPage tabPagescenarioMainParams;
        private System.Windows.Forms.DataGridView dataGridConfig_scenarioMainParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_programsPath_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_programsPath_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_scenarioMainParams_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_scenarioMainParams_value;
    }
}