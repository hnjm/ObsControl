using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    static class Program
    {
        /// <summary>
        /// Mutex for controlling one app instance
        /// </summary>
        public static Mutex mutex = new Mutex(true, "{1AFF842A-5B64-4A01-AFEA-8DCD118D97B9}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                //If program isn't already run...

                //Import settings from previously compiled versions
                AuxilaryProc.UpgradeSettings();

                //If it is first run chek for setup
                AuxilaryProc.CreateAutoStartLink();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormWeatherFileControl());

                mutex.ReleaseMutex();
            }
            else
            {
                //if already run - set window to foreground
                Utils.SetCurrentWindowToForeground();
            }

        }
    }
}
