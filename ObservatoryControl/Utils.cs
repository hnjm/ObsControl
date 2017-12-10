using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Deployment;
using System.Deployment.Application;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.Collections.Specialized;

namespace ObservatoryCenter
{
    class AuxilaryProc
    {
        /* ******
         * if HttpUtility would not included, add reference to System.Web manually in references
         * 
         */


        const string _PROGRAM_AUTOSTART_SHORTCUT_NAME = "TempControl autostart";

        /// <summary>
        /// Create .lnk to ClickOnce shortcut with autostart parameters
        /// </summary>
        public static void CreateAutoStartLink()
        {
            Logging.AddLog("CreateAutoStartLink enter", LogLevel.Debug);
            
            try
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                if (ad.IsFirstRun)
                {
                    Logging.AddLog("Creating autostart link file", LogLevel.Debug);

                    Assembly assembly = Assembly.GetEntryAssembly();

                    string company = string.Empty;
                    string description = string.Empty;
                    string productname = string.Empty;
                    string StartMenuShortcutName = string.Empty;

                    // Get company name
                    if (Attribute.IsDefined(assembly, typeof(AssemblyCompanyAttribute)))
                    {
                        AssemblyCompanyAttribute ascompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                        company = ascompany.Company;
                    }

                    // Get shortcut description
                    if (Attribute.IsDefined(assembly, typeof(AssemblyDescriptionAttribute)))
                    {
                        AssemblyDescriptionAttribute asdescription = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
                        description = asdescription.Description;
                    }

                    // Get shortcut product name
                    if (Attribute.IsDefined(assembly, typeof(AssemblyProductAttribute)))
                    {
                        AssemblyProductAttribute asproduct = (AssemblyProductAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));
                        productname = asproduct.Product;
                    }
                    Logging.AddLog(company + " | " + description + " | " + productname, LogLevel.Debug);

                    // Concat clickonce shortcut full path
                    if (!string.IsNullOrEmpty(company))
                    {
                        StartMenuShortcutName = string.Concat(
                                            Environment.GetFolderPath(Environment.SpecialFolder.Programs),
                                            "\\",
                                            company,
                                            "\\",
                                            productname,
                                            ".appref-ms");
                    }


                    //if everything is ok - create shortcut
                    if (!string.IsNullOrEmpty(StartMenuShortcutName))
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        Utils.CreateLink(_PROGRAM_AUTOSTART_SHORTCUT_NAME, desktopPath, StartMenuShortcutName, "-start");
                    }
                    else
                    {
                        Logging.AddLog("Couldn't locate ClickOnce shortcut in start menu",LogLevel.Debug);
                    }
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

                string FullMessage = "Error in creating Autostart link. ";
                FullMessage += "IOException source: " + ex.Data + " | " + ex.Message + " | " + messstr;

                Logging.AddLog(FullMessage,LogLevel.Important);
            }

            Logging.AddLog("CreateAutoStartLink exit", LogLevel.Trace);
        }


        /// <summary>
        /// Test and Parse command line arguments, including usual coomand line and ClickOnce URI parameters passing
        /// </summary>
        /// <param name="outAutoStart">(out) Returns autostart parameter</param>
        /// <param name="outComport">(out) Returns comport name</param>
        public static void CheckStartParams(out bool outAutoStart, out string outComport)
        {
            outAutoStart = false;
            outComport = "";
            //contents deleted
        }
    }

