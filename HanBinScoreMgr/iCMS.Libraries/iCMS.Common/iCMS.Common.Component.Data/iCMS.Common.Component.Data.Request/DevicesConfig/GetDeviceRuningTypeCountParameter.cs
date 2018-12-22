/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetDeviceRuningTypeCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/12 11:24:24 
 *描述：获取不同设备统计信息
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
    /// 获取不同设备统计信息
    /// </summary>
    public class GetDeviceRuningTypeCountParameter : BaseRequest
    {

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

    }
}
