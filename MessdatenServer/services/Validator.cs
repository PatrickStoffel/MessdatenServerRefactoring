using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessdatenServer.Models;

namespace MessdatenServer.services
{
    public class Validator
    {
        public static bool IsNewDeviceNameValid(List<Device> devices, Device newDevice)
        {
            foreach (Device existingDevice in devices)
            {
                if (existingDevice.Id.Equals(newDevice.Id.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}