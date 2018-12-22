/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  GetDeviceRuningTypeCountResult 
 * 创建人：  王颖辉
 * 创建时间：2017/10/12 11:34:53 
 * 描述：获取不同设备统计信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取不同设备统计信息
    /// </summary>
    public class GetDeviceRuningTypeCountResult
    {
        /// <summary>
        /// 运行总数 
        /// </summary>
        public int RunningDeviceCount
        {
            get;
            set;
        }

        /// <summary>
        /// 停机总数 
        /// </summary>
        public int StoppedDeviceCount
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
        /// 未采集数量 
        /// </summary>
        public int UnCollected
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
        /// 设备总数 
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 无线设备 
        /// </summary>
        public int WirelessDevice
        {
            get;
            set;
        }

        /// <summary>
        /// 有线设备 
        /// </summary>
        public int WireDevice
        {
            get;
            set;
        }
    }
}