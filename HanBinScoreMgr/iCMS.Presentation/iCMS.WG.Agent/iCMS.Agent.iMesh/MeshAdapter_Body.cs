using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 定义WS连接状态改变委托类
    /// </summary>
    /// <param name="mote"></param>
    public delegate void WsConnStatusChangedEventHandler(tMAC mote);
    /// <summary>
    /// 定义Manager重启通知委托类
    /// </summary>
    public delegate void ManagerResetEventHandler();

    public partial class MeshAdapter : IStackLayer, IDisposable
    {
        /// <summary>
        /// [常量]Manager最大重启次数
        /// </summary>
        public const byte RESET_MANAGER_MAX_TIME = 3;
        /// <summary>
        /// [常量]应用层头信息的字节个数
        /// </summary>
        public const byte APP_HEAD_LEN = 7;
        /// <summary>
        /// [常量]通知的头信息长度
        /// </summary>
        public const byte NOTIFY_HEAD_LEN = 25;
        /// <summary>
        /// [常量]等待Manager生成重启通知事件的超时时间
        /// </summary>
        public const int WAIT_MGR_RSTEVT_TIMEOUT = 1000;
        /// <summary>
        /// [常量]表示同步等待命令响应的最大时间
        /// </summary>
        private const int SYC_WAIT_MAX_TIME = 60000;
        /// <summary>
        /// 等待异步命令通知检测超时间隔时间
        /// </summary>
        private const int TIMEOUT_PRECISION = 20;
        /// <summary>
        /// 会话层实体
        /// </summary>
        private Session m_Session = null;
        /// <summary>
        /// 网络层实体
        /// </summary>
        private MESH m_Mesh = null;
        /// <summary>
        /// 串行层实体
        /// </summary>
        private Serial m_Serial = null;
        /// <summary>
        /// 凌特网络管理器实体
        /// </summary>
        private ManagerOperator m_Manager = null;
        /// <summary>
        /// 组件内部调用“订阅”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigSubs = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“获取网络配置”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigGetNwCfg = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“获取网络时间”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigGetTime = new AutoResetEvent(false);
        /// <summary>
        /// 心跳机制使用GetMoteConfig时，此信号表示相依到达或者，超时置位
        /// </summary>
        private AutoResetEvent m_asigGetMtCfg = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“订阅”命令的响应超时标志
        /// </summary>
        private volatile bool m_bSubsTimeout = false;
        /// <summary>
        /// 组件内部调用“获取网络配置”命令的响应超时标志
        /// </summary>
        private volatile bool m_bGetNwCfgTimeout = false;
        /// <summary>
        /// 组件内部调用“获取网络时间”命令的响应超时标志
        /// </summary>
        private volatile bool m_bGetTimeTimeout = false;
        /// <summary>
        /// 心跳机制使用GetMoteConfig时，此变量变量表示是否超时
        /// </summary>
        private volatile bool m_bGetMtCfgTimeout = false;
        /// <summary>
        /// 缓存网络中目前在线的WS，即升级时需要禁止上传数据的WS列表
        /// </summary>
        private volatile Dictionary<string, bool> m_dicOnLineWs = new Dictionary<string, bool>();
        /// <summary>
        /// 应用层请求命令等待WS应用层响应队列
        /// </summary>
        private volatile Dictionary<UserRequestElement, bool> m_dicWaitAppRespCmds = new Dictionary<UserRequestElement, bool>();
        //private DateTime ADBaseTime = new DateTime(0, 0, 0, 0, 0, 0);
        /// <summary>
        /// SmartMesh网络默认的UTC起始基时间，实则为GMT时区
        /// </summary>
        private DateTime UtcBaseTime = new DateTime(1970, 1, 1, 0, 0, 0);
        /// <summary>
        /// SmartMesh网络默认的UTC起始基时间，北京时区
        /// </summary>
        private DateTime UtcBaseTime_BIJ = new DateTime(1970, 1, 1, 8, 0, 0);
        /// <summary>
        /// SmartMesh网络默认的起始计时基准时间，太平洋时间(PST)
        /// </summary>
        private DateTime SMPSTBaseTime = new DateTime(2002, 7, 2, 20, 0, 0);
        /// <summary>
        /// SmartMesh网络默认的起始计时基准时间(2002/7/2 20:00:00 PST)转换成UTC时间(2002/7/3 4:00:00 GMT)
        /// </summary>
        private DateTime SMUTCBaseTime = new DateTime(2002, 7, 3, 4, 0, 0);
        /// <summary>
        /// SmartMesh网络默认的起始计时基准时间(2002/7/2 20:00:00 PST)转换成北京时间(2002/7/3 12:00:00 BIJ)
        /// </summary>
        private DateTime SMBEIJINGBaseTime = new DateTime(2002, 7, 3, 12, 0, 0);

        /// <summary>
        /// 供调用者使用的校准参数
        /// </summary>
        private UInt64 calibrationTime = 0;
        /// <summary>
        /// 供调用者使用的校准参数
        /// </summary>
        public UInt64 CalibrationTime
        {
            get
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "CaliTime(" + calibrationTime  + ")");
                return calibrationTime;
            }
        }
        /// <summary>
        /// 当前Manager的MAC地址
        /// </summary>
        private volatile string m_macManager = string.Empty;
        /// <summary>
        /// 当前Manager的MAC地址
        /// </summary>
        public string ManagerMacaddr
        {
            get { return m_macManager; }
        }
        /// <summary>
        /// 用户请求处理主线程启停控制事件
        /// </summary>
        private ManualResetEvent m_msigMainThreadRun = new ManualResetEvent(true);
        /// <summary>
        /// 用户请求响应处理主线程
        /// </summary>
        private Thread m_threadMshResponseWork = null;
        /// <summary>
        /// 用户请求处理主线程
        /// </summary>
        private Thread m_threadUserRequestWorker = null;
        /// <summary>
        /// 等待WS响应处理主线程
        /// </summary>
        private Thread m_threadWaitWsRspWorker = null;
        /// <summary>
        /// 系统心跳主线程
        /// </summary>
        private Thread m_threadHeartbeatWorker = null;
        /// <summary>
        /// 用户请求(响应)处理主线程运行与否
        /// </summary>
        private volatile bool m_bKeepRunning = true;
        /// <summary>
        /// 表示是否进行被动建立新的会话(Manager发送MgrHello报文)
        /// </summary>
        private volatile bool m_bPassiveNewSession = false;
        /// <summary>
        /// 表示当前是否启用新的线程处理MgrHello事件
        /// </summary>
        private volatile bool m_bProcessingNewSessin = false;
        /// <summary>
        /// 表示当前是否启用新的线程处理MgrHello事件
        /// </summary>
        //private byte m_u8ResetManagerTime = 0;
        /// <summary>
        /// [可配置]创建会话的重试次数
        /// </summary>
        private int cfgCreateSsnRetryCnt = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxRetryNewSsnCntForReset"].ToString());
        /// <summary>
        /// [可配置]心跳器的间隔时间
        /// </summary>
        private int cfgHeartbeatInterval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["HeartbeatInterval"].ToString());
        /// <summary>
        /// [可配置]用户请求重试次数
        /// </summary>
        private int cfgUserRequestRetryTimes = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UserRequestRetryTimes"].ToString());
        /// <summary>
        /// [可配置]重试请求基本间隔时间
        /// </summary>
        private int cfgRetryBaseTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RetryBaseTime"].ToString());
        /// <summary>
        /// [可配置]重试请求递增间隔时间
        /// </summary>
        private int cfgRetryMultTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RetryMultTime"].ToString());
        /// <summary>
        /// [可配置]针对异步命令，表示从Manager受理到命令处理完成的超时时间
        /// </summary>
        private int cfgAgtReq2MshRepTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AgtReq2MshRepTimeout"].ToString());
        /// <summary>
        /// [可配置]Agent请求到WS响应的超时时间
        /// </summary>
        private int cfgAgtReq2WsRepTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AgtReq2WsRepTimeout"].ToString());
        /// <summary>
        /// 心跳机制使用GetMoteConfig时，此变量表示访问对象WS在列表中的位置
        /// </summary>
        private volatile int m_i32MoniterWsIdx = 0;
        /// <summary>
        /// 心跳动作重试计数
        /// </summary>
        private volatile byte m_u8HeartbeatRetryCnt = 0;
        /// <summary>
        /// [常量]心跳动作重试次数
        /// </summary>
        private const int HEARTBEET_RETRY_TIMES = 3;
        /// <summary>
        /// 获取的Mote配置信息缓存
        /// </summary>
        private tGetMoteConfigEcho m_cacheGetMtCfgResult = new tGetMoteConfigEcho();
        /// <summary>
        /// 停止心跳线程标志位
        /// </summary>
        private volatile bool m_bHaultHeartbeat = false;
        /// <summary>
        /// 停止心跳线程同步信号
        /// </summary>
        private AutoResetEvent m_asigHaultHeartbeat = new AutoResetEvent(false);
        /// <summary>
        /// 通知上层Manager失连
        /// </summary>
        private SessionChangedEventHandler managerLost = null;
        /// <summary>
        /// 通知上层Manager失连
        /// </summary>
        public event SessionChangedEventHandler ManagerLost
        {
            add { managerLost += value; }
            remove { managerLost -= value; }
        }
        /// <summary>
        /// 通知上层Manager连接
        /// </summary>
        private SessionChangedEventHandler managerConnected = null;
        /// <summary>
        /// 通知上层Manager连接
        /// </summary>
        public event SessionChangedEventHandler ManagerConnected
        {
            add { managerConnected += value; }
            remove { managerConnected -= value; }
        }
        /// <summary>
        /// 通知上层Ws掉线
        /// </summary>
        private WsConnStatusChangedEventHandler wsLost = null;
        /// <summary>
        /// 通知上层Ws掉线
        /// </summary>
        public event WsConnStatusChangedEventHandler WsLost
        {
            add { wsLost += value; }
            remove { wsLost -= value; }
        }
        /// <summary>
        /// 通知上层Ws接入网络
        /// </summary>
        private WsConnStatusChangedEventHandler wsConnected = null;
        /// <summary>
        /// 通知上层Ws接入网络
        /// </summary>
        public event WsConnStatusChangedEventHandler WsConnected
        {
            add { wsConnected += value; }
            remove { wsConnected -= value; }
        }
        /// <summary>
        /// 通知上层Manager重启
        /// </summary>
        private ManagerResetEventHandler managerReset = null;
        /// <summary>
        /// 通知上层Manager重启
        /// </summary>
        public event ManagerResetEventHandler ManagerReset
        {
            add { managerReset += value; }
            remove { managerReset -= value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MeshAdapter()
        {
            CommStackLog.Run();

            m_Serial = new Serial();
            m_Mesh = new MESH(m_Serial);
            m_Session = new Session(m_Serial);
            m_Manager = new ManagerOperator(m_Serial);

            m_Manager.StabilizeManager();

            m_Session.SessionLost = sessnLost;
            m_Session.SessionBuilt = sessnConnected;

            // 向mesh层注册通知到达处理方法
            m_Mesh.NotifyArrived += notifyHandler;
            m_Mesh.ReplyArrived = normalApiResponse;

            // 将用户请求处理线程的优先级设置为最高
            // 在Windows系统中：
            // 线程的基本优先级 = [进程的基本优先级 - 2，进程的基本优先级 + 2]，由应用程序控制,
            // Highest，AboveNormal，Normal，BelowNormal，Lowest
            // 线程的动态优先级 = [ 进程的基本优先级 - 2, 31]，由NT核心控制
            // 时间配额，是一个线程从进入运行状态到系统检查是否有其他优先级相同的线程需要开始运行之间的时间总和

            // 系统线程userRequestQueueHandler
            m_threadUserRequestWorker = new Thread(userRequestQueueHandler);
            m_threadUserRequestWorker.Name = "userRequestQueueHandler";
            m_threadUserRequestWorker.Priority = ThreadPriority.AboveNormal;
            m_threadUserRequestWorker.Start();
            // 系统线程asynCmdResponseHandler
            m_threadMshResponseWork = new Thread(asynCmdResponseHandler);
            m_threadMshResponseWork.Name = "asynCmdResponseHandler";
            m_threadMshResponseWork.Priority = ThreadPriority.AboveNormal;
            m_threadMshResponseWork.Start();
            // 系统线程appCmdResponseHandler
            m_threadWaitWsRspWorker = new Thread(appCmdResponseHandler);
            m_threadWaitWsRspWorker.Name = "appCmdResponseHandler";
            m_threadWaitWsRspWorker.Priority = ThreadPriority.Normal;
            m_threadWaitWsRspWorker.Start();
            // 系统线程appCmdResponseHandler
            m_threadHeartbeatWorker = new Thread(heartbeatHandler);
            m_threadHeartbeatWorker.Name = "heartbeatHandler";
            m_threadHeartbeatWorker.Priority = ThreadPriority.BelowNormal;
            m_threadHeartbeatWorker.Start();
        }

        /// <summary>
        /// 会话失联后的动作
        /// </summary>
        private void sessnLost()
        {
            // 如果正在处理会话断开事件，则新到达的会话断开事件不予理会
            if (!m_bProcessingNewSessin)
            {
                // 如果系统运行至此，而且Session层状态为非失连状态(!SER_ST_DISCONNECTED)，则来到此处是由于系统心跳线程检测到Manager失连
                // 此处应该主动将Session状态置为失连状态(SER_ST_DISCONNECTED)
                if (m_Session.Status != enSerStatus.SER_ST_DISCONNECTED)
                    m_Session.Status = enSerStatus.SER_ST_DISCONNECTED;

                m_bProcessingNewSessin = true;
                m_msigMainThreadRun.Reset();
                m_bPassiveNewSession = true;
                m_Mesh.ReleaseWaitResponse();

                // 重新建立会话时，清空请求列表
                ReqBuffer.Reset();
                // 重新建立会话时，清空等待应用层响应列表
                lock (m_dicWaitAppRespCmds)
                {
                    m_dicWaitAppRespCmds.Clear();
                }
                // 重新建立会话时，清空异步命令等待响应列表
                lock (m_Mesh.m_dicAsyncCmdRespRecords)
                {
                    m_Mesh.m_dicAsyncCmdRespRecords.Clear();
                }

                // 准备新的会话建立
                Thread CreateNewSessionThread = new Thread(createNewSessionWorker);
                CreateNewSessionThread.Priority = ThreadPriority.Highest;
                CreateNewSessionThread.Start();
            }
        }

        Thread m_ThreadMoniterNewSessin = null;
        /// <summary>
        /// 建立新会话的线程处理主函数
        /// </summary>
        /// <param name="threadParam"></param>
        private void createNewSessionWorker()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "Creating new session...");
            m_asigAutoNwResetEvt.Reset();
            // 重新构建系统及会话
            if (preludeSystem(false))
            {
                //m_u8ResetManagerTime = 0;
                // 当重启是因为升级
                if (m_bUpdateReset)
                {
                    // Manager已经重启，清除标志位
                    m_bUpdateReset = false;
                    // 等待Manager自身重启事件失败，准备手动触发Manager重启事件
                    if (!m_asigAutoNwResetEvt.WaitOne(WAIT_MGR_RSTEVT_TIMEOUT))
                        GetNetworkInfo(true);
                }
            }
            // 构建失败则重启manager
            else
            {
                //// 如果重启Manager多次仍不能建立新的会话，则通知上层与Manager会话断开
                //if (++m_u8ResetManagerTime > RESET_MANAGER_MAX_TIME)
                //{
                //    m_u8ResetManagerTime = 0;
                //    managerLost();
                //    CommStackLog.RecordErr(enLogLayer.eAdapter, "Manager Lost!");
                //    return;
                //}
                //// 否则，需要硬件重启
                //else
                if (m_ThreadMoniterNewSessin == null)
                {
                    m_Manager.ResetManager();
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetManager HW!");
                    // 准备新的会话建立
                    m_ThreadMoniterNewSessin = new Thread(moniterNewSessinWorker);
                    m_ThreadMoniterNewSessin.Start();
                }
            }

            m_bProcessingNewSessin = false;
        }

        private void moniterNewSessinWorker()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "Wait 1 munites for new session");
            Thread.Sleep(60000);
            if (m_Session.Status != enSerStatus.SER_ST_CONNECTED)
            {
                managerLost();
                CommStackLog.RecordInf(enLogLayer.eAdapter, "managerLost");
            }

            m_ThreadMoniterNewSessin = null;
        }

        /// <summary>
        /// 停止心跳
        /// </summary>
        private bool stopHeartbeat()
        {
            m_bHaultHeartbeat = true;
            if (m_asigHaultHeartbeat.WaitOne(SYC_WAIT_MAX_TIME))
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "Stop heartbeatHandler ok");
                return true;
            }
            else
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "Stop heartbeatHandler failed");
                return false;
            }
        }

        /// <summary>
        /// 开启心跳
        /// </summary>
        private void startHeartbeat()
        {
            m_bHaultHeartbeat = false;
        }

        /// <summary>
        /// 系统脉搏定时器超时处理函数
        /// </summary>
        private void heartbeatHandler()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "heartbeatHandler running");

            while (m_bKeepRunning)
            {
                m_msigMainThreadRun.WaitOne();
				m_asigHaultHeartbeat.Reset();
                if (m_bHaultHeartbeat)
                {
                    m_asigHaultHeartbeat.Set();
                    Thread.Sleep(200);
                    continue;
                }

                try
                {
                    if (m_Mesh.IsQueryingAllWs)
                    {
                        tMAC queryIter = new tMAC("0000000000000000");
                    ITERATOR:
                        m_asigGetMtCfg.Reset();
                        m_bGetMtCfgTimeout = false;
                        if (GetNextMoteConfig(queryIter) != enURErrCode.ERR_NONE)
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "QueryAllWs unadmissible");
                            m_Mesh.IsQueryingAllWs = false;
                            if (queryAllWsFailed != null)
                                queryAllWsFailed();
                            continue;
                        }
                        else if (queryIter.isEqual(new tMAC("0000000000000000")))
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "QueryAllWs start");
                            lock(m_dicOnLineWs) { m_dicOnLineWs.Clear(); }
                            m_i32MoniterWsIdx = 0;
                        }

                        if (!m_asigGetMtCfg.WaitOne(m_Mesh.AgtReq2MgrRepTimeout))
                        {
                            m_bGetMtCfgTimeout = true;
                        }
                        if (!m_bGetMtCfgTimeout)
                        {
                            if (m_cacheGetMtCfgResult.RC == eRC.RC_OK)
                            {
                                if (!m_cacheGetMtCfgResult.isAP)
                                {
                                    // Kous: 此处决定只要Mote不为Lost状态，则认为为在线
                                    if (m_cacheGetMtCfgResult.u8State != (byte)enMoteState.Lost)
                                        lock (m_dicOnLineWs) { m_dicOnLineWs.Add(m_cacheGetMtCfgResult.mac.ToHexString(), true); }
                                    else
                                        lock (m_dicOnLineWs) { m_dicOnLineWs.Add(m_cacheGetMtCfgResult.mac.ToHexString(), false); }
                                }
                                else
                                    m_macManager = (string)(m_cacheGetMtCfgResult.mac.ToHexString()).Clone();

                                queryIter.Assign(m_cacheGetMtCfgResult.mac);
                                goto ITERATOR;
                            }
                            else if (m_cacheGetMtCfgResult.RC == eRC.RC_END_OF_LIST)
                            {
                                queryIter.Assign(new tMAC("0000000000000000"));
                                m_Mesh.IsQueryingAllWs = false;
                                lock (m_dicOnLineWs)
                                {
                                    if (QueryAllWsReaultNotify != null)
                                        QueryAllWsReaultNotify(m_dicOnLineWs);
                                }
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "QueryAllWs end");
                            }
                            else
                            {
                                m_Mesh.IsQueryingAllWs = false;
                                if (queryAllWsFailed != null)
                                    queryAllWsFailed();
                            }
                        }
                        else
                        {
                            m_Mesh.IsQueryingAllWs = false;
                            if (queryAllWsFailed != null)
                                queryAllWsFailed();
                        }

                        continue;
                    }

                    int iWsCnt1 = 0;
                    lock (m_dicOnLineWs) { iWsCnt1 = m_dicOnLineWs.Count; }
                    if (iWsCnt1 == 0)    // 1 网络中无在线的WS
                    {
                        // 1.1 本次心跳周期内无接收到的报文
                        if (m_Serial.RxFrameCnt <= 0)
                        {
                            if (m_macManager == string.Empty)
                            {
                                Thread.Sleep(200);
                                continue;
                            }
                            // 1.1.0 通过GetMoteConfig(Manager)感知系统脉搏
                            m_asigGetMtCfg.Reset();
                            m_bGetMtCfgTimeout = false;
                            GetMoteConfig(new tMAC(m_macManager));
                            m_asigGetMtCfg.WaitOne();
                            // 1.1.1 GetMoteConfig(Manager)超时
                            if (m_bGetMtCfgTimeout)
                            {
                                if (m_u8HeartbeatRetryCnt++ > HEARTBEET_RETRY_TIMES)
                                {
                                    m_u8HeartbeatRetryCnt = 0;
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making failed!");
                                    sessnLost();
                                }
                                else
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making timeout!");
                            }
                            // 1.1.2 GetMoteConfig(Manager)正常
                            else
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making");
                        }
                        // 1.2 本次心跳周期内有接收到的报文
                        else
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making(Rx " + m_Serial.RxFrameCnt + ")");
                            // Added in 2016.10.21 by Kous，添加下一条语句
                            m_Serial.RxFrameCnt = 0;
                        }
                    }
                    else   // 2 网络中有在线的WS
                    {
                        // 2.0 通过GetMoteConfig感知系统脉搏
                        m_asigGetMtCfg.Reset();
                        m_bGetMtCfgTimeout = false;
                        lock (m_dicOnLineWs) { GetMoteConfig(new tMAC(m_dicOnLineWs.Keys.ElementAt(m_i32MoniterWsIdx))); }
                        m_asigGetMtCfg.WaitOne();
                        // 2.1 GetMoteConfig超时
                        if (m_bGetMtCfgTimeout)
                        {
                            if (m_u8HeartbeatRetryCnt++ > HEARTBEET_RETRY_TIMES)
                            {
                                m_u8HeartbeatRetryCnt = 0;
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making failed!");
                                sessnLost();
                            }
                            else
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making timeout!");
                        }
                        // 2.2 GetMoteConfig正常
                        else
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Pace-making");
                            m_u8HeartbeatRetryCnt = 0;
                            int iWsCnt2 = 0;
                            lock (m_dicOnLineWs) { iWsCnt2 = m_dicOnLineWs.Count; }
                            // 轮询完毕，从头开始
                            if (++m_i32MoniterWsIdx >= iWsCnt2)
                                m_i32MoniterWsIdx = 0;
                        }
                    }

                    int residueSleepTime = cfgHeartbeatInterval;
                    while (residueSleepTime > 200)
                    {
                        Thread.Sleep(200);
                        if (m_Mesh.IsQueryingAllWs)
                            break;
                        residueSleepTime = residueSleepTime - 200;
                    }

                    if (m_Mesh.IsQueryingAllWs)
                        continue;

                    if (residueSleepTime > 0)
                        Thread.Sleep(residueSleepTime);

                    continue;
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// 会话建立后的动作
        /// </summary>
        private void sessnConnected()
        {
            if (managerConnected != null)
                managerConnected();
        }

        /// <summary>
        /// 将用户请求转换成有意义的字符串
        /// </summary>
        /// <param name="element">用户请求元素</param>
        /// <returns>用户请求字符串描述</returns>
        private string describeCmd(UserRequestElement element)
        {
            if (element == null)
                return "";
            if (element.cmd != enCmd.CMDID_SENDDATA)
                return element.cmd.ToString();
            else
            {
                tSENDDATA sendData = (tSENDDATA)element.param;
                enAppMainCMD cmd = (enAppMainCMD)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA]>>5);
                byte subCmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                switch (cmd)
                {
                    case enAppMainCMD.eNotify:
                    {
                        if (subCmd == (byte)enAppNotifySubCMD.eSelfReport) return "ReplySelfReport(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppNotifySubCMD.eHealthReport) return "ReplyHealthReport(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppNotifySubCMD.eWaveDesc) return "ReplyWaveDesc(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppNotifySubCMD.eWaveData) return "ReplyWaveData(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppNotifySubCMD.eEigenVal) return "ReplyEigenVal(" + sendData.mac.ToHexString() + ")";                        
                        else return null;
                    }
                    case enAppMainCMD.eSet:
                    {
                        if (subCmd == (byte)enAppSetSubCMD.eTimeCali) return "SetTimeCali(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eNetworkID) return "SetNetworkID(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eMeasDef) return "SetMeasDef(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eSn) return "SetSn(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eCaliCoeff) return "SetSensorCali(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eADCloseVolt) return "SetADCloseVolt(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eRevStop) return "SetRevStop(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eTrigParam) return "SetTrigParam(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eWsRouteMode) return "SetWsRouteMode(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppSetSubCMD.eWsDebugMode) return "SetWsDebugMode(" + sendData.mac.ToHexString() + ")";
                        else return null;
                    }
                    case enAppMainCMD.eGet:
                    {
                        if (subCmd == (byte)enAppGetSubCMD.eSn) return "GetSn(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppGetSubCMD.eCaliCoeff) return "GetCaliCoeff(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppGetSubCMD.eADCloseVolt) return "GetADCloseVolt(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppGetSubCMD.eRevStop) return "GetRevStop(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppGetSubCMD.eTrigParam) return "GetTrigParam(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppGetSubCMD.eWsRouteMode) return "GetWsRouteMode(" + sendData.mac.ToHexString() + ")";
                        else return null;
                    }
                    case enAppMainCMD.eRestore:
                    {
                        if (subCmd == (byte)enAppRestoreSubCMD.eWS) return "RestoreWS(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppRestoreSubCMD.eWG) return "RestoreWG" + sendData.mac.ToHexString() + ")";
                        else return null;
                    }
                    case enAppMainCMD.eReset:
                    {
                        if (subCmd == (byte)enAppResetSubCMD.eWS) return "ResetWS(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppResetSubCMD.eWG) return "ResetWG" + sendData.mac.ToHexString() + ")";
                        else return null;
                    }
                    case enAppMainCMD.eUpdate:
                    {
                        if (subCmd == (byte)enAppUpdateSubCMD.eFwDesc) return "UpdateFwDesc(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppUpdateSubCMD.eFwData) return "UpdateFwData(" + sendData.mac.ToHexString() + ")";
                        else if (subCmd == (byte)enAppUpdateSubCMD.eControl) return "UpdateControl(" + sendData.mac.ToHexString() + ")";
                        else return null;
                    }
                    default: return "";
                }
            }
        }

        /// <summary>
        /// iMesh组件使用前的前奏工作
        /// 建立新的会话->注册系统关注事件->获取网络当前配置->获取网络时间
        /// </summary>
        /// <param name="bFirstTime">表示是否第一次启动调用此函数</param>
        /// <returns>系统前奏工作是否成功</returns>
        private bool preludeSystem(bool bFirstTime = false)
        {
            m_asigSubs.Reset();
            m_asigGetNwCfg.Reset();
            m_asigGetTime.Reset();

            m_bSubsTimeout = false;
            m_bGetNwCfgTimeout = false;
            m_bGetTimeTimeout = false;

            // 打开新的会话
            byte u8CreateSsnTimes = 0;
            while (u8CreateSsnTimes++ < cfgCreateSsnRetryCnt)
            {
                if (!m_Session.Open())
                    continue;
                else
                    break;
            }
            // 尝试多次建立新的会话失败，返回
            if (u8CreateSsnTimes >= cfgCreateSsnRetryCnt)
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "Retry " + u8CreateSsnTimes + " times for new-session fialed");
                u8CreateSsnTimes = 0;
                return false;
            }
            u8CreateSsnTimes = 0;

            // 此时，可以启动iMesh组件的主要处理线程
            m_msigMainThreadRun.Set();
            // 是否第一次启动iMesh组件
            if (bFirstTime)
            {
                // 重新订阅网络事件
                Subscribe(enSubFilters.ICmsFocus, true);
                if (!m_asigSubs.WaitOne(SYC_WAIT_MAX_TIME))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Wait Subscribe ack too long");
                    return false;
                }
                else if (m_bSubsTimeout)
                {
                    m_bSubsTimeout = false;
                    return false;
                }

                // 获取当前网络配置
                GetNetworkConfig(true);
                if (!m_asigGetNwCfg.WaitOne(SYC_WAIT_MAX_TIME))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Wait GetNetworkConfig ack too long");
                    return false;
                }
                else if (m_bGetNwCfgTimeout)
                {
                    m_bGetNwCfgTimeout = false;
                    return false;
                }

                // 获取当前网络时间
                GetTime(true);
                if (!m_asigGetTime.WaitOne(SYC_WAIT_MAX_TIME))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Wait GetTime ack too long");
                    return false;
                }
                else if (m_bGetTimeTimeout)
                {
                    m_bGetTimeTimeout = false;
                    return false;
                }
                
                return true;
            }
            else
            {
                // 重新订阅网络事件
                Subscribe(enSubFilters.ICmsFocus, true);
                //m_asigSubs.WaitOne();
                if (!m_asigSubs.WaitOne(SYC_WAIT_MAX_TIME))
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Wait Subscribe ack too long");
                    return false;
                }
                else if (m_bSubsTimeout)
                {
                    m_bSubsTimeout = false;
                    return false;
                }
                // 升级状态下，来到此处是因为软件重启Manager，需要重新建立新的会话，并需订阅系统关注事件
                // 但，软件重启Manager后，伴随NetworkReset事件会调用GetTime，故此处不用主动调用
                if (!m_bUpdating)
                {
                    GetTime(true);
                    if (!m_asigGetTime.WaitOne(SYC_WAIT_MAX_TIME))
                    {
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Wait GetTime ack too long");
                        return false;
                    }
                    else if (m_bGetTimeTimeout)
                    {
                        m_bGetTimeTimeout = false;
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (m_Mesh != null)
                m_Mesh.Dispose();

            m_bKeepRunning = false;
            CommStackLog.Exit();
        }

        #region IStackLayer 实现
        /// <summary>
        /// MeshAdapter层初始化函数
        /// </summary>
        public void Initialize()
        {
            m_Mesh.Initialize();
            m_Session.Initialize();
            m_dicOnLineWs.Clear();
            ReqBuffer.Reset();
            if (!preludeSystem(true))
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "PreludeSystem failed in MeshAdapter.Initialize()!");
                throw new Exception("PreludeSystem failed in MeshAdapter.Initialize()!");
            }
        }

        public void Reset()
        {
            ;
        }
        #endregion IStackLayer 实现
    }
}
