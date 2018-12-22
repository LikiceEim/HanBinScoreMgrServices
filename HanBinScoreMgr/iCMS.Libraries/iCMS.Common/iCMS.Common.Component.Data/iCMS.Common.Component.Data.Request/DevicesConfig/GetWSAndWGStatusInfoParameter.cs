/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetWSAndWGStatusInfoParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/14 17:00:57 
 *描述：获取用户管理的WS和网关信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 获取用户管理的WS和网关信息
    /// </summary>
    public class GetWSAndWGStatusInfoParameter : BaseRequest
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }


        /// <summary>
        /// 第几页
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// 每页数目
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order
        {
            get;
            set;
        }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderName
        {
            get;
            set;
        }
    }
}
