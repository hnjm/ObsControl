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
            ObsConfig.Load();

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


            weatherChart.Series[0].XValueType = ChartValueType.DateTime;
            weatherChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "HH:mm";

            foreach (Series Sr in chartWT.Series)
            {
                Sr.XValueType = ChartValueType.DateTime;
            }
            foreach (ChartArea CA in chartWT.ChartAreas)
            {
                CA.AxisX.LabelStyle.Format = "HH:mm";
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region *** TIMERS *****************************************************************
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

        #endregion *** TIMERS *****************************************************************
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool HadWeatherData = false; //was at least once data received?
        /// <summary>
        /// Update weather state
        /// </summary>
        private void UpdateWeatherData()
        {

            double Temp = -100;
            double Cloud = -100;
            double RGC = -1;
            double Wet = 0;
            DateTime XVal;

            DataPoint EmptyP = new DataPoint { IsEmpty = true, XValue = DateTime.Now.AddSeconds(-5).ToOADate(), YValues = new double[] { 0 } };

            //Read weather station value
            if (ObsControl.objWSApp.IsRunning() && ObsControl.objWSApp.CMD_GetBoltwoodString())
            {
                if (!HadWeatherData)
                {
                    Logging.AddLog("Weather Station connected", LogLevel.Activity);
                }
                HadWeatherData = true; //flag, that at least one value was received

                //Display small widget
                Temp = ObsControl.objWSApp.BoltwoodState.Bolt_Temp;
                Cloud = ObsControl.objWSApp.BoltwoodState.Bolt_CloudIdx;
                XVal = ObsControl.objWSApp.BoltwoodState.LastMeasure;
                RGC = ObsControl.objWSApp.BoltwoodState.RGCVal;
                Wet = ObsControl.objWSApp.BoltwoodState.WetSensorVal;


                //draw value
                if (Temp > -100){
                    weatherChart.Series["Temp"].Points.AddXY(XVal.ToOADate(), Temp);
                } else {
                    weatherChart.Series["Temp"].Points.Add(EmptyP);
                }
                if (Cloud > -100)
                {
                    weatherChart.Series["CloudIdx"].Points.AddXY(XVal.ToOADate(), Cloud);
                }else {
                    weatherChart.Series["CloudIdx"].Points.Add(EmptyP);
                }

                txtRainCondition.Text = ObsControl.objWSApp.BoltwoodState.Bolt_RainFlag.ToString();

                //Data in small widget
                txtTemp.Text = Temp.ToString();
                txtCloudState.Text = Cloud.ToString();


                //Display large widget
                //draw value
                if (Temp > -100)
                {
                    chartWT.Series["Temp"].Points.AddXY(XVal.ToOADate(), Temp);
                }
                else
                {
                    chartWT.Series["Temp"].Points.Add(EmptyP);
                }
                if (Cloud > -100)
                {
                    chartWT.Series["CloudIdx"].Points.AddXY(XVal.ToOADate(), Cloud);
                }
                else
                {
                    chartWT.Series["CloudIdx"].Points.Add(EmptyP);
                }
                if (RGC >= 0)
                {
                    chartWT.Series["RGC"].Points.AddXY(XVal.ToOADate(), RGC);
                }
                else
                {
                    chartWT.Series["RGC"].Points.Add(EmptyP);
                }
                if (Wet > 0)
                {
                    chartWT.Series["Wet"].Points.AddXY(XVal.ToOADate(), 1024 - Wet);
                }
                else
                {
                    chartWT.Series["Wet"].Points.Add(EmptyP);
                }

                //Data in large widget
                txtWTTemp.Text = Temp.ToString();
                txtWTCloudIdx.Text = Cloud.ToString();
                txtWTCaseTemp.Text = ObsControl.objWSApp.BoltwoodState.Bolt_Heater.ToString();
                txtWTPreassure.Text = ObsControl.objWSApp.BoltwoodState.Preassure.ToString();
                txtWTRGC.Text = RGC.ToString();
                txtWTWet.Text = Wet.ToString();

            }
            else if (HadWeatherData)  
            {

                weatherChart.Series["Temp"].Points.Add(EmptyP);
                weatherChart.Series["CloudIdx"].Points.Add(EmptyP);

                chartWT.Series["Temp"].Points.Add(EmptyP);
                chartWT.Series["CloudIdx"].Points.Add(EmptyP);
                chartWT.Series["RGC"].Points.Add(EmptyP);
                chartWT.Series["Wet"].Points.Add(EmptyP);

                //Data in small widget
                txtTemp.Text = String.Empty;
                txtCloudState.Text = String.Empty;
                //Data in large widget
                txtWTTemp.Text = String.Empty;
                txtWTCloudIdx.Text = String.Empty;
                txtWTCaseTemp.Text = String.Empty;
                txtWTPreassure.Text = String.Empty;
                txtWTRGC.Text = String.Empty;
                txtWTWet.Text = String.Empty;
            }

        }

        private bool HadTTCData = false; //was at least once data received?
        /// <summary>
        /// Update TelecopeTempControl state
        /// </summary>
        private void UpdateTelescopeTempControlData()
        {

            DateTime XVal;

            DataPoint EmptyP = new DataPoint { IsEmpty = true, XValue = DateTime.Now.AddSeconds(-5).ToOADate(), YValues = new double[] { 0 } };

            //Read weather station value
            if (ObsControl.objTTCApp.IsRunning() && ObsControl.objTTCApp.CMD_GetSocketData())
            {
                if (!HadTTCData)
                {
                    Logging.AddLog("TelescopeTempControl connected", LogLevel.Activity);
                }
                HadTTCData = true; //flag, that at least one value was received



                //Data in small widget
                txtTTC_W_MainDelta.Text = ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Main.ToString();
                txtTTC_W_SecondDelta.Text = ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Secondary.ToString();

                txtTTC_W_FanRPM.Text = ObsControl.objTTCApp.TelescopeTempControl_State.FAN_RPM.ToString();
                txtTTC_W_Heater.Text = ObsControl.objTTCApp.TelescopeTempControl_State.HeaterPower.ToString();

                //Graphics
                XVal = ObsControl.objTTCApp.TelescopeTempControl_State.LastTimeDataParsed;
                //draw value
                if (ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Main > -100)
                {
                    chartTTC.Series["MainDelta"].Points.AddXY(XVal.ToOADate(), ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Main);
                }
                else
                {
                    chartTTC.Series["MainDelta"].Points.Add(EmptyP);
                }
                if (ObsControl.objTTCApp.TelescopeTempControl_State.FAN_RPM >= 0)
                {
                    chartTTC.Series["FanRPM"].Points.AddXY(XVal.ToOADate(), ObsControl.objTTCApp.TelescopeTempControl_State.FAN_RPM);
                }
                else
                {
                    chartTTC.Series["FanRPM"].Points.Add(EmptyP);
                }

                if (ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Secondary > -100 )
                {
                    chartTTC.Series["SecondDelta"].Points.AddXY(XVal.ToOADate(), ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Secondary);
                }
                else
                {
                    chartTTC.Series["SecondDelta"].Points.Add(EmptyP);
                }
                if (ObsControl.objTTCApp.TelescopeTempControl_State.HeaterPower >= 0)
                {
                    chartTTC.Series["Heater"].Points.AddXY(XVal.ToOADate(), ObsControl.objTTCApp.TelescopeTempControl_State.HeaterPower);
                }
                else
                {
                    chartTTC.Series["Heater"].Points.Add(EmptyP);
                }


                //Data in large widget
                txtTTC_Temp.Text = ObsControl.objTTCApp.TelescopeTempControl_State.Temp.ToString();
                txtTTC_Hum.Text = ObsControl.objTTCApp.TelescopeTempControl_State.Humidity.ToString();

                txtTTC_MainMirrorTemp.Text = ObsControl.objTTCApp.TelescopeTempControl_State.MainMirrorTemp.ToString();
                txtTTC_SecondMirrorTemp.Text = ObsControl.objTTCApp.TelescopeTempControl_State.SecondMirrorTemp.ToString();

                txtTTC_MainMirrorDelta.Text = ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Main.ToString();
                txtTTC_SecondMirrorDelta.Text = ObsControl.objTTCApp.TelescopeTempControl_State.DeltaTemp_Secondary.ToString();

                txtTTC_FanPower.Text = ObsControl.objTTCApp.TelescopeTempControl_State.FAN_FPWM.ToString();
                txtTTC_HeaterPower.Text = ObsControl.objTTCApp.TelescopeTempControl_State.HeaterPower.ToString();
                txtTTC_FanRPM.Text = ObsControl.objTTCApp.TelescopeTempControl_State.FAN_RPM.ToString();
            }
            else if (HadTTCData)
            {

                weatherChart.Series["Temp"].Points.Add(EmptyP);
                weatherChart.Series["CloudIdx"].Points.Add(EmptyP);

                chartWT.Series["Temp"].Points.Add(EmptyP);
                chartWT.Series["CloudIdx"].Points.Add(EmptyP);
                chartWT.Series["RGC"].Points.Add(EmptyP);
                chartWT.Series["Wet"].Points.Add(EmptyP);

                //Data in small widget

                //Data in large widget
                txtTTC_Temp.Text = String.Empty;
                txtTTC_Hum.Text = String.Empty;

                txtTTC_MainMirrorTemp.Text = String.Empty;
                txtTTC_SecondMirrorTemp.Text = String.Empty;

                txtTTC_MainMirrorDelta.Text = String.Empty;
                txtTTC_SecondMirrorDelta.Text = String.Empty;

                txtTTC_FanPower.Text = String.Empty;
                txtTTC_HeaterPower.Text = String.Empty;
                txtTTC_FanRPM.Text = String.Empty;
            }

        }

        /// <summary>
        /// Update PHD state
        /// </summary>
        private void UpdatePHDstate()
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                string curstate = ObsControl.objPHD2App.currentState.ToString();
                txtPHDState.Text = curstate;

                //Check if any pending events
                if (ObsControl.objPHD2App.CheckProgramEvents())
                {
                    //If guiding now, calclulate and display stats
                    if (ObsControl.objPHD2App.currentState == PHDState.Guiding)
                    {
                        double XVal1 = Math.Round(ObsControl.objPHD2App.LastRAError, 2);
                        double YVal1 = Math.Round(ObsControl.objPHD2App.LastDecError, 2);

                        txtGuiderErrorPHD.AppendText(XVal1 + " / " + YVal1 + Environment.NewLine);

                        double XVal = Math.Round(ObsControl.objPHD2App.LastRAError, 2); //* ObsControl.GuidePiexelScale
                        double YVal = Math.Round(ObsControl.objPHD2App.LastDecError, 2); //* ObsControl.GuidePiexelScale

                        GuidingStats.CalculateRMS(XVal, YVal);

                        string sername = "SeriesGuideError";
                        if (Math.Abs(XVal) > 1)
                        {
                            XVal = 2 * Math.Sign(XVal); sername = "SeriesGuideErrorOutOfScale";
                        }
                        if (Math.Abs(YVal) > 1)
                        {
                            YVal = 2 * Math.Sign(YVal); sername = "SeriesGuideErrorOutOfScale";
                        }
                        //draw value
                        chart1.Series[sername].Points.AddXY(XVal, YVal);

                        //display RMS
                        txtRMS_X.Text = Math.Round(GuidingStats.RMS_X, 2).ToString();
                        txtRMS_Y.Text = Math.Round(GuidingStats.RMS_Y, 2).ToString();
                        txtRMS.Text = Math.Round(GuidingStats.RMS, 2).ToString();
                    }
                }
            }
            else
            {
                txtPHDState.Text = "Not running";
            }

        }

        bool aMaximRunning = false;
        bool aPHDRunning = false;

        /// <summary>
        /// Update application status
        /// </summary>
        private void UpdateApplicationsRunningStatus()
        {
            //PHD2 status
            if (ObsControl.objPHD2App.IsRunning())
            {
                linkPHD2.LinkColor = Color.Green;
                btnGuiderConnect.Enabled = true;
                btnGuide.Enabled = true;
            }
            else
            {
                linkPHD2.LinkColor = Color.DeepPink;
                btnGuiderConnect.Enabled = false;
                btnGuide.Enabled = false;
            }
            //PHD Broker status
            if (ObsControl.objPHDBrokerApp.IsRunning()) { linkPHDBroker.LinkColor = Color.Green; } else { linkPHDBroker.LinkColor = Color.DeepPink; }

            //CdC status
            if (ObsControl.objCdCApp.IsRunning()) { linkCdC.LinkColor = Color.Green; } else { linkCdC.LinkColor = Color.DeepPink; }

            //FocusMax status
            if (ObsControl.objFocusMaxApp.IsRunning()) { linkFocusMax.LinkColor = Color.Green; } else { linkFocusMax.LinkColor = Color.DeepPink; }

            //CCDAP status
            if (ObsControl.objCCDAPApp.IsRunning()) { linkCCDAP.LinkColor = Color.Green; } else { linkCCDAP.LinkColor = Color.DeepPink; }

            //MaximDl status
            if (ObsControl.objMaxim.IsRunning()) { linkMaximDL.LinkColor = Color.Green; } else { linkMaximDL.LinkColor = Color.DeepPink; }

            //WeatherStation status
            if (ObsControl.objWSApp.IsRunning()) { linkWeatherStation.LinkColor = Color.Green; } else { linkWeatherStation.LinkColor = Color.DeepPink; }

            //TelescopeTempControl status
            if (ObsControl.objTTCApp.IsRunning()) { linkTelescopeTempControl.LinkColor = Color.Green; } else { linkTelescopeTempControl.LinkColor = Color.DeepPink; }


        }


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
                    if (CheckPowerStatusThread ==null || !CheckPowerStatusThread.IsAlive)
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SocketServer.ListenSocket();
        }



