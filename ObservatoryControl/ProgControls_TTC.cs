using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace ObservatoryCenter
{
    /// <summary>
    /// TelescopeTempControl data fields
    /// </summary>        
    public class TelescopeTempControlData
    {
        public DateTime LastTimeDataParsed;

        public double FAN_RPM = -1; //rpm 0 - 1300
        public double FAN_FPWM = -1; //0-255
        public double HeaterPower = -1; //0-100
        public double HeaterPWM = -1; //0-255
        public bool AutoControl_FanSpeed = false;
        public bool AutoControl_Heater = false;
        public double DeltaTemp_Main = -100.0;
        public double DeltaTemp_Secondary = -100.0;
        public double DewPoint = -100.0;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // TelescopeTempControl class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Weather station class
    /// </summary>
    public class TelescopeTempControl : ExternalApplicationSocketServer
    {
        public TelescopeTempControlData TelescopeTempControl_State;

        public TelescopeTempControl() : base()
        {
            TelescopeTempControl_State = new TelescopeTempControlData();
            LogPrefix = "TTC";
            ServerPort = 1054;
        }


        /// <summary>
        /// Connect equipment
        /// </summary>
        /// <returns></returns>
        public bool CMD_GetBoltwoodString()
        {
            string cmd_string = @"GET_DATA_JSON" + "\r\n";

            string jsonstring = "", ttcstr = "";
            bool res = SendCommand(cmd_string, out jsonstring);
            bool res2 = HandleTTCDataServerResponse(jsonstring, out ttcstr);

            string output = "";

            //Check
            if (!res)
            {
                output = LogPrefix+" get boltwood string error";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);

            }
            else
            {
                output = LogPrefix+" get boltwood string";
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
        public bool HandleTTCDataServerResponse(string responsest, out string result)
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

                    //if (!DateTime.TryParse(BoltwoodState.LastMeasure_s, out BoltwoodState.LastMeasure))
                    //{
                    //    BoltwoodState.LastMeasure = DateTime.MinValue;
                    //}


                    Logging.AddLog(LogPrefix+" message: " + curline, LogLevel.Debug);

                    LastCommand_Result = true;
                    LastCommand_Message = TelescopeTempControl_State.ToString();
                    result = TelescopeTempControl_State.ToString();
                    res = true;
                }
                catch (Exception Ex)
                {
                    Logging.LogExceptionMessage(Ex, LogPrefix+" bad message");

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

