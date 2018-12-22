/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  CloudPushErrorDataResult
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：获取云通讯推送维护页面查看接口
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 获取云通讯推送维护页面查看接口
    /// <summary>
    /// 获取云通讯推送维护页面查看接口
    /// </summary>
    public class CloudPushErrorDataResult
    {
        /// <summary>
        /// 推送维护页面信息集合
        /// </summary>
        public List<CloudMaintainInfo> CloudMaintainInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 推送总数
        /// </summary>
        public int Total
        {
            set;
            get;
        }
    }
    #endregion

    #region 推送数据
    /// <summary>
    /// 推送数据
    /// </summary>
    public class CloudMaintainInfo : CloudPush
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName
        {
            get;
            set;
        }
    }
    #endregion
}
