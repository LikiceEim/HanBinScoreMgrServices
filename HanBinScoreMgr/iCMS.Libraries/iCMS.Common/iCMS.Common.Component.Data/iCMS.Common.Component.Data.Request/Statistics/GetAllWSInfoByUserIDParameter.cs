/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.Statistics
 * 文件名：  GetAllWSInfoByUserIDParameter
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：查询用户管理的所有传感器的详细信息参数
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
    /// 创建记录：查询用户管理的所有传感器的详细信息参数
    /// </summary>
    public class GetAllWSInfoByUserIDParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：用户ID，-1：返回全部
        /// </summary>
        public int UserID { get; set; }
    }
}