using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessdatenServer.Models;
using System.IO;

namespace MessdatenServer.services
{
    public class ConfigurationAdapter
    {
        
        public static Device GetDeviceFromConfig(String deviceId)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();

            foreach (Device device in devices)
            {
                if (device.Id.Equals(deviceId))
                {
                    return device;
                }
            }
            return null;
        }

        public static Device UpdateDeviceInConfig(Device updatedDevice)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();

            foreach (Device origDevice in devices)
            {
                if (origDevice.Id.Equals(updatedDevice.Id))
                {
                    origDevice.HostIp = updatedDevice.HostIp;
                    origDevice.DataSource = updatedDevice.DataSource;
                    origDevice.Group = updatedDevice.Group;
                    origDevice.Protocol = updatedDevice.Protocol;

                    if (!ConfigurationAccess.SaveDeviceListToConfig(devices))
                    {
                        return null;
                    }
                    return origDevice;
                }
            }
            return null;
        }

        public static bool SaveNewDeviceInConfig(Device newdDevice)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            devices.Add(newdDevice);
            if (!ConfigurationAccess.SaveDeviceListToConfig(devices))
            {
                return false;
            }
            return true;
        }

        public static bool DeleteDeviceInConfig(String deviceId)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
           
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Id.Equals(deviceId))
                {
                    devices.RemoveAt(i);
                    break;            
                }
            }
          
            if (!ConfigurationAccess.SaveDeviceListToConfig(devices))
            {
                return false;
            }
            return true;
        }
    }
}