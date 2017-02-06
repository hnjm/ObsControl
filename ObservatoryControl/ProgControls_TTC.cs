using Newtonsoft.Json;
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

        public double Temp = -100.0;
        public double Humidity = -1.0;
        public double DHT_Temp = -100.0;

        public double MainMirrorTemp = -100.0;
        public double SecondMirrorTemp = -100.0;

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
        /// <summary>
        /// Current sensors and ttc settings state
        /// </summary>
        public TelescopeTempControlData TelescopeTempControl_State;

        public TelescopeTempControl() : base()
        {
            TelescopeTempControl_State = new TelescopeTempControlData();
            LogPrefix = "TTC";
            ServerPort = 1054;
            ParameterString = "-start";
        }


        /// <summary>
        /// Get socket data. After method run, TelescopeTempControl_State would contain current data
        /// </summary>
        /// <returns></returns>
        public bool CMD_GetJSONData()
        {
            string cmd_string = @"GET_DATA_JSON" + "\r\n";

            string jsonstring = "", ttcstr = "";
            
            //Send query and get response
            bool res = SendCommand(cmd_string, out jsonstring);
            
            //Parse response data into object
            bool res2 = Handle_JSON_Data_ServerResponse(jsonstring, out ttcstr);

            //Check result
            string output = "";
            if (!res || !res2)
            {
                output = LogPrefix+" get socket string error";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);

            }
            else
            {
                output = LogPrefix+ " get socket string";
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
        public bool Handle_JSON_Data_ServerResponse(string responsest, out string result)
        {
            bool res = false;
            result = "";

            if (responsest == null) return false;

            //1. Split into lines
            string[] lines = responsest.Split(new string[] { "\r\n", "\n\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            //2. Loop through lines
            foreach (string curline in lines)
            {

                //3. Convert response to JSON
                try
                {
                    //Just for try
                    TelescopeTempControl_State = JsonConvert.DeserializeObject<TelescopeTempControlData>(curline);

                    Logging.AddLog(LogPrefix + " message: " + curline, LogLevel.Debug);

                    LastCommand_Result = true;
                    LastCommand_Message = TelescopeTempControl_State.ToString();
                    result = TelescopeTempControl_State.ToString();
                    res = true;
                }
                catch (Exception Ex)
                {
                    Logging.LogExceptionMessage(Ex, LogPrefix + " bad message");

                    //reset command result
                    LastCommand_Result = false;
                    LastCommand_Message = "";
                    result = "";
                    res = false;
                }

            }

            return res;
        }


        /// <summary>
        /// Send command to switch FAN autocontrol on
        /// </summary>
        /// <returns></returns>
        public string CMD_SetFANControl_ON()
        {
            string cmd_string = @"SET_FAN_AUTOCONTROL 1" + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }

        /// <summary>
        /// Send command to switch FAN autocontrol off
        /// </summary>
        /// <returns></returns>
        internal string CMD_SetFANControl_OFF()
        {
            string cmd_string = @"SET_FAN_AUTOCONTROL 0" + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }

        /// <summary>
        /// Send command to switch HEATER autocontrol ON
        /// </summary>
        /// <returns>resonse string</returns>
        internal string CMD_SetHeaterControl_ON()
        {
            string cmd_string = @"SET_HEATER_AUTOCONTROL 1" + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }

        /// <summary>
        /// Send command to switch HEATER autocontrol OFF
        /// </summary>
        /// <returns>resonse string</returns>
        internal string CMD_SetHeaterControl_OFF()
        {
            string cmd_string = @"SET_HEATER_AUTOCONTROL 0" + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }

        /// <summary>
        /// Send command to set fan level PWR
        /// </summary>
        /// <returns>resonse string</returns>
        internal string CMD_SetFanPWR(string[] CommandString_param_arr)
        {
            int FanPwr = Convert.ToInt16(CommandString_param_arr[0]);
            string cmd_string = @"SET_FAN " + FanPwr + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }

        /// <summary>
        /// Send command to set heater level PWR
        /// </summary>
        /// <returns>resonse string</returns>
        internal string CMD_SetHeaterPWR(string[] CommandString_param_arr)
        {
            int HeaterPwr = Convert.ToInt16(CommandString_param_arr[0]);
            string cmd_string = @"SET_HEATER " + HeaterPwr + "\r\n";

            string output = "";

            bool res = SendCommand(cmd_string, out output);

            //Check
            if (!res)
            {
                output = LogPrefix + " get socket string error (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug, Highlight.Error);
            }
            else
            {
                output = LogPrefix + " get socket string (" + output + ")";
                Logging.AddLog(output, LogLevel.Debug);
            }

            LastCommand_Message = output;
            LastCommand_Result = res;
            return output;
        }
    }
}

