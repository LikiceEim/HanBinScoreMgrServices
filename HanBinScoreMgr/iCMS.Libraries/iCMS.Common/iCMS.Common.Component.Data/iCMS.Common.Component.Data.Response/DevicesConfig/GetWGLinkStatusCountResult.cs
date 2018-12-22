/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetWGLinkStatusCountResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 15:39:48 
 *描述：获取WG连接状态统计
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取WG连接状态统计
    /// </summary>
    public class GetWGLinkStatusCountResult
    {

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatusCount
        {
            get;
            set;
        }

        /// <summary>
        /// 断开状态
        /// </summary>
        public int UnLinkCount
        {
            get;
            set;
        }

    }
}
