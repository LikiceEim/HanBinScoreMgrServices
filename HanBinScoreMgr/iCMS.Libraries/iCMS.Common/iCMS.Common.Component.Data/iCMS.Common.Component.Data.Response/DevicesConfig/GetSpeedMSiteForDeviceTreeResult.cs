/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  GetSpeedMSiteForDeviceTreeResult 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：获取转速测量位置详细信息返回参数
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
    /// 创建记录：获取转速测量位置详细信息返回参数
    /// </summary>
    public class GetSpeedMSiteForDeviceTreeResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：转速测量定义ID
        /// </summary>
        public int SpeedMDFID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：转速测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：转速测量定义关联测点ID
        /// </summary>
        public int RelationMSiteID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：关联测点名称
        /// </summary>
        public string RelatedMSiteName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：传感器id
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：脉冲数
        /// </summary>
        public float PulsNumPerP { get; set; }
    }
}