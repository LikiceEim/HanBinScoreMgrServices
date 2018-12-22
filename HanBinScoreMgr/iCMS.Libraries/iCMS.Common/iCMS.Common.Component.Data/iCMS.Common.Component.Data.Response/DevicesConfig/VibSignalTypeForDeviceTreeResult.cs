/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  VibSignalTypeForDeviceTreeResult
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：返回振动信号类型数据信息的参数
/************************************************************************************/

using iCMS.Common.Component.Data.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回振动信号类型数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回振动信号类型数据信息的参数
    /// </summary>
    public class VibSignalTypeForDeviceTreeResult
    {
        /// <summary>
        /// 返回结果集合
        /// </summary>
        public List<CommonInfo> CommonInfos { get; set; }
    }
    #endregion
}