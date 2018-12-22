/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Statistics
 *文件名：  DevAlarmRecordResult
 *创建人：  王颖辉
 *创建时间：2016-10-26
 *描述：设备报警记录结果返回
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 设备报警记录结果返回
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-08-02
    /// 创建记录：设备报警记录结果返回
    /// </summary>
    public class DevAlarmRecordResult
    {
        /// <summary>
        /// 设备返回实体类集合
        /// </summary>
        public List<DevAlarmRecordInfo> AlarmRecordInfo { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-08-02
    /// 创建记录：设备报警记录返回实体
    /// </summary>
    public class DevAlarmRecordInfo
    {
        /// <summary>
        /// 报警是否接结束
        /// </summary>
        public int IsEnd { get; set; }

        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int AlmRecordID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevNo { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName { get; set; }

        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int SingalID { get; set; }

        /// <summary>
        /// 振动信号名称
        /// </summary>
        public string SingalName { get; set; }

        /// <summary>
        /// 特征值ID
        /// </summary>
        public int SingalAlmID { get; set; }

        /// <summary>
        /// 特征值
        /// </summary>
        public string SingalValue { get; set; }

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
        /// 趋势报警阈值
        /// </summary>
        public float? ThrendAlarmPrvalue { get; set; }

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
        /// 设备是否删除
        /// </summary>
        public bool IsDeleted
        { get; set; }

        /// <summary>
        /// 查看状态
        /// </summary>
        public int ViewStatus
        { get; set; }
    }

    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-10-23
    /// 创建记录：获取系统中可用的监测树类型数量
    /// </summary>
    public class MonitorTreeTypeCountResult
    {
        public int MonitorTreeTypeCount { get; set; }
    }

    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-10-23
    /// 创建记录：监测树级联下拉列表查询条件
    /// </summary>
    public class MonitorTreeListForSelectResult
    {
        public List<SelectMTList> SelectMTList { get; set; }
    }
    public class SelectMTList
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }
    }
    #endregion
}