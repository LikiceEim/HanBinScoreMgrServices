/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  DiagnoseReport
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
    [Table("T_SYS_DIAGNOSE_REPORT")]
    public partial class DiagnoseReport : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int DiagnoseReportID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 诊断报告名称
        /// </summary>
        public string DiagnoseReportName
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 设备ID
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
        /// 诊断报告内容
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