/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudDataOperation
 *创建人：  
 *创建时间：
 *描述：工厂枚举
 *
 *修改人：张辽阔
 *修改时间：2016-12-16
 *修改内容：迁移至该命名空间下
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    #region 云同步数据操作类型
    /// <summary>
    /// 云同步数据操作类型
    /// </summary>
    public enum EnumCloudDataOperation
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 0,
        /// <summary>
        /// JiaXun
        /// </summary>
        [Description("修改")]
        Update = 1,
        /// <summary>
        /// JiaXun
        /// </summary>
        [Description("删除")]
        Delete = 2,

        [Description("同步")]
        Sync = 3
    }
    #endregion
}