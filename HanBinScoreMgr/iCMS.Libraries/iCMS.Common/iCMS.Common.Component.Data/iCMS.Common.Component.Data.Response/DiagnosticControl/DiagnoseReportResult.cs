/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  DiagnoseReportResult
 *创建人：  王龙杰
 *创建时间：2017-09-28
 *描述：诊断报告返回值类
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    /// <summary>
    /// 诊断报告列表通用类
    /// </summary>
    public class DiagnoseReportList : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int DiagnoseReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string DiagnoseReportName { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备对应监测树ID
        /// </summary>
        public int DeviceMonitorTreeID { get; set; }

        /// <summary>
        /// 设备所属位置
        /// </summary>
        public string MonitorTreeRoute { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int AddUserID { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateUserID { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }


        public bool IsDeleted { get; set; }

        public bool IsTemplate { get; set; }

    }

    /// <summary>
    /// 查看设备诊断报告，获取列表
    /// </summary>
    public class GetDiagnoseReportResult
    {
        /// <summary>
        /// 总量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 诊断报告列表
        /// </summary>
        public List<DiagnoseReportList> DiagnoseReport { get; set; }
    }

    /// <summary>
    /// 查看设备诊断报告，报告详情
    /// </summary>
    public class GetDiagnoseReportDetailResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int DiagnoseReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string DiagnoseReportName { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int AddUserID { get; set; }
        public string AddUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateUserID { get; set; }
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 报告内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 编辑设备诊断报告
    /// </summary>
    public class AddDiagnoseReportResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int DiagnoseReportID { get; set; }
    }

    /// <summary>
    /// 编辑设备诊断报告
    /// </summary>
    public class EditDiagnoseReportResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int DiagnoseReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string DiagnoseReportName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
    /// </summary>
    public class GetUnreadDRptCountAndDRptByUserIDResult
    {
        public GetUnreadDRptCountAndDRptByUserIDResult()
        {
            TotalDRptCount = 0;
            UnreadDRptCount = 0;
            DiagnoseReport = new List<DiagnoseReportList>();
        }

        /// <summary>
        /// 可读全部诊断报告数量
        /// </summary>
        public int TotalDRptCount { get; set; }

        /// <summary>
        /// 未读诊断报告数量
        /// </summary>
        public int UnreadDRptCount { get; set; }

        /// <summary>
        /// 未读诊断报告列表
        /// </summary>
        public List<DiagnoseReportList> DiagnoseReport { get; set; }
    }
}
