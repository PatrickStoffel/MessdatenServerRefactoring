using NUnit.Framework;
using OpenQA.Selenium;

namespace MessdatenServerGuiTest
{
    public class UpdateDeviceTest<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string VALID_PROTOCOL = "com-3";

        [Test]
        public void UpdateDevice_WithValidValue_SaveUpdatedDeviceInConfig()
        {
            OpenUpdateView();
            ModifyProtocolWith(VALID_PROTOCOL);
            SaveDevice();

            string updatedProtocol = GetUpdatedProtocolFromList();
            Assert.AreEqual(VALID_PROTOCOL, updatedProtocol);
        }

        private void OpenUpdateView()
        {           
            GetDriver().FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[6]/button")).Click();
            WaitUntilElementDiplayed(By.Id("name"));
        }

        private void ModifyProtocolWith(string value)
        {
            GetDriver().FindElement(By.Id("protocol")).Clear();
            GetDriver().FindElement(By.Id("protocol")).SendKeys(value);
        }

        private string GetUpdatedProtocolFromList()
        {         
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[5]"));
            return GetDriver().FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[5]")).Text;
        }
    }
}
