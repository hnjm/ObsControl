using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using LoggingLib;

namespace ObservatoryCenter
{
    /// <summary>
    /// Class definitions for all programs you can find if ProgControls files:
    /// ProgControls_classes.cs - abstract class definitions
    /// ProgControls_[prog abrv].cs - special class definition for [prog]
    /// ProgControls_others.cs - class definitions for other programs (short text usually)
    /// *
    /// How it works:
    /// 1. Создается класс путем наследования от двух: ExternalApplication или ExternalApplicationSocketServer. Добавляютются специфические свойства.
    /// 2. В InitProgramsObj() здесь (ObservatoryControls_programs.cs) добавляется строка:
    ///     - создаеться объект
    ///     - заполняются важные свойства (Имя, путь, параметры запуска,...)
    /// 3. Создается wrapper здесь же (ObservatoryControls_programs.cs) для запуска соответствующей программы
    /// 4. Wrapper добавляется в интерпретатор комманд (ObservatoryControls_scenarios.cs)
    /// *
    /// </summary>

    public partial class ObservatoryControls
    {

        public CdC_ExternatApplication objCdCApp;
        public PHD_ExternatApplication objPHD2App;
        public PHDBroker_ExternatApplication objPHDBrokerApp;
        public CCDAP_ExternatApplication objCCDAPApp;
        public CCDC_ExternatApplication objCCDCApp;

        public FocusMax_ExternalApplication objFocusMaxApp;

        public Maxim_ExternalApplication objMaxim;

        public WeatherStation objWSApp;
        public TelescopeTempControl objTTCApp;
        public AstroTortilla objAstroTortilla;


        //public Process MaximDL_Process = new Process();


        public void InitProgramsObj()
        {
            //Cartes du Ciel
            objCdCApp = new CdC_ExternatApplication();
            objCdCApp.Name = "skychart";
            objCdCApp.FullName = ConfigManagement.getString("programsPath", "CdC") ?? @"c:\Program Files (x86)\Ciel\skychart.exe"; 
            objCdCApp.ParameterString = "--unique";

            //PHD2
            objPHD2App = new PHD_ExternatApplication();
            objPHD2App.Name = "phd2";
            objPHD2App.FullName = ConfigManagement.getString("programsPath", "PHD2") ?? @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";

            //PHDBroker
            objPHDBrokerApp = new PHDBroker_ExternatApplication();
            objPHDBrokerApp.Name = "PHDBroker_Server";
            //objPHDBrokerApp.FullName = PHDBrokerPath;
            objPHDBrokerApp.FullName = ConfigManagement.getString("programsPath", "PHDBROKER") ?? @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";

            //CCDAP
            objCCDAPApp = new CCDAP_ExternatApplication();
            objCCDAPApp.Name = "CCDAutoPilot5";
            objCCDAPApp.FullName = ConfigManagement.getString("programsPath", "CCDAP") ?? @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";
            objCCDAPApp.LogPath = ConfigManagement.getString("programsPath", "CCDAP_Logs") ?? @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Images\CCDAutoPilot_Logs";

            //CCDC
            objCCDCApp = new CCDC_ExternatApplication(this);
            objCCDCApp.Name = "CCDCommander";
            objCCDCApp.FullName = ConfigManagement.getString("programsPath", "CCDC") ?? @"c:\CCD Commander\CCDCommander.exe";
            objCCDCApp.LogPath = ConfigManagement.getString("programsPath", "CCDC_Logs") ?? @"c:\CCD Commander\Logs";
            objCCDCApp.ActionsPath = ConfigManagement.getString("programsPath", "CCDC_Actions") ?? @"c:\CCD Commander\Actions";
            objCCDCApp.ParameterString = objCCDCApp.GetLastActionFile().FullName;

            objCCDCApp.StartGuideScript = ConfigManagement.getString("programsPath", "PHDBrokerGuideStart") ?? @"c:\CCD Commander\auto.vbs";


            //FocusMax
            objFocusMaxApp = new FocusMax_ExternalApplication(this);
            objFocusMaxApp.Name = "FocusMax";
            objFocusMaxApp.FullName = ConfigManagement.getString("programsPath", "FOCUSMAX") ?? @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

            //MaxIm_DL
            objMaxim = new Maxim_ExternalApplication(this);
            objMaxim.Name = "MaxIm_DL";
            objMaxim.FullName = ConfigManagement.getString("programsPath", "MAXIMDL") ?? @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";

            //WeatherStation
            objWSApp = new WeatherStation();
            objWSApp.Name = "WeatherStation";
            objWSApp.FullName = ConfigManagement.getString("programsPath", "WS") ?? @"C:\Users\Emchenko\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Astromania.info\WeatherStation Monitor.appref-ms";

            //TelescopeTempControl
            objTTCApp = new TelescopeTempControl();
            objTTCApp.Name = "TelescopeTempControl";
            objTTCApp.FullName = ConfigManagement.getString("programsPath", "TTC") ?? @"C:\Users\Emchenko\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Astromania.info\TelescopeTempControl.appref-ms";

            //Astrotortilla
            objAstroTortilla = new AstroTortilla();
            objAstroTortilla.Name = "Astrotortilla";
            objAstroTortilla.FullName = ConfigManagement.getString("programsPath", "ASTROTORTILLA") ?? @"c:\Program Files (x86)\AstroTortilla\AstroTortilla.exe";
            objAstroTortilla.FullNameAutoIt= ConfigManagement.getString("programsPath", "ASTROTORTILLA_AUTOIT") ?? @"c:\Program Files (x86)\AstroTortilla\astrotortilla_solve.exe";

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
            objCCDAPApp.Init();
            return objCCDAPApp.ErrorSt;
        }

        public string startCCDC()
        {
            objCCDCApp.Run();
            objCCDCApp.Init();
            return objCCDCApp.ErrorSt;
        }

        public string startFocusMax()
        {
            objFocusMaxApp.Run();
            objFocusMaxApp.InitObjects();
            return objFocusMaxApp.ErrorSt;
        }

        public string startMaximDL()
        {
            objMaxim.Run(); //Run maximdl
            objMaxim.Init(); //Init maxin objects 
            return objMaxim.ErrorSt;
        }

        public void startAstrotortillaSolve()
        {
            objAstroTortilla.Solve(); //Run solving
        }


        public string startTTC()
        {
            objTTCApp.Run(); //Run TTC
            return objTTCApp.ErrorSt;
        }

        public string startWS()
        {
            objWSApp.Run(); //Run WS
            return objWSApp.ErrorSt;
        }

        public string startIQP()
        {
            ParentMainForm.IQPStart();
            return "IQP started";
        }
            

        #endregion Program controlling

        }

}
