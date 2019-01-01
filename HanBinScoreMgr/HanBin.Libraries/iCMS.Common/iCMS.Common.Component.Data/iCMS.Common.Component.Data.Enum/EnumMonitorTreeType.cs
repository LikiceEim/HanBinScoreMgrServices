/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumMonitorTreeType
 *创建人：  王颖辉  
 *创建时间：2016年9月12日
 *描述：监测树类型
 *
 *修改人：张辽阔
 *修改时间：2016-12-29
 *修改内容：修改文件名
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    #region 监测树类型
    /// <summary>
    /// 监测树类型
    /// </summary>
    public enum EnumMonitorTreeType
    {
        /// <summary>
        /// 集团
        /// </summary>
        [Description("集团")]
        Group = 1,

        /// <summary>
        /// 工厂
        /// </summary>
        [Description("工厂")]
        Factory = 2,

        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        Workshop = 3,

        /// <summary>
        /// 机组
        /// </summary>
        [Description("机组")]
        Crew = 4,
    }
    #endregion

    #region 监测树类型ID
    /// <summary>
    /// 监测树类型ID
    /// </summary>
    public enum EnumMonitorTreeTypeID
    {
        /// <summary>
        /// 集团
        /// </summary>
        [Description("集团")]
        Group = 1004,

        /// <summary>
        /// 工厂
        /// </summary>
        [Description("工厂")]
        Factory = 1005,

        /// <summary>
        /// 车间
        /// </summary>
        [Description("车间")]
        Workshop = 1006,

        /// <summary>
        /// 机组
        /// </summary>
        [Description("机组")]
        Crew = 1007,
    }
    #endregion
}