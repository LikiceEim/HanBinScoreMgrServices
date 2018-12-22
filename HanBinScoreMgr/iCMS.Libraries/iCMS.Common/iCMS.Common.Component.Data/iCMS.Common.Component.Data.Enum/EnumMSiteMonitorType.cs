/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumMSiteMonitorType
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：请求基类
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 测点类型
    /// <summary>
    /// 测点类型
    /// </summary>
    public enum EnumMSiteMonitorType
    {

        [Description("设备温度")]
        TempeDevice = 1,
        [Description("电池电压")]
        VoltageSet = 2,
        [Description("传感器温度")]
        TempeWS = 3,
        [Description("振动信号")]
        SignalAlm = 4
    }
    #endregion
}
