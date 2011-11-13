using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Windows.Forms;

namespace RecommenderSystem.Core.Helper
{
    public static class SystemHelper
    {
        public static void LogEntry(string message)
        {
            LogEntry log = new LogEntry();
            log.Message = message;
            MessageBox.Show(message);
            //Logger.Write(log);
        }

        public static string GetConfigValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }

        public static void SetConfigValue(string keyName, string newValue)
        {

            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(
              ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove(keyName);
            System.Configuration.KeyValueConfigurationElement element = new System.Configuration.KeyValueConfigurationElement(keyName, newValue);
            config.AppSettings.Settings.Add(element);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void EnrichUserData(ref System.Data.DataTable user)
        {
            foreach (System.Data.DataRow row in user.Rows)
            {
                row["password"] = CreateRandomString(row["id"].ToString(), 5);
            }
        }

        private static String CreateRandomString(string key, int len)
        {
            string result = "";
            char temp;
            for (int i = 0; i < len; i++)
            {
                temp = ((char)(long.Parse(key) % (0x5a - 0x41 + i) + 0x40));

                if (temp < 'A')
                    temp = 'A';
                if (temp > 'Z')
                    temp = 'Z';
                result += temp;
            }
            return result;
        }

    }
}
