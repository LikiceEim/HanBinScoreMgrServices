/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：  UploadWaveDataParameter
 * 创建人：  QXM
 * 创建时间：2018/01/11
 * 描述：    DAUAgent波形数据上传请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class UploadWaveDataParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-18
        /// 创建记录：测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 采集单元ID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 采集数据通道ID
        /// </summary>
        public int SampleDataChannelID { get; set; }

        /// <summary>
        /// 波形数据
        /// </summary>
        public float[] WaveData { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-03-23
        /// 创建记录：是否是波形
        /// </summary>
        public bool IsWave
        {
            get { return WaveData != null && WaveData.Length > 0; }
        }

        /// <summary>
        /// 数据点数
        /// </summary>
        public int SamplingPointData { get; set; }

        /// <summary>
        /// 波形类型 1：加速度，2：速度，4：包络
        /// </summary>
        public int WaveType { get; set; }

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
        public float? EffectiveValue { get; set; }

        /// <summary>
        /// 地毯值
        /// </summary>
        public float? CarpetValue { get; set; }
    }
}