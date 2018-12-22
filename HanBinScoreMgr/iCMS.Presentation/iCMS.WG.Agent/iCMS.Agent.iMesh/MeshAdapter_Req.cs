using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    public partial class MeshAdapter : IUserRequest
    {
        /// <summary>
        /// [常量]系统默认Netid
        /// </summary>
        private const UInt16 DEFAULT_NETID = 1229;
        /// <summary>
        /// SendData源端口号
        /// </summary>
        private UInt16 SENDDATA_SRCPORT_DEF = 0xF0B8;
        /// <summary>
        /// SendData目的端口号
        /// </summary>
        private UInt16 SENDDATA_DSTPORT_DEF = 0xF0B8;
        /// <summary>
        /// 用户请求队列(FIFO)
        /// </summary>
        internal volatile UserRequestQueue ReqBuffer = new UserRequestQueue();
        private object m_lockAcceptRequest = new object();
        /// <summary>
        /// 维护一份已订阅通知类型备份，以支持重复调用Subscribe订阅单个事件，而不影响之前调用的订阅请求
        /// </summary>
        private UInt32 u32Subscribed = 0;
        /// <summary>
        /// 不响应标志，当前不响应数据通知和事件通知
        /// </summary>
        private UInt32 u32Unack = (UInt32)(enSubFilters.Data | enSubFilters.Event);
        /// <summary>
        /// 网络配置信息缓存
        /// </summary>
        private volatile tNETWORKCONFIG NetworkConfigCopy = new tNETWORKCONFIG();
        
        internal enURErrCode Subscribe(enSubFilters filter, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSUBS param = new tSUBS();
                // 记录本次调用订阅的通知类型
                u32Subscribed |= (UInt32)filter;
                // 构造订阅参数
                param.u32Filter = u32Subscribed;
                param.u32UnackFilter = u32Unack;

                element.param = (object)param;
                element.cmd = enCmd.CMDID_SUBSCRIBE;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Subscribe unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    if (filter != enSubFilters.All)
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Subscribe " + filter.ToString());
                    else
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Subscribe Event|Log|Data|IPData|HealthReports");

                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode SubscribeUnack(enSubFilters filter, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSUBS param = new tSUBS();
                // 记录本次调用订阅的通知类型
                u32Subscribed |= (UInt32)filter;
                u32Unack |= (UInt32)filter;
                // 构造定于参数
                param.u32Filter = u32Subscribed;
                param.u32UnackFilter = u32Unack;

                element.param = (object)param;
                element.cmd = enCmd.CMDID_SUBSCRIBE;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Subscribe with unack unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    if (filter != enSubFilters.All)
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Subscribe " + filter.ToString() + " with unack");
                    else
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Subscribe Event|Log|Data|IPData|HealthReports with unack");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode Unsubscribe(enSubFilters filter, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSUBS param = new tSUBS();
                // 记录本次调用订阅的通知类型
                u32Subscribed &= ~((UInt32)filter);
                u32Unack &= ~((UInt32)filter);
                // 构造定于参数
                param.u32Filter = u32Subscribed;
                param.u32UnackFilter = u32Unack;

                element.param = (object)param;
                element.cmd = enCmd.CMDID_SUBSCRIBE;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Unsubscribe unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    if (filter != enSubFilters.All)
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Unsubscribe " + filter.ToString());
                    else
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Unsubscribe Event|Log|Data|IPData|HealthReports");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode GetTime(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETTIME;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetTime unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetTime");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode GetNetworkConfig(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETNETWORKCONFIG;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetNetworkConfig unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetNetworkConfig");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode SendData(tMAC mac, byte[] data, bool urgent = false, enPktPriority priority = enPktPriority.Medium)
        {
            lock (m_lockAcceptRequest)
            {
                if (data == null || data.Length <= 0)
                    return enURErrCode.ERR_INVALID_PARAM;

                UserRequestElement element = new UserRequestElement();
                tSENDDATA copy = new tSENDDATA();
                copy.mac.Assign(mac);
                copy.priority = priority;
                copy.u16SrcPort = SENDDATA_SRCPORT_DEF;
                copy.u16DstPort = SENDDATA_DSTPORT_DEF;
                copy.u8aData = new byte[data.Length];
                Array.Copy(data, 0, copy.u8aData, 0, data.Length);
                element.cmd = enCmd.CMDID_SENDDATA;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                    return enURErrCode.ERR_MORE_REQUEST;
                else
                    return enURErrCode.ERR_NONE;
            }
        }
        internal enURErrCode ResetSystem(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_RESET;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ResetSystem unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eMesh, "ResetSystem");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode ResetMote(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_RESET;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ResetMote unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eMesh, "ResetMote(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode RestoreFactoryDefaults(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16NetworkId = DEFAULT_NETID;
                element.cmd = enCmd.CMDID_RESTOREFACTORYDEFAULTS;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "RestoreFactoryDefaults unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreFactoryDefaults");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        internal enURErrCode GetMoteConfig(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                if (mac.IsNull)
                    return enURErrCode.ERR_INVALID_PARAM;

                UserRequestElement element = new UserRequestElement();
                tGETMOTECONFIG param = new tGETMOTECONFIG();
                param.mac.Assign(mac);
                param.next = false;
                element.cmd = enCmd.CMDID_GETMOTECONFIG;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetMoteConfig unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eMesh, "GetMoteConfig(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }

        internal enum enSNCItem
        {
            SNC_NWID = 0,
            SNC_AP_TXPWR,
            SNC_FRM_PROF,
            SNC_MAX_MOTE,
            SNC_BASE_BW,
            SNC_DN_FRM_MUL_VAL,
            SNC_NUM_PARENTS,
            SNC_CCA_MODE,
            SNC_CHAN_LIST,
            SNC_AUTO_START_NW,
            SNC_IOC_MODE,
            SNC_BB_MODE,
            SNC_IS_REDIO_TEST,
            SNC_BW_MULT,
            SNC_ONE_CHAN,
            SNC_NULL
        }

        private enSNCItem m_SetNetworkCfgItem = enSNCItem.SNC_NULL;
        public enURErrCode SetNetworkId(ushort networkId, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16NetworkId = networkId;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetNetworkId unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
				    m_SetNetworkCfgItem = enSNCItem.SNC_NWID;
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetNetworkId " + networkId.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetNetworkId()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u16NetworkId;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetAPTxPower(SByte txPower, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.s8ApTxPower = txPower;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetAPTxPower unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetAPTxPower " + txPower.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public SByte GetAPTxPower()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.s8ApTxPower;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetFrameProfile(enFrameProfile profile, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.frmProfile = profile;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetFrameProfile unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetFrameProfile " + profile.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enFrameProfile GetFrameProfile()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.frmProfile;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetMaxMotes(ushort maxMotes, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16MaxMotes = maxMotes;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetMaxMotes unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetMaxMotes " + maxMotes.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetMaxMotes()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u16MaxMotes;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetBaseBandwidth(ushort baseBandwidth, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16BaseBandwidth = baseBandwidth;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBaseBandwidth unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetBaseBandwidth " + baseBandwidth.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetBaseBandwidth()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u16BaseBandwidth;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetDownFrameMultVal(byte downFrameMultVal, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u8DownFrameMultVal = downFrameMultVal;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetDownFrameMultVal unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetDownFrameMultVal " + downFrameMultVal.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public byte GetDownFrameMultVal()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u8DownFrameMultVal;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetNumParents(byte numParents, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u8NumParents = numParents;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetNumParents unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetNumParents " + numParents.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public byte GetNumParents()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u8NumParents;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetCcaMode(enCcaMode mode, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.ccaMode = mode;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetCcaMode unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetCcaMode " + mode.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enCcaMode GetCcaMode()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.ccaMode;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetChannelList(ushort channelList, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16ChannelList = channelList;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;
                
                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetChannelList unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetChannelList " + channelList.ToString("X4"));
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetChannelList()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u16ChannelList;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetAutoStartNetwork(bool auto, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.bAutoStartNetwork = auto;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetAutoStartNetwork unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetAutoStartNetwork " + auto.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public bool GetAutoStartNetwork()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.bAutoStartNetwork;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetBackboneMode(enBBMode mode, byte size, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.bbMode = mode;
                NetworkConfigCopy.u8BBSize = size;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;
                urgent = true;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBackboneMode unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetBackboneMode " + mode.ToString() + " " + size);
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enBBMode GetBackboneMode()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.bbMode;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetBackboneSize(byte bbSize, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u8BBSize = bbSize;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBackboneSize unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetBackboneSize " + bbSize.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public byte GetBackboneSize()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u8BBSize;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetRadioTest(byte mode, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.isRadioTest = (mode == 0x00) ? false : true;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetRadioTest unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetRadioTest " + mode.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public bool GetRadioTest()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.isRadioTest;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetBwMult(ushort bwMult, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16BwMult = bwMult;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBwMult unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetBwMult " + bwMult.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetBwMult()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u16BwMult;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }
        public enURErrCode SetOneChannel(byte oneChannel, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u8OneChannel = oneChannel;
                element.cmd = enCmd.CMDID_SETNETWORKCONFIG;
                element.param = (object)NetworkConfigCopy;
               
                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetOneChannel unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetOneChannel " + oneChannel.ToString());
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public ushort GetOneChannel()
        {
            if (NetworkConfigCopy.bInit)
                return NetworkConfigCopy.u8OneChannel;
            else
                throw new Exception("NetworkConfig hasn't been initialized");
        }

        public enURErrCode ClearStatistics(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_CLEARSTATISTICS;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ClearStatistics unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ClearStatistics");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode ExchangeMoteJoinKey(tMAC mac, tSECKEY key, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tEXMOTEJOINKEY copy = new tEXMOTEJOINKEY();
                copy.mac.Assign(mac);
                copy.key.Assign(key);
                element.cmd = enCmd.CMDID_EXCHANGEMOTEJOINKEY;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ExchangeMoteJoinKey unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Exchange Mote(" + mac.ToHexString() + ")|JoinKey(" + key.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode ExchangeNetworkId(ushort networkId, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                NetworkConfigCopy.u16NetworkId = networkId;             
                element.cmd = enCmd.CMDID_EXCHANGENETWORKID;
                element.param = (object)NetworkConfigCopy.u16NetworkId;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ExchangeNetworkId unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Exchange NetworkId(" + networkId + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode RadiotestTx(tRADIOTESTTX param, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tRADIOTESTTX param1 = new tRADIOTESTTX();
                param1.Assign(param);
                element.cmd = enCmd.CMDID_RADIOTESTTX;
                element.param = (object)param1;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "RadiotestTx unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RadiotestTx");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode RadiotestRx(ushort chanMask, ushort duration, byte stationId, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tRADIOTESTRX param = new tRADIOTESTRX();
                param.chanMask = chanMask;
                param.duration = duration;
                param.stationId = stationId;
                element.cmd = enCmd.CMDID_RADIOTESTRX;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "RadiotestRx unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RadiotestRx");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetRadiotestStatistics(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETRADIOTESTSTATISTICS;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetRadiotestStatistics unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetRadiotestStatistics");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetACLEntry(tMAC mac, tSECKEY key, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSETACLENTRY copy = new tSETACLENTRY();
                copy.mac.Assign(mac);
                copy.key.Assign(key);
                element.cmd = enCmd.CMDID_SETACLENTRY;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetACLEntry unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetACLEntry MAC(" + mac.ToHexString() + ")|SECKEY(" + key.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetNextACLEntry(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_GETNEXTACLENTRY;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetNextACLEntry unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetNextACLEntry MAC(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode DeleteACLEntry(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_DELETEACLENTRY;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "DeleteACLEntry unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "DeleteACLEntry MAC(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode PingMote(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_PINGMOTE;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "PingMote unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "PingMote(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetLog(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_GETLOG;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetLog unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetLog MAC(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode StartNetwork(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_STARTNETWORK;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StartNetwork unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "StartNetwork");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetSystemInfo(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETSYSTEMINFO;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetSystemInfo unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSystemInfo");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetNextMoteConfig(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tGETMOTECONFIG param = new tGETMOTECONFIG();
                param.mac.Assign(mac);
                param.next = true;
                element.cmd = enCmd.CMDID_GETMOTECONFIG;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetNextMoteConfig unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eMesh, "GetNextMoteConfig(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetPathInfo(tMAC source, tMAC dest, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tGETPATHINFO param = new tGETPATHINFO();
                param.source.Assign(source);
                param.dest.Assign(dest);
                element.cmd = enCmd.CMDID_GETPATHINFO;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetPathInfo unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetPathInfo SRC(" + source.ToHexString() + ") to DST(" + dest.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetNextPathInfo(tMAC mac, enPathDirection filter, ushort pathId, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tGETNEXTPATHINFO param = new tGETNEXTPATHINFO();
                param.mac.Assign(mac);
                param.filter = filter;
                param.pathId = pathId;
                element.cmd = enCmd.CMDID_GETNEXTPATHINFO;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetNextPathInfo unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetNextPathInfo Mote(" + mac.ToHexString() + ")|Filter(" + filter + ")|PathId(" + pathId + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetAdvertising(enAdvState state, bool urgent= false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_SETADVERTISING;
                element.param = (object)state;

                if (!ReqBuffer.EnQ(element))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetAdvertising unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetAdvertising " + state);
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetDownstreamFrameMode(enDnstreamFrameMode mode, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_SETDOWNSTREAMFRAMEMODE;
                element.param = (object)mode;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetDownstreamFrameMode unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetDownstreamFrameMode " + mode);
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetManagerStatistics(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETMANAGERSTATISTICS;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetManagerStatistics unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetManagerStatistics ");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetTime(enSetimeTrigMode trig, tUTCTIMEL time, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSETTIME param = new tSETTIME();
                param.trig = trig;
                param.time = time;
                element.cmd = enCmd.CMDID_SETTIME;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetTime unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetTime TrigMode(" + trig + ")|Seconds(" + time.u64Seconds + ")|Microseconds(" + time.u32Microseconds + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetLicense(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETLICENSE;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetLicense unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetLicense");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetLicense(tLICENSE license, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tLICENSE copy = new tLICENSE();
                copy.Assign(license);
                element.cmd = enCmd.CMDID_SETLICENSE;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetLicense unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetLicense(" + license.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetCLIUser(enCLIUserRole role, tUSERPW password, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSETCLIUSER param = new tSETCLIUSER();
                param.role = role;
                param.password.Assign(password);
                element.cmd = enCmd.CMDID_SETCLIUSER;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetCLIUser unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetCLIUser");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SendIP(tMAC mac, enPktPriority priority, byte options, byte encryptedOffset, byte[] data, bool urgent = false)
        {
            throw new NotImplementedException();
        }
        public enURErrCode GetMoteInfo(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_GETMOTEINFO;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetMoteInfo unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMoteInfo(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetNetworkInfo(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETNETWORKINFO;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetNetworkInfo unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetNetworkInfo");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetMoteConfigById(ushort moteId, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETMOTECONFIGBYID;
                element.param = (object)moteId;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetMoteConfigById unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMoteConfigById(" + moteId + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetCommonJoinKey(tSECKEY key, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSECKEY secKey = new tSECKEY();
                element.cmd = enCmd.CMDID_SETCOMMONJOINKEY;
                element.param = (object)secKey;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetCommonJoinKey unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetCommonJoinKey(" + key.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetIPConfig(bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                element.cmd = enCmd.CMDID_GETIPCONFIG;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetIPConfig unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetIPConfig");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode SetIPConfig(tIPV6ADDR ipv6Address, tIPV6MASK mask, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tSETIPCONFIG param = new tSETIPCONFIG();
                param.ipv6Address.Assign(ipv6Address);
                param.mask.Assign(mask);
                element.cmd = enCmd.CMDID_SETIPCONFIG;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "SetIPConfig unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetIPConfig IPV6ADDR(" + ipv6Address.ToHexString() + ")|IPV6MASK(" + mask.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode DeleteMote(tMAC mac, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tMAC copy = new tMAC();
                copy.Assign(mac);
                element.cmd = enCmd.CMDID_DELETEMOTE;
                element.param = (object)copy;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "DeleteMote unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "DeleteMote(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
        public enURErrCode GetMoteLinks(tMAC mac, ushort idx, bool urgent = false)
        {
            lock (m_lockAcceptRequest)
            {
                UserRequestElement element = new UserRequestElement();
                tGETMOTELINKS param = new tGETMOTELINKS();
                param.mac.Assign(mac);
                param.idx = idx;
                element.cmd = enCmd.CMDID_GETMOTELINKS;
                element.param = (object)param;

                if (!ReqBuffer.EnQ(element, urgent))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "GetMoteLinks unadmissible");
                    return enURErrCode.ERR_MORE_REQUEST;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMoteLinks(" + mac.ToHexString() + ")");
                    return enURErrCode.ERR_NONE;
                }
            }
        }
    }
}
