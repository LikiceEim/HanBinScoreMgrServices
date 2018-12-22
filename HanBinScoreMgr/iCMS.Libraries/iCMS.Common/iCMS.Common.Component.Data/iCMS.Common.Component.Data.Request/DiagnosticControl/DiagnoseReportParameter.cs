/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  DiagnoseReportResult
 *创建人：  王龙杰
 *创建时间：2017-09-28
 *描述：诊断报告参数类
/************************************************************************************/


using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    /// <summary>
    /// 查看设备诊断报告，获取列表
    /// </summary>
    public class GetDiagnoseReportParameter : BaseRequest
    {
        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? BDate { get; set; }

        /// <summary>
        /// 查询截止时间
        /// </summary>
        public DateTime? EDate { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int IsRead { get; set; }

        /// <summary>
        /// 显示数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页  -1：全部
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        public EnumDataType DateType { get; set; }

    }

    /// <summary>
    /// 查看设备诊断报告，报告详情
    /// </summary>
    public class GetDiagnoseReportDetailParameter : BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int DiagnoseReportID { get; set; }
    }

    /// <summary>
    /// 添加设备诊断报告
    /// </summary>
    public class AddDiagnoseReportParameter : BaseRequest
    {
        /// <summary>
        /// 报告名称
        /// </summary>
        public string DiagnoseReportName { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int AddUserID { get; set; }

        /// <summary>
        /// 报告内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 编辑设备诊断报告
    /// </summary>
    public class EditDiagnoseReportParameter : BaseRequest
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
        /// 修改人
        /// </summary>
        public int UpdateUserID { get; set; }

        /// <summary>
        /// 报告内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 删除设备诊断报告
    /// </summary>
    public class DeleteDiagnoseReportParameter : BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public List<int> DiagnoseReportID { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
    /// </summary>
    public class GetUnreadDRptCountAndDRptByUserIDParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 查询前TopNumber条记录
        /// </summary>
        public int TopNumber { get; set; }
    }

    /// <summary>
    /// 确认诊断报告“已读”
    /// </summary>
    public class ReadDiagnoseReportComfirmParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 诊断报告ID
        /// </summary>
        public int DiagnoseReportID { get; set; }
    }

  
}
