/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Statistics
 *文件名：  AlarmRemindParameter
 *创建人：  王龙杰
 *创建时间：2017-10-12
 *描述：设备报警提醒
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    /// <summary>
    /// 未确认报警设备数量
    /// </summary>
    public class AlarmRemindDeviceCountParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 监测设备类型（被监测设备不使用）
        /// 1：网关
        /// 2：传感器
        /// </summary>
        public int MonitorDeviceType { get; set; }
    }

    /// <summary>
    /// 未确认报警设备
    /// </summary>
    public class AlarmRemindDeviceParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 监测设备类型（被监测设备不使用）
        /// 1：网关
        /// 2：传感器
        /// </summary>
        public int MonitorDeviceType { get; set; }

        /// <summary>
        /// 显示数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页  -1：全部
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order { get; set; }
    }

    /// <summary>
    /// 未确认报警提醒记录
    /// </summary>
    public class DeviceAlertRecordParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 监测设备类型/被监测设备ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 监测设备类型（被监测设备不使用）
        /// 1：网关
        /// 2：传感器
        /// </summary>
        public int MonitorDeviceType { get; set; }

        /// <summary>
        /// 显示数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页  -1：全部
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order { get; set; }
    }

    /// <summary>
    /// 报警提醒确认
    /// </summary>
    public class DeviceAlertConfirmParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 监测设备类型（被监测设备不使用）
        /// 1：网关
        /// 2：传感器
        /// </summary>
        public int MonitorDeviceType { get; set; }

        /// <summary>
        /// 被监测设备或监测设备ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int? AlermRecordID { get; set; }
    }
}
