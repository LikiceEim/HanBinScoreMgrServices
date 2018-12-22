/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  ChangeDevUsedTypeParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：改变设备使用状态
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 改变设备使用状态
    /// <summary>
    /// 改变设备使用状态
    /// </summary>
    public class ChangeDevUsedTypeParameter : BaseRequest
    {
        /// <summary>
        /// 要切换的设备
        /// </summary>
        public int UsedDevId { get; set; }
        /// <summary>
        /// 要切换的备用设备
        /// </summary>
        public int UnUsedDevId { get; set; }
    }
    #endregion
}
