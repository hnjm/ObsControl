﻿using System;
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

public enum FormAppearanceMode { MODE_SHORT, MODE_MAX };

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


        public bool bMinModeEnabled = true; //should or shoudn't enabled short mode
        private FormAppearanceMode FORM_APPEARANCE_MODE = FormAppearanceMode.MODE_MAX;
        private int Form_Normal_Width = 0;
        int borderWidth = 0;
        int titleBarHeight = 0;
        int statusBarHeight = 0;
        int prevX, prevY = 0;

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


            //FORM APPEARENCE
            Form_Normal_Width = this.Width;
            borderWidth = (this.Width - this.ClientSize.Width) / 2;
            titleBarHeight = this.Height - this.ClientSize.Height - 2 * borderWidth;
            statusBarHeight = statusBar.Height;
            prevX = this.Location.X;
            prevY = this.Location.Y;
            Form_SwitchTo_Maximum_Mode();


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
            UpdateRoofPicture(); //Checked for not quering device/program

            UpdateCCDCameraStatus();//Checked for not quering device/program
            UpdateCCDCameraCoolerStatus(); //Check for not quering device/program

            UpdateTelescopeStatus(); //Checked for not quering device/program

            UpdateSettingsTabStatusFileds(); // Checked for not quering device / program

            UpdateApplicationsRunningStatus();// Checked for not quering device / program
                       

            UpdatePHDstate();// Checked for not quering device / program


            UpdateCCDCstate();

            UpdateTimePannel(); //ok

            //Short form
            UpdateShortPanelButtonsStatus(); //ok


            //UpdateGuiderFieldsStatus(); //Maxim Guider
            //UpdateCCDAPstate(); //CCDAP

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

            //check telscope status
            ObsControl.ASCOMTelescope.CheckTelescopeStatus_async();

            //check phd status
            ObsControl.objPHD2App.CMD_GetConnectEquipmentStatus();

            //check maxim status
            ObsControl.objMaxim.CheckMaximDevicesStatus();

            //check maxim camera temp status
            ObsControl.objMaxim.checkCameraTemperatureStatus();

            //Update weather file
            ObsControl.Boltwood.WriteFile();



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
            btnStartAll.BackColor = OffColor;
            chkRun.Checked = true;
            chkRun.BackColor = OffColor;
            ObsControl.StartUpObservatory_async();
        }
        /// <summary>
        /// Inoveked from other thread when startup ends
        /// </summary>
        public void endRunAction()
        {
            chkRun.Checked = false;
            chkRun.BackColor = DefBackColor;

            btnStartAll.BackColor = DefBackColor;
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


        /*********************************************************************************************************************
         * Changing form appearence mode 
        *********************************************************************************************************************/
        #region FORM_APPEREANCE_MODE
        /// <summary>
        /// Capture events for Minimize / Maximize event for changing FORM MODE
        /// </summary>
        /// <param name="m"></param>
        //protected override void WndProc(ref Message m)
        protected override void WndProc(ref Message m)
        {
            bool stopEvents = false;

            if (m.Msg == 0x0112 && bMinModeEnabled) // WM_SYSCOMMAND and Min mode enabled in settings
            {
                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    if (FORM_APPEARANCE_MODE == FormAppearanceMode.MODE_SHORT)
                    {
                        Form_SwitchTo_Maximum_Mode();
                        stopEvents = true;
                    };

                }
                else if (m.WParam == new IntPtr(0xF020)) // Minimize event - SC_MINIMIZE from Winuser.h
                {
                    if (FORM_APPEARANCE_MODE == FormAppearanceMode.MODE_MAX)
                    {
                        Form_SwitchTo_Short_Mode();
                        stopEvents = true;
                    };
                }
            }

            if (!stopEvents)
            {
                base.WndProc(ref m);
            }
        }


        /// <summary>
        /// MINIMUM mode
        /// </summary>
        private void Form_SwitchTo_Short_Mode()
        {
            if (!bMinModeEnabled) return;

            //if maximized - switch to normal first
            if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;

            FORM_APPEARANCE_MODE = FormAppearanceMode.MODE_SHORT;

            //move to top
            this.Location = new Point(prevX, 0);
            //this.Update();

            //hide default pannel
            panelMaximum.Visible = false;
            statusBar.Visible = false;

            //show minimum pannel
            PanelShort.Location = new Point(0, 0);
            PanelShort.Visible = true;

            //change window behaviour
            this.TopMost = true;
            this.Opacity = 0.8;
            this.FormBorderStyle = FormBorderStyle.None;

            //change window size
            this.MinimumSize = new Size(Form_Normal_Width, PanelShort.Size.Height+2); ;
            this.MaximumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height);
            this.Size = new Size(this.MinimumSize.Width, this.MinimumSize.Height);

            //show maximize button
            btnMaximize.Location = new Point(this.ClientSize.Width - btnMaximize.Width, 2);
            btnMaximize.Visible = true;
        }


        /// <summary>
        /// MAXIMUM mode
        /// </summary>
        private void Form_SwitchTo_Maximum_Mode()
        {
            //if (!bMinModeEnabled) return;

            FORM_APPEARANCE_MODE = FormAppearanceMode.MODE_MAX;

            //hide min pannel
            //PanelShort.Visible = false;
            btnMaximize.Visible = false;

            //position min pannel
            PanelShort.Location = new Point(0, panelMaximum.Size.Height);


            //show maximum pannel
            panelMaximum.Visible = true;
            statusBar.Visible = true;

            //change window size
            this.MinimumSize = new Size(Form_Normal_Width, panelMaximum.Size.Height + PanelShort.Size.Height + titleBarHeight + statusBarHeight + borderWidth * 2);
            this.MaximumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height);
            this.Size = new Size(this.MinimumSize.Width, this.MinimumSize.Height);
            //this.ClientSize = new Size(this.ClientSize.Width, panelMaximum.Size.Height + statusBarHeight);

            //change window behaviour
            this.TopMost = false;
            this.Opacity = 1;
            if (this.FormBorderStyle != FormBorderStyle.Sizable) this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Location = new Point(prevX, prevY);
        }

        /// <summary>
        /// Move bordless form by draging form
        /// </summary>
        private bool mouseDown;
        private Point lastLocation;
        private int newX, newY;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                //newX = (this.Location.X - lastLocation.X) + e.X;
                //newY = (this.Location.Y - lastLocation.Y) + e.Y;

                //if (newX > 0 && newY > 0 &&
                //        newX < (Screen.PrimaryScreen.Bounds.Width - this.Width) && newY < Screen.PrimaryScreen.Bounds.Height - this.Height)
                //{
                //    this.Location = new Point(newX, newY);
                //    this.Update();
                //}

                newX = Math.Min(Math.Max((this.Location.X - lastLocation.X) + e.X, 0), (Screen.PrimaryScreen.Bounds.Width - this.Width));
                newY = Math.Min(Math.Max((this.Location.Y - lastLocation.Y) + e.Y, 0), Screen.PrimaryScreen.Bounds.Height - this.Height);
                this.Location = new Point(newX, newY);
                this.Update();

            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            Form_SwitchTo_Maximum_Mode();
        }


        //Make form in MIN MODE a bit transparent when not in focus
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (FORM_APPEARANCE_MODE == FormAppearanceMode.MODE_SHORT)
            {
                this.Opacity = 1;
            }
        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (FORM_APPEARANCE_MODE == FormAppearanceMode.MODE_SHORT)
            {
                this.Opacity = 0.8;
            }
        }

        #endregion

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
