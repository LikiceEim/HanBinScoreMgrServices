/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud
 *文件名：  GetCloudPushDataErrorDataResult
 *创建人：  张辽阔
 *创建时间：2016-12-07
 *描述：获取推送数据表返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-07
    /// 创建记录：获取推送数据表返回结果
    /// </summary>
    public class GetCloudPushDataErrorDataResult
    {
        /// <summary>
        /// 数据推送表结果集
        /// </summary>
        public List<CloudPush> CloudPushList { get; set; }
    }
}