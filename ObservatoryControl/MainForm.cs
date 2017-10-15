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
        Color InterColor = Color.Yellow;
        Color DefBackColor;


        // For logging window
        private bool AutoScrollLogFlag = true;
        private Int32 caretPos = 0;
        public Int32 MAX_LOG_LINES = 500;


        public Int16 maxNumberOfPointsInChart = 100;



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
            //DefBackColor
            DefBackColor = chkMaxim.BackColor;

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
                ObsControl.ASCOMSwitch.Connect = true;
                ObsControl.ASCOMSwitch.CheckPowerDeviceStatus_async();
            }
            catch (Exception ex)
            {
                Logging.AddLog("Error connecting Switch on startup [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            try
            {
                ObsControl.ASCOMDome.Connect = true;
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
            //Display about information
            VersionData.initVersionData();
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
        //Phylosophy:
        // 1. Query devices/programs in Long timer
        // 2. Query in separate thread - good practice
        // 3. mainTimerShort - change interface elements quering internal objects


        /// <summary>
        /// Main timer tick
        /// </summary>
        private void mainTimerShort_Tick(object sender, EventArgs e)
        {
            UpdatePowerButtonsStatus(); //checked for not quering device/program
            UpdateStatusbarASCOMStatus(); //checked for not quering device/program
            UpdateRoofPicture(); //Check for not quering device/program


            UpdateTelescopeStatus();


            UpdateSettingsTabStatusFileds();
            UpdateApplicationsRunningStatus();


            UpdateCCDCameraFieldsStatus();

            UpdatePHDstate();
            //UpdateGuiderFieldsStatus(); //Maxim Guider

            UpdateCCDAPstate();
            UpdateCCDCstate();

            UpdateTimePannel();

            //Short form
            UpdateShortPannelButtonsStatus();


        }


        /// <summary>
        /// Second main timer tick. More slower to not overload hardware
        /// </summary>
        private void mainTimer_Long_Tick(object sender, EventArgs e)
        {
            //check power switch status
            ObsControl.ASCOMSwitch.CheckPowerDeviceStatus_async();
            //check dome status
            ObsControl.ASCOMDome.CheckDomeShutterStatus_async();

            //check phd status
            ObsControl.objPHD2App.CMD_GetConnectEquipmentStatus();

            //check maxim status
            ObsControl.objMaxim.CheckMaximStatus();




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
            // Prepare Imaging run scenario
            // Not implmented yet

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
                ObsControl.ASCOMDome.DRIVER_NAME = Properties.Settings.Default.DomeDriverId;
                ObsControl.ASCOMTelescope.DRIVER_NAME = Properties.Settings.Default.TelescopeDriverId;
                ObsControl.ASCOMSwitch.DRIVER_NAME = Properties.Settings.Default.SwitchDriverId;

                ObsControl.ASCOMDome.Enabled = Properties.Settings.Default.DeviceEnabled_Dome;
                ObsControl.ASCOMTelescope.Enabled = Properties.Settings.Default.DeviceEnabled_Telescope;
                ObsControl.ASCOMSwitch.Enabled = Properties.Settings.Default.DeviceEnabled_Switch;

                //ObsControl.MaximDLPath = Properties.Settings.Default.MaximDLPath;
                //ObsControl.CCDAPPath = Properties.Settings.Default.CCDAPPath;
                //ObsControl.PlanetariumPath = Properties.Settings.Default.CartesPath;

                ObsControl.ASCOMSwitch.POWER_TELESCOPE_PORT = Convert.ToByte(Properties.Settings.Default.SwitchMountPort);
                ObsControl.ASCOMSwitch.POWER_CAMERA_PORT = Convert.ToByte(Properties.Settings.Default.SwitchCameraPort);
                ObsControl.ASCOMSwitch.POWER_FOCUSER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchFocuserPort);
                ObsControl.ASCOMSwitch.POWER_ROOFPOWER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofPowerPort);
                ObsControl.ASCOMSwitch.POWER_ROOFSWITCH_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofSwitchPort);

                RoofDuration = Convert.ToInt16(Properties.Settings.Default.RoofDuration);
                RoofDurationCount = Convert.ToInt16(Properties.Settings.Default.RoofDurationMeasurementsCount);

                AstroUtilsClass.Latitude = Convert.ToDouble(Properties.Settings.Default.LatGrad) + Convert.ToDouble(Properties.Settings.Default.LatMin) / 60.0 + Convert.ToDouble(Properties.Settings.Default.LatSec) / 3600.0;
                AstroUtilsClass.Longitude = Convert.ToDouble(Properties.Settings.Default.LongGrad) + Convert.ToDouble(Properties.Settings.Default.LongMin) / 60.0 + Convert.ToDouble(Properties.Settings.Default.LongSec) / 3600.0;

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



        /// <summary>
        /// Separate thread for socket server
        /// </summary>
        private void backgroundWorker_SocketServer_DoWork(object sender, DoWorkEventArgs e)
        {
            SocketServer.ListenSocket();
        }


        private void panelTele3D_Paint(object sender, PaintEventArgs e)
        {
            DrawTelescope3D(e);
        }


    }
}
