/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum 
 *文件名：  EnumDataType 
 *创建人：  王颖辉
 *创建时间：2018/1/17 16:37:26
 *描述：查询类型
 *
 *修改人：
 *修改时间：
 *描述：
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
    /// 查询类型
    /// </summary>
    public enum EnumDataType
    {
        /// <summary>
        /// 最近一次
        /// </summary>
        [Description("最近一次")]
        Last =1,
        /// <summary>
        /// 最近一天
        /// </summary>
        [Description("最近一天")]
        LastOneDay = 2,
        /// <summary>
        /// 最近一周
        /// </summary>
        [Description("最近一周")]
        LastOneWeek = 3,
        /// <summary>
        /// 最近一月
        /// </summary>
        [Description("最近一月")]
        LastOneMonth = 4,
        /// <summary>
        /// 最近一年
        /// </summary>
        [Description("最近一年")]
        LastOneYear = 5,
        /// <summary>
        /// 最近一次
        /// </summary>
        [Description("自定义")]
        Custom = 6,

    }
}
