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
    /// 用户信息表
    /// </summary>
    [Table("T_SYS_USER")]
    public class User : EntityBase
    {
        public User()
        {
            IsDeleted = false;
        }

        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// 角色ID（外键）
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 是否显示在前台0否，1是
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string PSW { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LockPSW { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否删除0否，1是（0：false，1：true）
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 用户登陆账号
        /// </summary>
        public string AccountName { get; set; }

        #endregion Model
    }
}