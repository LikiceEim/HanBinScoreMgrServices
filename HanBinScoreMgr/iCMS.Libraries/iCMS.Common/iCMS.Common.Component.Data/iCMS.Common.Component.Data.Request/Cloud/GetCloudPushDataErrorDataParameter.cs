/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud
 *文件名：  GetCloudPushDataErrorDataParameter
 *创建人：  张辽阔
 *创建时间：2016-12-19
 *描述：获取推送数据表DataError的参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-19
    /// 创建记录：获取推送数据表DataError的参数
    /// </summary>
    public class GetCloudPushDataErrorDataParameter : BaseRequest
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int PlatformId { get; set; }
    }
}