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
                string json = reader.ReadToEnd();
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
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}