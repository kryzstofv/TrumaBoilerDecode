using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumaBoilerDecode
{
    class LINReader
    {
        const byte BreakByte = 0x0;
        const byte SyncByte = 0x55;

        public List<LINmessage> Parse(CircularBuffer<byte> buffer)
        {
            List<LINmessage> foundMessages = new List<LINmessage>();

            //process circular buffer
            for (int i = 0; i < buffer.Count - 12; i++)
            {
                //look for the 'break'
                while (buffer.Get() != BreakByte)
                {
                    //skip bytes
                }
                //look for the 'sync'
                if (buffer.Get() == SyncByte)
                {
                    //message start found
                    byte[] command = new byte[12];

                    command[0] = 0x0;
                    command[1] = 0x55;
                    for (int j = 2; j < 12; j++)
                    {
                        command[j] = buffer.Get();
                    }

                    //populate command
                    foundMessages.Add(new LINmessage(command));
                }
                else
                {
                    //skip bytes
                }
            }

            return foundMessages;
        }
    }
}
