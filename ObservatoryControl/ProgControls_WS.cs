using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

using LoggingLib;

namespace ObservatoryCenter
{

    /// <summary>
    /// Boltwood data fields
    /// </summary>        
    public class BoltwoodClass_WS : BoltwoodClass
    {
        public string Bolt_RainFlag_LastDetected_s;
        public string Bolt_WetFlag_LastDetected_s;

        public double WetSensorVal = 0;
        public int RGCVal = -1;
        public double Preassure = 0;

        public string LastMeasure_s;
        public string Web_date = "";
    }

    public enum WetSensorsMode { wetSensBoth = 0, wetSensWetOnly = 1, wetSensRGCOnly = 2 }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // WEATHER STATION class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Weather station class
    /// </summary>
    public class WeatherStation : ExternalApplicationSocketServer
    {
        public BoltwoodClass_WS  BoltwoodState;

        public WeatherStation() : base()
        {
            BoltwoodState = new BoltwoodClass_WS ();
            ServerPort = 1604;
            LogPrefix = "WS";
            ParameterString = "-start";
        }

        /// <summary>
        /// Get data from WS
        /// </summary>
        /// <returns></returns>
        public bool CMD_GetBoltwoodString()
        {
            string message = @"GET_BOLTWOOD_STRING_JSON" + "\r\n";

            string jsonstring = "", boltwstr = "";
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
                    BoltwoodState = (BoltwoodClass_WS )new JavaScriptSerializer().Deserialize(curline, typeof(BoltwoodClass_WS ));
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


