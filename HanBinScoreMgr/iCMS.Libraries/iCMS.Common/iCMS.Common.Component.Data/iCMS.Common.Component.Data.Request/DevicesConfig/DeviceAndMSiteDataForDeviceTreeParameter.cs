/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  DeviceAndMSiteDataForDeviceTreeParameter
 * 创建人：  张辽阔
 * 创建时间：2016-11-01
 * 描述：获取设备和测量位置数据信息参数
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
    public class DeviceAndMSiteDataForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order
        {
            get;
            set;
        }
    }
    #endregion
}