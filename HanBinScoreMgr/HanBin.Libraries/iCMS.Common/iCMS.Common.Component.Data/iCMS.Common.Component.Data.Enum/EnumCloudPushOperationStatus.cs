/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudPushOperationStatus
 *创建人：  张辽阔
 *创建时间：2016-12-07
 *描述：云推送数据推送表操作状态枚举
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
    /// 创建记录：云推送数据推送表操作状态枚举
    /// </summary>
    public enum EnumCloudPushOperationStatus
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,
        
        /// <summary>
        /// 云平台数据周期同步操作，ADDED BY QXM,2017/02/08
        /// </summary>
        Sync = 4
    }
}