/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  MSiteAlmAndSignalDataForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：获取测量位置报警和振动信号数据信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取测量位置报警和振动信号数据信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：获取测量位置报警和振动信号数据信息参数
    /// </summary>
    public class MSiteAlmAndSignalDataForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }
    }
    #endregion
}