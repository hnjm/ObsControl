namespace ObservatoryCenter
{
    partial class __MainForm_new
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(__MainForm_new));
            this.groupBoxRoof = new System.Windows.Forms.GroupBox();
            this.btnStopRoof = new System.Windows.Forms.Button();
            this.btnCloseRoof = new System.Windows.Forms.Button();
            this.btnOpenRoof = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.rectRoof = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectBase = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.groupBoxTelescope = new System.Windows.Forms.GroupBox();
            this.panelTele3D = new System.Windows.Forms.Panel();
            this.btnConnectTelescope = new System.Windows.Forms.Button();
            this.btnTrack = new System.Windows.Forms.Button();
            this.btnPark = new System.Windows.Forms.Button();
            this.txtTelescopeDec = new System.Windows.Forms.TextBox();
            this.txtTelescopeAlt = new System.Windows.Forms.TextBox();
            this.txtTelescopeRA = new System.Windows.Forms.TextBox();
            this.txtTelescopeAz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.weatherSmallChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtRainCondition = new System.Windows.Forms.TextBox();
            this.txtCloudState = new System.Windows.Forms.TextBox();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_Switch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Dome = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Telescope = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Focuser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Camera = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Connection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownLogLevel = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripLogSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxRoof.SuspendLayout();
            this.groupBoxTelescope.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weatherSmallChart)).BeginInit();
            this.statusBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRoof
            // 
            this.groupBoxRoof.Controls.Add(this.btnStopRoof);
            this.groupBoxRoof.Controls.Add(this.btnCloseRoof);
            this.groupBoxRoof.Controls.Add(this.btnOpenRoof);
            this.groupBoxRoof.Controls.Add(this.shapeContainer2);
            this.groupBoxRoof.Location = new System.Drawing.Point(15, 15);
            this.groupBoxRoof.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxRoof.Name = "groupBoxRoof";
            this.groupBoxRoof.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxRoof.Size = new System.Drawing.Size(378, 242);
            this.groupBoxRoof.TabIndex = 4;
            this.groupBoxRoof.TabStop = false;
            this.groupBoxRoof.Text = "Roof";
            // 
            // btnStopRoof
            // 
            this.btnStopRoof.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopRoof.Enabled = false;
            this.btnStopRoof.Location = new System.Drawing.Point(142, 192);
            this.btnStopRoof.Margin = new System.Windows.Forms.Padding(6);
            this.btnStopRoof.Name = "btnStopRoof";
            this.btnStopRoof.Size = new System.Drawing.Size(74, 36);
            this.btnStopRoof.TabIndex = 1;
            this.btnStopRoof.Text = "Stop";
            this.btnStopRoof.UseVisualStyleBackColor = true;
            // 
            // btnCloseRoof
            // 
            this.btnCloseRoof.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCloseRoof.Enabled = false;
            this.btnCloseRoof.Location = new System.Drawing.Point(10, 192);
            this.btnCloseRoof.Margin = new System.Windows.Forms.Padding(6);
            this.btnCloseRoof.Name = "btnCloseRoof";
            this.btnCloseRoof.Size = new System.Drawing.Size(122, 36);
            this.btnCloseRoof.TabIndex = 1;
            this.btnCloseRoof.Text = "Close roof";
            this.btnCloseRoof.UseVisualStyleBackColor = true;
            // 
            // btnOpenRoof
            // 
            this.btnOpenRoof.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenRoof.Enabled = false;
            this.btnOpenRoof.Location = new System.Drawing.Point(238, 192);
            this.btnOpenRoof.Margin = new System.Windows.Forms.Padding(6);
            this.btnOpenRoof.Name = "btnOpenRoof";
            this.btnOpenRoof.Size = new System.Drawing.Size(132, 36);
            this.btnOpenRoof.TabIndex = 2;
            this.btnOpenRoof.Text = "Open roof";
            this.btnOpenRoof.UseVisualStyleBackColor = true;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(6, 30);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1,
            this.rectRoof,
            this.rectBase});
            this.shapeContainer2.Size = new System.Drawing.Size(366, 206);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
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
            // groupBoxTelescope
            // 
            this.groupBoxTelescope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTelescope.Controls.Add(this.panelTele3D);
            this.groupBoxTelescope.Controls.Add(this.btnConnectTelescope);
            this.groupBoxTelescope.Controls.Add(this.btnTrack);
            this.groupBoxTelescope.Controls.Add(this.btnPark);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeDec);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeAlt);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeRA);
            this.groupBoxTelescope.Controls.Add(this.txtTelescopeAz);
            this.groupBoxTelescope.Controls.Add(this.label4);
            this.groupBoxTelescope.Controls.Add(this.label3);
            this.groupBoxTelescope.Controls.Add(this.label2);
            this.groupBoxTelescope.Controls.Add(this.label1);
            this.groupBoxTelescope.Location = new System.Drawing.Point(9, 431);
            this.groupBoxTelescope.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxTelescope.Name = "groupBoxTelescope";
            this.groupBoxTelescope.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxTelescope.Size = new System.Drawing.Size(388, 448);
            this.groupBoxTelescope.TabIndex = 4;
            this.groupBoxTelescope.TabStop = false;
            this.groupBoxTelescope.Text = "Telescope";
            // 
            // panelTele3D
            // 
            this.panelTele3D.Location = new System.Drawing.Point(14, 27);
            this.panelTele3D.Margin = new System.Windows.Forms.Padding(6);
            this.panelTele3D.Name = "panelTele3D";
            this.panelTele3D.Size = new System.Drawing.Size(360, 247);
            this.panelTele3D.TabIndex = 9;
            // 
            // btnConnectTelescope
            // 
            this.btnConnectTelescope.Location = new System.Drawing.Point(14, 284);
            this.btnConnectTelescope.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnectTelescope.Name = "btnConnectTelescope";
            this.btnConnectTelescope.Size = new System.Drawing.Size(150, 50);
            this.btnConnectTelescope.TabIndex = 7;
            this.btnConnectTelescope.Text = "Connect";
            this.btnConnectTelescope.UseVisualStyleBackColor = true;
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(276, 284);
            this.btnTrack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(98, 48);
            this.btnTrack.TabIndex = 7;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = true;
            // 
            // btnPark
            // 
            this.btnPark.Location = new System.Drawing.Point(178, 284);
            this.btnPark.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPark.Name = "btnPark";
            this.btnPark.Size = new System.Drawing.Size(98, 48);
            this.btnPark.TabIndex = 7;
            this.btnPark.Text = "Park";
            this.btnPark.UseVisualStyleBackColor = true;
            // 
            // txtTelescopeDec
            // 
            this.txtTelescopeDec.Location = new System.Drawing.Point(252, 391);
            this.txtTelescopeDec.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTelescopeDec.Name = "txtTelescopeDec";
            this.txtTelescopeDec.Size = new System.Drawing.Size(118, 31);
            this.txtTelescopeDec.TabIndex = 6;
            // 
            // txtTelescopeAlt
            // 
            this.txtTelescopeAlt.Location = new System.Drawing.Point(46, 391);
            this.txtTelescopeAlt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTelescopeAlt.Name = "txtTelescopeAlt";
            this.txtTelescopeAlt.Size = new System.Drawing.Size(118, 31);
            this.txtTelescopeAlt.TabIndex = 6;
            // 
            // txtTelescopeRA
            // 
            this.txtTelescopeRA.Location = new System.Drawing.Point(252, 350);
            this.txtTelescopeRA.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTelescopeRA.Name = "txtTelescopeRA";
            this.txtTelescopeRA.Size = new System.Drawing.Size(118, 31);
            this.txtTelescopeRA.TabIndex = 6;
            // 
            // txtTelescopeAz
            // 
            this.txtTelescopeAz.Location = new System.Drawing.Point(46, 350);
            this.txtTelescopeAz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTelescopeAz.Name = "txtTelescopeAz";
            this.txtTelescopeAz.Size = new System.Drawing.Size(118, 31);
            this.txtTelescopeAz.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 394);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Dec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 353);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "RA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 394);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 353);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Az";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.weatherSmallChart);
            this.groupBox9.Controls.Add(this.txtRainCondition);
            this.groupBox9.Controls.Add(this.txtCloudState);
            this.groupBox9.Controls.Add(this.txtTemp);
            this.groupBox9.Location = new System.Drawing.Point(9, 888);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox9.Size = new System.Drawing.Size(378, 272);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Weather";
            // 
            // weatherSmallChart
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 6;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.Interval = 5D;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 6;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.Name = "ChartArea1";
            this.weatherSmallChart.ChartAreas.Add(chartArea1);
            this.weatherSmallChart.Location = new System.Drawing.Point(8, 75);
            this.weatherSmallChart.Margin = new System.Windows.Forms.Padding(6);
            this.weatherSmallChart.Name = "weatherSmallChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "CloudIdx";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "Temp";
            this.weatherSmallChart.Series.Add(series1);
            this.weatherSmallChart.Series.Add(series2);
            this.weatherSmallChart.Size = new System.Drawing.Size(370, 191);
            this.weatherSmallChart.TabIndex = 5;
            this.weatherSmallChart.Text = "chart2";
            // 
            // txtRainCondition
            // 
            this.txtRainCondition.Location = new System.Drawing.Point(192, 27);
            this.txtRainCondition.Margin = new System.Windows.Forms.Padding(6);
            this.txtRainCondition.Name = "txtRainCondition";
            this.txtRainCondition.ReadOnly = true;
            this.txtRainCondition.Size = new System.Drawing.Size(170, 31);
            this.txtRainCondition.TabIndex = 1;
            // 
            // txtCloudState
            // 
            this.txtCloudState.Location = new System.Drawing.Point(12, 27);
            this.txtCloudState.Margin = new System.Windows.Forms.Padding(6);
            this.txtCloudState.Name = "txtCloudState";
            this.txtCloudState.ReadOnly = true;
            this.txtCloudState.Size = new System.Drawing.Size(66, 31);
            this.txtCloudState.TabIndex = 1;
            // 
            // txtTemp
            // 
            this.txtTemp.Location = new System.Drawing.Point(116, 27);
            this.txtTemp.Margin = new System.Windows.Forms.Padding(6);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = true;
            this.txtTemp.Size = new System.Drawing.Size(66, 31);
            this.txtTemp.TabIndex = 1;
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_Switch,
            this.toolStripStatus_Dome,
            this.toolStripStatus_Telescope,
            this.toolStripStatus_Focuser,
            this.toolStripStatus_Camera,
            this.toolStripStatus_Connection,
            this.toolStripDropDownLogLevel,
            this.toolStripLogSize});
            this.statusBar.Location = new System.Drawing.Point(0, 1171);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(1894, 38);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusBar";
            // 
            // toolStripStatus_Switch
            // 
            this.toolStripStatus_Switch.DoubleClickEnabled = true;
            this.toolStripStatus_Switch.Name = "toolStripStatus_Switch";
            this.toolStripStatus_Switch.Size = new System.Drawing.Size(100, 33);
            this.toolStripStatus_Switch.Text = "SWITCH";
            // 
            // toolStripStatus_Dome
            // 
            this.toolStripStatus_Dome.DoubleClickEnabled = true;
            this.toolStripStatus_Dome.Name = "toolStripStatus_Dome";
            this.toolStripStatus_Dome.Size = new System.Drawing.Size(84, 33);
            this.toolStripStatus_Dome.Text = "DOME";
            // 
            // toolStripStatus_Telescope
            // 
            this.toolStripStatus_Telescope.DoubleClickEnabled = true;
            this.toolStripStatus_Telescope.Name = "toolStripStatus_Telescope";
            this.toolStripStatus_Telescope.Size = new System.Drawing.Size(133, 33);
            this.toolStripStatus_Telescope.Text = "TELESCOPE";
            this.toolStripStatus_Telescope.ToolTipText = "test";
            // 
            // toolStripStatus_Focuser
            // 
            this.toolStripStatus_Focuser.Name = "toolStripStatus_Focuser";
            this.toolStripStatus_Focuser.Size = new System.Drawing.Size(115, 33);
            this.toolStripStatus_Focuser.Text = "FOCUSER";
            // 
            // toolStripStatus_Camera
            // 
            this.toolStripStatus_Camera.Name = "toolStripStatus_Camera";
            this.toolStripStatus_Camera.Size = new System.Drawing.Size(108, 33);
            this.toolStripStatus_Camera.Text = "CAMERA";
            // 
            // toolStripStatus_Connection
            // 
            this.toolStripStatus_Connection.AutoSize = false;
            this.toolStripStatus_Connection.Name = "toolStripStatus_Connection";
            this.toolStripStatus_Connection.Size = new System.Drawing.Size(200, 33);
            this.toolStripStatus_Connection.Text = "CONNECTIONS: 0";
            // 
            // toolStripDropDownLogLevel
            // 
            this.toolStripDropDownLogLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownLogLevel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownLogLevel.Image")));
            this.toolStripDropDownLogLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownLogLevel.Name = "toolStripDropDownLogLevel";
            this.toolStripDropDownLogLevel.Size = new System.Drawing.Size(336, 36);
            this.toolStripDropDownLogLevel.Text = "toolStripDropDownLogLevel";
            // 
            // toolStripLogSize
            // 
            this.toolStripLogSize.Name = "toolStripLogSize";
            this.toolStripLogSize.Size = new System.Drawing.Size(96, 33);
            this.toolStripLogSize.Text = "LogRec:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(402, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1492, 1156);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1476, 1109);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1476, 1109);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(15, 274);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 96);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // MainForm_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1894, 1209);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBoxTelescope);
            this.Controls.Add(this.groupBoxRoof);
            this.Name = "MainForm_new";
            this.Text = "ShortForm";
            this.groupBoxRoof.ResumeLayout(false);
            this.groupBoxTelescope.ResumeLayout(false);
            this.groupBoxTelescope.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weatherSmallChart)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRoof;
        private System.Windows.Forms.Button btnStopRoof;
        private System.Windows.Forms.Button btnCloseRoof;
        private System.Windows.Forms.Button btnOpenRoof;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectRoof;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectBase;
        private System.Windows.Forms.GroupBox groupBoxTelescope;
        private System.Windows.Forms.Panel panelTele3D;
        private System.Windows.Forms.Button btnConnectTelescope;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Button btnPark;
        private System.Windows.Forms.TextBox txtTelescopeDec;
        private System.Windows.Forms.TextBox txtTelescopeAlt;
        private System.Windows.Forms.TextBox txtTelescopeRA;
        private System.Windows.Forms.TextBox txtTelescopeAz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.DataVisualization.Charting.Chart weatherSmallChart;
        private System.Windows.Forms.TextBox txtRainCondition;
        private System.Windows.Forms.TextBox txtCloudState;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Switch;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Dome;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Telescope;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Focuser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Camera;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Connection;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownLogLevel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLogSize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}