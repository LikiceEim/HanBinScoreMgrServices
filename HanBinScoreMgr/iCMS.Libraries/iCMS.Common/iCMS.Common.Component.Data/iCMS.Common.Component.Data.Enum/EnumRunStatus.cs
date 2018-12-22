/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  AlarmStatus
 *创建人：  LF  
 *创建时间：2016年7月26日10:13:17
 *描述：运行状态枚举
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 设备运行状态
    /// <summary>
    /// 设备运行状态
    /// </summary>
    [Description("设备运行状态")]
    public enum EnumRunStatus
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        [Description("运行")]
        RunNormal = 1,
        /// <summary>
        /// 检修状态
        /// </summary>
        [Description("检修")]
        Checking = 2,
        /// <summary>
        /// 停机状态
        /// </summary>
        [Description("停机")]
        Stop = 3
    }
    #endregion

}
