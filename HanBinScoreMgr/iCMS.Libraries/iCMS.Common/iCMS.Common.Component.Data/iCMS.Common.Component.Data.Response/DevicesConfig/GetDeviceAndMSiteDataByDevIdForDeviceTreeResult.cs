/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetDeviceAndMSiteDataByDevIdForDeviceTreeResult 
 *创建人：  王颖辉
 *创建时间：2017/10/19 14:11:49 
 *描述：请求基类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    public class GetDeviceAndMSiteDataByDevIdForDeviceTreeResult
    {
        /// <summary>
        /// 设备记录总数
        /// </summary>
        public int Total { set; get; }

        /// <summary>
        /// 设备信息集合
        /// </summary>
        public List<DeviceInfo> DeviceInfoList { set; get; }

        /// <summary>
        /// 设备信息对应测量位置信息集合
        /// </summary>
        public List<MeasureInfo> MeasureInfoList { set; get; }
    }
}
