/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  CloudPushErrorDataParameter
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：获取云通讯推送维护页面查看接口
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 获取云通讯推送维护页面查看接口
    /// <summary>
    /// 获取云通讯推送维护页面查看接口
    /// </summary>
    public class CloudPushErrorDataParameter: BaseRequest
    {
        /// <summary>
        /// 推送平台Id-1：表示全部平台
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }
        /// <summary>
        /// 排序名 PlatformId，AddDate，OperationStatus，TableName
        /// </summary>
        public string Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 排序方式，desc/asc
        /// </summary>
        public string Order
        {
            get;
            set;
        }
        /// <summary>
        /// 页数，从1开始,若为-1返回所有的报警记录（不需要分页，返回所有的数据）
        /// </summary>
        public int Page
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
    }
    #endregion
}
