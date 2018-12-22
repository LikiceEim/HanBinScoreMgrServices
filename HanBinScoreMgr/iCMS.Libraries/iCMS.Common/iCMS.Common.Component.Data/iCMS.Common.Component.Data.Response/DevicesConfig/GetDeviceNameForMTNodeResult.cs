/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetDeviceNameForMTNodeResult 
 *创建人：  王颖辉
 *创建时间：2017/9/27 18:19:10 
 *描述：判断监测树节点是否挂靠设备，返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{

    #region 判断监测树节点是否挂靠设备，返回结果
    /// <summary>
    /// 判断监测树节点是否挂靠设备，返回结果
    /// </summary>
    public class GetDeviceNameForMTNodeResult
    {
        /// <summary>
        /// 是否存在设备子节点
        /// </summary>
        public bool IsExistDeviceChild
        {
            get;
            set;
        }
    }
    #endregion
}
