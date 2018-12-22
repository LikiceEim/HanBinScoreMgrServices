/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  SignalAlmDataForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：获取振动信号报警配置数据信息参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取振动信号报警配置数据信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：获取振动信号报警配置数据信息参数
    /// </summary>
    public class SignalAlmDataForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int SignalID { get; set; }
    }
    #endregion
}