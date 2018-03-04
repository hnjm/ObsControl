using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

using LoggingLib;

namespace ObservatoryCenter
{
    static class Program
    {
        private static string appGuid = ((GuidAttribute) Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, appGuid))
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    try
                    {
                        if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                    catch (Exception ex)
                    {
                        Logging.AddLog("Unhandled exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                        Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                        MessageBox.Show("Unhandled exception: " + ex.ToString());
                    }
                }
                else
                {
                    //if already run - set window to foreground
                    Utils.SetCurrentWindowToForeground();
                }

                Logging.DumpToFile();
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
