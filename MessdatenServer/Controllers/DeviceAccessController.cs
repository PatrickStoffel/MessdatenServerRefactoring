using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessdatenServer.services;
using MessdatenServer.Models;
using MessdatenServer.Adapter;

namespace MessdatenServer.Controllers
{
    public class DeviceAccessController : ApiController
    {
        Dictionary<String, String> messages = new Dictionary<string, string>();

        [HttpGet]
        [Route("messdatenServer/value/{id}")]
        public IHttpActionResult GetMeasurementValue(String id)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            Device deviceToRead = ConfigurationAdapter.GetDeviceFromDeviceList(devices,id);
            if(deviceToRead == null)
            {
                return BadRequest("Device mit Id " + id + " wurde in der Konfiguration nicht gefunden!");
            }
            String actualValue = MeasurementValueReader.GetActualMeasurementValue(deviceToRead, messages);
            if(actualValue == null)
            {
                return BadRequest(messages[deviceToRead.Id]);
            }
            return Ok(Double.Parse(actualValue));
        }

        [HttpGet]
        [Route("messdatenServer/setZero/{id}")]
        public IHttpActionResult SetValueZero(String id)

        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            Device deviceToRead = ConfigurationAdapter.GetDeviceFromDeviceList(devices, id);
            if (deviceToRead == null)
            {
                return BadRequest("Device mit Id " + id + " wurde in der Konfiguration nicht gefunden!");
            }
            String handShake = new SylcvacComAccess(messages).SetActualValueToZero(deviceToRead);
            if (handShake == null)
            {
                return BadRequest(messages[deviceToRead.Id]);
            }
            return Ok(handShake);
        }
    }
}
