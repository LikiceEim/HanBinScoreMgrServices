/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  CloudConfigDataResult
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：云通讯基础数据页面查看接口返回值
/************************************************************************************/
using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 云通讯基础数据页面查看接口返回值
    /// <summary>
    /// 云通讯基础数据页面查看接口返回值
    /// </summary>
    public class CloudConfigDataResult
    {
        /// <summary>
        /// 云平台集合
        /// </summary>
        public List<CloudBaseDataInfo> CloudBaseDataInfo
        {
            get; set;
        }
    }
    #endregion

    #region 平台集合
    /// <summary>
    /// 平台集合
    /// </summary>

    public class CloudBaseDataInfo : CloudConfig
    {

    }
    #endregion
}
