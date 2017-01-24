using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    public class TestSequenceElement
    {
        public string Name = "";
        public CheckBox ChkBox;
        public Func<TestResultClass> Proc;
    }

    public class TestResultUserStateClass
    {
        public TestResultClass TestResult;
        public TestSequenceElement TestSequenceLink;
    }

    public class TestResultClass
    {
        public bool res = false;
        public List<string> UserOutput = new List<string>();

        public void AddStr(string St)
        {
            UserOutput.Add(St);
            Logging.AddLog(St, LogLevel.Debug);
        }
    }


    public class TestEquipmentClass
    {
        ObservatoryControls ObsControl;

        public TestEquipmentClass(ObservatoryControls ExtObsControl)
        {
            ObsControl = ExtObsControl;
        }

        /// <summary>
        /// Test MAXIM RUN
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLRun()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL run test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_RUN");

            //check result
            try
            { 
                float ver = ObsControl.objMaxim.MaximApplicationObj.Version;
                TestResult.AddStr("TestEquipment: Maxim DL version " + ver);
                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL run test passed");
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL run test failed");
            }
            return TestResult;
        }

        /// <summary>
        /// Test MAXIM CAMERA CONNECT
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLCamera()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL Camera Connect test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");

            //check result
            try
            {
                string name = ObsControl.objMaxim.CCDCamera.CameraName;
                MaxIm.CameraStatusCode status = ObsControl.objMaxim.CCDCamera.CameraStatus;
                TestResult.AddStr("TestEquipment: Maxim DL camera name: " + name + ", status: " + status);
                if (status!= MaxIm.CameraStatusCode.csError && status !=MaxIm.CameraStatusCode.csNoCamera)
                { 
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: Maxim DL Camera Connect test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: Maxim DL Camera Connect test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL Camera Connect test failed");
            }
            return TestResult;
        }


        /// <summary>
        /// Test MAXIM TELESCOPE CONNECT
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLTelescope()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL Telescope Connect test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");

            //check result
            try
            {
                bool status = ObsControl.objMaxim.MaximApplicationObj.TelescopeConnected;
                TestResult.AddStr("TestEquipment: Maxim DL Telescope connected status: " + status);
                if (status)
                {
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: Maxim DL Telescope Connect test passed");
                }
                else
                {
                    TestResult.res = false;
                    TestResult.AddStr("TestEquipment: Maxim DL Telescope Connect test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL Telescope Connect test failed");
            }
            return TestResult;
        }


        /// <summary>
        /// Test MAXIM CAMERA SET COOLING
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLCooling()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL Camera Cooling test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");

            //check result
            try
            {
                double Temp = ObsControl.objMaxim.GetCameraTemp();
                double SetPoint = ObsControl.objMaxim.GetCameraSetpoint();
                double Power = ObsControl.objMaxim.GetCoolerPower();
                bool CanSetTemperature = ObsControl.objMaxim.CCDCamera.CanSetTemperature;

                TestResult.AddStr("TestEquipment: Maxim DL camera setpoint info: can set temp = " + CanSetTemperature + ", setpoint = " + SetPoint + ", temp = " + Temp + ", power = " + Power);

                if (CanSetTemperature && SetPoint > -50 && SetPoint<50)
                { 
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: Maxim DL Camera Cooling test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: Maxim DL Camera Cooling test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL Camera Cooling test failed");
            }
            return TestResult;
        }


        /// <summary>
        /// Test MAXIM CAMERA SHOOTING
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLShoot()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL camera shoot test started");
            try
            {
                //run test
                bool res = ObsControl.objMaxim.CCDCamera.Expose(1, 1, 0);
                bool imageready = false;
                MaxIm.CameraStatusCode status = MaxIm.CameraStatusCode.csNoCamera;

                for (int i = 1; i < 5; i++)
                {
                    //wait
                    Thread.Sleep(1000);

                    //check result
                    imageready = ObsControl.objMaxim.CCDCamera.ImageReady;
                    status = ObsControl.objMaxim.CCDCamera.CameraStatus;
                    TestResult.AddStr("TestEquipment: Maxim DL camera shoot with res: " + res + ", imageready: " + imageready + ", status: " + status);

                    if (status == MaxIm.CameraStatusCode.csIdle || status == MaxIm.CameraStatusCode.csError || status == MaxIm.CameraStatusCode.csNoCamera)
                    {
                        break;
                    }
                }
                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL camera shoot test passed");
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL camera shoot test failed");
            }
            return TestResult;
        }

        /// <summary>
        /// Test MAXIM FILTERWHEEL
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestMaximDLFilterWheel()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: Maxim DL filter wheel test started");
            try
            {
                //RUN TEST
                //Get current filter
                short FW = ObsControl.objMaxim.CCDCamera.Filter;
                TestResult.AddStr("TestEquipment: Maxim DL filter wheel was: " + FW);

                //Change filter
                ObsControl.objMaxim.CCDCamera.Filter = (short)(FW != 3 ? 3 : 2);
                bool res = ObsControl.objMaxim.CCDCamera.Expose(1, 1);
                bool imageready = false;
                MaxIm.CameraStatusCode status = MaxIm.CameraStatusCode.csNoCamera;

                for (int i = 1; i < 5; i++)
                {
                    //wait
                    Thread.Sleep(1000);

                    //check result
                    imageready = ObsControl.objMaxim.CCDCamera.ImageReady;
                    status = ObsControl.objMaxim.CCDCamera.CameraStatus;
                    TestResult.AddStr("TestEquipment: Maxim DL filter wheel with res: " + res + ", imageready: " + imageready + ", status: " + status);

                    if (status == MaxIm.CameraStatusCode.csIdle || status == MaxIm.CameraStatusCode.csError || status == MaxIm.CameraStatusCode.csNoCamera)
                    {
                        break;
                    }
                }
                FW = ObsControl.objMaxim.CCDCamera.Filter;
                TestResult.AddStr("TestEquipment: Maxim DL filter wheel set: " + FW);

                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL filter wheel test passed");
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: Maxim DL filter wheel test failed");
            }
            return TestResult;
        }




        /// <summary>
        /// Test PHD RUN
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestPHD2Run()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: PHD2 run test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("PHD2_RUN");

            //check result
            try
            {
                if (ObsControl.objPHD2App.IsRunning())
                { 
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: PHD2 process started");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: PHD2 test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: PHD2 run test failed");
            }
            return TestResult;
        }

        /// <summary>
        /// Test PHD CONNECT
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestPHD2Connect()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: PHD2 connect test started");

            //run test
            Thread.Sleep(ObsConfig.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 300); //wait a bit
            string stout = "";

            string res = ObsControl.objPHD2App.CMD_GetCurrentProfile();

           //check result
            try
            {
                if (res != String.Empty )
                {
                    TestResult.AddStr("TestEquipment: phd2 equipment list: " + res + "");
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: PHD2 connect test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: PHD2 connect failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: PHD2 connect test failed");
            }
            return TestResult;
        }

        /// <summary>
        /// Test PHD GUIDE
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestPHD2Guide()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: PHD2 guide test started");

            //run test
            string stout = "";
            stout = ObsControl.objPHD2App.CMD_ConnectEquipment();

            Thread.Sleep(300);

            int resout = ObsControl.objPHD2App.CMD_StartGuiding();

            Thread.Sleep(5000);

            //check result
            try
            {
                ObsControl.objPHD2App.CheckProgramEvents();

                TestResult.AddStr("TestEquipment: PHD2 state = " + ObsControl.objPHD2App.currentState);

                if (ObsControl.objPHD2App.currentState == PHDState.Calibrating || ObsControl.objPHD2App.currentState == PHDState.Dithered || ObsControl.objPHD2App.currentState == PHDState.Guiding || ObsControl.objPHD2App.currentState == PHDState.Looping || ObsControl.objPHD2App.currentState == PHDState.Settling )
                {
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: PHD2 guide test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: PHD2 guide test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: PHD2 guide test failed");
            }
            return TestResult;
        }


        /// <summary>
        /// Test CdC RUN
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestCdCRun()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: CdC run test started");

            //run test
            ObsControl.CommandParser.ParseSingleCommand("CdC_RUN");

            //check result
            try
            {
                if (ObsControl.objCdCApp.IsRunning())
                {
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: CdC run test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: CdC run test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: CdC run test failed");
            }
            return TestResult;
        }

        /// <summary>
        /// Test CdC Connect
        /// </summary>
        /// <returns></returns>
        internal TestResultClass TestCdCConnect()
        {
            TestResultClass TestResult = new TestResultClass();
            TestResult.res = false;

            TestResult.AddStr("TestEquipment: CdC connect test started");

            //Connect
            ObsControl.CommandParser.ParseSingleCommand("CdC_TELESCOPE_CONNECT");

            //check result
            try
            {
                Thread.Sleep(500);

                string res = ObsControl.objCdCApp.GET_TelescopePos();
                TestResult.AddStr("TestEquipment: CdC pos = " + res);

                if (res.Contains("OK"))
                {
                    TestResult.res = true;
                    TestResult.AddStr("TestEquipment: CdC connect test passed");
                }
                else
                {
                    TestResult.AddStr("TestEquipment: CdC connect test failed");
                }
            }
            catch (Exception Ex)
            {
                TestResult.AddStr("TestEquipment: CdC connect test failed");
            }
            return TestResult;
        }
        
    }
}

