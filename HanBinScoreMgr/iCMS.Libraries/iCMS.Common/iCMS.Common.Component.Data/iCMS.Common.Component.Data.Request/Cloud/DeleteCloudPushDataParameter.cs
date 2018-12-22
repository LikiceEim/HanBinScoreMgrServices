/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud
 *文件名：  DeleteCloudPushDataParameter
 *创建人：  张辽阔
 *创建时间：2016-12-07
 *描述：删除推送数据表的参数
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
    /// 创建时间：2016-12-07
    /// 创建记录：删除推送数据表的参数
    /// </summary>
    public class DeleteCloudPushDataParameter : BaseRequest
    {
        /// <summary>
        /// 数据推送表主键ID
        /// </summary>
        public List<int> CloudPushID { get; set; }

        public DeleteCloudPushDataParameter()
        {
            CloudPushID = new List<int>();
        }
    }
}