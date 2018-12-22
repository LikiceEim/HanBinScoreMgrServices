/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  USERRELATIONWSALMRECORD
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
    /// 用户未查看网关及传感器报警提醒记录表
    /// </summary>
    [Table("T_SYS_USER_RELATION_WS_ALMRECORD")]
    public partial class UserRelationWSAlmRecord : EntityBase
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
        /// 监测设备类型 1：网关 2：传感器
        /// </summary>
        public int MonitorDeviceType
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 监测设备ID
        /// </summary>
        public int MonitorDeviceID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int WSNAlmRecordID
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态（2：高报； 3：高高报）
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime WSNAlmTime
        {
            get;
            set;
        }  
    }
}