﻿using NUnit.Framework;
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
#if DEBUG
        private const string URL = "http://localhost:58296/View/index.html";
#else
        private const string URL = "http://messdatenserver.azurewebsites.net/View/index.html";
#endif
        private const string TEST_CONFIGURATION = "set";
        private const string ORIGINAL_CONFIGURATION = "reset";
        private IWebDriver driver = null;

        [SetUp]
        public void Setup()
        {
            SetConfiguration(TEST_CONFIGURATION);
            driver = CreateDriver();
            LoadDeviceList();
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

        public IWebDriver GetDriver()
        {
            return driver;
        }

        private TWebDriver CreateDriver()
        {
            return  new TWebDriver();
        }

        public void WaitUntilElementDiplayed(By identificator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(identificator));
        }

        public void WaitUntilElementClickable(By identificator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(identificator));
        }

        private void LoadDeviceList()
        {
            driver.Navigate().GoToUrl(URL);
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"deviceTable\"]/tr[1]/td[1]"));   
        }

        public void SetConfiguration(String option)
        {
            WebRequest request = WebRequest.Create(
#if DEBUG
           "http://localhost:58296/messdatenServer/settest/" + option);
#else
           "http://messdatenserver.azurewebsites.net/messdatenServer/settest/" + option);
#endif
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            response.Close();
        }

        public string GetErrorMessage(By identificator)
        {
            WaitUntilElementDiplayed(identificator);
            return driver.FindElement(identificator).Text;
        }

        public void ConfirmChanges()
        {
            WaitUntilElementClickable(By.Id("btn_confirm"));
            GetDriver().FindElement(By.Id("btn_confirm")).Click();
        }
    }
}
