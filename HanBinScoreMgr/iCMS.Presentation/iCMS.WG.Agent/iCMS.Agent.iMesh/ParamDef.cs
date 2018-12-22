using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    interface IParamOper
    {
        /// <summary>
        /// 对象赋值函数，将右值对象赋予调用对象
        /// </summary>
        /// <param name="right"></param>
        void Assign(object right);
    }

    public class tMAC : IParamOper
    {
        public const int LEN = 8;
        public byte[] u8aData = new byte[LEN];

        public tMAC()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public tMAC(string macHexStr)
        {
            if (macHexStr == null)
                throw new Exception("parameter is null in constructor");

            int dicardedChar = 0;
            byte[] parseMac = GetBytes(macHexStr, out dicardedChar);

            if (parseMac.Length == LEN)
                Array.Copy(parseMac, 0, u8aData, 0, LEN);
            else
                throw new Exception("tMAC Constructer takes invalid parameter!");
        }

        public void Assign(object right)
        {
            tMAC val = right as tMAC;
            if (val == null)
                throw new Exception("object is not tMAC type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public bool isEqual(tMAC refer)
        {
            for (int i = 0; i < LEN; i++)
            {
                if (u8aData[i] != refer.u8aData[i])
                    return false;
            }

            return true;
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }

        /// <summary>
        /// 判定调用对象是否为“零值”
        /// </summary>
        /// <returns></returns>
        public bool IsNull
        {
            get
            {
                for (int i = 0; i < LEN; i++)
                {
                    if (u8aData[i] != 0x00)
                        return false;
                }

                return true;
            }
        }

        private bool IsHexDigit(char c)
        {
            if ('0' <= c && c <= '9')
                return true;

            if ('a' <= c && c <= 'f')
                return true;

            if ('A' <= c && c <= 'F')
                return true;

            return false;
        }

        private byte HexToByte(string hex)
        {
            byte tt = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return tt;
        }

        private byte[] GetBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;

            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }

            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];

            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }

            return bytes;
        }
    }

    public class tSECKEY : IParamOper
    {
        public const int LEN = 16;
        public byte[] u8aData = new byte[LEN];

        public tSECKEY()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tSECKEY val = right as tSECKEY;
            if (val == null)
                throw new Exception("object is not tSECKEY type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }

    public class tSEQDEF
    {
        public byte u8PkLen = 0;        // Length of packet (2-125 bytes) 
        public ushort u16Delay = 0;     // Delay after packet transmission, microseconds
    }

    public class tREDIOTESTX
    {
        public enRadioTestType type = enRadioTestType.Packet;
        public ushort chanMask = 0;
        public ushort repeatCnt = 0;
        public sbyte txPower = 0;
        public byte seqSize = 0;
        public tSEQDEF[] sequenceDef;
        public byte stationId = 0;
    }

    public class tUTCTIME
    {
        public UInt32 u32Seconds = 0;      // number of seconds since midnight of January 1, 1970.
        public UInt32 u32Microseconds = 0; // microseconds since the beginning of the current second.
    }

    public class tUTCTIMEL
    {
        public UInt64 u64Seconds = 0;      // number of seconds since midnight of January 1, 1970.
        public UInt32 u32Microseconds = 0; // microseconds since the beginning of the current second.
    }

    public class tASN : IParamOper
    {
        public const int LEN = 5;
        public byte[] u8aData = new byte[LEN];

        public tASN()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tASN val = right as tASN;
            if (val == null)
                throw new Exception("object is not tASN type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }
    }

    public class tLICENSE : IParamOper
    {
        public const int LEN = 13;
        public byte[] u8aData = new byte[LEN];

        public tLICENSE()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tLICENSE val = right as tLICENSE;
            if (val == null)
                throw new Exception("object is not tLICENSE type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }

    public class tUSERPW : IParamOper
    {
        public const int LEN = 16;
        public byte[] u8aData = new byte[LEN];

        public tUSERPW()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tUSERPW val = right as tUSERPW;
            if (val == null)
                throw new Exception("object is not tUSERPW type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }

    public class tIPV6ADDR : IParamOper
    {
        public const int LEN = 16;
        public byte[] u8aData = new byte[LEN];

        public tIPV6ADDR()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tIPV6ADDR val = right as tIPV6ADDR;
            if (val == null)
                throw new Exception("object is not tIPV6ADDR type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }

    public class tIPV6MASK : IParamOper
    {
        public const int LEN = 16;
        public byte[] u8aData = new byte[LEN];

        public tIPV6MASK()
        {
            Array.Clear(u8aData, 0, LEN);
        }

        public void Assign(object right)
        {
            tIPV6MASK val = right as tIPV6MASK;
            if (val == null)
                throw new Exception("object is not tIPV6MASK type!");
            else
                Array.Copy(val.u8aData, 0, this.u8aData, 0, LEN);
        }

        public string ToHexString()// 0xAE00CFAE00CF00CF => "AE00CFAE00CF00CF"
        {
            string hexString = string.Empty;

            if (u8aData != null && u8aData.Length != 0)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < u8aData.Length; i++)
                {
                    strB.Append(u8aData[i].ToString("X2"));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }

    /// <summary>
    /// 订阅命令参数
    /// </summary>
    public class tSUBS
    {
        public UInt32 u32Filter = 0;
        public UInt32 u32UnackFilter = 0;
    }

    public class tNETWORKCONFIG
    {
        public bool bInit = false;
        public UInt16 u16NetworkId = 0;
        public SByte s8ApTxPower = 0;
        public enFrameProfile frmProfile = enFrameProfile.Profile_01;
        public UInt16 u16MaxMotes = 0;
        public UInt16 u16BaseBandwidth = 0;
        public byte u8DownFrameMultVal = 0;
        public byte u8NumParents = 0;
        public enCcaMode ccaMode = enCcaMode.Off;
        public UInt16 u16ChannelList = 0;
        public bool bAutoStartNetwork = false;
        public byte u8LocMode = 0;
        public enBBMode bbMode = enBBMode.Off;
        public byte u8BBSize = 0;
        public bool isRadioTest = false;
        public UInt16 u16BwMult = 0;
        public byte u8OneChannel = 0;
    }

    public class tEXMOTEJOINKEY : IParamOper
    {
        public tMAC mac = new tMAC();
        public tSECKEY key = new tSECKEY();

        public void Assign(object right)
        {
            tEXMOTEJOINKEY val = right as tEXMOTEJOINKEY;
            if (val == null)
                throw new Exception("object is not tEXMOTEJOINKEY type!");
            else
            {
                this.mac.Assign(val.mac);
                this.key.Assign(val.key);
            }
        }
    }

    public class tRADIOTESTTX : IParamOper
    {
        public const int MAX_SEQ_DEF_NUM = 10;

        // Type of transmission test
        public enRadioTestType type = enRadioTestType.Packet;
        // Mask of channels (0–15) enabled for the test, for CM and cw, only one channel should be enabled
        public ushort chanMask = 0;
        // Number of times to repeat the packet sequence
        public ushort repeatCnt = 0;
        // Transmit power, in dB. Valid values are 0 and 8
        public sbyte txPower = 0;
        // Number of packets in each sequence
        public byte seqSize = 0;
        // sequenceDef
        public tSEQDEF[] sequenceDef = new tSEQDEF[MAX_SEQ_DEF_NUM];
        // Unique (1-255) identifier included in packets that identifies the sender
        public byte stationId;

        public void Assign(object right)
        {
            tRADIOTESTTX val = right as tRADIOTESTTX;
            if (val == null)
                throw new Exception("object is not tRADIOTESTTX type!");
            else
            {
                type = val.type;
                chanMask = val.chanMask;
                repeatCnt = val.repeatCnt;
                txPower = val.txPower;
                seqSize = val.seqSize;

                for (int i = 0; i < MAX_SEQ_DEF_NUM; i++)
                {
                    sequenceDef[i].u8PkLen = val.sequenceDef[i].u8PkLen;
                    sequenceDef[i].u16Delay = val.sequenceDef[i].u16Delay;
                }

                stationId = val.stationId;
            }
        }
    }

    public class tRADIOTESTRX : IParamOper
    {
        // Mask of RF channel to use for the test
        public ushort chanMask = 0;
        // Duration of test (in seconds)
        public ushort duration = 0;
        // Unique (0-255) station ID of this device
        public byte stationId = 0;

        public void Assign(object right)
        {
            tRADIOTESTRX val = right as tRADIOTESTRX;
            if (val == null)
                throw new Exception("object is not tRADIOTESTRX type!");
            else
            {
                chanMask = val.chanMask;
                duration = val.duration;
                stationId = val.stationId;
            }
        }
    }

    public class tSETACLENTRY : IParamOper
    {
        public tMAC mac = new tMAC();
        public tSECKEY key = new tSECKEY();

        public void Assign(object right)
        {
            tSETACLENTRY val = right as tSETACLENTRY;
            if (val == null)
                throw new Exception("object is not tSETACLENTRY type!");
            else
            {
                this.mac.Assign(val.mac);
                this.key.Assign(val.key);
            }
        }
    }

    public class tSENDDATA : IParamOper
    {
        public tMAC mac = new tMAC();
        public enPktPriority priority = enPktPriority.Medium;
        public UInt16 u16SrcPort;
        public UInt16 u16DstPort;
        public byte u8Options;
        public byte[] u8aData = null;

        public void Assign(object right)
        {
            tSENDDATA val = right as tSENDDATA;
            if (val == null)
                throw new Exception("object is not tSENDDATA type!");
            else
            {
                this.mac.Assign(val.mac);
                this.priority = val.priority;
                this.u16SrcPort = val.u16SrcPort;
                this.u16DstPort = val.u16DstPort;
                this.u8Options = val.u8Options;

                if (val.u8aData == null || val.u8aData.Length <= 0)
                    return;
                else
                {
                    this.u8aData = new byte[val.u8aData.Length];
                    Array.Copy(val.u8aData, this.u8aData, val.u8aData.Length);
                }
            }
        }
    }

    public class tSENDIPDATA : IParamOper
    {
        public tMAC mac = new tMAC();
        public enPktPriority priority = enPktPriority.Medium;
        public byte u8Options = 0;
        public byte u8EncryptedOffset = 0;
        public byte[] u8aData = null;


        public void Assign(object right)
        {
            tSENDIPDATA val = right as tSENDIPDATA;
            if (val == null)
                throw new Exception("object is not tSENDIPDATA type!");
            else
            {
                this.mac.Assign(val.mac);
                this.priority = val.priority;
                this.u8Options = val.u8Options;
                this.u8EncryptedOffset = val.u8EncryptedOffset;

                if (val.u8aData == null || val.u8aData.Length <= 0)
                    return;
                else
                {
                    this.u8aData = new byte[val.u8aData.Length];
                    Array.Copy(val.u8aData, this.u8aData, val.u8aData.Length);
                }
            }
        }
    }

    public class tGETMOTECONFIG : IParamOper
    {
        public tMAC mac = new tMAC();
        public bool next = false;

        public void Assign(object right)
        {
            tGETMOTECONFIG val = right as tGETMOTECONFIG;
            if (val == null)
                throw new Exception("object is not tGETMOTECONFIG type!");
            else
            {
                next = val.next;
                mac.Assign(val.mac);
            }
        }
    }

    public class tGETPATHINFO : IParamOper
    {
        public tMAC source = new tMAC();
        public tMAC dest = new tMAC();

        public void Assign(object right)
        {
            tGETPATHINFO val = right as tGETPATHINFO;
            if (val == null)
                throw new Exception("object is not tGETPATHINFO type!");
            else
            {
                source.Assign(val.source);
                dest.Assign(val.dest);
            }
        }
    }

    public class tGETNEXTPATHINFO : IParamOper
    {
        public tMAC mac = new tMAC();
        public enPathDirection filter = enPathDirection.None;
        public ushort pathId = 0;

        public void Assign(object right)
        {
            tGETNEXTPATHINFO val = right as tGETNEXTPATHINFO;
            if (val == null)
                throw new Exception("object is not tGETNEXTPATHINFO type!");
            else
            {
                mac.Assign(val.mac);
                filter = val.filter;
                pathId = val.pathId;
            }
        }
    }

    public class tSETTIME : IParamOper
    {
        public enSetimeTrigMode trig = enSetimeTrigMode.Immediate;
        public tUTCTIMEL time;

        public void Assign(object right)
        {
            tSETTIME val = right as tSETTIME;
            if (val == null)
                throw new Exception("object is not tSETTIME type!");
            else
            {
                trig = val.trig;
                time.u64Seconds = val.time.u64Seconds;
                time.u32Microseconds = val.time.u32Microseconds;
            }
        }
    }

    public class tSETCLIUSER : IParamOper
    {
        public enCLIUserRole role = enCLIUserRole.Viewer;
        public tUSERPW password = new tUSERPW();

        public void Assign(object right)
        {
            tSETCLIUSER val = right as tSETCLIUSER;
            if (val == null)
                throw new Exception("object is not tSETCLIUSER type!");
            else
            {
                role = val.role;
                password.Assign(val.password);
            }
        }
    }

    public class tSETIPCONFIG : IParamOper
    {
        public tIPV6ADDR ipv6Address = new tIPV6ADDR();
        public tIPV6MASK mask = new tIPV6MASK();

        public void Assign(object right)
        {
            tSETIPCONFIG val = right as tSETIPCONFIG;
            if (val == null)
                throw new Exception("object is not tSETIPCONFIG type!");
            else
            {
                ipv6Address.Assign(val.ipv6Address);
                mask.Assign(val.mask);
            }
        }
    }

    public class tGETMOTELINKS : IParamOper
    {
        public tMAC mac = new tMAC();
        public ushort idx = 0;

        public void Assign(object right)
        {
            tGETMOTELINKS val = right as tGETMOTELINKS;
            if (val == null)
                throw new Exception("object is not tGETMOTELINKS type!");
            else
            {
                mac.Assign(val.mac);
                idx = val.idx;
            }
        }
    }

    // neighborHRData structure
    public class tNeighborHRData
    {
        private const byte NEIGHBOR_ID_OFFSET = 0;
        private const byte NEIGHBOR_FLAG_OFFSET = NEIGHBOR_ID_OFFSET + sizeof(UInt16);
        private const byte RSST_OFFSET = NEIGHBOR_FLAG_OFFSET + 1;
        private const byte NUMTXPKTS_OFFSET = RSST_OFFSET + 1;
        private const byte NUMTXFAIL_OFFSET = NUMTXPKTS_OFFSET + 2;
        private const byte NUMRXPKTS_OFFSET = NUMTXFAIL_OFFSET + 2;

        public UInt16 u16NeighborId = 0;    // Neighbor Mote ID
        public byte u8NeighborFlag = 0;
        public sbyte s8Rssi = 0;            // RSSI of neighbor
        public UInt16 u16NumTxPackets = 0;  // Number of transmitted packets
        public UInt16 u16NumTxFailures = 0; // Number of failed transmission
        public UInt16 u16NumRxPackets = 0;  // Number of received packets

        public const int Len = 10;

        public void Unserialize(byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("tNeighborHRData.Unserialize take invalid parameter");
            if (stream.Length < (offset + Len))
                throw new Exception("tNeighborHRData.Unserialize take invalid Index");

            u16NeighborId = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NEIGHBOR_ID_OFFSET);
            u8NeighborFlag = stream[offset + NEIGHBOR_FLAG_OFFSET];
            s8Rssi = (sbyte)stream[offset + RSST_OFFSET];
            u16NumTxPackets = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMTXPKTS_OFFSET);
            u16NumTxFailures = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMTXFAIL_OFFSET);
            u16NumRxPackets = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMRXPKTS_OFFSET);
        }
    }

    // discoveredNeighborData structure
    public class tDiscoveredNeighborHRData
    {
        private const byte NEIGHBOR_ID_OFFSET = 0;
        private const byte RSST_OFFSET = NEIGHBOR_ID_OFFSET + sizeof(UInt16);
        private const byte NUMRX_OFFSET = RSST_OFFSET + 1;

        public UInt16 u16NeighborId = 0;    // Neighbor Mote ID
        public sbyte s8Rssi = 0;            // RSSI of neighbor
        public UInt16 u16NumRx = 0;         // Number of times a neighbor was heard

        public const int Len = 4;

        public void Unserialize(byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("tDiscoveredNeighborHRData.Unserialize take invalid parameter");
            if (stream.Length < (offset + Len))
                throw new Exception("tDiscoveredNeighborHRData.Unserialize take invalid Index");

            u16NeighborId = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NEIGHBOR_ID_OFFSET);
            s8Rssi = (sbyte)stream[offset + RSST_OFFSET];
            u16NumRx = stream[offset + NUMRX_OFFSET];
        }
    }
}
