/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  AddSpeedMSiteForDeviceTreeParameter 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：添加转速测量位置参数
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
    /// 创建记录：添加转速测量位置参数
    /// </summary>
    public class AddSpeedMSiteForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：测点类型
        /// </summary>
        public int MSiteTypeId { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：转速测量定义关联测点ID
        /// </summary>
        public int RelationMSiteID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：传感器id
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：脉冲数
        /// </summary>
        public float PulsNumPerP { get; set; }
    }
}