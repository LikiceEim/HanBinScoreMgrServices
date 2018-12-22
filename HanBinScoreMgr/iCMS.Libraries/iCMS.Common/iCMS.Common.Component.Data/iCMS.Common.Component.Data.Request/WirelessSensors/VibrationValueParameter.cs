/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  VibrationValueParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：无线传感器上传特征值信息实体类
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 无线传感器上传特征值信息实体类
    /// <summary>
    /// 无线传感器上传特征值信息实体类
    /// </summary>
    public class VibrationValueParameter : BaseRequest
    {
        /// <summary>
        /// 速度信号类型：2
        /// </summary>
        public int VelSignalType { get; set; }
        /// <summary>
        /// 速度峰值
        /// </summary>
        public float? VelPeakValue { get; set; }

        /// <summary>
        /// 速度峰峰值
        /// </summary>
        public float? VelPeakPeakValue { get; set; }

        /// <summary>
        /// 速度有效值
        /// </summary>
        public float? VelEffValue { get; set; }

        /// <summary>
        /// 速度低频能量值
        /// </summary>
        public float? VelLPEValue { get; set; }

        /// <summary>
        /// 速度中频能量值
        /// </summary>
        public float? VelMPEValue { get; set; }

        /// <summary>
        /// 速度高频能量值
        /// </summary>
        public float? VelHPEValue { get; set; }
        /// <summary>
        /// 加速度信号类型：1
        /// </summary>
        public int ACCSignalType { get; set; }
        /// <summary>
        /// 加速度峰值
        /// </summary>
        public float? ACCPeakValue { get; set; }

        /// <summary>
        /// 加速度峰峰值
        /// </summary>
        public float? ACCPeakPeakValue { get; set; }

        /// <summary>
        /// 加速度有效值
        /// </summary>
        public float? ACCEffValue { get; set; }

        /// <summary>
        /// 包络信号类型：4
        /// </summary>
        public int EnvlSignalType { get; set; }

        /// <summary>
        /// 包络峰值
        /// </summary>
        public float? EnvlPeakValue { get; set; }

        /// <summary>
        /// 包络地毯值
        /// </summary>
        public float? EnvlCarpetValue { get; set; }

        /// <summary>
        /// 包络均值
        /// </summary>
        public float? EnvlMeanValue { get; set; }

        /// <summary>
        /// 位移信号类型：3
        /// </summary>
        public int DispSignalType { get; set; }

        /// <summary>
        /// 位移峰值
        /// </summary>
        public float? DispPeakValue { get; set; }

        /// <summary>
        /// 位移峰峰值
        /// </summary>
        public float? DispPeakPeakValue { get; set; }
        /// <summary>
        /// 位移有效值
        /// </summary>
        public float? DispEffValue { get; set; }

        /// <summary>
        /// 轴承状态信号类型：5
        /// </summary>
        public int LQSignalType { get; set; }

        /// <summary>
        /// 轴承状态
        /// </summary>
        public float? LQValue { get; set; }

        /// <summary>
        /// 采集方式 1：定时采集；2：临时采集
        /// </summary>
        public string DAQStyle { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public System.DateTime SamplingTime { get; set; }
        /// <summary>
        /// WS MAC地址
        /// </summary>
        public string WSMAC { get; set; }
    }
    #endregion
}
