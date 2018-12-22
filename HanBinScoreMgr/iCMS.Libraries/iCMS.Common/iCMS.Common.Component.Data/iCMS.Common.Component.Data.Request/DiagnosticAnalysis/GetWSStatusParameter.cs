/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetWSStatusParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：获取无线传感器数据请求参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    #region 获取无线传感器数据请求参数
    /// <summary>
    /// 获取无线传感器数据请求参数
    /// </summary>
    public class GetWSStatusParameter : BaseRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 0:全部 1：无线网关 2：无线传感器
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 传感器编号
        /// </summary>
        public string WSNo { get; set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        public int? LinkStatus { get; set; }
        /// <summary>
        /// 报警状态
        /// </summary>
        public int? AlmStatus { get; set; }
        /// <summary>
        /// 使用状态0未使用，1使用,-1全部
        /// </summary>
        public int? UseStatus { get; set; }
        /// <summary>
        /// 排序名,如WSName,WGName,MACADDR,SensorType,Status,LinkStatus,RunStatus
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式，desc/asc
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 页数，从1开始,若为-1返回所有的无线传感器记录
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Int	页面行数，从1开始
        /// </summary>
        public int PageSize { get; set; }
    }
    #endregion
}
