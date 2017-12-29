using System;
using System.Collections.Generic;
using System.Web.Http;
using MessdatenServer.Models;
using MessdatenServer.services;

namespace MessdatenServer.Controllers
{
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

        [HttpGet]
        [Route("messdatenServer/settest/{option}")]
        public IHttpActionResult SetTestConfigFile(String option)
        {
            try
            {
                switch (option)
                {
                    case "set":
                        ConfigurationAccess.SetTestConfigFile();
                        break;
                    case "reset":
                        ConfigurationAccess.RestoreConfigFile();
                        break;
                }
                return Ok();
            }
            catch (ReadWriteException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

   
}
