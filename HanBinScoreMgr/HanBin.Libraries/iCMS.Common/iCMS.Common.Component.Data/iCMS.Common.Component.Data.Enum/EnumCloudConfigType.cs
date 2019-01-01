/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudConfigType
 *创建人：  张辽阔
 *创建时间：2016-12-07
 *描述：云推送配置表数据类型枚举
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
    /// 创建记录：云推送配置表数据类型枚举
    /// </summary>
    public enum EnumCloudConfigType
    {
        /// <summary>
        /// 基础数据
        /// </summary>
        BasicData = 1,

        /// <summary>
        /// 云平台
        /// </summary>
        CloudPlatform = 2,

        /// <summary>
        /// 配置数据
        /// </summary>
        ConfigData = 3,

        /// <summary>
        /// 日志是否记录
        /// </summary>
        IsLog = 4,

        /// <summary>
        /// 数据推送量
        /// </summary>
        DataPush = 5,
    }
}