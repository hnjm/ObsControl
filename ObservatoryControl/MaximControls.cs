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

        public MaximControls(MainForm MF)
        {
            MainForm ParentMainForm = MF;
        }

        /// <summary>
        /// Run this method to ready maxim for other methods
        /// </summary>
        public void Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
            Logging.AddLog("MaximDL started", 0);
        }


        public void ConnectCamera()
        {
            if (CCDCamera==null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.LinkEnabled = true;
                Logging.AddLog("Camera connected",0);
                Thread.Sleep(1000);
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

                Logging.AddLog("Camera connection failed [" + ex.Message+"]", 0, Highlight.Error);
                Logging.AddLog(FullMessage,2,Highlight.Error);
            }
        }

        public void ConnectTelescope()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try
            {
                MaximApplicationObj.TelescopeConnected = true;
                Logging.AddLog("Telescope connected", 0);
                Thread.Sleep(1000);
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

                Logging.AddLog("Telescope connection failed [" + ex.Message+"]", 0,Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
        }

        public void ConnectFocuser()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try{
                MaximApplicationObj.FocuserConnected = true;
                Logging.AddLog("Focuser in MaximDL connected", 0);
                Thread.Sleep(1000);
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

                Logging.AddLog("MaximDL focuser connection failed [" + ex.Message+"]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
        }

        public void SetCameraCooling()
        {
            CameraSetTemp = -30.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.CoolerOn = true;
                CCDCamera.TemperatureSetpoint = CameraSetTemp; ////////
                Logging.AddLog("Cooler set to " + CameraSetTemp + " deg", 0);

                //ParentMainForm.AppendLogText("Cooler set to " + SetTemp+" deg");
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

                Logging.AddLog("Set camera cooling failed [" + ex.Message + "]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
        }
        
        public double GetCameraTemp()
        {
            double getTemp = 200.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try{
                getTemp = CCDCamera.Temperature;
                Logging.AddLog("Camera temp is " + getTemp + " deg", 3);
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

                Logging.AddLog("Get camera temp failed [" + ex.Message+"]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
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
                Logging.AddLog("Camera cooler power is " + getPower + "%", 3);
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

                Logging.AddLog("Get camera cooler power failed [" + ex.Message + "]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
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
                Logging.AddLog("Camera status is " + camStatus.ToString() + "", 3);
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
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
            return camStatus.ToString();
        }
    }
}
