using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumaBoilerDecode
{
    public class LINmessage
    {
        public const int Length = 12;
        public byte Break;
        public byte Sync;
        public byte ID;
        public byte[] Payload;
        public byte[] RawData;
        public byte Checksum;
        public eChecksumType ChecksumType;
            
        public bool SyncValid = false;
        public bool ChecksumValid = false;

        public enum eChecksumType { Classic, Enhanced, Failed };
        public LINmessage(byte[] data)
        {
            if(data.Length != Length)
            {
                throw new Exception("Wrong length");
            }

            Payload = new byte[8];
            RawData = new byte[12];
            Break = data[0];
            Sync = data[1];
            if(Sync == 0x55)
            {
                SyncValid = true;
            }

            ID = data[2];
            data.CopyTo(RawData, 0);
            Array.Copy(data, 3, Payload, 0, 8);
            Checksum = data[11];
            ChecksumValid = true;

            if (TestClassicChecksum(RawData, Checksum))
            {
                ChecksumType = eChecksumType.Classic;
            }
            else if (TestEnhancedChecksum(RawData, Checksum))
            {
                ChecksumType = eChecksumType.Enhanced;
            }
            else
            {
                ChecksumType = eChecksumType.Failed;
                ChecksumValid = false;
            }
        }

        private bool TestClassicChecksum(byte[] data, byte checksum)
        {
            return TestChecksum(data, 3, 8, checksum);
        }

        private bool TestEnhancedChecksum(byte[] data, byte checksum)
        {
            return TestChecksum(data, 2, 9, checksum);
        }

        private bool TestChecksum(byte[] data, int start, int length, byte checksum)
        {
            if(start + length > data.Length)
            {
                throw new Exception("Bad test range specified");
            }
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += data[i+start];
                if(sum >= 256)
                {
                    sum -= 255;
                }
            }
            //invert the result to provide the checksum
            sum ^= 0xFF;

            if(sum == checksum)
            {
                return true;
            }
            return false;
        }
    }



}
