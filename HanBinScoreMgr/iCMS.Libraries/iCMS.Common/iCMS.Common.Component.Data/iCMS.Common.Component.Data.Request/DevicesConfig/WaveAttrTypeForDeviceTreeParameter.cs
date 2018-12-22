/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  WaveAttrTypeForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：获取波形属性(波长，上限频率，下限频率)类型数据信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取波形属性(波长，上限频率，下限频率)类型数据信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：获取波形属性(波长，上限频率，下限频率)类型数据信息参数
    /// </summary>
    public class WaveAttrTypeForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int VibratingSignalTypeID { get; set; }
    }
    #endregion
}