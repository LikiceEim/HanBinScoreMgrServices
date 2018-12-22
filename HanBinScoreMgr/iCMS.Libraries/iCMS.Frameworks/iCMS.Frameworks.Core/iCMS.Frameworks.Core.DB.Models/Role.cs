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
    /// 用户角色表
    /// </summary>
    [Table("T_SYS_ROLE")]
    public class Role : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int RoleID { get; set; }

        /// <summary>
        /// 是否显示在前台0否，1是
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 是否是默认0否，1是
        /// </summary>
        public int IsDeault { get; set; }

        /// <summary>
        /// 角色Code
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        #endregion Model

        #region Ctor
        public Role()
        {
            IsShow = 1;
        }
        #endregion
    }
}