﻿using Newtonsoft.Json;
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
        
        public static Device GetDeviceFromConfig(List<Device> devices,String deviceId)
        {
            foreach (Device device in devices)
            {
                if (device.Id.Equals(deviceId))
                {
                    return device;
                }
            }
            throw new ReadWriteException("Device " + deviceId + " wurde in der Konfiguration nicht gefunden!");
        }

        public static Device UpdateDeviceInConfig(List<Device> devices,Device updatedDevice)
        {
            foreach (Device origDevice in devices)
            {
                if (origDevice.Id.Equals(updatedDevice.Id))
                {
                    origDevice.HostIp = updatedDevice.HostIp;
                    origDevice.DataSource = updatedDevice.DataSource;
                    origDevice.Group = updatedDevice.Group;
                    origDevice.Protocol = updatedDevice.Protocol;

                    return origDevice;
                }
            }
            throw new ReadWriteException("Device " + updatedDevice.Id + " konnte nicht upgedated werden!");
        }

        public static bool SaveNewDeviceInConfig(List<Device> devices, Device newdDevice)
        {
            devices.Add(newdDevice);
            return true;
        }

        public static bool DeleteDeviceInConfig(List<Device> devices, String deviceId)
        {         
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Id.Equals(deviceId))
                {
                    devices.RemoveAt(i);
                    return true;            
                }
            }
            throw new ReadWriteException("Device " + deviceId + " konnte nicht gelöscht werden!");
        }
    }
}