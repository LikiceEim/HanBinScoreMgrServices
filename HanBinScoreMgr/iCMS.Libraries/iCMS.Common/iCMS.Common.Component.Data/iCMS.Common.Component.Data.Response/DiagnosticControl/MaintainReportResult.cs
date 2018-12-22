/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  MaintainReportResult
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
    public class MaintainReportList : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MaintainReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string MaintainReportName { get; set; }

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
        /// 设备维修日志为监测树路径；网关维修日志无；传感器维修日志为挂靠网关
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
        /// 是否已查看	False已查看，true未查看
        /// </summary>
        public bool IsRead { get; set; }


        public bool IsDeleted { get; set; }

        public bool IsTemplate { get; set; }

        /// <summary>
        /// 网关：设备形态类型 1：普通网关2：轻量级网关3：采集单元
        /// 传感器：设备形态类型 1：单板2：三轴3：有线传感器
        /// </summary>
        public int? DevFormType { get; set; }
    }

    /// <summary>
    /// 查看设备诊断报告，获取列表
    /// </summary>
    public class GetMaintainReportResult
    {
        /// <summary>
        /// 总量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 诊断报告列表
        /// </summary>
        public List<MaintainReportList> MaintainReport { get; set; }
    }

    /// <summary>
    /// 查看设备诊断报告，报告详情
    /// </summary>
    public class GetMaintainReportDetailResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MaintainReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string MaintainReportName { get; set; }

        public int DeviceID { get; set; }

        /// <summary>
        /// 维修日志类别
        /// 1：设备报告
        /// 2：无线传感器报告       add by lwj,2018.05.03
        /// 3：网关报告
        /// 4：有线传感器报告
        /// 5：采集单元报告
        /// </summary>
        public int ReportType { get; set; }

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

        ///// <summary>
        ///// 网关：设备形态类型 1：普通网关2：轻量级网关3：采集单元
        ///// 传感器：设备形态类型 1：单板2：三轴3：有线传感器
        ///// </summary>
        //public int? DevFormType { get; set; }
    }

    /// <summary>
    /// 新增维修日志
    /// </summary>
    public class AddMaintainReportResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MaintainReportID { get; set; }
    }

    /// <summary>
    /// 编辑维修日志
    /// </summary>
    public class EditMaintainReportResult
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MaintainReportID { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string MaintainReportName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
    /// </summary>
    public class GetUnreadMRptCountAndMRptByUserIDResult
    {
        public GetUnreadMRptCountAndMRptByUserIDResult()
        {
            TotalMRptCount = 0;
            UnreadMRptCount = 0;
            MaintainReport = new List<MaintainReportList>();
        }

        /// <summary>
        /// 可读全部诊断报告数量
        /// </summary>
        public int TotalMRptCount { get; set; }

        /// <summary>
        /// 未读诊断报告数量
        /// </summary>
        public int UnreadMRptCount { get; set; }

        /// <summary>
        /// 未读诊断报告列表
        /// </summary>
        public List<MaintainReportList> MaintainReport { get; set; }
    }

    /// <summary>
    /// 模板查看
    /// </summary>
    public class ViewReportTemplateResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ReportTemplateID { get; set; }

        /// <summary>
        /// 报告模板名称
        /// </summary>
        public string ReportTemplateName { get; set; }

        /// <summary>
        /// 仅维修日志中返回
        /// 1：设备维修日志
        /// 2：无线传感器维修日志
        /// 3：网关维修日志
        /// 4：有线传感器维修日志     add by lwj
        /// 5：采集单元维修日志      add by lwj
        /// </summary>
        public int ReportType { get; set; }

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
}
