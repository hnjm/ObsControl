using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ObservatoryCenter
{
    public partial class ObservatoryControls
    {

        /// <summary>
        /// Init command interpretator
        /// </summary>
        public void InitComandInterpretator()
        {
            CommandParser.Commands.Add("HELP", (a) => this.CommandParser.ListCommands());
            CommandParser.Commands.Add("VERSION", (a) => VersionData.getVersionString());

            CommandParser.Commands.Add("MAXIM_RUN", (a) => this.startMaximDL());
            CommandParser.Commands.Add("MAXIM_CAMERA_CONNECT", (a) => objMaxim.ConnectCamera());
            CommandParser.Commands.Add("MAXIM_CAMERA_SETCOOLING", (a) => objMaxim.SetCameraCooling());
            CommandParser.Commands.Add("MAXIM_TELESCOPE_CONNECT", (a) => objMaxim.ConnectTelescope());
            CommandParser.Commands.Add("MAXIM_FOCUSER_CONNECT", (a) => objMaxim.ConnectFocuser());

            CommandParser.Commands.Add("FOCUSMAX_RUN", (a) => this.startFocusMax());

            CommandParser.Commands.Add("CdC_RUN", (a) => this.startPlanetarium());
            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", (a) => this.objCdCApp.ConnectTelescope());

            CommandParser.Commands.Add("CCDAP_RUN", (a) => this.startCCDAP());

            CommandParser.Commands.Add("PHD2_RUN", (a) => this.startPHD2());
            CommandParser.Commands.Add("PHD2_CONNECT", (a) => this.objPHD2App.CMD_ConnectEquipment());
            CommandParser.Commands.Add("PHDBROKER_RUN", (a) => this.startPHDBroker());

            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", (a) => this.OBS_connectTelescope());
            CommandParser.Commands.Add("POWER_MOUNT_ON", (a) => this.PowerMountOn());
            CommandParser.Commands.Add("POWER_MOUNT_OFF", (a) => this.PowerMountOff());
            CommandParser.Commands.Add("POWER_CAMERA_ON", (a) => this.PowerCameraOn());
            CommandParser.Commands.Add("POWER_CAMERA_OFF", (a) => this.PowerCameraOff());
            CommandParser.Commands.Add("POWER_FOCUSER_ON", (a) => this.PowerFocuserOn());
            CommandParser.Commands.Add("POWER_FOCUSER_OFF", (a) => this.PowerFocuserOff());
            CommandParser.Commands.Add("POWER_ROOF_ON", (a) => this.PowerRoofOn());
            CommandParser.Commands.Add("POWER_ROOF_OFF", (a) => this.PowerRoofOff());

            CommandParser.Commands.Add("WS_RUN", (a) => this.startWS());

            CommandParser.Commands.Add("TTC_RUN", (a) => this.startTTC());
            CommandParser.Commands.Add("TTC_GETDATA", (a) => this.objTTCApp.CMD_GetJSONData().ToString());
            CommandParser.Commands.Add("TTC_FANAUTO_ON", (a) => this.objTTCApp.CMD_SetFANControl_ON());
            CommandParser.Commands.Add("TTC_FANAUTO_OFF", (a) => this.objTTCApp.CMD_SetFANControl_OFF());
            CommandParser.Commands.Add("TTC_HEATERAUTO_ON", (a) => this.objTTCApp.CMD_SetHeaterControl_ON());
            CommandParser.Commands.Add("TTC_HEATERAUTO_OFF", (a) => this.objTTCApp.CMD_SetHeaterControl_OFF());
            CommandParser.Commands.Add("TTC_SETFANPWR", (a) => this.objTTCApp.CMD_SetFanPWR(a));
            CommandParser.Commands.Add("TTC_SETHEATERPWR", (a) => this.objTTCApp.CMD_SetHeaterPWR(a));
        }

        #region Scenarios section ////////////////////////////////////////////////////////
        /// <summary>
        /// Init observatory activity 
        /// </summary>
        public void StartUpObservatory()
        {

            //1. Switch on power
            if (ObsConfig.getBool("scenarioMainParams", "POWER_ON") ?? false)
            {
                Logging.AddLog("StartUp run: Switching power on", LogLevel.Debug);
                CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
                CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
                CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");
            }

            //2.1 Run PHD2
            if (ObsConfig.getBool("scenarioMainParams", "PHD2_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHD2_RUN");
            }

            Thread.Sleep(ObsConfig.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 300);

            //2.2 PHD2 Connect equipment
            if (ObsConfig.getBool("scenarioMainParams", "PHD2_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: connect equipment in PHD2", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHD2_CONNECT");
            }

            //2.3 Rub broker app
            if (ObsConfig.getBool("scenarioMainParams", "PHDBROKER_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: run PHD Broker", LogLevel.Debug);
                CommandParser.ParseSingleCommand("PHDBROKER_RUN");
            }

            //3. Run MaximDL
            if (ObsConfig.getBool("scenarioMainParams", "MAXIM_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Maxim DL", LogLevel.Debug);
                CommandParser.ParseSingleCommand("MAXIM_RUN");
            }


            //3.1. CameraConnect
            if (ObsConfig.getBool("scenarioMainParams", "MAXIM_CAMERA_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: Maxim Camera connect", LogLevel.Debug);
                CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
                //ParentMainForm.AppendLogText("Camera connected");
            }

            //3.2. Set camera cooler
            if (ObsConfig.getBool("scenarioMainParams", "MAXIM_CAMERA_SETCOOLING") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");
            }

            //3.3. Connect telescope to Maxim
            if (ObsConfig.getBool("scenarioMainParams", "MAXIM_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");
            }

            //4. Run FocusMax
            if (ObsConfig.getBool("scenarioMainParams", "FOCUSMAX_RUN") ?? false)
            {
                Logging.AddLog("StartUp run: Start Focus Max", LogLevel.Debug);
                CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
                //ParentMainForm.AppendLogText("FocusMax started");
            }

            //5. Connect focuser in Maxim to FocusMax
            if (ObsConfig.getBool("scenarioMainParams", "MAXIM_FOCUSER_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");
            }

            //Thread.Sleep(2000);

            //6. Run Cartes du Ciel
            if (ObsConfig.getBool("scenarioMainParams", "CdC_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand("CdC_RUN");
            }

            //8. Start CCDAP
            if (ObsConfig.getBool("scenarioMainParams", "CCDAP_RUN") ?? false)
            {
                CommandParser.ParseSingleCommand("CCDAP_RUN");
            }

            //7. Connect telescope in Program
            if (ObsConfig.getBool("scenarioMainParams", "OBS_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("OBS_TELESCOPE_CONNECT");
            }

            Thread.Sleep(ObsConfig.getInt("scenarioMainParams", "CDC_CONNECT_PAUSE") ?? 0);

            //6.1. Connect telescope in Cartes du Ciel (to give time for CdC to run)
            if (ObsConfig.getBool("scenarioMainParams", "CdC_TELESCOPE_CONNECT") ?? false)
            {
                CommandParser.ParseSingleCommand("CdC_TELESCOPE_CONNECT");
            }

        }


        public void StartMaximDLroutines()
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


        #endregion Scenarios



    }
}
