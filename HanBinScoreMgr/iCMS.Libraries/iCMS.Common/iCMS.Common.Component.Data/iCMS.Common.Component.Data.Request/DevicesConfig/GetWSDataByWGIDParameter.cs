/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetWSDataByWGIDParameter 
 *创建人：  王颖辉
 *创建时间：2017/9/28 14:01:32 
 *描述：获取网关下的传感器
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
    ///获取网关下的传感器
    /// </summary>
    public class GetWSDataByWGIDParameter : BaseRequest
    {

        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID
        {
            get;
            set;
        }

        /// <summary>
        /// WS是否使用
        /// </summary>
        public int IsUsed
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

    }
}
