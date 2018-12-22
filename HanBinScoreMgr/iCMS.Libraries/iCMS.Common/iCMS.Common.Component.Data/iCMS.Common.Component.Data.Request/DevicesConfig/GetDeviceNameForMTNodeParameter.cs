/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetDeviceNameForMTNodeParameter 
 *创建人：  王颖辉
 *创建时间：2017/9/27 18:16:32 
 *描述：判断监测树节点是否挂靠设备
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 判断监测树节点是否挂靠设备
    /// <summary>
    /// 判断监测树节点是否挂靠设备
    /// </summary>
    public class GetDeviceNameForMTNodeParameter
    {
        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }
    }
    #endregion

    public class GetDeviceSelectListParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
}
