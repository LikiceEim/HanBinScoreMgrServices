/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  GetDeviceTopographicMapStatisticsResult 
 *创建人：  王颖辉
 *创建时间：2017/10/13 17:18:27 
 *描述：获取当前设备状态及报告统计
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 获取当前设备状态及报告统计
    /// </summary>
    public class GetDeviceTopographicMapStatisticsResult
    {
        /// <summary>
        /// 设备报警信息列表
        /// </summary>
        public List<DeviceAlarmInfoList> DeviceAlarmInfoList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备报警信息列表
    /// </summary>
    public class DeviceAlarmInfoList
    {

        /// <summary>
        /// 时间
        /// </summary>
        public string Date
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int DeviceStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 维修报告数
        /// </summary>
        public int DeviceMaintenanceLogCount
        {
            get;
            set;
        }

        /// <summary>
        /// 诊断报告数
        /// </summary>
        public int DeviceDiagnosticReport
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class DeviceDiagnosticReportDayStatResult
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int DeviceDiagnosticReport
        {
            get;
            set;
        }

        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public string Day
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class DeviceMaintenanceLogDayStatResult
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int DeviceMaintenanceLogCount
        {
            get;
            set;
        }

        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public string Day
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class AlartDayStat
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public string Day
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class DeviceMaintenanceLogMonthAlarmStatResult
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class DeviceMaintenanceMonthLogStat
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int DeviceMaintenanceLogCount
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备诊断报告返回值
    /// </summary>
    public class DeviceDiagnosticReportMonthStat
    {
        /// <summary>
        /// 设备诊断报告数量
        /// </summary>
        public int DeviceDiagnosticReport
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get;
            set;
        }
    }
}
