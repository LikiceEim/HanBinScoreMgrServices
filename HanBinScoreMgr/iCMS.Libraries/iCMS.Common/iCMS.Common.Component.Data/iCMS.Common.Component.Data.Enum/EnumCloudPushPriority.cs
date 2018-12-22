/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudPushOperationStatus
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：云推送数据推送表优先级枚举
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
    /// 创建时间：2016-12-08
    /// 创建记录：云推送数据推送表优先级枚举
    /// </summary>
    public enum EnumCloudPushPriority
    {
        /// <summary>
        /// 最优先
        /// </summary>
        First = 1,

        /// <summary>
        /// 中等
        /// </summary>
        Center = 2,

        /// <summary>
        /// 普通
        /// </summary>
        Normal = 3,
    }
}