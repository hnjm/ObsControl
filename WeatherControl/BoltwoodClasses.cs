using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherControl
{
    #region Custom data types for Sensors, Boltwood and so on

    /// <summary>
    /// Decimal separator override
    /// </summary>
    public enum decimalSeparatorType { useLocale = 0, useDot = 1, useComma = 2 }

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
    public enum RoofFlag { roofNotRequested = 0, roofWasRequested = 1 }
    public enum AlertFlag { alertNo = 0, alertYes = 1 }
    public enum CloudSensorModel { Classic = 1, AAG = 2 }


    /// <summary>
    /// Boltwood data fields base class (from specification)
    /// Shouldn't be used normally directly. More wisely to use extended class - BoltwoodClass
    /// </summary>        
    public class BoltwoodFields
    {
        public string Bolt_date = ""; //y y y y - m m - d d
        public string Bolt_time = ""; //h	h	:	m	m	:	s	s	.	s	s

        public string TempUnits = ""; // C / F
        public string WindSpeedUnits = ""; // K (=km per h) | M (=mph) | m (=m/s)

        public double Bolt_SkyTemp = -100; 
        public double Bolt_Temp = -100; 
        public double Bolt_SensorTemp = -100; //no direct var
        public double Bolt_WindSpeed = -100; //no direct var
        public double Bolt_Hum = -100; //no direct var
        public double Bolt_DewPoint = -100.0;
        public Int16 Bolt_Heater = -1; //0-100

        public RainFlag Bolt_RainFlag = RainFlag.rainFlagDry;
        public WetFlag Bolt_WetFlag = WetFlag.wetFlagDry;

        public UInt16 Bolt_SinceLastMeasure = 0; //sec since last data
        public double Bolt_now = 0; // date/time given as the VB6 Now() function result (in days) when Clarity II last wrote this file

        public CloudCond Bolt_CloudCond = CloudCond.cloudUnknown;
        public WindCond Bolt_WindCond = WindCond.windUnknown;
        public RainCond Bolt_RainCond = RainCond.rainUnknown;
        public DayCond Bolt_DaylighCond = DayCond.dayUnknown;

        public RoofFlag Bolt_RoofCloseFlag = RoofFlag.roofNotRequested; //roof close, =0 not requested, =1 if roof close was requested on this cycle
        public AlertFlag Bolt_AlertFlag = AlertFlag.alertNo; //alert, =0 when not alerting, =1 when alerting
    }

    #endregion

    public class BoltwoodClass : BoltwoodFields
    {

        internal DateTime LastMeasure;      //DateTime when last was measured
        internal bool RainNowFlag = false;  //internal field set that it is raining right now
        internal bool WetNowFlag = false;   //internal field set that it is wet right now


        public DateTime Bolt_RainFlag_LastDetected; //last time Rain was detected
        internal UInt16 Bolt_RainFlag_sinceLastDetected = 65535; //sec passed since
        public DateTime Bolt_WetFlag_LastDetected; //last time Wet was detected
        internal UInt16 Bolt_WetFlag_sinceLastDetected = 65535; //sec passed since

        internal double Bolt_CloudIdx = -100; //Caclulated cloud idx
        internal double Bolt_CloudIdxAAG = -100; //Caclulated cloud idx by AAG

        /// <summary>
        /// Configuration block with constants and parameters
        /// </summary>
        #region CONFIGURATION PARAMETERS

        #region CONFIGURATION: Boltwood internal settings
        /// <summary>
        /// This limit is used to caluclate: what is rain/wet NOW (1 sec ago? 20 sec ago?)
        /// </summary>
        internal Int32 TimeLimit_Now = 20; //sec
        /// <summary>
        /// This limit is used to caluclate: what is rain last minute (exactly 60 sec ago, or may be 61?)
        /// </summary>
        internal Int32 TimeLimit_LastMinute = 60; //sec
        #endregion

        #region CONFIGURATION: Cloud model parameters
        public CloudSensorModel CLOUDMODEL = CloudSensorModel.Classic; //Cloud Calculation model

        public double CLOUDINDEX_CLEAR = 20;        // >=
        public double CLOUDINDEX_CLOUDY = 10;       // >=
        public double CLOUDINDEX_CLOUDY_BAD = -10;  // <=

        public double CLOUDINDEXAAG_CLEAR = 5;
        public double CLOUDINDEXAAG_CLOUDY = 0;
        public double CLOUDINDEXAAG_CLOUDY_BAD = -20;

        // Coefficients for AAG cloud index model
        // Model for "This type of curve appears to work better during the cold season in cold climate regions where the sky temperature is lower than in mild climate regions."
        public double K1 = -7.0;
        public double K2 = 110.0;
        public double K3 = 46.0;
        public double K4 = 88.0;
        public double K5 = 88.0;
        public double K6 = 42.0;
        public double K7 = 25.0;

        // Usual coefficients
        //public double K1 = 33.0;
        //public double K2 = 0.0;
        //public double K3 = 4.0;
        //public double K4 = 100.0;
        //public double K5 = 100.0;
        //public double K6 = 0.0;
        //public double K7 = 0.0;
        #endregion

        #region CONFIGURATION: Daylight sensor limits
        /// <summary>
        /// Limits for daylight
        /// </summary> 
        public double DAYLIGHT_DARK_LIMIT = 0;
        public double DAYLIGHT_LIGHT_LIMIT = 50;
        #endregion

        #region CONFIGURATION: Windspeed sensor limits
        /// <summary>
        /// Limits for wind speed in m/s
        /// </summary>
        public double WINDSPEED_WINDY = 15 / 3.6; //15 kph - 4.2 m/s
        public double WINDSPEED_VERYWINDY = 30 / 3.6; //30 kph - 8.3 m/s
        #endregion

        #endregion

        #region Classic Fileds Read/Write
        public double SkyTemp
        {
            get { return Bolt_SkyTemp; }
            set
            {
                Bolt_SkyTemp = value;
                SetMeasurement();
            }
        }

        public double AmbientTemp
        {
            get { return Bolt_Temp; }
            set
            {
                Bolt_Temp = value;
                SetMeasurement();
            }
        }

        public double SensorTemp
        {
            get { return Bolt_SensorTemp; }
            set
            {
                Bolt_SensorTemp = value;
                SetMeasurement();
            }
        }

        public double WindSpeed
        {
            get { return Bolt_WindSpeed; }
            set
            {
                Bolt_WindSpeed = value;
                SetMeasurement();
            }
        }
        public double Humidity
        {
            get { return Bolt_Hum; }
            set
            {
                Bolt_Hum = value;
                SetMeasurement();
            }
        }

        public double DewPoint
        {
            get
            {
                return BolwoodCalculations.calcDewPoint(Bolt_Temp, Bolt_Hum);
            }
        }

        public short Heater
        {
            get { return Bolt_Heater; }
            set
            {
                Bolt_Heater = value;
                SetMeasurement();
            }
        }
        #endregion

        #region Rain | Wet Fields
        /// <summary>
        /// Set current rain status (NOW)
        /// e.g. that it is raining now or vice versa - not raining 
        /// </summary>
        public bool IsItRaining
        {
            set
            {
                RainNowFlag = value;
                if (value)
                {
                    Bolt_RainFlag_LastDetected = DateTime.Now;
                }
                SetMeasurement();
            }
        }

        /// <summary>
        /// Set current rain status (NOW)
        /// e.g. that it is raining now or vice versa - not raining 
        /// </summary>
        public bool IsItWet
        {
            set
            {
                WetNowFlag = value;
                if (value)
                {
                    Bolt_WetFlag_LastDetected = DateTime.Now;
                }
                SetMeasurement();
            }
        }


        public RainFlag RainFlag
        {
            get
            {
                TimeSpan MeasureIntervalRF = DateTime.Now.Subtract(Bolt_RainFlag_LastDetected);
                Bolt_RainFlag_sinceLastDetected = (ushort)Math.Round(MeasureIntervalRF.TotalSeconds, 0);

                if (Bolt_RainFlag_sinceLastDetected <= TimeLimit_Now)
                {
                    Bolt_RainFlag = RainFlag.rainFlagRightnow;
                }
                else if (Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_RainFlag = RainFlag.rainFlagLastminute;
                }
                else
                {
                    Bolt_RainFlag = RainFlag.rainFlagDry;
                }

                return Bolt_RainFlag;
            }
        }

        public WetFlag WetFlag
        {
            get
            {
                TimeSpan MeasureIntervalWF = DateTime.Now.Subtract(Bolt_WetFlag_LastDetected);
                Bolt_WetFlag_sinceLastDetected = (ushort)Math.Round(MeasureIntervalWF.TotalSeconds, 0);

                if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_Now)
                {
                    Bolt_WetFlag = WetFlag.wetFlagRightnow;
                }
                else if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_WetFlag = WetFlag.wetFlagLastminute;
                }
                else
                {
                    Bolt_WetFlag = WetFlag.wetFlagDry;
                }

                return Bolt_WetFlag;
            }
        }


        /// <summary>
        /// Set RainFlag firectly, try not to use it!
        /// </summary>
        internal RainFlag RainFlag_DirectSet
        {
            set
            {
                Bolt_RainFlag = value;


                SetMeasurement();
            }
        }

        /// <summary>
        /// Set WetFlag firectly, try not to use it!
        /// </summary>
        internal WetFlag WetFlag_DirectSet
        {
            set
            {
                Bolt_WetFlag = value;
                SetMeasurement();
            }
        }

        /// <summary>
        /// Get current rain condition
        /// Make this both on RainFlag and WetFlag
        /// during read set also Bolt_RainCond,Bolt_RainFlag, Bolt_WetFlag
        /// But it seems that is is ambigious
        /// </summary>
        public RainCond RainCond
        {
            get
            {
                RecheckTimeLimits();
                EvaluateRainWetConditions();

                return Bolt_RainCond;
            }
        }

        public RainCond RainCond_DirectSet
        {
            set
            {
                Bolt_RainCond = value;
                SetMeasurement();
            }
        }
        #endregion

        #region Conditions
        public CloudCond CloudCond
        {
            get
            {
                EvaluateCloudConditions();
                return Bolt_CloudCond;
            }
        }

        public WindCond WindCond
        {
            get
            {
                EvaluateWindConditions();
                return Bolt_WindCond;
            }

        }

        public DayCond DaylightCond
        {
            get
            {
                return Bolt_DaylighCond;
            }
            set
            {
                Bolt_DaylighCond = value;
                SetMeasurement();
            }
        }
        #endregion

        #region Alarms
        public RoofFlag RoofCloseFlag
        {
            get
            {
                return Bolt_RoofCloseFlag;
            }
            set
            {
                Bolt_RoofCloseFlag = value;
                SetMeasurement();
            }
        }

        public AlertFlag AlertFlag
        {
            get
            {
                return Bolt_AlertFlag;
            }
            set
            {
                Bolt_AlertFlag = value;
                SetMeasurement();
            }
        }
        #endregion

        #region Date / Time Fields
        public double SecondsSince
        {
            get
            {
                TimeSpan MeasureInterval = DateTime.Now.Subtract(LastMeasure);
                Bolt_SinceLastMeasure = (ushort)Math.Round(MeasureInterval.TotalSeconds, 0);
                return Bolt_SinceLastMeasure;
            }
        }
        public string Date
        {
            get
            {
                Bolt_date = DateTime.Now.ToString("yyyy-MM-dd");

                return Bolt_date;
            }
        }

        public string Time
        {
            get
            {
                Bolt_time = DateTime.Now.ToString("HH:mm:ss.ff");
                return Bolt_time;
            }
        }
        public double Now
        {
            get
            {
                Bolt_now = DateTime.Now.ToOADate();
                return Bolt_now;
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Write down the moment of measurement
        /// </summary>
        private void SetMeasurement()
        {
            LastMeasure = DateTime.Now;
        }


        /// <summary>
        /// Cehck if enough time passed to consider rain and wet to be over?
        /// </summary>
        private void RecheckTimeLimits()
        {
            //Recheck rain
            if (Bolt_RainFlag_sinceLastDetected <= TimeLimit_Now)
            {
                //STILL RAIN!
                RainNowFlag = true;
                WetNowFlag = true;
            }

            //Recheck wet
            if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_Now)
            {
                //STILL WET!
                WetNowFlag = true;
            }
        }


        /// <summary>
        /// Calculate RainCond, RainFlag, WetFlag status
        /// </summary>
        private void EvaluateRainWetConditions()
        {
            if (RainNowFlag)
            {
                //RAIN
                Bolt_RainCond = RainCond.rainRain;
                Bolt_RainFlag = RainFlag.rainFlagRightnow;
                Bolt_WetFlag = WetFlag.wetFlagRightnow;
            }
            else if (!RainNowFlag && WetNowFlag)
            {
                //WET
                Bolt_RainCond = RainCond.rainWet;
                Bolt_WetFlag = WetFlag.wetFlagRightnow;

                //Check Bolt_RainFlag
                if (Bolt_RainFlag_sinceLastDetected > TimeLimit_Now && Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_RainFlag = RainFlag.rainFlagLastminute;
                }
                else
                {
                    Bolt_RainFlag = RainFlag.rainFlagDry;
                }
            }
            else
            {
                //DRY
                Bolt_RainCond = RainCond.rainDry;

                //Check Bolt_RainFlag also
                if (Bolt_RainFlag_sinceLastDetected > TimeLimit_Now && Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_RainFlag = RainFlag.rainFlagLastminute;
                }
                else
                {
                    Bolt_RainFlag = RainFlag.rainFlagDry;
                }

                //Check Wet_RainFlag also
                if (Bolt_WetFlag_sinceLastDetected > TimeLimit_Now && Bolt_WetFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_WetFlag = WetFlag.wetFlagLastminute;
                }
                else
                {
                    Bolt_WetFlag = WetFlag.wetFlagDry;
                }
            }
        }

        private void EvaluateCloudConditions()
        {
            //Cloud condition
            Bolt_CloudIdx = this.calcCloudIndex(Bolt_SkyTemp, Bolt_Temp);
            Bolt_CloudIdxAAG = this.calcCloudIndexCorr(Bolt_SkyTemp, Bolt_Temp);

            if (CLOUDMODEL == CloudSensorModel.Classic)
            {
                if (Bolt_CloudIdx >= CLOUDINDEX_CLEAR) { Bolt_CloudCond = CloudCond.cloudClear; }
                else if (Bolt_CloudIdx >= CLOUDINDEX_CLOUDY) { Bolt_CloudCond = CloudCond.cloudCloudy; }
                else if (Bolt_CloudIdx > CLOUDINDEX_CLOUDY_BAD) { Bolt_CloudCond = CloudCond.cloudVeryCloudy; }
                else { Bolt_CloudCond = CloudCond.cloudUnknown; }
            }
            else
            {
                if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLEAR) { Bolt_CloudCond = CloudCond.cloudClear; }
                else if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLOUDY) { Bolt_CloudCond = CloudCond.cloudCloudy; }
                else if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLOUDY_BAD) { Bolt_CloudCond = CloudCond.cloudVeryCloudy; }
                else { Bolt_CloudCond = CloudCond.cloudUnknown; }
            }
        }

        private void EvaluateWindConditions()
        {
            Bolt_WindCond = WindCond.windUnknown;
            if (Bolt_WindSpeed >= WINDSPEED_VERYWINDY) { Bolt_WindCond = WindCond.windVeryWindy; }
            else if (Bolt_WindSpeed >= WINDSPEED_WINDY) { Bolt_WindCond = WindCond.windWindy; }
            else if (Bolt_WindSpeed < 0) { Bolt_WindCond = WindCond.windUnknown; }
            else { Bolt_WindCond = WindCond.windCalm; }
        }

        #endregion

        #region Cloud Calculations
        /// <summary>
        /// Calculates Cloud Index by classic methodology
        /// </summary>
        /// <param name="Tsky">Measured sky temperature</param>
        /// <param name="Tamb">Ambient temperature</param>
        /// <returns>Corrected sky temperature which is used as index</returns>
        public double calcCloudIndex(double Tsky, double Tamb, double Tcase = -100.0)
        {
            double Tclidx = -100.0;
            Tclidx = Tamb - Tsky;

            return Tclidx;
        }


        /// <summary>
        /// Calculates Cloud Index by AAG_CloudWatcher methodology
        /// </summary>
        /// <param name="Tsky">Measured sky temperature</param>
        /// <param name="Tamb">Ambient temperature</param>
        /// <returns>Corrected sky temperature which is used as index</returns>
        public double calcCloudIndexCorr(double Tsky, double Tamb)
        {
            double T67 = 0.0;

            if (Math.Abs((K2 / 10 - Tamb)) < 1)
            {
                T67 = Math.Sign(K6) * Math.Sign(Tamb - K2 / 10) * Math.Abs((K2 / 10 - Tamb));
            }
            else
            {
                T67 = K6 / 10 * Math.Sign(Tamb - K2 / 10) * (Math.Log(Math.Abs((K2 / 10 - Tamb))) / Math.Log(10) + K7 / 100);
            }

            double Td = (K1 / 100) * (Tamb - K2 / 10) + (K3 / 100) * Math.Pow(Math.Exp(K4 / 1000 * Tamb), (K5 / 100)) + T67;

            double Tcorr = Td - Tsky;

            return Tcorr;
        }
        #endregion

        #region Not used fields
        //internal string Bolt_RainFlag_LastDetected_s; //in string form
        //internal string Bolt_WetFlag_LastDetected_s; //in string form

        //public double WetSensorVal = 0; //specific for Astomania Weather Station
        //public int RGCVal = -1;         //specific for Astomania Weather Station
        //public double Preassure = 0;    //specific for Astomania Weather Station

        //public string LastMeasure_s;    //string form

        //public decimalSeparatorType ForcedDecimalSeparator = decimalSeparatorType.useLocale; //decimal separator
        #endregion

        /// <summary>
        /// Return clasic boltwood string
        /// </summary>   
        public string getBoltwoodString()
        {

            //Making boltwood string
            string bold_st = String.Format("{0,10} {1,11} C K {2,6:N1} {3,6:N1} {4,6:N1} {5,6:N1} {6,3:N0} {7,6:N1} {8,3:N0} {9,1:N0} {10,1:N0} {11,5:N0} {12,12:F5} {13,1:N0} {14,1:N0} {15,1:N0} {16,1:N0} {17,1:N0} {18,1:N0}",
                Date, Time, SkyTemp, AmbientTemp, SensorTemp, WindSpeed, Humidity, DewPoint, Heater,
                (int)RainFlag, (int)WetFlag, SecondsSince.ToString("00000"), Now.ToString("000000.#####"),
                (int)CloudCond, (int)WindCond, (int)RainCond, (int)DaylightCond, (int)RoofCloseFlag, (int)AlertFlag);

            return bold_st;
        }

        //Return Bolwood Object
        public BoltwoodFields getBoltwoodObject()
        {

            BoltwoodFields BoltwoodSate = new BoltwoodFields();

            BoltwoodSate.Bolt_date = Date;
            BoltwoodSate.Bolt_time = Time;

            BoltwoodSate.Bolt_SkyTemp = SkyTemp;
            BoltwoodSate.Bolt_Temp = AmbientTemp;
            BoltwoodSate.Bolt_SensorTemp = SensorTemp; 
            BoltwoodSate.Bolt_WindSpeed = WindSpeed; 
            BoltwoodSate.Bolt_Hum = Humidity; 

            BoltwoodSate.Bolt_DewPoint = DewPoint;
            BoltwoodSate.Bolt_Heater = Heater;

            BoltwoodSate.Bolt_RainFlag = RainFlag;
            BoltwoodSate.Bolt_WetFlag = WetFlag;

            BoltwoodSate.Bolt_SinceLastMeasure = (ushort)SecondsSince;
            BoltwoodSate.Bolt_now = Now;

            BoltwoodSate.Bolt_CloudCond = CloudCond;
            BoltwoodSate.Bolt_WindCond = WindCond;
            BoltwoodSate.Bolt_RainCond = RainCond;
            BoltwoodSate.Bolt_DaylighCond = DaylightCond;

            BoltwoodSate.Bolt_RoofCloseFlag = Bolt_RoofCloseFlag;
            BoltwoodSate.Bolt_AlertFlag = Bolt_AlertFlag;

            return BoltwoodSate;
        }
    }




    /// <summary>
    /// Static class, wich countains calculation options
    /// </summary>
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
            if (Temp == -100 || Hum < 0)
            {
                return -100;
            }
            else
            {
                double a = 17.271;
                double b = 237.7;
                double te = (a * Temp) / (b + Temp) + Math.Log(Hum / 100);
                double Td = (b * te) / (a - te);
                return Td;
            }
        }

    }



}