using NUnit.Framework;
using MessdatenServer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessdatenServer.Models;

namespace MessdatenServer.services.Tests
{
    [TestFixture()]
    public class DeviceListHandlerTests
    {
        private List<Device> devices = null;
        private Device dev1 = null;
        private Device dev2 = null;
        private Device dev3 = null;


        [SetUp]
        public void Init()
        {
            devices = new List<Device>();
            dev1 = new Device("dev1", "230.25.35.41", "COM1", "A", "com-1");
            dev2 = new Device("dev2", "230.25.35.42", "COM2", "B", "com-1");
            dev3 = new Device("dev3", "230.25.35.43", "COM3", "C", "com-1");
            devices.Add(dev1);
            devices.Add(dev2);
           
        }

        [Test()]
        public void GetDeviceFromDeviceList_WithExistingDevideId_ReturnsExpectedId()
        {
            Device device = DeviceListHandler.GetDeviceFromDeviceList(devices, "dev2");

            Assert.AreEqual("dev2", device.Id);
        }

        [Test()]
        public void UpdateDeviceInDeviceList_WithChangedProtocol_ReturnsExpectedProtocol()
        {
            dev1.Protocol = "txt";

            DeviceListHandler.UpdateDeviceInDeviceList(devices, dev1);

            Assert.AreEqual("txt", DeviceListHandler.GetDeviceFromDeviceList(devices, "dev1").Protocol);
        }

        [Test()]
        public void SaveNewDeviceInDeviceList_WithNewId_IncrementListCount()
        {
            int noOfDevicesBevorAddDeviceToList = devices.Count;

            DeviceListHandler.SaveNewDeviceInDeviceList(devices, dev3);

            Assert.AreEqual(noOfDevicesBevorAddDeviceToList + 1, devices.Count);
        }

        [Test()]
        public void DeleteDeviceInDeviceList_WithExistingId_DecrementListCount()
        {
            int noOfDevicesBevorDeleteDeviceFromList = devices.Count;

            DeviceListHandler.DeleteDeviceInDeviceList(devices, "dev1");

            Assert.AreEqual(noOfDevicesBevorDeleteDeviceFromList - 1, devices.Count);
        }

        [TearDown]
        public void CleanUp()
        {
            devices = null;
            dev1 = null;
            dev2 = null;
            dev3 = null;
        }
    }
}