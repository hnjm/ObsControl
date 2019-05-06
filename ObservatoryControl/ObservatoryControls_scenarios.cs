using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using LoggingLib;

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
            CommandParser.Commands.Add("VERSION", new Command((a) => VersionData.getVersionString(), "Get current program version")); //Pring version
            CommandParser.Commands.Add("HELP", new Command((a) => this.CommandParser.ListCommandsFormated2(), "List all available commands")); //list of commands
            CommandParser.Commands.Add("WAIT", new Command((a) => this.pauseScenarioExecution(a), "Pause execution for <N> milliseconds", "N")); //Pause execution for ... milliseconds

            //Self control commands
            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", new Command((a) => this.OBS_connectTelescope(), "Connect telescope in Observatory control software"));
            CommandParser.Commands.Add("OBS_MINIMIZE", new Command((a) => this.OBS_FormSwitchToShort(), "Switch main form of Observatory control software to minimum mode"));
            CommandParser.Commands.Add("OBS_MAXIMIZE", new Command((a) => this.OBS_FormSwitchToMax(), "Switch main form of Observatory control software to usual (maximum) mode"));

            //Power commands
            CommandParser.Commands.Add("POWER_ON", new Command((a) => this.ASCOMSwitch.PowerMainRelaysOn(), "Power on all switch channels")); //Power Mount, Camera, Focuser
            CommandParser.Commands.Add("POWER_MOUNT_ON", new Command((a) => this.ASCOMSwitch.PowerMountOn(), "Power on mount switch channel"));
            CommandParser.Commands.Add("POWER_MOUNT_OFF", new Command((a) => this.ASCOMSwitch.PowerMountOff(), "Power off mount switch channel"));
            CommandParser.Commands.Add("POWER_CAMERA_ON", new Command((a) => this.ASCOMSwitch.PowerCameraOn(), "Power on camera switch channel"));
            CommandParser.Commands.Add("POWER_CAMERA_OFF", new Command((a) => this.ASCOMSwitch.PowerCameraOff(), "Power off camera switch channel"));
            CommandParser.Commands.Add("POWER_OFF", new Command((a) => this.ASCOMSwitch.PowerMainRelaysOff(), "Power off all switch channel")); //Power Mount, Camera, Focuser


            //MaimDL
            CommandParser.Commands.Add("MAXIM_RUN", new Command((a) => this.startMaximDL(), "Run Maxim DL"));
            CommandParser.Commands.Add("MAXIM_CAMERA_CONNECT", new Command((a) => objMaxim.ConnectCamera(), "Connect camera in Maxim DL"));
            CommandParser.Commands.Add("MAXIM_TELESCOPE_CONNECT", new Command((a) => objMaxim.ConnectTelescope(), "Connect telescope in Maxim DL"));
            CommandParser.Commands.Add("MAXIM_FOCUSER_CONNECT", new Command((a) => objMaxim.ConnectFocuser(), "Connect focuser in Maxim DL"));

            CommandParser.Commands.Add("MAXIM_CAMERA_SETCOOLING", new Command((a) => objMaxim.CameraCoolingOn(), "Switch cooling on and set to def temp")); //switch cooling on and set to def temp
            CommandParser.Commands.Add("CAMERA_SET_COOLER_TEMP", new Command((a) => objMaxim.CameraCoolingChangeSetTemp(a), "switch cooling on and set to specified double <TEMP>", "TEMP"));//switch cooling on and set to specified temp
            CommandParser.Commands.Add("CAMERA_WARMPUP", new Command((a) => objMaxim.CameraCoolingOff(true), "Camera warmp up")); //warmup


            //FocusMax
            CommandParser.Commands.Add("FOCUSMAX_RUN", new Command((a) => this.startFocusMax(), "Run FocusMax"));

            //Cartes du Ciel
            CommandParser.Commands.Add("CdC_RUN", new Command((a) => this.startPlanetarium(), "Run Cartes du Ciel"));
            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", new Command((a) => this.objCdCApp.ConnectTelescope(), "Connect telescope in Cartes du Ciel current chart"));
            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT1", new Command((a) => this.objCdCApp.ConnectTelescopeInChart1(), "Connect telescope in Cartes du Ciel Chart_1"));
            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT2", new Command((a) => this.objCdCApp.ConnectTelescopeInChart2(), "Connect telescope in Cartes du Ciel Chart_2"));

            //CCDAP
            CommandParser.Commands.Add("CCDAP_RUN", new Command((a) => this.startCCDAP(), "Run CCDAutopilot"));

            //CCDC
            CommandParser.Commands.Add("CCDC_RUN", new Command((a) => this.startCCDC(), "Run CCDCommander"));

            //PHD2 related commands
            CommandParser.Commands.Add("PHD2_RUN", new Command((a) => this.startPHD2(), "Run PHD2"));
            CommandParser.Commands.Add("PHD2_CONNECT", new Command((a) => this.objPHD2App.CMD_ConnectEquipment2(), "Connect equipment in PHD2"));
            CommandParser.Commands.Add("PHDBROKER_RUN", new Command((a) => this.startPHDBroker(), "Run PHD Broker"));

            //IQP commands
            CommandParser.Commands.Add("IQP_START", new Command((a) => this.startIQP(), "Start built-in IQP monitoring"));

            //WS commands
            CommandParser.Commands.Add("WS_RUN", new Command((a) => this.startWS(), "Run WeatherStation monitor"));

            //TTC commands
            CommandParser.Commands.Add("TTC_RUN", new Command((a) => this.startTTC(), "Run TelescopeTempControl"));
            CommandParser.Commands.Add("TTC_GETDATA", new Command((a) => this.objTTCApp.CMD_GetJSONData().ToString(), "Query data from TTC"));
            CommandParser.Commands.Add("TTC_FANAUTO_ON", new Command((a) => this.objTTCApp.CMD_SetFANControl_ON(), "Switch main mirror cooling fan on and set control to auto"));
            CommandParser.Commands.Add("TTC_FANAUTO_OFF", new Command((a) => this.objTTCApp.CMD_SetFANControl_OFF(), "Switch main mirror cooling fan off"));
            CommandParser.Commands.Add("TTC_HEATERAUTO_ON", new Command((a) => this.objTTCApp.CMD_SetHeaterControl_ON(), "Switch secondary mirror heater on and set control to auto"));
            CommandParser.Commands.Add("TTC_HEATERAUTO_OFF", new Command((a) => this.objTTCApp.CMD_SetHeaterControl_OFF(), "Switch secondary mirror heater off"));
            CommandParser.Commands.Add("TTC_SETFANPWR", new Command((a) => this.objTTCApp.CMD_SetFanPWR(a), "Switch main mirror cooling fan speed to N", "N [0..255]"));
            CommandParser.Commands.Add("TTC_SETHEATERPWR", new Command((a) => this.objTTCApp.CMD_SetHeaterPWR(a), "Switch secondary mirror heater power to N", "N [0..255]"));

            //Complex commmands
            CommandParser.Commands.Add("IMAGING_RUN_PAUSE", new Command((a) => this.ImagingRun_Pause(), "Pause current imaging run, i.e. send pause command to CCDC through boltwood interface"));
            CommandParser.Commands.Add("IMAGING_RUN_RESUME", new Command((a) => this.ImagingRun_Resume(), "Resume current imaging run, i.e. release pause through boltwood interface"));
            CommandParser.Commands.Add("IMAGING_RUN_ABORT", new Command((a) => this.ImagingRun_Abort(),"Abort current image run. i.e. send pause command to CCDC through boltwood interface and engage camera warmup"));
            CommandParser.Commands.Add("IMAGING_RUN_ABORT_CANCEL", new Command((a) => this.ImagingRun_CancelAbort(), "Cancel aborting current image run. i.e. send resume command to CCDC through boltwood interface and set camera cooling"));

            //CommandParser.Commands.Add("IMAGING_RUN_ABORT_ASYNC", (a) => this.ImagingRun_Abort_async());
            //CommandParser.Commands2.Add("IMAGING_RUN_ABORT_ASYNC", new Command((a) => this.ImagingRun_Abort_async(), "Abort current image run (async mode). i.e. send pause command to CCDC through boltwood interface and engage camera warmup"));
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
                    CommandParser.ParseSingleCommand2(name + (argument != "" ? " " + argument : ""));
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
                CommandParser.ParseSingleCommand2("POWER_MOUNT_ON");
                CommandParser.ParseSingleCommand2("POWER_CAMERA_ON");
                CommandParser.ParseSingleCommand2("POWER_FOCUSER_ON");
            }

            //2.1 Run PHD2
            if (ConfigManagement.getBool("scenarioMainParams", "PHD2_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("PHD2_RUN");
            }

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 300);

            //2.2 PHD2 Connect equipment
            if (ConfigManagement.getBool("scenarioMainParams", "PHD2_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: connect equipment in PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("PHD2_CONNECT");
            }

            //2.3 Rub broker app
            if (ConfigManagement.getBool("scenarioMainParams", "PHDBROKER_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: run PHD Broker", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("PHDBROKER_RUN");
            }

            //3. Run MaximDL
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Maxim DL", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("MAXIM_RUN");
            }


            //3.1. CameraConnect
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_CAMERA_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: Maxim Camera connect", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("MAXIM_CAMERA_CONNECT");
                //ParentMainForm.AppendLogText("Camera connected");
            }

            //3.2. Set camera cooler
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_CAMERA_SETCOOLING") ?? false)
            {
                CommandParser.ParseSingleCommand2("MAXIM_CAMERA_SETCOOLING");
            }

            //3.3. Connect telescope to Maxim
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand2("MAXIM_TELESCOPE_CONNECT");
            }

            //4. Run FocusMax
            if (ConfigManagement.getBool("scenarioMainParams", "FOCUSMAX_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Focus Max", LogLevel.Debug);
                CommandParser.ParseSingleCommand2("FOCUSMAX_RUN");
                //ParentMainForm.AppendLogText("FocusMax started");
            }

            //5. Connect focuser in Maxim to FocusMax
            if (ConfigManagement.getBool("scenarioMainParams", "MAXIM_FOCUSER_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand2("MAXIM_FOCUSER_CONNECT");
            }

            //Thread.Sleep(2000);

            //6. Run Cartes du Ciel
            if (ConfigManagement.getBool("scenarioMainParams", "CdC_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand2("CdC_RUN");
            }

            //8. Start CCDAP
            if (ConfigManagement.getBool("scenarioMainParams", "CCDAP_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand2("CCDAP_RUN");
            }
            //8. Start CCDC
            if (ConfigManagement.getBool("scenarioMainParams", "CCDC_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand2("CCDC_RUN");
            }

            //7. Connect telescope in Program
            if (ConfigManagement.getBool("scenarioMainParams", "OBS_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand2("OBS_TELESCOPE_CONNECT");
            }

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "CDC_CONNECT_PAUSE") ?? 0);

            //6.1. Connect telescope in Cartes du Ciel (to give time for CdC to run)
            if (ConfigManagement.getBool("scenarioMainParams", "CdC_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand2("CdC_TELESCOPE_CONNECT");
            }

        }


        public void StartMaximDLroutines_old()
        {
            //1. Switch on power
            CommandParser.ParseSingleCommand2("POWER_MOUNT_ON");
            CommandParser.ParseSingleCommand2("POWER_CAMERA_ON");
            CommandParser.ParseSingleCommand2("POWER_FOCUSER_ON");

            //2. Run MaximDL
            CommandParser.ParseSingleCommand2("MAXIM_RUN");
            //ParentMainForm.AppendLogText("MaximDL started");

            //3. Run FocusMax
            CommandParser.ParseSingleCommand2("FOCUSMAX_RUN");
            //ParentMainForm.AppendLogText("FocusMax started");

            //4. CameraConnect
            CommandParser.ParseSingleCommand2("MAXIM_CAMERA_CONNECT");
            //ParentMainForm.AppendLogText("Camera connected");

            //5. Set camera cooler
            CommandParser.ParseSingleCommand2("MAXIM_CAMERA_SETCOOLING");

            //6. Connect telescope to Maxim
            CommandParser.ParseSingleCommand2("MAXIM_TELESCOPE_CONNECT");

            //7. Connect focuser in Maxim to FocusMax
            CommandParser.ParseSingleCommand2("MAXIM_FOCUSER_CONNECT");
        }


        //called from async method
        private void StartUpObservatory_wrapper()
        {
            Logging.AddLog("StartUp routine initiatied", LogLevel.Activity);

            ParseXMLScenario("scenarioMain");

            Logging.AddLog("StartUp routine finished", LogLevel.Activity);

            //Change user interface buttons
            ParentMainForm.Invoke(new Action(() => ParentMainForm.endRunAction()));
        }

        public void StartUpObservatory_async()
        {
            ThreadStart RunThreadRef = new ThreadStart(StartUpObservatory_wrapper);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            Logging.AddLog("Command 'Prepare run' was initiated", LogLevel.Activity);
        }
        #endregion Scenarios


        // Special scenario commands block 
        #region /// Special Commands //////////////////////

        /// <summary>
        /// Scenario element: Pause scenario execution for specified number of seconds
        /// </summary>
        /// <param name="CommandString_param_arr">1st param - number of milliseconds</param>
        public string pauseScenarioExecution(string[] CommandString_param_arr)
        {
            int pauseLength = 0;

            if (CommandString_param_arr.Count()!=0)
            {
                pauseLength = Convert.ToInt16(CommandString_param_arr[0]);
            }

            Thread.Sleep(pauseLength);

            return pauseLength.ToString();
        }

        /// <summary>
        /// CCDC will abort imaging run and park. Using switch weather to BAD
        /// </summary>
        /// <returns></returns>
        public string ImagingRun_Pause()
        {
            Logging.AddLog("Imaging run paused", LogLevel.Activity);

            objBoltwoodControl.Switch_to_BAD();

            return "RETURN: Imaging run paused";
        }

        /// <summary>
        /// CCDC will resume imaging run. Using switch weather back to GOOD
        /// </summary>
        /// <returns></returns>
        public string ImagingRun_Resume()
        {
            Logging.AddLog("Imaging run resumed", LogLevel.Activity);

            objBoltwoodControl.Switch_to_GOOD();

            return "RETURN: Imaging run resumed";
        }

        /// <summary>
        /// CCDC will abort imaging run, park and cooler warmup. Using switch weather to BAD and controlling this
        /// </summary>
        /// <returns></returns>
        public string ImagingRun_Abort()
        {
            Logging.AddLog("Imaging run abortion started", LogLevel.Activity);

            //CCDC will park
            ImagingRun_Pause();

            //Camera warmup
            CommandParser.ParseSingleCommand2("CAMERA_WARMPUP");

            //Wait and check if parked. If not - force parking
            if (objCCDCApp.IsRunning())
            {
                Thread.Sleep(20000); //wait

                if (!ASCOMTelescope.DirectAccessGetBool("AtPark") && !ASCOMTelescope.DirectAccessGetBool("Slewing"))
                {
                    Logging.AddLog("Seems there is no activity. Force parking", LogLevel.Activity);
                    ASCOMTelescope.Park();
                }
            }
            else
            {
                ASCOMTelescope.Park();
            }


            return "RETURN: Imaging run aborted";
        }

        /// <summary>
        /// CCDC will abort imaging run, park and cooler warmup. Using switch weather to BAD and controlling this
        /// </summary>
        /// <returns></returns>
        public string ImagingRun_CancelAbort()
        {
            Logging.AddLog("Imaging run abortion canceling...", LogLevel.Activity);

            //CCDC resume
            ImagingRun_Resume();

            //Camera set cool
            CommandParser.ParseSingleCommand2("MAXIM_CAMERA_SETCOOLING");
            

            return "RETURN: Imaging run abortion canceled";
        }

        #endregion
        // end of Special scenario commands block


    }
}
