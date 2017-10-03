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
            this.tabPageProgramPath = new System.Windows.Forms.TabPage();
            this.dataGridConfig_programsPath = new System.Windows.Forms.DataGridView();
            this.dataGridConfig_programsPath_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_programsPath_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPagescenarioMainParams = new System.Windows.Forms.TabPage();
            this.dataGridConfig_scenarioMain = new System.Windows.Forms.DataGridView();
            this.dataGridConfig_scenarioMain_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_scenarioMain_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridConfig_scenarioMain_run = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridConfig_scenarioMain_argument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLatitudeSec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLatitudeMin = new System.Windows.Forms.TextBox();
            this.txtLongtitudeSec = new System.Windows.Forms.TextBox();
            this.txtLatitudeGrad = new System.Windows.Forms.TextBox();
            this.txtLongtitudeMin = new System.Windows.Forms.TextBox();
            this.txtLongtitudeGrad = new System.Windows.Forms.TextBox();
            this.tabPageSwitch = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSwitchDriverId = new System.Windows.Forms.TextBox();
            this.btnConnectSwitchSettings = new System.Windows.Forms.Button();
            this.btnChooseSwitch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSwitchCameraPort = new System.Windows.Forms.TextBox();
            this.txtSwitchMountPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.tabSettings.SuspendLayout();
            this.tabPageProgramPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_programsPath)).BeginInit();
            this.tabPagescenarioMainParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_scenarioMain)).BeginInit();
            this.tabPageGeneral.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPageSwitch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageDome.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageTelescope.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRoofDuration)).BeginInit();
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
            this.tabSettings.Location = new System.Drawing.Point(2, 3);
            this.tabSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(874, 440);
            this.tabSettings.TabIndex = 0;
            // 
            // tabPageProgramPath
            // 
            this.tabPageProgramPath.Controls.Add(this.dataGridConfig_programsPath);
            this.tabPageProgramPath.Location = new System.Drawing.Point(8, 39);
            this.tabPageProgramPath.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageProgramPath.Name = "tabPageProgramPath";
            this.tabPageProgramPath.Size = new System.Drawing.Size(858, 393);
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
            this.dataGridConfig_programsPath.Location = new System.Drawing.Point(4, 5);
            this.dataGridConfig_programsPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridConfig_programsPath.Name = "dataGridConfig_programsPath";
            this.dataGridConfig_programsPath.RowHeadersVisible = false;
            this.dataGridConfig_programsPath.RowHeadersWidth = 10;
            this.dataGridConfig_programsPath.Size = new System.Drawing.Size(846, 379);
            this.dataGridConfig_programsPath.TabIndex = 3;
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
            // tabPagescenarioMainParams
            // 
            this.tabPagescenarioMainParams.Controls.Add(this.dataGridConfig_scenarioMain);
            this.tabPagescenarioMainParams.Location = new System.Drawing.Point(8, 39);
            this.tabPagescenarioMainParams.Margin = new System.Windows.Forms.Padding(2);
            this.tabPagescenarioMainParams.Name = "tabPagescenarioMainParams";
            this.tabPagescenarioMainParams.Size = new System.Drawing.Size(858, 393);
            this.tabPagescenarioMainParams.TabIndex = 6;
            this.tabPagescenarioMainParams.Text = "Scenario Main";
            this.tabPagescenarioMainParams.UseVisualStyleBackColor = true;
            // 
            // dataGridConfig_scenarioMain
            // 
            this.dataGridConfig_scenarioMain.AllowUserToAddRows = false;
            this.dataGridConfig_scenarioMain.AllowUserToDeleteRows = false;
            this.dataGridConfig_scenarioMain.AllowUserToResizeRows = false;
            this.dataGridConfig_scenarioMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridConfig_scenarioMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridConfig_scenarioMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridConfig_scenarioMain_name,
            this.dataGridConfig_scenarioMain_type,
            this.dataGridConfig_scenarioMain_run,
            this.dataGridConfig_scenarioMain_argument});
            this.dataGridConfig_scenarioMain.Location = new System.Drawing.Point(4, 5);
            this.dataGridConfig_scenarioMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridConfig_scenarioMain.Name = "dataGridConfig_scenarioMain";
            this.dataGridConfig_scenarioMain.RowHeadersVisible = false;
            this.dataGridConfig_scenarioMain.RowHeadersWidth = 10;
            this.dataGridConfig_scenarioMain.Size = new System.Drawing.Size(846, 379);
            this.dataGridConfig_scenarioMain.TabIndex = 4;
            // 
            // dataGridConfig_scenarioMain_name
            // 
            this.dataGridConfig_scenarioMain_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridConfig_scenarioMain_name.FillWeight = 200F;
            this.dataGridConfig_scenarioMain_name.HeaderText = "Name";
            this.dataGridConfig_scenarioMain_name.MinimumWidth = 100;
            this.dataGridConfig_scenarioMain_name.Name = "dataGridConfig_scenarioMain_name";
            this.dataGridConfig_scenarioMain_name.ReadOnly = true;
            // 
            // dataGridConfig_scenarioMain_type
            // 
            this.dataGridConfig_scenarioMain_type.HeaderText = "Type";
            this.dataGridConfig_scenarioMain_type.Name = "dataGridConfig_scenarioMain_type";
            // 
            // dataGridConfig_scenarioMain_run
            // 
            this.dataGridConfig_scenarioMain_run.HeaderText = "Run";
            this.dataGridConfig_scenarioMain_run.Name = "dataGridConfig_scenarioMain_run";
            // 
            // dataGridConfig_scenarioMain_argument
            // 
            this.dataGridConfig_scenarioMain_argument.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridConfig_scenarioMain_argument.HeaderText = "Argument";
            this.dataGridConfig_scenarioMain_argument.MinimumWidth = 100;
            this.dataGridConfig_scenarioMain_argument.Name = "dataGridConfig_scenarioMain_argument";
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBox5);
            this.tabPageGeneral.Location = new System.Drawing.Point(8, 39);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageGeneral.Size = new System.Drawing.Size(858, 393);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtLatitudeSec);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtLatitudeMin);
            this.groupBox5.Controls.Add(this.txtLongtitudeSec);
            this.groupBox5.Controls.Add(this.txtLatitudeGrad);
            this.groupBox5.Controls.Add(this.txtLongtitudeMin);
            this.groupBox5.Controls.Add(this.txtLongtitudeGrad);
            this.groupBox5.Location = new System.Drawing.Point(7, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(488, 233);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Latitude";
            // 
            // txtLatitudeSec
            // 
            this.txtLatitudeSec.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LatSec", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLatitudeSec.Location = new System.Drawing.Point(244, 64);
            this.txtLatitudeSec.Name = "txtLatitudeSec";
            this.txtLatitudeSec.Size = new System.Drawing.Size(57, 31);
            this.txtLatitudeSec.TabIndex = 0;
            this.txtLatitudeSec.Text = global::ObservatoryCenter.Properties.Settings.Default.LatSec;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Longtitude";
            // 
            // txtLatitudeMin
            // 
            this.txtLatitudeMin.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LatMin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLatitudeMin.Location = new System.Drawing.Point(170, 64);
            this.txtLatitudeMin.Name = "txtLatitudeMin";
            this.txtLatitudeMin.Size = new System.Drawing.Size(57, 31);
            this.txtLatitudeMin.TabIndex = 0;
            this.txtLatitudeMin.Text = global::ObservatoryCenter.Properties.Settings.Default.LatMin;
            // 
            // txtLongtitudeSec
            // 
            this.txtLongtitudeSec.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LongSec", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLongtitudeSec.Location = new System.Drawing.Point(244, 28);
            this.txtLongtitudeSec.Name = "txtLongtitudeSec";
            this.txtLongtitudeSec.Size = new System.Drawing.Size(57, 31);
            this.txtLongtitudeSec.TabIndex = 0;
            this.txtLongtitudeSec.Text = global::ObservatoryCenter.Properties.Settings.Default.LongSec;
            // 
            // txtLatitudeGrad
            // 
            this.txtLatitudeGrad.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LatGrad", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLatitudeGrad.Location = new System.Drawing.Point(97, 64);
            this.txtLatitudeGrad.Name = "txtLatitudeGrad";
            this.txtLatitudeGrad.Size = new System.Drawing.Size(57, 31);
            this.txtLatitudeGrad.TabIndex = 0;
            this.txtLatitudeGrad.Text = global::ObservatoryCenter.Properties.Settings.Default.LatGrad;
            // 
            // txtLongtitudeMin
            // 
            this.txtLongtitudeMin.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LongMin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLongtitudeMin.Location = new System.Drawing.Point(170, 28);
            this.txtLongtitudeMin.Name = "txtLongtitudeMin";
            this.txtLongtitudeMin.Size = new System.Drawing.Size(57, 31);
            this.txtLongtitudeMin.TabIndex = 0;
            this.txtLongtitudeMin.Text = global::ObservatoryCenter.Properties.Settings.Default.LongMin;
            // 
            // txtLongtitudeGrad
            // 
            this.txtLongtitudeGrad.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "LongGrad", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLongtitudeGrad.Location = new System.Drawing.Point(97, 28);
            this.txtLongtitudeGrad.Name = "txtLongtitudeGrad";
            this.txtLongtitudeGrad.Size = new System.Drawing.Size(57, 31);
            this.txtLongtitudeGrad.TabIndex = 0;
            this.txtLongtitudeGrad.Text = global::ObservatoryCenter.Properties.Settings.Default.LongGrad;
            // 
            // tabPageSwitch
            // 
            this.tabPageSwitch.Controls.Add(this.groupBox2);
            this.tabPageSwitch.Controls.Add(this.groupBox1);
            this.tabPageSwitch.Location = new System.Drawing.Point(8, 39);
            this.tabPageSwitch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSwitch.Name = "tabPageSwitch";
            this.tabPageSwitch.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSwitch.Size = new System.Drawing.Size(858, 393);
            this.tabPageSwitch.TabIndex = 1;
            this.tabPageSwitch.Text = "Switch";
            this.tabPageSwitch.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSwitchDriverId);
            this.groupBox2.Controls.Add(this.btnConnectSwitchSettings);
            this.groupBox2.Controls.Add(this.btnChooseSwitch);
            this.groupBox2.Location = new System.Drawing.Point(10, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(639, 88);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose driver";
            // 
            // txtSwitchDriverId
            // 
            this.txtSwitchDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchDriverId.Location = new System.Drawing.Point(9, 30);
            this.txtSwitchDriverId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSwitchDriverId.Name = "txtSwitchDriverId";
            this.txtSwitchDriverId.ReadOnly = true;
            this.txtSwitchDriverId.Size = new System.Drawing.Size(374, 31);
            this.txtSwitchDriverId.TabIndex = 9;
            this.txtSwitchDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchDriverId;
            // 
            // btnConnectSwitchSettings
            // 
            this.btnConnectSwitchSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectSwitchSettings.Location = new System.Drawing.Point(522, 26);
            this.btnConnectSwitchSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectSwitchSettings.Name = "btnConnectSwitchSettings";
            this.btnConnectSwitchSettings.Size = new System.Drawing.Size(108, 35);
            this.btnConnectSwitchSettings.TabIndex = 8;
            this.btnConnectSwitchSettings.Text = "Connect";
            this.btnConnectSwitchSettings.UseVisualStyleBackColor = true;
            this.btnConnectSwitchSettings.Click += new System.EventHandler(this.btnConnectSwitchSettings_Click);
            // 
            // btnChooseSwitch
            // 
            this.btnChooseSwitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseSwitch.Location = new System.Drawing.Point(405, 26);
            this.btnChooseSwitch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChooseSwitch.Name = "btnChooseSwitch";
            this.btnChooseSwitch.Size = new System.Drawing.Size(108, 35);
            this.btnChooseSwitch.TabIndex = 7;
            this.btnChooseSwitch.Text = "Choose";
            this.btnChooseSwitch.UseVisualStyleBackColor = true;
            this.btnChooseSwitch.Click += new System.EventHandler(this.btnChooseSwitch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSwitchCameraPort);
            this.groupBox1.Controls.Add(this.txtSwitchMountPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(10, 98);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(280, 223);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Switch";
            // 
            // txtSwitchCameraPort
            // 
            this.txtSwitchCameraPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchCameraPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchCameraPort.Location = new System.Drawing.Point(144, 58);
            this.txtSwitchCameraPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSwitchCameraPort.Name = "txtSwitchCameraPort";
            this.txtSwitchCameraPort.Size = new System.Drawing.Size(64, 31);
            this.txtSwitchCameraPort.TabIndex = 2;
            this.txtSwitchCameraPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchCameraPort;
            // 
            // txtSwitchMountPort
            // 
            this.txtSwitchMountPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "SwitchMountPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSwitchMountPort.Location = new System.Drawing.Point(144, 25);
            this.txtSwitchMountPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSwitchMountPort.Name = "txtSwitchMountPort";
            this.txtSwitchMountPort.Size = new System.Drawing.Size(64, 31);
            this.txtSwitchMountPort.TabIndex = 2;
            this.txtSwitchMountPort.Text = global::ObservatoryCenter.Properties.Settings.Default.SwitchMountPort;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mount port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Camera port";
            // 
            // tabPageDome
            // 
            this.tabPageDome.Controls.Add(this.groupBox3);
            this.tabPageDome.Location = new System.Drawing.Point(8, 39);
            this.tabPageDome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDome.Name = "tabPageDome";
            this.tabPageDome.Size = new System.Drawing.Size(858, 393);
            this.tabPageDome.TabIndex = 4;
            this.tabPageDome.Text = "Dome";
            this.tabPageDome.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtDomeDriverId);
            this.groupBox3.Controls.Add(this.btnConnectDomeSettings);
            this.groupBox3.Controls.Add(this.btnChooseDome);
            this.groupBox3.Location = new System.Drawing.Point(10, 5);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(639, 88);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose driver";
            // 
            // txtDomeDriverId
            // 
            this.txtDomeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "DomeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomeDriverId.Location = new System.Drawing.Point(9, 30);
            this.txtDomeDriverId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDomeDriverId.Name = "txtDomeDriverId";
            this.txtDomeDriverId.ReadOnly = true;
            this.txtDomeDriverId.Size = new System.Drawing.Size(373, 31);
            this.txtDomeDriverId.TabIndex = 9;
            this.txtDomeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.DomeDriverId;
            // 
            // btnConnectDomeSettings
            // 
            this.btnConnectDomeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectDomeSettings.Location = new System.Drawing.Point(522, 26);
            this.btnConnectDomeSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectDomeSettings.Name = "btnConnectDomeSettings";
            this.btnConnectDomeSettings.Size = new System.Drawing.Size(108, 35);
            this.btnConnectDomeSettings.TabIndex = 7;
            this.btnConnectDomeSettings.Text = "Connect";
            this.btnConnectDomeSettings.UseVisualStyleBackColor = true;
            this.btnConnectDomeSettings.Click += new System.EventHandler(this.btnConnectDomeSettings_Click);
            // 
            // btnChooseDome
            // 
            this.btnChooseDome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseDome.Location = new System.Drawing.Point(405, 26);
            this.btnChooseDome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChooseDome.Name = "btnChooseDome";
            this.btnChooseDome.Size = new System.Drawing.Size(108, 35);
            this.btnChooseDome.TabIndex = 6;
            this.btnChooseDome.Text = "Choose";
            this.btnChooseDome.UseVisualStyleBackColor = true;
            this.btnChooseDome.Click += new System.EventHandler(this.btnChooseDome_Click);
            // 
            // tabPageTelescope
            // 
            this.tabPageTelescope.Controls.Add(this.groupBox4);
            this.tabPageTelescope.Location = new System.Drawing.Point(8, 39);
            this.tabPageTelescope.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTelescope.Name = "tabPageTelescope";
            this.tabPageTelescope.Size = new System.Drawing.Size(858, 393);
            this.tabPageTelescope.TabIndex = 3;
            this.tabPageTelescope.Text = "Telescope";
            this.tabPageTelescope.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTelescopeDriverId);
            this.groupBox4.Controls.Add(this.btnConnectTelescopeSettings);
            this.groupBox4.Controls.Add(this.btnChooseTelescope);
            this.groupBox4.Location = new System.Drawing.Point(10, 5);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(639, 88);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose driver";
            // 
            // txtTelescopeDriverId
            // 
            this.txtTelescopeDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ObservatoryCenter.Properties.Settings.Default, "TelescopeDriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTelescopeDriverId.Location = new System.Drawing.Point(9, 30);
            this.txtTelescopeDriverId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTelescopeDriverId.Name = "txtTelescopeDriverId";
            this.txtTelescopeDriverId.ReadOnly = true;
            this.txtTelescopeDriverId.Size = new System.Drawing.Size(373, 31);
            this.txtTelescopeDriverId.TabIndex = 9;
            this.txtTelescopeDriverId.Text = global::ObservatoryCenter.Properties.Settings.Default.TelescopeDriverId;
            // 
            // btnConnectTelescopeSettings
            // 
            this.btnConnectTelescopeSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectTelescopeSettings.Location = new System.Drawing.Point(522, 26);
            this.btnConnectTelescopeSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnectTelescopeSettings.Name = "btnConnectTelescopeSettings";
            this.btnConnectTelescopeSettings.Size = new System.Drawing.Size(108, 35);
            this.btnConnectTelescopeSettings.TabIndex = 7;
            this.btnConnectTelescopeSettings.Text = "Connect";
            this.btnConnectTelescopeSettings.UseVisualStyleBackColor = true;
            this.btnConnectTelescopeSettings.Click += new System.EventHandler(this.btnConnectTelescopeSettings_Click);
            // 
            // btnChooseTelescope
            // 
            this.btnChooseTelescope.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChooseTelescope.Location = new System.Drawing.Point(405, 26);
            this.btnChooseTelescope.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChooseTelescope.Name = "btnChooseTelescope";
            this.btnChooseTelescope.Size = new System.Drawing.Size(108, 35);
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
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMisc.Size = new System.Drawing.Size(858, 393);
            this.tabPageMisc.TabIndex = 2;
            this.tabPageMisc.Text = "Misc";
            this.tabPageMisc.UseVisualStyleBackColor = true;
            // 
            // numUDRoofDuration
            // 
            this.numUDRoofDuration.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ObservatoryCenter.Properties.Settings.Default, "RoofDuration", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numUDRoofDuration.Location = new System.Drawing.Point(171, 14);
            this.numUDRoofDuration.Name = "numUDRoofDuration";
            this.numUDRoofDuration.Size = new System.Drawing.Size(87, 31);
            this.numUDRoofDuration.TabIndex = 1;
            this.numUDRoofDuration.Value = global::ObservatoryCenter.Properties.Settings.Default.RoofDuration;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "sec";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Open/Close duration";
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreDefaults.Location = new System.Drawing.Point(8, 454);
            this.btnRestoreDefaults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(112, 35);
            this.btnRestoreDefaults.TabIndex = 1;
            this.btnRestoreDefaults.Text = "Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(616, 454);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(764, 454);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(880, 503);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabPageProgramPath.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_programsPath)).EndInit();
            this.tabPagescenarioMainParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridConfig_scenarioMain)).EndInit();
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageSwitch;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSwitchMountPort;
        private System.Windows.Forms.TextBox txtSwitchCameraPort;
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
        private System.Windows.Forms.DataGridView dataGridConfig_scenarioMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_programsPath_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_programsPath_value;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLatitudeSec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLatitudeMin;
        private System.Windows.Forms.TextBox txtLongtitudeSec;
        private System.Windows.Forms.TextBox txtLatitudeGrad;
        private System.Windows.Forms.TextBox txtLongtitudeMin;
        private System.Windows.Forms.TextBox txtLongtitudeGrad;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_scenarioMain_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_scenarioMain_type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridConfig_scenarioMain_run;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridConfig_scenarioMain_argument;
    }
}