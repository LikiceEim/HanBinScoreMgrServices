/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Agent.Common
 *文件名：  LogHelper
 *创建人：  LF
 *创建时间：2016/2/16 14:58:21
 *描述：记录日志工具类
 *
/************************************************************************************/


using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ICSharpCode.SharpZipLib.Zip;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 记录日志类
    /// 日志存放路径：.\CurrentDirectroy\UserCreaterDirectory\yyyyMM\yyyyMMdd_no.log
    /// CurrentDirectroy：程序存储路径；
    /// UserCreaterDirectory：用户自定义文件夹名，用户可以直接存入通用日志和异常日志，通用文件夹为log，异常文件夹为Error
    /// yyyyMM：年月文件夹，如201601，指2016年1月；
    /// yyyyMMdd_no.log：如20160101_1.log，指2016年1月1日第一个日志文件，当文件大小超过2M后，创建20160101_2.log，依次类推。
    /// </summary>
    public class LogHelper
    {
        #region 变量
        private static Object thisLock = new Object();

        //上次压缩时间
        static DateTime LastCompressDate = DateTime.Now;
        #endregion

        #region 写日志，日志内容为字符串
        /// <summary>
        /// 写日志，需要指定存储路径，日志内容为字符串
        /// 日志内容不用添加时间
        /// </summary>
        /// <param name="path">日志文件夹</param>
        /// <param name="context">日志内容</param>
        public static void WriteLog(string path, string context)
        {
            try
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\" + path;
                //写日志
                Write(path, context);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }
        #endregion

        #region 写日志，日志内容为byte数组
        /// <summary>
        /// 写日志，需要指定存储路径，日志内容为byte数组
        /// </summary>
        /// <param name="path">日志文件夹</param>
        /// <param name="context">日志内容, byte数组</param>
        public static void WriteLog(string path, byte[] content)
        {
            try
            {

                path = AppDomain.CurrentDomain.BaseDirectory + @"\" + path;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < content.Length; i++)
                {
                    sb.Append(content[i].ToString("X2") + " ");
                }
                //写日志
                Write(path, sb.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }
        #endregion

        #region 写日志，日志内容为byte数组,并说明日志类型
        /// <summary>
        /// 写日志，需要指定存储路径，
        /// 日志内容为byte数组,并说明日志类型
        /// </summary>
        /// <param name="path">存储路径</param>
        /// <param name="message">日志类型</param>
        /// <param name="content">日志内容，byte数组</param>
        public static void WriteLog(string path, string message, byte[] content)
        {
            try
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\" + path;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < content.Length; i++)
                {
                    sb.Append(content[i].ToString("X2") + " ");
                }
                //写日志
                Write(path, message + "\n" + sb.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }
        #endregion

        #region 写通用日志
        /// <summary>
        /// 写通用日志, 保存路径为：.\程序运行路径\Log\
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(string content)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\Log";
                //写日志
                Write(path, content);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }
        #endregion

        #region 写异常日志
        /// <summary>
        /// 写异常日志, 保存路径为：.\程序运行路径\Error\
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\Error";
                //写日志
                Write(path, new ExceptionMessage(ex).ErrorDetails);
            }
            catch (Exception exc)
            {
                WriteLog(exc);
            }
        }
        #endregion

        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="path">文件夹</param>
        /// <param name="context">内容</param>
        private static void Write(string path, string context)
        {
            //取得日志文件名称
            string fileName = CreateLogFile(path);
            lock (thisLock)
            {
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString("000"));
                    sw.WriteLine(context);
                    sw.WriteLine();
                }
            }

            //压缩处理
            CompressAndDeleteLog(path);
        }
        #endregion

        #region 创建日志文件
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        private static string CreateLogFile(string path)
        {
            string dir = path;
            if (Directory.Exists(dir) == false)
            {
                //文件夹不存在创建
                Directory.CreateDirectory(dir);
            }

            string dirMonth = dir + @"\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00");
            if (Directory.Exists(dirMonth) == false)
            {
                //月份文件夹不存在创建
                Directory.CreateDirectory(dirMonth);
            }

            string filePre = dirMonth + @"\" + DateTime.Now.Year
                                + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
            string fileDay = "";
            int index = 1;
            while (true)
            {
                FileInfo fi;
                string fullname = filePre + "_" + index.ToString() + ".log";
                if (File.Exists(fullname) == true)
                {
                    fi = new FileInfo(fullname);
                    //判断文件大小是否超过2M
                    if (fi.Length / 1024 / 1024 >= 2)
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
        #endregion

        #region 判断是否进行压缩
        static void CompressAndDeleteLog(string path)
        {
            int days = GetCompressLogDays();
            if (days > 0)
            {
                //if (DateTime.Now.Date != LastCompressDate.Date)
                //{
                if ((DateTime.Now.Date - LastCompressDate.Date).Days >= days)
                {
                    Thread threadCompress = new Thread(CompresseAndDeleteLog_Thread);
                    threadCompress.Start(path);
                    //}
                }
            }
        }
        #endregion

        #region 压缩并删除文件
        private static void CompresseAndDeleteLog_Thread(object obj)
        {
            try
            {
                string path = (string)obj;
                if (Directory.Exists(path) == false)
                {
                    return;
                }

                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] arrDir = di.GetDirectories();
                if (arrDir.Length > 0)
                {
                    foreach (DirectoryInfo dir in arrDir)
                    {
                        CompresseAndDeleteLog_Thread(dir.FullName);
                    }
                }

                int deleteDays = GetDeleteLogDays();
                FileInfo[] arrFile = di.GetFiles();
                foreach (FileInfo info in arrFile)
                {
                    if (deleteDays > 0)
                    {
                        if ((DateTime.Now.Date - info.CreationTime.Date).Days >= deleteDays)
                        {
                            //删除文件
                            File.Delete(info.FullName);
                        }
                    }
                }

                arrFile = di.GetFiles();
                List<FileInfo> fiList = new List<FileInfo>();
                List<string> fiListDate = new List<string>();
                if (arrFile.Length > 0)
                {
                    int days = GetCompressLogDays();
                    foreach (FileInfo info in arrFile)
                    {
                        if (days > 0)
                        {
                            if (info.Name.Substring(info.Name.LastIndexOf(".") + 1).ToLower() != "zip")
                            {
                                if ((DateTime.Now.Date - info.CreationTime.Date).Days >= days)
                                {
                                    //逐个文件压缩
                                    //Compress(info);
                                    fiList.Add(info);
                                    //删除已压缩文件
                                    //File.Delete(info.FullName);
                                }
                                if (!fiListDate.Contains(info.Name.Substring(0, info.Name.IndexOf("_"))))
                                {
                                    fiListDate.Add(info.Name.Substring(0, info.Name.IndexOf("_")));
                                }
                            }
                        }
                    }
                    foreach (string fiDate in fiListDate)
                    {
                        List<FileInfo> tempList = new List<FileInfo>();
                        foreach (FileInfo fi in fiList)
                        {
                            if (fiDate == fi.Name.Substring(0, fi.Name.IndexOf("_")))
                            {
                                tempList.Add(fi);
                            }
                        }
                        if (tempList.Count > 0)
                        {
                            Compress(tempList, tempList[0].DirectoryName + "\\" + fiDate + ".zip", 5, 5);
                        }
                    }
                    foreach (FileInfo delInfo in fiList)
                    {
                        File.Delete(delInfo.FullName);
                    }
                }
                else
                {
                    Directory.Delete(di.FullName);
                }

                //更新压缩时间
                LastCompressDate = DateTime.Now;
            }
            catch { }
        }
        #endregion

        #region 压缩文件
        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".zip")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create(fi.FullName + ".zip"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);
                        }
                    }
                }
            }
        }
        #endregion

        #region 取得压缩时间间隔
        static int GetCompressLogDays()
        {
            int days = 0;
            try
            {
                string CompressLogDays = ConfigurationManager.AppSettings["CompressLogDays"];
                if (CompressLogDays != null)
                {
                    days = Convert.ToInt32(CompressLogDays);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return days;
        }
        #endregion

        #region 取得删除时间间隔
        /// <summary>
        /// 取得删除时间间隔 - 月
        /// </summary>
        /// <returns></returns>
        private static int GetDeleteLogDays()
        {
            int deleteDays = 0;
            try
            {
                string deleteLogDays = ConfigurationManager.AppSettings["DeleteLogDays"];
                if (deleteLogDays != null)
                {
                    deleteDays = Convert.ToInt32(deleteLogDays);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return deleteDays;
        }
        #endregion

        #region 压缩文件
        /// <summary>
        /// 压缩文件 add by masu 2016年3月30日10:58:42
        /// </summary>
        /// <param name="fileNames">要打包的文件列表</param>
        /// <param name="GzipFileName">目标文件名</param>
        /// <param name="CompressionLevel">压缩品质级别（0~9）</param>
        /// <param name="SleepTimer">休眠时间（单位毫秒）</param>     
        public static void Compress(List<FileInfo> fileNames, string GzipFileName, int CompressionLevel, int SleepTimer)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(GzipFileName));
            try
            {
                s.SetLevel(CompressionLevel);   //0 - store only to 9 - means best compression
                foreach (FileInfo file in fileNames)
                {
                    FileStream fs = null;
                    try
                    {
                        fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
                    }
                    catch
                    { continue; }

                    byte[] data = new byte[4096];
                    int size = 4096;
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file.Name));
                    entry.DateTime = (file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime);
                    s.PutNextEntry(entry);
                    while (true)
                    {
                        size = fs.Read(data, 0, size);
                        if (size <= 0) break;
                        s.Write(data, 0, size);
                    }
                    fs.Close();
                    Thread.Sleep(SleepTimer);

                }
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }
        #endregion
    }
}
