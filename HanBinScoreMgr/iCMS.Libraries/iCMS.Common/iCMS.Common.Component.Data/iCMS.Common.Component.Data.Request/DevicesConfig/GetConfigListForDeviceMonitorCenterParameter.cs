/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetConfigListForDeviceMonitorCenterParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/13 16:32:40 
 *描述：请求基类
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 系统配置信息通用查询接口
    /// </summary>
    public class GetConfigListForDeviceMonitorCenterParameter : BaseRequest
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型code
        /// </summary>
        public string DeviceTypeCode
        {
            get;
            set;
        }
    }
}
