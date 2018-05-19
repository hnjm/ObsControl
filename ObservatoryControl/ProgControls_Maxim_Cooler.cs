using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

using LoggingLib;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // MaximApp class
    //
    // Cooling management 
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// MaximApp partial class for COOLING MANAGEMENT
    /// </summary>
    public partial class Maxim_ExternalApplication
    {
        public const double TEMP_MIN = -99.0;
        public const double TEMP_MAX = 99.0;

        //Cooling status
        public double CameraTemp = TEMP_MIN;
        public double CameraSetPoint = TEMP_MIN;
        public double CameraCoolerPower = 0;
        public bool CameraCoolerOnStatus = false;
        public bool CameraWarmpUpNow = false;

        public const double DefaultCameraSetTemp = -20.0;
        public double TargetCameraSetTemp = DefaultCameraSetTemp;

        public double CameraTemp_MaxRecorded = TEMP_MIN; //for recording max temp in session
        public double CameraTemp_MinRecorded = TEMP_MAX; //for recording min temp in session

        // Threads
        private Thread CheckMaximCoolerStatusThread;
        private ThreadStart CheckMaximCoolerStatusThread_startref;


        /// <summary>
        /// Wrapper for async start
        /// </summary>
        public void checkCameraTemperatureStatus_async()
        {
            if (IsRunning() && MaximApplicationObj != null)
            {
                try
                {
                    if (CheckMaximCoolerStatusThread == null || !CheckMaximCoolerStatusThread.IsAlive)
                    {
                        CheckMaximCoolerStatusThread_startref = new ThreadStart(checkCameraTemperatureStatus);
                        CheckMaximCoolerStatusThread = new Thread(CheckMaximCoolerStatusThread_startref);
                        CheckMaximCoolerStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in checkCameraTemperatureStatus_asyn [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                //If not running, set current set temp to target set temp
                CameraSetPoint = TargetCameraSetTemp;
            }
        }

        /// <summary>
        /// Check current temp paramaters
        /// </summary>
        private void checkCameraTemperatureStatus()
        {
            CameraCoolerOnStatus = GetCoolerStatus();

            CameraTemp = GetCameraTemp();
            CameraSetPoint = GetCameraSetpoint();
            CameraCoolerPower = GetCoolerPower();

            if (CameraTemp != TEMP_MAX && CameraTemp > CameraTemp_MaxRecorded) CameraTemp_MaxRecorded = CameraTemp;
            if (CameraTemp != TEMP_MIN && CameraTemp < CameraTemp_MinRecorded) CameraTemp_MinRecorded = CameraTemp;
        }


        /// <summary>
        /// Overload method with string input (from scenarios management)
        /// </summary>
        public string CameraCoolingChangeSetTemp(string[] CommandString_param_arr)
        {
            double SetTemp;
            if (!Double.TryParse(CommandString_param_arr[0], out SetTemp))
                SetTemp = TEMP_MIN;

            bool res = CameraCoolingChangeSetTemp(SetTemp);
            string resst = "";
            if (res)
                resst = "Camera set temperature was set to ["+ SetTemp + "]";
            else
                resst = "Camera isn't connected. Target set temperature was changed to [" + SetTemp + "]";


            return resst;
        }


        /// <summary>
        /// Run this method to change cooler temp
        /// if camera connected - change current SetTemp, if not - change TargetSetTemp
        /// </summary>
        /// <param name="SetTemp"></param>
        /// <returns></returns>
        public bool CameraCoolingChangeSetTemp(double SetTemp)
        {
            bool res = false;

            if (SetTemp == TEMP_MIN)
                SetTemp = TargetCameraSetTemp;

            if (CameraConnected)
            {
                //1. Set cached field to this value (because on interface update this value will be changed back)
                CameraSetPoint = Convert.ToDouble(SetTemp);

                //2. Change set temp in cooler
                CameraCoolingOn(SetTemp);
                res = true;
            }
            else
            {
                //Change target temperature
                TargetCameraSetTemp = SetTemp;
                res = false;
            }
            return res;
        }


        
        /// <summary>
        /// Switch cooler on, and set main camera cooling temperature
        /// If Maxim isn't running then use it as a TragetSetTemp changing
        /// </summary>
        public string CameraCoolingOn(double SetTemp = TEMP_MIN)
        {
            //Changed logic - no temp given, no settemp
            //if (SetTemp == TEMP_MIN)
            //    SetTemp = TargetCameraSetTemp;
            CameraWarmpUpNow = false;

            if (IsRunning() && MaximApplicationObj != null)
            {
                if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
                try
                {
                    if (CCDCamera.CanSetTemperature)
                    {
                        CCDCamera.CoolerOn = true;
                        //CCDCamera.TemperatureSetpoint = SetTemp; ////////
                        Logging.AddLog("Camera cooler set to " + SetTemp + " deg", LogLevel.Activity);
                        return "Cooler set to " + SetTemp + " deg";
                    }
                    else
                    {
                        Logging.AddLog("Camera can't set temperature", LogLevel.Debug); //'Debug' to not dublicate messages
                        return "Camera can't set temperature";
                    }
                }
                catch (Exception ex)
                {
                    MaximLogError("MaximDL set camera cooling failed!", ex);
                    return "Set camera cooling failed";
                }
            }
            else
            {
                //Change target temperature
                //TargetCameraSetTemp = SetTemp;

                return "MaximDL is not running";
            }
        }

        /// <summary>
        /// Switch cooler off
        /// </summary>
        public string CameraCoolingOff(bool WarmUpFlag=false)
        {
            double WarmUpSetTemp = 50.0;

            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                if (WarmUpFlag)
                {
                    if (CCDCamera.CanSetTemperature)
                    {
                        CCDCamera.TemperatureSetpoint = WarmUpSetTemp;
                        CameraWarmpUpNow = true;
                        Logging.AddLog("Cooler warmup set to " + WarmUpSetTemp + " deg", LogLevel.Activity);
                        return "Cooler warmup set to " + WarmUpSetTemp + " deg";
                    }
                    else
                    {
                        CameraWarmpUpNow = false;
                        Logging.AddLog("Camera can't set temperature", LogLevel.Activity);
                        return "Camera can't set temperature";
                    }
                }
                else
                {
                    CCDCamera.CoolerOn = false;
                    CameraWarmpUpNow = false;
                    Logging.AddLog("Cooler switched off", LogLevel.Activity);
                    return "Cooler switched off";
                }
            }
            catch (Exception ex)
            {
                MaximLogError("MaximDL switch camera cooling off failed!", ex);
                return "Switch camera cooling off failed ";
            }
        }



        /// <summary>
        /// Check if cooler ON/OFF
        /// </summary>
        /// <returns></returns>
        private bool GetCoolerStatus()
        {
            bool getCoolerStatus = false;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getCoolerStatus = CCDCamera.CoolerOn;
                Logging.AddLog("Camera cooler is " + (getCoolerStatus ? "on" : "off"), LogLevel.Trace);
            }
            catch (Exception ex)
            {
                MaximLogError("MaximDL get camera cooler status failed!", ex);
            }
            CameraCoolerOnStatus = getCoolerStatus;
            return getCoolerStatus;
        }


        /// <summary>
        /// Get current Setpoint
        /// </summary>
        /// <returns></returns>
        public double GetCameraSetpoint()
        {
            double setTemp = TEMP_MIN;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                setTemp = CCDCamera.TemperatureSetpoint;
                Logging.AddLog("Camera setpoint is " + setTemp + " deg", LogLevel.Trace);
            }
            catch (Exception ex)
            {
                MaximLogError("MaximDL camera temp setpoint failed!", ex);
            }
            CameraSetPoint = setTemp;
            return setTemp;
        }

        public double GetCameraTemp()
        {
            double getTemp = TEMP_MAX;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getTemp = CCDCamera.Temperature;
                Logging.AddLog("Camera temp is " + getTemp + " deg", LogLevel.Trace);
            }
            catch (Exception ex)
            {
                MaximLogError("MaximDL get camera temp failed!", ex);
            }
            CameraTemp = getTemp;
            return getTemp;
        }

        public short GetCoolerPower()
        {
            short getPower = -1;
            if (CCDCamera == null) CCDCamera = new MaxIm.CCDCamera();
            try
            {
                getPower = CCDCamera.CoolerPower;
                Logging.AddLog("Camera cooler power is " + getPower + "%", LogLevel.Trace);
            }
            catch (Exception ex)
            {
                MaximLogError("MaximDL get camera cooler power failed!",ex);
            }

            CameraCoolerPower = getPower;
            return getPower;
        }

        public bool checkTempNearSetpoint()
        {
            bool res = false;

            if (CameraCoolerOnStatus)
            {
                if (CameraTemp <= CameraSetPoint)
                {
                    res = true;
                }
                else if (CameraTemp >= CameraSetPoint && CameraCoolerPower == 100)
                {
                    res = true;
                }
            }

            return res;
        }

        /// <summary>
        /// Calcualte SetTemp based on external temperature
        /// </summary>
        /// <returns></returns>
        public double CalcRecommendedCoolerTemp()
        {
            double SetPointCalc = TEMP_MAX;
            double ExternalTemp = TEMP_MIN;

            if (ParentObsControls.ASCOMFocuser.FocuserTemp != TEMP_MIN)
            {
                ExternalTemp = ParentObsControls.ASCOMFocuser.FocuserTemp;
            }
            else if (ParentObsControls.objFocusMaxApp.FM_FocuserTemp != TEMP_MIN)
            {
                ExternalTemp = ParentObsControls.objFocusMaxApp.FM_FocuserTemp;
            }
            else if (CameraTemp_MaxRecorded != TEMP_MIN)
            {
                ExternalTemp = CameraTemp_MaxRecorded;
            }

            //Расчитаем рекомендуемую температуру
            if (ExternalTemp != TEMP_MIN)
            {
                SetPointCalc = ExternalTemp - 37.5;
                SetPointCalc = Math.Ceiling(SetPointCalc / 5.0) * 5.0;
            }
            else
            {
                SetPointCalc = DefaultCameraSetTemp;
            }

            return SetPointCalc;
        }


        
        
        
        
        #region in development

        public void ToggleCameraCoolingAuto()
        {
        }

        #endregion
        /////// end of Cooling management ////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
