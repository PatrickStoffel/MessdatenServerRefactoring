using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessdatenServer.Models
{
    public class MeasurementData
    {
        public MeasurementData()
        {

        }

        public MeasurementData(String macAdress, double value, DateTime actualTime)
        {
            this.MacAdress = macAdress;
            this.Value = value;
            this.TimeStamp = actualTime;
        }

        public String MacAdress { get; set;}
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}