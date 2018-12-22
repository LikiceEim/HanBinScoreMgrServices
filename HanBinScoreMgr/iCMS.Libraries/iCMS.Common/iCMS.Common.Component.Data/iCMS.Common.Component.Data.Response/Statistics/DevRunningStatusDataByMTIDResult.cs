/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Statistics
 *文件名：  DevRunningStatCountDataInfoByMTId
 *创建人：  王颖辉
 *创建时间：2016-20-16
 *描述：获取设备运行状态的统计
 *
/************************************************************************************/

using System.Collections.Generic;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 获取设备运行状态的统计
    /// <summary>
    /// 获取设备运行状态的统计
    /// </summary>
    public class DevRunningStatCountDataInfoByMTId : EntityBase
    {
        public DevRunningStatCountDataInfoByMTId()
        {
            MonitorTreeId = -1;
            MonitorTreeName = string.Empty;
            TotalCount = 0;
            UseCount = 0;
            UnUseCount = 0;
            NomalCount = 0;
            WarningCount = 0;
            AlarmCount = 0;
        }
        /// <summary>
        /// 监测树 ID
        /// </summary>
        public int MonitorTreeId { get; set; }

        /// <summary>
        ///  监测树结点名称
        /// </summary>
        public string MonitorTreeName { get; set; }

        /// <summary>
        ///  总数	传入参数MTId下所有设备的数目
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 使用数	RUNSTAT状态为1型
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 未使用数	RUNSTAT状态为非1型 
        /// </summary>
        public int UnUseCount { get; set; }

        /// <summary>
        /// 正常数	传入参数MTId下所有AlmStat为1设备的数目
        /// </summary>
        public int NomalCount { get; set; }

        /// <summary>
        /// 高报数	传入参数MTId下所有AlmStat为2设备的数目
        /// </summary>
        public int WarningCount { get; set; }

        /// <summary>
        /// 高高报数	传入参数MTId下所有AlmStat为3设备的数目
        /// </summary>
        public int AlarmCount { get; set; }

        public string MonitorTreeType { get; set; }
    }

    /// <summary>
    /// 设备运行状态统计返回结果类
    /// </summary>
    public class DevRunningStatusDataByMTIDResult
    {
        public List<DevRunningStatCountDataInfoByMTId> DevRunningStatCountDataInfo { get; set; }

        public int Total { get; set; }

        public DevRunningStatusDataByMTIDResult()
        {
            DevRunningStatCountDataInfo = new List<DevRunningStatCountDataInfoByMTId>();
        }

    }
    #endregion
}
