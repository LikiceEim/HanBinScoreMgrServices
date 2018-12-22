/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  DeviceNameForMTNodeParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 获取设备名称
    /// <summary>
    /// 获取设备名称
    /// </summary>
    public class DeviceNameForMTNodeParameter : BaseRequest
    {
        public int MonitorTreeID { get; set; }
    }
    #endregion
}
