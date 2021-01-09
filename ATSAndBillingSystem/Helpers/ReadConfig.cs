using System;
using System.Configuration;

namespace ATS.Helpers
{
    public static class ReadConfig
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                string result = appSettings[key] ?? "Not Found";

                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return string.Empty;
        }
    }
}