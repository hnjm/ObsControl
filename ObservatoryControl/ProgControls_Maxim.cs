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

        public double CameraSetTemp = -30;

        public bool GuiderRunnig = false;
        public bool GuiderNewMeasurements = false;
        public double GuiderXError = 0.0, GuiderYError = 0.0;


        public Maxim_ExternalApplication() : base()
        { }

        public void Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
        }

        /// <summary>
        /// Connect cameras in MaximDL
        /// </summary>
        /// <returns></returns>
        public string ConnectCamera()
        {
            if (CCDCamera==null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.LinkEnabled = true;
                Logging.AddLog("MaximDL camera connected", LogLevel.Activity);
                return "Camera connected";
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
        /// Connect cameras in MaximDL
        /// </summary>
        /// <returns></returns>
        public bool ConnectCameraStatus()
        {
            bool res = false;

            if (CCDCamera != null)
            {
                try
                {
                    res = CCDCamera.LinkEnabled;
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
        /// Connect telescope in MaximDL
        /// </summary>
        /// <returns></returns>
        public bool ConnectTelescopeStatus()
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

/////// Cooling management ////////////////////////////////////////////////////////////////////////////////////////////////////
#region Cooling management

        /// <summary>
        /// Set main camera cooling temperature
        /// </summary>
        public string SetCameraCooling(double SetTemp = -1234.5)
        {
            if (SetTemp == -1234.5) SetTemp = CameraSetTemp;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (CCDCamera.CanSetTemperature)
                {
                    CCDCamera.CoolerOn = true;
                    CCDCamera.TemperatureSetpoint = SetTemp; ////////
                    Logging.AddLog("Cooler set to " + SetTemp + " deg", LogLevel.Debug);
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

        public string CameraCoolingOff(bool WarmUpFlag=false)
        {
            double SetTemp = 50.0;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (WarmUpFlag)
                {
                    if (CCDCamera.CanSetTemperature)
                    {
                        CCDCamera.TemperatureSetpoint = SetTemp;
                        Logging.AddLog("Cooler warmup set to " + SetTemp + " deg", LogLevel.Activity);
                        return "Cooler warmup set to " + SetTemp + " deg";
                    }
                    else
                    {
                        Logging.AddLog("Camera can't set temperature", LogLevel.Activity);
                        return "Camera can't set temperature";
                    }
                }
                else
                {
                    CCDCamera.CoolerOn = false;
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
            return getPower;
        }

#endregion
/////// end of Cooling management ////////////////////////////////////////////////////////////////////////////////////////////////////



        public bool CheckCameraAvailable()
        {
            bool res = false;

            if (CCDCamera == null || MaximApplicationObj == null)
            {
                if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
                if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            }

            try
            {
                res = (CCDCamera != null && CCDCamera.LinkEnabled && CCDCamera.CameraStatus != MaxIm.CameraStatusCode.csError);
            }
            catch (Exception ex)
            {
                res = false;
                CCDCamera = null;
                Logging.AddLog("Test camera exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
            }

            return res;
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


    }
}
