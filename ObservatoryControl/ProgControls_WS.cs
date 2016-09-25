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

    /// <summary>
    /// Decimal separator override
    /// </summary>
    public enum decimalSeparatorType { useLocale = 0, useDot = 1, useComma = 2 }


    /// <summary>
    /// Boltwood data fields
    /// </summary>        
    public class BoltwoodFields
    {
        public string Bolt_date = "";
        public string Bolt_time = "";

        public double Bolt_SkyTemp = -100; //no direct var
        public double Bolt_Temp = -100; //no direct var
        public double Bolt_CloudIdx = -100;
        public double Bolt_CloudIdxAAG = -100; //no direct var

        public double Bolt_SensorTemp = -100; //no direct var
        public double Bolt_WindSpeed = -100; //no direct var
        public double Bolt_Hum = -100; //no direct var
        public double Bolt_DewPoint = 0.0;
        public UInt16 Bolt_Heater = 0;

        public RainFlag Bolt_RainFlag = RainFlag.rainFlagDry;
        public DateTime Bolt_RainFlag_LastDetected;
        public string Bolt_RainFlag_LastDetected_s;
        public UInt16 Bolt_RainFlag_sinceLastDetected = 65535;

        public WetFlag Bolt_WetFlag = WetFlag.wetFlagDry;
        public DateTime Bolt_WetFlag_LastDetected;
        public string Bolt_WetFlag_LastDetected_s;
        public UInt16 Bolt_WetFlag_sinceLastDetected = 65535;

        public UInt16 Bolt_SinceLastMeasure = 0;
        public double Bolt_now = 0;

        public CloudCond Bolt_CloudCond = CloudCond.cloudUnknown;
        public WindCond Bolt_WindCond = WindCond.windUnknown;
        public RainCond Bolt_RainCond = RainCond.rainUnknown;
        public DayCond Bolt_DaylighCond = DayCond.dayUnknown;

        public UInt16 Bolt_RoofCloseFlag = 0;
        public UInt16 Bolt_AlertFlag = 0;

        public DateTime LastMeasure;
        public string LastMeasure_s;
        public string Web_date = "";
        public decimalSeparatorType ForcedDecimalSeparator = decimalSeparatorType.useLocale;
    }

    /// <summary>
    /// Boltwood Data Types
    /// </summary>
    public enum CloudCond { cloudUnknown = 0, cloudClear = 1, cloudCloudy = 2, cloudVeryCloudy = 3 }
    public enum WindCond { windUnknown = 0, windCalm = 1, windWindy = 2, windVeryWindy = 3 }
    public enum RainCond { rainUnknown = 0, rainDry = 1, rainWet = 2, rainRain = 3 }
    public enum DayCond { dayUnknown = 0, dayDark = 1, dayLight = 2, dayVeryLight = 3 }
    public enum RainFlag { rainFlagDry = 0, rainFlagLastminute = 1, rainFlagRightnow = 2 }
    public enum WetFlag { wetFlagDry = 0, wetFlagLastminute = 1, wetFlagRightnow = 2 }
    public enum WetSensorsMode { wetSensBoth = 0, wetSensWetOnly = 1, wetSensRGCOnly = 2 }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // WEATHER STATION class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Weather station class
    /// </summary>
    public class WeatherStation : ExternalApplication
    {
        public Int32 ServerPort = 1604; //port to connect socket server
        private Socket ProgramSocket = null;

        public BoltwoodFields BoltwoodState;

        public bool LastCommand_Result = false;
        public string LastCommand_Message = "";

        public WeatherStation() : base()
        {
            BoltwoodState = new BoltwoodFields();
        }


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
                Logging.AddLog("WeatherStation already connected", LogLevel.Activity);
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

            Logging.AddLog("WS sending comand: " + message, LogLevel.Debug);

            //Send command
            string output = SocketServerClass.SendToServer(ProgramSocket, message, out Error);

            if (Error >= 0)
            {
                Thread.Sleep(300);

                //Read response
                string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                //Check
                if (output2 == null || output2 == String.Empty )
                {
                    Error = -1;
                    ErrorSt = LastCommand_Message;
                    result = "";
                    Logging.AddLog("WS command failed: " + LastCommand_Message + "]", LogLevel.Debug, Highlight.Error);
                }
                else
                {
                    Error = 0;
                    ErrorSt = "";
                    result = output2;
                    Logging.AddLog("WS command succesfull", LogLevel.Debug);
                    res = true;
                }
            }
            else
            {
                result = "";
                Logging.AddLog("WS send command error: " + ErrorSt, LogLevel.Debug, Highlight.Error);
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
        /// Handle response string
        /// </summary>
        /// <param name="responsest">Raw string as returned from WS</param>
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
                LastCommand_Message = BoltwoodState.ToString();
                result = BoltwoodState.ToString();
                res = true;
                Logging.AddLog("Weather station message: " + curline, LogLevel.Debug);
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
        /// Connect equipment
        /// </summary>
        /// <returns></returns>
        public bool CMD_GetBoltwoodString()
        {
            string message = @"GET_BOLTWOOD_STRING_JSON" + "\r\n";

            string jsonstring = "", boltwstr="";
            bool res = SendCommand(message, out jsonstring);
            bool res2 = HandleBoltwoodDataServerResponse(jsonstring, out boltwstr);

            string output = "";

            //Check
            if (!res)
            {
                output = "WS get boltwood string error";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);

            }
            else
            {
                output = "WS get boltwood string";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return res;
        }

        /// <summary>
        /// Handle response string from weather station with boltwood object
        /// </summary>
        /// <param name="responsest">Raw string as returned from WS</param>
        /// <returns>true if succesfull</returns>
        public bool HandleBoltwoodDataServerResponse(string responsest, out string result)
        {
            bool res = false;
            result = "";

            if (responsest == null) return false;

            //1. Split into lines
            string[] lines = responsest.Split(new string[] { "\r\n", "\n\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            //2. Loop through lines
            foreach (string curline in lines)
            {

                //3. Convert respomnse to JSON
                try
                {
                    //Just for try
                    var json = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(curline);

                    //Convert to BoltwoodField object
                    BoltwoodState = (BoltwoodFields)new JavaScriptSerializer().Deserialize(curline, typeof(BoltwoodFields));
                    //Convert DateTime fields
                    if (!DateTime.TryParse(BoltwoodState.Bolt_RainFlag_LastDetected_s, out BoltwoodState.Bolt_RainFlag_LastDetected))
                    {
                        BoltwoodState.Bolt_RainFlag_LastDetected = DateTime.MinValue;
                    }
                    if (!DateTime.TryParse(BoltwoodState.Bolt_WetFlag_LastDetected_s, out BoltwoodState.Bolt_WetFlag_LastDetected))
                    {
                        BoltwoodState.Bolt_WetFlag_LastDetected = DateTime.MinValue;
                    }
                    if (!DateTime.TryParse(BoltwoodState.LastMeasure_s, out BoltwoodState.LastMeasure))
                    {
                        BoltwoodState.LastMeasure = DateTime.MinValue;
                    }


                    Logging.AddLog("Weather station message: " + curline, LogLevel.Debug);

                    LastCommand_Result = true;
                    LastCommand_Message = BoltwoodState.ToString();
                    result = BoltwoodState.ToString();
                    res = true;
                }
                catch (Exception Ex)
                {
                    Logging.LogExceptionMessage(Ex, "Weather station bad message");

                    //reset command result
                    LastCommand_Result = false;
                    LastCommand_Message = "";
                    result = "";
                    res = false;
                }

            }

            return res;
        }

    }
}


