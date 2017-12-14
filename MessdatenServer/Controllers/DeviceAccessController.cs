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
            try
            {
                List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
                Device deviceToRead = DeviceListHandler.GetDeviceFromDeviceList(devices, id);
                String actualValue = MeasurementValueReader.GetActualMeasurementValue(deviceToRead, messages);
                return Ok(Double.Parse(actualValue));
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }       
        }

        [HttpGet]
        [Route("messdatenServer/setZero/{id}")]
        public IHttpActionResult SetValueZero(String id)

        {
            try
            {
                List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
                Device deviceToRead = DeviceListHandler.GetDeviceFromDeviceList(devices, id);
                String handShake = new SylcvacComAccess(messages).SetActualValueToZero(deviceToRead);
                return Ok(handShake);
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
