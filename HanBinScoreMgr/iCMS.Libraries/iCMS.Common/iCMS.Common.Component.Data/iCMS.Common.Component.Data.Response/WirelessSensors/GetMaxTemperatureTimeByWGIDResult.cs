/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.WirelessSensors
 * 文件名：  GetMaxTemperatureTimeByWGIDResult
 * 创建人：  张辽阔
 * 创建时间：2017-06-07
 * 描述：根据网关ID获取最大的温度采集时间间隔结果类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.WirelessSensors
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2017-06-07
    /// 创建记录：根据网关ID获取最大的温度采集时间间隔结果类
    /// </summary>
    public class GetMaxTemperatureTimeByWGIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-07
        /// 创建记录：该网关下最大的温度、电池电压采集时间间隔
        /// </summary>
        public int? TemperatureTime { get; set; }
    }
}