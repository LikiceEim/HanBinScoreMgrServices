/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  UpdateWSFirmwareParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：升级包传感器
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 升级包传感器
    /// <summary>
    /// 升级包传感器
    /// </summary>
    public class UpdateWSFirmwareParameter : BaseRequest
    {
        /// <summary>
        /// 传感器ID列表，以“,”分隔
        /// </summary>
        public string WSIDs { get; set; }
        /// <summary>
        /// 升级数据网络地址
        /// </summary>
        public string Data { get; set; }
    }
    #endregion
}
