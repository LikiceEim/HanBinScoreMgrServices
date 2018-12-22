/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.Statistics
 * 文件名：  GetTopThreeDAUInfoByUserIDParameter
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：通过用户id获取关联的TOP3采集单元下的传感器类型个数参数
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
    /// 创建记录：通过用户id获取关联的传感器类型个数参数
    /// </summary>
    public class GetTopThreeDAUInfoByUserIDParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：用户id
        /// </summary>
        public int UserID { get; set; }
    }
}