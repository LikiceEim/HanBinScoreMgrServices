/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response
 *文件名：  ConfigResult
 *创建人：  王颖辉
 *创建时间：2016-10-28
 *描述：通用配置返回值
 *===================================================================================
 *修改人：张辽阔
 *修改时间：2016-11-08
 *修改记录：增加“通用配置信息实体类”
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    #region 通用配置返回值

    /// <summary>
    /// 通用配置返回值
    /// </summary>
    public class ConfigListByDeviceIDResult
    {
        public List<ConfigInfo> ConfigInfoList
        {
            get;
            set;
        }
    }

    #endregion

    #region 通用配置信息

    /// <summary>
    /// 添加人：张辽阔
    /// 添加时间：2016-11-08
    /// 添加记录：通用配置信息实体类
    /// </summary>
    public class ConfigInfo : Config
    {
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool IsExistChild { get; set; }
    }

    #endregion
}