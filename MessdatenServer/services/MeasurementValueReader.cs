using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessdatenServer.Models;
using MessdatenServer.Adapter;

namespace MessdatenServer.services
{
    public class MeasurementValueReader
    {
        public static String GetActualMeasurementValue(Device deviceToRead)
        {
            String actualValue = null;

            switch (deviceToRead.Protocol)
            {
                case "com-1":
                    actualValue = new SylcvacComAccess().GetActualValueFromComInterface(deviceToRead);
                    break;
                default:
                    throw new ReadWriteException("Keine Schnittstelle für das Prokololl " + deviceToRead.Protocol + " implementiert");
            }           
            return actualValue;
        }



    }
}