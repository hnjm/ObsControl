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

                Logging.AddLog("Focuser connection failed [" + ex.Message+"]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
        }

        public void SetCameraCooling()
        {
            double SetTemp=-30.0;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try{
                CCDCamera.CoolerOn = true;
                CCDCamera.TemperatureSetpoint = SetTemp; ////////
                Logging.AddLog("Cooler set to " + SetTemp+" deg", 0);

                //ParentMainForm.AppendLogText("Cooler set to " + SetTemp+" deg");
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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("Set camera cooling failed [" + ex.Message+"]", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);
            }
        }
    }
}
