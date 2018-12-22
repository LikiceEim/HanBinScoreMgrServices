/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Statistics
 *文件名：  AlarmRemindResult
 *创建人：  王龙杰
 *创建时间：2017-10-12
 *描述：报警提醒
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 设备报警提醒
    /// <summary>
    /// 报警设备
    /// </summary>
    public class AlertDevice : EntityBase
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        public int? AlmStatus { get; set; }

        /// <summary>
        /// 地点路径(**#**#**)
        /// </summary>
        public string MonitorTreeRouteID { get; set; }
        public string MonitorTreeRoute { get; set; }

        /// <summary>
        /// 最后报警时间
        /// </summary>
        public DateTime? LatestStartTime { get; set; }
    }

    /// <summary>
    /// 设备报警提醒
    /// </summary>
    public class DeviceAlarmRecord : EntityBase
    {
        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int AlermRecordID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public String DeviceNO { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        public int AlmStatus { get; set; }

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
        /// 采集值
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
        /// 监测树ID
        /// </summary>
        public string MonitorTreeID { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int MSAlmID { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime BDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EDate { get; set; }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 最后一次报警时间
        /// </summary>
        public DateTime LatestStartTime
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 通过用户ID获取未确认报警设备数量
    /// </summary>
    public class AlertDeviceCountResult
    {
        /// <summary>
        /// 未确认报警设备数量
        /// </summary>
        public int AlertDeviceCount { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未确认报警设备
    /// </summary>
    public class GetAlertDeviceByUserIDResult
    {
        public GetAlertDeviceByUserIDResult()
        {
            Total = 0;
            AlertDeviceList = new List<AlertDevice>();
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 未确认报警设备集合
        /// </summary>
        public List<AlertDevice> AlertDeviceList { get; set; }
    }

    /// <summary>
    /// 获取未确认的设备报警提醒
    /// </summary>
    public class GetDeviceAlertByDeviceIDResult
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 设备报警提醒集合
        /// </summary>
        public List<DeviceAlarmRecord> DeviceAlarmRecordList { get; set; }
    }
    #endregion

    #region 网关/传感器报警提醒
    /// <summary>
    /// 报警网关/传感器
    /// </summary>
    public class AlertSensor : EntityBase
    {
        /// <summary>
        /// 监测设备ID
        /// </summary>
        public int MonitorDeviceID { get; set; }

        /// <summary>
        /// 监测设备名称
        /// </summary>
        public string MonitorDeviceName { get; set; }

        /// <summary>
        /// 挂靠网关名称（监测设备类型为传感器时可用）
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        public int? AlmStatus { get; set; }

        /// <summary>
        /// 地点路径(**#**#**)
        /// </summary>
        public string MonitorTreeRouteID { get; set; }
        public string MonitorTreeRoute { get; set; }

        /// <summary>
        /// 最后报警时间
        /// </summary>
        public DateTime? LatestStartTime { get; set; }
    }

    /// <summary>
    /// 网关/传感器报警提醒
    /// </summary>
    public class SensorAlarmRecord : EntityBase
    {
        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int AlermRecordID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public String DeviceNO { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

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
        public string MonitorTreeRoute { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int MSAlmID { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime BDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EDate { get; set; }

        /// <summary>
        /// 最后报警时间
        /// </summary>
        public DateTime LatestStartTime { get; set; }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime? SamplingDate
        {
            get;
            set;
        }

        /// <summary>
        /// 采集值
        /// </summary>
        public float? SamplingValue
        {
            get;
            set;
        }

        /// <summary>
        /// 报警值
        /// </summary>
        public float? AlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 危险值
        /// </summary>
        public float? DangerValue
        {
            get;
            set;
        }


    }

    /// <summary>
    /// 通过用户ID获取未确认报警网关/传感器数量
    /// </summary>
    public class AlertSensorCountResult
    {
        /// <summary>
        /// 未确认报警设备数量
        /// </summary>
        public int AlertSensorCount { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未确认报警网关/传感器
    /// </summary>
    public class GetAlertSensorByUserIDResult
    {
        public GetAlertSensorByUserIDResult()
        {
            Total = 0;
            AlertSensorList = new List<AlertSensor>();
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 未确认报警设备集合
        /// </summary>
        public List<AlertSensor> AlertSensorList { get; set; }
    }

    /// <summary>
    /// 获取未确认的网关/传感器报警提醒
    /// </summary>
    public class GetSensorAlertByIDResult
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 设备报警提醒集合
        /// </summary>
        public List<SensorAlarmRecord> SensorAlarmRecord { get; set; }
    }
    #endregion

    public class AlertCountResult
    {
        public int AlertDeviceCount { get; set; }

        public int AlertWGCount { get; set; }

        public int AlertWSCount { get; set; }
    }

}
