using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    #region 各种命令的响应定义

    public class tEcho
    {
        public eRC RC = eRC.RC_OK;
    }

    public class tRcCbIdEcho : tEcho
    {
        public UInt32 u32CallbackId = 0;
    }

    // 重启命令响应信息
    public class tResetEcho : tEcho
    {
        public tMAC mac = new tMAC();
    }

    // 订阅命令响应信息
    public class tSubEcho : tEcho
    {
    }

    // 获取时间命令响应信息
    public class tGetTimeEcho : tEcho
    {
        public UInt32 u32Uptime = 0;                // Time (sec) that the response was generated (as uptime)
        public tUTCTIMEL utcTime = new tUTCTIMEL(); // UTC
        public tASN asn = new tASN();               // Absolute slot number (ASN)
        public UInt16 u16AsnOffset = 0;             // Offset inside ASN, in microseconds
    }

    // 配置网络命令响应信息
    public class tSetNetworkConfigEcho : tEcho
    {
    }

    // ClearStatistics命令响应信息
    public class tClearStatisticsEcho : tEcho
    {
    }

    // 更改Mote Join Key响应信息
    public class tExchangeMoteJoinKeyEcho : tRcCbIdEcho
    {
    }

    // 更改Mote Network Id响应信息
    public class tExchangeNetworkIdEcho : tRcCbIdEcho
    {
    }

    // RadiotestTx响应信息
    public class tRadiotestTxEcho : tEcho
    {
    }

    // RadiotestRx响应信息
    public class tRadiotestRxEcho : tEcho
    {
    }

    // 获取射频测试统计响应信息
    public class tGetRadiotestStatisticsEcho : tEcho
    {
        public UInt16 u16RxOk = 0;      // Number of packets received successfully
        public UInt16 u16RxFail = 0;    // Number of packets received with errors
    }

    // SetACLEntry响应信息
    public class tSetACLEntryEcho : tEcho
    {
    }

    // GetNextACLEntry响应信息
    public class tGetNextACLEntryEcho : tEcho
    {
        public tMAC mac = new tMAC();             // Mote MAC address
        public tSECKEY JoinKey = new tSECKEY();   // No join key reads are permitted, returns 0s
    }
    
    // DeleteACLEntry响应信息
    public class tDeleteACLEntryEcho : tEcho
    {
    }

    // PingMote响应信息getLog
    public class tPingMoteEcho : tRcCbIdEcho
    {
    }

    // GetLog响应信息
    public class tGetLogEcho : tEcho
    {
    }

    // SendData响应信息
    public class tSendDataEcho : tRcCbIdEcho
    {
    }

    // StartNetwork响应信息
    public class tStartNetworkEcho : tEcho
    {
    }

    // GetSystemInfo响应信息
    public class tGetSystemInfoEcho : tEcho
    {
        public tMAC mac = new tMAC();       // Mote MAC address
        public byte u8HwModel = 0;          // Hardware model
        public byte u8HwRev = 0;            // Hardware revision
        public byte u8SwMajor = 0;          // Software version, major
        public byte u8SwMinor = 0;          // Software version, minor
        public byte u8SwPatch = 0;          // Software version, patch
        public byte u8SwBuild = 0;          // Software version, build
    }

    // GetMoteConfig响应信息
    public class tGetMoteConfigEcho : tEcho
    {
        public tMAC mac = new tMAC();       // Mote MAC address
        public UInt16 u16MoteId = 0;        // Mote ID (used in health reports)
        public bool isAP = false;           // if Access mode 
        public byte u8State = 0;            // Mote state
        public byte u8Reserved = 0;         // Reserved, values should be ignored.
        public bool isRouting = false;      // Indicates whether this mote can be used as a non-leaf node in the network
    }

    // GetPathInfo响应信息
    public class tGetPathInfoEcho : tEcho
    {
        public tMAC Source = new tMAC();    // MAC address of source mote
        public tMAC Dest = new tMAC();      // MAC address of destination mote
        public enPathDirection Dir = enPathDirection.None;    // Path direction
        public byte u8NumLinks = 0;         // Number of links between motes for upstream frame
        public byte u8Quality = 0;          // An internal measurement of path quality based on a moving average of packets received over packets transmitted
        public byte u8RssiSrcDest = 0;      // Latest RSSI or 0 (if there is no data), for the path from source mote to destination mote
        public byte u8RssiDestSrc = 0;      // Latest RSSI or 0 (if there is no data), for the path from destination mote to source mote
    }

    // GetNextPathInfo响应信息
    public class tGetNextPathInfoEcho : tEcho
    {
        public UInt16 u16PathId;            // Path ID
        public tMAC Source = new tMAC();    // MAC address of source mote
        public tMAC Dest = new tMAC();      // MAC address of destination mote
        public enPathDirection Dir = enPathDirection.None;    // Path direction
        public byte u8NumLinks = 0;         // Number of links between motes for upstream frame
        public byte u8Quality = 0;          // An internal measurement of path quality based on a moving average of packets received over packets transmitted
        public byte u8RssiSrcDest = 0;      // Latest RSSI or 0 (if there is no data), for the path from source mote to destination mote
        public byte u8RssiDestSrc = 0;      // Latest RSSI or 0 (if there is no data), for the path from destination mote to source mote
    }

    // SetAdvertising响应信息
    public class tSetAdvertisingEcho : tRcCbIdEcho
    {
    }

    // SetDownstreamFrameMode响应信息
    public class tSetDownstreamFrameModeEcho : tRcCbIdEcho
    {
    }

    // GetManagerStatistics响应信息
    public class tGetManagerStatisticsEcho : tEcho
    {
        public UInt16 u16SerTxCnt = 0;      // Number of packets sent out on the serial port. This value may roll over if not cleared
        public UInt16 u16SerRxCnt = 0;      // Number of packets received on the serial port. This value may roll over if not cleared
        public UInt16 u16SerRxCRCErr = 0;   // Number of CRC errors
        public UInt16 u16SerRxOverruns = 0; // Number of overruns detected
        public UInt16 u16ApiEstabConn = 0;  // Number of established Serial API connections
        public UInt16 u16ApiDroppedConn = 0;// Number of established Serial API connections
        public UInt16 u16ApiTxOk = 0;       // Number of request packets sent on serial API for which ack-OK was received
        public UInt16 u16ApiTxErr = 0;      // Number of request packets sent on serial api for which acknowledgment error was received
        public UInt16 u16ApiTxFail = 0;     // Number of packets for which there was no acknowledgment
        public UInt16 u16ApiRxOk = 0;       // Number of request packets that were received and acknowledged
        public UInt16 u16ApiRxProtErr = 0;  // Number of packets that were received and dropped due to invalid packet format
    }

    // SetTime响应信息
    public class tSetTimeEcho : tEcho
    {
    }

    // GetLicense响应信息
    public class tGetLicenseEcho : tEcho
    {
        public tLICENSE License = new tLICENSE();   // Current software license key
    }

    // SetLicense响应信息
    public class tSetLicenseEcho : tEcho
    {
    }

    // SetCLIUser响应信息
    public class tSetCLIUserEcho : tEcho
    {
    }

    // SendIP响应信息
    public class tSendIPEcho : tRcCbIdEcho
    {
    }

    // RestoreFactoryDefaults响应信息
    public class tRestoreFactoryDefaultsEcho : tEcho
    {
    }

    // GetMoteInfo响应信息
    public class tGetMoteInfoEcho : tEcho
    {
        public tMAC mac = new tMAC();       // Mote MAC address
        public enMoteState State = enMoteState.Lost;            // Mote state
        public byte u8NumNbrs = 0;          // The number of motes within range of this mote, both currently and potentially connected
        public byte u8NumGoodNbrs = 0;      // The number of neighboring motes that have good (> 50) quality paths with this mote
        public UInt32 u32RequestedBw = 0;   // Bandwidth requested by mote, milliseconds per packet
        public UInt32 u32TotalNeededBw = 0; // Total bandwidth required by the mote and its children (includes requestedBw), milliseconds per packet
        public UInt32 u32AssignedBw = 0;    // Currently assigned bandwidth, milliseconds per packet
        public UInt32 u32PacketsReceived = 0;// Number of packets received by the manager from the mote
        public UInt32 u32PacketsLost = 0;   // Number of packets sent by the mote, but lost at the manager
        public UInt32 u32AvgLatency = 0;    // The average time (in milliseconds) taken for packets generated at the mote to reach the manager
    }
    
    // GetNetworkConfig响应信息
    public class tGetNetworkConfigEcho : tEcho
    {
        public UInt16 u16NetworkId = 0;     // Network ID
        public sbyte s8ApTxPower = 0;       // Access Point transmit power
        public enFrameProfile frmProfile = enFrameProfile.Profile_01;     // The frame profile describes the length of the slotframes during network building and normal operation                       
        public UInt16 u16MaxMotes = 0;      // The maximum number of motes allowed in the network
        public UInt16 u16BaseBandwidth = 0; // Base bandwidth is the default bandwidth allocated to each mote that joins
        public byte u8DownFrameMultVal = 0; // Downstream frame multiplier is a multiplier for the length of the primary downstream slotframe
        public byte u8NumParents = 0;       // Number of parents allocated to each mote
        public enCcaMode ccaMode = enCcaMode.Off; // Indicates the mode for Clear Channel Assessment (CCA) in the network
        public UInt16 u16ChannelList = 0;   // Bitmap of channels to use for communication
        public bool bAutoStartNetwork = false;  // The Auto Start Network flag tells the Manager whether to start the network as soon as the device is booted
        public byte u8LocMode = 0;          // Reserved
        public enBBMode bbMode = enBBMode.Off;// Backbone frame mode
        public byte u8BbSize = 0;           // Backbone frame size
        public bool isRadioTest = false;    // Indicates whether the Manager is in radiotest mode
        public UInt16 u16BwMult = 0;        // Bandwidth provisioning multiplier in percent (100-1000)
        public byte u8OneChannel = 0;       // Channel number for One Channel mode. 0xFF = One Channel mode is OFF
    }

    // GetNetworkInfoEcho响应信息
    public class tGetNetworkInfoEcho : tEcho
    {
        public UInt16 u16NumMotes = 0;                                      // Number of motes in the ”Operational” state
        public UInt16 u16AsnSize = 0;                                       // ASN size is the timeslot duration, in microseconds
        public enAdvState advState = enAdvState.Off;                        // Advertisement state
        public enDnstreamFrameMode dnfrmMode = enDnstreamFrameMode.Normal;  // Indicates the current downstream frame length, that is, whether or not the multiplier is applied
        public byte u8NetReliability = 0;                                   // Network reliability as a percentage
        public byte u8NetPathStability = 0;                                 // Path stability as a percentage
        public UInt32 u32NetLatency = 0;                                    // Path Latency as a percentage
        public enNetState netState = enNetState.Operational;                // Current network state
        public tIPV6ADDR ipv6Address = new tIPV6ADDR();                     // ipv6Address IPV6_ADDR IPV6 address of the system
        #region Manager固件版本V1.3.0中支持字段
        public UInt32 u32NumLostPackets = 0;                                // Number of lost packets (Added in Manager 1.3.0)
        public UInt64 u64NumArrivedPackets = 0;                             // Number of received packets (Added in Manager 1.3.0)
        public byte u8MaxNumbHops = 0;                                      // Maximum number of hops in network (Added in Manager 1.3.0)
        #endregion Manager固件版本V1.3.0中支持字段
    }

    // GetMoteConfigById响应信息
    public class tGetMoteConfigByIdEcho : tEcho
    {
        public tMAC mac = new tMAC();       // Mote MAC address
        public UInt16 u16MoteId = 0;        // Mote ID (used in health reports)
        public bool isAP = false;           // Indicates this is the Manager access point
        public enMoteState state = enMoteState.Lost;            // Mote state
        public byte u8Reserved = 0;         // Reserved
        public bool isRouting = false;      // Indicates whether this mote can be used as a non-leaf node in the network
    }

    // SetCommonJoinKey响应信息
    public class tSetCommonJoinKeyEcho : tEcho
    {
    }

    // GetIPConfig响应信息
    public class tGetIPConfigEcho : tEcho
    {
        public tIPV6ADDR ipv6Address = new tIPV6ADDR(); // Ipv6Address
        public tIPV6MASK ipv6Mask = new tIPV6MASK();    // Mask
    }

    // SetIPConfig响应信息
    public class tSetIPConfigEcho : tEcho
    {
    }

    // DeleteMote响应信息
    public class tDeleteMoteEcho : tEcho
    {
    }

    // Link structure
    public class tLink
    {
        public byte u8FrameId = 0;
        public UInt32 u32Slot = 0;
        public byte u8ChannelOffset = 0;
        public UInt16 u16MoteId = 0;
        public enLinkFlags Flags = 0;
    }

    // GetMoteLinks响应信息
    public class tGetMoteLinksEcho : tEcho
    {
        public UInt16 u16Idx = 0;
        public byte u8Utilization = 0;
        public byte u8NumLinks = 0;

        public tLink[] links = new tLink[10];
    }

    #endregion 各种命令的响应定义

    #region 通知内容定义

    /// <summary>
    /// 通知信息基类
    /// </summary>
    public class NotifBase
    {
        public enNotifyType NotifyType = enNotifyType.NOTIFID_NULL;

        public const int Len = 1;

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifBase.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("NotifBase.Unserialize take invalid Index");

                NotifyType = (enNotifyType)stream[offset];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // NotifLog信息定义
    public class NotifLog : NotifBase
    {
        private const int MAC_ADRR_OFFSET = NotifBase.Len;
        private const int LOG_OFFSET = MAC_ADRR_OFFSET + tMAC.LEN;

        public tMAC Mac = new tMAC();   // MAC address of notification source
        public byte u8LogMsgLen = 0;
        public byte[] u8aLogMsg = null; // Log message

        public int MinLen
        {
            get { return NotifBase.Len + tMAC.LEN; }
        }

        public new int Len
        {
            get { return MinLen + u8LogMsgLen; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifLog.Unserialize take invalid parameter");
                if (stream.Length < (offset + MinLen))
                    throw new Exception("NotifLog.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                // 提取MAC地址
                Array.Copy(stream, offset + MAC_ADRR_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                // 提取日志信息
                u8LogMsgLen = (byte)(stream.Length - MinLen);
                if (u8LogMsgLen > 0)
                {
                    u8aLogMsg = new byte[u8LogMsgLen];
                    Array.Copy(stream, offset + LOG_OFFSET, u8aLogMsg, 0, u8LogMsgLen);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // NotifData信息定义
    public class NotifData : NotifBase
    {
        private const int UTCSECS_OFFSET = NotifBase.Len;
        private const int UTCUSECS_OFFSET = UTCSECS_OFFSET + 8;
        private const int MAC_OFFSET = UTCUSECS_OFFSET + 4;
        private const int SRC_PORT_OFFSET = MAC_OFFSET + 8;
        private const int DEST_PORT_OFFSET = SRC_PORT_OFFSET + 2;
        private const int DATA_OFFSET = DEST_PORT_OFFSET + 2;

        public tUTCTIMEL tUTCTime = new tUTCTIMEL();      // Time that the packet was generated at the mote
        public tMAC Mac = new tMAC();   // MAC address of the generating mote
        public UInt16 u16SrcPort;       // Source port
        public UInt16 u16DstPort;       // Destination port
        public byte u8DataLen = 0;
        public byte[] u8aData = null;   // Data payload

        public int MinLen
        {
            get { return NotifBase.Len + 24; }
        }

        public new int Len
        {
            get { return MinLen + u8DataLen; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifLog.Unserialize take invalid parameter");
                if (stream.Length < (offset + MinLen))
                    throw new Exception("NotifLog.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                tUTCTime.u64Seconds = DataTypeConverter.MeshByteArrToUInt64(stream, offset + UTCSECS_OFFSET);
                tUTCTime.u32Microseconds = DataTypeConverter.MeshByteArrToUInt32(stream, offset + UTCUSECS_OFFSET);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                u16SrcPort = DataTypeConverter.MeshByteArrToUInt16(stream, offset + SRC_PORT_OFFSET);
                u16DstPort = DataTypeConverter.MeshByteArrToUInt16(stream, offset + DEST_PORT_OFFSET);
                // 提取通知数据
                u8DataLen = (byte)(stream.Length - MinLen);
                if (u8DataLen > 0)
                {
                    u8aData = new byte[u8DataLen];
                    Array.Copy(stream, offset + DATA_OFFSET, u8aData, 0, u8DataLen);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // NotifIpData信息定义
    public class NotifIpData : NotifBase
    {
        private const byte UTCSECS_OFFSET = NotifBase.Len;
        private const byte UTCUSECS_OFFSET = UTCSECS_OFFSET + 8;
        private const byte MAC_OFFSET = UTCUSECS_OFFSET + 4;
        private const byte DATA_OFFSET = MAC_OFFSET + tMAC.LEN;

        public tUTCTIMEL tUTCTime = new tUTCTIMEL();      // Time that the packet was generated at the mote
        public tMAC Mac = new tMAC();   // MAC address of the generating mote
        public byte u8DataLen = 0;
        public byte[] u8aData = null;   // Data payload

        public int MinLen
        {
            get { return NotifBase.Len + 20; }
        }

        public new int Len
        {
            get { return MinLen + u8DataLen; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifIpData.Unserialize take invalid parameter");
                if (stream.Length < (offset + MinLen))
                    throw new Exception("NotifIpData.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                tUTCTime.u64Seconds = DataTypeConverter.MeshByteArrToUInt64(stream, offset + UTCSECS_OFFSET);
                tUTCTime.u32Microseconds = DataTypeConverter.MeshByteArrToUInt32(stream, offset + UTCUSECS_OFFSET);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                // 提取通知数据
                u8DataLen = (byte)(stream.Length - MinLen);
                if (u8DataLen > 0)
                {
                    u8aData = new byte[u8DataLen];
                    Array.Copy(stream, offset + DATA_OFFSET, u8aData, 0, u8DataLen);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    #region 健康报告信息定义
    // NotifHealthReport信息定义
    public class NotifHR : NotifBase
    {
        private const byte MAC_OFFSET = NotifBase.Len;
        private const byte ID_OFFSET = MAC_OFFSET + tMAC.LEN;
        private const byte LENGTH_OFFSET = ID_OFFSET + 1;

        public tMAC Mac = new tMAC();       // The MAC address of the mote from which the health report was received
        public enHRID Id = enHRID.HRID_NULL;// Device Health Report identifier
        public byte u8Length = 0;           // Length of the remainder of the Device Health Report

        public new const int Len = NotifBase.Len + 10;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifHR.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("NotifHR.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                Id = (enHRID)stream[offset + ID_OFFSET];
                u8Length = stream[offset + LENGTH_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // Device Health Report信息定义
    public class NotifDeviceHR : NotifHR
    {
        private const byte CHARGE_OFFSET = NotifHR.Len;
        private const byte QUEUEOCC_OFFSET = CHARGE_OFFSET + 4;
        private const byte TMP_OFFSET = QUEUEOCC_OFFSET + 1;
        private const byte BAT_VOL_OFFSET = TMP_OFFSET + 1;
        private const byte NUMTXOK_OFFSET = BAT_VOL_OFFSET + 2;
        private const byte NUMTXFAIL_OFFSET = NUMTXOK_OFFSET + 2;
        private const byte NUMRXOK_OFFSET = NUMTXFAIL_OFFSET + 2;
        private const byte NUMRXLOST_OFFSET = NUMRXOK_OFFSET + 2;
        private const byte NUMMACDROP_OFFSET = NUMRXLOST_OFFSET + 2;
        private const byte NUMTXBAD_OFFSET = NUMMACDROP_OFFSET + 1;
        private const byte BADLINKFRMID_OFFSET = NUMTXBAD_OFFSET + 1;
        private const byte BADLINKSLOT_OFFSET = BADLINKFRMID_OFFSET + 1;
        private const byte BADLINKOFFS_OFFSET = BADLINKSLOT_OFFSET + 4;

        public UInt32 u32Charge = 0;        // Lifetime charge consumption (in mC)
        public byte u8QueueOcc = 0;         // Mean and max queue occupancy. The 4 most significant bits are mean queue occupancy, the 4 least significant bits are max queue occupancy.
        public sbyte u8Temperature = 0;     // Mote temperature (in degrees C)
        public UInt16 u16BatteryVoltage = 0;// Mote battery voltage (in mV)
        public UInt16 u16NumTxOk = 0;       // Number of packets sent from NET to MAC
        public UInt16 u16NumTxFail = 0;     // Number of packets not sent due to congestion or failure to allocate a packet
        public UInt16 u16NumRxOk = 0;       // Number of received packets
        public UInt16 u16NumRxLost = 0;     // Number of packets lost (discarded by NET layer due to misc errors)
        public byte u8NumMacDropped = 0;    // Number of packets dropped by MAC (due to retry count or age or no route)
        public byte u8NumTxBad = 0;         // Transmit failure counter for bad link
        public byte u8BadLinkFrameId = 0;   // Frame id of link with the worst performance over the last health report interval
        public UInt32 u32BadLinkSlot = 0;   // Slot of link with the worst performance over the last health report interval
        public byte u8BadLinkOffset = 0;    // Offset of link with the worst performance over the last health report interval

        public new int Len
        {
            get { return NotifHR.Len + 24; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifDeviceHR.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("NotifDeviceHR.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u32Charge = DataTypeConverter.MeshByteArrToUInt32(stream, offset + CHARGE_OFFSET);
                u8QueueOcc = stream[offset + QUEUEOCC_OFFSET];
                u8Temperature = (sbyte)stream[offset + TMP_OFFSET];
                u16BatteryVoltage = DataTypeConverter.MeshByteArrToUInt16(stream, offset + BAT_VOL_OFFSET);
                u16NumTxOk = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMTXOK_OFFSET);
                u16NumTxFail = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMTXFAIL_OFFSET);
                u16NumRxOk = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMRXOK_OFFSET);
                u16NumRxLost = DataTypeConverter.MeshByteArrToUInt16(stream, offset + NUMRXLOST_OFFSET);
                u8NumMacDropped = stream[offset + NUMMACDROP_OFFSET];
                u8NumTxBad = stream[offset + NUMTXBAD_OFFSET];
                u8BadLinkFrameId = stream[offset + BADLINKFRMID_OFFSET];
                u32BadLinkSlot = DataTypeConverter.MeshByteArrToUInt32(stream, offset + BADLINKSLOT_OFFSET);
                u8BadLinkOffset = stream[offset + BADLINKOFFS_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // Neighbors' Health Report信息定义
    public class NotifNeighborsHR : NotifHR
    {
        private const byte NUMITEMS_OFFSET = NotifHR.Len;
        private const byte NEIGHBOR_OFFSET = NUMITEMS_OFFSET + 1;

        public byte u8NumItems = 0;                     // Number of neighborHRData structures in this message
        public tNeighborHRData[] neighborHRData = null; // Sequence of numItems neighborHRData structures

        public int MinLen
        {
            get { return NotifHR.Len + 1; }
        }

        public new int Len
        {
            get { return MinLen + u8NumItems * tNeighborHRData.Len; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifNeighborsHR.Unserialize take invalid parameter");
                if (stream.Length < (offset + MinLen))
                    throw new Exception("NotifNeighborsHR.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u8NumItems = stream[offset + NUMITEMS_OFFSET];
                if (u8NumItems > 0)
                {
                    neighborHRData = new tNeighborHRData[u8NumItems];
                    for (int i = 0; i < u8NumItems; i++)
                    {
                        neighborHRData[i] = new tNeighborHRData();
                        neighborHRData[i].Unserialize(stream, offset + NEIGHBOR_OFFSET + i * tNeighborHRData.Len);
                    }
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // Discovered Neighbors Health Report
    public class NotifDiscoveredNeighborsHR : NotifHR
    {
        private const byte NUMJOINPARENTS_OFFSET = NotifHR.Len;
        private const byte NUMITEMS_OFFSET = NUMJOINPARENTS_OFFSET + 1;
        private const byte NEIGHBORDATA_OFFSET = NUMITEMS_OFFSET + 1;

        public byte u8NumJoinParents = 0;   // JoinParents 
        public byte u8NumItems = 0;         // Number of discovered neighbor structures in this message 
        public tDiscoveredNeighborHRData[] neighborHRData = null; // Sequence of numItems discoveredNeighborData structures

        public int MinLen
        {
            get { return NotifHR.Len + 2; }
        }

        public new int Len
        {
            get { return MinLen + u8NumItems * tDiscoveredNeighborHRData.Len; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotifDiscoveredNeighborsHR.Unserialize take invalid parameter");
                if (stream.Length < (offset + MinLen))
                    throw new Exception("NotifDiscoveredNeighborsHR.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u8NumJoinParents = stream[offset + NUMJOINPARENTS_OFFSET];
                u8NumItems = stream[offset + NUMITEMS_OFFSET];
                if (u8NumItems > 0)
                {
                    neighborHRData = new tDiscoveredNeighborHRData[u8NumItems];
                    for (int i = 0; i < u8NumItems; i++)
                    {
                        neighborHRData[i] = new tDiscoveredNeighborHRData();
                        neighborHRData[i].Unserialize(stream, offset + NEIGHBORDATA_OFFSET + i * tDiscoveredNeighborHRData.Len);
                    }
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }
    #endregion 健康报告信息定义

    #region 通知事件信息定义
    /// <summary>
    /// 通知事件基类
    /// </summary>
    public class NotEvent : NotifBase
    {
        private const byte EVENTID_OFFSET = NotifBase.Len;
        private const byte EVENTTYPE_OFFSET = EVENTID_OFFSET + sizeof(UInt32);

        public UInt32 u32EventId = 0;
        public enEventType EventType = enEventType.EVENTID_NULL;

        public new const int Len = NotifBase.Len + 5;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("NotEvent.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("NotEvent.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                DataTypeConverter.MeshByteArrToUInt32(stream, offset + EVENTID_OFFSET);
                EventType = (enEventType)stream[offset + EVENTTYPE_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteReset信息定义
    public class EventMoteReset : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;

        // Mote MAC address
        public tMAC Mac = new tMAC();

        public new int Len
        {
            get { return NotEvent.Len + tMAC.LEN; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteReset.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteReset.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventNetworkReset信息定义
    public class EventNetworkReset : NotEvent
    {
        public new int Len
        {
            get { return NotEvent.Len; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventNetworkReset.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventNetworkReset.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventCommandFinished信息定义
    public class EventCommandFinished : NotEvent
    {
        private const byte CALLBACKID_OFFSET = NotEvent.Len;
        private const byte RC_OFFSET = CALLBACKID_OFFSET + sizeof(UInt32);

        public UInt32 u32CallbackId = 0;    // Callback ID that was returned in the response packet of the corresponding command
        public enCommandFinishedResult u8RC = 0;               // Command finished result code

        public new int Len
        {
            get { return NotEvent.Len + 5; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventCommandFinished.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventCommandFinished.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(stream, offset + CALLBACKID_OFFSET);
                u8RC = (enCommandFinishedResult)stream[offset + RC_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteJoin信息定义
    public class EventMoteJoin : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;

        // Mote MAC address
        public tMAC Mac = new tMAC();

        public new int Len
        {
            get { return NotEvent.Len + tMAC.LEN; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteJoin.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteJoin.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteOperational信息定义
    public class EventMoteOperational : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;

        public tMAC Mac = new tMAC();

        public new int Len
        {
            get { return NotEvent.Len + tMAC.LEN; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteOperational.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteOperational.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteLost信息定义
    public class EventMoteLost : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;

        public tMAC Mac = new tMAC();

        public new int Len
        {
            get { return NotEvent.Len + tMAC.LEN; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteLost.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteLost.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventNetworkTime信息定义
    public class EventNetworkTime : NotEvent
    {
        private const byte UPTIME_OFFSET = NotEvent.Len;
        private const byte UTCSECS_OFFSET = UPTIME_OFFSET + sizeof(UInt32);
        private const byte UTCUSECS_OFFSET = UTCSECS_OFFSET + sizeof(UInt64);
        private const byte ASN_OFFSET = UTCUSECS_OFFSET + sizeof(UInt32);
        private const byte ASN_OFF_OFFSET = ASN_OFFSET + tASN.LEN;

        public UInt32 u32Uptime = 0;    // Time (sec) that the packet was generated (as uptime)
        public tUTCTIMEL tUTCTime = new tUTCTIMEL();      // Time that the packet was generated (as UTC)
        public tASN tAsn = new tASN();               // Absolute slot number
        public UInt16 u16AsnOffset = 0; // ASN offset (in microseconds).

        public new int Len
        {
            get { return NotEvent.Len + 23; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventNetworkTime.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventNetworkTime.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u32Uptime = DataTypeConverter.MeshByteArrToUInt32(stream, offset + UPTIME_OFFSET);
                tUTCTime.u64Seconds = DataTypeConverter.MeshByteArrToUInt64(stream, offset + UTCSECS_OFFSET);
                tUTCTime.u32Microseconds = DataTypeConverter.MeshByteArrToUInt32(stream, offset + UTCUSECS_OFFSET);
                Array.Copy(stream, offset + ASN_OFFSET, tAsn.u8aData, 0, tASN.LEN);
                u16AsnOffset = DataTypeConverter.MeshByteArrToUInt16(stream, offset + ASN_OFF_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventPingResponse信息定义
    public class EventPingResponse : NotEvent
    {
        private const byte CBID_OFFSET = NotEvent.Len;
        private const byte MACADDR_OFFSET = CBID_OFFSET + sizeof(UInt32);
        private const byte DELAY_OFFSET = MACADDR_OFFSET + tMAC.LEN;
        private const byte VOLTAGE_OFFSET = DELAY_OFFSET + sizeof(UInt32);
        private const byte TEMP_OFFSET = VOLTAGE_OFFSET + sizeof(UInt16);

        public UInt32 u32CallbackId = 0;    // The callback ID that was returned in the response packet associated with the ping mote request
        public tMAC Mac = new tMAC();       // MAC address of mote pinged
        public UInt32 u32Delay = 0;         // Round trip delay in milliseconds or -1: ping timeout
        public UInt16 u16Voltage = 0;       // Voltage reported by mote (millivolts)
        public byte u8Temperature = 0;      // Voltage reported by mote (millivolts)

        public new int Len
        {
            get { return NotEvent.Len + 19; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventPingResponse.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventPingResponse.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(stream, offset + CBID_OFFSET);
                Array.Copy(stream, offset + MACADDR_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                u32Delay = DataTypeConverter.MeshByteArrToUInt32(stream, offset + DELAY_OFFSET);
                u16Voltage = DataTypeConverter.MeshByteArrToUInt16(stream, offset + VOLTAGE_OFFSET);
                u8Temperature = stream[offset + TEMP_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventPathCreate信息定义
    public class EventPathCreate : NotEvent
    {
        private const byte SRC_MAC_OFFSET = NotEvent.Len;
        private const byte DST_MAC_OFFSET = SRC_MAC_OFFSET + tMAC.LEN;
        private const byte PATH_DIR_OFFSET = DST_MAC_OFFSET + tMAC.LEN;

        public tMAC Source = new tMAC();
        public tMAC Dest = new tMAC();
        public enPathDirection Direction = enPathDirection.None;

        public new int Len
        {
            get { return NotEvent.Len + 17; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventPathCreate.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventPathCreate.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + SRC_MAC_OFFSET, Source.u8aData, 0, tMAC.LEN);
                Array.Copy(stream, offset + DST_MAC_OFFSET, Dest.u8aData, 0, tMAC.LEN);
                Direction = (enPathDirection)stream[offset + PATH_DIR_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventPathDelete信息定义
    public class EventPathDelete : NotEvent
    {
        private const byte SRC_MAC_OFFSET = NotEvent.Len;
        private const byte DST_MAC_OFFSET = SRC_MAC_OFFSET + tMAC.LEN;
        private const byte PATH_DIR_OFFSET = DST_MAC_OFFSET + tMAC.LEN;

        public tMAC Source = new tMAC();
        public tMAC Dest = new tMAC();
        public enPathDirection Direction = enPathDirection.None;

        public new int Len
        {
            get { return NotEvent.Len + 17; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventPathDelete.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventPathDelete.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + SRC_MAC_OFFSET, Source.u8aData, 0, tMAC.LEN);
                Array.Copy(stream, offset + DST_MAC_OFFSET, Dest.u8aData, 0, tMAC.LEN);
                Direction = (enPathDirection)stream[offset + PATH_DIR_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventPacketSent信息定义
    public class EventPacketSent : NotEvent
    {
        private const byte CALLBACKID_OFFSET = NotEvent.Len;
        private const byte RC_OFFSET = CALLBACKID_OFFSET + sizeof(UInt32);

        public UInt32 u32CallbackId = 0;
        public byte u8RC = 0;

        public new int Len
        {
            get { return NotEvent.Len + 5; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventPacketSent.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventPacketSent.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(stream, offset + CALLBACKID_OFFSET);
                u8RC = stream[offset + RC_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteCreate信息定义
    public class EventMoteCreate : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;
        private const byte MOTE_ID_OFFSET = MAC_OFFSET + tMAC.LEN;

        public tMAC Mac = new tMAC();
        public UInt16 u16MoteId;

        public new int Len
        {
            get { return NotEvent.Len + 10; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteCreate.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteCreate.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                u16MoteId = DataTypeConverter.MeshByteArrToUInt16(stream, offset + MOTE_ID_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }

    // EventMoteDelete信息定义
    public class EventMoteDelete : NotEvent
    {
        private const byte MAC_OFFSET = NotEvent.Len;
        private const byte MOTE_ID_OFFSET = MAC_OFFSET + tMAC.LEN;

        public tMAC Mac = new tMAC();
        public UInt16 u16MoteId;

        public new int Len
        {
            get { return NotEvent.Len + 10; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("EventMoteDelete.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("EventMoteDelete.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                Array.Copy(stream, offset + MAC_OFFSET, Mac.u8aData, 0, tMAC.LEN);
                u16MoteId = DataTypeConverter.MeshByteArrToUInt16(stream, offset + MOTE_ID_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message);
            }
        }
    }
    #endregion 通知事件信息定义

    #endregion 通知内容定义
}
