using System;
using System.IO.Ports;
using MessdatenServer.Models;
using System.Text.RegularExpressions;
using MessdatenServer.services;

namespace MessdatenServer.Adapter
{
    public class SylcvacComAccess
    {
        private bool dataReceived = false;
        private String actualValue = null;
        private SerialPort comPort = null;

        public SylcvacComAccess()
        {
            comPort = new SerialPort();   
        }
        public String GetActualValueFromComInterface(Device deviceToRead)
        {
            dataReceived = false;
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            try
            {
                OpenComPort(deviceToRead);

                comPort.Write((char)32 + "?" + (char)Properties.Settings.Default.AsciCarriageReturn);

                WaitForDataReceivedComPort(deviceToRead);
            }
            catch (Exception ex)
            {
                throw new ReadWriteException("Fehler beim Zugriff auf " + deviceToRead.DataSource + "!", ex);
            }
            finally
            {
                comPort.Close();
            }            
            return actualValue;
        }

        public String SetActualValueToZero(Device deviceToRead)
        {
            dataReceived = false;
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_HandShakeReceived);
            try
            {
                OpenComPort(deviceToRead);

                comPort.Write("SET" + (char)Properties.Settings.Default.AsciCarriageReturn);

                WaitForDataReceivedComPort(deviceToRead);
            }
            catch (Exception ex)
            {
                throw new ReadWriteException("Fehler beim Zugriff auf " + deviceToRead.DataSource + "!", ex);
            }
            finally
            {
                comPort.Close();
            }
            return actualValue;
        }

        private void WaitForDataReceivedComPort(Device deviceToRead)
        {
            int startTime = DateTime.Now.Second;
            while (!dataReceived)
            {
                if (DateTime.Now.Second - startTime < Properties.Settings.Default.TimeOutReceiveData)
                {
                    System.Threading.Thread.Sleep(100);
                }
                else
                {
                    throw new ReadWriteException("Timeout beim Lesen von " + deviceToRead.DataSource + "!");
                }
            }
        }

        private void OpenComPort(Device deviceToRead)
        {
            if (comPort.IsOpen == true)
            {
                comPort.Close();
            }
            comPort.BaudRate = Properties.Settings.Default.BaudRateSylvacGauge;
            comPort.DataBits = Properties.Settings.Default.DataBitsSylvacGauge;
            comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Properties.Settings.Default.StopBitsSylvacGauge);
            comPort.Parity = (Parity)Enum.Parse(typeof(Parity), Properties.Settings.Default.ParitySylvacGauge);
            comPort.ReadTimeout = 2000;
            comPort.PortName = deviceToRead.DataSource;
            comPort.Open();
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String rawlValue = comPort.ReadExisting();
            double doubleValue  = ParseToDoubleFormat(rawlValue);
            actualValue = doubleValue.ToString();
            dataReceived = true;
        }

        private void comPort_HandShakeReceived(object sender, SerialDataReceivedEventArgs e)
        {
            actualValue = comPort.ReadExisting().Replace("\r", "");
            dataReceived = true;
        }

        private double ParseToDoubleFormat(String rawValue)
        {
            Match match = Regex.Match(rawValue, @"-?\d+(?:\.\d+)?");
            return Convert.ToDouble(match.Value);
        }
    }
}