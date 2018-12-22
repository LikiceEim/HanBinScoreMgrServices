/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Statistics
 * 文件名：  GetAllDAUStateByUserIDResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：查询用户管理的所有采集单元的连接状态返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：查询用户管理的所有采集单元的连接状态返回结果
    /// </summary>
    public class GetAllDAUStateByUserIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：网关简单状态信息集合
        /// </summary>
        public List<WGSimpleStateInfo> WGStateList { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：网关简单状态信息
    /// </summary>
    public class WGSimpleStateInfo
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：采集单元是否正在使用
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}