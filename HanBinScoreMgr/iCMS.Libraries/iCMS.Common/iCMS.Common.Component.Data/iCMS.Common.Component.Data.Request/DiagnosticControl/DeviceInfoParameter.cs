/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  DeviceInfoParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：获取设备信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 获取设备信息
    /// <summary>
    /// 获取设备信息
    /// </summary>
    public class DeviceInfoParameter: BaseRequest
    {
        public int DevID { get; set; }
        public int MSiteID { get; set; }
        public string SamplingDate { get; set; }
        public string ChartID { get; set; }
    }
    #endregion
}
