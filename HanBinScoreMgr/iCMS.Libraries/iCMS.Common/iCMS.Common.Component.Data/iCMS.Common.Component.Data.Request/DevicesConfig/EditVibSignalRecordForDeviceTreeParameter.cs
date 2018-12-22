/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  EditVibSignalRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：编辑振动信号信息参数
/************************************************************************************/
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 编辑振动信号信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：编辑振动信号信息参数
    /// </summary>
    public class EditVibSignalRecordForDeviceTreeParameter : AddVibSignalRecordForDeviceTreeParameter
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int SignalID { get; set; }
    }
    #endregion
}