/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnmuReceiveStatus
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：WS操作状态枚举
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    /// <summary>
    /// 接收WS返回状态
    /// </summary>
    public enum EnmuReceiveStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Succeed = 0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Faild,
        /// <summary>
        /// 底层网络不接受
        /// </summary>
        [Description("底层网络不接受")]
        Unaccept,
        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        TimeOut,
    }
}
