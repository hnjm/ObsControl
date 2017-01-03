using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Abstract class for all external application
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract class for all external application
    /// </summary>
    public abstract class ExternalApplication
    {
        public string Name = ""; //filename
        public string FullName = ""; //path to run
        public string ParameterString = ""; //parameters if needed


        /// <summary>
        /// Containg error string message if 
        /// </summary>
        public string ErrorSt = "";
        public int Error = 0;

        public bool CanRunMultipleInstances = false; //true to bypass already run check
        private int waitForProgramStartTimeOut = 10000; //how long to wait for program to start

        private Process objProcess = new Process();

        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalApplication()
        { }


        /// <summary>
        /// Run application
        /// </summary>
        /// <returns>true - if process is running (also was run earlier)</returns>
        public bool Run()
        {
            try
            {
                if (!CanRunMultipleInstances && !this.IsRunning())
                {
                    objProcess.StartInfo.FileName = FullName;
                    objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    objProcess.StartInfo.UseShellExecute = false;
                    objProcess.StartInfo.Arguments = ParameterString;
                    objProcess.Start();
                    objProcess.WaitForInputIdle(waitForProgramStartTimeOut); //wait for program to start
                    Logging.AddLog("Process [" + Name + "] started", LogLevel.Activity);
                    ErrorSt = "";
                    Error = 0;
                    return true;
                }
                else
                {
                    ErrorSt = "Process [" + Name + "] already running";
                    Error = 1;
                    Logging.AddLog(ErrorSt, LogLevel.Activity);

                    return true;
                }

            }
            catch (Exception Ex)
            {
                ErrorSt = "Process [" + Name + "] starting error! " + Ex.Message;
                Error = -1;
                Logging.AddLog(ErrorSt, LogLevel.Important, Highlight.Error);

                return false;
            }
        }

        /// <summary>
        /// Check if process is running.
        ///</summary>
        /// <param name="name">Process name. For Maxim it is "Maxim_DL"</param>
        /// <returns></returns>
        public bool IsRunning()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(Name))
                {
                    Logging.AddLog("Process " + Name + " is found", LogLevel.Trace);
                    return true;
                }
            }
            Logging.AddLog("Process " + Name + " not found in running processes", LogLevel.Trace);
            return false;
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Abstract class for all external application with SocketServer Control
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class ExternalApplicationSocketServer : ExternalApplication
    {
        public Int32 ServerPort = 1000; //port to connect socket server
        internal Socket ProgramSocket = null;

        public string LogPrefix = "(some_program)"; //net to be set on object init

        public bool LastCommand_Result = false;
        public string LastCommand_Message = "";

        /// <summary>
        /// Establish connection to server
        /// </summary>
        /// <returns>true if successful</returns>
        public bool EstablishConnection()
        {
            bool res = false;
            if (ProgramSocket == null)
            {
                //Connect
                string output = SocketServerClass.ConnectToServer(IPAddress.Parse("127.0.0.1"), ServerPort, out ProgramSocket, out Error);
                if (Error >= 0)
                {
                    Thread.Sleep(1000);

                    //Read response
                    string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                    //Parse response
                    HandleServerResponse(output2);

                    res = true;
                }
            }
            else
            {
                Logging.AddLog(LogPrefix + " already connected", LogLevel.Activity);
                res = true;
            }
            return res;
        }

        /// <summary>
        /// Handle response string
        /// </summary>
        /// <param name="responsest">Raw string as returned from serber</param>
        /// <returns>true if succesfull</returns>
        public bool HandleServerResponse(string responsest, out string result)
        {
            bool res = false;
            result = "";

            if (responsest == null) return false;

            //1. Split into lines
            string[] lines = responsest.Split(new string[] { "\r\n", "\n\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            //2. Loop through lines
            foreach (string curline in lines)
            {
                LastCommand_Result = true;
                LastCommand_Message = curline.ToString();
                result = curline.ToString();
                res = true;
                Logging.AddLog(LogPrefix + " message: " + curline, LogLevel.Debug);
            }

            return res;
        }


        /// <summary>
        /// Overload HandleServerResponse with only 1 argument (no need in result string)
        /// </summary>
        /// <param name="responsest"></param>
        /// <returns></returns>
        public bool HandleServerResponse(string responsest)
        {
            string dumbstring = "";
            bool res = HandleServerResponse(responsest, out dumbstring);
            return res;
        }



        /// <summary>
        /// Send commnad through Socket Interface
        /// </summary>
        /// <returns></returns>
        public bool SendCommand(string CommandString, out string result)
        {
            bool res = false;

            //if wasn't connected earlier, connect again
            if (ProgramSocket == null)
            {
                if (!EstablishConnection())
                {
                    Logging.AddLog("Failed to connect to "+ LogPrefix, LogLevel.Activity, Highlight.Error);
                    result = "";
                    return false;
                }
            }

            Logging.AddLog(LogPrefix + " sending comand: " + CommandString, LogLevel.Debug);

            //////////////////
            //Send command
            //////////////////
            string output = SocketServerClass.SendToServer(ProgramSocket, CommandString, out Error);

            //Release socket 
            if (ProgramSocket != null && Error != 0)
            {
                ProgramSocket.Shutdown(SocketShutdown.Both);
                ProgramSocket.Close();
                ProgramSocket = null;
            }

            if (Error >= 0)
            {
                //Wait a bit
                Thread.Sleep(300);

                //////////////////
                //Read response
                //////////////////
                string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                //Check
                if (output2 == null || output2 == String.Empty)
                {
                    Error = -1;
                    ErrorSt = LastCommand_Message;
                    result = "";
                    Logging.AddLog(LogPrefix + " command failed: " + LastCommand_Message + "]", LogLevel.Debug, Highlight.Error);
                }
                else
                {
                    Error = 0;
                    ErrorSt = "";
                    result = output2;
                    Logging.AddLog(LogPrefix+" command succesfull", LogLevel.Debug);
                    res = true;
                }
            }
            else
            {
                result = "";
                Logging.AddLog(LogPrefix + " send command error: " + ErrorSt, LogLevel.Debug, Highlight.Error);
            }

            return res;
        }

        /// <summary>
        /// SendCommand overload with only 1 parameter
        /// </summary>
        /// <param name="CommandString"></param>
        /// <returns></returns>
        public bool SendCommand(string CommandString)
        {
            string dumbstring = "";
            bool res = SendCommand(CommandString, out dumbstring);
            return res;
        }


    }


}