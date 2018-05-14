using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

using LoggingLib;

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
    public partial class Maxim_ExternalApplication : ExternalApplication
    {
        public MaxIm.Application    MaximApplicationObj;    //Application object
        public MaxIm.CCDCamera      CCDCamera;              //Camera object

        public bool TelescopeConnected = false;
        public bool CameraConnected = false;
        public bool FocuserConnected = false;


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



        public Maxim_ExternalApplication() : base()
        { }

        public void Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
        }

        private void MaximLogError(string message, Exception ex, LogLevel ForceLogLevel = LogLevel.Debug)
        {
            //1. Get some reflection information
            StackTrace st = new StackTrace(ex, true);
            StackFrame[] frames = st.GetFrames();
            string messstr = "";

            // Iterate over the frames extracting the information you need
            foreach (StackFrame frame in frames)
            {
                messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
            }

            // 2. Form full message
            string FullMessage = message + Environment.NewLine;
            FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                    + Environment.NewLine + Environment.NewLine + messstr;
            //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

            Logging.AddLog(message + " [" + ex.Message + "]", ForceLogLevel, Highlight.Error);
            Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
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
                MaximLogError("CheckMaximStatus exception!", ex, LogLevel.Important);
            }
        }
        #endregion





        ////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Connect devices and get its status

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
                MaximLogError("MaximDL camera connection failed", ex, LogLevel.Important);

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

                    //Read camera current status
                    ReadCameraStatus();
                }
                catch (Exception ex)
                {
                    MaximLogError("MaximDL camera check connection failed", ex, LogLevel.Important);
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
                MaximLogError("MaximDL telescope connection failed", ex, LogLevel.Important);

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
                    MaximLogError("MaximDL telescope check connection failed", ex, LogLevel.Important);
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
                MaximLogError("MaximDL focuser connection failed", ex, LogLevel.Important);

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
                    MaximLogError("MaximDL focuser check connection failed", ex, LogLevel.Important);
                }
            }
            return res;
        }



        public void ReadCameraStatus()
        {
            try
            {
                if (IsRunning() && MaximApplicationObj != null && CCDCamera != null && this.CameraConnected)
                {
                    //Read camera current status
                    CameraCurrentStatus = CCDCamera.CameraStatus;

                    //if (CameraCurrentStatus == MaxIm.CameraStatusCode.csError)
                    //{
                    //    ConnectCamera(false); // disconnect
                    //    return;
                    //}

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
                MaximLogError("CheckCameraStatus exception!", ex, LogLevel.Important);
            }
        }
        #endregion /// connections ///




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
