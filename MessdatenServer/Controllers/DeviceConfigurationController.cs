using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessdatenServer.Models;
using MessdatenServer.services;
using System.Web.Http.Cors;
using System.Net.Http.Headers;
using System.IO;

namespace MessdatenServer.Controllers
{

   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DeviceConfigurationController : ApiController
    {

        [HttpGet]
        [Route("messdatenServer/list")]
        public IHttpActionResult GetDeviceList()
        {
            return Ok(ConfigurationAccess.GetDeviceListFromConfig());
        }

        [HttpGet]
        [Route("messdatenServer/device/{id}")]
        public IHttpActionResult GetDevice(String id)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            return Ok(ConfigurationAdapter.GetDeviceFromConfig(devices, id));
        }

        [HttpPost]
        [Route("messdatenServer/update")]
        public IHttpActionResult UpdateDevice([FromBody]Device updatedDevice)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            if (services.ConfigurationAdapter.UpdateDeviceInConfig(devices, updatedDevice) == null)
            {
                return BadRequest("Update nicht erfolgreich");
            }
            return Ok();
        }

        [HttpPost]
        [Route("messdatenServer/save")]
        public IHttpActionResult SaveDevice([FromBody]Device newDevice)
        {
            if (!Validator.IsNewDeviceNameValid(newDevice))
            {
                return BadRequest("Die Id " + newDevice.Id + " existiert bereits in der Konfiguration,\n\ndie ID muss eindeutig sein!");
            }

            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            if (!services.ConfigurationAdapter.SaveNewDeviceInConfig(devices, newDevice))
            {
                return BadRequest("Device " + newDevice.Id + " konnte nicht gespeichert werden!");
            }
            return Ok();
        }

        [HttpGet]
        [Route("messdatenServer/delete/{id}")]
        public IHttpActionResult DeleteDevice(String id)
        {
            List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
            if (!services.ConfigurationAdapter.DeleteDeviceInConfig(devices, id))
            {
                return BadRequest("Device " + id + " konnte nicht gelöscht werden!");
            }
            return Ok();
        }
    }

   
}
