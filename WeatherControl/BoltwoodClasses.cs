using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherControl
{
    #region Custom data types for Sensors, Boltwood and so on
    /// <summary>
    /// Sensor type
    /// </summary>
    public enum SensorTypeEnum { Temp, Press, Hum, Illum, Wet, RGC, Relay, WSp };

    /// <summary>
    /// Decimal separator override
    /// </summary>
    public enum decimalSeparatorType { useLocale = 0, useDot = 1, useComma = 2 }


    /// <summary>
    /// Boltwood data fields
    /// </summary>        
    public class BoltwoodFields
    {
        public string Bolt_date = "";
        public string Bolt_time = "";

        public double Bolt_SkyTemp = -100; //no direct var
        public double Bolt_Temp = -100; //no direct var
        public double Bolt_CloudIdx = -100; //no direct var
        public double Bolt_CloudIdxAAG = -100; //no direct var
        public double Bolt_SensorTemp = -100; //no direct var
        public double Bolt_WindSpeed = -100; //no direct var
        public double Bolt_Hum = -100; //no direct var
        public double Bolt_DewPoint = -100.0;
        public Int16 Bolt_Heater = -1;

        public RainFlag Bolt_RainFlag = RainFlag.rainFlagDry;
        public DateTime Bolt_RainFlag_LastDetected;
        public string Bolt_RainFlag_LastDetected_s;
        public UInt16 Bolt_RainFlag_sinceLastDetected = 65535;

        public WetFlag Bolt_WetFlag = WetFlag.wetFlagDry;
        public DateTime Bolt_WetFlag_LastDetected;
        public string Bolt_WetFlag_LastDetected_s;
        public UInt16 Bolt_WetFlag_sinceLastDetected = 65535;

        public UInt16 Bolt_SinceLastMeasure = 0;
        public double Bolt_now = 0;

        public CloudCond Bolt_CloudCond = CloudCond.cloudUnknown;
        public WindCond Bolt_WindCond = WindCond.windUnknown;
        public RainCond Bolt_RainCond = RainCond.rainUnknown;
        public DayCond Bolt_DaylighCond = DayCond.dayUnknown;

        public UInt16 Bolt_RoofCloseFlag = 0;
        public UInt16 Bolt_AlertFlag = 0;

        public double WetSensorVal = 0;
        public int RGCVal = -1;
        public double Preassure = 0;

        public DateTime LastMeasure;
        public string LastMeasure_s;
        public decimalSeparatorType ForcedDecimalSeparator = decimalSeparatorType.useLocale;
    }

    /// <summary>
    /// Boltwood Data Types
    /// </summary>
    public enum CloudCond { cloudUnknown = 0, cloudClear = 1, cloudCloudy = 2, cloudVeryCloudy = 3 }
    public enum WindCond { windUnknown = 0, windCalm = 1, windWindy = 2, windVeryWindy = 3 }
    public enum RainCond { rainUnknown = 0, rainDry = 1, rainWet = 2, rainRain = 3 }
    public enum DayCond { dayUnknown = 0, dayDark = 1, dayLight = 2, dayVeryLight = 3 }
    public enum RainFlag { rainFlagDry = 0, rainFlagLastminute = 1, rainFlagRightnow = 2 }
    public enum WetFlag { wetFlagDry = 0, wetFlagLastminute = 1, wetFlagRightnow = 2 }


    //Non canonical
    public enum RoofFlag { roofNotRequested = 0, roofWasRequested = 1}
    public enum AlertFlag { alertNo = 0, alertYes = 1 }



    public static class BolwoodCalculations
    {
        /// <summary>
        /// Calc dew point
        /// </summary>
        /// <param name="Temp">temp in Celcicus</param>
        /// <param name="Hum">Humidity in percents (i.e. 50 for 50%)</param>
        /// <returns></returns>
        public static double calcDewPoint(double Temp, double Hum)
        {
            double a = 17.271;
            double b = 237.7;
            double te = (a * Temp) / (b + Temp) + Math.Log(Hum / 100);
            double Td = (b * te) / (a - te);

            return Td;

        }
    }
       
    #endregion 


    class BoltWoodProcessing
    {
        /// <summary>
        /// Method to generate contents of Boltwood format file
        /// - Changed: rain condition from main method
        /// </summary>   
        /*
        public string getBoltwoodString(out BoltwoodFields BoltwObj)
        {
            //Calculations for boltwood
            Bolt_DewPoint = BolwoodCalculations.calcDewPoint(BaseTempVal, HumidityVal);

            //Bolt_Heater
            Bolt_Heater = (short)(Relay1 == 1 ? 100 : 0);

            //Boltwood date time fields
            Bolt_date = DateTime.Now.ToString("yyyy-MM-dd");
            Bolt_time = DateTime.Now.ToString("HH:mm:ss.ff");
            Bolt_now = DateTime.Now.ToOADate();
            TimeSpan MeasureInterval = DateTime.Now.Subtract(LastMeasure);
            Bolt_SinceLastMeasure = (ushort)Math.Round(MeasureInterval.TotalSeconds, 0);

            //Cloud condition
            //CloudIdx = Temp1 - ObjTemp;
            if (CLOUDMODEL == CloudSensorModel.Classic)
            {
                if (CloudIdx > CLOUDINDEX_CLEAR) { Bolt_CloudCond = CloudCond.cloudClear; }
                else if (CloudIdx > CLOUDINDEX_CLOUDY) { Bolt_CloudCond = CloudCond.cloudCloudy; }
                else if (CloudIdx >= CLOUDINDEX_CLOUDY_BAD) { Bolt_CloudCond = CloudCond.cloudVeryCloudy; }
            }
            else
            {
                if (CloudIdxAAG > CLOUDINDEXAAG_CLEAR) { Bolt_CloudCond = CloudCond.cloudClear; }
                else if (CloudIdxAAG > CLOUDINDEXAAG_CLOUDY) { Bolt_CloudCond = CloudCond.cloudCloudy; }
                else if (CloudIdxAAG >= CLOUDINDEXAAG_CLOUDY_BAD) { Bolt_CloudCond = CloudCond.cloudVeryCloudy; }
            }

            //Bolt_WindCond: windCalm, windWindy, windVeryWindy
            Bolt_WindCond = WindCond.windUnknown;
            if (WindSpeedVal >= WINDSPEED_VERYWINDY) { Bolt_WindCond = WindCond.windVeryWindy; }
            else if (WindSpeedVal >= WINDSPEED_WINDY) { Bolt_WindCond = WindCond.windWindy; }
            else if (WindSpeedVal < 0) { Bolt_WindCond = WindCond.windUnknown; }
            else { Bolt_WindCond = WindCond.windCalm; }

            TimeSpan MeasureIntervalWF = DateTime.Now.Subtract(Bolt_WetFlag_LastDetected);
            Bolt_WetFlag_sinceLastDetected = (ushort)Math.Round(MeasureIntervalWF.TotalSeconds, 0);
            TimeSpan MeasureIntervalRF = DateTime.Now.Subtract(Bolt_RainFlag_LastDetected);
            Bolt_RainFlag_sinceLastDetected = (ushort)Math.Round(MeasureIntervalRF.TotalSeconds, 0);

            //Rain condition & Bolt_RainFlag + Bolt_WetFlag
            if (RainNow_Flag)
            {
                //RAIN
                Bolt_RainCond = RainCond.rainRain;

                Bolt_RainFlag = RainFlag.rainFlagRightnow;
                Bolt_RainFlag_LastDetected = DateTime.Now;

                Bolt_WetFlag = WetFlag.wetFlagRightnow;
                Bolt_WetFlag_LastDetected = DateTime.Now;
            }
            else if (!RainNow_Flag && RainNow_WetS_FlagC == RainCond.rainWet)
            {
                //WET
                Bolt_RainCond = RainCond.rainWet;
                Bolt_WetFlag = WetFlag.wetFlagRightnow;
                Bolt_WetFlag_LastDetected = DateTime.Now;
            }
            else
            {
                //DRY
                Bolt_RainCond = RainCond.rainDry;
                if (Bolt_RainFlag_sinceLastDetected > 0 && Bolt_RainFlag_sinceLastDetected < 60) { Bolt_RainFlag = RainFlag.rainFlagLastminute; }
                else { Bolt_RainFlag = RainFlag.rainFlagDry; }

                if (Bolt_WetFlag_sinceLastDetected > 0 && Bolt_WetFlag_sinceLastDetected < 60) { Bolt_WetFlag = WetFlag.wetFlagLastminute; }
                else { Bolt_WetFlag = WetFlag.wetFlagDry; }
            }

            //Daylight condition
            if (IllumVal > DAYLIGHT_LIGHT_LIMIT) { Bolt_DaylighCond = DayCond.dayVeryLight; }
            else if (IllumVal > DAYLIGHT_DARK_LIMIT) { Bolt_DaylighCond = DayCond.dayLight; }
            else if (IllumVal >= 0) { Bolt_DaylighCond = DayCond.dayDark; }

            //Bolt_RoofCloseFlag: roof close, =0 not requested, =1 if roof close was requested on this cycle 
            //not implemented yet

            //Bolt_AlertFlag: alert, =0 when not alerting, =1 when alerting 
            //not implemented yet

            //Making boltwood string
            string bold_st = String.Format("{0,10} {1,11} C K {2,6:N1} {3,6:N1} {4,6:N1} {5,6:N1} {6,3:N0} {7,6:N1} {8,3:N0} {9,1:N0} {10,1:N0} {11,5:N0} {12,12:F5} {13,1:N0} {14,1:N0} {15,1:N0} {16,1:N0} {17,1:N0} {18,1:N0}",
                Bolt_date, Bolt_time, ObjTempVal, BaseTempVal, SensorCaseTempVal, WindSpeedVal, HumidityVal, Bolt_DewPoint, Bolt_Heater,
                (int)Bolt_RainFlag, (int)Bolt_WetFlag, Bolt_SinceLastMeasure.ToString("00000"), Bolt_now.ToString("000000.#####"),
                (int)Bolt_CloudCond, (int)Bolt_WindCond, (int)Bolt_RainCond, (int)Bolt_DaylighCond, (int)Bolt_RoofCloseFlag, (int)Bolt_AlertFlag);


            BoltwoodSate.Bolt_date = Bolt_date;
            BoltwoodSate.Bolt_time = Bolt_time;

            BoltwoodSate.Bolt_SkyTemp = ObjTempVal;
            BoltwoodSate.Bolt_Temp = BaseTempVal;
            BoltwoodSate.Bolt_CloudIdx = CloudIdx;
            BoltwoodSate.Bolt_CloudIdxAAG = CloudIdxAAG;
            BoltwoodSate.Bolt_SensorTemp = SensorCaseTempVal; //no direct var
            BoltwoodSate.Bolt_WindSpeed = WindSpeedVal; //no direct var
            BoltwoodSate.Bolt_Hum = HumidityVal; //no direct var


            BoltwoodSate.Bolt_DewPoint = Bolt_DewPoint;
            BoltwoodSate.Bolt_Heater = Bolt_Heater;

            BoltwoodSate.Bolt_RainFlag = Bolt_RainFlag;
            BoltwoodSate.Bolt_RainFlag_LastDetected = Bolt_RainFlag_LastDetected;
            BoltwoodSate.Bolt_RainFlag_sinceLastDetected = Bolt_RainFlag_sinceLastDetected;


            BoltwoodSate.Bolt_WetFlag = Bolt_WetFlag;
            BoltwoodSate.Bolt_WetFlag_LastDetected = Bolt_WetFlag_LastDetected;
            BoltwoodSate.Bolt_WetFlag_sinceLastDetected = Bolt_WetFlag_sinceLastDetected;

            BoltwoodSate.Bolt_SinceLastMeasure = Bolt_SinceLastMeasure;
            BoltwoodSate.Bolt_now = Bolt_now;

            BoltwoodSate.Bolt_CloudCond = Bolt_CloudCond;
            BoltwoodSate.Bolt_WindCond = Bolt_WindCond;
            BoltwoodSate.Bolt_RainCond = Bolt_RainCond;
            BoltwoodSate.Bolt_DaylighCond = Bolt_DaylighCond;

            BoltwoodSate.WetSensorVal = WetVal;
            BoltwoodSate.RGCVal = RGCVal;

            BoltwoodSate.Preassure = PressureVal;

            BoltwoodSate.Bolt_RoofCloseFlag = Bolt_RoofCloseFlag;
            BoltwoodSate.Bolt_AlertFlag = Bolt_AlertFlag;

            BoltwoodSate.LastMeasure = LastMeasure;
            BoltwoodSate.ForcedDecimalSeparator = ForcedDecimalSeparator;


            //Return object with data:
            BoltwObj = BoltwoodSate; //return as out parameter

            return bold_st;
        }
        */

    }

}