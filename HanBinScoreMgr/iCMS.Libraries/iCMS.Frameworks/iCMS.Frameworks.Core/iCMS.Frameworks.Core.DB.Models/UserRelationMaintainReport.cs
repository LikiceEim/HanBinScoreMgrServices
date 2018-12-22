/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  USERRELATIONMAINTAINREPORT
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
    /// 用户未查看维修日志记录表
    /// </summary>
    [Table("T_SYS_USER_RELATION_MAINTAIN_REPORT")]
    public partial class UserRelationMaintainReport : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 维修日志类别
        /// </summary>
        public int ReportType
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
        /// 维修日志ID
        /// </summary>
        public int MaintainReportID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 维修日志创建时间
        /// </summary>
        public DateTime MaintainReportAddDate
        {
            get;
            set;
        }  
    
    }
}