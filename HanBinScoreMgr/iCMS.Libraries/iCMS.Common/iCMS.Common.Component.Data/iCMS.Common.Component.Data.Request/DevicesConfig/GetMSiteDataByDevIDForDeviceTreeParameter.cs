/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  GetMSiteDataByDevIDForDeviceTreeParameter
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：获取设备上所有的未挂靠转速的非转速测点参数
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
    /// 创建记录：获取设备上所有的未挂靠转速的非转速测点参数
    /// </summary>
    public class GetMSiteDataByDevIDForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：设备ID
        /// </summary>
        public int DeviceID { get; set; }
    }
}