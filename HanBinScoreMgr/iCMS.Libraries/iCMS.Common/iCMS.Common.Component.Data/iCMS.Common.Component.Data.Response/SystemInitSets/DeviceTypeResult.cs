/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  DeviceTypeResult
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：返回设备类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 返回设备类型数据信息的参数
    /// <summary>
    /// 返回设备类型数据信息的参数
    /// </summary>
    public class DeviceTypeResult
    {
        public List<DeviceTypeData> DeviceTypeList { get; set; }

        public DeviceTypeResult()
        {
            DeviceTypeList = new List<DeviceTypeData>();
        }
    }
    #endregion

    #region 设备类型信息
    /// <summary>
    /// 设备类型信息
    /// </summary>
    public class DeviceTypeData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string AddDate { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
        /// <summary>
        /// 对应设备类型下是否存在测点类型数据
        /// </summary>
        public bool IsExistChild { get; set; }
    }
    #endregion
}
