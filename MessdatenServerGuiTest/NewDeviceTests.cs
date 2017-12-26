using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Net;

namespace MessdatenServerGuiTest
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class NewDeviceTests<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string URL = "http://localhost:58296/View/index.html";
        private const string NEW_VALID_ID = "device66";
        private const string EXISTING_ID = "device1";
        private const string TEST_CONFIGURATION = "set";
        private const string ORIGINAL_CONFIGURATION = "reset";
        private IWebDriver driver = null;

        [SetUp]
        public void Setup()
        {
            SetConfiguration(TEST_CONFIGURATION);
            InitDriver();
            LoadDeviceList();
            OpenNewDevicePage();
        }

        [Test]
        public void CreateNewDevice_WithValidId_SaveDeviceInConfig()
        {          
            FillNewDeviceFormWith(NEW_VALID_ID);
            SaveDevice();

            string storedId = GetNewDeviceIdFromDeviceList();
            Assert.AreEqual(NEW_VALID_ID, storedId);
        }

        [Test]
        public void CreateNewDevice_WithExistingDevice_ReturnsErrorMessage()
        {
            FillNewDeviceFormWith(EXISTING_ID);
            SaveDevice();

            string errorMessage = GetErrorMessage(By.Id("errornew"));
            StringAssert.Contains("Die Id device1 existiert bereits in der Konfiguration, die ID muss eindeutig sein!", errorMessage);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SetConfiguration(ORIGINAL_CONFIGURATION);
        }

        private void InitDriver()
        {
            driver = new TWebDriver();
        }

        private void LoadDeviceList()
        {
            driver.Navigate().GoToUrl(URL);
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[1]"));
        }

        private void WaitUntilElementDiplayed(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(by).Displayed);
        }

        private void OpenNewDevicePage()
        {
            driver.FindElement(By.Id("btnNewDev")).Click();
            WaitUntilElementDiplayed(By.Id("name"));
        }

        private void FillNewDeviceFormWith(string id)
        {
            driver.FindElement(By.Id("name")).SendKeys(id);
            driver.FindElement(By.Id("hostIp")).SendKeys("136.230.68.65");
            driver.FindElement(By.Id("dataSource")).SendKeys("COM12");
            driver.FindElement(By.Id("group")).SendKeys("B");
            driver.FindElement(By.Id("protocol")).SendKeys("com-2");
        }

        private string GetNewDeviceIdFromDeviceList()
        {
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[4]/td[1]"));
            return driver.FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[4]/td[1]")).Text;
        }

        private void SaveDevice()
        {
            driver.FindElement(By.Id("btn_confirm")).Click();         
        }

        private string GetErrorMessage(By by)
        {
            WaitUntilElementDiplayed(By.Id("errornew"));
            return driver.FindElement(by).Text;
        }

        public void SetConfiguration(String option)
        {
            WebRequest request = WebRequest.Create(
              "http://localhost:58296/messdatenServer/settest/" + option);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            response.Close();
        }
    }
}
