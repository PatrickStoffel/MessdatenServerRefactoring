using NUnit.Framework;
using OpenQA.Selenium;


namespace MessdatenServerGuiTest
{
    public class NewDeviceTests<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string NEW_VALID_ID = "device66";
        private const string EXISTING_ID = "device1";
  
        [Test]
        public void CreateNewDevice_WithValidId_SaveDeviceInConfig()
        {
            OpenNewDevicePage();
            FillNewDeviceFormWith(NEW_VALID_ID);
            SaveDevice();

            string storedId = GetNewDeviceIdFromDeviceList();
            Assert.AreEqual(NEW_VALID_ID, storedId);
        }

        [Test]
        public void CreateNewDevice_WithExistingDevice_ReturnsErrorMessage()
        {
            OpenNewDevicePage();
            FillNewDeviceFormWith(EXISTING_ID);
            SaveDevice();

            string errorMessage = GetErrorMessage(By.Id("errornew"));
            StringAssert.Contains("Die Id device1 existiert bereits in der Konfiguration, die ID muss eindeutig sein!", errorMessage);
        }

        private void OpenNewDevicePage()
        {
            GetDriver().FindElement(By.Id("btnNewDev")).Click();
            WaitUntilElementDiplayed(By.Id("name"));
        }

        private void FillNewDeviceFormWith(string id)
        {
            GetDriver().FindElement(By.Id("name")).SendKeys(id);
            GetDriver().FindElement(By.Id("hostIp")).SendKeys("136.230.68.65");
            GetDriver().FindElement(By.Id("dataSource")).SendKeys("COM12");
            GetDriver().FindElement(By.Id("group")).SendKeys("B");
            GetDriver().FindElement(By.Id("protocol")).SendKeys("com-2");
        }

        private string GetNewDeviceIdFromDeviceList()
        {
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[4]/td[1]"));
            return GetDriver().FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[4]/td[1]")).Text;
        }

        private void SaveDevice()
        {
            GetDriver().FindElement(By.Id("btn_confirm")).Click();
        }
    }
}
