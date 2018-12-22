using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    // 定义会话状态改变委托类
    public delegate void SessionChangedEventHandler();

    /// <summary>
    /// 会话层定义，源码sm_lib中无显式定义会话层。
    /// iMesh实现中显示定义
    /// </summary>
    class Session : IStackLayer
    {
        // Manager最大重启所需时间
        private const int MAX_MGR_RESET_TIME = 10 * 60 * 1000;
        // Manager重启后发送握手信号的间隔时间
        private const int MGR_HELLO_INTERVAL = 10 * 1000;
        // 新会话超时时间
        private const int NEW_SESSION_TIMEOUT = 1000;

        private Serial m_Ser = null;
        // 同步因子
        private object m_SessLockObj = new object();
        // Serial层通知Session层新的会话已经建立，即收到Manager侧的HELLO_RESPONSE报文
        private AutoResetEvent m_sigConnected = new AutoResetEvent(false);
        // Serial层通知Session层当前会话已经断开，即收到Manager侧的MGR_HELLO报文
        private AutoResetEvent m_sigDisconnected = new AutoResetEvent(false);
        // 会话连接时有信号，会话断开时无信号
        private ManualResetEvent m_sigSessionOK = new ManualResetEvent(false);
        // 当前会话状态
        private volatile enSerStatus m_Status = enSerStatus.SER_ST_DISCONNECTED;
        public enSerStatus Status
        {
            get { lock (m_SessLockObj) { return m_Status; } }
            set { lock (m_SessLockObj) { m_Status = value; } }
        }        
        /// <summary>
        /// 定义会话失连通知函数
        /// </summary>
        public SessionChangedEventHandler SessionLost = null;
        /// <summary>
        /// 定义会话建立通知函数
        /// </summary>
        public SessionChangedEventHandler SessionBuilt = null;

        public Session(Serial ser)
        {
            if (ser != null)
            {
                m_Ser = ser;
                m_Ser.StatusChanged += SessionStatusChanged;
            }
        }

        public bool IsConnected
        {
            get
            {
                //m_sigSessionOK.Reset();
                if (m_sigSessionOK.WaitOne(MAX_MGR_RESET_TIME))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 会话状态变更回调函数，Serial层通过此函数通知Session层，当前会话状态变更
        /// </summary>
        /// <param name="newStatus">新的会话状态</param>
        private void SessionStatusChanged(enSerStatus newStatus)
        {
            switch (newStatus)
            {
                case enSerStatus.SER_ST_DISCONNECTED:
                {
                    Status = newStatus;
                    CommStackLog.RecordInf(enLogLayer.eSession, "Disconnected!");
                    m_sigDisconnected.Set();
                    m_sigConnected.Reset();
                    m_sigSessionOK.Reset();
                    // 然后通知上层新的会话需求
                    if (SessionLost != null)
                        SessionLost();

                    break;
                }
                case enSerStatus.SER_ST_CONNECTED:
                {
                    Status = newStatus;
                    CommStackLog.RecordInf(enLogLayer.eSession, "Connected!");
                    m_sigDisconnected.Reset();
                    // 新的会话构建完成，设置通知信号量
                    m_sigConnected.Set();
                    m_sigSessionOK.Set();

                    if (SessionBuilt != null)
                        SessionBuilt();
                    
                    break;
                }
                default:
                    break;
            }
        }
        /// <summary>
        /// 此处当前的判定依据是收到MgrHello报文，
        /// 是不是更合适的判定Manager已经重启的方式是等待networkReset通知事件？
        /// </summary>
        public bool ManagerSentMrgHello
        {
            get
            {
                if (m_sigDisconnected.WaitOne(MGR_HELLO_INTERVAL))
                    return true;
                else
                    return false;
            }
        }

        private void ResetHardware()
        {
            ;
        }
        /// <summary>
        /// 建立新的会话，直到会话建立返回
        /// </summary>
        public bool Open()
        {
            m_sigConnected.Reset();
            m_Ser.InitiateConnect();
            if (!m_sigConnected.WaitOne(NEW_SESSION_TIMEOUT))
            {
                CommStackLog.RecordErr(enLogLayer.eSession, "Open session timeout(" + NEW_SESSION_TIMEOUT + "ms)");
                return false;
            }
            return true;
        }

        #region IStackLayer 实现
        public void Initialize()
        {
        }
        public void Reset()
        {
        }
        #endregion
    }
}
