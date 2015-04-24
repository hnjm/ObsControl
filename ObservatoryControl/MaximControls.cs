using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace ObservatoryCenter
{
    public class MaximControls
    {
        public MaxIm.Application MaximApplicationObj;
        public MaxIm.CCDCamera CCDCamera;

        public MaximControls()
        {
        }

        /// <summary>
        /// Run this method to ready maxim for other methods
        /// </summary>
        public void Init()
        {
            CCDCamera = new MaxIm.CCDCamera();
            MaximApplicationObj = new MaxIm.Application();
        }


        #region Maxim controls
        public void ConnectCamera()
        {
            if (CCDCamera==null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                CCDCamera.LinkEnabled = true;
                Logging.Log("Camera connected",0);
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

                Logging.Log("Camera connection failed! " + ex.Message, 0);
                Logging.Log(FullMessage);
            }
        }

        public void ConnectTelescope()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try
            {
                MaximApplicationObj.TelescopeConnected = true;
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

                Logging.Log("Telescope connection failed! " + ex.Message, 0);
                Logging.Log(FullMessage);
            }
        }

        public void ConnectFocuser()
        {
            if (MaximApplicationObj == null) MaximApplicationObj = new MaxIm.Application();
            try{
                MaximApplicationObj.FocuserConnected = true;
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

                Logging.Log("Focuser connection failed! " + ex.Message, 0);
                Logging.Log(FullMessage);
            }
        }

        public void SetCameraCooling()
        {
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try{
                CCDCamera.CoolerOn = true;
                CCDCamera.TemperatureSetpoint = -30; ////////
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

                Logging.Log("Set camera cooling failed! " + ex.Message, 0);
                Logging.Log(FullMessage);
            }
        }
        #endregion Maxim controls
    }
}
