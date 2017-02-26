using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.Threading;
using System.Configuration;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.IO;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;

namespace ObservatoryCenter
{
    public partial class MainForm : Form
    {

        public ObservatoryControls ObsControl;

        /// <summary>
        /// Link to preferences form + functions for loading parameters
        /// </summary>
        public SettingsForm SetForm;

        /// <summary>
        /// SocketServer object
        /// </summary>
        public SocketServerClass SocketServer;

        /// <summary>
        /// Test form object
        /// </summary>
        public TestEquipmentForm TestForm;

        //Color constants
        Color OnColor = Color.DarkSeaGreen;
        Color OffColor = Color.Tomato;
        Color DisabledColor = Color.LightGray;


        // For logging window
        private bool AutoScrollLogFlag = true;
        private Int32 caretPos = 0;
        public Int32 MAX_LOG_LINES = 500;

        // Threads
        public Thread CheckPowerStatusThread;
        public ThreadStart CheckPowerStatusThread_startref;
        //public Thread SetPowerStatusThread;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            ObsControl = new ObservatoryControls(this);
            SetForm = new SettingsForm(this);
            TestForm = new TestEquipmentForm(this);

            //Prepare separate thread obj (just dummy init, because it couldn't be null)
            //CheckPowerStatusThread_ref = new ThreadStart(ObsControl.CheckPowerDeviceStatus); 
            //CheckPowerStatusThread = new Thread(CheckPowerStatusThread_ref);
            //SetPowerStatusThread = new Thread(ObsControl.SetDeviceStatus(null,null,null,null));

            Logging.AddLog("****************************************************************************************", LogLevel.Activity);
            Logging.AddLog("Observatory Center started", LogLevel.Activity);
            Logging.AddLog("****************************************************************************************", LogLevel.Activity);
        }

        /// <summary>
        /// Main form load event - startup actions take place here
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Load config
            ConfigManagement.Load();

            //Load parameters
            LoadParams();


            //Init programs objects using loaded settings
            ObsControl.InitProgramsObj();

            //Dump log, because interface may hang wating for connection
            Logging.DumpToFile();


