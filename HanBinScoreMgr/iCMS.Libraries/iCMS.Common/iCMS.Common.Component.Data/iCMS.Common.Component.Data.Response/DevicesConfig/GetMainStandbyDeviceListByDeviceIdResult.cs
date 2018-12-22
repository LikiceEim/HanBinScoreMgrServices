/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetMainStandbyDeviceListByDeviceIdResult 
 *创建人：  王颖辉
 *创建时间：2017/11/6 10:40:50 
 *描述：主备切换时获取设备列表信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    ///主备切换时获取设备列表信息
    /// </summary>
    public class GetMainStandbyDeviceListByDeviceIdResult
    {

        /// <summary>
        /// 设备列表
        /// </summary>
        public List<DeviceListForDeviceChange> DeviceList
        {
            get;
            set;
        }

    }
    /// <summary>
    /// 设备列表
    /// </summary>
    public class DeviceListForDeviceChange
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get;
            set;
        }
    }
}
