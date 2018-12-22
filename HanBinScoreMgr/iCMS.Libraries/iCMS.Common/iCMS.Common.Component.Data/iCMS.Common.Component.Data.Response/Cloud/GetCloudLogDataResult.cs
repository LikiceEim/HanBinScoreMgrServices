/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud
 *文件名：  GetCloudLogDataResult
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：获取云推送日志表返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 获取云推送日志表返回结果
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：获取云推送日志表返回结果
    /// </summary>
    public class GetCloudLogDataResult
    {
        /// <summary>
        /// 日志结果集
        /// </summary>
        public List<CloudLogInfo> CloudLogInfo { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int Total;
    }
    #endregion

    #region 日志页面信息集合
    /// <summary>
    /// 日志页面信息集合
    /// </summary>
    public class CloudLogInfo : CloudLog
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName
        {
            set;
            get;
        }
    }
    #endregion
}