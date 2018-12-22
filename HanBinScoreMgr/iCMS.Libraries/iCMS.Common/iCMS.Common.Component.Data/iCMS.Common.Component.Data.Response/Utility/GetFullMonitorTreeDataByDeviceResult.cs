/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Utility
 *文件名：  GetFullMonitorTreeDataByDeviceResult 
 *创建人：  王颖辉
 *创建时间：2017/9/28 15:50:49 
 *描述：根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
/************************************************************************************/

using iCMS.Common.Component.Data.Response.DevicesConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Utility
{
    /// <summary>
    /// 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
    /// </summary>
    public class GetFullMonitorTreeDataByDeviceResult
    {

        /// <summary>
        /// 监测树信息
        /// </summary>
        public List<MTInfoWithType> MTInfoWithTypeList
        {
            get;
            set;
        }

    }


}
