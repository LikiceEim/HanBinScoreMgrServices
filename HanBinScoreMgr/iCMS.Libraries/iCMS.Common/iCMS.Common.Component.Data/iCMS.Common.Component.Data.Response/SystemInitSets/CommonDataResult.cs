/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 * 文件名：  CommonDataResult
 * 创建人：  张辽阔
 * 创建时间：2016-10-31
 * 描述：返回设备类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 返回设备类型数据信息的参数

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：返回设备类型数据信息的参数
    /// </summary>
    public class CommonDataResult
    {
        /// <summary>
        /// 返回结果集合
        /// </summary>
        public List<CommonInfo> CommonInfoList { get; set; }

        public CommonDataResult()
        {
            CommonInfoList = new List<CommonInfo>();
        }
    }

    #endregion
}