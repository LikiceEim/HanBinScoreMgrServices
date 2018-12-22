/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  EditDeviceRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：编辑设备信息参数
/************************************************************************************/
using System.Collections.Generic;
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 编辑设备信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：编辑设备信息参数
    /// </summary>
    public class EditDeviceRecordForDeviceTreeParameter : AddDeviceRecordForDeviceTreeParameter
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 有线网关集合
        /// </summary>
        public string WiredWGID { get; set; }

        /// <summary>
        /// 无线网关集合
        /// </summary>
        public string WireLessWGID { get; set; }
    }
    #endregion
}