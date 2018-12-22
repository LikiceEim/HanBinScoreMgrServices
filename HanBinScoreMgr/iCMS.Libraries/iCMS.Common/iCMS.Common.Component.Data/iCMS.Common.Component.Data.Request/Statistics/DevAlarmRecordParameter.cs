/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  DevAlarmRecordParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：获取设备报警记录
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    #region 获取设备报警记录

    /// <summary>
    /// 获取设备报警记录
    /// </summary>
    public class DevAlarmRecordParameter : BaseRequest
    {
        //把 DateTime 改成 DateTime? 张辽阔 2016-11-10 修改
        public DateTime? BDate { get; set; }
        //把 DateTime 改成 DateTime? 张辽阔 2016-11-10 修改
        public DateTime? EDate { get; set; }
        public int? MSAlmID { get; set; }
        public int? MonitorTreeID { get; set; }
        public int? AlmStatus { get; set; }
        public int? DevID { get; set; }
        public int? MSiteID { get; set; }
        public int? SignalID { get; set; }
        public int? SignalAlmID { get; set; }
        public string DateType { get; set; }
        public int ViewStatus { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? IsStartStopFunc { get; set; }

        public int UserID { get; set; }


    }

    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-10-23
    /// 创建记录：监测树级联下拉列表查询条件
    /// </summary>
    public class MonitorTreeListForSelectParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 下拉列表类别
        /// -1：最高级别监测树
        /// 1：监测树级别
        /// 2：设备级别
        /// 3：测点级别
        /// 4：振动信号级别
        /// 5：特征值级别
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID { get; set; }
    }

    #endregion
}