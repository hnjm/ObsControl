using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Cartes du Ciel class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Cartes Du Ciel class
    /// </summary>
    public class CdC_ExternatApplication : ExternalApplication
    {
        public Int32 ServerPort = 3292; //port to connect socket server

        public CdC_ExternatApplication (): base()
        { }

        public string ConnectTelescope()
        {
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, "CONNECTTELESCOPE\r\n", out Error);
            ErrorSt = output;
            //Error = 0;
            if (Error<0)
            {
                Logging.AddLog("Telescope connection error in CdC", LogLevel.Important, Highlight.Error);
            }
            else
            {
                Logging.AddLog("Telescope connected in CdC",LogLevel.Activity);
            }
            return output;
        }
    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // PHD Broker class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// PHD Broker class
    /// </summary>
    public class PHDBroker_ExternatApplication : ExternalApplication
    {
        public PHDBroker_ExternatApplication() : base()
        { }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // CCDAP class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CCDAP class
    /// </summary>
    public class CCDAP_ExternatApplication : ExternalApplication
    {
        public CCDAP_ExternatApplication() : base()
        { }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Focusmax class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Focusmax class
    /// </summary>
    public class FocusMax_ExternatApplication : ExternalApplication
    {
        public FocusMax_ExternatApplication() : base()
        { }
    }

    public partial class ObservatoryControls
    {
        //public string PlanetariumPath = @"c:\Program Files (x86)\Ciel\skychart.exe";
        //public string PHD2Path = @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";
        //public string PHDBrokerPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";
        //public string CCDAPPath = @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

        //public string MaximDLPath = @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";
        //public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

        public CdC_ExternatApplication objCdCApp;
        public PHD_ExternatApplication objPHD2App;
        public PHDBroker_ExternatApplication objPHDBrokerApp;
        public CCDAP_ExternatApplication objCCDAPApp;
        public FocusMax_ExternatApplication objFocusMaxApp;

        public Maxim_ExternalApplication objMaxim;

        public WeatherStation objWSApp;

        //public Process MaximDL_Process = new Process();


        public void InitProgramsObj()
        {
            //Cartes du Ciel
            objCdCApp = new CdC_ExternatApplication();
            objCdCApp.Name = "skychart";
            objCdCApp.FullName = ObsConfig.getString("programsPath", "CdC") ?? @"c:\Program Files (x86)\Ciel\skychart.exe"; 
            objCdCApp.ParameterString = "--unique";

            //PHD2
            objPHD2App = new PHD_ExternatApplication();
            objPHD2App.Name = "phd2";
            objPHD2App.FullName = ObsConfig.getString("programsPath", "PHD2") ?? @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";

            //PHDBroker
            objPHDBrokerApp = new PHDBroker_ExternatApplication();
            objPHDBrokerApp.Name = "PHDBroker_Server";
            //objPHDBrokerApp.FullName = PHDBrokerPath;
            objPHDBrokerApp.FullName = ObsConfig.getString("programsPath", "PHDBROKER") ?? @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";

            //CCDAP
            objCCDAPApp = new CCDAP_ExternatApplication();
            objCCDAPApp.Name = "CCDAutoPilot5";
            objCCDAPApp.FullName = ObsConfig.getString("programsPath", "CCDAP") ?? @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

            //FocusMax
            objFocusMaxApp = new FocusMax_ExternatApplication();
            objFocusMaxApp.Name = "FocusMax";
            objFocusMaxApp.FullName = ObsConfig.getString("programsPath", "FOCUSMAX") ?? @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

            //MaxIm_DL
            objMaxim = new Maxim_ExternalApplication();
            objMaxim.Name = "MaxIm_DL";
            objMaxim.FullName = ObsConfig.getString("programsPath", "MAXIMDL") ?? @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";


            //WeatherStation
            objWSApp = new WeatherStation();
            objWSApp.Name = "WeatherStation";

        }

        #region Programs Controlling  ///////////////////////////////////////////////////////////////////
        public string startPlanetarium()
        {
            objCdCApp.Run();
            return objCdCApp.ErrorSt;
        }

        public string startPHD2()
        {
            objPHD2App.Run();
            objPHD2App.EstablishConnection(); // connect to server
            return objPHD2App.ErrorSt;
        }

        public string startPHDBroker()
        {
            objPHDBrokerApp.Run();
            return objPHDBrokerApp.ErrorSt;
        }

        public string startCCDAP()
        {
            objCCDAPApp.Run();
            return objCCDAPApp.ErrorSt;
        }

        public string startFocusMax()
        {
            objFocusMaxApp.Run();
            return objFocusMaxApp.ErrorSt;
        }

        public string startMaximDL()
        {
            objMaxim.Run(); //Run maximdl
            objMaxim.Init(); //Init maxin objects 
            return objMaxim.ErrorSt;
        }

        #endregion Program controlling


    }

}
