/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  GetWSStatusAndReportStatisticsResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 18:51:28 
 *描述：请求基类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 获取当前传感器状态及报告统计
    /// </summary>
    public class GetWSStatusAndReportStatisticsResult
    {
        /// <summary>
        /// ws状态集合
        /// </summary>
        public List<WSStatusInfoList> WSStatusInfoList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// ws状态集合
    /// </summary>
    public class WSStatusInfoList
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
        public int WSStatus
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


    }

    /// <summary>
    /// 获取ws报警状态 月份 
    /// </summary>
    public class WSAlartMonthStatResult
    {
        public int AlmStatus
        {
            get;
            set;
        }

        public string Month
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 获取ws报警状态 日期 
    /// </summary>
    public class WSAlartDayStatResult
    {
        public int AlmStatus
        {
            get;
            set;
        }

        public string Day
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备维修日志月份
    /// </summary>

    public class DeviceMaintenanceLogMonthStat
    {
        /// <summary>
        /// 设备维修日志数量
        /// </summary>
        public int DeviceDiagnosticReport
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Month
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 设备维修日志月份
    /// </summary>

    public class DeviceMaintenanceLogDayStat
    {
        /// <summary>
        /// 设备维修日志数量
        /// </summary>
        public int DeviceDiagnosticReport
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Day
        {
            get;
            set;
        }
    }
}
