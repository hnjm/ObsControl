using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

using ASCOM;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;



namespace RollOffRoof_IP9212_2
{
    class IP9212_hardware_2
    {
        public string IP9212_switch_id = "IP9212_switch";

        public string ip_addr, ip_port, ip_login, ip_pass;
        public string switch_port, opened_port, closed_port;
        public string switch_port_state_type, opened_port_state_type, closed_port_state_type;

        //input sensors state
        private int[] input_state_arr = new int[1] { -1 };
            // [0] - overall read status
            // [1..8] - status of # input
        
        public bool hardware_connected_flag=false;

        public static Semaphore IP9212Semaphore;

        //error message (on hardware level) - don't forget, that there is another one on driver level
        //all this done for saving error message text during exception and display it to user (MaximDL tested)
        public string ASCOM_ERROR_MESSAGE = "";

        public bool opened_shutter_flag;
        public bool closed_shutter_flag;

        public IP9212_hardware_2()
        {
            Trace("> IP9212_harware.constructor: enter");
            hardware_connected_flag = false; 
            readSettings();
            IP9212Semaphore = new Semaphore(1, 2, "ip9212");
            Trace("> IP9212_harware.constructor: exit");
        }

        public void readSettings()
        {
            Trace("> IP9212_harware.readSettings: enter");
            using (ASCOM.Utilities.Profile p = new Profile())
            {
                System.Collections.ArrayList T = p.RegisteredDeviceTypes;

                p.DeviceType = "Switch";
                ip_addr = p.GetValue(IP9212_switch_id, "ip_addr");
                ip_port = p.GetValue(IP9212_switch_id, "ip_port");
                ip_login = p.GetValue(IP9212_switch_id, "ip_login");
                ip_pass = p.GetValue(IP9212_switch_id, "ip_pass");

                switch_port = p.GetValue(IP9212_switch_id, "switch_port");
                opened_port = p.GetValue(IP9212_switch_id, "opened_port");
                closed_port = p.GetValue(IP9212_switch_id, "closed_port");

                switch_port_state_type = p.GetValue(IP9212_switch_id, "switch_port_state_type");
                opened_port_state_type = p.GetValue(IP9212_switch_id, "opened_port_state_type");
                closed_port_state_type = p.GetValue(IP9212_switch_id, "closed_port_state_type");

                Trace("> IP9212_harware.readSettings: ip: " + ip_addr + "      flag:" + opened_port_state_type);
                Trace("> IP9212_harware.readSettings: exit");
            }
        }

