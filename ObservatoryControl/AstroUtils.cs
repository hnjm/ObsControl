using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCOM.Utilities;


namespace ObservatoryCenter
{
    public class AstroUtils
    {
        static ASCOM.Utilities.Util ASCOMUtils;

        static AstroUtils()
        {
            ASCOMUtils = new Util();
        }

        static DateTime GetUTCTime()
        {
            return DateTime.UtcNow;
        }

        static public double GetJD()
        {
            return ASCOMUtils.JulianDate;
        }


    }
}
