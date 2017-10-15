using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

namespace ObservatoryCenter
{

    public enum PHDState
    {
        Unknown     = 0,
        Stopped     = 1,
        StarSelected= 2,
        Calibrating = 3,
        Guiding     = 4,
        LostLock    = 5,
        Paused      = 6,
        Looping     = 7,
        Settling    = 8,
        StarLost    = 9,
        Dithered    = 10
    }

    class PHDEvent
    {
        string Event;
        double Timestamp;
        string Host;
        int Inst;
    }

    class PHDEvent_GuideStep : PHDEvent
    {
        int Frame;
        double Time;
        string Mount;
        double dx;
        double dy;
        double RADistanceRaw;
        double DECDistanceRaw;
        double RADistanceGuide;
        double DECDistanceGuide;
        int StarMass;
        double SNR;
        double AvgDist;
        //"dx":-0.019,"dy":-0.003,"RADistanceRaw":0.019,"DECDistanceRaw":0.001,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":89804,"SNR":18.44,"AvgDist":0.13}
    }

    public static class GuidingStats
    {
        internal static double SUM_XX, SUM_YY;
        internal static int NUMX, NUMY;

        public static double RMS_X, RMS_Y, RMS;
        public static double LastRAError, LastDecError;

        public static void Reset()
        {
            SUM_XX = 0; NUMX = 0;
            SUM_YY = 0; NUMY = 0;
            RMS_X = 0;
            RMS_Y = 0;
            RMS = 0;
        }
        public static void CalculateRMS(double XVal, double YVal)
        {
            LastRAError = XVal;
            LastDecError = YVal;

            SUM_XX += XVal * XVal;
            NUMX++; 
            SUM_YY += YVal * YVal;
            NUMY++;

            RMS_X = Math.Sqrt(SUM_XX/NUMX);
            RMS_Y = Math.Sqrt(SUM_YY/NUMY);

            RMS = Math.Sqrt(RMS_X * RMS_X + RMS_Y * RMS_Y);
        }

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

        public bool EquipmentConnected = false;
        // Threads
        private Thread CheckPHDStatusThread;
        private ThreadStart CheckPHDStatusThread_startref;

        public Double LastRAError, LastDecError;

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
                //Make connection to server
                string output = SocketServerClass.ConnectToServer(IPAddress.Parse("127.0.0.1"), ServerPort, out ProgramSocket, out Error);

