/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  DeteleVibSignalRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：删除振动信号信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 删除振动信号信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：删除振动信号信息参数
    /// </summary>
    public class DeteleVibSignalRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 信号ID
        /// </summary>
        public int SignalID { get; set; }
    }
    #endregion
}