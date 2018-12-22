/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  USERRELATIONWS
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
    /// 用户管理传感器关系表
    /// </summary>
    [Table("T_SYS_USER_RELATION_WS")]
    public partial class UserRelationWS : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int UserRalationWSID
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
        /// 传感器ID
        /// </summary>
        public int WSID
        {
            get;
            set;
        }  
    }
}