                if (Error >= 0)
                {
                    Thread.Sleep(600);

                    //Read response
                    string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                    //Parse response
                    Handle_PHD_Response(output2);

                    res = true;
                }
            }
            else
            {
                Logging.AddLog("PHD2 server already connected", LogLevel.Activity);
                res = true;
            }
            return res;
        }


        /// <summary>
        /// Send commnad
        /// </summary>
        /// <returns></returns>
        public bool SendCommand(string message, out string result)
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
                Thread.Sleep(200);

                //Read response
                string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);
                Logging.AddLog("PHD2_SendCommand: server response = " + output2, LogLevel.Debug, Highlight.Error);

                //Parse response
                Handle_PHD_Response(output2, out result);

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
                result = "";
                Logging.AddLog("PHD2 send command error: "+ErrorSt, LogLevel.Debug, Highlight.Error);
            }

            return res;
        }


        /// <summary>
        /// SendCommand overload with only 1 parameter
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendCommand(string message)
        {
            string dumbstring = "";
            bool res = SendCommand(message, out dumbstring);
            return res;
        }


        /// <summary>
        /// Send commnad - just send, no read
        /// </summary>
        /// <returns></returns>
        public bool SendCommand2(string message, out string str_result)
        {
            bool res = false;
            string outMessage = "";

            //if wasn't connected earlier, connect again
            if (ProgramSocket == null)
            {
                EstablishConnection();
            }


            //Send command
            Logging.AddLog("PHD2 sending comand: " + message, LogLevel.Debug);
            int resSrv = SocketServerClass.SendToServer2(ProgramSocket, message, out outMessage);

            if (resSrv >= 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }

            str_result = outMessage;
            return res;
        }


        /// <summary>
        /// Run this method to check if there are incoming PHD messages and parse them
        /// </summary>
        /// <returns></returns>
        public bool CheckProgramEvents()
        {
            bool res = false;
            //Read response
            string output = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

            //Parse response
            if (output != null)
            {
                Handle_PHD_Response(output);
                res = true;

            }

            return res;
        }


        /// <summary>
        /// Handle response string
        /// </summary>
        /// <param name="responsest">Raw string as returned from PHD2</param>
        /// <returns>true if succesfull</returns>
        public bool Handle_PHD_Response(string responsest, out string result, int search_id=0)
        {
            bool res = false;
            result = "";

            if (responsest == null) return false;

            //1. Split into lines
            string[] lines = responsest.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            Logging.AddLog("PHD2_ParseServerResponse: response lines count = " + lines.Count(), LogLevel.Debug, Highlight.Error);

            //2. Loop through lines
            foreach (string curline in lines)
            {
                //3. Split response string into parts
                string attribs = "", id = "", st_event = "";
                st_event = Parse_PHD_ResponseString(curline, out attribs, out id);

                Logging.AddLog("PHD2_ParseResponseString result: st_event = " + st_event + ", attributes = " + attribs + "", LogLevel.Debug, Highlight.Error);

                //in case ID is given
                //search and exit after evaluation
                if (search_id>0 && id == search_id.ToString())
                {
                    if (st_event == "result" || st_event == "error")
                    {
                        if (st_event == "result")
                        {
                            if (attribs == "true")
                            {
                                result = attribs;
                                return true;
                            }else
                            {
                                result = "false";
                                return false;
                            }
                        }
                        else if (st_event == "error")
                        {
                            result = "false";
                            return false;
                        }
                    }
                }

                //Logging.AddLog("PHD2 response. Event: " + st_event + ". Attributes: " + attribs + ". ID: " + id, LogLevel.Trace);

                //4. Parse string into commands

                //4. Check for response type
                //4.1. Response on command
                if (st_event == "result" || st_event == "error")
                {
                    Logging.AddLog("PHD2 comand response: [" + st_event + "], attributes: [" + attribs+"]", LogLevel.Debug);
                    if (st_event == "result")
                    {
                        LastCommand_Result = true;
                        LastCommand_Message = attribs;
                        res = true;
                    }
                    else if (st_event == "error")
                    {
                        LastCommand_Result = false;
                        LastCommand_Message = attribs;
                        res = true;
                    }
                    result = attribs;
                }
                else
                //4.2. Events
                {
                    //Parse server events
                    res = ParseServerEvents(st_event, attribs,id, curline);
                    
                    //reset command result
                    LastCommand_Result = false; 
                    LastCommand_Message = "";
                    result = attribs;
                }

            }

            return res;
        }

        /// <summary>
        /// Overload HandleServerResponse with only 1 argument (no need in result string)
        /// </summary>
        /// <param name="responsest"></param>
        /// <returns></returns>
        public bool Handle_PHD_Response(string responsest)
        {
            string dumbstring = "";
            bool res = Handle_PHD_Response(responsest, out dumbstring);
            return res;
        }


        /// <summary>
        /// Parse raw string into parts
        /// </summary>
        /// <param name="responsestr"></param>
        /// <param name="attributes"></param>
        /// <param name="id"></param>
        /// <returns>event name or response tag</returns>
        private string Parse_PHD_ResponseString(string responsestr, out string attributes, out string id, string jsonstring="")
        {
            //{"Event":"Version","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"PHDVersion":"2.6.2","PHDSubver":"","MsgVersion":1}
            //{"Event":"CalibrationComplete","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"Mount":"On Camera"}
            //{"Event":"AppState","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"State":"Stopped"}
            //{"Event":"LoopingExposures","Timestamp":1474311898.642,"Host":"MAIN","Inst":1,"Frame":11}
            //{"Event":"LoopingExposuresStopped","Timestamp":1474311899.349,"Host":"MAIN","Inst":1}
            //{"Event":"LockPositionLost","Timestamp":1474311899.349,"Host":"MAIN","Inst":1}
            //good response: {"jsonrpc":"2.0","result":0,"id":2}
            //bad response:  {"jsonrpc":"2.0","error":{"code":1,"message":"cannot connect equipment when Connect Equipment dialog is open"},"id":1}
            //{"Event":"GuideStep","Timestamp":1474313655.899,"Host":"MAIN","Inst":1,"Frame":30,"Time":32.081,"Mount":"On Camera","dx":-0.019,"dy":-0.003,"RADistanceRaw":0.019,"DECDistanceRaw":0.001,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":89804,"SNR":18.44,"AvgDist":0.13}

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
                    Match match = Regex.Match(responsestr, @"{""Event"":""(.+)"",""Timestamp"":(.+),""Host"":""(.+)"",""Inst"":(\d+),*(.*)}");
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
        private bool ParseServerEvents(string eventst, string attributes, string id, string jsonstring = "")
        {
            //{"Event":"Version","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"PHDVersion":"2.6.2","PHDSubver":"","MsgVersion":1}
            //{"Event":"CalibrationComplete","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"Mount":"On Camera"}
            //{"Event":"AppState","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"State":"Stopped"}
            //{\"Event\":\"LoopingExposures\",\"Timestamp\":1474237179.397,\"Host\":\"MAIN\",\"Inst\":1,\"Frame\":22}
            //{\"Event\":\"LoopingExposures\",\"Timestamp\":1474237180.448,\"Host\":\"MAIN\",\"Inst\":1,\"Frame\":23}
            //{"Event":"LoopingExposuresStopped","Timestamp":1474311899.349,"Host":"MAIN","Inst":1}
            //{"Event":"LockPositionLost","Timestamp":1474311899.349,"Host":"MAIN","Inst":1}
            //{"Event":"GuideStep","Timestamp":1474313653.803,"Host":"MAIN","Inst":1,"Frame":28,"Time":29.985,"Mount":"On Camera","dx":0.131,"dy":0.172,"RADistanceRaw":-0.168,"DECDistanceRaw":0.140,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":81185,"SNR":19.05,"AvgDist":0.21}
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
                //currentState = PHDState.Guiding;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "GuideStep")
            {
                //{"Event":"GuideStep","Timestamp":1474313653.803,"Host":"MAIN","Inst":1,"Frame":28,"Time":29.985,"Mount":"On Camera","dx":0.131,"dy":0.172,"RADistanceRaw":-0.168,"DECDistanceRaw":0.140,"RADistanceGuide":0.000,"DECDistanceGuide":0.000,"StarMass":81185,"SNR":19.05,"AvgDist":0.21}
                currentState = PHDState.Guiding;
                var json = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(jsonstring);
                LastRAError = (double)json["RADistanceRaw"];
                LastDecError = (double)json["DECDistanceRaw"];
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
            else if (eventst == "Settling")
            {
                currentState = PHDState.Settling;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "StarLost")
            {
                currentState = PHDState.StarLost;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "GuidingDithered")
            {
                currentState = PHDState.Dithered;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            else if (eventst == "StarSelected")
            {
                currentState = PHDState.StarSelected;
                Logging.AddLog("PHD2 message: " + eventst, LogLevel.Debug);
                resparsedflag = true;
            }
            
            else
            {
                Logging.AddLog("PHD2 unkown message: " + eventst + ", attribs: "+ attributes, LogLevel.Debug);
            }

            return resparsedflag;
        }

        /// <summary>
        /// Connect equipment
        /// </summary>
        /// <returns></returns>
        public string CMD_ConnectEquipment()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}" + "\r\n";
            
            bool res = SendCommand(message); //send command to connect equipment to phd2

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
        /// Connect equipment
        /// </summary>
        /// <returns></returns>
        public string CMD_ConnectEquipment2()
        {
            if (!this.IsRunning())
                return "";

            //calc id
            int CMD_ID = 81;
            Random rand = new Random();
            int temp = rand.Next(999);
            int id = CMD_ID * 1000 + temp;

            //make message
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": " + id + "}" + "\r\n";

            //send message to PHD2
            string st_result = "", ret_message="";

            bool resSend = SendCommand2(message, out st_result);
            if (!resSend)
            {
                ret_message = "PHD2 CMD_ConnectEquipment2 send message error: [" + resSend + "]" + st_result;
                Logging.AddLog(ret_message, LogLevel.Debug, Highlight.Error);
                EquipmentConnected = false;
                return ret_message;
            }

            //wait
            Thread.Sleep(200);

            //Read response
            int errorCode = -999;
            string output_from_server = SocketServerClass.ReceiveFromServer(ProgramSocket, out errorCode);
            if (errorCode < 0)
            {
                ret_message = "PHD2 CMD_ConnectEquipment2 receive message error: [" + errorCode + "]";
                Logging.AddLog(ret_message, LogLevel.Debug, Highlight.Error);
                EquipmentConnected = false;
                return ret_message;
            }
            Logging.AddLog("PHD2_SendCommand: server response = " + output_from_server, LogLevel.Debug, Highlight.Error);

            //Parse response
            Handle_PHD_Response(output_from_server, out st_result, id);

            //Check
            if (st_result == "true")
            {
                Error = 0;
                ret_message = "PHD2 equipment connected";
                Logging.AddLog(ret_message, LogLevel.Activity);
            }
            else
            {
                Error = -1;
                ret_message = "PHD2 equipment not connected: " + LastCommand_Message + "]";
                Logging.AddLog(ret_message, LogLevel.Important, Highlight.Error);
            }

            this.EquipmentConnected = (Error == 0);

            return ret_message;
        }


        /// <summary>
        /// get connect equipment status
        /// </summary>
        /// <returns></returns>
        public void CMD_GetConnectEquipmentStatus()
        {
            if (!this.IsRunning())
                return;
            
            //calc id
            int CMD_ID = 80;
            Random rand = new Random();
            int temp = rand.Next(999);
            int id = CMD_ID * 1000 + temp;
            
            //make message
            string message = @"{""method"": ""get_connected"", ""id"": " + id + "}" + "\r\n";

            //send message to PHD2
            string st_result = "";
            bool resSend = SendCommand2(message, out st_result);
            if (!resSend)
            {
                Logging.AddLog("PHD2 CMD_GetConnectEquipmentStatus send message error: ["+ resSend + "]" + st_result, LogLevel.Debug, Highlight.Error);
                EquipmentConnected = false;
                return;
            }

            //wait
            Thread.Sleep(200);

            //Read response
            int errorCode = -999;
            string output_from_server = SocketServerClass.ReceiveFromServer(ProgramSocket, out errorCode);
            if (errorCode<0)
            {
                Logging.AddLog("PHD2 CMD_GetConnectEquipmentStatus receive message error: [" + resSend + "]" + st_result, LogLevel.Debug, Highlight.Error);
                EquipmentConnected = false;
                return;
            }
            Logging.AddLog("PHD2_SendCommand: server response = " + output_from_server, LogLevel.Debug, Highlight.Error);

            //Parse response
            Handle_PHD_Response(output_from_server, out st_result, id);

            //Check
            if (st_result == "true")
            {
                Error = 0;
                ErrorSt = "";
                Logging.AddLog("PHD2 equipment connected", LogLevel.Debug);
            }
            else
            {
                Error = -1;
                ErrorSt = LastCommand_Message;
                Logging.AddLog("PHD2 equipment not connected or error: " + LastCommand_Message + "]", LogLevel.Debug, Highlight.Error);
            }

            this.EquipmentConnected = (Error == 0);

            return;
        }

        /// <summary>
        /// get connect equipment status
        /// </summary>
        /// <returns></returns>
        public void CMD_GetConnectEquipmentStatus_async()
        {
            if (this.IsRunning())
            {
                try
                {
                    if (CheckPHDStatusThread == null || !CheckPHDStatusThread.IsAlive)
                    {
                        CheckPHDStatusThread_startref = new ThreadStart(CMD_GetConnectEquipmentStatus);
                        CheckPHDStatusThread = new Thread(CheckPHDStatusThread_startref);
                        CheckPHDStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in CheckPowerDeviceStatus_async [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }

        }


        /// <summary>
        /// Get pixel scale
        /// </summary>
        /// <returns></returns>
        public double CMD_GetPixelScale()
        {
            //{"method": "get_pixel_scale", "id": 1}
            string message = @"{""method"": ""get_pixel_scale"", ""id"": 1}" + "\r\n";
            double result = -1;
            string result_st = "";
            bool res = SendCommand(message, out result_st);

            string output = "";

            //Check
            if (!res)
            {
                output = "Get pixel scale command error";
                Logging.AddLog(output, LogLevel.Important, Highlight.Error);

            }
            else
            {
                output = "Get pixel scale command result: " + result_st;
                Logging.AddLog(output, LogLevel.Activity);
            }
            
            //Parse data
            if (!Double.TryParse(result_st, out result)) result = -1;

            return result;
        }

        /// <summary>
        /// Get current equipment list
        /// </summary>
        /// <returns></returns>
        public string CMD_GetCurrentProfile()
        {
            //{"method": "get_profile", "id": 1}
            string message = @"{""method"": ""get_profile"", ""id"": 1}" + "\r\n";
            string result = "";
            string result_st = "";
            bool res = SendCommand(message, out result_st);

            string output = "";

            //Check
            if (!res)
            {
                output = "Get Current Profile command error";
                Logging.AddLog(output, LogLevel.Important, Highlight.Error);

            }
            else
            {
                output = "Get Current Profile command result: " + result_st;
                result = result_st;
                Logging.AddLog(output, LogLevel.Activity);
            }

            //Parse data
            //for now without parsings, json string
            return result;
        }
        

        /// <summary>
        /// Star guiding
        /// </summary>
        /// <returns></returns>
        public int CMD_StartGuiding()
        {

            //{"method": "guide", "params": [{"pixels": 1.5, "time": 8, "timeout": 40}, false], "id": 42}
            //{"Event":"LockPositionLost","Timestamp":1474578834.669,"Host":"MAIN","Inst":1}
            //{"jsonrpc":"2.0","result":0,"id":42}
            //{"Event":"LoopingExposures","Timestamp":1474578839.719,"Host":"MAIN","Inst":1,"Frame":1}
            //{"Event":"LockPositionSet","Timestamp":1474578839.879,"Host":"MAIN","Inst":1,"X":343.940,"Y":338.555}
            //{"Event":"LoopingExposures","Timestamp":1474578839.879,"Host":"MAIN","Inst":1,"Frame":1}
            //{"Event":"StarSelected","Timestamp":1474578839.879,"Host":"MAIN","Inst":1,"X":343.940,"Y":338.555}
            //{"Event":"LoopingExposures","Timestamp":1474578845.029,"Host":"MAIN","Inst":1,"Frame":2}
            //{"Event":"LockPositionSet","Timestamp":1474578850.139,"Host":"MAIN","Inst":1,"X":344.368,"Y":338.689}
            //{"Event":"StartGuiding","Timestamp":1474578850.149,"Host":"MAIN","Inst":1}
            //{"Event":"Settling","Timestamp":1474578855.289,"Host":"MAIN","Inst":1,"Distance":0.42,"Time":0.0,"SettleTime":8.0}
            //{"Event":"GuideStep","Timestamp":1474578855.289,"Host":"MAIN","Inst":1,"Frame":1,"Time":5.140,"Mount":"On Camera","dx":0.303,"dy":-0.176,"RADistanceRaw":-0.252,"DECDistanceRaw":-0.236,"RADistanceGuide":-0.159,"DECDistanceGuide":0.000,"RADuration":46,"RADirection":"East","StarMass":71113,"SNR":16.15,"AvgDi
            string message = @"{""method"": ""guide"", ""params"": [{""pixels"": 1.5, ""time"": 8, ""timeout"": 40}, false], ""id"": 1}" + "\r\n";
            int result = -1;
            string result_st = "";
            bool res = SendCommand(message, out result_st);

            string output = "";

            //Check
            if (!res)
            {
                output = "Start guiding command error";
                Logging.AddLog(output, LogLevel.Important, Highlight.Error);

            }
            else
            {
                output = "Start guiding command result: " + result_st;
                Logging.AddLog(output, LogLevel.Activity);
            }
            if (!int.TryParse(result_st, out result)) result = -1;

            return result;
        }

        
        public string ConnectEquipment_old()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}";
            message = message + "\r\n";
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, message, out Error);
            string attribs = "", id = "";
            Parse_PHD_ResponseString(output, out attribs, out id);
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
