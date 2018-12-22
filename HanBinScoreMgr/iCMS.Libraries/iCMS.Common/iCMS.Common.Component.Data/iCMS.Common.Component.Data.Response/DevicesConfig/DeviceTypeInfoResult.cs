/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  DeviceTypeInfoResult
 *创建人：  张辽阔
 *创建时间：2016-10-31
 *描述：返回设备类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;
using iCMS.Common.Component.Data.Response.Common;
namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回设备类型数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：返回设备类型数据信息的参数
    /// </summary>
    public class DeviceTypeInfoResult
    {
        /// <summary>
        /// 返回结果集合
        /// </summary>
        public List<CommonInfo> CommonInfos { get; set; }
    }
    #endregion

    #region 获取设备下拉列表
    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-11-10
    /// 创建记录：获取设备下拉列表
    /// </summary>
    public class GetDeviceSelectListResult
    {
        public List<DeviceSelect> DeviceSelectList { get; set; }
    }

    public class DeviceSelect
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
    }
    #endregion

}