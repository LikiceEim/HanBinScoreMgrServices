/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  CopyDevInfoParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：请求基类
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 复制设备信息
    /// <summary>
    /// 复制设备信息
    /// </summary>
    public class CopyDevInfoParameter : BaseRequest
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int SourceDevId { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int TargetDevId { get; set; }
    }
    #endregion
}
