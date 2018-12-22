/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 * 文件名：  GetMaxTemperatureTimeByWGIDParameter
 * 创建人：  张辽阔  
 * 创建时间：2017-06-07
 * 描述：根据网关ID获取最大的温度采集时间间隔请求参数类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2017-06-07
    /// 创建记录：根据网关ID获取最大的温度采集时间间隔请求参数类
    /// </summary>
    public class GetMaxTemperatureTimeByWGIDParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-07
        /// 创建记录：网关ID
        /// </summary>
        public int WGID { get; set; }
    }
}