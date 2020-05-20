using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Samples.Common.Configuration
{
    public static class ConfigurationHelper
    {
        public static int GetInt(string configurationKey, int defaultValue)
        {
            var appSetting = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(appSetting))
            {
                return defaultValue;
            }

            if (!int.TryParse(appSetting, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            {
                Trace.TraceError("Could not parse AppSetting {configurationKey} into an integer, using default");
                value = defaultValue;
            }

            if (value <= 0)
            {
                value = defaultValue;
            }

            return value;
        }

        public static bool GetBool(string configurationKey, bool defaultValue = false)
        {
            var appSetting = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(appSetting))
            {
                return defaultValue;
            }

            if (bool.TryParse(appSetting, out var parsedValue))
            {
                return parsedValue;
            }

            Trace.TraceError($"Could not parse AppSetting {configurationKey} into a boolean, using default");
            return defaultValue;
        }

        public static ICollection<string> GetStrings(string configurationKey, char separator)
        {
            var results = new List<string>();

            var appSetting = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(appSetting))
            {
                return results;
            }

            return appSetting.Split(separator).ToList();
        }
    }
}