            //Connect Devices, which are general adapters (no need to power or control something)
            try
            {
                ObsControl.connectSwitch = true;
                CheckPowerSwitchStatus_caller();
            }
            catch (Exception ex)
            {
                Logging.AddLog("Error connecting Switch on startup [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            try
            {
                ObsControl.connectDome = true;
            }
            catch (Exception ex)
            {
                Logging.AddLog("Error connecting Dome on startup [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            //Update visual interface statuses
            UpdateStatusbarASCOMStatus();
            UpdatePowerButtonsStatus();

            //init graphic elements
            ROOF_startPos = rectRoof.Location;
            //Update visual Roof Status
            UpdateRoofPicture();

            //Start tcp server
            SocketServer = new SocketServerClass(this);
            toolStripStatus_Connection.Text = "CONNECTIONS: 0";
            if (true)
            {
                backgroundWorker_SocketServer.RunWorkerAsync();
                toolStripStatus_Connection.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatus_Connection.ForeColor = Color.Gray;
            }

            //init vars
            //DrawTelescopeV(panelTelescope);

            //Init versiondata static class
            VersionData.initVersionData();
            //Display about information
            LoadAboutData();

            //Init Log DropDown box
            foreach (LogLevel C in Enum.GetValues(typeof(LogLevel)))
            {
                toolStripDropDownLogLevel.DropDownItems.Add(Enum.GetName(typeof(LogLevel), C));
            }
            toolStripDropDownLogLevel.Text = Enum.GetName(typeof(LogLevel), LogLevel.Activity);


            //Run all timers at the end
            mainTimer_Short.Enabled = true;
            mainTimer_Long.Enabled = true;
            mainTimer_VeryLong.Enabled = true;
            logRefreshTimer.Enabled = true;


            weatherSmallChart.Series[0].XValueType = ChartValueType.DateTime;
            weatherSmallChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "HH:mm";

            foreach (Series Sr in chartWT.Series)
            {
                Sr.XValueType = ChartValueType.DateTime;
            }
            foreach (ChartArea CA in chartWT.ChartAreas)
            {
                CA.AxisX.LabelStyle.Format = "HH:mm";
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save(); // Commit changes

            try
            {
                SocketServer.Dispose();
            }
            catch { };
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Timers block
        #region /// TIMERS *****************************************************************
        /// <summary>
        /// Main timer tick
        /// </summary>
        private void mainTimerShort_Tick(object sender, EventArgs e)
        {
            UpdatePowerButtonsStatus();
            UpdateStatusbarASCOMStatus();
            UpdateTelescopeStatus();
            UpdateRoofPicture();
            UpdateSettingsTabStatusFileds();
            UpdateApplicationsRunningStatus();

            UpdateCCDCameraFieldsStatus();

            UpdatePHDstate();
            //UpdateGuiderFieldsStatus(); //Maxim Guider

            UpdateCCDAPstate();
            UpdateCCDCstate();

            UpdateTimePannel();
        }


        /// <summary>
        /// Second main timer tick. More slower to not overload hardware
        /// </summary>
        private void mainTimer_Long_Tick(object sender, EventArgs e)
        {
            //check power switch status
            CheckPowerSwitchStatus_caller();


            //update AstroTortilla solver status
            UpdateSolverFileds();

        }

        /// <summary>
        /// Third main timer tick. Very slow for updating information
        /// </summary>
        private void mainTime_VeryLong_Tick(object sender, EventArgs e)
        {
            UpdateWeatherData();

            UpdateTelescopeTempControlData();

            UpdateEvents();

        }


        /// <summary>
        /// Timer to work with log information (save it, display, etc)
        /// </summary>
        private void logRefreshTimer_Tick(object sender, EventArgs e)
        {
            //Get current loglevel value
            LogLevel CurLogLevel = LogLevel.Activity;
            if (!Enum.TryParse(toolStripDropDownLogLevel.Text, out CurLogLevel))
            {
                CurLogLevel = LogLevel.Activity;
            }


            //add line to richtextbox
            Logging.DisplayLogInTextBox(txtLog, CurLogLevel);

            //write to file
            Logging.DumpToFile();
        }

        #endregion /// TIMERS *****************************************************************
        // END OF TIMERS BLOCK

         // Region block with hadnling power management visual interface
        #region /// POWER BUTTONS HANDLING ///////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnTelescopePower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.PowerSet(ObsControl.POWER_MOUNT_PORT, "POWER_MOUNT_PORT", SwitchState, out ObsControl.Mount_power_flag))
            {
                //if switching was successful
                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Mount_power_flag = SwitchState;
            }
            else
            {
                //if switching wasn't proceed
            }
        }

        private void btnRoofPower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.PowerSet(ObsControl.POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", SwitchState, out ObsControl.Roof_power_flag))
            {
                //if switching was successful
                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Roof_power_flag = SwitchState;
            }
            else
            {
                //if switching wasn't proceed
            }
        }

        private void btnCameraPower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.PowerSet(ObsControl.POWER_CAMERA_PORT, "POWER_CAMERA_PORT", SwitchState, out ObsControl.Camera_power_flag))
            {
                //if switching was successful

                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Camera_power_flag = SwitchState;
                /////
                //txtCameraName.BackColor = (SwitchState ? OnColor : OffColor);
            }
            else
            {
                //if switching wasn't proceed

            }
        }

        private void btnFocuserPower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.PowerSet(ObsControl.POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT", SwitchState, out ObsControl.Focuser_power_flag))
            {
                //if switching was successful

                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Focuser_power_flag = SwitchState;
            }
            else
            {
                //if switching wasn't proceed

            }
        }

        private void btnPowerAll_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Power all")
            {
                //Power all
                ObsControl.PowerCameraOn();
                ObsControl.PowerMountOn();
                ObsControl.PowerFocuserOn();
                ObsControl.PowerRoofOn();
            }
            else if (((Button)sender).Text == "Depower all")
            {
                //Power all
                ObsControl.PowerCameraOff();
                ObsControl.PowerMountOff();
                ObsControl.PowerFocuserOff();
                ObsControl.PowerRoofOff();
            }
        }

        #endregion Power button handling
        // End of block with power buttons handling

        // Block with Scenarios run
        #region /// Scenarios run procedures ////////////////////////////////////////////////////
        private void btnStartAll_Click(object sender, EventArgs e)
        {
            ThreadStart RunThreadRef = new ThreadStart(ObsControl.StartUpObservatory);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            Logging.AddLog("Command 'Prepare run' was initiated", LogLevel.Debug);
        }

        private void btnBeforeImaging_Click(object sender, EventArgs e)
        {
            /// Prepare Imaging run scenario
            //ObsControl.?

        }

        /// <summary>
        /// Run maxim and connect it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximStart_Click(object sender, EventArgs e)
        {
            ThreadStart RunThreadRef = new ThreadStart(ObsControl.StartMaximDLroutines);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            Logging.AddLog("Command 'Prepare maxim' was initiated", LogLevel.Debug);
        }


        #endregion Scenarios run procedures
        //End of autorun procedures block

        // Settings block
        #region /// Settings block ////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetForm.ShowDialog();
        }

