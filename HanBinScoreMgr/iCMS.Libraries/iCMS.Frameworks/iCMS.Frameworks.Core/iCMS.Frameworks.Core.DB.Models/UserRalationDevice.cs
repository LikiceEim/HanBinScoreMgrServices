/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 用户查看范围关系表
    /// </summary>
    [Table("T_SYS_USER_RELATION_DEVICE")]
    public class UserRalationDevice : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int? DevId { get; set; }

        /// <summary>
        /// 以#间隔并逆序显示
        /// </summary>
        public string MTIds { get; set; }

        #endregion Model
    }
}