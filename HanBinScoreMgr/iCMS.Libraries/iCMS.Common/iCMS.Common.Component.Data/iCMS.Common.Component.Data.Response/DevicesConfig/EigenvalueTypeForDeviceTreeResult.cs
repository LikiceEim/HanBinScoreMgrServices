/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  EigenvalueTypeForDeviceTreeResult
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：返回特征值类型数据信息的参数
/************************************************************************************/
using System.Collections.Generic;
using iCMS.Common.Component.Data.Response.Common;
namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回特征值类型数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回特征值类型数据信息的参数
    /// </summary>
    public class EigenvalueTypeForDeviceTreeResult
    {
        /// <summary>
        /// 返回结果集合
        /// </summary>
        public List<CommonInfo> CommonInfos { get; set; }
    }
    #endregion
}