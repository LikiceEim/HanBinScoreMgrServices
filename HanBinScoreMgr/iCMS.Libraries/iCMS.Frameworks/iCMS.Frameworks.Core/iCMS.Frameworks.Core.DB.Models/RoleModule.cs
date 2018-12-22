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
    /// 角色权限表
    /// </summary>
    [Table("T_SYS_ROLEMODULE")]
    public class RoleModule : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int RMID { get; set; }

        /// <summary>
        /// 角色Code（外键）
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 模块Code（外键）
        /// </summary>
        public string ModuleCode { get; set; }

        #endregion Model
    }
}