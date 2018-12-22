/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  MAINTAINREPORT
 *创建人：  王颖辉
 *创建时间：2017/02/22 18:01:13
 *描述： 
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("T_SYS_MAINTAIN_REPORT")]
    public partial class MaintainReport : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int MaintainReportID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 维修日志名称
        /// </summary>
        public string MaintainReportName
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 维修日志类别
        /// 1：设备报告
        /// 2：传感器报告
        /// 3：网关报告
        /// </summary>
        public int ReportType
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 设备、传感器或网关 ID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 创建人
        /// </summary>
        public int AddUserID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 修改人
        /// </summary>
        public int UpdateUserID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool? IsDeleted
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 维修日志内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是模板
        /// </summary>
        public bool IsTemplate
        {
            get;
            set;
        }
    }
}