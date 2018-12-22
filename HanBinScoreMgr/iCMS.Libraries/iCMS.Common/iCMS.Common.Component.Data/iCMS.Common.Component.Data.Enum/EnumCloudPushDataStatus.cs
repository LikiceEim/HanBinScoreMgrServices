/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudPushDataStatus
 *创建人：  张辽阔
 *创建时间：2016-12-07
 *描述：云推送数据推送表数据状态枚举
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-07
    /// 创建记录：云推送数据推送表数据状态枚举
    /// </summary>
    public enum EnumCloudPushDataStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3,
    }
}