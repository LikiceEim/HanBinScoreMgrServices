/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  CommonMethods
 *创建人：  张辽阔
 *创建时间：2016-08-01
 *描述：公共方法
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class CalculateWaveXYValue
    {
        private static object LockFile = new object();

        #region 取得时域Y轴数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-01
        /// 创建记录：得到时域Y轴数据
        /// </summary>
        /// <param name="path">Y轴数据存储路径</param>
        /// <returns></returns>
        public static List<double> GetTDYData(string path)
        {
            try
            {
                lock (LockFile)
                {
                    if (File.Exists(path) == true)
                    {
                        // 波形文件存在
                        FileStream aFile = new FileStream(path, FileMode.Open);
                        StreamReader sr = new StreamReader(aFile);
                        string datas = sr.ReadToEnd();
                        string[] datasStr = datas.Split(',');
                        aFile.Dispose();
                        sr.Dispose();
                        return Array.ConvertAll<string, double>(datasStr, item => Math.Round(double.Parse(item), 2)).ToList();
                    }
                    else
                    {
                        //波形文件不存在
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //异常时
                return null;
            }
        }

        #endregion

        #region 得到时域X轴数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-01
        /// 创建记录：得到时域X轴数据
        /// </summary>
        /// <param name="samplingPointData">采样点数</param>
        /// <param name="fs">时间采样频率</param>
        /// <returns></returns>
        public static List<double> GetTDXData(int samplingPointData, float fs)
        {
            List<double> xs = new List<double>();
            float s = ((float)1 / fs);
            for (int i = 0; i < samplingPointData; i++)
            {
                xs.Add(s * i);
            }
            return xs;
        }

        #endregion

        #region 得到频域X轴数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-02
        /// 创建记录：得到频域X轴数据
        /// </summary>
        /// <param name="samplingPointData">采样点数</param>
        /// <param name="fs">采样频率</param>
        /// <returns></returns>
        public static List<double> GetFDXData(int samplingPointData, float fs)
        {

            List<double> xs = new List<double>();
            double s = (double)fs / samplingPointData;
            for (int i = 0; i < samplingPointData / 2; i++)
            {
                xs.Add(s * i);
            }
            return xs;
        }

        #endregion

        #region 得到频域Y轴数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-02
        /// 创建记录：得到频域Y轴数据
        /// </summary>
        /// <param name="path">Y轴数据存储路径</param>
        /// <param name="fs">采样频率</param>
        /// <param name="isEnvl">是否包络</param>
        /// <returns></returns>
        public static List<double> GetFDYData(string path, float fs, bool isEnvl)
        {
            try
            {
                lock (LockFile)
                {
                    FileStream aFile = new FileStream(path, FileMode.Open);
                    StreamReader sr = new StreamReader(aFile);
                    string datas = sr.ReadToEnd();
                    string[] datasStr = datas.Split(',');
                    aFile.Dispose();
                    sr.Dispose();
                    double[] das = Array.ConvertAll<string, double>(datasStr, item => double.Parse(item));

                    //包络频谱做均值拉平处理
                    if (isEnvl == true)
                    {
                        double dc = das.Average();

                        for (int inx = 0; inx < das.Length; inx++)
                        {
                            das[inx] = das[inx] - dc;
                        }
                    }

                    return FrepTools.GetFrepData(das, (double)fs).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        #endregion

        #region 得到频域Y轴数据
        /// <summary>
        /// 通过波形数据（List）得到频域Y轴数据
        /// </summary>
        /// <param name="wave"></param>
        /// <param name="fs"></param>
        /// <param name="isEnvl"></param>
        /// <returns></returns>
        public static List<double> GetFDYData(List<float> wave, float fs, bool isEnvl)
        {
            try
            {
                if (wave == null || wave.Count == 0)
                {
                    return null;
                }

                float[] waveF = wave.ToArray();
                double[] das = new double[waveF.Length];
                for (int i = 0; i < das.Length; i++)
                {
                    das[i] = (double)waveF[i];
                }

                if (isEnvl == true)
                {
                    //包络频谱均值拉平
                    double dc = das.Average();

                    for (int inx = 0; inx < das.Length; inx++)
                    {
                        das[inx] = das[inx] - dc;
                    }
                }
                return FrepTools.GetFrepData(das, (double)fs).ToList();

            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }
        #endregion

        #region 计算电机特征频率

        #region 计算转子基频
        /// <summary>
        /// 计算转子基频
        /// </summary>
        /// <param name="motorSpeed">电机实际转速</param>
        /// <returns>转子基频（单位Hz）</returns>
        public static double GetRFFrequency(double motorSpeed)
        {
            //转子基频 = 电机实际转速 / 60
            return motorSpeed / 60;
        }
        #endregion

        #region 计算滑差频率
        /// <summary>
        /// 计算滑差频率
        /// </summary>
        /// <param name="PwFrequency">电源频率</param>
        /// <param name="MPLogarithmic">电机极对数</param>
        /// <param name="motorSpeed">电机实际转速</param>
        /// <returns></returns>
        public static double GetSlipFrequency(int PwFrequency, int MPLogarithmic, double motorSpeed)
        {
            //电机同步转速 = (电源频率 * 60) / 电机极对数
            double motorSyncSpeed = (PwFrequency * 60) / MPLogarithmic;
            //滑差频率 = （电机同步转速 - 电机实际转速）/ 60
            return (motorSyncSpeed - motorSpeed) / 60;
        }
        #endregion

        #region 计算极通过频率
        /// <summary>
        /// 计算极通过频率
        /// </summary>
        /// <param name="PwFrequency">电源频率</param>
        /// <param name="MPLogarithmic">电机极对数</param>
        /// <param name="motorSpeed">电机实际转速</param>
        /// <returns></returns>
        public static double GetExFrequency(int PwFrequency, int MPLogarithmic, double motorSpeed)
        {
            //极通过频率 = 滑差频率 * 电机极对数
            return GetSlipFrequency(PwFrequency, MPLogarithmic, motorSpeed) * MPLogarithmic;
        }
        #endregion

        #region 计算转子通过频率
        /// <summary>
        /// 计算转子通过频率
        /// </summary>
        /// <param name="RANumber">转子条数</param>
        /// <param name="motorSpeed">电机实际转速</param>
        /// <returns></returns>
        public static double GetRotorFrequency(int RANumber, double motorSpeed)
        {
            //转子通过频率 = 转子条数 * 转子基频
            return GetRFFrequency(motorSpeed) * RANumber;
        }
        #endregion

        #endregion

        //add by iLine 20160622
        #region 将波形数据转换为byte数组
        /// <summary>
        /// 将波形数据转换为byte数组
        /// </summary>
        /// <param name="waveValues"></param>
        /// <returns></returns>
        public static byte[] GetWaveValueBytes(List<float> waveValues)
        {
            byte[] bData = new byte[waveValues.Count * 4];
            int index = 0;
            foreach (float value in waveValues)
            {
                Array.Copy(BitConverter.GetBytes(value), 0, bData, index * 4, 4);
                index++;
            }

            return bData;
        }
        #endregion

        //add by iLine 20160622
        #region 将byte数组转换为字符串
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wave"></param>
        /// <returns></returns>
        public static string GetWaveValueByteString(byte[] wave)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wave.Length; i++)
            {
                sb.Append(wave[i].ToString("X2"));
            }

            return sb.ToString();
        }
        #endregion

    }
}