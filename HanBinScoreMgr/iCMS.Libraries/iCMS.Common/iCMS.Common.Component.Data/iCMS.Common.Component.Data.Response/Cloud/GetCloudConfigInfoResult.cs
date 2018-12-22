/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud
 *文件名：  GetCloudConfigInfoResult
 *创建人：  张辽阔
 *创建时间：2016-12-14
 *描述：获取云通讯配置信息返回结果
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
    /// 创建时间：2016-12-14
    /// 创建记录：获取云通讯配置信息返回结果
    /// </summary>
    public class GetCloudConfigInfoResult
    {
        /// <summary>
        /// 云通讯配置信息集合
        /// </summary>
        public List<CloudConfigInfo> CloudConfigInfoList { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-14
    /// 创建记录：云通讯配置信息实体
    /// </summary>
    public class CloudConfigInfo : CloudConfig
    {
    }

    public class PlatformInfoResult2
    {
        public string BaseURL { get; set; }
    }
}