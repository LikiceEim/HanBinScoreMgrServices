/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  VibSignalRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：添加振动信号信息参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 添加振动信号信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：添加振动信号信息参数
    /// </summary>
    public class AddVibSignalRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 1或空为定时；2为临时。数据库有默认值不为空，王颖辉修改 2016-08-03
        /// </summary>
        public int DAQStyle { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SignalType { get; set; }

        /// <summary>
        /// 上限频率
        /// </summary>
        public int? UpLimitFrequency { get; set; }

        /// <summary>
        /// 下限频率
        /// </summary>
        public int? LowLimitFrequency { get; set; }

        /// <summary>
        /// 波长
        /// </summary>
        public int WaveDataLength { get; set; }

        /// <summary>
        /// 包络带宽
        /// </summary>
        public int? EnlvpBandW { get; set; }

        /// <summary>
        /// 包络滤波器
        /// </summary>
        public int? EnlvpFilter { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion
}