/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  CheckWSVersionParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：验证升级包版本与传感器当前版本是否一致
 *=====================================================================**/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 验证升级包版本与传感器当前版本是否一致
    /// <summary>
    /// 验证升级包版本与传感器当前版本是否一致
    /// </summary>
    public class CheckWSVersionParameter : BaseRequest
    {
        /// <summary>
        /// c传感器ID
        /// </summary>
        public string WSIDs { set; get; }
        /// <summary>
        /// 升级文件所在网络位置
        /// </summary>
        public string FileUrl { set; get; }
    }
    #endregion
}
