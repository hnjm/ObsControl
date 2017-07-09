using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using MaxIm;

namespace ObservatoryCenter
{
    public partial class ObservatoryControls
    {
        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;


        //public ASCOM.DriverAccess.Focuser objFocuser = null;
        //public ASCOM.DriverAccess.Camera objCamera = null;

        public double GuiderFocalLen;
        public double CamPixelSizeX;
        public double CamPixelSizeY;
        public double GuidePiexelScale = 1; //value will br received from PHD2

        public ASCOM.Utilities.Util ASCOMUtils=new ASCOM.Utilities.Util();

        /// <summary>
        /// Command dictionary for interpretator
        /// </summary>
        public CommandInterpretator CommandParser;

        public ObservatoryControls_ASCOMSwitch ASCOMSwitch;
        public ObservatoryControls_ASCOMTelescope ASCOMTelescope;
        public ObservatoryControls_ASCOMDome ASCOMDome;


        /// <summary>
        /// Conctructor
        /// </summary>
        public ObservatoryControls(MainForm MF)
        {
            ParentMainForm=MF;

            CommandParser = new CommandInterpretator();
            InitComandInterpretator();

            ASCOMSwitch = new ObservatoryControls_ASCOMSwitch();
            ASCOMTelescope = new ObservatoryControls_ASCOMTelescope();
            ASCOMDome = new ObservatoryControls_ASCOMDome(ASCOMSwitch);

            //for debug
            //SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            //DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            //TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";

            //objMaxim = new MaximControls(ParentMainForm);
        }




        public double CalcRecommendedCoolerTemp()
        {
            return -20.0;
        }

        public string OBS_connectTelescope()
        {
            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
            ASCOMTelescope.Connect = true;
            return "Telescope in ObsContrtol connected";
        }


    }

}