        /// <summary>
        /// Check the availability of IP server by starting async read from input sensors. Result handeled to checkLink_DownloadCompleted()
        /// </summary>
        /// <returns>Nothing</returns> 
        public void checkLink_async()
        {
            Trace("> IP9212_harware.checkLink_async(): enter");
            
            //Check - address was specified?
            if (string.IsNullOrEmpty(ip_addr))
            {
                hardware_connected_flag = false;
                Trace("> IP9212_harware.checkLink_async(): ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                //throw new ASCOM.DriverException("checkLink_async error");
                return;
            }
            
            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getio";
            //FOR DEBUGGING
            siteipURL = "http://localhost/ip9212/getio.php";
            
            Uri uri_siteipURL = new Uri(siteipURL);
            Trace("> IP9212_harware.checkLink_async() download url:" + siteipURL);

            // Send http query
            WebClient client = new WebClient();
            try
            {
                IP9212Semaphore.WaitOne(); // lock working with IP9212
                
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(checkLink_DownloadCompleted);

                client.DownloadDataAsync(uri_siteipURL);

                Trace("> IP9212_harware.checkLink_async(): http request was sent");
            }
            catch (WebException e)
            {
                IP9212Semaphore.Release();//unlock ip9212 device for others
                hardware_connected_flag = false;
                Trace("> IP9212_harware.checkLink_async() error:" + e.Message);
                //throw new ASCOM.NotConnectedException("Couldn't reach network server");
                Trace("> IP9212_harware.checkLink_async(): exit by web error ");
            }
        }

        private void checkLink_DownloadCompleted(Object sender, DownloadDataCompletedEventArgs e)
        {
            IP9212Semaphore.Release();//unlock ip9212 device for others

            Trace("> IP9212_harware.checkLink_DownloadCompleted(): http request was processed");
            if (e.Error != null)
            {
                hardware_connected_flag = false;
                Trace("> IP9212_harware.checkLink_DownloadCompleted() error: " + e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                string downloadedData = Encoding.Default.GetString(e.Result);
                if (downloadedData.IndexOf("P5") >= 0)
                {
                    hardware_connected_flag = true;
                    Trace("> IP9212_harware.checkLink_DownloadCompleted(): ok ");
                }
                else
                {
                    hardware_connected_flag = false;
                    Trace("> IP9212_harware.checkLink_DownloadCompleted(): string not found");
                }
            }
            else
            {
                Trace("> IP9212_harware.checkLink_DownloadCompleted(): bad result");
                hardware_connected_flag = false;
            }
            return;
        }

        /// <summary>
        /// Check the availability of IP server by straight read (NON ASYNC manner)
        /// </summary>
        /// <returns>Aviability of IP server </returns> 
        public bool checkLink_forced()
        {
            Trace("> IP9212_harware.checkLink_forced(): enter");

            //Check - address was specified?
            if (string.IsNullOrEmpty(ip_addr))
            {
                hardware_connected_flag = false;
                Trace("> IP9212_harware.checkLink_forced(): ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                //throw new ASCOM.DriverException("checkLink_async error");
                return hardware_connected_flag;
            }
            
            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getio";
            //FOR DEBUGGING
            siteipURL = "http://localhost/ip9212/getio.php";
            
            Uri uri_siteipURL = new Uri(siteipURL);
            Trace("> IP9212_harware.checkLink_forced() download url:" + siteipURL);

            // Send http query
            IP9212Semaphore.WaitOne(); // lock working with IP9212

            string s = "";
            WebClient client = new WebClient();
            try
            {
                Stream data = client.OpenRead(uri_siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                Trace("> IP9212_harware.checkLink_forced() download str:" + s);
                //wait
                //Thread.Sleep(1000);

                IP9212Semaphore.Release();//unlock ip9212 device for others

                if (s.IndexOf("P5") >= 0)
                {
                    hardware_connected_flag = true;
                    Trace("> IP9212_harware.checkLink_forced(): check downloaded data: ok ");
                }
                else
                {
                    hardware_connected_flag = false;
                    Trace("> IP9212_harware.checkLink_forced(): check downloaded data: string not found");
                }
            }
            catch (WebException e)
            {
                IP9212Semaphore.Release();//unlock ip9212 device for others
                hardware_connected_flag = false;
                Trace("> IP9212_harware.checkLink_forced() error:" + e.Message);
                //throw new ASCOM.NotConnectedException("Couldn't reach network server");
                Trace("> IP9212_harware.checkLink_forced(): exit by web error ");
            }
            return hardware_connected_flag;
        }


        /// <summary>
        /// Get input sensor status
        /// </summary>
        /// <returns>Returns int array [0..8] with status flags of each input sensor. arr[0] is for read status (-1 for error, 1 for good read, 0 for smth else)</returns> 
        public int[] getInputStatus()
        {
            Trace("> IP9212_harware.getInputStatus(): enter");


            if (string.IsNullOrEmpty(ip_addr))
            {
                input_state_arr[0] = -1;
                Trace("> IP9212_harware.getInputStatus(): ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "getInputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return input_state_arr;
            }
            
            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getio";
            //FOR DEBUGGING
            siteipURL = "http://localhost/ip9212/getio.php";
            Trace("> IP9212_harware.getInputStatus() download url:" + siteipURL);

            // Send http query
            IP9212Semaphore.WaitOne(); // lock working with IP9212
            string s = "";
            WebClient client = new WebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                Trace("> IP9212_harware.getInputStatus() download str:" + s);
                IP9212Semaphore.Release();//unlock ip9212 device for others
                //wait
                //Thread.Sleep(1000);
            }
            catch (WebException e)
            {
                IP9212Semaphore.Release();//unlock ip9212 device for others
                input_state_arr[0] = -1;
                Trace("> IP9212_harware.getInputStatus() error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "getInputStatus(): couldn't reach network server";
                throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                Trace("> IP9212_harware.getInputStatus(): exit by web error ");
                return input_state_arr;
                // report a problem with the port name (never get there)
            }
            
            // Parse data
            try
            {
                // Parse result string
                string[] stringSeparators = new string[] { "P5" };
                string[] iprawdata_arr = s.Split(stringSeparators, StringSplitOptions.None);

                Array.Resize(ref input_state_arr,iprawdata_arr.Length);

                //Parse an array
                for (var i = 1; i < iprawdata_arr.Length; i++)
                {
                    //Убираем запятую
                    if (iprawdata_arr[i].Length > 3)
                    {
                        iprawdata_arr[i] = iprawdata_arr[i].Substring(0, 3);
                    }
                    //Trace(iprawdata_arr[i]);

                    //Разбиваем на пары "номер порта"="значение"
                    char[] delimiterChars = { '=' };
                    string[] data_arr = iprawdata_arr[i].Split(delimiterChars);
                    //st = st + " |" + i + ' ' + data_arr[1];
                    if (data_arr.Length > 1)
                    {
                        input_state_arr[i] = Convert.ToInt16(data_arr[1]);
                        Trace(input_state_arr[i]);
                    }
                    else
                    {
                        input_state_arr[i] = -1;
                    }
                }
                input_state_arr[0] = 1;
                Trace("> IP9212_harware.getInputStatus(): data was read");
            } catch
            {
                Trace("> IP9212_harware.getInputStatus(): ERROR (Exception)!");
                input_state_arr[0] = -1;
                Trace("> IP9212_harware.getInputStatus(): exit by parse error ");
                return input_state_arr;
            }
            Trace("> IP9212_harware.getInputStatus(): exit");
            return input_state_arr;
        }

        /// <summary>
        /// Get output relay status
        /// </summary>
        /// <returns>Returns int array [0..8] with status flags of each realya status. arr[0] is for read status (-1 for error, 1 for good read, 0 for smth else)</returns> 
        public int[] getOutputStatus()
        {
            Trace("> IP9212_harware.getOutputStatus(): enter");

            // get the ip9212 settings from the profile
            //readSettings();

            //return data
            int[] ipdata = new int[1] { 0 };

            if (string.IsNullOrEmpty(ip_addr))
            {
                ipdata[0] = -1;
                Trace("> IP9212_harware.getOutputStatus(): ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "getOutputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return input_state_arr;

            }
            string siteipURL;
            siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=getpower";
            //FOR DEBUGGING
            siteipURL = "http://localhost/ip9212/getpower.php";

            Trace("> IP9212_harware.getOutputStatus() download url:" + siteipURL);

            // Send http query
            IP9212Semaphore.WaitOne(); // lock working with IP9212
            string s = "";
            WebClient client = new WebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                Trace("> IP9212_harware.getOutputStatus() download str:" + s);
                IP9212Semaphore.Release();//unlock ip9212 device for others
                //wait
                //Thread.Sleep(1000);

            }
            catch (WebException e)
            {
                IP9212Semaphore.Release();//unlock ip9212 device for others
                ipdata[0] = -1;
                Trace("> IP9212_harware.getOutputStatus() error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "getInputStatus(): Couldn't reach network server";
                throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                    Trace("> IP9212_harware.getOutputStatus(): exit by web error");
                    return ipdata;
            }

            // Parse data
            try
            {
                string[] stringSeparators = new string[] { "P6" };
                string[] iprawdata_arr = s.Split(stringSeparators, StringSplitOptions.None);

                Array.Resize(ref ipdata, iprawdata_arr.Length);

                //Parse an array
                for (var i = 1; i < iprawdata_arr.Length; i++)
                {
                    //Убираем запятую
                    if (iprawdata_arr[i].Length > 3)
                    {
                        iprawdata_arr[i] = iprawdata_arr[i].Substring(0, 3);
                    }
                    //Console.WriteLine(iprawdata_arr[i]);

                    //Разбиваем на пары "номер порта"="значение"
                    char[] delimiterChars = { '=' };
                    string[] data_arr = iprawdata_arr[i].Split(delimiterChars);
                    //st = st + " |" + i + ' ' + data_arr[1];
                    if (data_arr.Length > 1)
                    {
                        ipdata[i] = Convert.ToInt16(data_arr[1]);
                        Trace(ipdata[i]);
                    }
                    else
                    {
                        ipdata[i] = -1;
                    }
                }
                ipdata[0] = 1;
                Trace("> IP9212_harware.getOutputStatus(): data was read");
            }
            catch
            {
                ipdata[0] = -1;
                Trace("> IP9212_harware.getOutputStatus(): ERROR (Exception)!");
                Trace("> IP9212_harware.getOutputStatus(): exit by parse error");
                return ipdata;
            }
            return ipdata;
        }


        /// <summary>
        /// Chage output relay state
        /// </summary>
        /// <param name="PortNumber">Relay port number, int [1..9]</param>
        /// <param name="PortValue">Port value [0,1]</param>
        /// <returns>Returns true in case of success</returns> 
        public bool setOutputStatus(int PortNumber, int PortValue)
        {
            Trace("> IP9212_harware.setOutputStatus(" + PortNumber + "," + PortValue + "): enter");

            // get the ip9212 settings from the profile
            //readSettings();

            //return data
            bool ret = false;

            if (string.IsNullOrEmpty(ip_addr))
            {
                Trace("> IP9212_harware.setOutputStatus(" + PortNumber + "," + PortValue + "): ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "setOutputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return ret;
            }
            string siteipURL = "http://" + ip_login + ":" + ip_pass + "@" + ip_addr + ":" + ip_port + "/set.cmd?cmd=setpower+P6" + PortNumber + "=" + PortValue;
            //FOR DEBUGGING
            siteipURL = "http://localhost/ip9212/set.php?cmd=setpower+P6" + PortNumber + "=" + PortValue;


            // Send http query
            IP9212Semaphore.WaitOne(); // lock working with IP9212
            string s = "";
            WebClient client = new WebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                Trace("> IP9212_harware.setOutputStatus(" + PortNumber + "," + PortValue + ") download str:" + s);
                //wait
                //Thread.Sleep(1000);
                IP9212Semaphore.Release();//unlock ip9212 device for others

                ret = true;
            }
            catch (WebException e)
            {
                IP9212Semaphore.Release();//unlock ip9212 device for others
                ret = false;
                Trace("> IP9212_harware.setOutputStatus(" + PortNumber + "," + PortValue + ") error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "setOutputStatus(" + PortNumber + "," + PortValue + "): Couldn't reach network server";
                throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                    Trace("> IP9212_harware.setOutputStatus(" + PortNumber + "," + PortValue + "): exit by web error");
                    return ret;
                // report a problem with the port name (never get there)
            }
            // Parse data
            // not implemented yet

            return ret;
        }

        /// <summary>
        /// Press switch button to open/close roof
        /// </summary>
        /// <returns>Returns true in case of success</returns> 
        //press switch
        public bool pressSwitch()
        {
            Trace("> IP9212_harware.pressSwitch(): enter");

            //Get config data
            int PortNumber = 0;
            bool bool_switch_port_state_type;
            try
            {
                
                PortNumber = Convert.ToInt16(switch_port);
                bool_switch_port_state_type = Convert.ToBoolean(switch_port_state_type);
            }
            catch (FormatException e)
            {
                Trace("> IP9212_harware.pressSwitch(): port number or port state bad format [" + e.Message + "]");
                ASCOM_ERROR_MESSAGE = "pressSwitch(): port number or port state bad format [" + e.Message + "]";
                throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
            }
            int int_switch_port_state_type = (bool_switch_port_state_type ? 0 : 1);
            int int_inverted_switch_port_state_type = (bool_switch_port_state_type ? 1 : 0);

            //read output states
            int[] outStates=getOutputStatus();
            int curPortState = outStates[PortNumber];
            Trace("> IP9212_harware.pressSwitch() curPortState: " + curPortState);

            //check - what is the state of switch port?
            if (outStates[PortNumber] != int_switch_port_state_type)
            {
                //return to normal value
                Trace("> IP9212_harware.pressSwitch(): first need to return switch to normal state");
                setOutputStatus(PortNumber, int_switch_port_state_type);
            }

            //press switch
            Trace("> IP9212_harware.pressSwitch(): press");
            setOutputStatus(PortNumber, int_inverted_switch_port_state_type);

            //wait
            Thread.Sleep(1000);

            //release switch
            Trace("> IP9212_harware.pressSwitch(): release");
            setOutputStatus(PortNumber, int_switch_port_state_type);

            Trace("> IP9212_harware.pressSwitch(): exit");
            return true;
        }

        /// <summary>
        /// return true if OPENNED STATE SENSOR signaling 
        /// </summary>
        /// <returns>Returns true in case of opened state signaling, false otherwise</returns> 
        public bool OpenedSensorState()
        {
            Trace("> IP9212_harware.OpenedSensorState(): enter");
           
            //read OPENED_PORT value
            int int_opened_port = 0;
            try
            {
                int_opened_port = Convert.ToInt16(opened_port);
            }
            catch (FormatException e)
            {
                Trace("> IP9212_harware.OpenedSensorState(): Input string is not a sequence of digits [" + e.Message + "]");
                ASCOM_ERROR_MESSAGE="OpenedSensorState(): opened_port is not a numeric value";
                throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
            }

            //read OPENED_PORT STATE TYPE value
            bool bool_opened_port_state_type;
            int int_opened_port_state_type;
            try
            {
                bool_opened_port_state_type = Convert.ToBoolean(opened_port_state_type);
            }
            catch (FormatException e)
            {
                Trace("> IP9212_harware.OpenedSensorState(): Input string is not a boolean string [" + e.Message + "]");
                ASCOM_ERROR_MESSAGE = "OpenedSensorState(): opened_port state type is not a boolean value";
                throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
            }
            int_opened_port_state_type = (bool_opened_port_state_type ? 1 : 0);

            // READ CURRENT INPUT STATE
            if (input_state_arr[0] <= 0)
            {
                getInputStatus();
            }

            //calculate state
            bool boolState;
            if (input_state_arr[int_opened_port] == int_opened_port_state_type){
                boolState=true;
            } else {
                boolState=false;
            }
            
            Trace("> IP9212_harware.OpenedSensorState() exit: (" + boolState+")");
            return boolState;
        }

        /// <summary>
        /// return true if CLOSED STATE sensor signaling 
        /// </summary>
        /// <returns>Returns true in case of closed state signaling, false otherwise</returns> 
        public bool ClosedSensorState()
        {
            Trace("> IP9212_harware.ClosedSensorState(): enter");

            //read closED_PORT value
            int int_closed_port = 0;
            try
            {
                int_closed_port = Convert.ToInt16(closed_port);
            }
            catch (FormatException e)
            {
                Trace("> IP9212_harware.ClosedSensorState(): Input string is not a sequence of digits [" + e.Message + "]");
                ASCOM_ERROR_MESSAGE="ClosedSensorState(): closed_port is not a numeric value";
                throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
            }

            //read closED_PORT STATE TYPE value
            bool bool_closed_port_state_type;
            int int_closed_port_state_type;
            try
            {
                bool_closed_port_state_type = Convert.ToBoolean(closed_port_state_type);
            }
            catch (FormatException e)
            {
                Trace("> IP9212_harware.ClosedSensorState(): Input string is not a boolean string [" + e.Message + "]");
                ASCOM_ERROR_MESSAGE = "ClosedSensorState(): closed_port state type is not a boolean value";
                throw new ASCOM.InvalidValueException(ASCOM_ERROR_MESSAGE);
            }
            int_closed_port_state_type = (bool_closed_port_state_type ? 1 : 0);

            // READ CURRENT INPUT STATE
            if (input_state_arr[0] <= 0)
            {
                getInputStatus();
            }

            //calculate state
            bool boolState;
            if (input_state_arr[int_closed_port] == int_closed_port_state_type)
            {
                boolState = true;
            }
            else
            {
                boolState = false;
            }

            Trace("> IP9212_harware.ClosedSensorState() exit: (" + boolState+")");
            return boolState;
        }

        /// <summary>
        /// Tracing (logging) - 3 overloaded method
        /// </summary>
        public void Trace(string st)
        {
            Console.WriteLine(st);
            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }
        
        public void Trace(int st)
        {
            Console.WriteLine(st);
        }

        public void Trace(string st, int[] arr_int)
        {
            string st_out = st;
            foreach (int el in arr_int)
            {
                st_out=st_out+el+" ";
            }

            Console.WriteLine(st_out);

            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st_out);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }

    }
}
