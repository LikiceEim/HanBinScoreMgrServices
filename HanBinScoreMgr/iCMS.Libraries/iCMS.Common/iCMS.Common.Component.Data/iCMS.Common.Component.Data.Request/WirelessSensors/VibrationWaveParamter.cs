/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  VibrationWaveParamter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：无线传感器上传波形信息实体类
 *=====================================================================**/
using System.Collections.Generic;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 无线传感器上传波形信息实体类
    /// <summary>
    /// 无线传感器上传波形信息实体类
    /// </summary>
    public class VibrationWaveParamter : BaseRequest
    {
        /// <summary>
        /// 信号类型 1：加速度；2：速度；3：位移；4：包络；5： LQ;
        /// </summary>
        public int SignalType { get; set; }
        /// <summary>
        /// 波形数据
        /// </summary>
        public List<float> WaveData { get; set; }
        /// <summary>
        /// 转换因子
        /// </summary>
        public float TranceformCofe { get; set; }
        /// <summary>
        ///波长 
        /// </summary>
        public float WaveLength { get; set; }
        /// <summary>
        /// 上限频率（包络时为空）
        /// </summary>
        public float UpLimitFrequency { get; set; }
        /// <summary>
        /// 下限频率（包络时为空）
        /// </summary>
        public float LowLimitFrequency { get; set; }
        /// <summary>
        /// 包络带宽（包络以外时为空）
        /// </summary>
        public float EnlvpBandWidth { get; set; }
        /// <summary>
        /// 包络滤波器（包络以外时为空）
        /// </summary>
        public float EnlvpFilter { get; set; }
        /// <summary>
        /// 峰值
        /// </summary>
        public float? PeakValue { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public float? PeakPeakValue { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public float? EffValue { get; set; }
        /// <summary>
        /// 地毯值
        /// </summary>
        public float? CarpetValue { get; set; }
        /// <summary>
        /// 轴承状态
        /// </summary>
        public float? LQValue { get; set; }

        /// <summary>
        /// 低频能量值
        /// </summary>
        public float? LPEValue { get; set; }
        /// <summary>
        /// 中频能量值
        /// </summary>
        public float? MPEValue { get; set; }
        /// <summary>
        /// 高频能量值
        /// </summary>
        public float? HPEValue { get; set; }
        /// <summary>
        /// 均值
        /// </summary>
        public float? MeanValue { get; set; }
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
