/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  QueryDevWarningDataParameter
 *创建人：  LF
 *创建时间：2016-11-1
 *描述：报警提醒请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    #region 查询设备报警数据
    /// <summary>
    /// 查询设备报警数据
    /// </summary>
    public class QueryDevWarningDataParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public int DevAlmStat { get; set; }
        /// <summary>
        /// 时间范围起始时间
        /// </summary>
        public string BDate { get; set; }
        /// <summary>
        /// 时间范围结束时间
        /// </summary>
        public string EDate { get; set; }
        /// <summary>
        /// 用户确认报警记录
        /// </summary>
        public string DevIDLastAlarmTime { get; set; }
        /// <summary>
        /// 排序名
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式，desc/asc
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 页数，从1开始,若为-1返回所有的报警记录
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页面行数，从1开始
        /// </summary>
        public int PageSize { get; set; }

    }
    #endregion
}
