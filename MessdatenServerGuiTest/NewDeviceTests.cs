using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MessdatenServerGuiTest
{
    [TestFixture]
    public class NewDeviceTests
    {
        private const string NEW_VALID_ID = "device66";
        private IWebDriver driver = null;

        [SetUp]
        public void Setup()
        {
            InitDriver();
            LoadDeviceList();         
        }

        [Test]
        public void CreateNewDevice_WithValidId_SaveDeviceInConfig()
        {
            OpenNewDevicePage();
            FillNewDeviceForm();
            SaveNewDevice();

            string id = GetNewDeviceIdFromDeviceList();
            Assert.AreEqual(NEW_VALID_ID, id);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }

        private void InitDriver()
        {
            driver = new ChromeDriver();
        }

        private void LoadDeviceList()
        {
            driver.Navigate().GoToUrl("http://localhost:58296/View/index.html");
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[1]"));
        }

        private void WaitUntilElementDiplayed(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(by).Displayed);
        }

        private void OpenNewDevicePage()
        {
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/button")).Click();
            WaitUntilElementDiplayed(By.Id("name"));
        }

        private void FillNewDeviceForm()
        {
            driver.FindElement(By.Id("name")).SendKeys(NEW_VALID_ID);
            driver.FindElement(By.Id("hostIp")).SendKeys("136.230.68.65");
            driver.FindElement(By.Id("dataSource")).SendKeys("COM12");
            driver.FindElement(By.Id("group")).SendKeys("B");
            driver.FindElement(By.Id("protocol")).SendKeys("com-2");
        }

        private string GetNewDeviceIdFromDeviceList()
        {
            return driver.FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[4]/td[1]")).Text;
        }

        private void SaveNewDevice()
        {
            driver.FindElement(By.XPath("//*[@id=\"btn_confirm\"]")).Click();
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[1]"));
        }
    }
}
