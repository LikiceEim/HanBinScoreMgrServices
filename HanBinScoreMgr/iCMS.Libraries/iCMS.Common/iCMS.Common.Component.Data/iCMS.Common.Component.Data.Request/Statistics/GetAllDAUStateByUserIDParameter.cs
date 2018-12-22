/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.Statistics
 * 文件名：  GetAllDAUStateByUserIDParameter
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：查询用户管理的所有采集单元的连接状态参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：查询用户管理的所有采集单元的连接状态参数
    /// </summary>
    public class GetAllDAUStateByUserIDParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：用户ID，-1：返回全部
        /// </summary>
        public int UserID { get; set; }
    }
}