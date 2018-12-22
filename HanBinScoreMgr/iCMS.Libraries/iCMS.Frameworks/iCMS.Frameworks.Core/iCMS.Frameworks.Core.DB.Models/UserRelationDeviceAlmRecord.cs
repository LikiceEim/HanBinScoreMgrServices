/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  USERRELATIONDEVALMRECORD
 *创建人：  王颖辉
 *创建时间：2017/09/22 18:01:13
 *描述： 
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 用户未查看设备报警提醒记录表
    /// </summary>
    [Table("T_SYS_USER_RELATION_DEV_ALMRECORD")]
    public partial class UserRelationDeviceAlmRecord : EntityBase
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
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 设备报警记录ID
        /// </summary>
        public int DeviceAlmRecordID
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
        public DateTime DeviceAlmTime
        {
            get;
            set;
        }  
    }
}