using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    internal partial class MESH
    {
        #region meshAPI请求响应函数

        /// <summary>
        /// 需要放慢对Manager的访问速度
        /// </summary>
        private volatile bool m_bStressfull = false;
        /// <summary>
        /// 当前命令的对象WS的Mac地址，用于检测WS是否在线，如果不在线需要告诉上层那个WS掉线
        /// </summary>
        private tMAC m_macCurTarget = null;
        private void ShearedReplyCB(enCmd cmdId, tEcho echo, tMAC mac = null)
        {
            // 忙于通信的标志位重置
            CancelTx();
            // 通知上层请求响应的到达
            if (ReplyArrived != null)
                ReplyArrived(cmdId, echo);

            // 当前请求Manager的速度超过其能够承受的极限，故应放慢对Manager的访问速度
            if (echo.RC == eRC.RC_NACK)
                m_bStressfull = true;
            else
                m_bStressfull = false;

            // 通知请求处理线程，请求响应到达，可以处理新的用户请求
            m_asigMgrResponsed.Set();
            
            // 这种情况下表示遍历网络中所有WS到最后一个
            if (IsQueryingAllWs && cmdId == enCmd.CMDID_GETMOTECONFIG && echo.RC == eRC.RC_END_OF_LIST)
                return;
            else if (cmdId == enCmd.CMDID_GETMOTELINKS && echo.RC == eRC.RC_END_OF_LIST)
                return;

            // 如果某条命令的响应码是RC_END_OF_LIST，则表示此Mote已经掉线
            // wst:如果某条命令的响应码是RC_INV_STATE，则表示此Mote已经掉线
            if (echo.RC == eRC.RC_INV_STATE || echo.RC == eRC.RC_END_OF_LIST || echo.RC == eRC.RC_NOT_FOUND)
            {
                try
                {
                    // 人为构造EVENTMOTELOST事件，并通知上层
                    EventMoteLost evtMoteLost = new EventMoteLost();
                    evtMoteLost.NotifyType = enNotifyType.NOTIFID_NOTIFEVENT;
                    evtMoteLost.EventType = enEventType.EVENTID_EVENTMOTELOST;
                    evtMoteLost.Mac.Assign(m_macCurTarget);
                    m_macCurTarget = null;
                    CommStackLog.RecordInf(enLogLayer.eMesh, "Detected mote lost!");
                    m_evNotifyArrived(evtMoteLost.NotifyType, evtMoteLost.EventType, evtMoteLost/*,6*/);
                }
                catch (Exception ex)
                {
                    m_macCurTarget = null;
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
            }
            else
                m_macCurTarget = null;
        }

        private const byte SENDDATA_REPLY_CALLBACKID_OFFS = 0;
        private const byte SENDDATA_REPLY_LEN = SENDDATA_REPLY_CALLBACKID_OFFS + sizeof(UInt32);
        public void SendDataReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            tSendDataEcho echo = new tSendDataEcho();
            echo.RC = rc;

            if (rc == eRC.RC_OK)
            {
                if (len < SENDDATA_REPLY_LEN)
                {
                    CancelTx();
                    CommStackLog.RecordInf(enLogLayer.eMesh, "SendData reply payload too short");
                    return;
                }

                echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, SENDDATA_REPLY_CALLBACKID_OFFS);
                lock (m_dicAsyncCmdRespRecords)
                {
                    if (!m_dicAsyncCmdRespRecords.ContainsKey(echo.u32CallbackId))
                    {
                        AsyncCmdRespEntry elem = new AsyncCmdRespEntry();
                        elem.ACK1ST = true;
                        elem.SiblingRequest = Adapter2MeshBridge;
                        m_dicAsyncCmdRespRecords.Add(echo.u32CallbackId, elem);
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Wait SDCBID(" + echo.u32CallbackId + ")");
                    }
                    else
                    {
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACKTime = DateTime.Now;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACK1ST = true;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].SiblingRequest = Adapter2MeshBridge;
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Overdue SDCBID(" + echo.u32CallbackId + ")");
                    }
                }
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SendDataReply RC(" + rc + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte RESET_REPLY_MACADDRESS_OFFS = 0;
        private const byte RESET_REPLY_LEN = 8;
        public void ResetReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < RESET_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "Reset reply payload too short");
                return;
            }

            tResetEcho echo = new tResetEcho();
            echo.RC = rc;
            if (echo.RC == eRC.RC_OK)
            {
                Array.Copy(payload, RESET_REPLY_MACADDRESS_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
                CommStackLog.RecordInf(enLogLayer.eMesh, "Reset Mote(" + echo.mac.ToHexString() + ") OK");
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "Reset Mote(" + echo.mac.ToHexString() + ") RC(" + echo.RC + ")");
            
            ShearedReplyCB(cmdId, echo, echo.mac);
        }

        private const byte SUB_REPLY_LEN = 0;
        public void SubscribeReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SUB_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "Subscribe reply payload too short");
                return;
            }

            tSubEcho echo = new tSubEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "Subscribe RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "Subscribe OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETIME_REPLY_UPTIME_OFFS = 0;
        private const byte GETIME_REPLY_UTCSEC_OFFS = 4;
        private const byte GETIME_REPLY_UTCMSEC_OFFS = 12;
        private const byte GETIME_REPLY_ASN_OFFS = 16;
        private const byte GETIME_REPLY_ASNOFFSET_OFFS = 21;
        private const byte GETIME_REPLY_LEN = 23;
        public void GetTimeReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETIME_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetTime reply payload too short");
                return;
            }

            tGetTimeEcho echo = new tGetTimeEcho();
            echo.RC = rc;
            echo.u32Uptime = DataTypeConverter.MeshByteArrToUInt32(payload, GETIME_REPLY_UPTIME_OFFS);
            echo.utcTime.u64Seconds = DataTypeConverter.MeshByteArrToUInt64(payload, GETIME_REPLY_UTCSEC_OFFS);
            echo.utcTime.u32Microseconds = DataTypeConverter.MeshByteArrToUInt32(payload, GETIME_REPLY_UTCMSEC_OFFS);
            Array.Copy(payload, GETIME_REPLY_ASN_OFFS, echo.asn.u8aData, 0, tASN.LEN);
            echo.u16AsnOffset = DataTypeConverter.MeshByteArrToUInt16(payload, GETIME_REPLY_ASNOFFSET_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetTime OK, Uptime(" + echo.u32Uptime.ToString() +
                    ")|UTCSec(" + echo.utcTime.u64Seconds.ToString() +
                    ")|UTCMSec(" + echo.utcTime.u32Microseconds.ToString() +
                    ")|AsnOffset(" + echo.u16AsnOffset.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetTime RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETNETWORKCONFIG_REPLY_LEN = 0;
        public void SetNetworkConfigReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
            {
                CommStackLog.RecordInf(enLogLayer.eMesh, "cmdId=" + cmdId + " m_u8CmdId=" + m_u8CmdId);
                return;
            }
            if (len < SETNETWORKCONFIG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetNetworkConfig reply payload too short");
                return;
            }

            tSetNetworkConfigEcho echo = new tSetNetworkConfigEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetNetworkConfig RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetNetworkConfig OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte CLEARSTATICS_REPLY_LEN = 0;
        public void ClearStatisticsReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < CLEARSTATICS_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "ClearStatistics reply payload too short");
                return;
            }

            tClearStatisticsEcho echo = new tClearStatisticsEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "ClearStatistics RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "ClearStatistics OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte EXCHMOTEJK_REPLY_CALLBACKID_OFFS = 0;
        private const byte EXCHMOTEJK_REPLY_LEN = 4;
        public void ExchangeMoteJoinKeyReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;

            tExchangeMoteJoinKeyEcho echo = new tExchangeMoteJoinKeyEcho();
            echo.RC = rc;

            if (rc == eRC.RC_OK)
            {
                if (len < EXCHMOTEJK_REPLY_LEN)
                {
                    CancelTx();
                    CommStackLog.RecordInf(enLogLayer.eMesh, "ExchangeMoteJoinKey reply payload too short");
                    return;
                }

                echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, EXCHMOTEJK_REPLY_CALLBACKID_OFFS);
                lock (m_dicAsyncCmdRespRecords)
                {
                    if (!m_dicAsyncCmdRespRecords.ContainsKey(echo.u32CallbackId))
                    {
                        AsyncCmdRespEntry elem = new AsyncCmdRespEntry();
                        elem.ACK1ST = true;
                        elem.SiblingRequest = Adapter2MeshBridge;
                        m_dicAsyncCmdRespRecords.Add(echo.u32CallbackId, elem);
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Wait EXMJKCBID(" + echo.u32CallbackId + ")");
                    }
                    else
                    {
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACKTime = DateTime.Now;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACK1ST = true;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].SiblingRequest = Adapter2MeshBridge;
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Overdue EXMJKCBID(" + echo.u32CallbackId + ")");
                    }
                }
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "ExchangeMoteJoinKey RC(" + rc + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte EXCHNWID_REPLY_CALLBACKID_OFFS = 0;
        private const byte EXCHNWID_REPLY_LEN = 4;
        public void ExchangeNetworkIdReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;

            tExchangeNetworkIdEcho echo = new tExchangeNetworkIdEcho();
            echo.RC = rc;

            if (rc == eRC.RC_OK)
            {
                if (len < EXCHNWID_REPLY_LEN)
                {
                    CancelTx();
                    CommStackLog.RecordInf(enLogLayer.eMesh, "ExchangeNetworkId reply payload too short");
                    return;
                }

                echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, EXCHNWID_REPLY_CALLBACKID_OFFS);
                lock (m_dicAsyncCmdRespRecords)
                {
                    if (!m_dicAsyncCmdRespRecords.ContainsKey(echo.u32CallbackId))
                    {
                        AsyncCmdRespEntry elem = new AsyncCmdRespEntry();
                        elem.ACK1ST = true;
                        elem.SiblingRequest = Adapter2MeshBridge;
                        m_dicAsyncCmdRespRecords.Add(echo.u32CallbackId, elem);
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Wait EXNIDCBID(" + echo.u32CallbackId + ")");
                    }
                    else
                    {
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACKTime = DateTime.Now;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].ACK1ST = true;
                        m_dicAsyncCmdRespRecords[echo.u32CallbackId].SiblingRequest = Adapter2MeshBridge;
                        CommStackLog.RecordInf(enLogLayer.eMesh, "Overdue EXNIDCBID(" + echo.u32CallbackId + ")");
                    }
                }
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "ExchangeNetworkId RC(" + rc + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte RADIOTESTTX_REPLY_LEN = 0;
        public void RadioTestTxReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < RADIOTESTTX_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestTx reply payload too short");
                return;
            }

            tRadiotestTxEcho echo = new tRadiotestTxEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestTx RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestTx OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte RADIOTESTRX_REPLY_LEN = 0;
        public void RadioTestRxReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < RADIOTESTRX_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestRx reply payload too short");
                return;
            }

            tRadiotestRxEcho echo = new tRadiotestRxEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestRx RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestRx OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETRADIOTESTSTATICS_REPLY_RXOK_OFFS = 0;
        private const byte GETRADIOTESTSTATICS_REPLY_RXFAIL_OFFS = 2;
        private const byte GETRADIOTESTSTATICS_REPLY_LEN = 0;
        public void GetRadiotestStatisticsReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETRADIOTESTSTATICS_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetRadiotestStatistics reply payload too short");
                return;
            }

            tGetRadiotestStatisticsEcho echo = new tGetRadiotestStatisticsEcho();
            echo.RC = rc;
            echo.u16RxOk = DataTypeConverter.MeshByteArrToUInt16(payload, GETRADIOTESTSTATICS_REPLY_RXOK_OFFS);
            echo.u16RxFail = DataTypeConverter.MeshByteArrToUInt16(payload, GETRADIOTESTSTATICS_REPLY_RXFAIL_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetRadiotestStatistics OK, RxOk(" + echo.u16RxOk.ToString() +
                    ")|RxFail(" + echo.u16RxFail.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetRadiotestStatistics RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETACLENTRY_REPLY_LEN = 0;
        public void SetACLEntryReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETACLENTRY_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetACLEntry reply payload too short");
                return;
            }

            tSetACLEntryEcho echo = new tSetACLEntryEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetACLEntry RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetACLEntry OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETNEXTACLENTRY_REPLY_MACADDR_OFFS = 0;
        private const byte GETNEXTACLENTRY_REPLY_JOINKEY_OFFS = 8;
        private const byte GETNEXTACLENTRY_REPLY_LEN = 24;
        public void GetNextACLEntryReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETNEXTACLENTRY_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextACLEntry reply payload too short");
                return;
            }

            tGetNextACLEntryEcho echo = new tGetNextACLEntryEcho();
            echo.RC = rc;
            Array.Copy(payload, GETNEXTACLENTRY_REPLY_MACADDR_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
            Array.Copy(payload, GETNEXTACLENTRY_REPLY_JOINKEY_OFFS, echo.JoinKey.u8aData, 0, tSECKEY.LEN);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextACLEntry OK, MAC: " + echo.mac.ToHexString() +
                    ", JoinKey: " + echo.JoinKey.ToHexString());
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextACLEntry RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo, echo.mac);
        }

        private const byte DELETEACLENTRY_REPLY_LEN = 0;
        public void DeleteACLEntryReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < DELETEACLENTRY_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteACLEntry reply payload too short");
                return;
            }

            tDeleteACLEntryEcho echo = new tDeleteACLEntryEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteACLEntry RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteACLEntry OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte PINGMOTE_REPLY_CALLBACKID_OFFS = 0;
        private const byte PINGMOTE_REPLY_LEN = 4;
        public void PingMoteReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < PINGMOTE_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "PingMote reply payload too short");
                return;
            }

            tPingMoteEcho echo = new tPingMoteEcho();
            echo.RC = rc;
            echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, PINGMOTE_REPLY_CALLBACKID_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "PingMote OK, CallbackId: " + echo.u32CallbackId.ToString());
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "PingMote RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETLOG_REPLY_LEN = 0;
        public void GetLogReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETLOG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLog reply payload too short");
                return;
            }

            tGetLogEcho echo = new tGetLogEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLog RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLog OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte STARTNETWORK_REPLY_LEN = 0;
        public void StartNetworkReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < STARTNETWORK_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "StartNetwork reply payload too short");
                return;
            }

            tStartNetworkEcho echo = new tStartNetworkEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "StartNetwork RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "StartNetwork OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETSYSTEMINFO_REPLY_MACADDR_OFFS = 0;
        private const byte GETSYSTEMINFO_REPLY_HWMODEL_OFFS = 8;
        private const byte GETSYSTEMINFO_REPLY_HWREV_OFFS = 9;
        private const byte GETSYSTEMINFO_REPLY_SWMAJOR_OFFS = 10;
        private const byte GETSYSTEMINFO_REPLY_SWMINOR_OFFS = 11;
        private const byte GETSYSTEMINFO_REPLY_SWPATCH_OFFS = 12;
        private const byte GETSYSTEMINFO_REPLY_SWBUILD_OFFS = 13;
        private const byte GETSYSTEMINFO_REPLY_LEN = 14;
        public void GetSystemInfoReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < STARTNETWORK_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetSystemInfo reply payload too short");
                return;
            }

            tGetSystemInfoEcho echo = new tGetSystemInfoEcho();
            echo.RC = rc;
            Array.Copy(payload, GETSYSTEMINFO_REPLY_MACADDR_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
            echo.u8HwModel = payload[GETSYSTEMINFO_REPLY_HWMODEL_OFFS];
            echo.u8HwRev = payload[GETSYSTEMINFO_REPLY_HWREV_OFFS];
            echo.u8SwMajor = payload[GETSYSTEMINFO_REPLY_SWMAJOR_OFFS];
            echo.u8SwMinor = payload[GETSYSTEMINFO_REPLY_SWMINOR_OFFS];
            echo.u8SwPatch = payload[GETSYSTEMINFO_REPLY_SWPATCH_OFFS];
            echo.u8SwBuild = payload[GETSYSTEMINFO_REPLY_SWBUILD_OFFS];

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetSystemInfo OK, HwModel(" + echo.u8HwModel.ToString() +
                    ")|HwRev(" + echo.u8HwRev.ToString() +
                    ")|SwVer(" + echo.u8SwMajor.ToString() + "." +
                    echo.u8SwMinor.ToString() + "." + echo.u8SwPatch.ToString() + "." + echo.u8SwBuild.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetSystemInfo RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo, echo.mac);
        }

        private const byte GETMOTECONFIG_REPLY_MACADDR_OFFS = 0;
        private const byte GETMOTECONFIG_REPLY_MOTEID_OFFS = 8;
        private const byte GETMOTECONFIG_REPLY_ISAP_OFFS = 10;
        private const byte GETMOTECONFIG_REPLY_STATE_OFFS = 11;
        private const byte GETMOTECONFIG_REPLY_RESERVED_OFFS = 12;
        private const byte GETMOTECONFIG_REPLY_ISROUTING_OFFS = 13;
        private const byte GETMOTECONFIG_REPLY_LEN = 14;
        public void GetMoteConfigReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;

            tGetMoteConfigEcho echo = new tGetMoteConfigEcho();
            echo.RC = rc;
            if (echo.RC == eRC.RC_OK)
            {
                if (len < GETMOTECONFIG_REPLY_LEN)
                {
                    CancelTx();
                    CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfig reply payload too short");
                    return;
                }

                Array.Copy(payload, GETMOTECONFIG_REPLY_MACADDR_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
                echo.u16MoteId = DataTypeConverter.MeshByteArrToUInt16(payload, GETMOTECONFIG_REPLY_MOTEID_OFFS);
                echo.isAP = (payload[GETMOTECONFIG_REPLY_ISAP_OFFS] == 0) ? false : true;
                echo.u8State = payload[GETMOTECONFIG_REPLY_STATE_OFFS];
                echo.isRouting = (payload[GETMOTECONFIG_REPLY_ISROUTING_OFFS] == 0) ? false : true;

                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfig OK, Mote(" + echo.mac.ToHexString() +
                        ")|MoteId(" + echo.u16MoteId.ToString() + ")|isAP(" + echo.isAP.ToString() +
                        ")|State(" + ((enMoteState)echo.u8State).ToString() +
                        ")|isRouting(" + echo.isRouting.ToString() + ")");
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfig RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETPATHINFO_REPLY_SRCMACADDR_OFFS = 0;
        private const byte GETPATHINFO_REPLY_DSTMACADDR_OFFS = 8;
        private const byte GETPATHINFO_REPLY_DIRECTION_OFFS = 16;
        private const byte GETPATHINFO_REPLY_NUMLINKS_OFFS = 17;
        private const byte GETPATHINFO_REPLY_QUALITY_OFFS = 18;
        private const byte GETPATHINFO_REPLY_RSSISRCDST_OFFS = 19;
        private const byte GETPATHINFO_REPLY_RSSIDSTSRC_OFFS = 20;
        private const byte GETPATHINFO_REPLY_LEN = 21;
        public void GetPathInfoReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETPATHINFO_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetPathInfo reply payload too short");
                return;
            }

            tGetPathInfoEcho echo = new tGetPathInfoEcho();
            echo.RC = rc;
            Array.Copy(payload, GETPATHINFO_REPLY_SRCMACADDR_OFFS, echo.Source.u8aData, 0, tMAC.LEN);
            Array.Copy(payload, GETPATHINFO_REPLY_DSTMACADDR_OFFS, echo.Dest.u8aData, 0, tMAC.LEN);
            echo.Dir = (enPathDirection)payload[GETPATHINFO_REPLY_DIRECTION_OFFS];
            echo.u8NumLinks = payload[GETPATHINFO_REPLY_NUMLINKS_OFFS];
            echo.u8Quality = payload[GETPATHINFO_REPLY_NUMLINKS_OFFS];
            echo.u8RssiSrcDest = payload[GETPATHINFO_REPLY_RSSISRCDST_OFFS];
            echo.u8RssiDestSrc = payload[GETPATHINFO_REPLY_RSSIDSTSRC_OFFS];

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetPathInfo OK, SrcMote(" + echo.Source.ToHexString() +
                    ")|DstMote(" + echo.Dest.ToString() +
                    ")|Direction(" + echo.Dir.ToString() +
                    ")|NumLinks(" + echo.u8NumLinks.ToString() +
                    ")|Quality(" + echo.u8Quality.ToString() +
                    ")|RssiSrcDest(" + echo.u8RssiSrcDest.ToString() +
                    ")|RssiDestSrc(" + echo.u8RssiDestSrc.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetPathInfo RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETNEXTPATHINFO_REPLY_PATHID_OFFS = 0;
        private const byte GETNEXTPATHINFO_REPLY_SRCMACADDR_OFFS = 1;
        private const byte GETNEXTPATHINFO_REPLY_DSTMACADDR_OFFS = 9;
        private const byte GETNEXTPATHINFO_REPLY_DIRECTION_OFFS = 17;
        private const byte GETNEXTPATHINFO_REPLY_NUMLINKS_OFFS = 18;
        private const byte GETNEXTPATHINFO_REPLY_QUALITY_OFFS = 19;
        private const byte GETNEXTPATHINFO_REPLY_RSSISRCDST_OFFS = 20;
        private const byte GETNEXTPATHINFO_REPLY_RSSIDSTSRC_OFFS = 21;
        private const byte GETNEXTPATHINFO_REPLY_LEN = 22;
        public void GetNextPathInfoReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETNEXTPATHINFO_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextPathInfo reply payload too short");
                return;
            }

            tGetNextPathInfoEcho echo = new tGetNextPathInfoEcho();
            echo.RC = rc;
            echo.u16PathId = DataTypeConverter.MeshByteArrToUInt16(payload, GETNEXTPATHINFO_REPLY_PATHID_OFFS);
            Array.Copy(payload, GETNEXTPATHINFO_REPLY_SRCMACADDR_OFFS, echo.Source.u8aData, 0, tMAC.LEN);
            Array.Copy(payload, GETNEXTPATHINFO_REPLY_DSTMACADDR_OFFS, echo.Dest.u8aData, 0, tMAC.LEN);
            echo.Dir = (enPathDirection)payload[GETNEXTPATHINFO_REPLY_DIRECTION_OFFS];
            echo.u8NumLinks = payload[GETNEXTPATHINFO_REPLY_NUMLINKS_OFFS];
            echo.u8Quality = payload[GETNEXTPATHINFO_REPLY_QUALITY_OFFS];
            echo.u8RssiSrcDest = payload[GETNEXTPATHINFO_REPLY_RSSISRCDST_OFFS];
            echo.u8RssiDestSrc = payload[GETNEXTPATHINFO_REPLY_RSSIDSTSRC_OFFS];

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextPathInfo OK, PathId(" + echo.u16PathId.ToString() +
                    ")|SrcMote(" + echo.Source.ToHexString() +
                    ")|DstMote(" + echo.Dest.ToString() +
                    ")|Direction(" + echo.Dir.ToString() +
                    ")|NumLinks(" + echo.u8NumLinks.ToString() +
                    ")|Quality(" + echo.u8Quality.ToString() +
                    ")|RssiSrcDest(" + echo.u8RssiSrcDest.ToString() +
                    ")|RssiDestSrc(" + echo.u8RssiDestSrc.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextPathInfo RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETADVERTISING_REPLY_CALLBACKID_OFFS = 0;
        private const byte SETADVERTISING_REPLY_LEN = 4;
        public void SetAdvertisingReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETADVERTISING_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetAdvertising reply payload too short");
                return;
            }

            tSetAdvertisingEcho echo = new tSetAdvertisingEcho();
            echo.RC = rc;
            echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, SETADVERTISING_REPLY_CALLBACKID_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetAdvertising OK, CallbackId: " + echo.u32CallbackId.ToString());
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetAdvertising RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETDOWNSTREAMFRAMEMODE_REPLY_CALLBACKID_OFFS = 0;
        private const byte SETDOWNSTREAMFRAMEMODE_REPLY_LEN = 4;
        public void SetDownstreamFrameModeReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETDOWNSTREAMFRAMEMODE_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetDownstreamFrameMode reply payload too short");
                return;
            }

            tSetDownstreamFrameModeEcho echo = new tSetDownstreamFrameModeEcho();
            echo.RC = rc;
            echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, SETDOWNSTREAMFRAMEMODE_REPLY_CALLBACKID_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetDownstreamFrameMode OK, CallbackId: " + echo.u32CallbackId.ToString());
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetDownstreamFrameMode RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETMANAGERSTATISTICS_REPLY_SERTXCNT_OFFS = 0;
        private const byte GETMANAGERSTATISTICS_REPLY_SERRXCNT_OFFS = 2;
        private const byte GETMANAGERSTATISTICS_REPLY_SERRXCRCERR_OFFS = 4;
        private const byte GETMANAGERSTATISTICS_REPLY_SERRXOVERRUNS_OFFS = 6;
        private const byte GETMANAGERSTATISTICS_REPLY_APIESTABCONN_OFFS = 8;
        private const byte GETMANAGERSTATISTICS_REPLY_APIDROPPEDCONN_OFFS = 10;
        private const byte GETMANAGERSTATISTICS_REPLY_APITXOK_OFFS = 12;
        private const byte GETMANAGERSTATISTICS_REPLY_APITXERR_OFFS = 14;
        private const byte GETMANAGERSTATISTICS_REPLY_APITXFAIL_OFFS = 16;
        private const byte GETMANAGERSTATISTICS_REPLY_APIRXOK_OFFS = 18;
        private const byte GETMANAGERSTATISTICS_REPLY_APIRXPROTERR_OFFS = 20;
        private const byte GETMANAGERSTATISTICS_REPLY_LEN = 22;
        public void GetManagerStatisticsReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETMANAGERSTATISTICS_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetManagerStatistics reply payload too short");
                return;
            }

            tGetManagerStatisticsEcho echo = new tGetManagerStatisticsEcho();
            echo.RC = rc;
            echo.u16SerTxCnt = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_SERTXCNT_OFFS);
            echo.u16SerRxCnt = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_SERRXCNT_OFFS);
            echo.u16SerRxCRCErr = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_SERRXCRCERR_OFFS);
            echo.u16SerRxOverruns = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_SERRXOVERRUNS_OFFS);
            echo.u16ApiEstabConn = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APIESTABCONN_OFFS);
            echo.u16ApiDroppedConn = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APIDROPPEDCONN_OFFS);
            echo.u16ApiTxOk = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APITXOK_OFFS);
            echo.u16ApiTxErr = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APITXERR_OFFS);
            echo.u16ApiTxFail = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APITXFAIL_OFFS);
            echo.u16ApiRxOk = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APIRXOK_OFFS);
            echo.u16ApiRxProtErr = DataTypeConverter.MeshByteArrToUInt16(payload, GETMANAGERSTATISTICS_REPLY_APIRXPROTERR_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetManagerStatistics OK, SerTxCnt(" + echo.u16SerTxCnt.ToString() +
                    ")|SerRxCnt(" + echo.u16SerRxCnt.ToString() +
                    ")|SerRxCRCErr(" + echo.u16SerRxCRCErr.ToString() +
                    ")|SerRxOverruns(" + echo.u16SerRxOverruns.ToString() +
                    ")|ApiEstabConn(" + echo.u16ApiEstabConn.ToString() +
                    ")|ApiDroppedConn(" + echo.u16ApiDroppedConn.ToString() +
                    ")|ApiTxOk(" + echo.u16ApiTxOk.ToString() +
                    ")|ApiTxErr(" + echo.u16ApiTxErr.ToString() +
                    ")|ApiTxFail(" + echo.u16ApiTxFail.ToString() +
                    ")|ApiRxOk(" + echo.u16ApiRxOk.ToString() +
                    ")|ApiRxProtErr(" + echo.u16ApiRxProtErr.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetManagerStatistics RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETTIME_REPLY_LEN = 0;
        public void SetTimeReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETTIME_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetTime reply payload too short");
                return;
            }

            tSetTimeEcho echo = new tSetTimeEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetTime RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetTime OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETLICENSE_REPLY_LICENSE_OFFS = 0;
        private const byte GETLICENSE_REPLY_LEN = 13;
        public void GetLicenseReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETLICENSE_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLicense reply payload too short");
                return;
            }

            tGetLicenseEcho echo = new tGetLicenseEcho();
            echo.RC = rc;
            Array.Copy(payload, GETLICENSE_REPLY_LICENSE_OFFS, echo.License.u8aData, 0, tLICENSE.LEN);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLicense OK, License(" + echo.License.ToHexString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetLicense RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETLICENSE_REPLY_LEN = 0;
        public void SetLicenseReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETLICENSE_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetLicense reply payload too short");
                return;
            }

            tSetLicenseEcho echo = new tSetLicenseEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetLicense RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetLicense OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETCLIUSER_REPLY_LEN = 0;
        public void SetUserReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETCLIUSER_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetUser reply payload too short");
                return;
            }

            tSetCLIUserEcho echo = new tSetCLIUserEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetUser RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetUser OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SENDIP_REPLY_CALLBACKID_OFFS = 0;
        private const byte SENDIPA_REPLY_LEN = SENDIP_REPLY_CALLBACKID_OFFS + sizeof(UInt32);
        public void SendIPReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SENDIPA_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SendIP reply payload too short");
                return;
            }

            tSendIPEcho echo = new tSendIPEcho();
            echo.RC = rc;
            echo.u32CallbackId = DataTypeConverter.MeshByteArrToUInt32(payload, SENDIP_REPLY_CALLBACKID_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SendIP OK, CallbackId: " + echo.u32CallbackId.ToString());
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SendIP RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte RESTOREFACTORYDEFAULTS_REPLY_LEN = 0;
        public void RestoreFactoryDefaultsReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < RESTOREFACTORYDEFAULTS_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "RestoreFactoryDefaults reply payload too short");
                return;
            }

            tRestoreFactoryDefaultsEcho echo = new tRestoreFactoryDefaultsEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "RestoreFactoryDefaults RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "RestoreFactoryDefaults OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETMOTEINFO_REPLY_MACADDR_OFFS = 0;
        private const byte GETMOTEINFO_REPLY_STATE_OFFS = 8;
        private const byte GETMOTEINFO_REPLY_NUMNBRS_OFFS = 9;
        private const byte GETMOTEINFO_REPLY_NUMGOODNBRS_OFFS = 10;
        private const byte GETMOTEINFO_REPLY_REQUESTEDBW_OFFS = 11;
        private const byte GETMOTEINFO_REPLY_TOTALNEEDEDBW_OFFS = 15;
        private const byte GETMOTEINFO_REPLY_ASSIGNEDBW_OFFS = 19;
        private const byte GETMOTEINFO_REPLY_PACKETSRECEIVED_OFFS = 23;
        private const byte GETMOTEINFO_REPLY_PACKETSLOST_OFFS = 27;
        private const byte GETMOTEINFO_REPLY_AVGLATENCY_OFFS = 31;
        private const byte GETMOTEINFO_REPLY_LEN = 35;
        public void GetMoteInfoReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETMOTEINFO_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteInfo reply payload too short");
                return;
            }

            tGetMoteInfoEcho echo = new tGetMoteInfoEcho();
            echo.RC = rc;
            Array.Copy(payload, GETMOTEINFO_REPLY_MACADDR_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
            echo.State = (enMoteState)payload[GETMOTEINFO_REPLY_STATE_OFFS];
            echo.u8NumNbrs = payload[GETMOTEINFO_REPLY_NUMNBRS_OFFS];
            echo.u8NumGoodNbrs = payload[GETMOTEINFO_REPLY_NUMGOODNBRS_OFFS];
            echo.u32RequestedBw = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_REQUESTEDBW_OFFS);
            echo.u32TotalNeededBw = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_TOTALNEEDEDBW_OFFS);
            echo.u32AssignedBw = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_ASSIGNEDBW_OFFS);
            echo.u32PacketsReceived = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_PACKETSRECEIVED_OFFS);
            echo.u32PacketsLost = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_PACKETSLOST_OFFS);
            echo.u32AvgLatency = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTEINFO_REPLY_AVGLATENCY_OFFS);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteInfo OK, Mote(" + echo.mac.ToHexString() +
                    ")|State: " + echo.State.ToString() +
                    ")|NumNbrs: " + echo.u8NumNbrs.ToString() +
                    ")|NumGoodNbrs: " + echo.u8NumGoodNbrs.ToString() +
                    ")|RequestedBw: " + echo.u32RequestedBw.ToString() +
                    ")|TotalNeededBw: " + echo.u32TotalNeededBw.ToString() +
                    ")|AssignedBw: " + echo.u32AssignedBw.ToString() +
                    ")|PacketsReceived: " + echo.u32PacketsReceived.ToString() +
                    ")|PacketsLost: " + echo.u32PacketsLost.ToString() +
                    ")|AvgLatency: " + echo.u32AvgLatency.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteInfo RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo, echo.mac);
        }

        private const byte GETNETWORKCONFIG_REPLY_NETWORKID_OFFS = 0;
        private const byte GETNETWORKCONFIG_REPLY_APTXPOWER_OFFS = 2;
        private const byte GETNETWORKCONFIG_REPLY_FRAMEPROFILE_OFFS = 3;
        private const byte GETNETWORKCONFIG_REPLY_MAXMOTES_OFFS = 4;
        private const byte GETNETWORKCONFIG_REPLY_BASEBW_OFFS = 6;
        private const byte GETNETWORKCONFIG_REPLY_DNFRAMEMULTVAL_OFFS = 8;
        private const byte GETNETWORKCONFIG_REPLY_NUMPARENTS_OFFS = 9;
        private const byte GETNETWORKCONFIG_REPLY_CCAMODE_OFFS = 10;
        private const byte GETNETWORKCONFIG_REPLY_CHANNELLIST_OFFS = 11;
        private const byte GETNETWORKCONFIG_REPLY_AUTOSTARTNETWORK_OFFS = 13;
        private const byte GETNETWORKCONFIG_REPLY_IOCMODE_OFFS = 14;
        private const byte GETNETWORKCONFIG_REPLY_BBMODE_OFFS = 15;
        private const byte GETNETWORKCONFIG_REPLY_BBSIZE_OFFS = 16;
        private const byte GETNETWORKCONFIG_REPLY_ISRADIOTEST_OFFS = 17;
        private const byte GETNETWORKCONFIG_REPLY_BWMULT_OFFS = 18;
        private const byte GETNETWORKCONFIG_REPLY_ONECHANNEL_OFFS = 20;
        private const byte GETNETWORKCONFIG_REPLY_LEN = 21;
        public void GetNetworkConfigReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETNETWORKCONFIG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkConfig reply payload too short");
                return;
            }

            tGetNetworkConfigEcho echo = new tGetNetworkConfigEcho();
            echo.RC = rc;
            echo.u16NetworkId = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKCONFIG_REPLY_NETWORKID_OFFS);
            echo.s8ApTxPower = (sbyte)payload[GETNETWORKCONFIG_REPLY_APTXPOWER_OFFS];
            echo.frmProfile = (enFrameProfile)payload[GETNETWORKCONFIG_REPLY_FRAMEPROFILE_OFFS];
            echo.u16MaxMotes = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKCONFIG_REPLY_MAXMOTES_OFFS);
            echo.u16BaseBandwidth = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKCONFIG_REPLY_BASEBW_OFFS);
            echo.u8DownFrameMultVal = payload[GETNETWORKCONFIG_REPLY_DNFRAMEMULTVAL_OFFS];
            echo.u8NumParents = payload[GETNETWORKCONFIG_REPLY_NUMPARENTS_OFFS];
            echo.ccaMode = (enCcaMode)payload[GETNETWORKCONFIG_REPLY_CCAMODE_OFFS];
            echo.u16ChannelList = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKCONFIG_REPLY_CHANNELLIST_OFFS);
            echo.bAutoStartNetwork = (payload[GETNETWORKCONFIG_REPLY_AUTOSTARTNETWORK_OFFS] == 0) ? false : true;
            echo.u8LocMode = payload[GETNETWORKCONFIG_REPLY_IOCMODE_OFFS];
            echo.bbMode = (enBBMode)payload[GETNETWORKCONFIG_REPLY_BBMODE_OFFS];
            echo.u8BbSize = payload[GETNETWORKCONFIG_REPLY_BBSIZE_OFFS];
            echo.isRadioTest = (payload[GETNETWORKCONFIG_REPLY_ISRADIOTEST_OFFS] == 0) ? false : true;
            echo.u16BwMult = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKCONFIG_REPLY_BWMULT_OFFS);
            echo.u8OneChannel = payload[GETNETWORKCONFIG_REPLY_ONECHANNEL_OFFS];

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkConfig OK, NetworkId(" + echo.u16NetworkId.ToString() +
                    ")|ApTxPower(" + echo.s8ApTxPower.ToString() +
                    ")|frmProfile(" + echo.frmProfile.ToString() +
                    ")|MaxMotes(" + echo.u16MaxMotes.ToString() +
                    ")|BaseBandwidth(" + echo.u16BaseBandwidth.ToString() +
                    ")|DownFrameMultVal(" + echo.u8DownFrameMultVal.ToString() +
                    ")|NumParents(" + echo.u8NumParents.ToString() +
                    ")|ccaMode(" + echo.ccaMode.ToString() +
                    ")|ChannelList(" + echo.u16ChannelList.ToString() +
                    ")|AutoStartNetwork(" + echo.bAutoStartNetwork.ToString() +
                    ")|LocMode(" + echo.u8LocMode.ToString() +
                    ")|bbMode(" + echo.bbMode.ToString() +
                    ")|BbSize(" + echo.u8BbSize.ToString() +
                    ")|isRadioTest(" + echo.isRadioTest.ToString() +
                    ")|BwMult(" + echo.u16BwMult.ToString() +
                    ")|OneChannel(" + echo.u8OneChannel.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkConfig RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETNETWORKINFO_REPLY_NUMMOTES_OFFS = 0;
        private const byte GETNETWORKINFO_REPLY_ASNSIZE_OFFS = 2;
        private const byte GETNETWORKINFO_REPLY_ADVSTATE_OFFS = 4;
        private const byte GETNETWORKINFO_REPLY_DNFRAMESTATE_OFFS = 5;
        private const byte GETNETWORKINFO_REPLY_NETRELIABILITY_OFFS = 6;
        private const byte GETNETWORKINFO_REPLY_NETPATHSTABILITY_OFFS = 7;
        private const byte GETNETWORKINFO_REPLY_NETLATENCY_OFFS = 8;
        private const byte GETNETWORKINFO_REPLY_NETSTATE_OFFS = 12;
        private const byte GETNETWORKINFO_REPLY_IPV6ADDR_OFFS = 13;
        #region Manager固件版本V1.3.0中支持字段
        private const byte GETNETWORKINFO_REPLY_NUMLOSTPACKETS_OFFS = 29;
        private const byte GETNETWORKINFO_REPLY_NUMARRIVEDPACKETS_OFFS = 33;
        private const byte GETNETWORKINFO_REPLY_MAXNUMHOOPS_OFFS = 41;
        #endregion Manager固件版本V1.3.0中支持字段
        private const byte GETNETWORKINFO_REPLY_LEN = 29;
        public void GetNetworkInfoReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETNETWORKINFO_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkInfo reply payload(" + len + ") too short");
                return;
            }

            tGetNetworkInfoEcho echo = new tGetNetworkInfoEcho();
            echo.RC = rc;
            echo.u16NumMotes = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKINFO_REPLY_NUMMOTES_OFFS);
            echo.u16AsnSize = DataTypeConverter.MeshByteArrToUInt16(payload, GETNETWORKINFO_REPLY_ASNSIZE_OFFS);
            echo.advState = (enAdvState)payload[GETNETWORKINFO_REPLY_ADVSTATE_OFFS];
            echo.dnfrmMode = (enDnstreamFrameMode)payload[GETNETWORKINFO_REPLY_DNFRAMESTATE_OFFS];
            echo.u8NetReliability = payload[GETNETWORKINFO_REPLY_NETRELIABILITY_OFFS];
            echo.u8NetPathStability = payload[GETNETWORKINFO_REPLY_NETPATHSTABILITY_OFFS];
            echo.u32NetLatency = DataTypeConverter.MeshByteArrToUInt32(payload, GETNETWORKINFO_REPLY_NETLATENCY_OFFS);
            echo.netState = (enNetState)payload[GETNETWORKINFO_REPLY_NETSTATE_OFFS];
            Array.Copy(payload, GETNETWORKINFO_REPLY_IPV6ADDR_OFFS, echo.ipv6Address.u8aData, 0, tIPV6ADDR.LEN);
            #if false
            #region Manager固件版本V1.3.0中支持字段
            echo.u32NumLostPackets = DataTypeConverter.MeshByteArrToUInt32(payload, GETNETWORKINFO_REPLY_NUMLOSTPACKETS_OFFS);
            echo.u64NumArrivedPackets = DataTypeConverter.MeshByteArrToUInt64(payload, GETNETWORKINFO_REPLY_NUMARRIVEDPACKETS_OFFS);
            echo.u8MaxNumbHops = payload[GETNETWORKINFO_REPLY_MAXNUMHOOPS_OFFS];
            #endregion Manager固件版本V1.3.0中支持字段
            #endif
            if (echo.RC == eRC.RC_OK)
            {
                // 打印信息
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkInfo OK, NumMotes(" + echo.u16NumMotes.ToString() +
                    ")|AsnSize(" + echo.u16AsnSize.ToString() +
                    ")|advState(" + echo.advState.ToString() +
                    ")|dnfrmMode(" + echo.dnfrmMode.ToString() +
                    ")|NetReliability(" + echo.u8NetReliability.ToString() +
                    ")|NetPathStability(" + echo.u8NetPathStability.ToString() +
                    ")|NetLatency(" + echo.u32NetLatency.ToString() +
                    ")|netState(" + echo.netState.ToString() +
                    ")|ipv6Address(" + echo.ipv6Address.ToHexString() +
                    ")|NumLostPackets(" + echo.u32NumLostPackets.ToString() +
                    ")|NumArrivedPackets(" + echo.u64NumArrivedPackets.ToString() +
                    ")|MaxNumbHops(" + echo.u8MaxNumbHops.ToString() + ")");
            }
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkInfo RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETMOTECONFIGBYID_REPLY_MACADDR_OFFS = 0;
        private const byte GETMOTECONFIGBYID_REPLY_MOTEID_OFFS = 8;
        private const byte GETMOTECONFIGBYID_REPLY_ISAP_OFFS = 10;
        private const byte GETMOTECONFIGBYID_REPLY_STATE_OFFS = 11;
        private const byte GETMOTECONFIGBYID_REPLY_REV_OFFS = 12;
        private const byte GETMOTECONFIGBYID_REPLY_ISROUTE_OFFS = 13;
        private const byte GETMOTECONFIGBYID_REPLY_LEN = 14;
        public void GetMoteConfigByIdReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETMOTECONFIGBYID_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfigById reply payload too short");
                return;
            }

            tGetMoteConfigByIdEcho echo = new tGetMoteConfigByIdEcho();
            echo.RC = rc;
            Array.Copy(payload, GETMOTECONFIGBYID_REPLY_MACADDR_OFFS, echo.mac.u8aData, 0, tMAC.LEN);
            echo.u16MoteId = DataTypeConverter.MeshByteArrToUInt16(payload, GETMOTECONFIGBYID_REPLY_MOTEID_OFFS);
            echo.isAP = (payload[GETMOTECONFIGBYID_REPLY_ISAP_OFFS] == 0) ? false : true;
            echo.state = (enMoteState)payload[GETMOTECONFIGBYID_REPLY_STATE_OFFS];
            echo.u8Reserved = payload[GETMOTECONFIGBYID_REPLY_REV_OFFS];
            echo.isRouting = (payload[GETMOTECONFIGBYID_REPLY_ISROUTE_OFFS] == 0) ? false : true;

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfigById OK, Mote(" + echo.mac.ToHexString() +
                    ")|uMoteId(" + echo.u16MoteId.ToString() +
                    ")|isAP(" + echo.isAP.ToString() +
                    ")|state(" + echo.state.ToString() +
                    ")|isRouting(" + echo.isRouting.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfigById RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo, echo.mac);
        }

        private const byte SETCOMMONJOINKEY_REPLY_LEN = 0;
        public void SetCommonJoinKeyReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETCOMMONJOINKEY_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetCommonJoinKey reply payload too short");
                return;
            }

            tSetCommonJoinKeyEcho echo = new tSetCommonJoinKeyEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetCommonJoinKey RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetCommonJoinKey OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETIPCONFIG_REPLY_IPV6ADDR_OFFS = 0;
        private const byte GETIPCONFIG_REPLY_MASK_OFFS = GETIPCONFIG_REPLY_IPV6ADDR_OFFS + tIPV6ADDR.LEN;
        private const byte GETIPCONFIG_REPLY_LEN = GETIPCONFIG_REPLY_MASK_OFFS + tIPV6MASK.LEN;
        public void GetIPConfigReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETIPCONFIG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetIPConfig reply payload too short");
                return;
            }

            tGetIPConfigEcho echo = new tGetIPConfigEcho();
            echo.RC = rc;
            Array.Copy(payload, GETIPCONFIG_REPLY_IPV6ADDR_OFFS, echo.ipv6Address.u8aData, 0, tIPV6ADDR.LEN);
            Array.Copy(payload, GETIPCONFIG_REPLY_MASK_OFFS, echo.ipv6Mask.u8aData, 0, tIPV6MASK.LEN);

            if (echo.RC == eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetIPConfig OK, ipv6Address(" + echo.ipv6Address.ToHexString() +
                    ")|ipv6Mask(" + echo.ipv6Mask.ToString() + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetIPConfig RC(" + echo.RC + ")");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte SETIPCONFIG_REPLY_LEN = 0;
        public void SetIPConfigReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < SETIPCONFIG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetIPConfig reply payload too short");
                return;
            }

            tSetIPConfigEcho echo = new tSetIPConfigEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetIPConfig RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "SetIPConfig OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte DELETEMOTE_REPLY_LEN = 0;
        public void DeleteMoteReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < DELETEMOTE_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteMote reply payload too short");
                return;
            }

            tDeleteMoteEcho echo = new tDeleteMoteEcho();
            echo.RC = rc;

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteMote RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteMote OK");

            ShearedReplyCB(cmdId, echo);
        }

        private const byte GETMOTELINKS_REPLY_IDX_OFFS = 0;
        private const byte GETMOTELINKS_REPLY_UTILIZATION_OFFS = GETMOTELINKS_REPLY_IDX_OFFS + sizeof(UInt16);
        private const byte GETMOTELINKS_REPLY_NUMLINKS_OFFS = GETMOTELINKS_REPLY_UTILIZATION_OFFS + sizeof(byte);
        private const byte GETMOTELINKS_REPLY_LINKS_OFFS = GETMOTELINKS_REPLY_NUMLINKS_OFFS + sizeof(byte);
        private const byte GETMOTELINKS_REPLY_LEN = GETIPCONFIG_REPLY_MASK_OFFS + tIPV6MASK.LEN;
        public void GetMoteLinksReplyCB(enCmd cmdId, eRC rc, byte[] payload, byte len)
        {
            if (!m_bBusyTx || cmdId != m_u8CmdId)
                return;
            if (len < GETIPCONFIG_REPLY_LEN)
            {
                CancelTx();
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteLinks reply payload too short");
                return;
            }

            tGetMoteLinksEcho echo = new tGetMoteLinksEcho();
            echo.RC = rc;
            echo.u16Idx = DataTypeConverter.MeshByteArrToUInt16(payload, GETMOTELINKS_REPLY_IDX_OFFS);
            echo.u8Utilization = payload[GETMOTELINKS_REPLY_UTILIZATION_OFFS];
            echo.u8NumLinks = payload[GETMOTELINKS_REPLY_NUMLINKS_OFFS];

            for (int i = 0; i < echo.u8NumLinks; i++)
            {
                echo.links[i].u8FrameId = payload[GETMOTELINKS_REPLY_LINKS_OFFS + i * 9];
                echo.links[i].u32Slot = DataTypeConverter.MeshByteArrToUInt32(payload, GETMOTELINKS_REPLY_LINKS_OFFS + i * 9 + 1);
                echo.links[i].u8ChannelOffset = payload[GETMOTELINKS_REPLY_LINKS_OFFS + i * 9 + 5];
                echo.links[i].u16MoteId = DataTypeConverter.MeshByteArrToUInt16(payload, GETMOTELINKS_REPLY_LINKS_OFFS + i * 9 + 6);
                echo.links[i].Flags = (enLinkFlags)payload[GETMOTELINKS_REPLY_LINKS_OFFS + i * 9 + 8];
            }

            if (echo.RC != eRC.RC_OK)
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteLinks RC(" + echo.RC + ")");
            else
                CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteLinks OK");

            ShearedReplyCB(cmdId, echo);
        }

        #endregion
    }
}
