using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh.IO.ZLAN
{
    class ControlZlanGPIO
    {
        private bool bZlanIoctl = false;

        /// <summary>
        /// 卓岚IO控制Endpoint
        /// </summary>
        private IPEndPoint ioctlEndpoint = null;
        /// <summary>
        /// 卓岚IO控制Socket
        /// </summary>
        private Socket ioctlSocket = null;
        /// <summary>
        /// 本地UDP通信Endpoint
        /// </summary>
        private IPEndPoint localEndpoint = null;
        /// <summary>
        /// 本地UDP通信Socket
        /// </summary>
        private Socket localSocket = null;
        
        public ControlZlanGPIO()
        {
            bZlanIoctl = (int.Parse(System.Configuration.ConfigurationManager.AppSettings["IoCntl"].ToString()) == 0) ? false : true;

            string ipZlIO = System.Configuration.ConfigurationManager.AppSettings["ZLIOIp"].ToString();
            string portZlIO = System.Configuration.ConfigurationManager.AppSettings["ZLIOPort"].ToString();
            ioctlEndpoint = new IPEndPoint(getValidIP(ipZlIO), getValidPort(portZlIO));
            ioctlSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //ioctlSocket.Bind(ioctlEndpoint);

            string ipLocal = GetLocalIP();
            //string ipLocal = System.Configuration.ConfigurationManager.AppSettings["LocalIp"].ToString();
            localEndpoint = new IPEndPoint(getValidIP(ipLocal), getValidPort(portZlIO));
            localSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            localSocket.Bind(localEndpoint);
        }

        /// <summary>  
        /// 获取本地IP  
        /// </summary>  
        /// <returns></returns> 
        public static string GetLocalIP()
        {
            string result = RunApp("route", "print");
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
                return m.Groups[2].Value;
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>  
        /// 运行一个控制台程序并返回其输出参数。  
        /// </summary>  
        /// <param name="filename">程序名</param>  
        /// <param name="arguments">输入参数</param>  
        /// <returns></returns>  
        public static string RunApp(string filename, string arguments)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    //string txt = sr.ReadToEnd();  
                    //sr.Close();  
                    //if (recordLog)  
                    //{  
                    //    Trace.WriteLine(txt);  
                    //}  
                    //if (!proc.HasExited)  
                    //{  
                    //    proc.Kill();  
                    //}  
                    //上面标记的是原文，下面是我自己调试错误后自行修改的  
                    Thread.Sleep(100);           // 貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行  
                                                 // txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应  
                    if (!proc.HasExited)         // 在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行  
                    {                            // txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行  
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    return txt;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>  
        /// 获取本机主DNS  
        /// </summary>  
        /// <returns></returns>  
        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "");
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
                return m.Value;
            else
                return null;
        }

        /// <summary>
        /// 获取并检验有效的端口号
        /// </summary>
        /// <param name="port">端口号字符串</param>
        /// <returns>端口号</returns>
        private int getValidPort(string port)
        {
            int lport;
            // 测试端口号是否有效  
            try
            {
                // 是否为空
                if (port == "")
                    throw new ArgumentException("ZLIOPort is null");

                lport = System.Convert.ToInt32(port);
            }
            catch (Exception)
            {
                throw new ArgumentException("ZLIOPort is invalid");
            }

            return lport;
        }

        /// <summary>
        /// 获取并检验有效的IP
        /// </summary>
        /// <param name="ip">IP字符串</param>
        /// <returns>IP</returns>
        private IPAddress getValidIP(string ip)
        {
            IPAddress lip = null;
            // 测试IP是否有效  
            try
            {
                // 是否为空
                if (ip == "")
                    throw new ArgumentException("ZLIOIp is null");

                // 是否为空  
                if (!IPAddress.TryParse(ip, out lip))
                    throw new ArgumentException("ZLIOIp is invalid");
            }
            catch (Exception)
            {
                throw new ArgumentException("ZLIOIp is invalid");
            }

            return lip;
        }

        byte[] cmdSetIoState = new byte[113]
        {
            0x5a, 0x4c, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00
        };

        byte[] cmdGetIoState = new byte[113]
        {
            0x5a, 0x4c, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00
        };

        /// <summary>
        /// 设置引脚电平低位
        /// </summary>
        /// <param name="ID">引脚ID</param>
        /// <returns>设置成功与否</returns>
        public bool SetLowLevel(UInt32 ID = 0)
        {
            if (bZlanIoctl)
            {
                if (ID == 3 || ID > 7)
                    return false;

                if (ID == 0)
                    cmdSetIoState[112] = 0x01;
                else if (ID == 1)
                    cmdSetIoState[112] = 0x02;
                else if (ID == 2)
                    cmdSetIoState[112] = 0x04;
                else if (ID == 4)
                    cmdSetIoState[112] = 0x10;
                else if (ID == 5)
                    cmdSetIoState[112] = 0x20;
                else if (ID == 6)
                    cmdSetIoState[112] = 0x40;
                else
                    cmdSetIoState[112] = 0x80;

                // 首先读取当前的IO配置


                cmdSetIoState[112] |= 0x08;
                localSocket.SendTo(cmdSetIoState, 0, cmdSetIoState.Length, SocketFlags.None, ioctlEndpoint);
                EndPoint Remote = (EndPoint)(ioctlEndpoint);
                byte[] responseBytes = new byte[1024];
                localSocket.ReceiveFrom(responseBytes, ref Remote);
                if (responseBytes[112] != cmdSetIoState[112])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 设置引脚电平高位
        /// </summary>
        /// <param name="ID">引脚ID</param>
        /// <returns>设置成功与否</returns>
        public bool SetHighLevel(UInt32 ID = 0)
        {
            if (bZlanIoctl)
            {
                if (ID == 3 || ID > 7)
                    return false;

                if (ID == 0)
                    cmdSetIoState[112] = 0x01;
                else if (ID == 1)
                    cmdSetIoState[112] = 0x02;
                else if (ID == 2)
                    cmdSetIoState[112] = 0x04;
                else if (ID == 4)
                    cmdSetIoState[112] = 0x10;
                else if (ID == 5)
                    cmdSetIoState[112] = 0x20;
                else if (ID == 6)
                    cmdSetIoState[112] = 0x40;
                else
                    cmdSetIoState[112] = 0x80;

                localSocket.SendTo(cmdSetIoState, 0, cmdSetIoState.Length, SocketFlags.None, ioctlEndpoint);
                EndPoint Remote = (EndPoint)(ioctlEndpoint);
                byte[] responseBytes = new byte[1024];
                localSocket.ReceiveFrom(responseBytes, ref Remote);
                if (responseBytes[112] != cmdSetIoState[112])
                    return false;
            }

            return true;
        }
    }
}
