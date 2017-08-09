using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WeatherControl
{
    #region Custom data types

    /// <summary>
    /// Decimal separator override
    /// </summary>
    public enum decimalSeparatorType { useLocale = 0, useDot = 1, useComma = 2 }

    /// <summary>
    /// Boltwood Data Types
    /// </summary>
    public enum Enum_CloudCond { cloudUnknown = 0, cloudClear = 1, cloudCloudy = 2, cloudVeryCloudy = 3 }
    public enum Enum_WindCond { windUnknown = 0, windCalm = 1, windWindy = 2, windVeryWindy = 3 }
    public enum Enum_RainCond { rainUnknown = 0, rainDry = 1, rainWet = 2, rainRain = 3 }
    public enum Enum_DayCond { dayUnknown = 0, dayDark = 1, dayLight = 2, dayVeryLight = 3 }
    public enum Enum_RainFlag { rainFlagDry = 0, rainFlagLastminute = 1, rainFlagRightnow = 2 }
    public enum Enum_WetFlag { wetFlagDry = 0, wetFlagLastminute = 1, wetFlagRightnow = 2 }


    //Non canonical
    public enum Enum_RoofFlag { roofNotRequested = 0, roofWasRequested = 1 }
    public enum Enum_AlertFlag { alertNo = 0, alertYes = 1 }
    public enum Enum_CloudSensorModel { Classic = 1, AAG = 2 }

    #endregion

    /// <summary>
    /// Boltwood data fields base class (from specification)
    /// Shouldn't be used normally directly. More wisely to use extended class - BoltwoodClass
    /// </summary>        
    public class BoltwoodFields
    {
        public string Bolt_date = ""; //y y y y - m m - d d
        public string Bolt_time = ""; //h	h	:	m	m	:	s	s	.	s	s

        public string TempUnits = "C"; // C / F
        public string WindSpeedUnits = "K"; // K (=km per h) | M (=mph) | m (=m/s)

        public double Bolt_SkyTemp = -100; 
        public double Bolt_Temp = -100; 
        public double Bolt_SensorTemp = -100; //no direct var
        public double Bolt_WindSpeed = -100; //no direct var
        public double Bolt_Hum = -100; //no direct var
        public double Bolt_DewPoint = -100.0;
        public Int16 Bolt_Heater = -1; //0-100

        public Enum_RainFlag Bolt_RainFlag = Enum_RainFlag.rainFlagDry;
        public Enum_WetFlag Bolt_WetFlag = Enum_WetFlag.wetFlagDry;

        public UInt16 Bolt_SinceLastMeasure = 0; //sec since last data
        public double Bolt_now = 0; // date/time given as the VB6 Now() function result (in days) when Clarity II last wrote this file

        public Enum_CloudCond Bolt_CloudCond = Enum_CloudCond.cloudUnknown;
        public Enum_WindCond Bolt_WindCond = Enum_WindCond.windUnknown;
        public Enum_RainCond Bolt_RainCond = Enum_RainCond.rainUnknown;
        public Enum_DayCond Bolt_DaylighCond = Enum_DayCond.dayUnknown;

        public Enum_RoofFlag Bolt_RoofCloseFlag = Enum_RoofFlag.roofNotRequested; //roof close, =0 not requested, =1 if roof close was requested on this cycle
        public Enum_AlertFlag Bolt_AlertFlag = Enum_AlertFlag.alertNo; //alert, =0 when not alerting, =1 when alerting

        public BoltwoodFields()
        {
        }
            
        /// <summary>
        /// Copy constructor
        /// Coping from other BoltwoodFields obj
        /// </summary>
        /// <param name="objCopied"></param>
        public BoltwoodFields(BoltwoodFields objCopied)
        {
            Bolt_date = objCopied.Bolt_date;
            Bolt_time = objCopied.Bolt_time;

            TempUnits = objCopied.TempUnits;
            WindSpeedUnits = objCopied.WindSpeedUnits;

            Bolt_SkyTemp = objCopied.Bolt_SkyTemp;
            Bolt_Temp = objCopied.Bolt_Temp;
            Bolt_SensorTemp = objCopied.Bolt_SensorTemp;
            Bolt_WindSpeed = objCopied.Bolt_WindSpeed;
            Bolt_Hum = objCopied.Bolt_Hum;
            Bolt_Heater = objCopied.Bolt_Heater;

            Bolt_RainFlag = objCopied.Bolt_RainFlag;
            Bolt_WetFlag = objCopied.Bolt_WetFlag;

            Bolt_SinceLastMeasure = objCopied.Bolt_SinceLastMeasure;
            Bolt_now = objCopied.Bolt_now;

            Bolt_CloudCond = objCopied.Bolt_CloudCond;
            Bolt_WindCond = objCopied.Bolt_WindCond;
            Bolt_RainCond = objCopied.Bolt_RainCond;
            Bolt_DaylighCond = objCopied.Bolt_DaylighCond;

            Bolt_RoofCloseFlag = objCopied.Bolt_RoofCloseFlag;
            Bolt_AlertFlag = objCopied.Bolt_AlertFlag;
        }

        /// <summary>
        /// Contructor with simultaneous copy from another BoltwoodClass object 
        /// </summary>
        /// <param name="objCopied"></param>
        public BoltwoodFields(BoltwoodClass objCopied)
        {
            Bolt_date = objCopied.Bolt_date;
            Bolt_time = objCopied.Bolt_time;

            TempUnits = objCopied.TempUnits;
            WindSpeedUnits = objCopied.WindSpeedUnits;

            Bolt_SkyTemp = objCopied.Bolt_SkyTemp;
            Bolt_Temp = objCopied.Bolt_Temp;
            Bolt_SensorTemp = objCopied.Bolt_SensorTemp;
            Bolt_WindSpeed = objCopied.Bolt_WindSpeed;
            Bolt_Hum = objCopied.Bolt_Hum;
            Bolt_Heater = objCopied.Bolt_Heater;

            Bolt_RainFlag = objCopied.Bolt_RainFlag;
            Bolt_WetFlag = objCopied.Bolt_WetFlag;

            Bolt_SinceLastMeasure = objCopied.Bolt_SinceLastMeasure;
            Bolt_now = objCopied.Bolt_now;

            Bolt_CloudCond = objCopied.Bolt_CloudCond;
            Bolt_WindCond = objCopied.Bolt_WindCond;
            Bolt_RainCond = objCopied.Bolt_RainCond;
            Bolt_DaylighCond = objCopied.Bolt_DaylighCond;

            Bolt_RoofCloseFlag = objCopied.Bolt_RoofCloseFlag;
            Bolt_AlertFlag = objCopied.Bolt_AlertFlag;
        }
        
        /// <summary>
        /// Copy from another BoltwoodClass object 
        /// </summary>
        /// <param name="objCopied"></param>
        public void CopyEssentialParameters(BoltwoodFields objCopied)
        {
            if (objCopied == null) return;

            TempUnits = objCopied.TempUnits;
            WindSpeedUnits = objCopied.WindSpeedUnits;

            Bolt_SkyTemp = objCopied.Bolt_SkyTemp;
            Bolt_Temp = objCopied.Bolt_Temp;
            Bolt_SensorTemp = objCopied.Bolt_SensorTemp;
            Bolt_WindSpeed = objCopied.Bolt_WindSpeed;
            Bolt_Hum = objCopied.Bolt_Hum;
            Bolt_Heater = objCopied.Bolt_Heater;

            Bolt_RainFlag = objCopied.Bolt_RainFlag;
            Bolt_WetFlag = objCopied.Bolt_WetFlag;

            Bolt_CloudCond = objCopied.Bolt_CloudCond;
            Bolt_WindCond = objCopied.Bolt_WindCond;
            Bolt_RainCond = objCopied.Bolt_RainCond;
            Bolt_DaylighCond = objCopied.Bolt_DaylighCond;

            Bolt_RoofCloseFlag = objCopied.Bolt_RoofCloseFlag;
            Bolt_AlertFlag = objCopied.Bolt_AlertFlag;
        }

        public string SerializeToJSON_old()
        {
            string st = "";

            st += (st != String.Empty ? ", " : "") + "\"Bolt_date\": " + "\"" + Convert.ToString(Bolt_date) + "\"";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_time"": " + @"""" + Convert.ToString(Bolt_time) + @"""";

            st += (st != String.Empty ? ", " : "") + @"""Bolt_SkyTemp"": " + Convert.ToString(Bolt_SkyTemp);
            st += (st != String.Empty ? ", " : "") + @"""Bolt_Temp"": " + Convert.ToString(Bolt_Temp);

            st += (st != String.Empty ? ", " : "") + @"""Bolt_SensorTemp"": " + Convert.ToString(Bolt_SensorTemp);
            st += (st != String.Empty ? ", " : "") + @"""Bolt_WindSpeed"": " + Convert.ToString(Bolt_WindSpeed);
            st += (st != String.Empty ? ", " : "") + @"""Bolt_Hum"": " + Convert.ToString(Bolt_Hum);


            //st += (st != String.Empty ? ", " : "") + @"""Bolt_DewPoint"": " + @"""" + Convert.ToString(Bolt_DewPoint) + @"""" ;
            st += (st != String.Empty ? ", " : "") + @"""Bolt_DewPoint"": " + "3.1111";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_Heater"": " + Convert.ToString(Bolt_Heater);


            st += (st != String.Empty ? ", " : "") + @"""Bolt_RainFlag"": " + @"""" + Convert.ToString((int)Bolt_RainFlag) + @"""";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_WetFlag"": " + @"""" + Convert.ToString(Bolt_WetFlag) + @"""";


            //st += (st != String.Empty ? ", " : "") + @"""Bolt_SinceLastMeasure"": " + Convert.ToString(Bolt_SinceLastMeasure);
            //st += (st != String.Empty ? ", " : "") + @"""Bolt_now"": " + Convert.ToString(Bolt_now);

            st += (st != String.Empty ? ", " : "") + @"""Bolt_CloudCond"": " + @"""" + Convert.ToString(Bolt_CloudCond) + @"""";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_WindCond"": " + @"""" + Convert.ToString(Bolt_WindCond) + @"""";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_RainCond"": " + @"""" + Convert.ToString(Bolt_RainCond) + @"""";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_DaylighCond"": " + @"""" + Convert.ToString(Bolt_DaylighCond) + @"""";

            st += (st != String.Empty ? ", " : "") + @"""Bolt_RoofCloseFlag"": " + @"""" + Convert.ToString(Bolt_RoofCloseFlag) + @"""";
            st += (st != String.Empty ? ", " : "") + @"""Bolt_AlertFlag"": " + @"""" + Convert.ToString(Bolt_AlertFlag) + @"""";

            if (st != String.Empty) st = "{" + st + "}";

            return st;
        }

        public string SerializeToJSON()
        {
            //make copy of current obj in BoltwoodFields format (to not include misc fileds)
            BoltwoodFields obj = new BoltwoodFields(this);
            //serialize
            string st = new JavaScriptSerializer().Serialize(obj);

            return st;
        }

        public void DeserializeFromJSON(string st)
        {
            //Just for try
            var json = new JavaScriptSerializer().DeserializeObject(st);
            var json2 = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(st);

            //Convert to BoltwoodField object
            BoltwoodFields tempBoltwoodState = (BoltwoodFields)new JavaScriptSerializer().Deserialize(st, typeof(BoltwoodFields));
            CopyEssentialParameters(tempBoltwoodState);
        }

    }


    public class BoltwoodClass : BoltwoodFields
    {

        internal DateTime LastMeasure;      //DateTime when last was measured
        internal bool RainNowEvent_Flag = false;  //internal field set that it is raining right now
        internal bool WetNowEvent_Flag = false;   //internal field set that it is wet right now


        public DateTime Bolt_RainFlag_LastDetected; //last time Rain was detected
        internal UInt16 Bolt_RainFlag_sinceLastDetected = 65535; //sec passed since
        public DateTime Bolt_WetFlag_LastDetected; //last time Wet was detected
        internal UInt16 Bolt_WetFlag_sinceLastDetected = 65535; //sec passed since

        internal double Bolt_CloudIdx = -100; //Caclulated cloud idx
        internal double Bolt_CloudIdxAAG = -100; //Caclulated cloud idx by AAG

        public decimalSeparatorType ForcedDecimalSeparator = decimalSeparatorType.useLocale; //decimal separator

        /// <summary>
        /// Configuration block with constants and parameters
        /// </summary>
        #region CONFIGURATION PARAMETERS

        public bool DONT_USE_DIRECT_ACCESS = true;

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
        public Enum_CloudSensorModel CLOUDMODEL = Enum_CloudSensorModel.Classic; //Cloud Calculation model

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
                Bolt_DewPoint = BolwoodCalculations.calcDewPoint(Bolt_Temp, Bolt_Hum);
                return Bolt_DewPoint;
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
                RainNowEvent_Flag = value;
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
                WetNowEvent_Flag = value;
                if (value)
                {
                    Bolt_WetFlag_LastDetected = DateTime.Now;
                }
                SetMeasurement();
            }
        }


        public Enum_RainFlag RainFlag
        {
            get
            {
                if (DONT_USE_DIRECT_ACCESS)
                {
                    TimeSpan MeasureIntervalRF = DateTime.Now.Subtract(Bolt_RainFlag_LastDetected);
                    double Bolt_RainFlag_sinceLastDetected_dbl = Math.Round(MeasureIntervalRF.TotalSeconds, 0);
                    if (Bolt_RainFlag_sinceLastDetected_dbl > ushort.MaxValue) { Bolt_RainFlag_sinceLastDetected = ushort.MaxValue; }
                    else { Bolt_RainFlag_sinceLastDetected = (ushort)Bolt_RainFlag_sinceLastDetected_dbl; }


                    if (Bolt_RainFlag_sinceLastDetected <= TimeLimit_Now)
                    {
                        Bolt_RainFlag = Enum_RainFlag.rainFlagRightnow;
                    }
                    else if (Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                    {
                        Bolt_RainFlag = Enum_RainFlag.rainFlagLastminute;
                    }
                    else
                    {
                        Bolt_RainFlag = Enum_RainFlag.rainFlagDry;
                    }
                }

                return Bolt_RainFlag;
            }
        }

        public Enum_WetFlag WetFlag
        {
            get
            {
                if (DONT_USE_DIRECT_ACCESS)
                {
                    TimeSpan MeasureIntervalWF = DateTime.Now.Subtract(Bolt_WetFlag_LastDetected);
                    double Bolt_WetFlag_sinceLastDetected_dbl = Math.Round(MeasureIntervalWF.TotalSeconds, 0);
                    if (Bolt_WetFlag_sinceLastDetected_dbl > ushort.MaxValue) { Bolt_WetFlag_sinceLastDetected = ushort.MaxValue; }
                    else { Bolt_WetFlag_sinceLastDetected = (ushort)Bolt_WetFlag_sinceLastDetected_dbl; }

                    if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_Now)
                    {
                        Bolt_WetFlag = Enum_WetFlag.wetFlagRightnow;
                    }
                    else if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_LastMinute)
                    {
                        Bolt_WetFlag = Enum_WetFlag.wetFlagLastminute;
                    }
                    else
                    {
                        Bolt_WetFlag = Enum_WetFlag.wetFlagDry;
                    }
                }
                return Bolt_WetFlag;
            }
        }


        /// <summary>
        /// Set RainFlag firectly, try not to use it!
        /// </summary>
        internal Enum_RainFlag RainFlag_DirectSet
        {
            set
            {
                Bolt_RainFlag = value;

                if (DONT_USE_DIRECT_ACCESS)
                {
                    if (value == Enum_RainFlag.rainFlagRightnow)
                    {
                        //set raining now!
                        this.IsItRaining = true;
                        this.IsItWet = true;
                    }
                    else if (value == Enum_RainFlag.rainFlagLastminute)
                    {
                        this.IsItRaining = false;
                        Bolt_RainFlag_LastDetected = DateTime.Now.AddSeconds(-TimeLimit_Now - 1); //override date to the nearest valid past
                    }
                    else if (value == Enum_RainFlag.rainFlagDry)
                    {
                        this.IsItRaining = false;
                        Bolt_RainFlag_LastDetected = DateTime.Now.AddSeconds(-TimeLimit_LastMinute - 1); //override date to the nearest valid past
                    }
                }
                SetMeasurement();
            }
        }

        /// <summary>
        /// Set WetFlag firectly, try not to use it!
        /// </summary>
        internal Enum_WetFlag WetFlag_DirectSet
        {
            set
            {
                Bolt_WetFlag = value;

                if (DONT_USE_DIRECT_ACCESS)
                {

                    if (value == Enum_WetFlag.wetFlagRightnow)
                    {
                        //set raining now!
                        this.IsItWet = true;
                    }
                    else if (value == Enum_WetFlag.wetFlagLastminute)
                    {
                        this.IsItRaining = false;
                        Bolt_WetFlag_LastDetected = DateTime.Now.AddSeconds(-TimeLimit_Now - 1); //override date to the nearest valid past
                    }
                    else if (value == Enum_WetFlag.wetFlagDry)
                    {
                        this.IsItRaining = false;
                        Bolt_WetFlag_LastDetected = DateTime.Now.AddSeconds(-TimeLimit_LastMinute - 1); //override date to the nearest valid past
                    }
                }
                SetMeasurement();
            }
        }

        /// <summary>
        /// Get current rain condition
        /// Make this both on RainFlag and WetFlag
        /// during read set also Bolt_RainCond,Bolt_RainFlag, Bolt_WetFlag
        /// But it seems that is is ambigious
        /// </summary>
        public Enum_RainCond RainCond
        {
            get
            {
                if (DONT_USE_DIRECT_ACCESS)
                {
                    RecheckTimeLimits();
                    EvaluateRainWetConditions();
                }

                return Bolt_RainCond;
            }
        }

        public Enum_RainCond RainCond_DirectSet
        {
            set
            {
                Bolt_RainCond = value;
                if (DONT_USE_DIRECT_ACCESS)
                {
                    if (value == Enum_RainCond.rainRain)
                    {
                        this.IsItRaining = true;
                        this.IsItWet = true;
                    }
                    else if (value == Enum_RainCond.rainWet)
                    {
                        this.IsItRaining = false;
                        this.IsItWet = true;
                    }
                    else if (value == Enum_RainCond.rainDry)
                    {
                        this.IsItRaining = false;
                        this.IsItWet = false;
                    }
                    else if (value == Enum_RainCond.rainUnknown)
                    {
                        this.IsItRaining = false;
                        this.IsItWet = false;
                    }
                        
                }
                SetMeasurement();
            }
        }
        #endregion

        #region Conditions
        public Enum_CloudCond CloudCond
        {
            get
            {
                if (DONT_USE_DIRECT_ACCESS)
                {
                    EvaluateCloudConditions();
                }
                return Bolt_CloudCond;
            }
        }

        public Enum_WindCond WindCond
        {
            get
            {
                if (DONT_USE_DIRECT_ACCESS)
                {
                    EvaluateWindConditions();
                }
                return Bolt_WindCond;
            }
        }

        public Enum_DayCond DaylightCond
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

        public Enum_CloudCond CloudCond_DirectSet
        {
            set
            {
                Bolt_CloudCond = value;
                if (DONT_USE_DIRECT_ACCESS)
                {
                    if (value == Enum_CloudCond.cloudClear)
                    {
                        Bolt_SkyTemp = Bolt_Temp - (CLOUDINDEX_CLEAR);
                    }
                    else if (value == Enum_CloudCond.cloudCloudy)
                    {
                        Bolt_SkyTemp = Bolt_Temp - (CLOUDINDEX_CLOUDY);
                    }
                    else if (value == Enum_CloudCond.cloudVeryCloudy)
                    {
                        Bolt_SkyTemp = Bolt_Temp;
                    }
                    else if (value == Enum_CloudCond.cloudUnknown)
                    {
                        Bolt_SkyTemp = Bolt_Temp - (CLOUDINDEX_CLOUDY_BAD-10);
                    }
                }
                SetMeasurement();
            }
        }

        public Enum_WindCond WindCond_DirectSet
        {
            set
            {
                Bolt_WindCond = value;
                SetMeasurement();
            }
        }
        #endregion

        #region Alarms
        public Enum_RoofFlag RoofCloseFlag
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

        public Enum_AlertFlag AlertFlag
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
                double SinceLastMeasure_dbl= Math.Round(MeasureInterval.TotalSeconds, 0);
                if (SinceLastMeasure_dbl > ushort.MaxValue)
                {
                    Bolt_SinceLastMeasure = ushort.MaxValue;
                }
                else
                {
                    Bolt_SinceLastMeasure = Convert.ToUInt16(SinceLastMeasure_dbl);
                }
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
        public void SetMeasurement()
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
                RainNowEvent_Flag = true;
                WetNowEvent_Flag = true;
            }

            //Recheck wet
            if (Bolt_WetFlag_sinceLastDetected <= TimeLimit_Now)
            {
                //STILL WET!
                WetNowEvent_Flag = true;
            }
        }


        /// <summary>
        /// Calculate RainCond, RainFlag, WetFlag status
        /// </summary>
        private void EvaluateRainWetConditions()
        {
            if (RainNowEvent_Flag)
            {
                //RAIN
                Bolt_RainCond = Enum_RainCond.rainRain;
                Bolt_RainFlag = Enum_RainFlag.rainFlagRightnow;
                Bolt_WetFlag = Enum_WetFlag.wetFlagRightnow;
            }
            else if (!RainNowEvent_Flag && WetNowEvent_Flag)
            {
                //WET
                Bolt_RainCond = Enum_RainCond.rainWet;
                Bolt_WetFlag = Enum_WetFlag.wetFlagRightnow;

                //Check Bolt_RainFlag
                if (Bolt_RainFlag_sinceLastDetected > TimeLimit_Now && Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_RainFlag = Enum_RainFlag.rainFlagLastminute;
                }
                else
                {
                    Bolt_RainFlag = Enum_RainFlag.rainFlagDry;
                }
            }
            else
            {
                //DRY
                Bolt_RainCond = Enum_RainCond.rainDry;

                //Check Bolt_RainFlag also
                if (Bolt_RainFlag_sinceLastDetected > TimeLimit_Now && Bolt_RainFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_RainFlag = Enum_RainFlag.rainFlagLastminute;
                }
                else
                {
                    Bolt_RainFlag = Enum_RainFlag.rainFlagDry;
                }

                //Check Wet_RainFlag also
                if (Bolt_WetFlag_sinceLastDetected > TimeLimit_Now && Bolt_WetFlag_sinceLastDetected <= TimeLimit_LastMinute)
                {
                    Bolt_WetFlag = Enum_WetFlag.wetFlagLastminute;
                }
                else
                {
                    Bolt_WetFlag = Enum_WetFlag.wetFlagDry;
                }
            }
        }

        private void EvaluateCloudConditions()
        {
            //Cloud condition
            Bolt_CloudIdx = this.calcCloudIndex(Bolt_SkyTemp, Bolt_Temp);
            Bolt_CloudIdxAAG = this.calcCloudIndexCorr(Bolt_SkyTemp, Bolt_Temp);

            if (CLOUDMODEL == Enum_CloudSensorModel.Classic)
            {
                if (Bolt_CloudIdx >= CLOUDINDEX_CLEAR) { Bolt_CloudCond = Enum_CloudCond.cloudClear; }
                else if (Bolt_CloudIdx >= CLOUDINDEX_CLOUDY) { Bolt_CloudCond = Enum_CloudCond.cloudCloudy; }
                else if (Bolt_CloudIdx > CLOUDINDEX_CLOUDY_BAD) { Bolt_CloudCond = Enum_CloudCond.cloudVeryCloudy; }
                else { Bolt_CloudCond = Enum_CloudCond.cloudUnknown; }
            }
            else
            {
                if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLEAR) { Bolt_CloudCond = Enum_CloudCond.cloudClear; }
                else if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLOUDY) { Bolt_CloudCond = Enum_CloudCond.cloudCloudy; }
                else if (Bolt_CloudIdxAAG >= CLOUDINDEXAAG_CLOUDY_BAD) { Bolt_CloudCond = Enum_CloudCond.cloudVeryCloudy; }
                else { Bolt_CloudCond = Enum_CloudCond.cloudUnknown; }
            }
        }

        private void EvaluateWindConditions()
        {
            Bolt_WindCond = Enum_WindCond.windUnknown;
            if (Bolt_WindSpeed >= WINDSPEED_VERYWINDY) { Bolt_WindCond = Enum_WindCond.windVeryWindy; }
            else if (Bolt_WindSpeed >= WINDSPEED_WINDY) { Bolt_WindCond = Enum_WindCond.windWindy; }
            else if (Bolt_WindSpeed < 0) { Bolt_WindCond = Enum_WindCond.windUnknown; }
            else { Bolt_WindCond = Enum_WindCond.windCalm; }
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

            //Decimal separator conversion
            if (ForcedDecimalSeparator == decimalSeparatorType.useDot)
            {
                bold_st=bold_st.Replace(',', '.');
            }
            else if (ForcedDecimalSeparator == decimalSeparatorType.useComma)
            {
                bold_st = bold_st.Replace('.', ',');
            }

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