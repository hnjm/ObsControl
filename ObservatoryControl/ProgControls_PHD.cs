using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ObservatoryCenter
{

    public enum PHDState
    {
        Unknown     = 0,
        Stopped     = 1,
        Selected    =2,
        Calibrating =3,
        Guiding     =4,
        LostLock    =5,
        Paused      =6,
        Looping     =7
    }



    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // PHD2 class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// PHD2 class
    /// </summary>
    public class PHD_ExternatApplication : ExternalApplication
    {
        public Int32 ServerPort = 4400; //port to connect socket server
        private Socket ProgramSocket = null;

        public PHDState currentState = PHDState.Unknown;

        public bool LastCommand_Result = false;
        public string LastCommand_Message="";

        public PHD_ExternatApplication() : base()
        { }


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
                Logging.AddLog("Already connected", LogLevel.Activity);
                res = true;
            }
            return res;
        }


        /// <summary>
        /// Send commnad
        /// </summary>
        /// <returns></returns>
        public bool SendCommand(string message)
        {
            bool res = false;
            
            //if wasn't connected earlier, connect again
            if (ProgramSocket == null)
            {
                EstablishConnection();
            }

            Logging.AddLog("PHD2 sending comand: "+message, LogLevel.Debug);

            //Send command
            string output = SocketServerClass.SendToServer(ProgramSocket, message, out Error);

            if (Error >= 0)
            {
                Thread.Sleep(300);

                //Read response
                string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                //Parse response
                HandleServerResponse(output2);

                //Check
                if (!LastCommand_Result)
                {
                    Error = -1;
                    ErrorSt = LastCommand_Message;
                    Logging.AddLog("PHD2 command failed: "+ LastCommand_Message+"]", LogLevel.Debug, Highlight.Error);
                }
                else
                {
                    Error = 0;
                    ErrorSt = "";
                    Logging.AddLog("PHD2 command succesfull", LogLevel.Debug);
                    res = true;
                }
            }
            else
            {
                Logging.AddLog("PHD2 send command error: "+ErrorSt, LogLevel.Debug, Highlight.Error);
            }

            return res;
        }
        /// <summary>
        /// Connect equipment
        /// </summary>
        /// <returns></returns>
        public string CMD_ConnectEquipment()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}" + "\r\n";
            bool res = SendCommand(message);

            string output = "";

            //Check
            if (!res)
            {
                output = "PHD2 equipment connection error";
                Logging.AddLog(output, LogLevel.Important, Highlight.Error);
                
            }
            else
            {
                output = "PHD2 equipment connected";
                Logging.AddLog(output, LogLevel.Activity);
            }

            return output;
        }

        /// <summary>
        /// Check for incoming PHD messages
        /// </summary>
        /// <returns></returns>
        public string CheckProgramEvents()
        {
            //Read response
            string output = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

            //Parse response
            HandleServerResponse(output);

            return currentState.ToString();
        }

        /// <summary>
        /// Handle response string
        /// </summary>
        /// <param name="responsest">Raw string as returned from PHD2</param>
        /// <returns>true if succesfull</returns>
        public bool HandleServerResponse(string responsest)
        {
            bool res = false;

            if (responsest == null) return false;

            //1. Split into lines
            string[] lines = responsest.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            //2. Loop through lines
            foreach (string curline in lines)
            {

                //3. Split response string into parts
                string attribs = "", id = "", st_event = "";
                st_event = ParseServerString(curline, out attribs, out id);

                //Logging.AddLog("PHD2 response. Event: " + st_event + ". Attributes: " + attribs + ". ID: " + id, LogLevel.Trace);

                //4. Parse string into commands
                
                //4. Check for response type
                //4.1. Response on command
                if (st_event == "result" || st_event == "error")
                {
                    Logging.AddLog("PHD2 comand response: [" + st_event + "], attributes: [" + attribs+"]", LogLevel.Debug);
                    if (st_event == "result")
                    {
                        if (attribs == "0")
                            LastCommand_Result = true;
                        else
                            LastCommand_Result = false;

                        LastCommand_Message = attribs;
                        res = true;
                    }
                    else if (st_event == "error")
                    {
                        LastCommand_Result = false;
                        LastCommand_Message = attribs;
                        res = true;
                    }
                }
                else
                //4.2. Events
                {
                    //Parse server events
                    res = ParseServerEvents(st_event, attribs,id);
                    
                    //reset command result
                    LastCommand_Result = false; 
                    LastCommand_Message = "";
                }

            }

            return res;
        }

        /// <summary>
        /// Parse raw string into parts
        /// </summary>
        /// <param name="responsestr"></param>
        /// <param name="attributes"></param>
        /// <param name="id"></param>
        /// <returns>event name or response tag</returns>
        private string ParseServerString(string responsestr, out string attributes, out string id)
        {
            //{"Event":"Version","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"PHDVersion":"2.6.2","PHDSubver":"","MsgVersion":1}
            //{"Event":"CalibrationComplete","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"Mount":"On Camera"}
            //{"Event":"AppState","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"State":"Stopped"}
            //good response: {"jsonrpc":"2.0","result":0,"id":2}
            //bad response:  {"jsonrpc":"2.0","error":{"code":1,"message":"cannot connect equipment when Connect Equipment dialog is open"},"id":1}

            string responsetag = "";
            id = "-1";
            attributes = "";

            if (responsestr != null && responsestr != string.Empty)
            {
                if (responsestr.Length > 7 && responsestr.Substring(2, 5) == "jsonr")
                {
                    //Server response
                    Match match = Regex.Match(responsestr, @"{""jsonrpc"":""(.+)"",""(\w+)"":(.+),""id"":(\d+)}?");
                    if (match.Success)
                    {
                        //Group 0 - all string
                        //Group 1 - JSON RPC protocol version
                        //Group 2 - response tag ("result" or "error")
                        //Group 3 - response text ("0" for result ok, text for error)
                        //Group 4 - id
                        responsetag = match.Groups[2].Value;
                        attributes = match.Groups[3].Value;
                        id = match.Groups[4].Value;
                    }

                }
                else if (responsestr.Length > 7 && responsestr.Substring(2, 5) == "Event")
                {
                    //Event message
                    Match match = Regex.Match(responsestr, @"{""Event"":""(.+)"",""Timestamp"":(.+),""Host"":""(.+)"",""Inst"":(\d+),(.+)}");
                    if (match.Success)
                    {
                        //Group 0 - all string
                        //Group 1 - the name of the event
                        //Group 2 - the timesamp of the event in seconds from the epoch, including fractional seconds
                        //Group 3 - the hostname of the machine running PHD
                        //Group 4 - the PHD instance number
                        //Group 5 - attribute string (e.g. for "Version": "PHDVersion":"2.0.4","PHDSubver":"a","MsgVersion":1)
                        responsetag = match.Groups[1].Value;
                        attributes = match.Groups[5].Value;
                        id = match.Groups[4].Value;
                    }
                }
                else
                {
                    //Unkown message type
                    Logging.AddLog("ParseServerMessages - unknown string [" + responsestr + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                //????
                Logging.AddLog("ParseServerMessages - null or empty string", LogLevel.Debug, Highlight.Error);
            }

            return responsetag;
        }

        /// <summary>
        /// Parse event from server
        /// </summary>
        /// <param name="eventst"></param>
        /// <param name="attributes"></param>
        /// <param name="id"></param>
        /// <returns>true if succesfull</returns>
        private bool ParseServerEvents(string eventst, string attributes, string id)
        {
            //{"Event":"Version","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"PHDVersion":"2.6.2","PHDSubver":"","MsgVersion":1}
            //{"Event":"CalibrationComplete","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"Mount":"On Camera"}
            //{"Event":"AppState","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"State":"Stopped"}
            //{\"Event\":\"LoopingExposures\",\"Timestamp\":1474237179.397,\"Host\":\"MAIN\",\"Inst\":1,\"Frame\":22}
            //{\"Event\":\"LoopingExposures\",\"Timestamp\":1474237180.448,\"Host\":\"MAIN\",\"Inst\":1,\"Frame\":23}
            //{ "Event":"GuideStep","Timestamp":1474237558.050,"Host":"MAIN","Inst":1,"Frame":11,"Time":11.471,"Mount":"On Camera","dx":0.000,"dy":0.000,"RADistanceRaw":-0.000,"DECDistanceRaw":-0.000,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":56546,"SNR":32.88,"AvgDist":0.00}
            bool resparsedflag = false;

            if (eventst == "AppState")
            {
                // "State":"Stopped"
                /* Initial information (only on connect)
                 *  Stopped 	PHD is idle
                 *  Selected 	A star is selected but PHD is neither looping exposures, calibrating, or guiding
                 *  Calibrating 	PHD is calibrating
                 *  Guiding 	PHD is guiding
                 *  LostLock 	PHD is guiding, but the frame was dropped
                 *  Paused 	PHD is paused
                 *  Looping 	PHD is looping exposures
                 */

                string statest = attributes.Substring(9, attributes.Length-10);

                PHDState repState = PHDState.Unknown;
                if (Enum.TryParse(statest, out repState))
                {
                    currentState = repState;
                }
                else
                {
                    currentState = PHDState.Unknown;
                }
                Logging.AddLog("PHD2 state: " + repState.ToString(), LogLevel.Debug);
                resparsedflag = true;

            }
            else if (eventst == "LoopingExposures")
            {
            //LoopingExposures
                currentState = PHDState.Looping;
                Logging.AddLog("PHD2 message: "+ eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "StartGuiding")
            {
                currentState = PHDState.Guiding;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "GuideStep")
            {
                //{ "Event":"GuideStep","Timestamp":1474237558.050,"Host":"MAIN","Inst":1,"Frame":11,"Time":11.471,"Mount":"On Camera","dx":0.000,"dy":0.000,"RADistanceRaw":-0.000,"DECDistanceRaw":-0.000,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":56546,"SNR":32.88,"AvgDist":0.00}
                currentState = PHDState.Guiding;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "GuidingStopped")
            {
                //{"Event":"GuidingStopped","Timestamp":1474237854.500,"Host":"MAIN","Inst":1}
                currentState = PHDState.Paused;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "LoopingExposuresStopped")
            {
                //{"Event":"LoopingExposuresStopped","Timestamp":1474237854.502,"Host":"MAIN","Inst":1}
                currentState = PHDState.Stopped;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }

            else
            {
                Logging.AddLog("PHD2 unkown message: " + eventst + ", attribs: "+ attributes, LogLevel.Debug);
            }

            return resparsedflag;
        }


        public string ConnectEquipment_old()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}";
            message = message + "\r\n";
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, message, out Error);
            string attribs = "", id = "";
            ParseServerString(output, out attribs, out id);
            if (!output.Contains("{\"jsonrpc\":\"2.0\",\"result\":0,\"id\":1}\r\n"))
            {
                Error = -2;
            }
            //good response: {"jsonrpc":"2.0","result":0,"id":2}
            //bad response:  {"jsonrpc":"2.0","error":{"code":1,"message":"cannot connect equipment when Connect Equipment dialog is open"},"id":1}
            ErrorSt = output;
            //Error = 0;
            if (Error < 0)
            {
                Logging.AddLog("PHD2 equipment connection error", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Error: " + ErrorSt, LogLevel.Chat, Highlight.Error);

            }
            else
            {
                Logging.AddLog("PHD2 equipment connected", LogLevel.Activity);
            }
            return output;
        }



    }

}
