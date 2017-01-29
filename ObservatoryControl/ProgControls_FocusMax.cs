using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FocusMax;
using System.Threading;

using System.Diagnostics;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Focusmax class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Focusmax class
    /// </summary>
    public class FocusMax_ExternalApplication : ExternalApplication
    {
        public FocusMax.FocusControl FocusControlObj;       //Application object
        public FocusMax.Focuser FocuserObj;                 //Focuser object

        public FocusMax_ExternalApplication() : base()
        { }

        /// <summary>
        /// Init main COM objects for FOCUS MAX
        /// </summary>
        public void Init()
        {
            try { 
                if (FocusControlObj == null)  FocusControlObj = new FocusMax.FocusControl();
                if (FocuserObj == null) FocuserObj = new FocusMax.Focuser();
            }
            catch (Exception ex)
            {
                Logging.AddLog("FocusMax COM objects creation error", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }
        }

        /// <summary>
        /// Connect focuser
        /// </summary>
        /// <returns></returns>
        public string ConnectFocuser()
        {
            Init();

            try
            {
                FocuserObj.Link = true;
                Logging.AddLog("Focuser in FocusMax is connected", LogLevel.Activity);

                return "Focuser in FocusMax is connected";
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

                string FullMessage = "FocusMax focuser connection failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("FocusMax focuser connection failed [" + ex.Message + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "FocusMax focuser connection failed";
            }
        }


    }
}
