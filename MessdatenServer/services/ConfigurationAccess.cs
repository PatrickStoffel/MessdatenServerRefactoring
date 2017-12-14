using System;
using System.Collections.Generic;
using MessdatenServer.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Web;

namespace MessdatenServer.services
{
    public class ConfigurationAccess
    {
        public static List<Device> GetDeviceListFromConfig()
        {
            List<Device> items = null;
            using (StreamReader reader = new StreamReader(Properties.Settings.Default.ConfigPath))
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
            using (StreamWriter writer = new StreamWriter(Properties.Settings.Default.ConfigPath))
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
    }
}