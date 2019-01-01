/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  WGLinkStatus
 *创建人：  LF  
 *创建时间：2016年7月26日16:04:33
 *描述：无线网关枚举
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 无线网关枚举
    /// <summary>
    /// 无线网关枚举
    /// </summary>
    public enum EnumWGLinkStatus
    {
        [Description("断开")]
        Disconnect = 0,
        [Description("连接")]
        Connect = 1
    }
    #endregion

}
