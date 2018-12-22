/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemManager
 *文件名：  UserViewDataResult
 *创建人：  李峰
 *创建时间：2016.07.26
 *描述：用户返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.SystemManager
{
    #region 用户返回结果类
    /// <summary>
    /// 创建人：李峰
    /// 创建时间：2016-10-26
    /// 创建记录：用户返回结果类
    /// </summary>
    public class UsersInfoResult
    {
        /// <summary>
        /// 用户返回实体类集合
        /// </summary>
        public List<UserInfo> UserInfo { get; set; }

        /// <summary>
        /// 集合总数
        /// </summary>
        public int Total { get; set; }
    }
    #endregion

    #region 用户返回实体类
    /// <summary>
    /// 创建人：LF
    /// 创建时间：2016-10-26
    /// 创建记录：用户返回实体类
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public string LastLoginDate { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string AddDate { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 用户管理设备树
        /// </summary>
        public string UserManagedDevs { get; set; }

        /// <summary>
        /// 用户管理设备树
        /// </summary>
        public string UserManagedDevsName { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string AccountName { get; set; }

    }
    #endregion

    #region 用户返回实体类
    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-10-17
    /// 创建记录：添加用户返回实体类
    /// </summary>
    public class AddUserResult
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        
    }
    #endregion
}