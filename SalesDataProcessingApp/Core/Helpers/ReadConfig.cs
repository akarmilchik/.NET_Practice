using System.Configuration;

namespace Core.Helpers
{
    public static class ReadConfig
    {
        public static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;

            var settingString = appSettings[key];

            if (settingString != null && settingString != string.Empty)
            {
                return settingString;
            }

            return string.Empty;
        }
    }
}