using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using System.Diagnostics;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // MaximApp class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// MaximApp class
    /// </summary>
    public class Maxim_ExternalApplication : ExternalApplication
    {
        public MaxIm.Application    MaximApplicationObj;    //Application object
        public MaxIm.CCDCamera      CCDCamera;              //Camera object

        public bool TelescopeConnected = false;
        public bool CameraConnected = false;
        public bool FocuserConnected = false;


        //Cooling status
        public double CameraTemp = -99;
        public double CameraSetPoint = -99;
        public double CameraCoolerPower = 0;
        public bool CameraCoolerOnStatus = false;
        public bool CameraWarmpUpNow = false;

        public double TargetCameraSetTemp = -30;


        //Camera status
        public MaxIm.CameraStatusCode CameraCurrentStatus = MaxIm.CameraStatusCode.csError;
        public string CurrentFilter = "";

        public bool GuiderRunnig = false;
        public bool GuiderNewMeasurements = false;
        public double GuiderXError = 0.0, GuiderYError = 0.0;

        //Camera parameters
        public string CameraName = "";
        public int CameraBin = 0;
        public string GuiderName = "";

        // Threads
        private Thread CheckMaximStatusThread;
        private ThreadStart CheckMaximStatusThread_startref;

        private Thread CheckMaximCoolerStatusThread;
        private ThreadStart CheckMaximCoolerStatusThread_startref;

        public Maxim_ExternalApplication() : base()
        { }

        public void Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
        }


        #region Multithreading ////////////////////////////////////////////////////////////////////////////////////////////


        public void CheckMaximDevicesStatus_async()
        {
            if (IsRunning() && MaximApplicationObj != null)
            {
                try
                {
                    if (CheckMaximStatusThread == null || !CheckMaximStatusThread.IsAlive)
                    {
                        CheckMaximStatusThread_startref = new ThreadStart(CheckMaximDevicesStatus);
                        CheckMaximStatusThread = new Thread(CheckMaximStatusThread_startref);
                        CheckMaximStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in CheckMaximDevicesStatus_async [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
        }

        /// <summary>
        /// Get Maxim Devices status
        /// </summary>
        public void CheckMaximDevicesStatus()
        {
            try
            {
                if (IsRunning() && MaximApplicationObj != null)
                {
                    TelescopeConnected = TelescopeConnectStatus();
                    CameraConnected = CameraConnectStatus();
                    FocuserConnected = FocuserConnectStatus(); 
                }
            }
            catch (Exception ex)
            {
                string FullMessage = "CheckMaximStatus exception!" + Environment.NewLine;
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("CheckMaximStatus exception [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
        }


        public void CheckCameraStatus()
        {
            try
            {
                if (IsRunning() && MaximApplicationObj != null && CCDCamera != null && this.CameraConnected)
                {
                    //Read camera current status
                    CameraCurrentStatus = CCDCamera.CameraStatus;

                    if (CameraCurrentStatus == MaxIm.CameraStatusCode.csError)
                    {
                        ConnectCamera(false); // disconnect
                        return;
                    }

                    //Camera name
                    CameraName = CCDCamera.CameraName;
                    GuiderName = CCDCamera.GuiderName;

                    //Binning
                    CameraBin = CCDCamera.BinX;

                    //Filters
                    try
                    {
                        var st = CCDCamera.FilterNames;
                        CurrentFilter = Convert.ToString(st[CCDCamera.Filter]);
                    }
                    catch (Exception ex)
                    {
                        CurrentFilter = "";
                        Logging.AddLog("Read filters exception: " + ex.Message, LogLevel.Trace, Highlight.Error);
                        //Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                    }
                }
            }
            catch (Exception ex)
            {
                string FullMessage = "CheckCameraStatus exception!" + Environment.NewLine;
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("CheckCameraStatus exception [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
        }

        #endregion





        ////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Connect devices

        /// <summary>
        /// Connect cameras in MaximDL
        /// </summary>
        /// <returns></returns>
        public string ConnectCamera(bool ConnectDisconnectFlag = true)
        {
            if (CCDCamera==null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (ConnectDisconnectFlag)
                { 
                    CCDCamera.LinkEnabled = true;
                    Logging.AddLog("MaximDL camera connected", LogLevel.Activity);
                    return "Camera connected";
                }
                else
                {
                    CCDCamera.LinkEnabled = false;
                    Logging.AddLog("MaximDL camera disconnected", LogLevel.Activity);
                    return "Camera disconnected";
                }
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

                string FullMessage = "MaximDL camera connection failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Camera connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Camera connection failed";
            }
        }

        /// <summary>
        /// Camera connection status in MaximDL
        /// </summary>
        /// <returns></returns>
        private bool CameraConnectStatus()
        {
            bool res = false;

            if (CCDCamera != null)
            {
                try
                {
                    res = (CCDCamera.LinkEnabled && CCDCamera.CameraStatus != MaxIm.CameraStatusCode.csError);
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

                    string FullMessage = "MaximDL camera check connection failed!" + Environment.NewLine;
                    FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                            + Environment.NewLine + Environment.NewLine + messstr;
                    //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                    Logging.AddLog("Camera check connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                }
            }

            CameraConnected = res;
            return res;
        }


        /// <summary>
        /// Connect telescope in MaximDL
        /// </summary>
        /// <returns></returns>
        public string ConnectTelescope()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try
            {
                MaximApplicationObj.TelescopeConnected = true;
                Logging.AddLog("MaximDL telescope connected", LogLevel.Activity);
                return "MaximDL telescope connected";
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

                string FullMessage = "MaximDL telescope connection failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("MaximDL telescope connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "MaximDL telescope connection failed ";
            }
        }

        /// <summary>
        /// Telescope connection status in MaximDL
        /// </summary>
        /// <returns></returns>
        private bool TelescopeConnectStatus()
        {
            bool res = false;
            if (MaximApplicationObj != null)
            {
                try
                {
                    res = MaximApplicationObj.TelescopeConnected;
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

                    string FullMessage = "MaximDL telescope check connection failed!" + Environment.NewLine;
                    FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                            + Environment.NewLine + Environment.NewLine + messstr;
                    //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                    Logging.AddLog("MaximDL telescope check connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                }
            }
            return res;
        }


        /// <summary>
        /// Connect focuser in MaximDL
        /// </summary>
        /// <returns></returns>
        public string ConnectFocuser()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try
            {
                MaximApplicationObj.FocuserConnected = true;
                Logging.AddLog("Focuser in MaximDL connected", LogLevel.Activity);
                
                return "Focuser in MaximDL connected";
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

                string FullMessage = "MaximDL focuser connection failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("MaximDL focuser connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "MaximDL focuser connection failed";
            }
        }

        /// <summary>
        /// Focuser connection state in MaximDL
        /// </summary>
        /// <returns></returns>
        private bool FocuserConnectStatus()
        {
            bool res = false;
            if (MaximApplicationObj != null)
            {
                try
                {
                    res = MaximApplicationObj.FocuserConnected;
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

                    string FullMessage = "MaximDL focuser check connection failed!" + Environment.NewLine;
                    FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                            + Environment.NewLine + Environment.NewLine + messstr;
                    //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                    Logging.AddLog("MaximDL focuser check connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                }
            }
            return res;
        }
        #endregion /// connections ///





        /////// Cooling management ////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Cooling management

        public void checkCameraTemperatureStatus_async()
        {
            if (IsRunning() && MaximApplicationObj != null)
            {
                try
                {
                    if (CheckMaximCoolerStatusThread == null || !CheckMaximCoolerStatusThread.IsAlive)
                    {
                        CheckMaximCoolerStatusThread_startref = new ThreadStart(checkCameraTemperatureStatus);
                        CheckMaximCoolerStatusThread = new Thread(CheckMaximCoolerStatusThread_startref);
                        CheckMaximCoolerStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in checkCameraTemperatureStatus_asyn [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
        }

        private void checkCameraTemperatureStatus()
        {

            //Cooling
            CameraCoolerOnStatus = GetCoolerStatus();

            CameraTemp = GetCameraTemp();
            CameraSetPoint = GetCameraSetpoint();
            CameraCoolerPower = GetCoolerPower();
        }


        public void ToggleCameraCoolingAuto()
        {
        }

        /// <summary>
        /// Switch cooler on, and set main camera cooling temperature
        /// </summary>
        public string CameraCoolingOn(double SetTemp = -1234.5)
        {
            if (SetTemp == -1234.5) SetTemp = TargetCameraSetTemp;
            CameraWarmpUpNow = false;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (CCDCamera.CanSetTemperature)
                {
                    CCDCamera.CoolerOn = true;
                    CCDCamera.TemperatureSetpoint = SetTemp; ////////
                    Logging.AddLog("Camera cooler set to " + SetTemp + " deg", LogLevel.Activity);
                    return "Cooler set to " + SetTemp + " deg";
                }
                else
                {
                    Logging.AddLog("Camera can't set temperature", LogLevel.Debug); //'Debug' to not dublicate messages
                    return "Camera can't set temperature";
                }

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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Set camera cooling failed ";
            }
        }



        /// <summary>
        /// Overload method with string input (from scenarios management)
        /// </summary>
        public string CameraCoolingOn(string[] CommandString_param_arr)
        {
            double SetTemp = -1234.5;
            if (!Double.TryParse(CommandString_param_arr[0], out SetTemp))
                SetTemp = -1234.5;

            return CameraCoolingOn(SetTemp);
        }


        public string CameraCoolingOnGradual_start(double SetTemp = -1234.5)
        {

            double curTemp = GetCameraTemp();

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (CCDCamera.CanSetTemperature)
                {
                    CCDCamera.CoolerOn = true;
                    CCDCamera.TemperatureSetpoint = SetTemp; ////////
                    Logging.AddLog("Camera cooler set to " + SetTemp + " deg", LogLevel.Activity);
                    return "Cooler set to " + SetTemp + " deg";



                }
                else
                {
                    Logging.AddLog("Camera can't set temperature", LogLevel.Debug); //'Debug' to not dublicate messages
                    return "Camera can't set temperature";
                }

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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Set camera cooling failed ";
            }

        }

        /// <summary>
        /// Switch cooler on, and set main camera cooling temperature
        /// </summary>
        public string CameraCoolingOnGradual_step(double SetTemp = -1234.5)
        {
            if (SetTemp == -1234.5) SetTemp = TargetCameraSetTemp;
            CameraWarmpUpNow = false;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (CCDCamera.CanSetTemperature)
                {
                    CCDCamera.CoolerOn = true;
                    CCDCamera.TemperatureSetpoint = SetTemp; ////////
                    Logging.AddLog("Camera cooler set to " + SetTemp + " deg", LogLevel.Activity);
                    return "Cooler set to " + SetTemp + " deg";
                }
                else
                {
                    Logging.AddLog("Camera can't set temperature", LogLevel.Debug); //'Debug' to not dublicate messages
                    return "Camera can't set temperature";
                }

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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Set camera cooling failed ";
            }
        }



        /// <summary>
        /// Switch cooler off
        /// </summary>
        public string CameraCoolingOff(bool WarmUpFlag=false)
        {
            double WarmUpSetTemp = 50.0;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (WarmUpFlag)
                {
                    if (CCDCamera.CanSetTemperature)
                    {
                        CCDCamera.TemperatureSetpoint = WarmUpSetTemp;
                        CameraWarmpUpNow = true;
                        Logging.AddLog("Cooler warmup set to " + WarmUpSetTemp + " deg", LogLevel.Activity);
                        return "Cooler warmup set to " + WarmUpSetTemp + " deg";

                    }
                    else
                    {
                        CameraWarmpUpNow = false;
                        Logging.AddLog("Camera can't set temperature", LogLevel.Activity);
                        return "Camera can't set temperature";
                    }
                }
                else
                {
                    CCDCamera.CoolerOn = false;
                    CameraWarmpUpNow = false;
                    Logging.AddLog("Cooler switched off", LogLevel.Activity);
                    return "Cooler switched off";
                }
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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Set camera cooling failed ";
            }
        }



        /// <summary>
        /// Check if cooler ON/OFF
        /// </summary>
        /// <returns></returns>
        private bool GetCoolerStatus()
        {
            bool getCoolerStatus = false;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getCoolerStatus = CCDCamera.CoolerOn;
                Logging.AddLog("Camera cooler is " + (getCoolerStatus ? "on" : "off"), LogLevel.Trace);
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

                string FullMessage = "MaximDL get camera cooler status failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera cooler status failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            CameraCoolerOnStatus = getCoolerStatus;
            return getCoolerStatus;
        }

        /// <summary>
        /// Get current Setpoint
        /// </summary>
        /// <returns></returns>
        public double GetCameraSetpoint()
        {
            double setTemp = 200.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                setTemp = CCDCamera.TemperatureSetpoint;
                Logging.AddLog("Camera setpoint is " + setTemp + " deg", LogLevel.Trace);
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

                string FullMessage = "MaximDL camera setpoint temp failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Camera get setpoint failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            CameraSetPoint = setTemp;
            return setTemp;
        }

        public double GetCameraTemp()
        {
            double getTemp = 200.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getTemp = CCDCamera.Temperature;
                Logging.AddLog("Camera temp is " + getTemp + " deg", LogLevel.Trace);
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

                string FullMessage = "MaximDL get camera temp failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera temp failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            CameraTemp = getTemp;
            return getTemp;
        }

        public short GetCoolerPower()
        {
            short getPower = -1;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getPower = CCDCamera.CoolerPower;
                Logging.AddLog("Camera cooler power is " + getPower + "%", LogLevel.Trace);
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

                string FullMessage = "MaximDL get camera cooler power failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera cooler power failed [" + ex.Message + "]", LogLevel.Debug, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            CameraCoolerPower = getPower;
            return getPower;
        }

        public bool checkTempNearSetpoint()
        {
            bool res = false;

            if (CameraCoolerOnStatus)
            {
                if (CameraTemp <= CameraSetPoint)
                {
                    res = true;
                }
                else if (CameraTemp >= CameraSetPoint && CameraCoolerPower == 100)
                {
                    res = true;
                }
            }

            return res;
        }

#endregion
        /////// end of Cooling management ////////////////////////////////////////////////////////////////////////////////////////////////////







        #region Maxim Guider. Left for compatability and future needs

        /// <summary>
        /// GetGuiderAggressiveness
        /// </summary>
        /// <returns></returns>
        public double[] GetGuiderAggressiveness()
        {
            double aggresX = 0.0;
            double aggresY = 0.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                aggresX = CCDCamera.GuiderAggressivenessX;
                aggresY = CCDCamera.GuiderAggressivenessY;
                Logging.AddLog("Guider Aggressiveness on X: " + aggresX + " Y: " + aggresY, LogLevel.Trace);
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

                string FullMessage = "MaximDL get guider aggressiveness failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera temp failed [" + ex.Message + "]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            return new[] { aggresX, aggresY };
        }

        /// <summary>
        /// GetGuiderParams
        /// </summary>
        /// <returns></returns>
        public double[] GetGuiderParams()
        {
            double guiderAngel = 0.0;
            double guiderXSpeed = 0.0;
            double guiderYSpeed = 0.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                guiderAngel = CCDCamera.GuiderAngle;
                guiderXSpeed = CCDCamera.GuiderXSpeed;
                guiderYSpeed = CCDCamera.GuiderYSpeed;
                Logging.AddLog("Guider angel: " + guiderAngel + " X speed: " + guiderXSpeed + " Y speed: " + guiderYSpeed, LogLevel.Activity);
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

                string FullMessage = "MaximDL get guider aggressiveness failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera temp failed [" + ex.Message + "]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            return new[] { guiderAngel, guiderXSpeed, guiderYSpeed };
        }

        public double[] GetGuiderState()
        {
            /* 
             * CCDCamera.GuiderMoving (read only, Boolean)
             * Returns True if a the guider is slewing the mount.
             * 
             * CCDCamera.GuiderNewMeasurement (read only, Boolean)
             * Returns true if the autoguider measurements (GuiderXError, GuiderYError) have been updated. 
             * Cleared when the autoguider measurements are read. 

             * CCDCamera.GuiderRunning (read only, Boolean)
             * Returns True if the autoguider is currently running. Returns False if it is currently idle. 
             * Used to check for completion of a guider command such as GuiderCalibrate.
            
             * CCDCamera.GuiderXError (read only, double)
             * Returns the current measured error in the autoguider tracking, in pixels.

             * CCDCamera.GuiderXSpeed [= Single]
             * Sets the guider's X axis speed, in pixels per second.

             * CCDCamera.GuiderXStarPosition (read only, float)
             * Returns the current X coordinate of the guide star, as displayed in the Guide Star X edit box on the Guide tab. 
             * After successful acquisition of a guide star, this will be the X coordinate of the selected star. 
             * During tracking, GuiderXStarPosition may vary if offset tracking is in use. 
             * Note that this quantity reports the desired position of the star, not its actual measured position in the latest tracking exposure.


             */
            return new[] { 0.0, 0.0 };
        }
    #endregion //end of guider block

    }
}
