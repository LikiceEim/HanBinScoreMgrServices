/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Base
 *文件名：  CopyAllMSParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：复制所有位置信息
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 复制所有位置信息
    /// <summary>
    /// 复制所有位置信息
    /// </summary>
    public class CopyAllMSParameter : BaseRequest
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int TargetDevId { get; set; }
        /// <summary>
        /// 源ms与新WS的匹配字符串（1#4，4#）
        /// </summary>
        public string MSAndWSStr { get; set; }
    }
    #endregion
}
