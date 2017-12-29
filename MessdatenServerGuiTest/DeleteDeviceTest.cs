using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Threading;

namespace MessdatenServerGuiTest
{
    public class DeleteDeviceTest<TWebDriver> : DeviceTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string DEVICE_ID = "device3";

        [Test]
        public void DeleteDevice_WithExistingDevice_RemoveDeviceFromConfig()
        {
            OpenViewToDelete();
            Thread.Sleep(500);
            ConfirmChanges();

            bool existDevice = ExistDeviceIdInConfig(DEVICE_ID);
            Assert.False(existDevice);
        }

        private bool ExistDeviceIdInConfig(string wantedId)
        {
            WaitUntilElementDiplayed(By.XPath("//*[@id=\"list\"]"));
            IWebElement deviceTable = GetDriver().FindElement(By.XPath("//*[@id=\"list\"]"));
            ReadOnlyCollection<IWebElement> allRows = deviceTable.FindElements(By.TagName("tr"));

            for (int row = 1; row < allRows.Count; row++)
            {
                string deviceId = allRows[row].FindElement(By.ClassName("deviceName")).Text;
                if (deviceId.Equals(wantedId))
                {
                    return true;
                }
            }
            return false;
        }

        private void OpenViewToDelete()
        {
            GetDriver().FindElement(By.XPath("//*[@id=\"deviceTable\"]/tr[3]/td[7]/button")).Click();
            WaitUntilElementDiplayed(By.Id("protocol"));
        }
    }
}
