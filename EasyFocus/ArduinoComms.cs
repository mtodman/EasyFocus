using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ASCOM.EasyFocus
{
    public class ArduinoComms : System.IO.Ports.SerialPort
    {
        #region Manager Variables
        private string _portName = string.Empty;
        private SerialPort _serialPort = new SerialPort();
        const byte WRITE_POSITION = 197; // Advises the Arduino that it's about to receive a position update

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="name">Desired PortName</param>
        public ArduinoComms(string name)
        {
            _portName = name;
            //now add an event handler
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        public ArduinoComms()
        {
        }


        #endregion


        public int TestConnection(byte cmd)
        {
            _serialPort.PortName = PortName;
            _serialPort.ReadTimeout = 2000;
            _serialPort.WriteTimeout = 2000;
            //_serialPort.DtrEnable = true; //Added to cater for Arduino Leonardo
            _serialPort.Open();
            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();
            int response;
            byte[] data = new byte[1];
            data[0] = cmd;
            _serialPort.Write(data, 0, data.Length);
            //wait for reply
            try
            {
                response = _serialPort.ReadByte();
            }
            catch (System.TimeoutException ex)
            {
                //throw new TimeoutException(ex.Message);
                _serialPort.Close();
                return 99;
            }
            _serialPort.Close();
            return response;
        }


        public int GetData(byte cmd)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = PortName;
                _serialPort.ReadTimeout = 2000;
                _serialPort.WriteTimeout = 2000;
                _serialPort.Open();
            }
            int response;
            byte[] data = new byte[1];
            data[0] = cmd;
            int b1, b2;
            _serialPort.Write(data, 0, data.Length);
            //wait for reply
            try
            {
                b1 = _serialPort.ReadByte();
                b2 = _serialPort.ReadByte();
                response = ((int)b1) * 256 + b2;
            }
            catch (System.TimeoutException ex)
            {
                throw new TimeoutException(ex.Message);
            }
            _serialPort.Close();
            return response;
        }


        public bool SetCurPos(int curpos)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = PortName;
                _serialPort.ReadTimeout = 2000;
                _serialPort.WriteTimeout = 2000;
                _serialPort.Open();
            }
            bool response;
            int ack;
            byte[] cmdarray = new byte[3];
            cmdarray[0] = WRITE_POSITION;
            cmdarray[1] = IntToByte_upper(curpos);
            cmdarray[2] = IntToByte_lower(curpos);
            _serialPort.Write(cmdarray, 0, cmdarray.Length);
            //wait for reply
            try
            {
                ack = _serialPort.ReadByte();
                if (ack == 33)
                {
                    response = true;
                }
                else
                {
                    response = false;
                }
            }
            catch (System.TimeoutException ex)
            {
                return false;
                throw new TimeoutException(ex.Message);
            }
            _serialPort.Close();
            return response;
        }


        public bool GotoPos(byte cmd, int Steps, int stepdelay)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = PortName;
                _serialPort.ReadTimeout = 2000;
                _serialPort.WriteTimeout = 2000;
                _serialPort.Open();
            }
            bool response = true;
            int ack;
            byte[] cmdarray = new byte[1];
            cmdarray[0] = cmd;
            for (int index = 0; index < Steps; index++)
            {
                _serialPort.Write(cmdarray, 0, cmdarray.Length);
                //wait for reply
                try
                {
                    ack = _serialPort.ReadByte();
                    if (ack == 33)
                    {
                        response = true;
                    }
                    else
                    {
                        response = false;
                    }
                }
                catch (System.TimeoutException ex)
                {
                    return false;
                    throw new TimeoutException(ex.Message);
                }
                System.Threading.Thread.Sleep(stepdelay);
            }
            _serialPort.Close();
            return response;
        }

        public byte IntToByte_upper(int input)
        {
            return (byte)(input / 256);
        }

        public byte IntToByte_lower(int input)
        {
            return (byte)(input % 256);
        }
    }
}
