using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 用户请求原子
    /// </summary>
    internal class UserRequestElement
    {
        /// <summary>
        /// 用户请求命令
        /// </summary>
        public volatile enCmd cmd = enCmd.CMDID_END;
        /// <summary>
        /// 用户请求命令参数
        /// </summary>
        public volatile object param = null;
        /// <summary>
        /// 表示异步命令的重试次数，包括尝试成功发送请求的重试次数，
        /// 也包括发送成功请求后，等待WS响应超时的重试次数
        /// </summary>
        public volatile byte retryTime = 0;
        /// <summary>
        /// 请求成功发送的时刻值
        /// </summary>
        public DateTime SuccessReqTime = DateTime.Now;
    }
    

    /// <summary>
    /// 网络响应元素
    /// </summary>
    internal class AsyncCmdRespEntry
    {
        /// <summary>
        /// 表示已经从ACK中接收到异步命令的CbId
        /// </summary>
        public volatile bool ACK1ST = false;
        /// <summary>
        /// 表示已经从packetSent通知中接收到异步命令的CbId
        /// </summary>
        public volatile bool NOT1ST = false;
        /// <summary>
        /// 表示Manager响应异步命令的时间
        /// </summary>
        public DateTime ACKTime = DateTime.Now;
        /// <summary>
        /// 伴生的异步命令记录元素
        /// </summary>
        public volatile UserRequestElement SiblingRequest = null;
		/// <summary>
        /// 2016年11月7日添加，缓存某命令异步通知的返回码
        /// </summary>
        public volatile byte AsyncRC = 0;
    }

    /// <summary>
    /// 定义用户请求的缓存队列
    /// </summary>
    internal class UserRequestQueue
    {
        /// <summary>
        /// 普通请求缓存队列
        /// </summary>
        private volatile Queue<UserRequestElement> normalReqQ = null;
        /// <summary>
        /// 紧急请求缓存队列
        /// </summary>
        private volatile Queue<UserRequestElement> urgentReqQ = null;
        /// <summary>
        /// 最大能够容纳的请求数量
        /// </summary>
        private int capacity = 0;
        /// <summary>
        /// 多线程同步锁
        /// </summary>
        private object lockObj = new object();
        /// <summary>
        /// 表示队列中有新的用户请求加入
        /// </summary>
        private AutoResetEvent sigNewReqArrived = new AutoResetEvent(false);
        /// <summary>
        /// 当前正在处理的用户请求
        /// </summary>
        private UserRequestElement current = null;
        /// <summary>
        /// 当前正在处理的用户请求
        /// </summary>
        public UserRequestElement Current
        {
            get { return current; }
            set {}
        }

        /// <summary>
        /// 判定请求队列中是否有新的未处理请求，如无调用线程阻塞到此处
        /// </summary>
        public bool HaveRequest
        {
            get
            {
                // 如有，立即返回
                if (urgentReqQ.Count > 0 || normalReqQ.Count > 0)
                    return true;
                // 如无，等待新的请求
                else
                {
                    sigNewReqArrived.WaitOne();
                    return true;
                }
            }
            set {}
        }

        /// <summary>
        /// UserRequestQueue构造函数
        /// </summary>
        public UserRequestQueue()
        {
            capacity = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UserRequestPoolSize"].ToString());
            if (capacity < 1)
            {
                throw new Exception("parameter UserRequestPoolSize is invalid in iMesh.config file");
            }
            // 三个不同优先级的实现队列作为整体使用
            normalReqQ = new Queue<UserRequestElement>(capacity);
            urgentReqQ = new Queue<UserRequestElement>(capacity);
        }

        /// <summary>
        /// 将新的用户请求入紧急或者普通队列
        /// </summary>
        /// <param name="element">新请求元素</param>
        /// <returns>true表示请求入队列成功，false表示请求入队列失败，意味请求队列已满</returns>
        public bool EnQ(UserRequestElement element, bool urgent = false)
        {
            lock (lockObj)
            {
                // 检查请求队列是否已满
                if ((normalReqQ.Count + urgentReqQ.Count) >= capacity)
                    return false;

                // 请求队列未满
                // 根据请求紧迫程度入队列
                if (urgent)
                    urgentReqQ.Enqueue(element);
                else
                    normalReqQ.Enqueue(element);

                sigNewReqArrived.Set();
                return true;
            }
        }

        /// <summary>
        /// 获取等待时间最长的用户请求，请求出队列
        /// </summary>
        /// <returns>等待时间最长的用户请求</returns>
        public UserRequestElement DeQ()
        {
            lock (lockObj)
            {
                if (urgentReqQ.Count > 0)
                    current = urgentReqQ.Dequeue();
                else if (normalReqQ.Count > 0)
                    current = normalReqQ.Dequeue();
                // 队列中无请求
                else
                    current = null;

                // 当用户请求队列中无请求原子时，重置心情求入队列信号
                if ((urgentReqQ.Count + normalReqQ.Count) <= 0)
                    sigNewReqArrived.Reset();

                return current;
            }
        }

        /// <summary>
        /// 检测用户请求队列是否为空
        /// </summary>
        /// <returns>请求队列是否为空</returns>
        public bool IsEmpty()
        {
            lock (lockObj)
            {
                if ((urgentReqQ.Count + normalReqQ.Count) <= 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 检查用户请求队列是否已满
        /// </summary>
        /// <returns>是否已满</returns>
        public bool IsFull()
        {
            lock (lockObj)
            {
                if ((urgentReqQ.Count + normalReqQ.Count) >= capacity)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 重置用户请求队列，清空请求队列
        /// </summary>
        public void Reset()
        {
            lock (lockObj)
            {
                normalReqQ.Clear();
                urgentReqQ.Clear();
                sigNewReqArrived.Reset();
            }
        }

        /// <summary>
        /// 报告当前请求队列的繁忙程度
        /// </summary>
        /// <returns></returns>
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            lock (lockObj)
            {
                sb.Append("NQ(" + normalReqQ.Count + ")|UQ(" + urgentReqQ.Count + ")");
            }

            return sb.ToString();
        }
    }
}
