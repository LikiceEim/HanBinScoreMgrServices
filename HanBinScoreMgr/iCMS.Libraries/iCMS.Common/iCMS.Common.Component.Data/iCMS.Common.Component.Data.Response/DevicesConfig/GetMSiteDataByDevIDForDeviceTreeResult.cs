/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  GetMSiteDataByDevIDForDeviceTreeResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：获取设备上所有的未挂靠转速的非转速测点返回结果
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
    /// 创建记录：获取设备上所有的未挂靠转速的非转速测点返回结果
    /// </summary>
    public class GetMSiteDataByDevIDForDeviceTreeResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：设备信息对应测量位置信息集合
        /// </summary>
        public List<MeasureInfoResult> MeasureInfoList { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-08
    /// 创建记录：测量位置信息返回结果
    /// </summary>
    public class MeasureInfoResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：测量位置ID
        /// </summary>
        public int WID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：测量位置类型ID
        /// </summary>
        public int MSiteTypeId { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：测量位置名称
        /// </summary>
        public string MStieTypeName { get; set; }
    }
}