/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Statistics
 * 文件名：  GetDAUMaintainReportResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：查看用户管理的有线传感器的未查看的维修日志返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：查看用户管理的有线传感器的未查看的维修日志返回结果
    /// </summary>
    public class GetWSMaintainReportSimpleInfoResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：维修日志信息返回结果集合
        /// </summary>
        public List<MaintainReportSimpleInfoResult> MaintainReport { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：维修日志总数
        /// </summary>
        public int Total { get; set; }

        public GetWSMaintainReportSimpleInfoResult()
        {
            this.MaintainReport = new List<MaintainReportSimpleInfoResult>();
        }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：维修日志信息返回结果
    /// </summary>
    public class MaintainReportSimpleInfoResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：维修日志主键ID
        /// </summary>
        public int MaintainReportID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：维修日志名称
        /// </summary>
        public string MaintainReportName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：维修日志修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}