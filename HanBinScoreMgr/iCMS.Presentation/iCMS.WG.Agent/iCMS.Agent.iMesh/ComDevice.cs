using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 流控方式定义
    /// </summary>
    internal enum enFCType
    {
        None = 0,           // 无流控
        Software = 1,       // 软件流控
        Hardware = 2,       // 硬件流控
        HardSoftware = 3,   // 硬件软件结合流控
    }
    /// <summary>
    /// api串口模式
    /// </summary>
    internal enum enAPIMode
    {
        Mod2 = 0,
        Mod4 = 1,
    }

    internal delegate void DlgtRx1Byte(byte b);

    /// <summary>
    /// 串口设备
    /// </summary>
    internal class ComDevice : IStackLayer, IDisposable
    {
        /// <summary>
        /// 此处定义256保证串行线路上最大报文接受长度
        /// </summary>
        private const ushort HW_RX_DATA_MAX_SIZE = 256;
        /// <summary>
        /// 硬件层接受通信码流缓存
        /// </summary>
        private volatile byte[] m_u8aHwRxData = new byte[HW_RX_DATA_MAX_SIZE];
        /// <summary>
        /// 接收缓冲区读取线程
        /// </summary>
        private Thread m_threadReadReceiveBuffer = null;
        /// <summary>
        /// 串口读取线程终止标志
        /// </summary>
        private volatile bool m_bKeepReading = true;
        /// <summary>
        //RxCts状态是否正常
        /// </summary>
        public volatile bool m_bManageRxCtsState = true;
        /// <summary>
        /// API串口
        /// </summary>
        private SerialPort m_ApiDevice = new SerialPort();
        /// <summary>
        /// CLI串口
        /// </summary>
        private SerialPort m_CliDevice = new SerialPort();
        /// <summary>
        /// [可配置]主动读串口间隔时间
        /// </summary>
        private int cfgReadApiInterval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ApiReadInterval"].ToString());
        /// <summary>
        /// [可配置]API串口模式
        /// </summary>
        private enAPIMode cfgAPIMode = (enAPIMode)int.Parse(System.Configuration.ConfigurationManager.AppSettings["ApiMode"].ToString());
        /// <summary>
        /// [可配置]CLI串口是否存在
        /// </summary>
        private bool cfgbCliExist = (int.Parse(System.Configuration.ConfigurationManager.AppSettings["CliExist"].ToString()) == 0) ? false : true;
        /// <summary>
        /// 表示字节数据接收事件的方法。
        /// </summary>
        private DlgtRx1Byte m_evByteArrived = null;
        /// <summary>
        /// 表示字节数据接收事件的方法。
        /// </summary>
        public event DlgtRx1Byte ByteArrived
        {
            add { m_evByteArrived += value; }
            remove { m_evByteArrived -= value; }
        }

        /// <summary>
        /// ComDevice的构造方法
        /// </summary>
        public ComDevice()
        {
            OpenApi();
            OpenCli();
            m_threadReadReceiveBuffer = new Thread(readApiBufferWorker);
            m_threadReadReceiveBuffer.Priority = ThreadPriority.Highest;
            m_threadReadReceiveBuffer.Start();
        }

        /// <summary>
        /// 获取接收缓存区的数据
        /// </summary>       
        private void readApiBufferWorker()
        {
            CommStackLog.RecordInf(enLogLayer.eDevice, "readApiBufferWorker running");
            m_ApiDevice.ReadTimeout = 4000;
            while (m_bKeepReading)
            {
                if (m_ApiDevice.IsOpen)
                {
                    try
                    {
                        if (cfgAPIMode == enAPIMode.Mod2)
                        {
                            if (!Dsr())//Dsr=false代表低电平
                            {
                                Rts(false);// Rts(false)代表此时低电平
                                int buffSize = m_ApiDevice.BytesToRead;
                                if (buffSize == 0)
                                {
                                    Thread.Sleep(2);
                                    continue;
                                }

                                byte[] readBuffer = new byte[buffSize];
                                int count = m_ApiDevice.Read(readBuffer, 0, buffSize);
                                if (count != buffSize)
                                    CommStackLog.RecordErr(enLogLayer.eDevice, "readApiBufferWorker disappointed");

                                for (int loop = 0; loop < count; loop++)
                                {
                                    if (m_evByteArrived != null)
                                        m_evByteArrived(readBuffer[loop]);
                                    else
                                        CommStackLog.RecordErr(enLogLayer.eDevice, "Event ByteArrived is null");
                                }
                            }
                            else
                                Rts(true);
                        }
                        else
                        {
                            int buffSize = m_ApiDevice.BytesToRead;
                            if (buffSize == 0)
                            {
                                if (cfgReadApiInterval > 0)
                                    Thread.Sleep(cfgReadApiInterval);
                                continue;
                            }

                            byte[] readBuffer = new byte[buffSize];
                            int count = m_ApiDevice.Read(readBuffer, 0, buffSize);
                            if (count != buffSize)
                                CommStackLog.RecordErr(enLogLayer.eDevice, "readApiBufferWorker disappointed");

                            for (int loop = 0; loop < count; loop++)
                            {
                                if (m_evByteArrived != null)
                                    m_evByteArrived(readBuffer[loop]);
                                else
                                    CommStackLog.RecordErr(enLogLayer.eDevice, "Event ByteArrived is null");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                    }
                }
                else
                {
                    CommStackLog.RecordErr(enLogLayer.eDevice, "API Port not open");
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 1, 0);
                    Thread.Sleep(waitTime);
                }
            }
        }

        /// <summary>
        /// ComDevice的析构方法，提供com资源的有效释放方法
        /// </summary>
        ~ComDevice()
        {
            if (m_ApiDevice != null && m_ApiDevice.IsOpen)
            {
                try
                {
                    m_ApiDevice.Close();
                    m_ApiDevice.Dispose();
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
                finally
                {
                    m_ApiDevice.Close();
                }
            }

            if (cfgbCliExist)
            {
                if (m_CliDevice != null && m_CliDevice.IsOpen)
                {
                    try
                    {
                        m_CliDevice.Close();
                        m_CliDevice.Dispose();
                    }
                    catch (Exception ex)
                    {
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                    }
                    finally
                    {
                        m_CliDevice.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 打开API通信设备
        /// </summary>
        public void OpenApi()
        {
            try
            {
                m_ApiDevice.PortName = System.Configuration.ConfigurationManager.AppSettings["ApiPortName"].ToString();
                m_ApiDevice.BaudRate = 115200;
                m_ApiDevice.Parity = Parity.None;
                m_ApiDevice.StopBits = StopBits.One;
                m_ApiDevice.DataBits = 8;
                m_ApiDevice.Handshake = Handshake.None;

                m_ApiDevice.Open();

                Dtr(false);
                Rts(true);
                Dtr(true);

            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
            }
        }

        /// <summary>
        /// 注册给底层串口的数据接收回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cli_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (m_CliDevice.IsOpen)
            {
                try
                {
                    int len = m_CliDevice.BytesToRead;
                    byte[] u8aTmpBuff = new byte[len];
                    m_CliDevice.Read(u8aTmpBuff, 0, len);
                    CommStackLog.RecordClinf(u8aTmpBuff/*, len*/);
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
            }
            else
                CommStackLog.RecordErr(enLogLayer.eDevice, "CLI_SerialPort not open");
        }

        /// <summary>
        /// 打开CLI通信设备
        /// </summary>
        public void OpenCli()
        {
            if (cfgbCliExist)
            {
                try
                {
                    m_CliDevice.PortName = System.Configuration.ConfigurationManager.AppSettings["CliPortName"].ToString();
                    m_CliDevice.BaudRate = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CliBaudRate"].ToString());
                    m_CliDevice.Parity = Parity.None;
                    m_CliDevice.StopBits = StopBits.One;
                    m_CliDevice.DataBits = 8;
                    m_CliDevice.Handshake = Handshake.None;

                    // 数据接收方法注册
                    m_CliDevice.DataReceived += new SerialDataReceivedEventHandler(cli_DataReceived);
                    m_CliDevice.Open();
                    Thread.Sleep(10);
                    MoniterCli();
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
        /// 监控CLI
        /// </summary>
        public void MoniterCli()
        {
            if (cfgbCliExist)
            {
                if (m_CliDevice.IsOpen)
                {
                    string strCmd = System.Configuration.ConfigurationManager.AppSettings["CliCommand"].ToString();
                    strCmd = strCmd.Trim();
                    char[] delimiterChars = { '|' };
                    string[] cmds = strCmd.Split(delimiterChars);
                    foreach (string cmd in cmds)
                        m_CliDevice.WriteLine(cmd.Trim());
                }
                else
                    CommStackLog.RecordErr(enLogLayer.eDevice, "WriteCliCom not open");
            }
        }

        /// <summary>
        /// 软件重启Manager
        /// </summary>
        public void ResetManager()
        {
            if (cfgbCliExist)
            {
                if (m_CliDevice.IsOpen)
                    m_CliDevice.WriteLine("reset system");
                else
                    CommStackLog.RecordErr(enLogLayer.eDevice, "WriteCliCom not open");
            }
        }

        /// <summary>
        /// 发送字节数组
        /// </summary>
        /// <param name="arr">发送数据</param>
        /// <param name="len">发送数据长度</param>
        public void SendArr(byte[] arr, int len)
        {
            if (m_ApiDevice.IsOpen)
            {
                try
                {
                    if (cfgAPIMode == enAPIMode.Mod2)
                    {
                        Dtr(true);//Dtr(true) = 低电平
                        while (!Cts())
                        {
                            CommStackLog.RecordInf(enLogLayer.eDevice, "Cts is invalid!");
                            Thread.Sleep(2);
                            continue;
                        }
                        m_ApiDevice.Write(arr, 0, len);

                        while (m_ApiDevice.BytesToWrite != 0)
                        {
                            CommStackLog.RecordInf(enLogLayer.eDevice, "BytesToWrite = " + m_ApiDevice.BytesToWrite.ToString());
                            Thread.Sleep(1);
                            continue;
                        }

                        Thread.Sleep(5);
                        Dtr(false);
                    }
                    else
                        m_ApiDevice.Write(arr, 0, len);
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
            }
            else
                CommStackLog.RecordErr(enLogLayer.eDevice, "SendArr not open");
        }

        /// <summary>
        /// 刷新输出缓冲区
        /// </summary>
        public void FlushTx()
        {
            try
            {
                m_ApiDevice.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
            }
        }

        /// <summary>
        /// 流控之Dtr控制，目前只支持硬件流控
        /// </summary>
        /// <param name="enable">设置Dtr状态</param>
        public void Dtr(bool enable)
        {
            try
            {
                m_ApiDevice.DtrEnable = enable;
                //CommStackLog.RecordInf(enLogLayer.eDevice, "Dtr " + ((enable) ? "true" : "false"));
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
            }
        }

        /// <summary>
        /// 流控之Dsr控制，目前只支持硬件流控
        /// </summary>
        /// <param name="enable">获取Dsr状态</param>
        public bool Dsr()
        {
            bool DsrState = true;
            try
            {
                DsrState = m_ApiDevice.DsrHolding;
                //CommStackLog.RecordInf(enLogLayer.eDevice, "Dsr = " + DsrState.ToString());
                return DsrState;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                return true;
            }
        }

        /// 流控之Rts，目前只支持硬件流控
        /// </summary>
        /// <param name="enable">设置Rts状态</param>
        public void Rts(bool enable)
        {
            try
            {
                m_ApiDevice.RtsEnable = enable;
                //CommStackLog.RecordInf(enLogLayer.eDevice, "Rts " + ((enable) ? "true" : "false"));
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
            }
        }

        /// <summary>
        /// 流控之Cts，目前只支持硬件流控
        /// </summary>
        /// <param name="enable">获取Cts状态</param>
        public bool Cts()
        {
            bool CtsState = false;
            try
            {
                CtsState = m_ApiDevice.CtsHolding;
                // CommStackLog.RecordInf(enLogLayer.eDevice, "Cts = " + CtsState.ToString());
                return CtsState;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                return false;
            }
        }

        #region IDisposable 实现
        public void Dispose()
        {
            if (m_ApiDevice != null && m_ApiDevice.IsOpen)
            {
                try
                {
                    m_bKeepReading = false;
                    m_ApiDevice.Close();
                    m_ApiDevice.Dispose();
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }
                finally
                {
                    // 通过调用GC.SuppressFinalize(this)来告诉GC，让它不用再调用对象的析构函数中
                    //GC.SuppressFinalize(this);
                }
            }

            if (cfgbCliExist)
            {
                if (m_CliDevice != null && m_CliDevice.IsOpen)
                {
                    try
                    {
                        m_CliDevice.Close();
                        m_CliDevice.Dispose();
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
        }
        #endregion

        #region IStackLayer 实现
        public void Initialize()
        {
            try
            {
                m_ApiDevice.DiscardInBuffer();
                m_ApiDevice.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eDevice, ex.ToString());
            }
        }

        public void Reset()
        {
            try
            {
                m_ApiDevice.DiscardInBuffer();
                m_ApiDevice.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eDevice, ex.ToString());
            }
        }
        #endregion
    }
}
