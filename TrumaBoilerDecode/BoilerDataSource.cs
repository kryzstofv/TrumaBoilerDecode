using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.Timers;

namespace TrumaBoilerDecode
{
    class BoilerDataSource
    {
        SerialPort port;
        CircularBuffer<byte> readBuffer;
        Timer t;

        //LIN format reader
        LINReader LINReader;

        //subscriber to Truma Control Panel Setting Data, i.e. what has been selected on the panel
        public event EventHandler<DisplayDataEventArgs> NewDisplayData;


        public BoilerDataSource(string comPort)
        {
            readBuffer = new CircularBuffer<byte>(2 * 1024);

            LINReader = new LINReader();

            port = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);           
        }

        public void Open()
        {
            port.Open();

            t = new Timer(300);
            t.Elapsed += TimedFunction;
            t.AutoReset = true;
            t.Start();
        }

        public void Close()
        {
            port.Close();
            t.Stop();
        }

        /// <summary>
        /// Periodically pull bytes from the serial port and process.
        /// Empties the port buffer each call.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimedFunction(object sender, ElapsedEventArgs e)
        {
            int bytes = port.BytesToRead;
            if(bytes > readBuffer.Space)
            {
                bytes = readBuffer.Space;
            }

            //doesn't handle any overflow anywhere

            //bytewise port read into circular buffer
            for (int i = 0; i < bytes; i++)
            {
                int readByte = port.ReadByte();
                if(readByte < 0)
                {
                    //end of stream
                    break;
                }
                readBuffer.Put((byte)readByte);
            }

#if false
            //linear read then bytewise copy
            //read from port
            byte[] newData = new byte[bytes];
            port.Read(newData, 0, bytes);

            //put in circular buffer
            for (int i=0;i<bytes;i++)
            {
                readBuffer.Put(newData[i]);
            }
#endif
            //get all LIN messages from the buffer
            List<LINmessage> list = ParseReceivedData(readBuffer);

            //process the messages and publish
            ProcessMessages(list);
        }

        /// <summary>
        /// Parse the received byte buffer and pull out LIN messages
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public List<LINmessage> ParseReceivedData(CircularBuffer<byte> buffer)
        {
            //get all LIN messages from the buffer
            return LINReader.Parse(buffer);
        }

        /// <summary>
        /// Loop through all LIN messages provided and decode into Truma format commands.
        /// Publish to any subscribed event handlers.
        /// </summary>
        /// <param name="messages"></param>
        public void ProcessMessages(List<LINmessage> messages)
        {
            foreach (LINmessage m in messages)
            {
                //only process valid data
                if(m.ChecksumValid == false)
                {
                    continue;
                }

                //decode the message and publish to any subscribed event handler
                switch (m.ID)
                {
                    case 0x20:
                        TrumaDisplayCommand cmd = new TrumaDisplayCommand(m);

                        DisplayDataEventArgs publishData = new DisplayDataEventArgs(cmd);
                        NewDisplayData?.Invoke(this, publishData);

                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Data from the Truma Control Panel, published to a subscribed handler
    /// </summary>
    public class DisplayDataEventArgs
    {
        public readonly TrumaDisplayCommand data;
        public DisplayDataEventArgs(TrumaDisplayCommand cmd)
        {
            data = cmd;
        }
    }
}
