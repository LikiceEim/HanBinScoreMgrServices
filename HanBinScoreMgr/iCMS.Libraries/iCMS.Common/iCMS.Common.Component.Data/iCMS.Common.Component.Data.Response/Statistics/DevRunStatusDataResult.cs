/************************************************************************************
* Copyright (c) 2016iLine All Rights Reserved.
*命名空间：iCMS.Common.Component.Data.Response.Statistics
*文件名：  DevRunStatusDataResult
*创建人：  王颖辉
*创建时间：2016-10-26
*描述：设备运行状态查看返回结果类
*
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 设备运行状态查看返回结果类
    /// <summary>
    /// 设备运行状态查看返回结果类
    /// </summary>
    public class DevRunStatusDataResult
    {
        public List<DevRunningStatListDataInfo> DevRunningStatListDataInfo { get; set; }

        public int Total { get; set; }

        public DevRunStatusDataResult()
        {
            DevRunningStatListDataInfo = new List<DevRunningStatListDataInfo>();
        }
    }


    /// <summary>
    /// 单个设备的状态类
    /// </summary>
    public class DevRunningStatListDataInfo : EntityBase
    {
        public int DevId { get; set; }

        public string DevName { get; set; }

        public string DevNo { get; set; }

        public int DevType { get; set; }

        public string DevTypeName { get; set; }

        public int UseType { get; set; }

        public int DevRunningStat { get; set; }
        /// <summary>
        /// 只有为运行时才有状态其它为-1 DevRunningStat = 1s
        /// </summary>
        public int DevAlarmStat { get; set; }

        public string LastUpdateTime { get; set; }
    }

    public class DevRunningStatusDataByUserIDResult
    {
        public List<GetDevRunningStatCountDataByUserId> GetDevRunningStatCountDataByUserId { get; set; }

        public int Total { get; set; }

        public DevRunningStatusDataByUserIDResult()
        {
            GetDevRunningStatCountDataByUserId = new List<GetDevRunningStatCountDataByUserId>();
        }
    }

    public class GetDevRunningStatCountDataByUserId
    {
        public int UseCount { get; set; }

        public int UnUseCount { get; set; }

        public int TotalCount { get; set; }

        public int NomalCount { get; set; }

        public int WarningCount { get; set; }

        public int AlarmCount { get; set; }
    }
    #endregion
}
