/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  FrepTools
 *创建人：  张辽阔
 *创建时间：2016-08-02
 *描述：波形工具
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppFramework.Analysis;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-08-02
    /// 创建记录：波形工具
    /// </summary>
    public class FrepTools
    {
        /// <summary>
        /// 采样频率
        /// </summary>
        public static float FS = 51200;

        /// <summary>
        /// 得到频域数据
        /// </summary>
        /// <param name="accTimeWave">波形数据</param>
        /// <param name="fs">采集频率</param>
        /// <returns></returns>
        public static double[] GetFrepData(double[] accTimeWave, double fs)
        {
            return FrepSpectrum.AmplitudeSpectrum(accTimeWave, fs);
        }
    }
}