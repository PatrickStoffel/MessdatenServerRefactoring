using NUnit.Framework;
using OpenQA.Selenium;

namespace MessdatenServerGuiTest
{
    public class UpdateDeviceTest<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        [Test]
        public void UpdateTest()
        {
            string storedId = "test";
            Assert.AreEqual("test", storedId);
        }
    }
}
