using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using System.Diagnostics;

namespace ObservatoryCenter
{
    public class MaximControls
    {
        public MaxIm.Application MaximApplicationObj;
        public MaxIm.CCDCamera CCDCamera;

        MainForm ParentMainForm;

        public double CameraSetTemp = -30;

        public bool GuiderRunnig = false;
        public bool GuiderNewMeasurements = false;
        public double GuiderXError=0.0, GuiderYError = 0.0;



        public MaximControls(MainForm MF)
        {
            MainForm ParentMainForm = MF;
        }

        /// <summary>
        /// Run this method to ready maxim for other methods
        /// </summary>
        public string Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
            Logging.AddLog("MaximDL started", 0);
            return "MaximDL started";
        }


        public string ConnectCamera()
        {
            if (CCDCamera==null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.LinkEnabled = true;
                Logging.AddLog("Camera connected",0);
                Thread.Sleep(1000);
                return "Camera connected";
            }
            catch(Exception ex)
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

                Logging.AddLog("Camera connection failed [" + ex.Message+"]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage,LogLevel.Debug,Highlight.Error);
                return "Camera connection failed";
            }
        }

        public string ConnectTelescope()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try
            {
                MaximApplicationObj.TelescopeConnected = true;
                Logging.AddLog("MaximDL telescope connected", 0);
                Thread.Sleep(1000);
                return "MaximDL telescope connected";
            }
            catch(Exception ex)
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

                Logging.AddLog("MaximDL telescope connection failed [" + ex.Message + "]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "MaximDL telescope connection failed ";
            }
        }

        public string ConnectFocuser()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try{
                MaximApplicationObj.FocuserConnected = true;
                Logging.AddLog("Focuser in MaximDL connected", 0);
                Thread.Sleep(1000);
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

                Logging.AddLog("MaximDL focuser connection failed [" + ex.Message + "]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "MaximDL focuser connection failed";
            }
        }

        public string SetCameraCooling()
        {
            CameraSetTemp = -30.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.CoolerOn = true;
                CCDCamera.TemperatureSetpoint = CameraSetTemp; ////////
                Logging.AddLog("Cooler set to " + CameraSetTemp + " deg", 0);
                return "Cooler set to " + CameraSetTemp + " deg";
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

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "Set camera cooling failed ";
            }
        }
        
        public double GetCameraTemp()
        {
            double getTemp = 200.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try{
                getTemp = CCDCamera.Temperature;
                //Logging.AddLog("Camera temp is " + getTemp + " deg", LogLevel.Critical);
            }
            catch(Exception ex)
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

                Logging.AddLog("Get camera temp failed [" + ex.Message+"]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
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

                Logging.AddLog("Get camera cooler power failed [" + ex.Message + "]", LogLevel.Critical, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            return getPower;
        }


        public string GetCameraStatus()
        {
            MaxIm.CameraStatusCode camStatus = MaxIm.CameraStatusCode.csError;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                camStatus = CCDCamera.CameraStatus;
                Logging.AddLog("Camera status is " + camStatus.ToString() + "", LogLevel.Trace);
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

                string FullMessage = "MaximDL get camera status failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Get camera status failed [" + ex.Message + "]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
            }
            return camStatus.ToString();
        }

        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public double[] GetGuiderAggressiveness()
        {
            double aggresX= 0.0;
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
            return new [] {aggresX, aggresY};
        }

        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public double[] GetGuiderParams()
        {
            double guiderAngel = 0.0;
            double guiderXSpeed= 0.0;
            double guiderYSpeed= 0.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                guiderAngel = CCDCamera.GuiderAngle;
                guiderXSpeed= CCDCamera.GuiderXSpeed;
                guiderYSpeed= CCDCamera.GuiderYSpeed;
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
            return new [] {guiderAngel, guiderXSpeed, guiderYSpeed};
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
            return new[] {0.0, 0.0};
        }

    }
}
