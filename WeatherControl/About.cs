using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;

using System.Reflection; //for assembly version
using System.Deployment.Application; //for publish version. Add System.Deployment reference
using System.Diagnostics; //for file verision


namespace WeatherControl
{

    /// <summary>
    /// Class for handling version information
    /// </summary>
    public static class VersionData
    {
        public static Version AssemblyVersion;
        public static string AssemblyVersionSt = "";

        public static string FileVersionSt = "";

        public static string ProductVersionSt = "";

        public static Version PublishVersion;
        public static string PublishVersionSt = "";

        public static DateTime CompileTime;
        public static string CompileTimeSt = "";


        public static void initVersionData()
        {
            //Assembly Version
            VersionData.AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            VersionData.AssemblyVersionSt = VersionData.AssemblyVersion.Major.ToString() + "." + VersionData.AssemblyVersion.Minor.ToString() + "." + VersionData.AssemblyVersion.Build.ToString() + " rev " + VersionData.AssemblyVersion.Revision.ToString();

            //File Version
            VersionData.FileVersionSt = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

            //Product Version
            VersionData.ProductVersionSt = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            //Publish version
            VersionData.PublishVersion = new Version("0.0.0.0");
            try
            {
                VersionData.PublishVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch { }
            VersionData.PublishVersionSt = VersionData.PublishVersion.Major.ToString() + "." + VersionData.PublishVersion.Minor.ToString() + "." + VersionData.PublishVersion.Build.ToString() + " rev " + VersionData.PublishVersion.Revision.ToString();

            VersionData.CompileTime = RetrieveLinkerTimestamp();
        }

        public static string getVersionString()
        {
            string VersionSt = "";
            VersionSt += "Publish version: " + VersionData.PublishVersionSt;
            VersionSt += Environment.NewLine + "Assembly version: " + VersionData.AssemblyVersionSt;
            VersionSt += Environment.NewLine + "File version: " + VersionData.FileVersionSt;
            //VersionSt += Environment.NewLine + "Product version " + ProductVersionSt;

            //MessageBox.Show("Application " + assemName.Name + ", Version " + ver.ToString());
            VersionSt += Environment.NewLine + "Compile time: " + VersionData.CompileTime.ToString("yyyy-MM-dd HH:mm:ss");

            return VersionSt;
        }

        private static DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

    }


}
