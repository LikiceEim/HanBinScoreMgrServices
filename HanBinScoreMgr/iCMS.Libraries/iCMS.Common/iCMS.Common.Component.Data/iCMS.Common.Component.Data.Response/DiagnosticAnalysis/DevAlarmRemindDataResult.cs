/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response
 *文件名：  DevAlarmRemindDataResult
 *创建人：  LF
 *创建时间：2016-08-04
 *描述：报警提醒返回结果实体类
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    #region 报警提醒返回结果实体类
    /// <summary>
    /// 创建人：LF
    /// 创建时间：2016-11-01
    /// 创建记录：报警提醒返回结果实体类
    /// </summary>
    public class DevAlarmRemindDataResult
    {
        /// <summary>
        /// 列表信息列表
        /// </summary>
        public List<DevAlarmRemindInfo> DevAlarmRemindDataInfo { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 最小刷新时间
        /// </summary>
        public int MinFreshTime { get; set; }

    }
    #endregion

    #region 报警提醒返回实体类
    /// <summary>
    /// 创建人：LF
    /// 创建时间：2016-11-01
    /// 创建记录：报警提醒返回实体类
    /// </summary>
    public class DevAlarmRemindInfo
    {

        /// <summary>
        /// 监测树id字符串组合，以正序方式用#隔开
        /// </summary>
        public string MTIDs { get; set; }

        /// <summary>
        /// 监测树name字符串组合，以正序方式用#隔开
        /// </summary>
        public string MTNames { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DevType { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlarmStat { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public string LastAlarmTime { get; set; }
    }
    #endregion
}