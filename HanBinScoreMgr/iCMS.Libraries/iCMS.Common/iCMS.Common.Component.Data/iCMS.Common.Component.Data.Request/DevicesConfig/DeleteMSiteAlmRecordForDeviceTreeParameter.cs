/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  DeleteMSiteAlmRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-02
 *描述：删除测量位置报警配置及报警值信息参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 删除测量位置报警配置及报警值信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：删除测量位置报警配置及报警值信息参数
    /// </summary>
    public class DeleteMSiteAlmRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置报警ID
        /// </summary>
        public int MSiteAlmID { get; set; }

        /// <summary>
        /// 测量位置监测类型ID
        /// </summary>
        public int MSiteAlmType { get; set; }
    }
    #endregion

}