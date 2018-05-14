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

using LoggingLib;
using IQPEngineLib;

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

        /// <summary>
        /// ASCOM Utils wrapper
        /// </summary>
        public ASCOM.Utilities.Util ASCOMUtils=new ASCOM.Utilities.Util();

        /// <summary>
        /// Command dictionary for interpretator
        /// </summary>
        public CommandInterpretator CommandParser;

        public ObservatoryControls_ASCOMSwitch ASCOMSwitch;
        public ObservatoryControls_ASCOMTelescope ASCOMTelescope;
        public ObservatoryControls_ASCOMDome ASCOMDome;
        public ObservatoryControls_ASCOMFocuser ASCOMFocuser;


        /// <summary>
        /// Boltwood file generation object
        /// </summary>
        internal ObservatoryControls_boltwood objBoltwoodControl;

        /// <summary>
        /// IQP Object
        /// </summary>
        public IQPEngine objIQPEngine;



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
            ASCOMFocuser = new ObservatoryControls_ASCOMFocuser();

            objBoltwoodControl = new ObservatoryControls_boltwood();

            objIQPEngine = new IQPEngine(new IQPEngine.CallBackFunction(ParentMainForm.IQP_PublishFITSData)); //with callbackfunction


            //for debug
            //SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            //DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            //TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";

            //objMaxim = new MaximControls(ParentMainForm);
        }



        public string OBS_connectTelescope()
        {
            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
            ASCOMTelescope.Connect = true;
            return "Telescope in ObsContrtol connected";
        }


    }

}