// Block with update elements
#region ///// Update visual elements (Status bar, telescope, etc) /////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates markers in status bar
        /// </summary>
        private void UpdateStatusbarASCOMStatus()
        {
            //SWITCH
            if (ObsControl.Switch_connected_flag)
            {
                toolStripStatus_Switch.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Switch.ForeColor = Color.Gray;
            }
            toolStripStatus_Switch.ToolTipText = "DRIVER: " + ObsControl.SWITCH_DRIVER_NAME + Environment.NewLine;

            //DOME
            if (ObsControl.Dome_connected_flag)
            {
                toolStripStatus_Dome.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Dome.ForeColor = Color.Gray;
            }
            toolStripStatus_Dome.ToolTipText = "DRIVER: " + ObsControl.DOME_DRIVER_NAME+ Environment.NewLine;

            //TELESCOPE
            bool Tprog = (ObsControl.Mount_connected_flag);
            bool Tmaxim = false;
            try
            {
                Tmaxim = (ObsControl.objMaxim.MaximApplicationObj != null && ObsControl.objMaxim.MaximApplicationObj.TelescopeConnected);
            }
            catch { Tmaxim = false; }
            bool Tcdc=false; //later organize checking
            toolStripStatus_Telescope.ToolTipText = "DRIVER: " + ObsControl.TELESCOPE_DRIVER_NAME + Environment.NewLine;
            toolStripStatus_Telescope.ToolTipText += "Control center direct connection: " + (Tprog ? "ON" : "off") + Environment.NewLine;
            toolStripStatus_Telescope.ToolTipText += "Maxim telescope connection: " + (Tmaxim ? "ON" : "off") + Environment.NewLine;
            toolStripStatus_Telescope.ToolTipText += "CdC telescope connection: " + (Tcdc ? "ON" : "off") + Environment.NewLine;

            if (Tprog && Tmaxim && Tcdc)
            {
                toolStripStatus_Telescope.ForeColor = Color.Blue;
            }
            else if (Tprog || Tmaxim || Tcdc)
            {
                toolStripStatus_Telescope.ForeColor = Color.MediumOrchid;
            }
            else
            {
                toolStripStatus_Telescope.ForeColor = Color.Gray;
            }

            //FOCUSER
            bool testFocus=false;
            string FocusSt = "";
            try
            {
                testFocus = (ObsControl.objMaxim.MaximApplicationObj != null && ObsControl.objMaxim.MaximApplicationObj.FocuserConnected);
            }
            catch { testFocus = false; }
            if (testFocus)
            {
                toolStripStatus_Focuser.ForeColor = Color.Blue;
                FocusSt="";//?

            }
            else
            {
                toolStripStatus_Focuser.ForeColor = Color.Gray;
            }
            toolStripStatus_Focuser.ToolTipText = "DRIVER: " + FocusSt + Environment.NewLine;

            //CAMERA
            bool testCamera = ObsControl.objMaxim.TestCamera();
            if (testCamera)
            {
                toolStripStatus_Camera.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Camera.ForeColor = Color.Gray;
            }

        }

        /// <summary>
        /// Updates buttons status
        /// </summary>
        private void UpdatePowerButtonsStatus()
        {
            //Mount
            if (ObsControl.Mount_power_flag == null)
            {
                btnTelescopePower.Enabled = false;
                btnTelescopePower.BackColor = DisabledColor;
            }
            else
            {
                btnTelescopePower.Enabled = true;
                btnTelescopePower.BackColor = ((bool)ObsControl.Mount_power_flag ? OnColor : OffColor);
            }

            //Camera
            if (ObsControl.Camera_power_flag == null)
            {
                btnCameraPower.Enabled = false;
                btnCameraPower.BackColor = DisabledColor;
            }
            else
            {
                btnCameraPower.Enabled = true;
                btnCameraPower.BackColor = ((bool)ObsControl.Camera_power_flag ? OnColor : OffColor);
            }

            //Focuser
            if (ObsControl.Focuser_power_flag == null)
            {
                btnFocuserPower.Enabled = false;
                btnFocuserPower.BackColor = DisabledColor;
            }
            else
            {
                btnFocuserPower.Enabled = true;
                btnFocuserPower.BackColor = ((bool)ObsControl.Focuser_power_flag ? OnColor : OffColor);
            }

            //Roof power
            if (ObsControl.Roof_power_flag == null)
            {
                btnRoofPower.Enabled = false;
                btnRoofPower.BackColor = DisabledColor;
            }
            else
            {
                btnRoofPower.Enabled = true;
                btnRoofPower.BackColor = ((bool)ObsControl.Roof_power_flag ? OnColor : OffColor);
            }

            //All button
            if (ObsControl.Roof_power_flag == true && ObsControl.Mount_power_flag == true && ObsControl.Focuser_power_flag == true && ObsControl.Camera_power_flag == true)
            {
                btnPowerAll.Text = "Depower all";
            }
            else
            {
                btnPowerAll.Text = "Power all";
            }

        }

        /// <summary>
        /// Updates CCD camera status
        /// </summary>
        private void UpdateCCDCameraFieldsStatus()
        {
            if (ObsControl.objMaxim.TestCamera())
            {
                //Binning
                int bin=ObsControl.objMaxim.CCDCamera.BinX;
                txtCameraBinMode.Text = Convert.ToString(bin) + "x" +Convert.ToString(bin);
                //Filters
                try
                {
                    var st = ObsControl.objMaxim.CCDCamera.FilterNames;
                    txtFilterName.Text = Convert.ToString(st[ObsControl.objMaxim.CCDCamera.Filter]);
                }
                catch (Exception ex)
                {
                    txtFilterName.Text = "";
                    Logging.AddLog("Read filters exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                }

                //Cooling
                txtCameraTemp.Text = String.Format("{0:0.0}", ObsControl.objMaxim.GetCameraTemp());
                updownCameraSetPoint.Text = String.Format("{0:0.0}", ObsControl.objMaxim.GetCameraSetpoint());
                txtCameraCoolerPower.Text = String.Format("{0:0}%", ObsControl.objMaxim.GetCoolerPower());

                //Camera current status
                txtCameraStatus.Text = ObsControl.objMaxim.GetCameraStatus();


                txtFilterName.BackColor = OnColor;
                txtCameraBinMode.BackColor = OnColor;
                txtCameraStatus.BackColor = OnColor;

                if (ObsControl.objMaxim.CCDCamera.CoolerOn)
                {
                    txtCameraTemp.BackColor = OnColor;
                    updownCameraSetPoint.BackColor = OnColor;
                    txtCameraCoolerPower.BackColor = OnColor;
                }else
                {
                    txtCameraTemp.BackColor = OffColor;
                    updownCameraSetPoint.BackColor = OffColor;
                    txtCameraCoolerPower.BackColor = OffColor;
                }
            }
            else
            {
                txtCameraTemp.Text = "";
                txtFilterName.Text = "";
                txtCameraBinMode.Text = "";
                updownCameraSetPoint.Text = "";
                txtCameraCoolerPower.Text = "";
                txtCameraStatus.Text = "";

                txtFilterName.BackColor = SystemColors.Control;
                txtCameraBinMode.BackColor = SystemColors.Control;
                txtCameraStatus.BackColor = SystemColors.Control;

                txtCameraTemp.BackColor = SystemColors.Control;
                updownCameraSetPoint.BackColor = SystemColors.Control;
                txtCameraCoolerPower.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Update guider status fields
        /// </summary>
        private void UpdateGuiderFieldsStatus()
        {
            bool testCamera = false;
            try
            {
                testCamera = (ObsControl.objMaxim.CCDCamera != null && ObsControl.objMaxim.CCDCamera.LinkEnabled);
            }
            catch { testCamera = false; }

            if (testCamera)
            {
                ObsControl.objMaxim.GuiderRunnig = ObsControl.objMaxim.CCDCamera.GuiderRunning;

                btnGuider.Text = (ObsControl.objMaxim.GuiderRunnig ? "Guider running" : "Guider stoped");
                btnGuider.BackColor = (ObsControl.objMaxim.GuiderRunnig ? OnColor : OffColor);

                txtGuider_AggX.Text = Convert.ToString(ObsControl.objMaxim.CCDCamera.GuiderAggressivenessX);
                txtGuider_AggY.Text = Convert.ToString(ObsControl.objMaxim.CCDCamera.GuiderAggressivenessY);

                txtGuiderExposure.Text = Convert.ToString(ObsControl.objMaxim.CCDCamera.GuiderAngle); //for now
                txtGuiderLastErrSt.Text=ObsControl.objMaxim.CCDCamera.LastGuiderError;

                if (ObsControl.objMaxim.CCDCamera.GuiderNewMeasurement)
                {
                    ObsControl.objMaxim.GuiderXError=ObsControl.objMaxim.CCDCamera.GuiderXError;
                    ObsControl.objMaxim.GuiderYError = ObsControl.objMaxim.CCDCamera.GuiderYError;

                    string ErrTxt = String.Format("{0:0.00}  {1:0.00}" + Environment.NewLine, ObsControl.objMaxim.GuiderXError, ObsControl.objMaxim.GuiderYError);
                    //if (txtGuiderError.Lines.Count() > 4)
                    //{
                    //    Array.Resize<String>(ref txtGuiderError.Lines,4);
                    //    txtGuiderError.AppendText(ErrTxt);
                    //}
                }

            }
            else
            {
            }
        }


        /// <summary>
        /// Update Telescope Fields and Draw
        /// </summary>
        private void UpdateTelescopeStatus()
        {
            if (ObsControl.objTelescope != null)
            {
                txtTelescopeAz.Enabled = true;
                txtTelescopeAlt.Enabled = true;
                txtTelescopeRA.Enabled = true;
                txtTelescopeDec.Enabled = true;
                btnPark.Enabled = true;
                btnTrack.Enabled = true;

                if (ObsControl.objTelescope.AtPark)
                {
                    //btnPark.Text = "Parked";
                    btnPark.BackColor = OffColor;
                }
                else
                {
                    //btnPark.Text = "UnParked";
                    btnPark.BackColor = OnColor;
                }

                if (ObsControl.objTelescope.Tracking)
                {
                    //btnTrack.Text = "Parked";
                    btnTrack.BackColor = OnColor;
                }
                else
                {
                    //btnTrack.Text = "UnParked";
                    btnTrack.BackColor = OffColor;
                }

                //update fields
                /*
                //txtTelescopeAz.Text = Convert.ToString(Math.Truncate(ObsControl.objTelescope.Azimuth)) + " " + Convert.ToString(Math.Truncatse(ObsControl.objTelescope.Azimuth));
                txtTelescopeAz.Text = ObsControl.ASCOMUtils.DegreesToDMS(ObsControl.objTelescope.Azimuth);
                txtTelescopeAlt.Text = ObsControl.ASCOMUtils.DegreesToDMS(ObsControl.objTelescope.Altitude);
                txtTelescopeRA.Text = ObsControl.ASCOMUtils.HoursToHMS(ObsControl.objTelescope.RightAscension);
                txtTelescopeDec.Text = ObsControl.ASCOMUtils.DegreesToDMS(ObsControl.objTelescope.Declination);

                if (ObsControl.objTelescope.SideOfPier == PierSide.pierEast)
                {
                    txtPierSide.Text = "East, looking West";
                    PoinitingSide = false;
                    VAzAdjust = 180;
                }
                else if (ObsControl.objTelescope.SideOfPier == PierSide.pierWest)
                {
                    txtPierSide.Text = "West, looking East";
                    PoinitingSide = true;
                    VAzAdjust = 0;
                }
                else
                {
                    txtPierSide.Text = "Unknown";
                }

                //Redraw
                angelAz = (Int16)(Math.Round(ObsControl.objTelescope.Azimuth) + NorthAzimuthCorrection + VAzAdjust);
                panelTelescope.Invalidate();

                if (PoinitingSide)
                {
                    angelAlt = (Int16)(Math.Round(ObsControl.objTelescope.Altitude+180));
                }
                else
                {
                    angelAlt = (Int16)(Math.Round(ObsControl.objTelescope.Altitude));
                }
                //HTelescope Az corrections
                if (ObsControl.objTelescope.Azimuth < 90 || ObsControl.objTelescope.Azimuth > 270)
                {
                    angelAlt = 180-angelAlt;
                }
                angelAlt_raw = ObsControl.objTelescope.Altitude;
                */
                calculateTelescopePositionsVars();
                panelTele3D.Invalidate();
            }
            else
            {
                txtTelescopeAz.Enabled = false;
                txtTelescopeAlt.Enabled = false;
                txtTelescopeRA.Enabled = false;
                txtTelescopeDec.Enabled = false;

                btnPark.Enabled = false;
                btnTrack.Enabled = false;
            }

            //init variables (will not draw anyway)
            //DrawTelescopeV(panelTelescopeV);
            //DrawTelescopeH(panelTelescopeH);
        }


        /// <summary>
        /// Update values on settings tab
        /// </summary>
        public void UpdateSettingsTabStatusFileds()
        {
            //ASCOM DATA
            txtSet_Switch.Text = ObsControl.SWITCH_DRIVER_NAME;
            if (ObsControl.Switch_connected_flag == true)
            {
                txtSet_Switch.BackColor = OnColor;
            }
            else
            {
                txtSet_Switch.BackColor = SystemColors.Control;
            }

            txtSet_Dome.Text = ObsControl.DOME_DRIVER_NAME;
            if (ObsControl.Dome_connected_flag == true)
            {
                txtSet_Dome.BackColor = OnColor;
            }
            else
            {
                txtSet_Dome.BackColor = SystemColors.Control;
            }

            txtSet_Telescope.Text = ObsControl.TELESCOPE_DRIVER_NAME;
            if (ObsControl.Mount_connected_flag == true)
            {
                txtSet_Telescope.BackColor = OnColor;
            }
            else
            {
                txtSet_Telescope.BackColor = SystemColors.Control;
            }

            //MAXIM DATA
            if (ObsControl.objMaxim.TestCamera())
            {
                txtSet_Maxim_Camera1.Text =  ObsControl.objMaxim.CCDCamera.CameraName;
                txtSet_Maxim_Camera1.BackColor = (ObsControl.objMaxim.CCDCamera.LinkEnabled ? OnColor:SystemColors.Control);

                txtSet_Maxim_Camera2.Text = ObsControl.objMaxim.CCDCamera.GuiderName;
                txtSet_Maxim_Camera2.BackColor = (ObsControl.objMaxim.CCDCamera.LinkEnabled ? OnColor : SystemColors.Control);

                //txtSet_Maxim_Camera2.Text = ObsControl.objMaxim.CCDCamera.GuiderName;
                //txtSet_Maxim_Camera2.BackColor = (ObsControl.objMaxim.CCDCamera.LinkEnabled ? OnColor : SystemColors.Control);
             }
            else
            {
                txtSet_Maxim_Camera1.Text =  "";
                txtSet_Maxim_Camera1.BackColor = SystemColors.Control;

                txtSet_Maxim_Camera2.Text = "";
                txtSet_Maxim_Camera2.BackColor = SystemColors.Control;
            }


            //testFocus = (ObsControl.objMaxim.MaximApplicationObj != null && ObsControl.objMaxim.MaximApplicationObj.FocuserConnected);
            //testCamera2 = (ObsControl.objMaxim.CCDCamera != null && ObsControl.objMaxim.CCDCamera.LinkEnabled && ObsControl.objMaxim.CCDCamera.GuiderName != "");


        }



        /// <summary>
        /// Update solver messages
        /// </summary>
        public void UpdateSolverFileds()
        {
            //AstroTortilla
            txtATSolver_Status.Text = ObsControl.objAstroTortilla.LastAttemptMessage;
            txtATSolver_Status.BackColor = (ObsControl.objAstroTortilla.LastAttemptSolvedFlag == false ? OffColor : SystemColors.Control);
        }
#endregion update visual elements
            // end of block


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
            Logging.AddLog("Command 'Prepare run' was initiated",LogLevel.Debug);
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

        private void btnConnectTelescope_Click(object sender, EventArgs e)
        {
            if (btnConnectTelescope.Text == "Connect")
            {
                ObsControl.connectMount = true;
                btnConnectTelescope.Text = "Diconnect";
                btnConnectTelescope.BackColor = OnColor;
            }
            else
            {
                ObsControl.connectMount = false;
                btnConnectTelescope.Text = "Connect";
                btnConnectTelescope.BackColor = OffColor;
                btnTrack.BackColor = SystemColors.Control;
                btnPark.BackColor = SystemColors.Control;
            }
        }

#endregion Telescope routines
// End of telescope routines

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
#endregion Status bar event handling

#region //// About information //////////////////////////////////////
        private void LoadAboutData()   
        {
            lblVersion.Text += "Publish version: " + VersionData.PublishVersionSt;
            lblVersion.Text += Environment.NewLine + "Assembly version: " + VersionData.AssemblyVersionSt;
            lblVersion.Text += Environment.NewLine + "File version: " + VersionData.FileVersionSt;
            //lblVersion.Text += Environment.NewLine + "Product version " + ProductVersionSt;
          
            //MessageBox.Show("Application " + assemName.Name + ", Version " + ver.ToString());
            lblVersion.Text += Environment.NewLine+"Compile time: "+VersionData.CompileTime.ToString("yyyy-MM-dd HH:mm:ss");

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
            
            Thread.Sleep(ObsConfig.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 0);

            ObsControl.CommandParser.ParseSingleCommand("PHD2_CONNECT");
        }

        private void linkMaximDL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startMaximDL();
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
#endregion //// AppLinks Events //////////////////////////////////////

        //Change log level control
        private void toolStripDropDownLogLevel_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripDropDownLogLevel.Text = e.ClickedItem.Text;
        }

        private void toolStripStatus_Camera_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.ConnectCamera();
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                SocketServer.Dispose();
            }
            catch { };
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
    }
}
