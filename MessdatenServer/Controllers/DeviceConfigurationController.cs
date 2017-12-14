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
            try
            {
                return Ok(ConfigurationAccess.GetDeviceListFromConfig());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("messdatenServer/device/{id}")]
        public IHttpActionResult GetDevice(String id)
        {
            try
            {
                List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
                return Ok(DeviceListHandler.GetDeviceFromDeviceList(devices, id));
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("messdatenServer/update")]
        public IHttpActionResult UpdateDevice([FromBody]Device updatedDevice)
        {
            try
            {
                List<Device> devicesInConfig = ConfigurationAccess.GetDeviceListFromConfig();
                List<Device> updetedList = DeviceListHandler.UpdateDeviceInDeviceList(devicesInConfig, updatedDevice);
                ConfigurationAccess.SaveDeviceListToConfig(updetedList);
                return Ok();
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("messdatenServer/save")]
        public IHttpActionResult SaveDevice([FromBody]Device newDevice)
        {
            try
            {
                List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
                if (!Validator.IsNewDeviceNameValid(devices, newDevice))
                {
                    return BadRequest("Die Id " + newDevice.Id + " existiert bereits in der Konfiguration, die ID muss eindeutig sein!");
                }
                List<Device> savedDevices = DeviceListHandler.SaveNewDeviceInDeviceList(devices, newDevice);
                ConfigurationAccess.SaveDeviceListToConfig(savedDevices);
                return Ok();
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("messdatenServer/delete/{id}")]
        public IHttpActionResult DeleteDevice(String id)
        {
            try
            {
                List<Device> devices = ConfigurationAccess.GetDeviceListFromConfig();
                List<Device> modifiedList = DeviceListHandler.DeleteDeviceInDeviceList(devices, id);
                ConfigurationAccess.SaveDeviceListToConfig(modifiedList);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

   
}
