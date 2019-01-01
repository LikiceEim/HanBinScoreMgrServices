/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumOperationStatus
 *创建人：  LF  
 *创建时间：2016年8月1日09:48:04
 *描述：操作状态
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 操作状态
    /// <summary>
    /// 操作状态
    /// </summary>
    public enum EnumOperationStatus
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("成功")]
        Success = 1,
        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("失败")]
        Faild = 2,
        /// <summary>
        /// 操作进行中
        /// </summary>
        [Description("进行中")]
        Operating = 3,
        /// <summary>
        /// WS返回超时
        /// </summary>
        [Description("超时")]
        WsTimeOut = 4,
        /// <summary>
        /// WS返回网络忙
        /// </summary>
        [Description("网络忙，底层网络不接受")]
        NetworkBusy = 5,
        /// <summary>
        /// WS无响应超时
        /// </summary>
        [Description("server自检测超时")]
        NoResponseTimeOut = 6
    }
    #endregion
}
