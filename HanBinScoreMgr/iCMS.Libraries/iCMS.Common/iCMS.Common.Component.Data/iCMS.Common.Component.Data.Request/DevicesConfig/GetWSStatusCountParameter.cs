/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetWSStatusCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/14 14:46:35 
 *描述：获取用户所管理传感器连接状态统计
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
    /// 获取用户所管理传感器连接状态统计
    /// </summary>
    public class GetWSStatusCountParameter : BaseRequest
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
    }
}
