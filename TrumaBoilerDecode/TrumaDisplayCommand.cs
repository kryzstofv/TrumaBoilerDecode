using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumaBoilerDecode
{
    public class TrumaDisplayCommand
    {
        private LINmessage message;
        public enum PowerMode { Gas, Mix1, Mix2, Elec1, Elec2 };

        public enum WaterMode { Off, Eco, Hot, Boost };

        public int ventPower;              ///displayed blown air speed (when heating off), off = 0, through to 10
        public PowerMode powerMode;        ///displayed heat source
        public WaterMode waterMode;        ///displayed water heating power
        public int airTempSetPointDegs;    ///displayed air temperature set point in degs C

        private PowerMode GetPowerMode(LINmessage message)
        {
            PowerMode powerMode = PowerMode.Gas;
            if (message.Payload[3] == 0x0)
            {
                //Elec
                if (message.Payload[4] == 0x12)
                {
                    powerMode = PowerMode.Elec2;
                }
                else if (message.Payload[4] == 0x09)
                {
                    powerMode = PowerMode.Elec1;
                }
            }
            else if (message.Payload[3] == 0xFA)
            {
                if (message.Payload[4] == 0x12)
                {
                    powerMode = PowerMode.Mix2;
                }
                else if (message.Payload[4] == 0x09)
                {
                    powerMode = PowerMode.Mix1;
                }
                else if (message.Payload[4] == 0)
                {
                    powerMode = PowerMode.Gas;
                }
            }
            return powerMode;
        }

        private int GetVentMode(LINmessage message)
        {
            return (message.Payload[5] & 0xF) >> 4;
        }

        private WaterMode GetWaterMode(LINmessage message)
        {
            WaterMode mode = WaterMode.Off;
            switch (message.Payload[1])
            {
                case 0xAA:
                    mode = WaterMode.Off;
                    break;
                case 0xAB:
                    mode = WaterMode.Eco;
                    break;
                case 0x0A:
                    mode = WaterMode.Hot;
                    break;
                case 0x2A:
                    mode = WaterMode.Boost;
                    break;
            }
            return mode;
        }

        private int GetAirTempSetPoint(LINmessage message)
        {
            int data = message.Payload[0];
            return ((data - 4) / 10) + 9;
        }

        public TrumaDisplayCommand(byte[] data)
        {
            message = new LINmessage(data);
            ventPower = GetVentMode(message);
            powerMode = GetPowerMode(message);
            waterMode = GetWaterMode(message);
            airTempSetPointDegs = GetAirTempSetPoint(message);
        }

        public TrumaDisplayCommand(LINmessage _message)
        {
            message = _message;
            ventPower = GetVentMode(message);
            powerMode = GetPowerMode(message);
            waterMode = GetWaterMode(message);
            airTempSetPointDegs = GetAirTempSetPoint(message);
        }

    }


#if false
        public const int commandLength = 12;
        public byte[] fullData;
        public byte[] commandData;
        public byte commandID;
        public byte checksum;
        public bool checksumValid;
        public TrumaCommand(byte[] data)
        {
            if(data.Length != commandLength)
            {
                throw new Exception("Command length incorrect");
            }
            if((data[0] != 0x0) || (data[1] != 0x55))
            {
                throw new Exception("Command header incorrect");
            }

            data.CopyTo(fullData, 0);
            fullData = new byte[12];
            commandID = data[2];
            checksum = data[11];
            commandData = new byte[8];
            Array.Copy(data, 3, commandData, 0, 8);

            checksumValid = TestChecksum();
        }

        private bool TestChecksum()
        {
            int remainder = 0;
            int sum = 0;
            switch (commandID)
            {
                case 0x3C:
                    remainder = 60;
                    break;
                case 0x7D:
                    remainder = 140;
                    break;
                default:
                    remainder = 0;
                    break;
            }

            sum += commandID;
            for(int i=0;i<8;i++)
            {
                sum += commandData[i];
            }
            sum += checksum;

            return ((sum % 255) == remainder);
        }
    }
#endif
}
