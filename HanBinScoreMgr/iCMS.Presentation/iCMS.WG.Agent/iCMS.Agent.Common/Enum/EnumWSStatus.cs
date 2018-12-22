/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  WSStatus
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：WS状态类型枚举
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
    [Description("WS状态")]
    public enum EnumWSStatus
    {

        /// <summary>
        /// 已更新
        /// </summary>
        [Description("已更新")]
        WSOff = 0,
        /// <summary>
        /// WS连接
        /// </summary>
        [Description("WS连接")]
        WSOn = 1,

      

    }
    [Description("WG状态")]
    public enum WGStatus
    {
      

        /// <summary>
        /// WG断开
        /// </summary>
        [Description("WG断开")]
        WGOff = 0,
        /// <summary>
        /// WG已连接
        /// </summary>
        [Description("WG已连接")]
        WGOn = 1,
    }
}
