using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherControl;

namespace ObservatoryCenter
{
    internal class ObservatoryControls_boltwood
    {

        public BoltwoodClass BoltwoodObj;
        private BoltwoodFields BoltwoodObj_GoodState;
        private BoltwoodFields BoltwoodObj_BadState;

        /// <summary>
        /// Constructor
        /// </summary>
        public ObservatoryControls_boltwood()
        {
            BoltwoodFileClass.BoltwoodFilePath = ConfigManagement.ProgDocumentsPath;

            BoltwoodObj = new BoltwoodClass();
            BoltwoodObj_GoodState = new BoltwoodFields();
            BoltwoodObj_BadState = new BoltwoodFields();

            //set settings
            BoltwoodObj.ForcedDecimalSeparator = decimalSeparatorType.useComma;
            BoltwoodObj.DONT_USE_DIRECT_ACCESS = false;

            //load default presets
            Load_Presets();

            Switch_to_GOOD();
        }

        public void UpdateFile()
        {
            //Update boltwood file
            WriteFile();
        }


        private void WriteFile()
        {
            BoltwoodObj.SetMeasurement(); //update measured time
            BoltwoodFileClass.WriteBoltwoodData(BoltwoodObj.getBoltwoodString());
        }

        public void Switch_to_GOOD()
        {
            BoltwoodObj.CopyEssentialParameters(BoltwoodObj_GoodState);
            WriteFile();
        }

        public void Switch_to_BAD()
        {
            BoltwoodObj.CopyEssentialParameters(BoltwoodObj_BadState);
            WriteFile();
        }

        private void Load_Presets()
        {

            //good
            string st_good = @"{""Bolt_date"":"""",""Bolt_time"":"""",""TempUnits"":""C"",""WindSpeedUnits"":""K"",""Bolt_SkyTemp"":30,""Bolt_Temp"":10,""Bolt_SensorTemp"":10,""Bolt_WindSpeed"":2,""Bolt_Hum"":70,""Bolt_DewPoint"":3,""Bolt_Heater"":30,""Bolt_RainFlag"":0,""Bolt_WetFlag"":0,""Bolt_SinceLastMeasure"":0,""Bolt_now"":0,""Bolt_CloudCond"":1,""Bolt_WindCond"":1,""Bolt_RainCond"":1,""Bolt_DaylighCond"":1,""Bolt_RoofCloseFlag"":0,""Bolt_AlertFlag"":0}";
            BoltwoodObj_GoodState.DeserializeFromJSON(st_good);

            //bad
            string st_bad = @"{""Bolt_date"":"""",""Bolt_time"":"""",""TempUnits"":""C"",""WindSpeedUnits"":""K"",""Bolt_SkyTemp"":10,""Bolt_Temp"":10,""Bolt_SensorTemp"":10,""Bolt_WindSpeed"":20,""Bolt_Hum"":70,""Bolt_DewPoint"":3,""Bolt_Heater"":30,""Bolt_RainFlag"":2,""Bolt_WetFlag"":2,""Bolt_SinceLastMeasure"":0,""Bolt_now"":0,""Bolt_CloudCond"":3,""Bolt_WindCond"":3,""Bolt_RainCond"":3,""Bolt_DaylighCond"":1,""Bolt_RoofCloseFlag"":1,""Bolt_AlertFlag"":1}";
            BoltwoodObj_BadState.DeserializeFromJSON(st_bad);
        }

    }
}
