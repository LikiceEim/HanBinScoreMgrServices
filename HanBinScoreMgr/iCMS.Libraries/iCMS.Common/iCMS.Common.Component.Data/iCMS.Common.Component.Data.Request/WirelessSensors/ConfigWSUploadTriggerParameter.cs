/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  SetWSNUploadTriggerDefineParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：传感器触发式上传
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 传感器触发式上传
    /// <summary>
    /// 传感器触发式上传
    /// </summary>
    public class ConfigWSUploadTriggerParameter : BaseRequest
    {
        /// <summary>
        /// 待设置传感器ID
        /// </summary>
        public string MSiteIDs { set; get; }
        /// <summary>
        /// 触发式上传处理状态
        /// </summary>
        public string TriggerStatus { set; get; }
    }
    #endregion

}
