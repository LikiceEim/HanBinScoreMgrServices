/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  MSiteAlmAndSignalDataForDeviceTreeResult
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：返回测量位置报警和振动信号数据信息的参数
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回测量位置报警和振动信号数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回测量位置报警和振动信号数据信息的参数
    /// </summary>
    public class MSiteAlmAndSignalDataForDeviceTreeResult
    {
        /// <summary>
        /// 振动信息集合
        /// </summary>
        public List<VibSignalInfo> VibSignalInfoList { set; get; }

        /// <summary>
        /// 测量位置报警信息集合
        /// </summary>
        public List<MSiteAlmInfo> MSiteAlmInfoList { set; get; }
    }
    #endregion

    #region 振动信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：振动信息实体
    /// </summary>
    public class VibSignalInfo
    {
        /// <summary>
        /// 信号ID
        /// </summary>
        public int WID { get; set; }

        /// <summary>
        /// 信号ID
        /// </summary>
        public int SingalID { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 1或空为定时；2为临时。
        /// </summary>
        public int? DAQStyle { get; set; }

        /// <summary>
        /// 上限频率
        /// </summary>
        public int? UpLimitFrequencyId { get; set; }

        /// <summary>
        /// 上限频率名称
        /// </summary>
        public string UpLimitFrequencyName { get; set; }

        /// <summary>
        /// 下限频率
        /// </summary>
        public int? LowLimitFrequencyId { get; set; }

        /// <summary>
        /// 下限频率名称
        /// </summary>
        public string LowLimitFrequencyName { get; set; }

        /// <summary>
        /// 包络带宽
        /// </summary>
        public int? EnlvpBandWId { get; set; }

        /// <summary>
        /// 包络带宽名称
        /// </summary>
        public string EnlvpBandWName { get; set; }

        /// <summary>
        /// 包络滤波器
        /// </summary>
        public int? EnlvpFilterId { get; set; }

        /// <summary>
        /// 包络滤波器名称
        /// </summary>
        public string EnlvpFilterName { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SignalTypeId { get; set; }

        /// <summary>
        /// 信号类型名称
        /// </summary>
        public string SignalTypeName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int SignalStatus { get; set; }

        /// <summary>
        /// 状态更新时间
        /// </summary>
        public DateTime SignalSDate { get; set; }

        /// <summary>
        /// 波长
        /// </summary>
        public int WaveDataLengthTypeId { get; set; }

        /// <summary>
        /// 波长名称
        /// </summary>
        public string WaveDataLengthName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 子级数量
        /// </summary>
        public int ChildrenCount { get; set; }
    }
    #endregion

    #region 测量位置报警信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：测量位置报警信息实体
    /// </summary>
    public class MSiteAlmInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int WID { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int MsiteAlmID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int MSDType { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 警告值
        /// </summary>
        public float WarnValue { get; set; }

        /// <summary>
        /// 告警值
        /// </summary>
        public float AlmValue { get; set; }

        /// <summary>
        /// 告警状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }
    }
    #endregion
}