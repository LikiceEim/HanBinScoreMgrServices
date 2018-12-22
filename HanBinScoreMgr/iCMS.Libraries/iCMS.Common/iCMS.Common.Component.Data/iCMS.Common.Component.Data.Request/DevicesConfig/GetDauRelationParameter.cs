/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  GetDauRelationParameter
 * 创建人：  张辽阔
 * 创建时间：2018-05-17
 * 描述：获取采集单元信息参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-17
    /// 创建记录：获取采集单元信息参数
    /// </summary>
    public class GetDauRelationParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-17
        /// 创建记录：Agent地址
        /// </summary>
        public string AgentHostIPAddress { get; set; }
    }
}