using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    internal enum enSerStatus
    {
        SER_ST_DISCONNECTED = 0x00,
        SER_ST_HELLO_SENT = 0x01,
        SER_ST_CONNECTED = 0x02,
    }

    // 定义状态改变委托类
    internal delegate void DlgtStatusChanged(enSerStatus newStatus);
    // 定义响应回调委托类
    public delegate void DlgtReply(enCmd cmdId, eRC rc, byte[] payload, byte len);
    // 定义请求回调委托类
    public delegate void DlgtRequest(enCmd cmdId, byte flags, byte[] payload, byte len);

    /// <summary>
    /// 串行层
    /// </summary>
    internal class Serial : IStackLayer, IDisposable
    {
        public const byte API_VERSION              = 4;
        public const byte FLAG_DATA                = (0 << 0);//0000,0000
        public const byte FLAG_ACK                 = (1 << 0);//0000,0001
        public const byte FLAG_UNACKNOWLEDGED      = (0 << 1);//0000,0000
        public const byte FLAG_ACKNOWLEDGED        = (1 << 1);//0000,0010
        public const byte HELLO_RESP_OFFS_RC       = 0;
        public const byte HELLO_RESP_OFFS_VERSION  = 1;
        public const byte HELLO_RESP_OFFS_MGRSEQNO = 2;
        public const byte HELLO_RESP_OFFS_CLISEQNO = 3;
        public const byte HELLO_RESP_OFFS_MODE     = 4;
        private const byte SER_RX_PAYLOAD_MAX_SIZE = 128;

        /// <summary>
        /// 当前SER(链路)层的状态
        /// </summary>
        private volatile enSerStatus m_SerStatus = enSerStatus.SER_ST_DISCONNECTED;
        /// <summary>
        /// 发送的报文id
        /// </summary>
        private volatile byte m_u8TxPacketId = 0;
        /// <summary>
        /// 发送的报文id
        /// </summary>
        public byte TxPacketId
        {
            get { return m_u8TxPacketId; }
        }
        /// <summary>
        /// 接收的报文id，最初保存HelloResponse报文中的mgrSeqNo
        /// </summary>
        private volatile byte m_u8RxPacketId = 0;
        /// <summary>
        /// 间接表示是否已经建立会话
        /// </summary>
        private volatile bool m_bRxPacketIdInit = false;
        /// <summary>
        /// 接收帧的控制字段
        /// </summary>
        private volatile byte m_u8RxControl = 0;
        /// <summary>
        /// 接收帧的命令ID
        /// </summary>
        private volatile enCmd m_RxCmdId = enCmd.CMDID_END;
        /// <summary>
        /// 接收帧的序列码
        /// </summary>
        private volatile byte m_u8RxSeqNum = 0;
        /// <summary>
        /// 接收帧的负载数据长度
        /// </summary>
        private volatile byte m_u8RxPyldLen = 0;
        /// <summary>
        /// 接收帧的负载数据
        /// </summary>
        private volatile byte[] m_u8aRxPayload = new byte[SER_RX_PAYLOAD_MAX_SIZE];
        /// <summary>
        /// 响应帧除去错误码外的数据负载长度
        /// </summary>
        private volatile byte m_u8RxDataLen = 0;
        /// <summary>
        /// 响应帧除去错误码外的数据负载数据
        /// </summary>
        private volatile byte[] m_u8aRxData = new byte[SER_RX_PAYLOAD_MAX_SIZE];
        /// <summary>
        /// 上层调用的请求id，本层缓存以验证响应消息的一致性
        /// </summary>
        private volatile enCmd m_ReqCmdId = enCmd.CMDID_END;
        /// <summary>
        /// 定义MeshAPI响应回调委托实例
        /// </summary>
        private volatile DlgtReply m_evReplyArrived = null;
        /// <summary>
        /// 最后一次收到MgrHello的时间
        /// </summary>
        private DateTime m_dtLastMgrHello = DateTime.Now;
        /// <summary>
        /// 记录两次心跳之间串行层接收到的报文个数
        /// </summary>
        private volatile Int32 m_i32RxFrameCnt = 0;
        /// <summary>
        /// 记录两次心跳之间串行层接收到的报文个数
        /// </summary>
        public Int32 RxFrameCnt
        {
            get { return m_i32RxFrameCnt; }
            set { m_i32RxFrameCnt = 0; }
        }
        /// <summary>
        /// HDLC层组件对象
        /// </summary>
        private volatile HDLC m_hdlcore = new HDLC();
        /// <summary>
        /// 保证发送报文的原子性
        /// </summary>
        private volatile object lockOutpacket = new object();
        /// <summary>
        /// 表示会话状态变更事件的方法
        /// </summary>
        private DlgtStatusChanged m_evStatusChanged = null;
        /// <summary>
        /// 表示会话状态变更事件的方法
        /// </summary>
        public event DlgtStatusChanged StatusChanged
        {
            add { m_evStatusChanged += value; }
            remove { m_evStatusChanged -= value; }
        }
        /// <summary>
        /// 表示网络请求事件的方法
        /// </summary>
        private DlgtRequest m_evRequestArrived = null;
        /// <summary>
        /// 表示网络请求事件的方法
        /// </summary>
        public event DlgtRequest RequestArrived
        {
            add { m_evRequestArrived += value; }
            remove { m_evRequestArrived -= value; }
        }
        /// <summary>
        /// 最后一次发送数据的时刻值
        /// </summary>
        private DateTime m_dtLastSend = DateTime.Now;
        /// <summary>
        /// [可配置]最小输出帧间隔时间
        /// </summary>
        private int cfgMinOutFrameInterval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinOutFrameInterval"].ToString());
        /// <summary>
        /// 构造函数
        /// </summary>
        public Serial()
        {
            m_hdlcore.HDLCFrameArrived += rxHdlcFrame;
        }

        /// <summary>
        /// 进行会话初始化工作，即发送Hello报文
        /// </summary>
        /// <returns>错误码</returns>
        public enErrCode InitiateConnect()
        {
            byte[] payload = new byte[3];
            // 重置client sequence number
            m_u8TxPacketId = 0;
            // prepare hello packet
            payload[0] = API_VERSION;     // version
            payload[1] = m_u8TxPacketId;  // cliSeqNo
            payload[2] = 0;               // mode
            // send hello packet
            sendRequestNoCheck(enCmd.CMID_HELLO,// cmdId
                               false,           // isAck
                               false,           // shouldBeAcked
                               payload,         // payload
                               3,               // length
                               null);           // replyCb
            // remember state
            m_SerStatus = enSerStatus.SER_ST_HELLO_SENT;
            return enErrCode.ERR_NONE;
        }

        /// <summary>
        /// 检查接收的报文命令码是否正确
        /// </summary>
        /// <param name="cmd">接受的报文命令码</param>
        /// <returns>是否有效的命令码</returns>
        private bool isInvalidCmd(byte cmd)
        {
            if (cmd >= (byte)enCmd.CMDID_END)
                return true;
            else if (0x04 <= cmd && cmd <= 0x13)
                return true;
            else if (0x18 <= cmd && cmd <= 0x19)
                return true;
            else if (0x1B <= cmd && cmd <= 0x1E)
                return true;
            else if (0x20 == cmd || 0x24 == cmd || 0x34 == cmd || 0x39 == cmd || 0x3C == cmd)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 输出一帧数据
        /// </summary>
        /// <param name="control">输出帧的控制字段</param>
        /// <param name="cmdId">输出帧的命令ID</param>
        /// <param name="packetId">输出帧的报文ID</param>
        /// <param name="len">输出帧的负载长度</param>
        /// <param name="payload">输出帧的负载</param>
        /// <param name="isAck">输出帧是否为响应报文</param>
        /// <returns>输出成功与否</returns>
        private bool output1Frame(byte control,
                                  byte cmdId,
                                  byte packetId,
                                  byte len,
                                  byte[] payload,
                                  bool isAck = false)
        {
            lock (lockOutpacket)
            {
                if (payload.Length < len)
                {
                    CommStackLog.RecordErr(enLogLayer.eSerial, "Frame payload length is shorter than expected");
                    return false;
                }

                TimeSpan tsSendInterval = DateTime.Now - m_dtLastSend;
                if (tsSendInterval.TotalMilliseconds < cfgMinOutFrameInterval)
                    Thread.Sleep((int)(cfgMinOutFrameInterval - tsSendInterval.TotalMilliseconds + 1));

                try
                {
                    m_hdlcore.OutputOpen();
                    m_hdlcore.OutputWrite(control);         // Control
                    m_hdlcore.OutputWrite(cmdId);           // Packet Type
                    m_hdlcore.OutputWrite(packetId);        // Seq. Number
                    m_hdlcore.OutputWrite(len);             // Payload Length
                    for (int i = 0; i < len; i++)           // Payload
                        m_hdlcore.OutputWrite(payload[i]);
                    m_hdlcore.OutputClose();
                    // 更新最近发送的时刻值
                    m_dtLastSend = DateTime.Now;
                    return true;
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eSerial, ex.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        /// 发送请求去报文
        /// </summary>
        /// <param name="cmdId">输出报文的命令ID</param>
        /// <param name="isAck">输出报文是否为响应报文</param>
        /// <param name="shouldBeAcked">输出报文是否需要响应</param>
        /// <param name="payload">输出报文的负载</param>
        /// <param name="length">输出报文的负载长度</param>
        /// <param name="replyCb">响应回调函数</param>
        /// <param name="bRetry">是否为重发报文</param>
        /// <returns>错误码</returns>
        private enErrCode sendRequestNoCheck(enCmd     cmdId,
                                             bool      isAck,
                                             bool      shouldBeAcked,
                                             byte[]    payload,
                                             byte      length,
                                             DlgtReply replyCb,
                                             bool      bRetry = false)
        {
            byte control = 0;
            // register reply callback
            m_ReqCmdId = cmdId;
            m_evReplyArrived = replyCb;

            // create the control byte
            if (isAck)
                control |= FLAG_ACK;
            else
                control |= FLAG_DATA;

            if (shouldBeAcked)
                control |= FLAG_ACKNOWLEDGED;
            else
                control |= FLAG_UNACKNOWLEDGED;

            lock (m_hdlcore)
            {
                // send the frame over serial
                if (bRetry)
                    m_u8TxPacketId--;

                if (!output1Frame(control, (byte)cmdId, m_u8TxPacketId, length, payload))
                    return enErrCode.ERR_INVALID_PARAM;
                // increment the txPacketId
                m_u8TxPacketId++;
            }

            return enErrCode.ERR_NONE;
        }

        /// <summary>
        /// 发送请求去报文
        /// </summary>
        /// <param name="cmdId">输出报文的命令ID</param>
        /// <param name="isAck">输出报文是否为响应报文</param>
        /// <param name="payload">输出报文的负载</param>
        /// <param name="length">输出报文的负载长度</param>
        /// <param name="replyCb">响应回调函数</param>
        /// <param name="bRetry">是否为重发报文</param>
        /// <returns>错误码</returns>
        public enErrCode SendRequest(enCmd     cmdId,
                                     bool      isAck,
                                     byte[]    payload,
                                     byte      length,
                                     DlgtReply replyCb,
                                     bool      bRetry = false)
        {
            // abort if not connected
            if (m_SerStatus != enSerStatus.SER_ST_CONNECTED)
                return enErrCode.ERR_NOT_CONNECTED;
            // send the request
            return sendRequestNoCheck(cmdId, isAck, !isAck, payload, length, replyCb, bRetry);
        }

        /// <summary>
        /// 分发响应
        /// </summary>
        /// <param name="cmdId">响应报文命令ID</param>
        /// <param name="payload">响应负载</param>
        /// <param name="length">响应负载长度</param>
        private void dispatchResponse(enCmd cmdId, byte[] payload, byte length)
        {
            eRC rc = (eRC)payload[0];
            m_u8RxDataLen = (byte)(length - 1);
            Array.Copy(payload, 1, m_u8aRxData, 0, m_u8RxDataLen);
            // 检查接收到的命令id是否与请求的id相同
            if (cmdId == m_ReqCmdId)
            {
                if (m_evReplyArrived != null)
                {
                    // call the callback
                    m_evReplyArrived(cmdId, rc, m_u8aRxData, m_u8RxDataLen);
                    // reset
                    m_ReqCmdId = enCmd.CMDID_END;
                    // Kous: 注销事件处理函数的方式可能存在问题
                    m_evReplyArrived = null;
                    // Kous: 清空接收数据缓存
                    Array.Clear(m_u8aRxData, 0, m_u8RxDataLen);
                    m_u8RxDataLen = 0;
                }
                else
                    CommStackLog.RecordErr(enLogLayer.eSerial, "m_evReplyArrived=null");
            }
            else
                CommStackLog.RecordErr(enLogLayer.eSerial, "cmdId=" + cmdId.ToString() + " m_ReqCmdId=" + m_ReqCmdId.ToString());
        }

        /// <summary>
        /// 接受新的数据帧解析函数
        /// </summary>
        /// <param name="rxFrame">帧数据及</param>
        /// <param name="rxFrameLen">帧长度</param>
        private void rxHdlcFrame(byte[] rxFrame, byte rxFrameLen)
        {
            if (rxFrame == null)
                return;
            if (isInvalidCmd(rxFrame[1]))
            {
                CommStackLog.RecordErr(enLogLayer.eSerial, "Err cmd(0x" + rxFrame[1].ToString("X2") + ")");
                return;
            }

            bool isAck = false;
            bool shouldAck = false;
            bool isRepeatId = false;
            bool isNotifData = false;
            //接收报文的个数
            m_i32RxFrameCnt++;
            // parse header
            m_u8RxControl = rxFrame[0];
            m_RxCmdId = (enCmd)rxFrame[1];
            m_u8RxSeqNum = rxFrame[2];
            m_u8RxPyldLen = rxFrame[3];
            enNotifyType notType = (enNotifyType)rxFrame[4];

            if (m_RxCmdId == enCmd.CMDID_NOTIFICATION
                && (notType == enNotifyType.NOTIFID_NOTIFDATA || notType == enNotifyType.NOTIFID_NOTIFEVENT))
                isNotifData = true;

            // 提取出HDLC帧中负载数据
            Array.Copy(rxFrame, 4, m_u8aRxPayload, 0, m_u8RxPyldLen);
            // 解析接受到的HDLC帧是响应帧还是数据帧(请求帧)
            isAck = ((m_u8RxControl & FLAG_ACK) != 0);
            // 如果HDLC帧是数据帧解析接受到的HDLC帧是否需要应答
            shouldAck = ((m_u8RxControl & FLAG_ACKNOWLEDGED) != 0);
            // 应答报文
            if (isAck)
            {
                // 通知上层响应帧到达
                if (m_u8RxPyldLen > 0)
                    // Kous: 是上层应用请求的响应报文，回调上层应用定义的响应通知函数
                    dispatchResponse(m_RxCmdId, m_u8aRxPayload, m_u8RxPyldLen);
            }
            // 请求报文
            else
            {
                // 非数据通知时才检测更新m_u8RxPacketId
                if (!isNotifData)
                {
                    // 是否为重复请求报文
                    if (m_bRxPacketIdInit && m_u8RxSeqNum == m_u8RxPacketId)
                    {
                        CommStackLog.RecordInf(enLogLayer.eSerial, "SO(" + m_u8RxSeqNum.ToString() + ") repeats");
                        isRepeatId = true;
                    }
                    else
                    {
                        isRepeatId = false;
                        m_bRxPacketIdInit = true;
                        m_u8RxPacketId = m_u8RxSeqNum; // 记录接收到的报文序列号
                    }
                }
                // 如果报文是需要响应的报文，则在Ser层直接回应
                if (shouldAck)
                {
                    byte len = 1;
                    byte[] payload = new byte[len];
                    payload[0] = (byte)eRC.RC_OK;
                    //Thread.Sleep(20);
                    output1Frame(FLAG_ACK | FLAG_ACKNOWLEDGED, (byte)m_RxCmdId, m_u8RxPacketId, len, payload, true);
                }

                switch (m_RxCmdId)
                {
                    case enCmd.CMID_HELLO_RESPONSE:
                    {
                        if (m_u8RxPyldLen >= 5
                            && m_u8aRxPayload[HELLO_RESP_OFFS_RC] == 0
                            && m_u8aRxPayload[HELLO_RESP_OFFS_VERSION] == API_VERSION)
                        {
                            // change state
                            m_SerStatus = enSerStatus.SER_ST_CONNECTED;
                            // record manager sequence number
                            m_bRxPacketIdInit = true;
                            m_u8RxPacketId = m_u8aRxPayload[HELLO_RESP_OFFS_MGRSEQNO];
                            // 通知会话层新的会话已经建立
                            if (m_evStatusChanged != null)
                                m_evStatusChanged(m_SerStatus);

                            // 新的会话建立完成，则下发CLI命令
                            m_hdlcore.MoniterCli();
                        };
                        break;
                    }
                    case enCmd.CMID_MGR_HELLO:
                    {
                        // 以下8行用于过滤短时间内收到的重复MgrHello报文
                        TimeSpan tsMgrHello = DateTime.Now - m_dtLastMgrHello;
                        m_dtLastMgrHello = DateTime.Now;
                        // 4秒内收到的MgrHello认为为重复
                        if (tsMgrHello.TotalMilliseconds < 2000)
                        {
                            CommStackLog.RecordInf(enLogLayer.eSerial, "Redundant MgrHello");
                            break;
                        }
                        // 以上8行用于过滤短时间内收到的重复MgrHello报文

                        if (m_u8RxPyldLen >= 2)
                        {
                            // change state
                            m_SerStatus = enSerStatus.SER_ST_DISCONNECTED;
                            // 通知会话层新的当前会话已经失效
                            if (m_evStatusChanged != null)
                                m_evStatusChanged(m_SerStatus);
                        }
                        break;
                    }
                    default:
                    {
                        // dispatch
                        //if (m_u8RxPyldLen > 0 && m_evRequestArrived != null && isRepeatId == false)
                        //    m_evRequestArrived(m_RxCmdId, m_u8RxControl, m_u8aRxPayload, m_u8RxPyldLen);
                        if (m_u8RxPyldLen > 0 && m_evRequestArrived != null)
                        {
                            if (isNotifData)
                                m_evRequestArrived(m_RxCmdId, m_u8RxControl, m_u8aRxPayload, m_u8RxPyldLen);
                            else if (isRepeatId == false)
                                m_evRequestArrived(m_RxCmdId, m_u8RxControl, m_u8aRxPayload, m_u8RxPyldLen);
                        }

                        break;
                    }
                }

                // Kous: 清空接收负载缓存
                Array.Clear(m_u8aRxPayload, 0, m_u8RxPyldLen);
                m_u8RxPyldLen = 0;
            }
        }

        /// <summary>
        /// 软件重启Manager
        /// </summary>
        public void ResetManager()
        {
            m_hdlcore.ResetManager();
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (m_hdlcore != null)
            {
                m_hdlcore.Dispose();
            }
        }
        #endregion

        #region IStackLayer 实现
        public void Initialize()
        {
            m_hdlcore.Initialize();
        }
        public void Reset()
        {
            m_hdlcore.Reset();
        }
        #endregion
    }
}
