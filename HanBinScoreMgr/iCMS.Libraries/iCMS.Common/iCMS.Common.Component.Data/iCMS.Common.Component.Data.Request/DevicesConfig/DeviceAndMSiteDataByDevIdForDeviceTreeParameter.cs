/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  DeviceAndMSiteDataByDevIdForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：获取设备和测量位置数据信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取设备和测量位置数据信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：获取设备和测量位置数据信息参数
    /// </summary>
    public class DeviceAndMSiteDataByDevIdForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevId { get; set; }


        /// <summary>
        /// 1:定时，2：临时
        /// </summary>
        public int DAQStyle { get; set; }
    }
    #endregion
}