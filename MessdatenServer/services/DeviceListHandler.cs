using System;
using System.Collections.Generic;
using MessdatenServer.Models;

namespace MessdatenServer.services
{
    public class DeviceListHandler
    {
        
        public static Device GetDeviceFromDeviceList(List<Device> devices,String deviceId)
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

        public static List<Device> UpdateDeviceInDeviceList(List<Device> devices,Device updatedDevice)
        {
            foreach (Device origDevice in devices)
            {
                if (origDevice.Id.Equals(updatedDevice.Id))
                {
                    origDevice.HostIp = updatedDevice.HostIp;
                    origDevice.DataSource = updatedDevice.DataSource;
                    origDevice.Group = updatedDevice.Group;
                    origDevice.Protocol = updatedDevice.Protocol;

                    return devices;
                }
            }
            throw new ReadWriteException("Update nicht erfolgreich, der Device mit Id " + updatedDevice.Id + " ist nicht in der Konfiguration!");
        }

        public static List<Device> SaveNewDeviceInDeviceList(List<Device> devices, Device newDevice)
        {
            foreach (Device existingDevice in devices)
            {
                if (existingDevice.Id.Equals(newDevice.Id.Trim()))
                {
                    throw new ReadWriteException("Die Id " + newDevice.Id + " existiert bereits in der Konfiguration, die ID muss eindeutig sein!");
                }
            }
            devices.Add(newDevice);
            return devices;
        }

        public static List<Device> DeleteDeviceInDeviceList(List<Device> devices, String deviceId)
        {         
            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].Id.Equals(deviceId))
                {
                    devices.RemoveAt(i);
                    return devices;            
                }
            }
            throw new ReadWriteException("Löschen nicht erfolgreich, der Device mit Id " + deviceId + " ist nicht in der Konfiguration!");
        }
    }
}