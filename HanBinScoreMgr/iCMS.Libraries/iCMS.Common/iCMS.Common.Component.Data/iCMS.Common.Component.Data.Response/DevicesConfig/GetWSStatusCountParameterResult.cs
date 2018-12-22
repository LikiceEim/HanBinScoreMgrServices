/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetWSStatusCountParameterResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 14:48:37 
 *描述：获取用户所管理传感器连接状态统计
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取用户所管理传感器连接状态统计
    /// </summary>
    public class GetWSStatusCountParameterResult
    {

        /// <summary>
        /// WS总数
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatusCount
        {
            get;
            set;
        }


        /// <summary>
        /// 断开状态
        /// </summary>
        public int UnLinkCount
        {
            get;
            set;
        }

        /// <summary>
        /// 未采集数量
        /// </summary>
        public int UnCollectedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 正常总数
        /// </summary>
        public int NormalDeviceCount
        {
            get;
            set;
        }

        /// <summary>
        /// 中级报警总数
        /// </summary>
        public int AlertDeviceCount
        {
            get;
            set;
        }

        /// <summary>
        /// 高级报警总数
        /// </summary>
        public int WarnDeviceCount
        {
            get;
            set;
        }

        /// <summary>
        /// 网关列表
        /// </summary>
        public List<MonitorInfo> MonitorStats
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 网关信息
    /// </summary>

    public class MonitorInfo
    {
        /// <summary>
        /// 网关Id
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 网关数量
        /// </summary>
        public int Count
        {
            set;
            get;
        }
    }
}
