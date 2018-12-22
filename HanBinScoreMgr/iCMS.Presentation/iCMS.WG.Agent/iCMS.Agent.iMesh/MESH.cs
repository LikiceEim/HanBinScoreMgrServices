using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    // 定义通知到达委托类
    internal delegate void DlgtNotify(enNotifyType notType, enEventType eventType, NotifBase notObj/*, byte len*/);
    // 定义请求响应到达委托类
    internal delegate void DlgtMeshApiResponse(enCmd cmd, tEcho result);

    /// <summary>
    /// Kous根据凌特方提供代码翻译成C#类
    /// </summary>
    internal partial class MESH : IStackLayer, IDisposable
    {
        /// <summary>
        /// [常量]最大原始HDLC报文长度
        /// </summary>
        public const byte MAX_FRAME_LENGTH = 128;
        /// <summary>
        /// 最近的请求命令缓存
        /// </summary>
        private volatile enCmd m_u8CmdId = enCmd.CMDID_END;
        /// <summary>
        /// 通知报文去除每种通知类型的公共头后的最终数据负载
        /// </summary>
        private volatile byte[] m_u8aNotifBuf = new byte[MAX_FRAME_LENGTH];
        /// <summary>
        /// 输出数据缓存
        /// </summary>
        private volatile byte[] m_u8aOutputBuf = new byte[MAX_FRAME_LENGTH];
        /// <summary>
        /// 输出数据长度
        /// </summary>
        private volatile byte m_u8OutputBufLen = 0;
        /// <summary>
        /// 多线程同步锁
        /// </summary>
        private volatile object m_MeshLockObj = new object();
        /// <summary>
        /// 表示当前MESH层正在处理网络请求
        /// </summary>
        private volatile bool m_bBusyTx = false;
        /// <summary>
        /// Adapter层向本层传递UserRequestElement
        /// </summary>
        public UserRequestElement Adapter2MeshBridge = null;
        /// <summary>
        /// 异步命令回调ID记录
        /// </summary>
        public volatile Dictionary<UInt32, AsyncCmdRespEntry> m_dicAsyncCmdRespRecords = new Dictionary<UInt32, AsyncCmdRespEntry>();
        /// <summary>
        /// 链路层核心
        /// </summary>
        private Serial m_Ser = null;
        /// <summary>
        /// 表示Manager压力缓解的信号量
        /// </summary>
        public AutoResetEvent m_asigEased = new AutoResetEvent(false);
        /// <summary>
        /// 信号量表示当前请求的响应已经到达，可以进行下一请求的处理
        /// </summary>
        private AutoResetEvent m_asigMgrResponsed = new AutoResetEvent(false);
        /// <summary>
        /// [可配置]请求到Manager响应之间的等待超时时间
        /// </summary>
        private int cfgAgtReq2MgrRepTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AgtReq2MgrRepTimeout"].ToString());
        /// <summary>
        /// [可配置]请求到Manager响应之间的等待超时时间
        /// </summary>
        public int AgtReq2MgrRepTimeout
        {
            get { return cfgAgtReq2MgrRepTimeout; }
        }
        /// <summary>
        /// 表示网络通知事件的方法
        /// </summary>
        private DlgtNotify m_evNotifyArrived = null;
        /// <summary>
        /// 表示网络通知事件的方法
        /// </summary>
        public event DlgtNotify NotifyArrived
        {
            add { m_evNotifyArrived += value; }
            remove { m_evNotifyArrived -= value; }
        }
		/// <summary>
        /// 各种请求对应的响应回调函数
        /// </summary>
        public DlgtMeshApiResponse ReplyArrived = null;
        /// <summary>
        /// 表示上层调用QueryAllWs
        /// </summary>
        private volatile bool m_bQueryAllWs = false;
        public bool IsQueryingAllWs
        {
            get { lock (this) { return m_bQueryAllWs; } }
            set { lock (this) { m_bQueryAllWs = value; } }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ser"></param>
        public MESH(Serial ser)
        {
            if (ser != null)
            {
                m_Ser = ser;
                m_Ser.RequestArrived += MeshNotify;
            }
        }
        /// <summary>
        /// 主动释放等待信号
        /// </summary>
        public void ReleaseWaitResponse()
        {
            m_asigMgrResponsed.Set();
        }
        /// <summary>
        /// 重置等待信号
        /// </summary>
        public void ResetWaitResponse()
        {
            m_asigMgrResponsed.Reset();
        }
        /// <summary>
        /// API请求等待Manager响应，超时时间为cfgAgtReq2MgrRepTimeout
        /// </summary>
        /// <returns>成功等待与否</returns>
        public bool WaitMgrResponse()
        {
            if (m_asigMgrResponsed.WaitOne(AgtReq2MgrRepTimeout))
            {
                m_asigMgrResponsed.Reset();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 取消当前正在处理的网络请求
        /// </summary>
        public void CancelTx()
        {
            m_bBusyTx = false;
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (m_Ser != null)
            {
                m_Ser.Dispose();
            }
        }
        #endregion

        #region IStackLayer 实现
        public void Initialize()
        {
            if (m_Ser != null)
            {
                m_Ser.Initialize();
            }
            else
            {
                CommStackLog.RecordErr(enLogLayer.eMesh, "Serial is not bound to MESH");
            }
        }

        public void Reset()
        {
            // 重置底层
            if (m_Ser != null)
                m_Ser.Reset();

            m_bBusyTx = false;
            m_u8CmdId = enCmd.CMDID_END;
            m_u8OutputBufLen = 0;
            Adapter2MeshBridge = null;
            lock (m_dicAsyncCmdRespRecords)
            {
                m_dicAsyncCmdRespRecords.Clear();
            }

            m_asigMgrResponsed.Reset();
        }

        #endregion
    }
}
