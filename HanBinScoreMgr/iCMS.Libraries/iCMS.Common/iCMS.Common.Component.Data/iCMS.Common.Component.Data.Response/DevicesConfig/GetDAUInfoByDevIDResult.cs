/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  GetDAUInfoByDevIDResult 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：通过设备ID读取设备上的采集单元返回参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-08
    /// 创建记录：通过设备ID读取设备上的采集单元返回参数
    /// </summary>
    public class GetDAUInfoByDevIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：采集单元信息
        /// </summary>
        public List<WGSimpleInfoResult> WGInfo { get; set; }
    }
}