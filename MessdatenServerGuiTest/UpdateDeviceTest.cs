using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace MessdatenServerGuiTest
{
    public class UpdateDeviceTest<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string VALID_PROTOCOL = "com-3";
        private const string EMPTY_STRING = "";

        [Test]
        public void UpdateDevice_WithValidValue_SaveUpdatedDeviceInConfig()
        {
            UpdateAndSaveDeviceWith(VALID_PROTOCOL);

            string updatedProtocol = GetUpdatedProtocolFromList();
            Assert.AreEqual(VALID_PROTOCOL, updatedProtocol);
        }

        [Test]
        public void UpdateDevice_WithMissingValue_ReturnsErrorMessage()
        {
            UpdateAndSaveDeviceWith(EMPTY_STRING);

            string errorMessage = GetErrorMessage(By.Id("errornew"));
            StringAssert.Contains("Eingabefelder für Device nicht vollständig! Benötigte Felder: Id, HostIp, DataSource und Protocol", errorMessage);
        }

        private void UpdateAndSaveDeviceWith(string value)
        {
            OpenUpdateView();
            ModifyProtocolWith(value);
            ConfirmChanges();
        }

        private void OpenUpdateView()
        {                                    
            GetDriver().FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[6]/button")).Click();
            WaitUntilElementDiplayed(By.Id("protocol"));
        }

        private void ModifyProtocolWith(string value)
        {
            Thread.Sleep(1000);
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