        /// <summary>
        /// Used to load all prameters during startup
        /// </summary>
        public void LoadParams()
        {
            Logging.AddLog("Loading saved parameters", LogLevel.Trace);
            try
            {
                ObsControl.DOME_DRIVER_NAME = Properties.Settings.Default.DomeDriverId;
                ObsControl.TELESCOPE_DRIVER_NAME = Properties.Settings.Default.TelescopeDriverId;
                ObsControl.SWITCH_DRIVER_NAME = Properties.Settings.Default.SwitchDriverId;

                ObsControl.DomeEnabled = Properties.Settings.Default.DeviceEnabled_Dome;
                ObsControl.TelescopeEnabled = Properties.Settings.Default.DeviceEnabled_Telescope;
                ObsControl.SwitchEnabled = Properties.Settings.Default.DeviceEnabled_Switch;

                //ObsControl.MaximDLPath = Properties.Settings.Default.MaximDLPath;
                //ObsControl.CCDAPPath = Properties.Settings.Default.CCDAPPath;
                //ObsControl.PlanetariumPath = Properties.Settings.Default.CartesPath;

                ObsControl.POWER_MOUNT_PORT = Convert.ToByte(Properties.Settings.Default.SwitchMountPort);
                ObsControl.POWER_CAMERA_PORT = Convert.ToByte(Properties.Settings.Default.SwitchCameraPort);
                ObsControl.POWER_FOCUSER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchFocuserPort);
                ObsControl.POWER_ROOFPOWER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofPowerPort);
                ObsControl.POWER_ROOFSWITCH_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofSwitchPort);

                RoofDuration = Convert.ToInt16(Properties.Settings.Default.RoofDuration);
                RoofDurationCount = Convert.ToInt16(Properties.Settings.Default.RoofDurationMeasurementsCount);
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                string FullMessage = "Error loading params. ";
                FullMessage += "Exception source: " + ex.Data + " | " + ex.Message + " | " + messstr;

                Logging.AddLog(FullMessage, LogLevel.Important, Highlight.Error);
            }
            Logging.AddLog("Saved parameters loaded", LogLevel.Activity);
        }


        #endregion /// Settings block ////////////////////////////////////////////////////////////////////////////////////////////////
        // End of settings block

        // Telescope routines
        #region //// Telescope routines //////////////////////////////////////


        #endregion Telescope routines
        // End of telescope routines

        // Status bar event handling block
        #region //// Status bar events handling //////////////////////////////////////
        private void toolStripStatus_Switch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ObsControl.connectSwitch = !ObsControl.Switch_connected_flag;
                CheckPowerSwitchStatus_caller();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in status bar switch connect/disconnect! " + ex.ToString());
            }
        }
        private void toolStripStatus_Dome_Click(object sender, EventArgs e)
        {
            try
            {
                ObsControl.connectDome = !ObsControl.Dome_connected_flag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in status bar dome connect/disconnect! " + ex.ToString());
            }

        }
        private void toolStripStatus_Telescope_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ObsControl.connectMount = !ObsControl.Mount_connected_flag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in telescope connect/disconnect! " + ex.ToString());
            }

        }
        private void toolStripStatus_Camera_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.ConnectCamera();
        }
        //Change log level control
        private void toolStripDropDownLogLevel_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripDropDownLogLevel.Text = e.ClickedItem.Text;
        }
        #endregion /// Status bar event handling //////////////////////////////////////////////
        // End of Status bar event handling block

        #region //// About information //////////////////////////////////////
        private void LoadAboutData()
        {
            lblVersion.Text += "Publish version: " + VersionData.PublishVersionSt;
            lblVersion.Text += Environment.NewLine + "Assembly version: " + VersionData.AssemblyVersionSt;
            lblVersion.Text += Environment.NewLine + "File version: " + VersionData.FileVersionSt;
            //lblVersion.Text += Environment.NewLine + "Product version " + ProductVersionSt;

            //MessageBox.Show("Application " + assemName.Name + ", Version " + ver.ToString());
            lblVersion.Text += Environment.NewLine + "Compile time: " + VersionData.CompileTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Add link
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.astromania.info/";
            linkAstromania.Links.Add(link);
        }

        private void linkAstromania_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        #endregion About information

        // AppLinks Events 
        #region //// AppLinks Events //////////////////////////////////////
        private void linkCdC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startPlanetarium();
        }
        private void linkTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //ObsControl.objPHD2App.CMD_ConnectEquipment(); //connect equipment
            //ObsControl.GuidePiexelScale=ObsControl.objPHD2App.CMD_GetPixelScale(); //connect equipment

            ObsControl.objWSApp.CMD_GetBoltwoodString(); //get booltwood string

        }
        private void linkPHD2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand("PHD2_RUN");

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 0);

            ObsControl.CommandParser.ParseSingleCommand("PHD2_CONNECT");
        }
        private void linkMaximDL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_RUN");
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");
        }
        private void linkCCDAP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startCCDAP();
        }
        private void linkPHDBroker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startPHDBroker();
        }
        private void linkFocusMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startFocusMax();
        }
        private void linkWeatherStation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startWS();
        }
        private void linkTelescopeTempControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startTTC();
        }

        private void linkCCDC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand("CCDC_RUN");
        }


        #endregion //// AppLinks Events //////////////////////////////////////
        // End of AppLinks Events block 

        // Settings tab ASCOM Devices
        #region /// Settings tab ASCOM Devices ////////////////////////////////////////////////////////////////
        private void chkASCOM_Enable_Switch_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.connectSwitch = false;
                ObsControl.SwitchEnabled = false;
                ObsControl.resetSwitch();
            }
            else
            {
                //connect
                ObsControl.SwitchEnabled = true;
                ObsControl.connectSwitch = true;
                CheckPowerSwitchStatus_caller();
            }
            Update_SWITCH_related_elements();
            Properties.Settings.Default.DeviceEnabled_Switch = ObsControl.SwitchEnabled;
        }
        private void btnASCOM_Choose_Switch_Click(object sender, EventArgs e)
        {
            ObsControl.SWITCH_DRIVER_NAME = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
            txtSet_Switch.Text = ObsControl.SWITCH_DRIVER_NAME;
            if (ObsControl.SWITCH_DRIVER_NAME != "")
            {
                chkASCOM_Enable_Switch.Checked = true;
            }

            ObsControl.resetSwitch();
            ObsControl.connectSwitch = true;
            CheckPowerSwitchStatus_caller();
            Update_SWITCH_related_elements();
        }
        private void chkASCOM_Enable_Dome_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.connectDome = false;
                ObsControl.DomeEnabled = false;
                ObsControl.resetDome();
            }
            else
            {
                //connect
                ObsControl.DomeEnabled = true;
                ObsControl.connectDome = true;
            }
            Update_DOME_related_elements();
            Properties.Settings.Default.DeviceEnabled_Dome = ObsControl.DomeEnabled;
        }
        private void btnASCOM_Choose_Dome_Click(object sender, EventArgs e)
        {
            ObsControl.DOME_DRIVER_NAME = ASCOM.DriverAccess.Dome.Choose(Properties.Settings.Default.DomeDriverId);
            txtSet_Dome.Text = ObsControl.DOME_DRIVER_NAME;
            if (ObsControl.DOME_DRIVER_NAME != "")
            {
                chkASCOM_Enable_Dome.Checked = true;
            }
            ObsControl.resetDome();
            ObsControl.connectDome = true;
            Update_DOME_related_elements();
        }
        private void chkASCOM_Enable_Telescope_CheckedChanged(object sender, EventArgs e)
        {

            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.connectMount = false;
                ObsControl.TelescopeEnabled = false;
                ObsControl.resetTelescope();
            }
            else
            {
                //connect
                ObsControl.TelescopeEnabled = true;
                ObsControl.connectMount = true;
            }
            Update_TELESCOPE_related_elements();
            Properties.Settings.Default.DeviceEnabled_Telescope = ObsControl.TelescopeEnabled;
        }
        private void btnASCOM_Choose_Telescope_Click(object sender, EventArgs e)
        {
            ObsControl.TELESCOPE_DRIVER_NAME = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.TelescopeDriverId);
            txtSet_Telescope.Text = ObsControl.TELESCOPE_DRIVER_NAME;
            if (ObsControl.TELESCOPE_DRIVER_NAME != "")
            {
                chkASCOM_Enable_Telescope.Checked = true;
            }
            ObsControl.resetTelescope();
            ObsControl.connectMount = true;
            Update_TELESCOPE_related_elements();
        }

        #endregion /// Settings tab ASCOM Devices /////////////////////////////////////////////////////////////////
        // End of Settings tab ASCOM Devices block

        /// <summary>
        /// Wrapper to call check power switch status on background (separate thread)
        /// because in case of network timeout it can hang system
        /// </summary>
        public void CheckPowerSwitchStatus_caller()
        {
            if (ObsControl.Switch_connected_flag)
            {
                try
                {
                    if (CheckPowerStatusThread == null || !CheckPowerStatusThread.IsAlive)
                    {
                        CheckPowerStatusThread_startref = new ThreadStart(ObsControl.CheckPowerDeviceStatus);
                        CheckPowerStatusThread = new Thread(CheckPowerStatusThread_startref);
                        CheckPowerStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception in Main timer CheckPowerDeviceStatus! " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Separate thread for socket server
        /// </summary>
        private void backgroundWorker_SocketServer_DoWork(object sender, DoWorkEventArgs e)
        {
            SocketServer.ListenSocket();
        }

        /// <summary>
        /// Run TestEquipment Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunTest_Click(object sender, EventArgs e)
        {
            TestForm.Show();
        }


        private void btnCoolerOn_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.SetCameraCooling();
        }

        private void btnCoolerOff_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOff();
        }

        private void btnCoolerWarm_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOff(true);
        }

        private void UpDownSetPoint_ValueChanged(object sender, EventArgs e)
        {
            ObsControl.objMaxim.SetCameraCooling(Convert.ToDouble(updownCameraSetPoint.Value));
        }

        private void btnClearGuidingStat_Click(object sender, EventArgs e)
        {
            txtGuiderErrorPHD.Text = "";
            GuidingStats.Reset();

            chart1.Series["SeriesGuideError"].Points.Clear();
            chart1.Series["SeriesGuideErrorOutOfScale"].Points.Clear();

            txtRMS_X.Text = "";
            txtRMS_Y.Text = "";
            txtRMS.Text = "";

        }


        private void btnGuiderConnect_Click(object sender, EventArgs e)
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                ObsControl.objPHD2App.CMD_ConnectEquipment(); //connect equipment
            }
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                ObsControl.GuidePiexelScale = ObsControl.objPHD2App.CMD_GetPixelScale(); //connect equipment
                Thread.Sleep(100);
                ObsControl.GuidePiexelScale = ObsControl.objPHD2App.CMD_StartGuiding(); //start  quiding
            }
        }


        private void panelTele3D_Paint(object sender, PaintEventArgs e)
        {
            Draw3DTelescope(e);
        }

        private void btnAstrotortillaSolve_Click(object sender, EventArgs e)
        {
            //Run async
            ThreadStart RunThreadRef = new ThreadStart(ObsControl.startAstrotortillaSolve);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            //Logging.AddLog("Command 'Prepare run' was initiated", LogLevel.Debug);

        }







        private void btnPark_Click(object sender, EventArgs e)
        {
            ObsControl.Telescope_Park();
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
            ObsControl.Telescope_TrackToggle();
        }


        private void tempNotImplemented_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet");
        }
    }
}
