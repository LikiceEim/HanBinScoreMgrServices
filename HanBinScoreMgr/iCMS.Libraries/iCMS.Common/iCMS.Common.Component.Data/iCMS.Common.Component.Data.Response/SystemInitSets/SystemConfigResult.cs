/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  SystemConfigResult
 *创建人：  LF
 *创建时间：2016-10-31
 *描述：系统配置
/************************************************************************************/
using System.Collections.Generic;
using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 系统配置
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigResult
    {
        public List<Config> ConfigList { get; set; }
    }
    #endregion
}
