/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  MSiteAlmRecordForDeviceTreeParameter
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：添加测量位置报警配置及报警值信息参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{

    #region 添加测量位置报警配置及报警值信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：添加测量位置报警配置及报警值信息参数
    /// </summary>
    public class AddMSiteAlmRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int MSiteAlmID { set; get; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public int MSDType { set; get; }

        /// <summary>
        /// 高报阈值
        /// </summary>
        public string WarnValue { set; get; }

        /// <summary>
        /// 高高报阈值
        /// </summary>
        public string AlmValue { set; get; }
    }
    #endregion
}