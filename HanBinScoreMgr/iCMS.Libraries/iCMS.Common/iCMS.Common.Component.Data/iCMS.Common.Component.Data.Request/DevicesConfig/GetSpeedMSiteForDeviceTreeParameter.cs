/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  GetSpeedMSiteForDeviceTreeParameter 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：获取转速测点信息参数
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
    /// 创建时间：2018-05-08
    /// 创建记录：获取转速测点信息参数
    /// </summary>
    public class GetSpeedMSiteForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：转速测量位置ID
        /// </summary>
        public int MSiteID { get; set; }
    }
}