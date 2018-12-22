/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessDevicesConfig
 *文件名：  AgentAccessResult
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：返回验证Agent是否可以访问数据信息的参数
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    #region 返回验证Agent是否可以访问数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回验证Agent是否可以访问数据信息的参数
    /// </summary>
    public class AgentAccessResult
    {
        /// <summary>
        /// 1成功，0失败
        /// </summary>
        public string IsAccess { get; set; }
    }
    #endregion
}

