using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

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
            using (Mutex mutex = new Mutex(false, appGuid))
            {
                if (!mutex.WaitOne(1000, false))
                {
                    MessageBox.Show("Already runnig, return");
                    return;
                }

                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Unhandled exception: " + ex.Message, LogLevel.Critical, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                    MessageBox.Show("Unhandled exception: " + ex.ToString());
                }
            }
        }
    }
}
