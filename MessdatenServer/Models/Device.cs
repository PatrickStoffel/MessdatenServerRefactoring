using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MessdatenServer.Models
{
    public class Device
    {
        public String Id { get; set; }
        public String HostIp { get; set; }
        public String DataSource { get; set; }
        public String Group { get; set; }
        public String Protocol { get; set; }

        public Device(String id, String hostIp, String dataSource, String group, String protocol)
        {
            this.Id = id;
            this.HostIp = hostIp;
            this.DataSource = dataSource;
            this.Group = group;
            this.Protocol = protocol;
        }


    }
}