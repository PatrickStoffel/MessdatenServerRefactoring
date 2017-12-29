using System;
using System.Collections.Generic;
using MessdatenServer.Models;
using System.IO;
using Newtonsoft.Json;

namespace MessdatenServer.services
{
    public class ConfigurationAccess
    {
        public static List<Device> GetDeviceListFromConfig()
        {
            List<Device> items = null;
            using (StreamReader reader = new StreamReader(GetConfigFilePath()))
            {
                string json = null;
                try
                {
                    json = reader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw new ReadWriteException("Device-Liste konnte nicht aus der Konfiguration gelesen werden", ex);
                }
                items = JsonConvert.DeserializeObject<List<Device>>(json);
            }
            return items;
        }

        public static bool SaveDeviceListToConfig(List<Device> devices)
        {
            using (StreamWriter writer = new StreamWriter(GetConfigFilePath()))
            {
                string json = JsonConvert.SerializeObject(devices);
                try
                {
                    writer.Write(json);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new ReadWriteException("Device-Liste konnte nicht in Konfiguration gespeichert werden", ex);
                }
            }
        }

        public static void SetTestConfigFile()
        {
            try
            {
                File.Copy(GetConfigFilePath(), GetTempConfigFilePath(), true);
                File.Copy(GetTestConfigFilePath(), GetConfigFilePath(), true);
            }
            catch (Exception ex)
            {
                throw new ReadWriteException("Das Test-Configfile konnte nicht erstellt werden!", ex);
            }
        }

        public static void RestoreConfigFile()
        {
            try
            {
                File.Copy(GetTempConfigFilePath(), GetConfigFilePath(), true);
            }
            catch (Exception ex)
            {
                throw new ReadWriteException("Das Configfile konnte nicht wieder hergestellt werden!", ex);
            }
        }

        private static string GetConfigFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), Properties.Settings.Default.ConfigFile);
        }

        private static string GetTempConfigFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), Properties.Settings.Default.TempConfigFile);
        }

        private static string GetTestConfigFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), Properties.Settings.Default.TestConfigFile);
        }
    }
}