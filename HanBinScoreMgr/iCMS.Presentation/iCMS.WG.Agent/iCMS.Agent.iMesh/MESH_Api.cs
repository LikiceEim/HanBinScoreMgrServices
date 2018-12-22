using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    internal partial class MESH
    {
        #region meshAPI请求宏定义
        private const byte RESET_TYPE_OFFS = 0;
        private const byte RESET_MACADDR_OFFS = RESET_TYPE_OFFS + sizeof(byte);
        private const byte RESET_PAYLOAD_LEN = RESET_MACADDR_OFFS + tMAC.LEN;
        private const byte SUB_FILTER_OFFS = 0;
        private const byte SUB_UNACKFILTER_OFFS = SUB_FILTER_OFFS + sizeof(UInt32);
        private const byte SUB_PAYLOAD_LEN = SUB_UNACKFILTER_OFFS + sizeof(UInt32);
        private const byte GETTIME_PAYLOAD_LEN = 0;
        private const byte SETNTCFG_NTID_OFFS = 0;
        private const byte SETNTCFG_APTXPOWER_OFFS = SETNTCFG_NTID_OFFS + sizeof(UInt16);
        private const byte SETNTCFG_FRMPROFILE_OFFS = SETNTCFG_APTXPOWER_OFFS + sizeof(byte);
        private const byte SETNTCFG_MAXMOTES_OFFS = SETNTCFG_FRMPROFILE_OFFS + sizeof(byte);
        private const byte SETNTCFG_BASEBW_OFFS = SETNTCFG_MAXMOTES_OFFS + sizeof(UInt16);
        private const byte SETNTCFG_DOWNFRMMULTVAL_OFFS = SETNTCFG_BASEBW_OFFS + sizeof(UInt16);
        private const byte SETNTCFG_NUMPARENTS_OFFS = SETNTCFG_DOWNFRMMULTVAL_OFFS + sizeof(byte);
        private const byte SETNTCFG_CCAMODE_OFFS = SETNTCFG_NUMPARENTS_OFFS + sizeof(byte);
        private const byte SETNTCFG_CHANLIST_OFFS = SETNTCFG_CCAMODE_OFFS + sizeof(byte);
        private const byte SETNTCFG_AUTOSTARTNT_OFFS = SETNTCFG_CHANLIST_OFFS + sizeof(UInt16);
        private const byte SETNTCFG_IOMODE_OFFS = SETNTCFG_AUTOSTARTNT_OFFS + sizeof(byte);
        private const byte SETNTCFG_BBMODE_OFFS = SETNTCFG_IOMODE_OFFS + sizeof(byte);
        private const byte SETNTCFG_BBSIZE_OFFS = SETNTCFG_BBMODE_OFFS + sizeof(byte);
        private const byte SETNTCFG_ISRADIOTEST_OFFS = SETNTCFG_BBSIZE_OFFS + sizeof(byte);
        private const byte SETNTCFG_BWMULT_OFFS = SETNTCFG_ISRADIOTEST_OFFS + sizeof(byte);
        private const byte SETNTCFG_ONECHAN_OFFS = SETNTCFG_BWMULT_OFFS + sizeof(UInt16);
        private const byte SETNTCFG_PAYLOAD_LEN = SETNTCFG_ONECHAN_OFFS + sizeof(byte);
        private const byte CLEARSTATISTICS_PAYLOAD_LEN = 0;
        private const byte EXCHMOTEJK_MAC_OFFS = 0;
        private const byte EXCHMOTEJK_KEY_OFFS = EXCHMOTEJK_MAC_OFFS + tMAC.LEN;
        private const byte EXCHMOTEJK_PAYLOAD_LEN = EXCHMOTEJK_KEY_OFFS + tSECKEY.LEN;
        private const byte EXCHNTID_ID_OFFS = 0;
        private const byte EXCHNTID_PAYLOAD_LEN = EXCHNTID_ID_OFFS + sizeof(UInt16);
        private const byte RADIOTESTTX_MAX_SEQSIZE = 10;
        private const byte RADIOTESTTX_TESTTYPE_OFFS = 0;
        private const byte RADIOTESTTX_CHANMASK_OFFS = RADIOTESTTX_TESTTYPE_OFFS + sizeof(byte);
        private const byte RADIOTESTTX_REPEATCNT_OFFS = RADIOTESTTX_CHANMASK_OFFS + sizeof(UInt16);
        private const byte RADIOTESTTX_TXPOWER_OFFS = RADIOTESTTX_REPEATCNT_OFFS + sizeof(UInt16);
        private const byte RADIOTESTTX_SEQSIZE_OFFS = RADIOTESTTX_TXPOWER_OFFS + sizeof(sbyte);
        private const byte RADIOTESTTX_SEQDEF_OFFS = RADIOTESTTX_SEQSIZE_OFFS + sizeof(byte);
        private const byte RADIOTESTTX_STATIONID_OFFS = RADIOTESTTX_SEQDEF_OFFS + RADIOTESTTX_MAX_SEQSIZE * 3;
        private const byte RADIOTESTTX_PAYLOAD_LEN = RADIOTESTTX_STATIONID_OFFS + sizeof(byte);
        private const byte RADIOTESTRX_CHANMASK_OFFS = 0;
        private const byte RADIOTESTRX_DURATION_OFFS = RADIOTESTRX_CHANMASK_OFFS + sizeof(UInt16);
        private const byte RADIOTESTRX_STATIONID_OFFS = RADIOTESTRX_DURATION_OFFS + sizeof(UInt16);
        private const byte RADIOTESTRX_PAYLOAD_LEN = RADIOTESTRX_STATIONID_OFFS + sizeof(byte);
        private const byte GETRADIOTESTSTATICS_PAYLOAD_LEN = 0;
        private const byte SETACLENTRY_MACADDR_OFFS = 0;
        private const byte SETACLENTRY_JOINKEY_OFFS = SETACLENTRY_MACADDR_OFFS + tMAC.LEN;
        private const byte SETACLENTRY_PAYLOAD_LEN = SETACLENTRY_JOINKEY_OFFS + tSECKEY.LEN;
        private const byte GETNEXTACLENTRY_MACADDR_OFFS = 0;
        private const byte GETNEXTACLENTRY_PAYLOAD_LEN = GETNEXTACLENTRY_MACADDR_OFFS + tMAC.LEN;
        private const byte DELETEACLENTRY_MACADDR_OFFS = 0;
        private const byte DELETEACLENTRY_PAYLOAD_LEN = DELETEACLENTRY_MACADDR_OFFS + tMAC.LEN;
        private const byte PINGMOTE_MACADDR_OFFS = 0;
        private const byte PINGMOTE_PAYLOAD_LEN = PINGMOTE_MACADDR_OFFS + tMAC.LEN;
        private const byte GETLOG_MACADDR_OFFS = 0;
        private const byte GETLOG_PAYLOAD_LEN = GETLOG_MACADDR_OFFS + tMAC.LEN;
        private const byte SENDDATA_MACADDR_OFFS = 0;
        private const byte SENDDATA_PRIORITY_OFFS = SENDDATA_MACADDR_OFFS + tMAC.LEN;
        private const byte SENDDATA_SRCPORT_OFFS = SENDDATA_PRIORITY_OFFS + sizeof(byte);
        private const byte SENDDATA_DSTPORT_OFFS = SENDDATA_SRCPORT_OFFS + sizeof(UInt16);
        private const byte SENDDATA_OPTIONS_OFFS = SENDDATA_DSTPORT_OFFS + sizeof(UInt16);
        private const byte SENDDATA_DATA_OFFS = SENDDATA_OPTIONS_OFFS + sizeof(byte);
        private const byte SENDDATA_PAYLOAD_LEN = SENDDATA_DATA_OFFS;
        private const byte STARTNETWORK_PAYLOAD_LEN = 0;
        private const byte GETSYSTEMINFO_PAYLOAD_LEN = 0;
        private const byte GETMOTECONFIG_MACADDR_OFFS = 0;
        private const byte GETMOTECONFIG_NEXT_OFFS = GETMOTECONFIG_MACADDR_OFFS + tMAC.LEN;
        private const byte GETMOTECONFIG_PAYLOAD_LEN = GETMOTECONFIG_NEXT_OFFS + sizeof(byte);
        private const byte GETPATHINFO_SRCMACADDR_OFFS = 0;
        private const byte GETPATHINFO_DSTMACADDR_OFFS = GETPATHINFO_SRCMACADDR_OFFS + tMAC.LEN;
        private const byte GETPATHINFO_PAYLOAD_LEN = GETPATHINFO_DSTMACADDR_OFFS + tMAC.LEN;
        private const byte GETNEXTPATHINFO_MACADDR_OFFS = 0;
        private const byte GETNEXTPATHINFO_FILTER_OFFS = GETNEXTPATHINFO_MACADDR_OFFS + tMAC.LEN;
        private const byte GETNEXTPATHINFO_PATHID_OFFS = GETNEXTPATHINFO_FILTER_OFFS + sizeof(byte);
        private const byte GETNEXTPATHINFO_PAYLOAD_LEN = GETNEXTPATHINFO_PATHID_OFFS + sizeof(UInt16);
        private const byte SETADVERTISING_ACTIVATE_OFFS = 0;
        private const byte SETADVERTISING_PAYLOAD_LEN = SETADVERTISING_ACTIVATE_OFFS + sizeof(byte);
        private const byte SETDOWNSTREAMFRAMEMODE_FRAMEMODE_OFFS = 0;
        private const byte SETDOWNSTREAMFRAMEMODE_PAYLOAD_LEN = SETDOWNSTREAMFRAMEMODE_FRAMEMODE_OFFS + sizeof(byte);
        private const byte GETMANAGERSTATISTICS_PAYLOAD_LEN = 0;
        private const byte SETTIME_TRIGGER_OFFS = 0;
        private const byte SETTIME_UTCSECS_OFFS = SETTIME_TRIGGER_OFFS + sizeof(byte);
        private const byte SETTIME_UTCMSECS_OFFS = SETTIME_UTCSECS_OFFS + sizeof(UInt64);
        private const byte SETTIME_PAYLOAD_LEN = SETTIME_UTCMSECS_OFFS + sizeof(UInt32);
        private const byte GETLICENSE_PAYLOAD_LEN = 0;
        private const byte SETLICENSE_LICENSE_OFFS = 0;
        private const byte SETLICENSE_PAYLOAD_LEN = SETLICENSE_LICENSE_OFFS + tLICENSE.LEN;
        private const byte SETCLIUSER_ROLE_OFFS = 0;
        private const byte SETCLIUSER_PASSWORD_OFFS = SETCLIUSER_ROLE_OFFS + sizeof(byte);
        private const byte SETCLIUSER_PAYLOAD_LEN = SETCLIUSER_PASSWORD_OFFS + tUSERPW.LEN;
        private const byte SENDIP_MACADDR_OFFS = 0;
        private const byte SENDIP_PRIORITY_OFFS = SENDDATA_MACADDR_OFFS + tMAC.LEN;
        private const byte SENDIP_OPTIONS_OFFS = SENDDATA_DSTPORT_OFFS + sizeof(byte);
        private const byte SENDIP_ENCRYPTEDOFFSET_OFFS = SENDIP_OPTIONS_OFFS + sizeof(byte);
        private const byte SENDIP_DATA_OFFS = SENDIP_ENCRYPTEDOFFSET_OFFS + sizeof(byte);
        private const byte SENDIP_PAYLOAD_LEN = SENDIP_DATA_OFFS;
        private const byte RESTOREFACTORYDEFAULTS_PAYLOAD_LEN = 0;
        private const byte GETMOTEINFO_MACADDR_OFFS = 0;
        private const byte GETMOTEINFO_PAYLOAD_LEN = GETMOTEINFO_MACADDR_OFFS + tMAC.LEN;
        private const byte GETNETWORKCONFIG_PAYLOAD_LEN = 0;
        private const byte GETNETWORKINFO_PAYLOAD_LEN = 0;
        private const byte GETMOTECONFIGBYID_MOTEID_OFFS = 0;
        private const byte GETMOTECONFIGBYID_PAYLOAD_LEN = GETMOTECONFIGBYID_MOTEID_OFFS + sizeof(UInt64);
        private const byte SETCOMMONJOINKEY_KEY_OFFS = 0;
        private const byte SETCOMMONJOINKEY_PAYLOAD_LEN = SETCOMMONJOINKEY_KEY_OFFS + tSECKEY.LEN;
        private const byte GETIPCONFIG_PAYLOAD_LEN = 0;
        private const byte SETIPCONFIG_IPV6ADDR_OFFS = 0;
        private const byte SETIPCONFIG_MASK_OFFS = SETIPCONFIG_IPV6ADDR_OFFS + tIPV6ADDR.LEN;
        private const byte SETIPCONFIG_PAYLOAD_LEN = SETIPCONFIG_MASK_OFFS + tIPV6MASK.LEN;
        private const byte DELETEMOTE_MACADDR_OFFS = 0;
        private const byte DELETEMOTE_PAYLOAD_LEN = DELETEMOTE_MACADDR_OFFS + tMAC.LEN;
        private const byte GETMOTELINKS_MACADDR_OFFS = 0;
        private const byte GETMOTELINKS_IDX_OFFS = GETMOTELINKS_MACADDR_OFFS + tMAC.LEN;
        private const byte GETMOTELINKS_PAYLOAD_LEN = GETMOTELINKS_IDX_OFFS + sizeof(UInt16);
        #endregion meshAPI请求宏定义
        /// <summary>
        /// 用户请求处理函数
        /// </summary>
        /// <param name="newReq">用户请求元素</param>
        /// <param name="bRetry">表示是否重试用户请求</param>
        /// [Modify] by Kous in 2017.2.11
        /// ① 放开lock
        /// ② 将ResetWaitResponse位置提前到下发命令之前
        /// ③ 将返回码判断放在lock中
        /// ④ 添加代码及时清除m_bStressfull
        /// <returns>处理错误码</returns>
        public enErrCode MeshAPI(UserRequestElement newReq, bool bRetry = false)
        {
            if (newReq == null)
                return enErrCode.ERR_INVALID_PARAM;

            enErrCode RC = enErrCode.ERR_NONE;
            lock (m_MeshLockObj)
            {
                // 如果已经有请求在处理，在收到应答之前不进行新的请求
                if (m_bBusyTx)
                    return enErrCode.ERR_BUSY;

                // 缓存新请求命令码，以便在收到响应后验证应答正确性
                m_u8CmdId = newReq.cmd;

                ResetWaitResponse();
                switch (newReq.cmd)
                {
                    #region CMDID_RESET
                    case enCmd.CMDID_RESET:
                    {
                        if (newReq.param == null)
                        {
                            m_u8aOutputBuf[RESET_TYPE_OFFS] = (byte)enResetType.System;
                            Array.Clear(m_u8aOutputBuf, RESET_MACADDR_OFFS, tMAC.LEN);
                            m_u8OutputBufLen = RESET_PAYLOAD_LEN;
                            /*
                             * 经过测试，Agent调用ResetSystem命令后，Manager是不会给Agent返回响应信息，直接重启
                             * 顾此处不能调用m_bBusyTx = true进行赋值操作，因为ResetSystem没有响应顾m_bBusyTx得
                             * 不到释放，导致其他命令无法处理
                             * 后期经过测试，以上三行说明可能与CLI的波特率有关系，如果CLI的波特率提高后，有可能
                             * 会导致Reset system无响应
                             */
                            if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, ResetReplyCB, bRetry)))
                                m_bBusyTx = true;
                        }
                        else
                        {
                            tMAC param = (tMAC)newReq.param;

                            // 记录当前命令目标ws
                            m_macCurTarget = param;

                            m_u8aOutputBuf[RESET_TYPE_OFFS] = (byte)enResetType.Mote;
                            Array.Copy(param.u8aData, 0, m_u8aOutputBuf, RESET_MACADDR_OFFS, tMAC.LEN);
                            m_u8OutputBufLen = RESET_PAYLOAD_LEN;

                            if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, ResetReplyCB, bRetry)))
                                m_bBusyTx = true;
                        }

                        break;
                    }
                    #endregion
                    #region CMDID_SUBSCRIBE
                    case enCmd.CMDID_SUBSCRIBE:
                    {
                        tSUBS param = (tSUBS)newReq.param;
                        DataTypeConverter.UInt32ToMeshByteArr(param.u32Filter, m_u8aOutputBuf, SUB_FILTER_OFFS);
                        DataTypeConverter.UInt32ToMeshByteArr(param.u32UnackFilter, m_u8aOutputBuf, SUB_UNACKFILTER_OFFS);
                        m_u8OutputBufLen = SUB_PAYLOAD_LEN;

                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SubscribeReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETTIME
                    case enCmd.CMDID_GETTIME:
                    {
                        m_u8OutputBufLen = GETTIME_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetTime");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetTimeReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETNETWORKCONFIG
                    case enCmd.CMDID_SETNETWORKCONFIG:
                    {
                        tNETWORKCONFIG param = (tNETWORKCONFIG)newReq.param;

                        DataTypeConverter.UInt16ToMeshByteArr(param.u16NetworkId, m_u8aOutputBuf, SETNTCFG_NTID_OFFS);
                        m_u8aOutputBuf[SETNTCFG_APTXPOWER_OFFS] = (byte)param.s8ApTxPower;
                        m_u8aOutputBuf[SETNTCFG_FRMPROFILE_OFFS] = (byte)param.frmProfile;
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16MaxMotes, m_u8aOutputBuf, SETNTCFG_MAXMOTES_OFFS);
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16BaseBandwidth, m_u8aOutputBuf, SETNTCFG_BASEBW_OFFS);
                        m_u8aOutputBuf[SETNTCFG_DOWNFRMMULTVAL_OFFS] = param.u8DownFrameMultVal;
                        m_u8aOutputBuf[SETNTCFG_NUMPARENTS_OFFS] = param.u8NumParents;
                        m_u8aOutputBuf[SETNTCFG_CCAMODE_OFFS] = (byte)param.ccaMode;
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16ChannelList, m_u8aOutputBuf, SETNTCFG_CHANLIST_OFFS);
                        m_u8aOutputBuf[SETNTCFG_AUTOSTARTNT_OFFS] = (byte)((param.bAutoStartNetwork == true) ? 1 : 0);
                        m_u8aOutputBuf[SETNTCFG_IOMODE_OFFS] = (byte)param.u8LocMode;
                        m_u8aOutputBuf[SETNTCFG_BBMODE_OFFS] = (byte)param.bbMode;
                        m_u8aOutputBuf[SETNTCFG_BBSIZE_OFFS] = (byte)param.u8BBSize;
                        m_u8aOutputBuf[SETNTCFG_ISRADIOTEST_OFFS] = (byte)((param.isRadioTest) ? 0x01 : 0x00);
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16BwMult, m_u8aOutputBuf, SETNTCFG_BWMULT_OFFS);
                        m_u8aOutputBuf[SETNTCFG_ONECHAN_OFFS] = param.u8OneChannel;
                        m_u8OutputBufLen = SETNTCFG_PAYLOAD_LEN;

                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetNetworkConfigReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_CLEARSTATISTICS
                    case enCmd.CMDID_CLEARSTATISTICS:
                    {
                        m_u8OutputBufLen = CLEARSTATISTICS_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "ClearStatistics");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, ClearStatisticsReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_EXCHANGEMOTEJOINKEY
                    case enCmd.CMDID_EXCHANGEMOTEJOINKEY:
                    {
                        tEXMOTEJOINKEY param = (tEXMOTEJOINKEY)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = param.mac;

                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, EXCHMOTEJK_MAC_OFFS, tMAC.LEN);
                        Array.Copy(param.key.u8aData, 0, m_u8aOutputBuf, EXCHMOTEJK_KEY_OFFS, tSECKEY.LEN);
                        m_u8OutputBufLen = EXCHMOTEJK_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "ExchangeMoteJoinKey, (Mac, Joinkey)(" + param.mac.ToHexString() + ", " + param.key.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, ExchangeMoteJoinKeyReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_EXCHANGENETWORKID
                    case enCmd.CMDID_EXCHANGENETWORKID:
                    {
                        UInt16 networkId = (UInt16)newReq.param;
                        DataTypeConverter.UInt16ToMeshByteArr(networkId, m_u8aOutputBuf, EXCHNTID_ID_OFFS);
                        m_u8OutputBufLen = EXCHNTID_PAYLOAD_LEN;
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, ExchangeNetworkIdReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break; 
                    }
                    #endregion
                    #region CMDID_RADIOTESTTX
                    case enCmd.CMDID_RADIOTESTTX:
                    {
                        tRADIOTESTTX param = (tRADIOTESTTX)newReq.param;
                        m_u8aOutputBuf[RADIOTESTTX_TESTTYPE_OFFS] = (byte)param.type;
                        DataTypeConverter.UInt16ToMeshByteArr(param.chanMask, m_u8aOutputBuf, RADIOTESTTX_CHANMASK_OFFS);
                        DataTypeConverter.UInt16ToMeshByteArr(param.repeatCnt, m_u8aOutputBuf, RADIOTESTTX_REPEATCNT_OFFS);
                        m_u8aOutputBuf[RADIOTESTTX_TXPOWER_OFFS] = (byte)param.txPower;
                        m_u8aOutputBuf[RADIOTESTTX_SEQSIZE_OFFS] = (byte)param.seqSize;
                        for (byte i = 0; i < param.seqSize; i++)
                        {
                            m_u8aOutputBuf[RADIOTESTTX_SEQDEF_OFFS + i * 3] = param.sequenceDef[i].u8PkLen;
                            DataTypeConverter.UInt16ToMeshByteArr(param.sequenceDef[i].u16Delay, m_u8aOutputBuf, (byte)(RADIOTESTTX_SEQDEF_OFFS + i * 3 + 1));
                        }
                        m_u8aOutputBuf[RADIOTESTTX_STATIONID_OFFS] = (byte)param.stationId;
                        m_u8OutputBufLen = RADIOTESTTX_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestTx");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, RadioTestTxReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_RADIOTESTRX
                    case enCmd.CMDID_RADIOTESTRX:
                    {
                        tRADIOTESTRX param = (tRADIOTESTRX)newReq.param;
                        DataTypeConverter.UInt16ToMeshByteArr(param.chanMask, m_u8aOutputBuf, RADIOTESTRX_CHANMASK_OFFS);
                        DataTypeConverter.UInt16ToMeshByteArr(param.duration, m_u8aOutputBuf, RADIOTESTRX_DURATION_OFFS);
                        m_u8aOutputBuf[RADIOTESTRX_STATIONID_OFFS] = (byte)param.stationId;
                        m_u8OutputBufLen = RADIOTESTRX_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "RadioTestRx");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, RadioTestRxReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETRADIOTESTSTATISTICS
                    case enCmd.CMDID_GETRADIOTESTSTATISTICS:
                    {
                        m_u8OutputBufLen = GETRADIOTESTSTATICS_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetRadiotestStatistics");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetRadiotestStatisticsReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETACLENTRY
                    case enCmd.CMDID_SETACLENTRY:
                    {
                        tSETACLENTRY param = (tSETACLENTRY)newReq.param;
                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, SETACLENTRY_MACADDR_OFFS, tMAC.LEN);
                        Array.Copy(param.key.u8aData, 0, m_u8aOutputBuf, SETACLENTRY_JOINKEY_OFFS, tSECKEY.LEN);
                        m_u8OutputBufLen = SETACLENTRY_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetACLEntry (Mac, Key)(" + param.mac.ToHexString() + ", " + param.key.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetACLEntryReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETNEXTACLENTRY
                    case enCmd.CMDID_GETNEXTACLENTRY:
                    {
                        tMAC mac = (tMAC)newReq.param;
                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, GETNEXTACLENTRY_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = GETNEXTACLENTRY_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextACLEntry refer to Mote(" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetNextACLEntryReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_DELETEACLENTRY
                    case enCmd.CMDID_DELETEACLENTRY:
                    {
                        tMAC mac = (tMAC)newReq.param;
                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, DELETEACLENTRY_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = DELETEACLENTRY_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteACLEntry refer to Mote(" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, DeleteACLEntryReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_PINGMOTE
                    case enCmd.CMDID_PINGMOTE:
                    {
                        tMAC mac = (tMAC)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = mac;

                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, PINGMOTE_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = PINGMOTE_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "PingMote(" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, PingMoteReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETLOG
                    case enCmd.CMDID_GETLOG:
                    {
                        tMAC mac = (tMAC)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = mac;

                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, GETLOG_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = GETLOG_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetLog Mote(" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetLogReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SENDDATA
                    case enCmd.CMDID_SENDDATA:
                    {
                        tSENDDATA param = (tSENDDATA)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = param.mac;

                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, SENDDATA_MACADDR_OFFS, tMAC.LEN);
                        m_u8aOutputBuf[SENDDATA_PRIORITY_OFFS] = (byte)param.priority;
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16SrcPort, m_u8aOutputBuf, SENDDATA_SRCPORT_OFFS);
                        DataTypeConverter.UInt16ToMeshByteArr(param.u16DstPort, m_u8aOutputBuf, SENDDATA_DSTPORT_OFFS);
                        m_u8aOutputBuf[SENDDATA_OPTIONS_OFFS] = param.u8Options;
                        Array.Copy(param.u8aData, 0, m_u8aOutputBuf, SENDDATA_DATA_OFFS, param.u8aData.Length);
                        m_u8OutputBufLen = (byte)(SENDDATA_PAYLOAD_LEN + param.u8aData.Length);
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SendDataReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_STARTNETWORK
                    case enCmd.CMDID_STARTNETWORK:
                    {
                        m_u8OutputBufLen = STARTNETWORK_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "StartNetwork");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, StartNetworkReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETSYSTEMINFO
                    case enCmd.CMDID_GETSYSTEMINFO:
                    {
                        m_u8OutputBufLen = GETSYSTEMINFO_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetSystemInfo");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetSystemInfoReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETMOTECONFIG
                    case enCmd.CMDID_GETMOTECONFIG:
                    {
                        tGETMOTECONFIG param = (tGETMOTECONFIG)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = param.mac;

                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, GETMOTECONFIG_MACADDR_OFFS, tMAC.LEN);
                        m_u8aOutputBuf[GETMOTECONFIG_NEXT_OFFS] = (byte)((param.next == true) ? 0x01 : 0x00);
                        m_u8OutputBufLen = GETMOTECONFIG_PAYLOAD_LEN;
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetMoteConfigReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETPATHINFO
                    case enCmd.CMDID_GETPATHINFO:
                    {
                        tGETPATHINFO param = (tGETPATHINFO)newReq.param;
                        Array.Copy(param.source.u8aData, 0, m_u8aOutputBuf, GETPATHINFO_SRCMACADDR_OFFS, tMAC.LEN);
                        Array.Copy(param.dest.u8aData, 0, m_u8aOutputBuf, GETPATHINFO_DSTMACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = GETPATHINFO_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetPathInfo (SRC(" + param.source.ToHexString() + ")->DST(" + param.dest.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetPathInfoReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETNEXTPATHINFO
                    case enCmd.CMDID_GETNEXTPATHINFO:
                    {
                        tGETNEXTPATHINFO param = (tGETNEXTPATHINFO)newReq.param;
                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, GETNEXTPATHINFO_MACADDR_OFFS, tMAC.LEN);
                        m_u8aOutputBuf[GETNEXTPATHINFO_FILTER_OFFS] = (byte)param.filter;
                        DataTypeConverter.UInt16ToMeshByteArr(param.pathId, m_u8aOutputBuf, GETNEXTPATHINFO_PATHID_OFFS);
                        m_u8OutputBufLen = GETNEXTPATHINFO_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextPathInfo(" + param.mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetNextPathInfoReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETADVERTISING
                    case enCmd.CMDID_SETADVERTISING:
                    {
                        enAdvState state = (enAdvState)newReq.param;
                        m_u8aOutputBuf[SETADVERTISING_ACTIVATE_OFFS] = (byte)state;
                        m_u8OutputBufLen = SETADVERTISING_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetAdvertising " + state.ToString());
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetAdvertisingReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETDOWNSTREAMFRAMEMODE
                    case enCmd.CMDID_SETDOWNSTREAMFRAMEMODE:
                    {
                        enDnstreamFrameMode mode = (enDnstreamFrameMode)newReq.param;
                        m_u8aOutputBuf[SETDOWNSTREAMFRAMEMODE_FRAMEMODE_OFFS] = (byte)mode;
                        m_u8OutputBufLen = SETDOWNSTREAMFRAMEMODE_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetDownstreamFrameMode " + mode.ToString());
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetDownstreamFrameModeReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETMANAGERSTATISTICS
                    case enCmd.CMDID_GETMANAGERSTATISTICS:
                    {
                        m_u8OutputBufLen = GETMANAGERSTATISTICS_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetManagerStatistics");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetManagerStatisticsReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETTIME
                    case enCmd.CMDID_SETTIME:
                    {
                        tSETTIME param = (tSETTIME)newReq.param;
                        m_u8aOutputBuf[SETTIME_TRIGGER_OFFS] = (byte)param.trig;
                        DataTypeConverter.UInt64ToMeshByteArr(param.time.u64Seconds, m_u8aOutputBuf, SETTIME_UTCSECS_OFFS);
                        DataTypeConverter.UInt32ToMeshByteArr(param.time.u32Microseconds, m_u8aOutputBuf, SETTIME_UTCMSECS_OFFS);
                        m_u8OutputBufLen = SETTIME_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetTime, seconds(" + param.time.u64Seconds.ToString() +
                        //    ")|microseconds(" + param.time.u32Microseconds.ToString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetTimeReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETLICENSE
                    case enCmd.CMDID_GETLICENSE:
                    {
                        m_u8OutputBufLen = GETLICENSE_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetLicense");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetLicenseReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETLICENSE
                    case enCmd.CMDID_SETLICENSE:
                    {
                        tLICENSE license = (tLICENSE)newReq.param;
                        Array.Copy(license.u8aData, 0, m_u8aOutputBuf, SETLICENSE_LICENSE_OFFS, tLICENSE.LEN);
                        m_u8OutputBufLen = SETLICENSE_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetLicense (" + license.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetLicenseReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETCLIUSER
                    case enCmd.CMDID_SETCLIUSER:
                    {
                        tSETCLIUSER param = (tSETCLIUSER)newReq.param;
                        m_u8aOutputBuf[SETCLIUSER_ROLE_OFFS] = (byte)param.role;
                        Array.Copy(param.password.u8aData, 0, m_u8aOutputBuf, SETCLIUSER_PASSWORD_OFFS, tUSERPW.LEN);
                        m_u8OutputBufLen = SETCLIUSER_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetUser password");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetUserReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SENDIP
                    case enCmd.CMDID_SENDIP:
                    {
                        tSENDIPDATA param = (tSENDIPDATA)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = param.mac;

                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, SENDIP_MACADDR_OFFS, tMAC.LEN);
                        m_u8aOutputBuf[SENDIP_PRIORITY_OFFS] = (byte)param.priority;
                        m_u8aOutputBuf[SENDIP_OPTIONS_OFFS] = param.u8Options;
                        m_u8aOutputBuf[SENDIP_ENCRYPTEDOFFSET_OFFS] = param.u8EncryptedOffset;
                        Array.Copy(param.u8aData, 0, m_u8aOutputBuf, SENDIP_DATA_OFFS, param.u8aData.Length);
                        m_u8OutputBufLen = (byte)(SENDIP_PAYLOAD_LEN + param.u8aData.Length);
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SendIP data");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SendIPReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_RESTOREFACTORYDEFAULTS
                    case enCmd.CMDID_RESTOREFACTORYDEFAULTS:
                    {
                        m_u8OutputBufLen = RESTOREFACTORYDEFAULTS_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "RestoreFactoryDefaults");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, RestoreFactoryDefaultsReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETMOTEINFO
                    case enCmd.CMDID_GETMOTEINFO:
                    {
                        tMAC mac = (tMAC)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = mac;

                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, GETMOTEINFO_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = GETMOTEINFO_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteInfo (" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetMoteInfoReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETNETWORKCONFIG
                    case enCmd.CMDID_GETNETWORKCONFIG:
                    {
                        m_u8OutputBufLen = GETNETWORKCONFIG_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkConfig");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetNetworkConfigReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETNETWORKINFO
                    case enCmd.CMDID_GETNETWORKINFO:
                    {
                        m_u8OutputBufLen = GETNETWORKINFO_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetNetworkInfo");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetNetworkInfoReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETMOTECONFIGBYID
                    case enCmd.CMDID_GETMOTECONFIGBYID:
                    {
                        ushort moteID = (ushort)newReq.param;
                        DataTypeConverter.UInt16ToMeshByteArr(moteID, m_u8aOutputBuf, GETMOTECONFIGBYID_MOTEID_OFFS);
                        m_u8OutputBufLen = GETMOTECONFIGBYID_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfigById (" + moteID.ToString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetMoteConfigByIdReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETCOMMONJOINKEY
                    case enCmd.CMDID_SETCOMMONJOINKEY:
                    {
                        tSECKEY key = (tSECKEY)newReq.param;
                        Array.Copy(key.u8aData, 0, m_u8aOutputBuf, SETCOMMONJOINKEY_KEY_OFFS, tSECKEY.LEN);
                        m_u8OutputBufLen = SETCOMMONJOINKEY_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetCommonJoinKey (" + key.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetCommonJoinKeyReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETIPCONFIG
                    case enCmd.CMDID_GETIPCONFIG:
                    {
                        m_u8OutputBufLen = GETIPCONFIG_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetIPConfig");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetIPConfigReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_SETIPCONFIG
                    case enCmd.CMDID_SETIPCONFIG:
                    {
                        tSETIPCONFIG param = (tSETIPCONFIG)newReq.param;
                        Array.Copy(param.ipv6Address.u8aData, 0, m_u8aOutputBuf, SETIPCONFIG_IPV6ADDR_OFFS, tIPV6ADDR.LEN);
                        Array.Copy(param.mask.u8aData, 0, m_u8aOutputBuf, SETIPCONFIG_MASK_OFFS, tIPV6MASK.LEN);
                        m_u8OutputBufLen = SETIPCONFIG_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "SetIPConfig, ipv6Address(" + param.ipv6Address.ToHexString() + "|mask(" +
                        //    param.mask.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, SetIPConfigReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_DELETEMOTE
                    case enCmd.CMDID_DELETEMOTE:
                    {
                        tMAC mac = (tMAC)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = mac;

                        Array.Copy(mac.u8aData, 0, m_u8aOutputBuf, DELETEMOTE_MACADDR_OFFS, tMAC.LEN);
                        m_u8OutputBufLen = DELETEMOTE_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "DeleteMote (" + mac.ToHexString() + ")");
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, DeleteMoteReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    #region CMDID_GETMOTELINKS
                    case enCmd.CMDID_GETMOTELINKS:
                    {
                        tGETMOTELINKS param = (tGETMOTELINKS)newReq.param;

                        // 记录当前命令目标ws
                        m_macCurTarget = param.mac;

                        Array.Copy(param.mac.u8aData, 0, m_u8aOutputBuf, GETMOTELINKS_MACADDR_OFFS, tMAC.LEN);
                        DataTypeConverter.UInt16ToMeshByteArr(param.idx, m_u8aOutputBuf, GETMOTELINKS_IDX_OFFS);
                        m_u8OutputBufLen = GETMOTELINKS_PAYLOAD_LEN;
                        //CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteLinks (" + param.mac.ToHexString() + ") links refer to idx " + param.idx.ToString());
                        if (enErrCode.ERR_NONE == (RC = m_Ser.SendRequest(newReq.cmd, false, m_u8aOutputBuf, m_u8OutputBufLen, GetMoteLinksReplyCB, bRetry)))
                            m_bBusyTx = true;

                        break;
                    }
                    #endregion
                    default:
                        return enErrCode.ERR_INVALID_PARAM;
                }

                if (RC == enErrCode.ERR_NONE)
                {
                    if (!WaitMgrResponse())
                        return enErrCode.ERR_MGR_RESPONSE_TIMEOUT;

                    if (m_bStressfull)
                    {
                        // 需要及时清空Manager当前压力大的标志
                        m_bStressfull = false;
                        return enErrCode.ERR_MGR_STRESSFUL;
                    }
                }
            }

            return RC;
        }

        #region meshAPI通知
        private const byte MIN_NOTIF_LEN = 1;
        /// <summary>
        /// mesh层注册给serial层的通知回调函数
        /// </summary>
        /// <param name="cmdId">必须是CMDID_NOTIFICATION</param>
        /// <param name="flags">"Control" field in api header</param>
        /// <param name="payload">Notification payload data</param>
        /// <param name="len">Notification payload data length</param>
        public void MeshNotify(enCmd cmdId, byte flags, byte[] payload, byte len)
        {
            // 事件类型
            enEventType eventType = enEventType.EVENTID_NULL;
            // 通知实体
            NotifBase notObj = null;
            // abort if not a notification
            if (cmdId != enCmd.CMDID_NOTIFICATION)
                return;
            // abort no space for notifType
            if (len < MIN_NOTIF_LEN)
                return;
            // abort if Notify callback is null
            if (m_evNotifyArrived == null)
                return;

            // 验证基本的通知信息
            NotifBase tmpNotify = new NotifBase();
            if (len < NotifBase.Len)
                return;
            else
                tmpNotify.Unserialize(payload);

            // 根据不同的通知类型进行通知解析
            switch (tmpNotify.NotifyType)
            {
                #region NOTIFID_NOTIFLOG
                case enNotifyType.NOTIFID_NOTIFLOG:
                {
                    NotifLog notLog = new NotifLog();
                    if (len < notLog.MinLen)
                        return;
                    else
                    {
                        eventType = enEventType.EVENTID_NULL;
                        notLog.Unserialize(payload);
                        notObj = notLog;
                    }

                    break;
                }
                #endregion
                #region NOTIFID_NOTIFDATA
                case enNotifyType.NOTIFID_NOTIFDATA:
                {
                    NotifData notData = new NotifData();
                    if (len < notData.MinLen)
                        return;
                    else
                    {
                        eventType = enEventType.EVENTID_NULL;
                        notData.Unserialize(payload);
                        notObj = notData;
                    }

                    break;
                }
                #endregion
                #region NOTIFID_NOTIFIPDATA
                case enNotifyType.NOTIFID_NOTIFIPDATA:
                {
                    NotifIpData notIpData = new NotifIpData();

                    if (len < notIpData.MinLen)
                        return;
                    else
                    {
                        eventType = enEventType.EVENTID_NULL;
                        notIpData.Unserialize(payload);
                        notObj = notIpData;
                    }

                    break;
                }
                #endregion
                #region NOTIFID_NOTIFHEALTHREPORT
                case enNotifyType.NOTIFID_NOTIFHEALTHREPORT:
                {
                    NotifHR tmpHR = new NotifHR();
                    if (len < NotifHR.Len)
                        return;
                    else
                        tmpHR.Unserialize(payload);

                    if (tmpHR.Id == enHRID.HRID_DEVICE)
                    {
                        NotifDeviceHR notDeviceHR = new NotifDeviceHR();
                        if (len < notDeviceHR.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_NULL;
                            notDeviceHR.Unserialize(payload);
                            notObj = notDeviceHR;

                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]DHR of Mote(" + notDeviceHR.Mac.ToHexString() + "): " +
                                "charge(" + notDeviceHR.u32Charge.ToString() + 
                                ")|queueOcc(" + notDeviceHR.u8QueueOcc.ToString() +
                                ")|temperature(" + notDeviceHR.u8Temperature.ToString() +
                                ")|batteryVoltage(" + notDeviceHR.u16BatteryVoltage.ToString() +
                                ")|numTxOk(" + notDeviceHR.u16NumTxOk.ToString() +
                                ")|numTxFail(" + notDeviceHR.u16NumTxFail.ToString() +
                                ")|numRxOk(" + notDeviceHR.u16NumRxOk.ToString() +
                                ")|numRxLost(" + notDeviceHR.u16NumRxLost.ToString() +
                                ")|numMacDropped(" + notDeviceHR.u8NumMacDropped.ToString() +
                                ")|numTxBad(" + notDeviceHR.u8NumTxBad.ToString() +
                                ")|badLinkFrameId(" + notDeviceHR.u8BadLinkFrameId.ToString() +
                                ")|badLinkSlot(" + notDeviceHR.u32BadLinkSlot.ToString() +
                                ")|badLinkOffset(" + notDeviceHR.u8BadLinkOffset.ToString() + ")");
                        }
                    }
                    else if (tmpHR.Id == enHRID.HRID_NEIGHBOR)
                    {
                        NotifNeighborsHR notNeighborsHR = new NotifNeighborsHR();
                        if (len < notNeighborsHR.MinLen)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_NULL;
                            notNeighborsHR.Unserialize(payload);
                            notObj = notNeighborsHR;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]NHR of Mote(" + notNeighborsHR.Mac.ToHexString() + "): ");
                            for (int i = 0; i < notNeighborsHR.u8NumItems; i++)
                            {
                                CommStackLog.RecordInf(enLogLayer.eMesh,
                                    "[NM]neighborId(" + notNeighborsHR.neighborHRData[i].u16NeighborId.ToString() +
                                    ")|neighborFlag(" + notNeighborsHR.neighborHRData[i].u8NeighborFlag.ToString() +
                                    ")|rssi(" + notNeighborsHR.neighborHRData[i].s8Rssi.ToString() +
                                    ")|numTxPackets(" + notNeighborsHR.neighborHRData[i].u16NumTxPackets.ToString() +
                                    ")|numTxFailures(" + notNeighborsHR.neighborHRData[i].u16NumTxFailures.ToString() +
                                    ")|numRxPackets(" + notNeighborsHR.neighborHRData[i].u16NumTxPackets.ToString() + ")");
                            }
                        }
                    }
                    else if (tmpHR.Id == enHRID.HRID_DISCOVERED_NEIGHBOR)
                    {
                        NotifDiscoveredNeighborsHR notDiscoveredNeighborsHR = new NotifDiscoveredNeighborsHR();
                        if (len < notDiscoveredNeighborsHR.MinLen)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_NULL;
                            notDiscoveredNeighborsHR.Unserialize(payload);
                            notObj = notDiscoveredNeighborsHR;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]DNHR of Mote(" + notDiscoveredNeighborsHR.Mac.ToHexString() + "): ");
                            for (int i = 0; i < notDiscoveredNeighborsHR.u8NumItems; i++)
                            {
                                CommStackLog.RecordInf(enLogLayer.eMesh,
                                    "[NM]neighborId(" + notDiscoveredNeighborsHR.neighborHRData[i].u16NeighborId.ToString() +
                                    ")|rssi(" + notDiscoveredNeighborsHR.neighborHRData[i].s8Rssi.ToString() +
                                    ")|numRx(" + notDiscoveredNeighborsHR.neighborHRData[i].u16NumRx.ToString() + ")");
                            }
                        }
                    }

                    break;
                }
                #endregion
                #region NOTIFID_NOTIFEVENT
                case enNotifyType.NOTIFID_NOTIFEVENT:
                {
                    NotEvent tmpEvent = new NotEvent();
                    if (len < NotEvent.Len)
                        return;
                    else
                        tmpEvent.Unserialize(payload);
                    #region EVENTID_EVENTMOTERESET
                    if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTERESET)
                    {
                        EventMoteReset evtMoteReset = new EventMoteReset();
                        if (len < evtMoteReset.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTERESET;
                            evtMoteReset.Unserialize(payload);
                            notObj = evtMoteReset;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteReset.Mac.ToHexString() + ") reset");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTNETWORKRESET
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTNETWORKRESET)
                    {
                        EventNetworkReset evtNetworkReset = new EventNetworkReset();
                        if (len < evtNetworkReset.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTNETWORKRESET;
                            evtNetworkReset.Unserialize(payload);
                            notObj = evtNetworkReset;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Network reset");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTCOMMANDFINISHED
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTCOMMANDFINISHED)
                    {
                        EventCommandFinished evtCommandFinished = new EventCommandFinished();
                        if (len < evtCommandFinished.Len)
                        {
                            CommStackLog.RecordErr(enLogLayer.eMesh, "evtCommandFinished less length");
                            return;
                        }
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTCOMMANDFINISHED;
                            evtCommandFinished.Unserialize(payload);
                            notObj = evtCommandFinished;

                            lock (m_dicAsyncCmdRespRecords)
                            {
                                if (!m_dicAsyncCmdRespRecords.ContainsKey(evtCommandFinished.u32CallbackId))
                                {
                                    AsyncCmdRespEntry elem = new AsyncCmdRespEntry();
                                    elem.NOT1ST = true;
									elem.AsyncRC = (byte)evtCommandFinished.u8RC;
                                    m_dicAsyncCmdRespRecords.Add(evtCommandFinished.u32CallbackId, elem);
                                }
                                else
                                    m_dicAsyncCmdRespRecords[evtCommandFinished.u32CallbackId].NOT1ST = true;
                            }

                            CommStackLog.RecordInf(enLogLayer.eMesh, "CBID(" + evtCommandFinished.u32CallbackId + ") sent");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTMOTEJOIN
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTEJOIN)
                    {
                        EventMoteJoin evtMoteJoin = new EventMoteJoin();
                        if (len < evtMoteJoin.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTEJOIN;
                            evtMoteJoin.Unserialize(payload);
                            notObj = evtMoteJoin;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteJoin.Mac.ToHexString() + ") joined");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTMOTEOPERATIONAL
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTEOPERATIONAL)
                    {
                        EventMoteOperational evtMoteOperational = new EventMoteOperational();
                        if (len < evtMoteOperational.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTEOPERATIONAL;
                            evtMoteOperational.Unserialize(payload);
                            notObj = evtMoteOperational;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteOperational.Mac.ToHexString() + ") operational");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTMOTELOST
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTELOST)
                    {
                        EventMoteLost evtMoteLost = new EventMoteLost();
                        if (len < evtMoteLost.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTELOST;
                            evtMoteLost.Unserialize(payload);
                            notObj = evtMoteLost;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteLost.Mac.ToHexString() + ") lost");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTNETWORKTIME
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTNETWORKTIME)
                    {
                        EventNetworkTime evtNetworkTime = new EventNetworkTime();
                        if (len < evtNetworkTime.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTNETWORKTIME;
                            evtNetworkTime.Unserialize(payload);
                            notObj = evtNetworkTime;

                            CommStackLog.RecordInf(enLogLayer.eMesh, "Network time as: uptime(" +
                                evtNetworkTime.u32Uptime.ToString() + ")|utcTime(" + evtNetworkTime.tUTCTime.u64Seconds + "." + evtNetworkTime.tUTCTime.u32Microseconds +
                                ")|asnOffset(" + evtNetworkTime.u16AsnOffset.ToString() + ")");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTPINGRESPONSE
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTPINGRESPONSE)
                    {
                        EventPingResponse evtPingResponse = new EventPingResponse();
                        if (len < evtPingResponse.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTPINGRESPONSE;
                            evtPingResponse.Unserialize(payload);
                            notObj = evtPingResponse;

                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtPingResponse.Mac.ToHexString() + ") ping response: " +
                                "callbackId(" + evtPingResponse.u32CallbackId.ToString() +
                                ")|delay(" + evtPingResponse.u32Delay.ToString() +
                                ")|voltage(" + evtPingResponse.u16Voltage.ToString() +
                                ")|temperature(" + evtPingResponse.u8Temperature.ToString() + ")");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTPATHCREATE
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTPATHCREATE)
                    {
                        EventPathCreate evtPathCreate = new EventPathCreate();
                        if (len < evtPathCreate.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTPATHCREATE;
                            evtPathCreate.Unserialize(payload);
                            notObj = evtPathCreate;

                            string strDirc = "?";
                            if (evtPathCreate.Direction == enPathDirection.None) strDirc = "?";
                            else if (evtPathCreate.Direction == enPathDirection.Unused) strDirc = "-";
                            else if (evtPathCreate.Direction == enPathDirection.Upstream) strDirc = "↑";
                            else if (evtPathCreate.Direction == enPathDirection.Downstream) strDirc = "↓";

                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Path(" + evtPathCreate.Source.ToHexString() + ">" +
                                evtPathCreate.Dest.ToHexString() + ")" + strDirc + " created");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTPATHDELETE
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTPATHDELETE)
                    {
                        EventPathDelete evtPathDelete = new EventPathDelete();
                        if (len < evtPathDelete.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTPATHDELETE;
                            evtPathDelete.Unserialize(payload);
                            notObj = evtPathDelete;

                            string strDirc = "?";
                            if (evtPathDelete.Direction == enPathDirection.None) strDirc = "?";
                            else if (evtPathDelete.Direction == enPathDirection.Unused) strDirc = "-";
                            else if (evtPathDelete.Direction == enPathDirection.Upstream) strDirc = "↑";
                            else if (evtPathDelete.Direction == enPathDirection.Downstream) strDirc = "↓";

                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Path(" + evtPathDelete.Source.ToHexString() + ">" +
                                evtPathDelete.Dest.ToHexString() + ")" + strDirc + " deleted");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTPACKETSENT
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTPACKETSENT)
                    {
                        EventPacketSent evtPacketSent = new EventPacketSent();
                        if (len < evtPacketSent.Len)
                        {
                            CommStackLog.RecordErr(enLogLayer.eMesh, "EvtPacketSent less length");
                            return;
                        }
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTPACKETSENT;
                            evtPacketSent.Unserialize(payload);
                            notObj = evtPacketSent;

                            lock (m_dicAsyncCmdRespRecords)
                            {
                                if (!m_dicAsyncCmdRespRecords.ContainsKey(evtPacketSent.u32CallbackId))
                                {
                                    AsyncCmdRespEntry elem = new AsyncCmdRespEntry();
                                    elem.NOT1ST = true;
                                    m_dicAsyncCmdRespRecords.Add(evtPacketSent.u32CallbackId, elem);
                                }
                                else
                                    m_dicAsyncCmdRespRecords[evtPacketSent.u32CallbackId].NOT1ST = true;
                            }
                            m_asigEased.Set();
                            CommStackLog.RecordInf(enLogLayer.eMesh, "SDCBID(" + evtPacketSent.u32CallbackId + ") sent");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTMOTECREATE
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTECREATE)
                    {
                        EventMoteCreate evtMoteCreate = new EventMoteCreate();
                        if (len < evtMoteCreate.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTECREATE;
                            evtMoteCreate.Unserialize(payload);
                            notObj = evtMoteCreate;
                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteCreate.Mac.ToHexString() + ") joined 1st time");
                        }
                    }
                    #endregion
                    #region EVENTID_EVENTMOTEDELETE
                    else if (tmpEvent.EventType == enEventType.EVENTID_EVENTMOTEDELETE)
                    {
                        EventMoteDelete evtMoteDelete = new EventMoteDelete();
                        if (len < evtMoteDelete.Len)
                            return;
                        else
                        {
                            eventType = enEventType.EVENTID_EVENTMOTEDELETE;
                            evtMoteDelete.Unserialize(payload);
                            notObj = evtMoteDelete;

                            CommStackLog.RecordInf(enLogLayer.eMesh, "[NM]Mote(" + evtMoteDelete.Mac.ToHexString() + ") deleted");
                        }
                    }
                    #endregion

                    break;
                }
                #endregion
                default:
                    break;
            }

            m_evNotifyArrived(tmpNotify.NotifyType, eventType, notObj/*,len*/);
        }

        #endregion
    }
}
