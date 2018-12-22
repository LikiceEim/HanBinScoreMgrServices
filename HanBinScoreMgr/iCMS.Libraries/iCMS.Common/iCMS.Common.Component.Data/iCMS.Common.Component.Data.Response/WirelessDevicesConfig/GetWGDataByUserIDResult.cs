/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessDevicesConfig 
 *文件名：  GetWGDataByUserIDResult 
 *创建人：  王颖辉
 *创建时间：2017/12/5 16:55:21
 *描述：获取无线网关数据接口
 *
 *修改人：
 *修改时间：
 *描述：
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    /// <summary>
    /// 获取无线网关数据接口
    /// </summary>
    public class GetWGDataByUserIDResult
    {
        public List<WGInfoForGetWGDataByUserID> WGInfo
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 返回值
    /// </summary>
    public class WGInfoForGetWGDataByUserID
    {
        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName
        {
            get;
            set;
        }

        /// <summary>
        /// 网关id
        /// </summary>
        public int WGID
        {
            get;
            set;
        }
    }
}
