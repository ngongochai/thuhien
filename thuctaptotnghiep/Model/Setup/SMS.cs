using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Model.Setup
{
    public class SMS
    {
        public string phonenumber{get;set;}
        public string Mess{get;set;}
        public AutoResetEvent receiveNow = new AutoResetEvent(false);
        public string ReadResponse(SerialPort port, int timeout)
        {
            string serialPortData = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string data = port.ReadExisting();
                        serialPortData += data;
                    }
                }
                while (!serialPortData.EndsWith("\r\nOK\r\n") && !serialPortData.EndsWith("\r\n> ") && !serialPortData.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return serialPortData;
        }
        public string ExecCommand(SerialPort port, string command, int responseTimeout)
        {
             
            try
            {

                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SerialPort port()
        {
            var _serialPort = new SerialPort("COM4", 460800);
            _serialPort.Open();
            return _serialPort;
        }
        public bool SendSms(string phonenumber,string mess)
        {
            try
            {

                var _serialPort = new SerialPort("COM4", 460800);
                Thread.Sleep(1000);

                _serialPort.Open();

                Thread.Sleep(1000);

                _serialPort.Write("AT+CMGF=1\r");

                Thread.Sleep(1000);

                _serialPort.Write("AT+CMGS=\"" + phonenumber + "\"\r\n");

                Thread.Sleep(1000);

                _serialPort.Write(mess + "\x1A");

                Thread.Sleep(1000);

                _serialPort.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string listsms()
        {
            SerialPort _serialPort = new SerialPort("COM7", 460800);

            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.XOnXOff;

            _serialPort.DtrEnable = true;
            _serialPort.RtsEnable = true;
            _serialPort.NewLine = Environment.NewLine;
            _serialPort.Open();

            _serialPort.Write("AT" + System.Environment.NewLine);
            Thread.Sleep(1000);

            _serialPort.WriteLine("AT+CMGF=1\r" + System.Environment.NewLine);
            Thread.Sleep(1000);
            _serialPort.WriteLine("AT+CMGL=\"ALL\"\r" + System.Environment.NewLine); 
            Thread.Sleep(3000);
            string input = _serialPort.ReadExisting();
            _serialPort.Close();
            // Read the messages
            return input;
        
        } 
    }
}
