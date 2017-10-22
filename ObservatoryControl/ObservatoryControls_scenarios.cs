using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace ObservatoryCenter
{
    public partial class ObservatoryControls
    {

        /*
         SCENARION XML FILE HELP
         1. Нужно создать секцию с именем сценария <scenarioMain></scenarioMain>
         2. Внутри могут размещаться команды или глобальные параметры
            Примеры:
            <CDC_CONNECT_PAUSE type="parameter" value="2000" />
            <WAIT run="true" argument="2000" />
            Формат
                TYPE:
                "command" or omitted    - this is command(will try to run)
                type="parameter"        - this is parameter

                RUN:
                "true" or omitted = run command
                "false" = not run

                ARGUMENT:
                parameter to be passed to command
         4. Команды будут выполняться последовательно, если они не параметры и если у них RUN не false
         */
         
        /// <summary>
        /// Init command interpretator
        /// </summary>
        public void InitComandInterpretator()
        {
            //Internal commands
            CommandParser.Commands.Add("HELP", (a) => this.CommandParser.ListCommands()); //list of commands
            CommandParser.Commands.Add("VERSION", (a) => VersionData.getVersionString()); //Pring version
            CommandParser.Commands.Add("WAIT", (a) => this.pauseExecution(a)); //Pause execution for ... milliseconds

            //Self control commands
            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", (a) => this.OBS_connectTelescope());

            //Power commands
            CommandParser.Commands.Add("POWER_ON", (a) => this.ASCOMSwitch.PowerMainRelaysOn()); //Power Mount, Camera, Focuser
            CommandParser.Commands.Add("POWER_MOUNT_ON", (a) => this.ASCOMSwitch.PowerMountOn());
            CommandParser.Commands.Add("POWER_MOUNT_OFF", (a) => this.ASCOMSwitch.PowerMountOff());
            CommandParser.Commands.Add("POWER_CAMERA_ON", (a) => this.ASCOMSwitch.PowerCameraOn());
            CommandParser.Commands.Add("POWER_CAMERA_OFF", (a) => this.ASCOMSwitch.PowerCameraOff());
            //CommandParser.Commands.Add("POWER_FOCUSER_ON", (a) => this.ASCOMSwitch.PowerFocuserOn());
            //CommandParser.Commands.Add("POWER_FOCUSER_OFF", (a) => this.ASCOMSwitch.PowerFocuserOff());
            //CommandParser.Commands.Add("POWER_ROOF_ON", (a) => this.ASCOMSwitch.PowerRoofOn());
            //CommandParser.Commands.Add("POWER_ROOF_OFF", (a) => this.ASCOMSwitch.PowerRoofOff());
            CommandParser.Commands.Add("POWER_OFF", (a) => this.ASCOMSwitch.PowerMainRelaysOff()); //Power Mount, Camera, Focuser

            //MaimDL
            CommandParser.Commands.Add("MAXIM_RUN", (a) => this.startMaximDL());
            CommandParser.Commands.Add("MAXIM_CAMERA_CONNECT", (a) => objMaxim.ConnectCamera());
            CommandParser.Commands.Add("MAXIM_TELESCOPE_CONNECT", (a) => objMaxim.ConnectTelescope());
            CommandParser.Commands.Add("MAXIM_FOCUSER_CONNECT", (a) => objMaxim.ConnectFocuser());

            CommandParser.Commands.Add("MAXIM_CAMERA_SETCOOLING", (a) => objMaxim.CameraCoolingOn()); //switch cooling on and set to def temp
            CommandParser.Commands.Add("CAMERA_SET_COOLER_TEMP", (a) => objMaxim.CameraCoolingOn(a));//switch cooling on and set to specified temp
            CommandParser.Commands.Add("CAMERA_WARMPUP", (a) => objMaxim.CameraCoolingOff(true)); //warmup


            //FocusMax
            CommandParser.Commands.Add("FOCUSMAX_RUN", (a) => this.startFocusMax());

            //Cartes du Ciel
            CommandParser.Commands.Add("CdC_RUN", (a) => this.startPlanetarium());
            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", (a) => this.objCdCApp.ConnectTelescope());

            //CCDAP
            CommandParser.Commands.Add("CCDAP_RUN", (a) => this.startCCDAP());

            //CCDC
            CommandParser.Commands.Add("CCDC_RUN", (a) => this.startCCDC());

            //PHS2 related commands
            CommandParser.Commands.Add("PHD2_RUN", (a) => this.startPHD2());
            CommandParser.Commands.Add("PHD2_CONNECT", (a) => this.objPHD2App.CMD_ConnectEquipment2());
            CommandParser.Commands.Add("PHDBROKER_RUN", (a) => this.startPHDBroker());

            //WS commands
            CommandParser.Commands.Add("WS_RUN", (a) => this.startWS());

            //TTC commands
            CommandParser.Commands.Add("TTC_RUN", (a) => this.startTTC());
            CommandParser.Commands.Add("TTC_GETDATA", (a) => this.objTTCApp.CMD_GetJSONData().ToString());
            CommandParser.Commands.Add("TTC_FANAUTO_ON", (a) => this.objTTCApp.CMD_SetFANControl_ON());
            CommandParser.Commands.Add("TTC_FANAUTO_OFF", (a) => this.objTTCApp.CMD_SetFANControl_OFF());
            CommandParser.Commands.Add("TTC_HEATERAUTO_ON", (a) => this.objTTCApp.CMD_SetHeaterControl_ON());
            CommandParser.Commands.Add("TTC_HEATERAUTO_OFF", (a) => this.objTTCApp.CMD_SetHeaterControl_OFF());
            CommandParser.Commands.Add("TTC_SETFANPWR", (a) => this.objTTCApp.CMD_SetFanPWR(a));
            CommandParser.Commands.Add("TTC_SETHEATERPWR", (a) => this.objTTCApp.CMD_SetHeaterPWR(a));

            //Complex commmands
            CommandParser.Commands.Add("IMAGING_RUN_PAUSE", (a) => this.ImagingRun_Pause());
            CommandParser.Commands.Add("IMAGING_RUN_RESUME", (a) => this.ImagingRun_Resume());

            CommandParser.Commands.Add("IMAGING_RUN_ABORT", (a) => this.ImagingRun_Abort());
        }

        /// <summary>
        /// Run scenario by parsing special CONFIG section
        /// </summary>
        /// <param name="ScenarioName">Scenario name</param>
        public void ParseXMLScenario(string ScenarioName)
        {
            XmlNode scenarioSet = ConfigManagement.getXMLNode(ScenarioName);

            foreach (XmlElement ScenarioElem in scenarioSet)
            {
                //Имя команды
                string name = ScenarioElem.Name;
                //Флаг RUN
                string runflag_st = ScenarioElem.GetAttribute("run");
                bool runflag;
                if (!Boolean.TryParse(runflag_st, out runflag))
                {
                    runflag = true;
                }
                //Тип командры
                string eltype = ScenarioElem.GetAttribute("type");
                //Параметры команды
                string argument = ScenarioElem.GetAttribute("argument");

                if (runflag && eltype != "parameter")
                {
                    //RUN this command
                    CommandParser.ParseSingleCommand(name + (argument != "" ? " " + argument : ""));
                }
            }
        }

        #region Scenarios section ////////////////////////////////////////////////////////
        /// <summary>
        /// Init observatory activity. OBSOLETE
        /// </summary>
        public void StartUpObservatory_old()
        {

            //1. Switch on power
            if (ConfigManagement.getBool("scenarioMainParams", "POWER_ON") ?? false)
            {
                Logging.AddLog("StartUp run: Switching power on", LogLevel.Debug);
                CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
                CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
                CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");
            }

            //2.1 Run PHD2
            if (ConfigManagement.getBool("scenarioMainParams", "PHD2_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHD2_RUN");
            }

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 300);

            //2.2 PHD2 Connect equipment
            if (ConfigManagement.getBool("scenarioMainParams", "PHD2_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: connect equipment in PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHD2_CONNECT");
            }

            //2.3 Rub broker app
            if (ConfigManagement.getBool("scenarioMainParams", "PHDBROKER_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: run PHD Broker", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHDBROKER_RUN");
            }

            //3. Run MaximDL
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Maxim DL", LogLevel.Debug);
                CommandParser.ParseSingleCommand("MAXIM_RUN");
            }


            //3.1. CameraConnect
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_CAMERA_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: Maxim Camera connect", LogLevel.Debug);
                CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
                //ParentMainForm.AppendLogText("Camera connected");
            }

            //3.2. Set camera cooler
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_CAMERA_SETCOOLING") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");
            }

            //3.3. Connect telescope to Maxim
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");
            }

            //4. Run FocusMax
            if (ConfigManagement.getBool("scenarioMainParams", "FOCUSMAX_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Focus Max", LogLevel.Debug);
                CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
                //ParentMainForm.AppendLogText("FocusMax started");
            }

            //5. Connect focuser in Maxim to FocusMax
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_FOCUSER_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");
            }

            //Thread.Sleep(2000);

            //6. Run Cartes du Ciel
            if (ConfigManagement.getBool("scenarioMainParams", "CdC_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand("CdC_RUN");
            }

            //8. Start CCDAP
            if (ConfigManagement.getBool("scenarioMainParams", "CCDAP_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand("CCDAP_RUN");
            }
            //8. Start CCDC
            if (ConfigManagement.getBool("scenarioMainParams", "CCDC_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand("CCDC_RUN");
            }

            //7. Connect telescope in Program
            if (ConfigManagement.getBool("scenarioMainParams", "OBS_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("OBS_TELESCOPE_CONNECT");
            }

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "CDC_CONNECT_PAUSE") ?? 0);

            //6.1. Connect telescope in Cartes du Ciel (to give time for CdC to run)
            if (ConfigManagement.getBool("scenarioMainParams", "CdC_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("CdC_TELESCOPE_CONNECT");
            }

        }


        public void StartMaximDLroutines_old()
        {
            //1. Switch on power
            CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
            CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
            CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");

            //2. Run MaximDL
            CommandParser.ParseSingleCommand("MAXIM_RUN");
            //ParentMainForm.AppendLogText("MaximDL started");

            //3. Run FocusMax
            CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
            //ParentMainForm.AppendLogText("FocusMax started");

            //4. CameraConnect
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            //ParentMainForm.AppendLogText("Camera connected");

            //5. Set camera cooler
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");

            //6. Connect telescope to Maxim
            CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");

            //7. Connect focuser in Maxim to FocusMax
            CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");
        }


        public void StartUpObservatory()
        {
            Logging.AddLog("StartUp routine initiatied", LogLevel.Activity);

            ParseXMLScenario("scenarioMain");

            Logging.AddLog("StartUp routine finished", LogLevel.Activity);

            //Change user interface buttons
            ParentMainForm.Invoke(new Action(() => ParentMainForm.endRunAction()));
        }

        public void StartUpObservatory_async()
        {
            ThreadStart RunThreadRef = new ThreadStart(StartUpObservatory);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            Logging.AddLog("Command 'Prepare run' was initiated", LogLevel.Activity);
        }
        #endregion Scenarios


        // Special scenario commands block 
        #region /// Special Commands //////////////////////

        /// <summary>
        /// Pause scenario execution for specified number of seconds
        /// </summary>
        /// <param name="CommandString_param_arr">1st param - number of milliseconds</param>
        public string pauseExecution(string[] CommandString_param_arr)
        {
            int pauseLength = Convert.ToInt16(CommandString_param_arr[0]);

            Thread.Sleep(pauseLength);

            return pauseLength.ToString();
        }

        public string ImagingRun_Pause()
        {
            Logging.AddLog("Imaging run paused", LogLevel.Activity);

            Boltwood.Switch_to_BAD();
            Boltwood.WriteFile();

            return "RETURN: Imaging run paused";
        }

        public string ImagingRun_Resume()
        {
            Logging.AddLog("Imaging run resumed", LogLevel.Activity);

            Boltwood.Switch_to_GOOD();
            Boltwood.WriteFile();

            return "RETURN: Imaging run resumed";
        }

        public string ImagingRun_Abort()
        {
            Logging.AddLog("Imaging run abortion started", LogLevel.Activity);

            ImagingRun_Pause();

            CommandParser.ParseSingleCommand("CAMERA_WARMPUP");

            return "RETURN: Imaging run aborted";
        }
        

        #endregion
        // end of Special scenario commands block


    }
}