/**************************************************************************************************************************************************
 * Module with program independent utils
 * 
 * 
 ***************************************************************************************************************************************************/
    class Utils
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);


        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
            List<IntPtr> result = new List<IntPtr>();
            int ct = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true && ct < maxCount)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;

                result.Add(currChild);
                prevChild = currChild;

                ++ct;
            }
            return result;
        }

        private enum ShowWindowEnum
        {
            SW_HIDE = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, SW_SHOW = 5,
            SW_MINIMIZE = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            SW_RESTORE = 9, ShowDefault = 10, SW_FORCEMINIMIZE = 11
        };

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        public static void BringWindowToFront(string WindowName)
        {
            BringWindowToFront(WindowName, WindowName, 0);
        }

        public static void BringWindowToFront(string ProcessName, string WindowName, int ChildIndx=0)
        {
            IntPtr handleMainWindow = IntPtr.Zero; //parent window handler
            IntPtr handleTargetWindow = IntPtr.Zero; //target window handler

            IntPtr wdwIntPtr2 = FindWindow(null, WindowName); //main window handler
                                                              //IntPtr wdwIntPtr2 = (IntPtr) 0x216A8; - CCDC handler

            IntPtr wdwIntPtr3 = FindWindow(null, "CCD Commmander - "); //main window handler
            IntPtr wdwIntPtr4 = FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, "CCD Commander");

            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName == ProcessName)
                {
                    handleMainWindow = clsProcess.MainWindowHandle;
                }
            }


            //if ChildIndx is not zero, search child windows
            if (ChildIndx > 0)
            { 
                List<IntPtr> childWindowsList= GetAllChildrenWindowHandles(handleMainWindow, 100);
                if (childWindowsList.Count >= ChildIndx)
                {
                    handleTargetWindow = childWindowsList[ChildIndx - 1];
                }
                else
                {
                    handleTargetWindow = handleMainWindow;
                }
            }
            else
            {
                handleTargetWindow = handleMainWindow;
            }

            ////Get the palcement of window
            //Windowplacement placement = new Windowplacement();
            //GetWindowPlacement(handleTargetWindow, ref placement);

            //// Check if window is minimized
            //if (placement.showCmd == 2)
            //{
            //    //the window is hidden so we restore it
            //    ShowWindow(handleTargetWindow, ShowWindowEnum.SW_RESTORE);
            //}

            ShowWindow(handleMainWindow, ShowWindowEnum.SW_SHOW);  // Make the window visible if it was hidden
            ShowWindow(handleMainWindow, ShowWindowEnum.SW_RESTORE);  // Next, restore it if it was minimized
            SetForegroundWindow(handleMainWindow);  // Finally, activate the window 


            ShowWindow(handleTargetWindow, ShowWindowEnum.SW_SHOW);  // Make the window visible if it was hidden
            ShowWindow(handleTargetWindow, ShowWindowEnum.SW_RESTORE);  // Next, restore it if it was minimized
            SetForegroundWindow(handleTargetWindow);  // Finally, activate the window 

        }


        /// <summary>
        /// Send current process window to foreground using WinAPI functions
        /// </summary>
        public static void SetCurrentWindowToForeground()
        {
            Process current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }
            }
        }


        /// <summary>
        /// Create windows .lnk file (shortcut)
        /// </summary>
        /// <param name="LinkName">The result name of the shortcut</param>
        /// <param name="LinkPath">Path where to place shortcut</param>
        /// <param name="ProgramFullPath">Full path to program</param>
        /// <param name="argm">Command line arguments to pass to program</param>
        static public void CreateLink(string LinkName, string LinkPath, string ProgramFullPath, string argm)
        {
            IShellLink link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription(LinkName);
            link.SetPath(ProgramFullPath);

            if (!string.IsNullOrEmpty(argm))
            {
                link.SetArguments(argm);
            }

            // save it
            IPersistFile file = (IPersistFile)link;
            file.Save(Path.Combine(LinkPath, LinkName + ".lnk"), false);
        }

        /// <summary>
        /// Create URL shortcut of current run program
        /// </summary>
        /// <param name="linkName">The name of the shortcut</param>
        /// <param name="linkPath">Path where to place the shortcut</param>
        public static void CreateURLShortcut(string linkName, string linkPath)
        {
            using (StreamWriter writer = new StreamWriter(linkPath + "\\" + linkName + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");

                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);

                writer.Flush();
            }
        }
    
        /// <summary>
        /// Create ClickOnce Shortcut of the current program on desktop
        /// </summary>
        /// <param name="name"></param>
        public static void CreateClickOnceShortcut(string name)
        {
            ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

            if (ad.IsFirstRun)
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                string company = string.Empty;
                string description = string.Empty;

                // Company name
                if (Attribute.IsDefined(assembly, typeof(AssemblyCompanyAttribute)))
                {
                    AssemblyCompanyAttribute ascompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                    company = ascompany.Company;
                }

                // Description
                if (Attribute.IsDefined(assembly, typeof(AssemblyDescriptionAttribute)))
                {
                    AssemblyDescriptionAttribute asdescription = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
                    description = asdescription.Description;
                }
                
                if (!string.IsNullOrEmpty(company))
                {
                    string desktopPath = string.Empty;
                    desktopPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                    "\\",
                                    description,
                                    ".appref-ms");

                    string shortcutName = string.Empty;
                    shortcutName = string.Concat(
                                     Environment.GetFolderPath(Environment.SpecialFolder.Programs),
                                     "\\",
                                     company,
                                     "\\",
                                     description,
                                     ".appref-ms");

                    System.IO.File.Copy(shortcutName, desktopPath, true);
                }
            }
        }


        /// <summary>
        /// Convert from string to double, but check for alternative separator
        /// </summary>
        /// <param name="Val">double in string format</param>
        /// <returns>double value</returns>
        public static double ConvertToDouble(string Val)
        {
            double DblRes= double.MinValue;
            //1. Try to convert
            if (double.TryParse(Val, out DblRes))
            {
                return DblRes;
            }
            else
            {
                //2.1. Automatic decimal point correction
                char Separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                char BadSeparator = '.';

                if (Separator == '.') { BadSeparator = ','; }
                if (Separator == ',') { BadSeparator = '.'; }

                string Val_st = Val.Replace(BadSeparator, Separator);

                //2.2. Try to convert to double. 
                try
                {
                    DblRes = Convert.ToDouble(Val_st);
                }
                catch (Exception Ex)
                {
                    throw;
                }

                return DblRes;
            }
        }
        public static bool TryParseToDouble(string Val, out double DblRes)
        {
            DblRes = double.MinValue;
            //1. Try to convert
            if (double.TryParse(Val, out DblRes))
            {
                return true;
            }
            else
            {
                //2.1. Automatic decimal point correction
                char Separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                char BadSeparator = '.';

                if (Separator == '.') { BadSeparator = ','; }
                if (Separator == ',') { BadSeparator = '.'; }

                string Val_st = Val.Replace(BadSeparator, Separator);

                //2.2. Try to convert to double. 
                try
                {
                    DblRes = Convert.ToDouble(Val_st);
                    return true;
                }
                catch (Exception Ex)
                {
                    return false;
                }

                return true;
            }
        }

    }

    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

}
