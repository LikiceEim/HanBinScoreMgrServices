/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  WSUsedStatus
 *创建人：  LF  
 *创建时间：2016年7月26日16:12:49
 *描述：无线传感器使用状态
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 无线传感器使用状态
    /// <summary>
    /// 无线传感器使用状态
    /// </summary>
    public enum EnumWSUsedStatus
    {
        [Description("未使用")]
        Unused = 0,
        [Description("使用")]
        Used = 1
    }
    #endregion

}
