/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  ConfigListByDeviceIDParameter
 *创建人：  王颖辉
 *创建时间：2016-10-28
 *描述：通过设备Id,获取形貌图配置下的所有位置信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    #region 通过设备Id,获取形貌图配置下的所有位置信息
    /// <summary>
    /// 通过设备Id,获取形貌图配置下的所有位置信息
    /// </summary>
    public class ConfigListByDeviceIDParameter : BaseRequest
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }
    }
    #endregion
}