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
                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL Camera Connect test passed");
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
                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL Telescope Connect test passed");
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

                TestResult.AddStr("TestEquipment: Maxim DL camera setpoint: " + SetPoint + ", temp: " + Temp + ", power: " + Power);
                TestResult.res = true;
                TestResult.AddStr("TestEquipment: Maxim DL Camera Cooling test passed");
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
    }
}

