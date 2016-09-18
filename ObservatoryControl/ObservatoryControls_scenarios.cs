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
            CommandParser.Commands.Add("MAXIM_RUN", () => this.startMaximDL());
            CommandParser.Commands.Add("FOCUSMAX_RUN", () => this.startFocusMax());
            CommandParser.Commands.Add("CdC_RUN", () => this.startPlanetarium());
            CommandParser.Commands.Add("CCDAP_RUN", () => this.startCCDAP());
            CommandParser.Commands.Add("PHD2_RUN", () => this.startPHD2());
            CommandParser.Commands.Add("PHDBROKER_RUN", () => this.startPHDBroker());

            CommandParser.Commands.Add("POWER_MOUNT_ON", () => this.PowerMountOn());
            CommandParser.Commands.Add("POWER_MOUNT_OFF", () => this.PowerMountOff());

            CommandParser.Commands.Add("POWER_CAMERA_ON", () => this.PowerCameraOn());
            CommandParser.Commands.Add("POWER_CAMERA_OFF", () => this.PowerCameraOff());

            CommandParser.Commands.Add("POWER_FOCUSER_ON", () => this.PowerFocuserOn());
            CommandParser.Commands.Add("POWER_FOCUSER_OFF", () => this.PowerFocuserOff());

            CommandParser.Commands.Add("POWER_ROOF_ON", () => this.PowerRoofOn());
            CommandParser.Commands.Add("POWER_ROOF_OFF", () => this.PowerRoofOff());

            CommandParser.Commands.Add("MAXIM_CAMERA_CONNECT", () => objMaxim.ConnectCamera());
            CommandParser.Commands.Add("MAXIM_CAMERA_SETCOOLING", () => objMaxim.SetCameraCooling());
            CommandParser.Commands.Add("MAXIM_TELESCOPE_CONNECT", () => objMaxim.ConnectTelescope());
            CommandParser.Commands.Add("MAXIM_FOCUSER_CONNECT", () => objMaxim.ConnectFocuser());

            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", () => this.objCdCApp.ConnectTelescope());

            CommandParser.Commands.Add("PHD2_CONNECT", () => this.objPHD2App.CMD_ConnectEquipment());

            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", () => this.OBS_connectTelescope());
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

            Thread.Sleep(ObsConfig.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 0);

            //2.2 PHD2 Connect equipment
            if (ObsConfig.getBool("scenarioMainParams", "PHD2_CONNECT") ?? false)
            {
                Logging.AddLog("StartUp run: connect equipeun in PHD2", LogLevel.Debug);
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
