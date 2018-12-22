/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetMainStandbyDeviceListByDeviceIdParameter 
 *创建人：  王颖辉
 *创建时间：2017/11/6 10:39:18 
 *描述：主备切换时获取设备列表信息
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
    /// 主备切换时获取设备列表信息
    /// </summary>
    public class GetMainStandbyDeviceListByDeviceIdParameter:BaseRequest
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
        /// 用户ID（-1为icmsadministrator）
        /// </summary>
        public int UserId
        {
            get;
            set;
        }
    }
}
