/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumOperationType 
 *创建人：  王颖辉
 *创建时间：2017/10/18 15:49:19 
 *描述：操作类型
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum EnumOperationType
    {
        /// <summary>
        ///下发测量定义
        /// </summary>
        [Description("下发测量定义")]
        Config = 1,
        /// <summary>
        ///升级
        /// </summary>
        [Description("升级")]
        Updategrade = 2,
        /// <summary>
        ///触发式上传
        /// </summary>
        [Description("触发式上传")]
        Trigger = 3,
    }
}
