/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  WaveDataHelper
 *创建人：  iLine
 *创建时间：2016/2/16 15:00:44
 *描述：波形数据工具类
 *
 *=====================================================================
 *修改标记
 *修改时间：2016/2/16 15:00:44
 *修改人： iLine
 *描述：
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

using iCMS.Common.Component.Tool;

namespace iCMS.WG.Agent.Common
{
    public class WaveDataHelper
    {
        #region 变量
        private static Object thisLock = new Object();

        //上次压缩时间
        static DateTime LastCompressDate = DateTime.Now;

        static char SeparatorChar = ',';
        static string SeparatorString = ",";
        #endregion

        #region 保存波形
        /// <summary>
        /// 保存波形，波形内容以字符串形式传递，
        /// 字符串内容为：float,float,float
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="context">波形内容</param>
        /// <param name="filename">波形文件名称</param>
        /// <param name="samplingDate">波形采集时间</param>
        public static void SaveWave(string path, string context, string filename, DateTime samplingDate)
        {
            try
            {
                if (context == null || context.Length == 0)
                {
                    //波形内容为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形内容为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                if (filename.Length == 0)
                {
                    //波形文件名称为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形文件名称为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                //保存
                Save(path, context, filename, samplingDate);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 保存波形，波形内容以float列表传递
        /// <summary>
        /// 保存波形，波形内容以float列表传递
        /// 保存时以,分隔
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="context">波形内容</param>
        /// <param name="filename">波形文件名称</param>
        /// <param name="samplingDate">波形采集时间</param>
        public static void SaveWave(string path, List<float> waveData, string filename, DateTime samplingDate)
        {
            try
            {
                if (waveData == null || waveData.Count == 0)
                {
                    //波形内容为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形内容为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                if (filename.Length == 0)
                {
                    //波形文件名称为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形文件名称为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                foreach (float value in waveData)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(value.ToString("0.0000"));
                    }
                    else
                    {
                        sb.Append(SeparatorString + value.ToString("0.0000"));
                    }
                }

                //保存
                Save(path, sb.ToString(), filename, samplingDate);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 保存波形，波形内容以byte数组传递，即2byte为一位16进制
        /// <summary>
        /// 保存波形，波形内容以byte数组传递，即2byte为一位16进制
        /// 除以转换系数，转换为float型，保存时以,分隔
        /// </summary>
        /// <param name="path"></param>
        /// <param name="waveData"></param>
        /// <param name="filename"></param>
        /// <param name="samplingDate"></param>
        /// <param name="transformCofe"></param>
        public static void SaveWave(string path, byte[] waveData, string filename, DateTime samplingDate, float transformCofe)
        {
            try
            {
                if (waveData == null || waveData.Length == 0)
                {
                    //波形内容为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形内容为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                if (filename.Length == 0)
                {
                    //波形文件名称为空时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的波形文件名称为空，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                if (transformCofe == 0)
                {
                    //转换系数为0时，不保存
                    string message = string.Format("波形文件名称为:{0},采集时间为:{1}的波形数据的转换系数为0，无法保存", filename, samplingDate.ToString());
                    LogHelper.WriteLog(new Exception(message));
                    return;
                }

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < waveData.Length; i += 2)
                {
                    if (i + 1 == waveData.Length)
                    {
                        break;
                    }
                    float value = (float)(ConvertUtility.ConvertToInt16(waveData, i)) / transformCofe;
                    if (i == waveData.Length - 2)
                    {
                        sb.Append(value.ToString("0.0000"));
                    }
                    else
                    {
                        sb.Append(value.ToString("0.0000") + SeparatorString);
                    }
                }
                //保存
                Save(path, sb.ToString(), filename, samplingDate);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 保存波形
        /// <summary>
        /// 保存波形
        /// </summary>
        /// <param name="path">保存波形文件夹</param>
        /// <param name="context">波形内容</param>
        /// <param name="filename">波形文件名</param>
        /// <param name="samplingDate">波形采集时间</param>
        private static void Save(string path, string context, string filename, DateTime samplingDate)
        {
            //取得波形文件全路径
            string fileFullName = GetWaveDataFullName(filename, samplingDate);

            using (StreamWriter sw = new StreamWriter(fileFullName, true))
            {
                sw.Write(context);
            }

            //压缩、删除处理
            CompressAndDeleteLog(path);
        }
        #endregion

        #region 取得波形文件的全路径
        /// <summary>
        /// 取得波形文件的全路径
        /// </summary>
        /// <param name="filename">波形文件名称</param>
        /// <param name="samplingDate">波形采集时间</param>
        /// <returns></returns>
        private static string GetWaveDataFullName(string filename, DateTime samplingDate)
        {
            string dirWave = ConfigurationManager.AppSettings["WaveDirectory"];
            if (Directory.Exists(dirWave) == false)
            {
                //文件夹不存在创建
                Directory.CreateDirectory(dirWave);
            }

            string dirYear = dirWave + @"\" + samplingDate.Year + "年";
            if (Directory.Exists(dirYear) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirYear);
            }

            string dirMonth = dirYear + @"\" + samplingDate.Month.ToString("00") + "月";
            if (Directory.Exists(dirMonth) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirMonth);
            }

            string dirDay = dirMonth + @"\" + samplingDate.Day.ToString("00") + "日";
            if (Directory.Exists(dirDay) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirDay);
            }

            string fileFullName = dirDay + @"\" + filename;
            if (File.Exists(fileFullName) == true)
            {
                //删除已存在名字
                File.Delete(fileFullName);
            }
            return fileFullName;
        }
        #endregion

        #region 对波形文件进行压缩、删除处理
        /// <summary>
        /// 对波形文件进行压缩、删除处理
        /// </summary>
        /// <param name="path">存储波形路径</param>
        private static void CompressAndDeleteLog(string path)
        {
            int days = GetCompressLogDays();
            if (days > 0)
            {
                if (DateTime.Now.Date != LastCompressDate.Date)
                {
                    if ((DateTime.Now.Date - LastCompressDate.Date).Days >= days)
                    {
                        Thread threadCompress = new Thread(CompresseAndDeleteLog_Thread);
                        threadCompress.Start(path);
                    }
                }
            }
        }
        #endregion

        #region 压缩并删除波形文件
        /// <summary>
        /// 压缩并删除波形文件
        /// </summary>
        /// <param name="obj">波形文件路径</param>
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

                FileInfo[] arrFile = di.GetFiles();
                int days = GetCompressLogDays();
                foreach (FileInfo info in arrFile)
                {
                    if ((DateTime.Now.Date - info.CreationTime.Date).Days >= days)
                    {
                        //逐个文件压缩
                        Compress(info);

                        //删除已压缩文件
                        File.Delete(info.FullName);
                    }
                }
                //更新压缩时间
                LastCompressDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 压缩波形文件
        /// <summary>
        /// 压缩波形文件
        /// </summary>
        /// <param name="fi">波形文件信息对象</param>
        private static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".zip")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create(fi.FullName + ".zip"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into the compression stream.
                            inFile.CopyTo(Compress);
                        }
                    }
                }
            }
        }
        #endregion

        #region 取得压缩时间间隔
        /// <summary>
        /// 取得压缩时间间隔
        /// </summary>
        /// <returns></returns>
        private static int GetCompressLogDays()
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

        #region 读取波形数据
        /// <summary>
        /// 读取波形数据
        /// </summary>
        /// <param name="waveFullName">波形文件全名称</param>
        /// <returns></returns>
        public static List<double> GetWaveDataFromFile(string waveFullName)
        {
            if (File.Exists(waveFullName) == false)
            {
                Exception ex = new Exception(string.Format("波形数据文件{0}不存在", waveFullName));
                LogHelper.WriteLog(ex);
                return null;
            }

            try
            {
                using (FileStream aFile = new FileStream(waveFullName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(aFile))
                    {
                        string datas = sr.ReadToEnd();
                        string[] datasStr = datas.Split(SeparatorChar);
                        return Array.ConvertAll<string, double>(datasStr, item => Math.Round(double.Parse(item), 2)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }
        #endregion
    }
}
