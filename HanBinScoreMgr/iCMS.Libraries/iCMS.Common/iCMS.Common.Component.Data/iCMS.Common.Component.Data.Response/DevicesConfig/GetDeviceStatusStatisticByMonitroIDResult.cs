/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetDeviceStatusStatisticByMonitroIDResult 
 *创建人：  王颖辉
 *创建时间：2017/10/13 15:20:47 
 *描述：获取某监测树下所有设备状态
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取某监测树下所有设备状态
    /// </summary>
    public class GetDeviceStatusStatisticByMonitroIDResult
    {
        /// <summary>
        /// 设备运行状态统计列表信息
        /// </summary>
        public List<DevRunningStatListDataInfo> DevRunningStatListDataInfo
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
    }

    /// <summary>
    /// 设备运行状态统计列表信息
    /// </summary>
    public class DevRunningStatListDataInfo: EntityBase
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName
        {
            get;
            set;
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevNO
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DevType
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DevTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int UseType
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int DevRunningStat
        {
            get;
            set;
        }

        /// <summary>
        /// 设备报警状态
        /// </summary>
        public int DevAlarmStat
        {
            get;
            set;
        }

        /// <summary>
        /// 最新更新时间
        /// </summary>
        public DateTime LastUpdateTime
        {
            get;
            set;
        }
    }
}
