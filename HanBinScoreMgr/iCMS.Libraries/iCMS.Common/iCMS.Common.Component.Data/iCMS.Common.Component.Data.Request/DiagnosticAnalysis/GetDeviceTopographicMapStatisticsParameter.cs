/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetDeviceTopographicMapStatisticsParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/13 17:15:48 
 *描述：获取当前设备状态及报告统计
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    /// <summary>
    /// 获取当前设备状态及报告统计
    /// </summary>
    public class GetDeviceTopographicMapStatisticsParameter:BaseRequest
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// 1：最近12个月
        /// 0：最近30天
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 天数或者月数
        /// </summary>
        public int Num
        {
            get;
            set;
        }
    }
}
