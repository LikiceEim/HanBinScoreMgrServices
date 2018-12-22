/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetDeviceStatusStatisticResult 
 *创建人：  王颖辉
 *创建时间：2017/10/13 10:59:04 
 *描述：获取某监测树下设备状态统计
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取某监测树下设备状态统计
    /// </summary>
    public class GetDeviceStatusStatisticResult
    {
        /// <summary>
        /// 监测树类型
        /// </summary>
        public int MonitorTreeType
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树类型Code
        /// </summary>
        public string MonitorTreeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DeviceStatusStatistic> StatisticInfo
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 设备状态统计
    /// </summary>
    public class DeviceStatusStatistic
    {
        /// <summary>
        /// 未采集状态总数
        /// </summary>
        public int UnCollected
        {
            get;
            set;
        }

        /// <summary>
        /// 正常设备总数
        /// </summary>
        public int NormalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 报警设备总数
        /// </summary>
        public int AlertCount
        {
            get;
            set;
        }

        /// <summary>
        /// 危险设备总数
        /// </summary>
        public int DangerCount
        {
            get;
            set;
        }


        /// <summary>
        /// 停机状态总数
        /// </summary>
        public int StoppingCount
        {
            get;
            set;
        }


        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树名称
        /// </summary>
        public string MonitorTreeName
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 状态信息
    /// </summary>
    public class DeviceStatusInfo
    {

        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 监测树统计类型
    /// </summary>
    public class MonitorTreeStatusInfo
    {

        /// <summary>
        /// 监测树id
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树类型
        /// </summary>
        public int MonitorTreeType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 报警状态统计返回值
    /// </summary>
    public class AlarmStatusResult
    {
        /// <summary>
        /// 监测树Id
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 运行状态统计返回值
    /// </summary>
    public class RunStatusResult
    {
        /// <summary>
        /// 监测树Id
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get;
            set;
        }
    }
    
}
