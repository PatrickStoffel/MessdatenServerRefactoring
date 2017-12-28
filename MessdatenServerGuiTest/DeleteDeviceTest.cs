using NUnit.Framework;
using OpenQA.Selenium;

namespace MessdatenServerGuiTest
{
    public class DeleteDeviceTest<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        [Test]
        public void DeleteDevice_WithExistingChoosen_RemoveDeviceFromConfig()
        {

        }
    }
}
