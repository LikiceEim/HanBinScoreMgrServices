/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  CloudConfigMappingData
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：关系数据页面查看
/************************************************************************************/
using iCMS.Frameworks.Core.DB.Models;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 关系数据页面查看，返回结果
    /// <summary>
    /// 关系数据页面查看，返回结果
    /// </summary>
    public class CloudConfigMappingDataResult
    {
        /// <summary>
        /// 基础数据信息集合
        /// </summary>
        public List<BaseDataInfo> BaseDataInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 平台信息集合
        /// </summary>
        public List<BaseDataInfo> PlatformDataInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 配置数据
        /// </summary>
        public List<BaseDataInfo> ConfigDataInfo
        {
            get;
            set;
        }
    }
    #endregion

    #region 基础数据类
    /// <summary>
    /// 基础数据类
    /// </summary>
    public class BaseDataInfo : CloudConfig
    {

    }
    #endregion
}
