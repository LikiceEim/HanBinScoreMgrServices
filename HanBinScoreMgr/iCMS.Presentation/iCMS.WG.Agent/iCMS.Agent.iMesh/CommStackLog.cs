using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum enLogType
    {
        eNull = 0,
        // 错误日志
        eErr = 1,
        // 信息日志
        eInfo = 2,
        // CLI日志
        eCli = 3,
    }

    /// <summary>
    /// 协议栈层
    /// </summary>
    public enum enLogLayer
    {
        eNull    = 0,
        eDevice  = 1,
        eHdlc    = 2,
        eSerial  = 3,
        eMesh    = 4,
        eSession = 5,
        eAdapter = 6,
    }

    /// <summary>
    /// 日志信息记录元素
    /// </summary>
    public class LogElement
    {
        public enLogLayer LogLayer = enLogLayer.eNull;      // 来源
        public enLogType LogType = enLogType.eNull;         // 类型
        private string LogTime = null;                      // 时间
        private string LogContent = null;                   // 内容
        public bool NewLine = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="layer">协议栈分层</param>
        /// <param name="type">日志类型</param>
        /// <param name="content">日志内容</param>
        /// <param name="newline">是否在新的一行中记录日志</param>
        public LogElement(enLogLayer layer, enLogType type, string content, bool newline = true)
        {
            LogTime = DateTime.Now.ToString("MM/dd HH:mm:ss:fff");
            
            LogLayer = layer;
            LogType = type;
            if (content == null)
                LogContent = string.Empty;
            else
                LogContent = content;

            NewLine = newline;
        }

        /// <summary>
        /// 日志元素转字符串操作
        /// </summary>
        /// <returns>日志字符串</returns>
        public override string ToString()
        {
            StringBuilder strB = new StringBuilder();
            if (NewLine)
            {
                // 写时间
                if (LogTime != null)
                    strB.Append(LogTime);
                if (LogLayer == enLogLayer.eDevice) strB.Append("[HW]");
                else if (LogLayer == enLogLayer.eHdlc) strB.Append("[HL]");
                else if (LogLayer == enLogLayer.eSerial) strB.Append("[SR]");
                else if (LogLayer == enLogLayer.eMesh) strB.Append("[MS]");
                else if (LogLayer == enLogLayer.eSession) strB.Append("[SN]");
                else if (LogLayer == enLogLayer.eAdapter) strB.Append("[AD]");
                else strB.Append(" ");
            }

            if (LogContent != null)
                strB.Append(LogContent);

            return strB.ToString();
        }
    }

    /// <summary>
    /// 日志元素队列
    /// </summary>
    public class LogQueue
    {
        // 错误日志元素队列
        private volatile Queue<LogElement> errLogQ = null;
        // 信息日志元素队列
        private volatile Queue<LogElement> infLogQ = null;
        // CLI日志元素队列
        private volatile Queue<LogElement> CLILogQ = null;
        // 最大能够容纳的数量
        private int capacity = 0;
        // 多线程同步锁
        private object lockQ = new object();
        // 表示队列中有新的日志元素加入
        private AutoResetEvent sigNewLogElement = new AutoResetEvent(false);

        /// <summary>
        /// 查询队列中是否有日志元素
        /// </summary>
        public bool HaveLogElement
        {
            get
            {
                // 如有，立即返回
                if (errLogQ.Count > 0 || infLogQ.Count > 0 || CLILogQ.Count > 0)
                    return true;
                // 如无，等待新的请求
                else
                {
                    sigNewLogElement.WaitOne();
                    return true;
                }
            }
            set { }
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        public LogQueue()
        {
            capacity = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogPoolSize"].ToString());
            if (capacity < 1)
                throw new Exception("parameter LogPoolSize is invalid in iMesh.config file");

            errLogQ = new Queue<LogElement>(capacity);
            infLogQ = new Queue<LogElement>(capacity);
            CLILogQ = new Queue<LogElement>(capacity);
        }

        /// <summary>
        /// 日志元素入队列
        /// </summary>
        /// <param name="element">日志元素</param>
        /// <returns>入队列是否成功</returns>
        public bool EnQ(LogElement element)
        {
            lock (lockQ)
            {
                if (element.LogType == enLogType.eErr)
                {
                    if (errLogQ.Count >= capacity)
                        return false;
                    else
                    {
                        errLogQ.Enqueue(element);
                        sigNewLogElement.Set();
                        return true;
                    }
                }
                else if (element.LogType == enLogType.eInfo)
                {
                    if (infLogQ.Count >= capacity)
                        return false;
                    else
                    {
                        infLogQ.Enqueue(element);
                        sigNewLogElement.Set();
                        return true;
                    }
                }
                else if (element.LogType == enLogType.eCli)
                {
                    if (CLILogQ.Count >= capacity)
                        return false;
                    else
                    {
                        CLILogQ.Enqueue(element);
                        sigNewLogElement.Set();
                        return true;
                    }
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// 日志元素出队列
        /// </summary>
        /// <returns>出队列是否成功</returns>
        public LogElement DeQ()
        {
            lock (lockQ)
            {
                LogElement retElem = null;

                if (errLogQ.Count > 0)
                    retElem = errLogQ.Dequeue();
                else if (infLogQ.Count > 0)
                    retElem = infLogQ.Dequeue();
                else if (CLILogQ.Count > 0)
                    retElem = CLILogQ.Dequeue();
                else
                    retElem = null;

                return retElem;
            }
        }
    }
    /// <summary>
    /// 日志记录器
    /// </summary>
    public static class CommStackLog
    {
        /// <summary>
        /// 错误日志目录
        /// </summary>
        private static string pathErr = AppDomain.CurrentDomain.BaseDirectory + "iMeshLog\\Error";
        /// <summary>
        /// 信息日志目录
        /// </summary>
        private static string pathInf = AppDomain.CurrentDomain.BaseDirectory + "iMeshLog\\Infor";
        /// <summary>
        /// Manager
        /// </summary>
        private static string pathCLI = AppDomain.CurrentDomain.BaseDirectory + "iMeshLog\\Clinf";
        /// <summary>
        /// 锁
        /// </summary>
        private static Object thisLock = new Object();
        /// <summary>
        /// 锁
        /// </summary>
        private static object lockLog = new object();
        /// <summary>
        /// 锁
        /// </summary>
        public static volatile object LockLog = new object();
        /// <summary>
        /// 串口读取线程终止标志
        /// </summary>
        private static bool m_bCLIFirstWrite = true;
        /// <summary>
        /// [可配置]日志压缩频率
        /// </summary>
        private static int cfgLogCopressFreq = 0;
        /// <summary>
        /// [可配置]日志留盘时间
        /// </summary>
        private static int cfgLogPersistentTime = 0;
        /// <summary>
        /// [可配置]日志压缩时间
        /// </summary>
        private static int cfgLogNeatenTime = 0;
        /// <summary>
        /// 上次日志压缩时间
        /// </summary>
        private static DateTime LastCompressDate = new DateTime(2016, 4, 15);
        /// <summary>
        /// 需要压缩文件的创建时间
        /// </summary>
        private static DateTime CompressCreatDate = new DateTime(2016, 4, 15);
        /// <summary>
        /// 整顿日志标志位
        /// </summary>
        private static bool bNeatening = false;
        /// <summary>
        /// 协议栈层日志开关
        /// </summary>
        private static volatile bool[] layerLogSwitch = new bool[(int)enLogLayer.eAdapter + 1];
        /// <summary>
        /// 日志队列
        /// </summary>
        private static volatile LogQueue logQ = new LogQueue();
        /// <summary>
        /// 日志记录线程
        /// </summary>
        private static Thread logThread = null;
        /// <summary>
        /// 线程退出信号量
        /// </summary>
        private static AutoResetEvent asigThreadExit = new AutoResetEvent(false);
        /// <summary>
        /// 线程退出标志
        /// </summary>
        private static volatile bool bExit = false;

        /// <summary>
        /// 退出日志记录线程
        /// </summary>
        public static void Exit()
        {
            bExit = true;
            // 等待30秒内线程退出
            if (!asigThreadExit.WaitOne(30000))
            {
                try
                {
                    logThread.Abort();
                }
                catch (Exception)
                {

                }
            }

            bExit = false;
            logThread = null;
        }

        /// <summary>
        /// 将字节数组转换成16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="size">数组大小</param>
        /// <returns>16进制字符串</returns>
        public static string ToHexString(byte[] bytes, int size = 0)// 0xAE00CF => "AE 00 CF"
        {
            string hexString = string.Empty;
            if (bytes != null
                && bytes.Length != 0
                && size > 0
                && size <= bytes.Length)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < size; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                    if (i != (bytes.Length - 1))
                        strB.Append(bytes[i].ToString(" "));
                }

                hexString = strB.ToString();
            }

            return hexString;
        }

        /// <summary>
        /// 将字节数组转换成字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToCharString(byte[] bytes, int idx = 0, int size = 0)
        {
            if (bytes == null || bytes.Length == 0 || idx < 0 || size < 0 || (idx + size) > bytes.Length)
                return string.Empty;

            return System.Text.Encoding.Default.GetString(bytes, idx, size);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="layer">协议层</param>
        /// <param name="err">错误信息</param>
        public static void RecordErr(enLogLayer layer, string err)
        {
            lock (lockLog)
            {
                LogElement aLogElement = new LogElement(layer, enLogType.eErr, err);
                logQ.EnQ(aLogElement);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="layer">协议层</param>
        /// <param name="err">错误数据</param>
        /// <param name="len">错误数据量</param>
        public static void RecordErr(enLogLayer layer, byte[] err, int len = 0)
        {
            lock (lockLog)
            {
                LogElement aLogElement = null;
                if (err != null && err.Length != 0)
                {
                    if (len > err.Length)
                        aLogElement = new LogElement(layer, enLogType.eErr, ToHexString(err));
                    else
                    {
                        byte[] arrValid = new byte[len];
                        Array.Copy(err, 0, arrValid, 0, len);
                        aLogElement = new LogElement(layer, enLogType.eErr, ToHexString(arrValid));
                    }
                }

                logQ.EnQ(aLogElement);
            }
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="layer">协议层</param>
        /// <param name="inf">信息</param>
        public static void RecordInf(enLogLayer layer, string inf)
        {
            lock (lockLog)
            {
                if (layerLogSwitch[(int)layer])
                {
                    LogElement aLogElement = new LogElement(layer, enLogType.eInfo, inf);
                    logQ.EnQ(aLogElement);
                }
            }
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="layer">协议层</param>
        /// <param name="inf">数据</param>
        /// <param name="len">数据量</param>
        public static void RecordInf(enLogLayer layer, byte[] inf, int len = 0)
        {
            lock (lockLog)
            {
                if (layerLogSwitch[(int)layer])
                {
                    LogElement aLogElement = null;

                    if (inf != null && inf.Length != 0)
                    {
                        if (len > inf.Length)
                            aLogElement = new LogElement(layer, enLogType.eInfo, ToHexString(inf));
                        else
                        {
                            byte[] arrValid = new byte[len];
                            Array.Copy(inf, 0, arrValid, 0, len);
                            aLogElement = new LogElement(layer, enLogType.eInfo, ToHexString(arrValid));
                        }
                    }

                    logQ.EnQ(aLogElement);
                }
            }
        }
      
        /// <summary>
        /// 写CLI日志
        /// </summary>
        /// <param name="path">文件夹</param>
        /// <param name="Cli">内容</param>
        public static void RecordClinf(byte[] u8Buff, int len)
        {
            string fileName = null;
            
            try
            {
                // 固定每天凌晨1点对之前的日志进行压缩
                if (DateTime.Now.Hour == cfgLogNeatenTime
                    && LastCompressDate.Day != DateTime.Now.Day
                    && !bNeatening)
                {
                    Thread NeatenLogger = new Thread(NeatenLog);
                    NeatenLogger.Priority = ThreadPriority.Normal;
                    bNeatening = true;
                    NeatenLogger.Start();
                }      
                fileName = CreateLogFile(pathCLI, enLogType.eCli);

                lock (thisLock)
                {
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    {
                        if (m_bCLIFirstWrite == true)
                        {
                            m_bCLIFirstWrite = false;
                            sw.WriteLine(DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString("000"));                           
                        }
                        
                        for (int i = 0; i < len; i++)
                        {
                            byte[] u8aTmpBuff = new byte[1];
                            u8aTmpBuff[0] = u8Buff[i];
                            if (u8Buff[i] == 0x0A)                           
                            {
                                if (i != (len - 1))
                                {
                                    sw.Write(DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString("000"));
                                    sw.Write(ToCharString(u8aTmpBuff, 1));
                                }
                                else
                                    m_bCLIFirstWrite = true;
                            }
                            else
                                sw.Write(ToCharString(u8aTmpBuff, 1));
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        static StringBuilder sbCliCache = new StringBuilder();
        static bool b0x0Dend = false;
        static bool b0x0Aend = false;
        public static void RecordClinf(byte[] CliBuff)
        {
            lock (lockLog)
            {
                try
                {
                    int j = 0;
                    // 预判
                    if (CliBuff == null || CliBuff.Length == 0)
                        return;

                    if (b0x0Dend)
                    {
                        if (CliBuff[0] == 0x0A)
                        {
                            b0x0Dend = false;
                            logQ.EnQ(new LogElement(enLogLayer.eNull, enLogType.eCli, sbCliCache.ToString()));
                            sbCliCache.Clear();
                            j = 1;
                        }
                        else
                        {
                            b0x0Dend = false;
                            sbCliCache.Append(Convert.ToChar(0x0D));
                        }
                    }
                    else if (b0x0Aend)
                    {
                        if (CliBuff[0] == 0x0D)
                        {
                            b0x0Aend = false;
                            logQ.EnQ(new LogElement(enLogLayer.eNull, enLogType.eCli, sbCliCache.ToString()));
                            sbCliCache.Clear();
                            j = 1;
                        }
                        else
                        {
                            b0x0Aend = false;
                            sbCliCache.Append(Convert.ToChar(0x0A));
                        }
                    }

                    // 过滤
                    List<byte> listCli = new List<byte>();
                    for (; j < CliBuff.Length; j++)
                    {
                        if (CliBuff[j] == 0x3E) // 过滤'>'
                            continue;
                        else
                            listCli.Add(CliBuff[j]);
                    }

                    // 转义
                    for (int i = 0; i < listCli.Count; i++)
                    {
                        if (i < listCli.Count - 1)
                        {
                            if (listCli[i] == 0x0A)
                            {
                                if (listCli[i + 1] == 0x0D)
                                {
                                    i++;
                                    logQ.EnQ(new LogElement(enLogLayer.eNull, enLogType.eCli, sbCliCache.ToString()));
                                    sbCliCache.Clear();
                                }
                                else
                                    sbCliCache.Append(Convert.ToChar(listCli[i]));
                            }
                            else if (listCli[i] == 0x0D)
                            {
                                if (listCli[i + 1] == 0x0A)
                                {
                                    i++;
                                    logQ.EnQ(new LogElement(enLogLayer.eNull, enLogType.eCli, sbCliCache.ToString()));
                                    sbCliCache.Clear();
                                }
                                else
                                    sbCliCache.Append(Convert.ToChar(listCli[i]));
                            }
                            else
                                sbCliCache.Append(Convert.ToChar(listCli[i]));
                        }
                        else
                        {
                            if (CliBuff[i] == 0x0D)
                                b0x0Dend = true;
                            else if (CliBuff[i] == 0x0A)
                                b0x0Aend = true;
                            else
                                sbCliCache.Append(Convert.ToChar(listCli[i]));
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 运行日志记录线程
        /// </summary>
        public static void Run()
        {
            layerLogSwitch[(int)enLogLayer.eDevice] = true;
            layerLogSwitch[(int)enLogLayer.eHdlc] = true;
            layerLogSwitch[(int)enLogLayer.eSerial] = true;
            layerLogSwitch[(int)enLogLayer.eMesh] = true;
            layerLogSwitch[(int)enLogLayer.eSession] = true;
            layerLogSwitch[(int)enLogLayer.eAdapter] = true;
            cfgLogCopressFreq = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogCopressFreq"].ToString());
            cfgLogPersistentTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogPersistentTime"].ToString());
            cfgLogNeatenTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogNeatenTime"].ToString());
            
            if (logThread == null)
                logThread = new Thread(CommStackLogger);
            // 日志记录线程具有组件最低线程优先级
            logThread.Priority = ThreadPriority.BelowNormal;
            bExit = false;
            // 启动日志记录线程
            logThread.Start();
        }

        /// <summary>
        /// 创建日志文件名称
        /// </summary>
        /// <param name="path">日志路径</param>
        /// <param name="logType">日志类型</param>
        /// <returns></returns>
        private static string CreateLogFile(string path, enLogType logType)
        {
            string dir = path;
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            //string dirMonth = dir + @"\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00");
            //if (Directory.Exists(dirMonth) == false)
            //    Directory.CreateDirectory(dirMonth);

            string filePre = dir + @"\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
            string fileDay = "";
            int index = 1;
            while (true)
            {
                FileInfo fi;
                string fullname = null;
                if (logType == enLogType.eErr)
                    fullname = filePre + "_" + index.ToString() + ".err";
                else if (logType == enLogType.eInfo)
                    fullname = filePre + "_" + index.ToString() + ".ifr";
                else if(logType == enLogType.eCli)
                    fullname = filePre + "_" + index.ToString() + ".cli";

                if (File.Exists(fullname) == true)
                {
                    fi = new FileInfo(fullname);
                    //判断文件大小是否超过10M
                    if (fi.Length / 1024 / 1024 >= 10)
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        fileDay = fullname;
                        break;
                    }
                }
                else
                {
                    fileDay = fullname;
                    break;
                }
            }

            return fileDay;
        }

        /// <summary>
        /// 压缩信息日志
        /// </summary>
        private static void CompressInfoLog()
        {
            if (Directory.Exists(pathInf) == false)
                return;

            DirectoryInfo diInf = new DirectoryInfo(pathInf);
            // 搜索路径中所有文件
            FileInfo[] arrInfFile = diInf.GetFiles();
            List<string> compressFileList = new List<string>();
            // 寻找需压缩文件
            foreach (FileInfo info in arrInfFile)
            {
                // 压缩文件选择：
                // ① 非压缩文件
                // ② 文件创建时间为今天之前
                if (info.Name.Substring(info.Name.LastIndexOf(".") + 1).ToLower() != "zip"
                    && info.CreationTime.Date < DateTime.Now.Date)
                {
                    compressFileList.Add(pathInf + @"\" + info.Name);
                    CompressCreatDate = info.CreationTime;
                }
            }
           
            if (compressFileList.Count > 0)
            {
                // Kous: 压缩文件名称计算不对，可直接用LastCompressDate
                string compressFileName = pathInf + @"\" + CompressCreatDate.Year + CompressCreatDate.Month.ToString("00") + CompressCreatDate.Day.ToString("00") + ".zip";
                ZipOutputStream zos = new ZipOutputStream(File.Create(compressFileName));
                zos.SetLevel(9); // 0 - store only to 9 - means best compression

                Crc32 crc = new Crc32();
                foreach (string comFile in compressFileList)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(comFile);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = comFile.Substring(comFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zos.PutNextEntry(entry);

                    zos.Write(buffer, 0, buffer.Length);

                    File.Delete(comFile);
                }
                zos.Finish();
                zos.Close();
            }
        }
        /// <summary>
        /// 压缩错误日志
        /// </summary>
        private static void CompressErrLog()
        {
            if (Directory.Exists(pathErr) == false)
                return;

            DirectoryInfo diErr = new DirectoryInfo(pathErr);
            FileInfo[] arrErrFile = diErr.GetFiles();
            List<string> compressFileList = new List<string>();
            foreach (FileInfo info in arrErrFile)
            {
                // 压缩文件选择：
                // ① 非压缩文件
                // ② 文件创建时间为今天之前
                if (info.Name.Substring(info.Name.LastIndexOf(".") + 1).ToLower() != "zip"
                    && info.CreationTime.Date < DateTime.Now.Date)
                {
                    compressFileList.Add(pathErr + @"\" + info.Name);
                    CompressCreatDate = info.CreationTime;
                }
            }
           

            if (compressFileList.Count > 0)
            {
                string compressFileName = pathErr + @"\" + CompressCreatDate.Year + CompressCreatDate.Month.ToString("00") + CompressCreatDate.Day.ToString("00") + ".zip";
                ZipOutputStream zos = new ZipOutputStream(File.Create(compressFileName));
                zos.SetLevel(9); // 0 - store only to 9 - means best compression
                Crc32 crc = new Crc32();
                foreach (string comFile in compressFileList)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(comFile);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = comFile.Substring(comFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zos.PutNextEntry(entry);

                    zos.Write(buffer, 0, buffer.Length);

                    File.Delete(comFile);
                }
                zos.Finish();
                zos.Close();
            }
        }

        /// <summary>
        /// 压缩CLI日志
        /// </summary>
        private static void CompressCLILog()
        {
            if (Directory.Exists(pathCLI) == false)
                return;

            DirectoryInfo diCLI = new DirectoryInfo(pathCLI);
            FileInfo[] arrCLIFile = diCLI.GetFiles();
            List<string> compressFileList = new List<string>();
            foreach (FileInfo info in arrCLIFile)
            {
                // 压缩文件选择：
                // ① 非压缩文件
                // ② 文件创建时间为今天之前
                if (info.Name.Substring(info.Name.LastIndexOf(".") + 1).ToLower() != "zip"
                    && info.CreationTime.Date < DateTime.Now.Date)
                {
                    compressFileList.Add(pathCLI + @"\" + info.Name);
                    CompressCreatDate = info.CreationTime;
                }
            }


            if (compressFileList.Count > 0)
            {
                string compressFileName = pathCLI + @"\" + CompressCreatDate.Year + CompressCreatDate.Month.ToString("00") + CompressCreatDate.Day.ToString("00") + ".zip";
                ZipOutputStream zos = new ZipOutputStream(File.Create(compressFileName));
                zos.SetLevel(9); // 0 - store only to 9 - means best compression
                Crc32 crc = new Crc32();
                foreach (string comFile in compressFileList)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(comFile);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = comFile.Substring(comFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zos.PutNextEntry(entry);

                    zos.Write(buffer, 0, buffer.Length);

                    File.Delete(comFile);
                }
                zos.Finish();
                zos.Close();
            }
        }

        /// <summary>
        /// 删除过期的日志
        /// </summary>
        private static void DeleteExpiredLog()
        {
            List<string> deleteFileList = new List<string>();
            // 删除文件选择：
            // 今天后推30天的日志信息全部删除
            DateTime expiredDate = DateTime.Now.AddDays(0 - cfgLogPersistentTime);

            if (Directory.Exists(pathErr) == false)
                return;

            DirectoryInfo diErr = new DirectoryInfo(pathErr);
            FileInfo[] arrErrFile = diErr.GetFiles();
            foreach (FileInfo info in arrErrFile)
            {
                if (DateTime.Compare(info.CreationTime, expiredDate) < 0)
                    deleteFileList.Add(info.Name);
            }

            if (deleteFileList.Count > 0)
            {
                foreach (string delFile in deleteFileList)
                    File.Delete(delFile);
            }

            deleteFileList.Clear();

            if (Directory.Exists(pathInf) == false)
                return;

            DirectoryInfo diInf = new DirectoryInfo(pathInf);
            FileInfo[] arrInfFile = diInf.GetFiles();
            foreach (FileInfo info in arrInfFile)
            {
                if (DateTime.Compare(info.CreationTime, expiredDate) < 0)
                    deleteFileList.Add(info.Name);
            }

            if (deleteFileList.Count > 0)
            {
                foreach (string delFile in deleteFileList)
                    File.Delete(delFile);
            }

            if (Directory.Exists(pathCLI) == false)
                return;

            DirectoryInfo diCli = new DirectoryInfo(pathCLI);
            FileInfo[] arrCliFile = diCli.GetFiles();
            foreach (FileInfo info in arrCliFile)
            {
                if (DateTime.Compare(info.CreationTime, expiredDate) < 0)
                    deleteFileList.Add(info.Name);
            }

            if (deleteFileList.Count > 0)
            {
                foreach (string delFile in deleteFileList)
                    File.Delete(delFile);
            }
        }

        /// <summary>
        /// 整理日志线程主函数
        /// </summary>
        private static void NeatenLog()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "NeatenLog running");
            DeleteExpiredLog();
            // 如果当前时间以上次压缩间隔超过配置的压缩频率，则进行日志压缩
            if ((DateTime.Now.Date - LastCompressDate.Date).Days >= cfgLogCopressFreq)
            {
                CompressInfoLog();
                CompressErrLog();
                CompressCLILog();
                LastCompressDate = DateTime.Now;
            }

            bNeatening = false;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="log">日志信息</param>
        /// <param name="newline">是否在新的一行中记录</param>
        private static void WriteLog(enLogType logType, string log, bool newline = true)
        {
            string fileName = null;
            try
            {
                // 固定每天凌晨1点对之前的日志进行压缩
                if (DateTime.Now.Hour == cfgLogNeatenTime
                    && LastCompressDate.Day != DateTime.Now.Day
                    && !bNeatening)
                {
                    Thread NeatenLogger = new Thread(NeatenLog);
                    NeatenLogger.Priority = ThreadPriority.Normal;
                    bNeatening = true;
                    NeatenLogger.Start();
                }

                if (logType == enLogType.eErr)
                    fileName = CreateLogFile(pathErr, logType);
                else if (logType == enLogType.eInfo)
                    fileName = CreateLogFile(pathInf, logType);
                else if (logType == enLogType.eCli)
                    fileName = CreateLogFile(pathCLI, logType);
                lock (thisLock)
                {
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    { 
                        if (newline)
                            sw.WriteLine(log);
                        else
                            sw.Write(" " + log);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 日志处理主线程函数
        /// </summary>
        private static void CommStackLogger()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "CommStackLogger running");
            while (!bExit && logQ.HaveLogElement)
            {
                LogElement logElem = logQ.DeQ();
                if (logElem != null)
                    WriteLog(logElem.LogType, logElem.ToString(), logElem.NewLine);
            }

            asigThreadExit.Set();
        }
    }
}
