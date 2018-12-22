/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetFullMonitorTreeDataByDeviceParameter 
 *创建人：  王颖辉
 *创建时间：2017/9/28 15:48:41 
 *描述：根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
    /// <summary>
    /// 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
    /// </summary>
    public class GetFullMonitorTreeDataByDeviceParameter : BaseRequest
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID
        {
            get;
            set;
        }


        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID
        {
            get;
            set;
        }

    }
    #endregion
}
