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
    public class DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string URL = "http://localhost:58296/View/index.html";
        private const string TEST_CONFIGURATION = "set";
        private const string ORIGINAL_CONFIGURATION = "reset";
        private IWebDriver driver = null;

        public IWebDriver Driver { get => driver; set => driver = value; }

        [SetUp]
        public void Setup()
        {
            SetConfiguration(TEST_CONFIGURATION);
            Driver = CreateDriver();
            LoadDeviceList();
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SetConfiguration(ORIGINAL_CONFIGURATION);
        }

        private TWebDriver CreateDriver()
        {
            return  new TWebDriver();
        }

        public void WaitUntilElementDiplayed(By identificator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(identificator).Displayed);
        }

        private void LoadDeviceList()
        {
            Driver.Navigate().GoToUrl(URL);
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[1]"));
        }

        public void SetConfiguration(String option)
        {
            WebRequest request = WebRequest.Create(
              "http://localhost:58296/messdatenServer/settest/" + option);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            response.Close();
        }

        public string GetErrorMessage(By identificator)
        {
            WaitUntilElementDiplayed(identificator);
            return Driver.FindElement(identificator).Text;
        }
    }
}
