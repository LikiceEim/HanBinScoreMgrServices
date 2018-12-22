/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetWSStatusAndReportStatisticsParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/14 18:49:31 
 *描述：获取当前传感器状态及报告统计
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
    /// 获取当前传感器状态及报告统计
    /// </summary>
    public class GetWSStatusAndReportStatisticsParameter:BaseRequest
    {
        /// <summary>
        /// WSid
        /// </summary>
        public int WSID
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// 0：最近30天
        /// 1：最近12个月
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
