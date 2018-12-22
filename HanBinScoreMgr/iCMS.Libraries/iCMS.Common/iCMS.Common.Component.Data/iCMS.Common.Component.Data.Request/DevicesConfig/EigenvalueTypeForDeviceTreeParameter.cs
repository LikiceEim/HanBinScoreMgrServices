/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  EigenvalueTypeForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：获取特征值类型数据信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取特征值类型数据信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：获取特征值类型数据信息参数
    /// </summary>
    public class EigenvalueTypeForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int VibratingSignalTypeID { get; set; }
    }
    #endregion
}