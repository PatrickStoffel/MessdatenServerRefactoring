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
        public static String GetActualMeasurementValue(Device deviceToRead, Dictionary<String, String> messages)
        {
            String actualValue = null;

            switch (deviceToRead.Protocol)
            {
                case "com-1":
                    actualValue = new SylcvacComAccess(messages).GetActualValueFromComInterface(deviceToRead);
                    break;
                default:
                    messages[deviceToRead.Id] = "Keine Schnittstelle für das Prokololl " + deviceToRead.Protocol + " implementiert";
                    break;
            }           
            return actualValue;
        }



    }
}