using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    /// <summary>
    /// 查看设备维修日志，获取列表
    /// </summary>
    public class GetDeviceMaintainReportParameter : BaseRequest
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
        /// 日期类型
        /// </summary>
        public EnumDataType DateType { get; set; }
    }

    /// <summary>
    /// 查看网关维修日志，获取列表
    /// </summary>
    public class GetWGMaintainReportParameter : BaseRequest
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// 查询开始时间
        /// </summary>
        public DateTime? BDate { get; set; }

        /// <summary>
        /// 查询截止时间
        /// </summary>
        public DateTime? EDate { get; set; }

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
        /// 日期类型
        /// </summary>
        public EnumDataType DateType { get; set; }
    }

    /// <summary>
    /// 查看传感器维修日志，获取列表
    /// </summary>
    public class GetWSMaintainReportParameter : BaseRequest
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// 查询开始时间
        /// </summary>
        public DateTime? BDate { get; set; }

        /// <summary>
        /// 查询截止时间
        /// </summary>
        public DateTime? EDate { get; set; }

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
        /// 日期类型
        /// </summary>
        public EnumDataType DateType { get; set; }
    }

    /// <summary>
    /// 查看维修日志，报告详情
    /// </summary>
    public class GetMaintainReportDetailParameter : BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int MaintainReportID { get; set; }
    }

    /// <summary>
    /// 添加维修日志
    /// </summary>
    public class AddMaintainReportParameter : BaseRequest
    {
        /// <summary>
        /// 报告名称
        /// </summary>
        public string MaintainReportName { get; set; }

        /// <summary>
        /// 报告类型
        /// 1：设备报告
        /// 2：无线传感器报告
        /// 3：网关报告
        /// 4：有线传感器报告
        /// 5：采集单元报告
        /// </summary>
        public int ReportType { get; set; }

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
    /// 编辑维修日志
    /// </summary>
    public class EditMaintainReportParameter : BaseRequest
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
        /// 修改人
        /// </summary>
        public int UpdateUserID { get; set; }

        /// <summary>
        /// 报告内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 删除维修日志
    /// </summary>
    public class DeleteMaintainReportParameter : BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public List<int> MaintainReportID { get; set; }
    }

    /// <summary>
    /// 通过用户ID获取未读维修日志数量、可读全部日志数量，以及未读维修日志列表
    /// </summary>
    public class GetUnreadMRptCountAndMRptByUserIDParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 报告类型
        /// 1：设备报告
        /// 2：传感器报告
        /// 3：网关报告
        /// </summary>
        public int ReportType { get; set; }

        /// <summary>
        /// ID
        /// ReportType=1：为监测树ID
        /// ReportType=2：为无线传感器ID
        /// ReportType=3：为网关ID
        /// ReportType=3：为无线传感器ID
        /// ReportType=3：为采集单元报告
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 查询前TopNumber条记录
        /// </summary>
        public int TopNumber { get; set; }
    }

    /// <summary>
    /// 确认维修日志“已读”
    /// </summary>
    public class ReadMaintainReportComfirmParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 维修日志ID
        /// </summary>
        public int MaintainReportID { get; set; }
    }

    /// <summary>
    /// 模板查看
    /// </summary>
    public class ViewReportTemplateParameter : BaseRequest
    {
        /// <summary>
        /// 报告模板类型
        /// 1：设备诊断报告
        /// 2：设备维修日志
        /// 3：传感器维修日志
        /// 4：网关维修日志
        /// 5：有线传感器日志  add by lwj---2018.05.03
        /// 6：采集单元日志 add by lwj---2018.05.03
        /// </summary>
        public int ReportTemplateType { get; set; }
    }

    /// <summary>
    /// 模板编辑
    /// </summary>
    public class EditReportTemplateParameter : BaseRequest
    {
        /// <summary>
        /// 报告模板类型
        /// 1：设备诊断报告
        /// 2：设备维修日志
        /// 3：传感器维修日志
        /// 4：网关维修日志
        /// 5：有线传感器维修日志     add by lwj
        /// 6：采集单元维修日志      add by lwj
        /// </summary>
        public int ReportTemplateType { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public int ReportTemplateID { get; set; }

        /// <summary>
        /// 报告模板名称
        /// </summary>
        public string ReportTemplateName { get; set; }

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
    /// 判断诊断报告/维修日志名称是否重复
    /// </summary>
    public class IsReportNameExistParameter : BaseRequest
    {
        /// <summary>
        /// 报告类型：1 诊断报告; 2 维修日志
        /// </summary>
        public int ReportType { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
