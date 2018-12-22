/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessDevicesConfig 
 *文件名：  GetWGDataByUserIDParameter 
 *创建人：  王颖辉
 *创建时间：2017/12/5 16:54:18
 *描述：获取网关数据
 *
 *修改人：
 *修改时间：
 *描述：
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.WirelessDevicesConfig
{
    /// <summary>
    /// 获取网关数据
    /// </summary>
    public class GetWGDataByUserIDParameter:BaseRequest
    {
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
