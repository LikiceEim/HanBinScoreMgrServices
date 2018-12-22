/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemManager
 *文件名：  RolesDataResult
 *创建人：  LF
 *创建时间：2016-10-26
 *描述：角色数据返回结果
/************************************************************************************/
using System;
using System.Collections.Generic;


namespace iCMS.Common.Component.Data.Response.SystemManager
{
    #region 角色返回结果类
    /// <summary>
    /// 角色返回结果类
    /// </summary>
    public class RolesDataResult
    {
        /// <summary>
        /// 角色信息集合
        /// </summary>
        public List<RoleInfo> RoleInfo { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
    #endregion

    #region 角色信息

    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { set; get; }
        /// <summary>
        /// 角色Code
        /// </summary>
        public String RoleCode { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public String AddDate { set; get; }
        /// <summary>
        /// 权限
        /// </summary>
        public String ModuleName { set; get; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public String ModuleCode { set; get; }
        /// <summary>
        /// 是否显示在前台
        /// </summary>
        public int IsShow { set; get; }
        /// <summary>
        /// 是否是必选功能0否，1是
        /// </summary>
        public int IsDeault { set; get; }


    }
    #endregion
}