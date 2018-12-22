/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Statistics
 *文件名：  WsnAlarmRecordResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：无传传感器报警记录返回值
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 无传传感器报警记录返回值
    /// <summary>
    /// 无传传感器报警记录返回值
    /// </summary>
    public class WsnAlarmRecordResult
    {
        /// <summary>
        /// 无线传感器返回实体类集合
        /// </summary>
        public List<AlarmRecordInfo> AlarmRecordInfo { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
    #endregion

    #region 报警记录
    /// <summary>
    /// 报警记录
    /// </summary>

    public class AlarmRecordInfo
    {
        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int AlmRecordID { get; set; }

        /// <summary>
        /// 报警是否接结束 1：未结束，2：已结束
        /// </summary>
        public int IsEnd { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int? DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int? DevNo { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int? MSiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName { get; set; }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public string MonitorTreeID { get; set; }

        /// <summary>
        /// 监测树路径
        /// </summary>
        public string MonitorTreeRoute { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int MSAlmID { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 采集数据
        /// </summary>
        public float? SamplingValue { get; set; }

        /// <summary>
        /// 高报阈值
        /// </summary>
        public float? WarningValue { get; set; }

        /// <summary>
        /// 高高报阈值
        /// </summary>
        public float? DangerValue { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EDate { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 最近发生时间
        /// </summary>
        public DateTime LatestStartTime { get; set; }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 查看状态
        /// </summary>
        public bool ViewStatus
        { get; set; }
    }
    #endregion
}