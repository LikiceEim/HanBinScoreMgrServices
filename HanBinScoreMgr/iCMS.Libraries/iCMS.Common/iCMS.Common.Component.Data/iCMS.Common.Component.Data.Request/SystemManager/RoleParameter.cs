/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemManager
 *文件名：  RoleParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：角色类，适用于查询、编辑、添加
 *=====================================================================**/
using System;
using iCMS.Common.Component.Data.Base;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Request.SystemManager
{
    #region 角色

    #region 添加角色
    /// <summary>
    /// 添加角色
    /// </summary>
    public class AddRoleParameter : BaseRequest
    {
        /// <summary>
        /// 角色Code
        /// </summary>
        public String RoleCode { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int IsDeault { get; set; }

        /// <summary>
        //授权模块ID列表
        /// </summary>
        public List<string> Module { set; get; }
    }
    #endregion

    #region 删除角色
    /// <summary>
    /// 删除角色
    /// </summary>
    public class DeleteRoleParameter : BaseRequest
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public List<int> RoleID { set; get; }
    }
    #endregion

    #region 编辑角色
    /// <summary>
    /// 编辑角色
    /// </summary>
    public class EditRoleParameter : BaseRequest
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { set; get; }
        /// <summary>
        /// 角色Code
        /// </summary>
        public string RoleCode { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int IsDeault { get; set; }

        /// <summary>
        //授权模块ID列表
        /// </summary>
        public List<string> Module { set; get; }
    }
    #endregion

    #region 角色权限
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RoleAuthorizationParameter : BaseRequest
    {
        /// <summary>
        /// 角色Code
        /// </summary>
        public String RoleCode { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { set; get; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int IsDeault { get; set; }
        /// <summary>
        /// 授权模块列表
        /// </summary>
        public List<string> Module { set; get; }

        public int RoleID { set; get; }
    }
    #endregion

    #region 查询角色信息
    /// <summary>
    /// 查询角色信息
    /// </summary>
    public class QueryRolesInfoParameter : BaseRequest
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { set; get; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string Order { set; get; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { set; get; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 是否管理员
        /// 原IsRole  是否 IsSuperAdmin更合适？
        /// </summary>
        public bool IsSuperAdmin { set; get; }
    }
    #endregion

    #region 角色获取设备树和Server树
    public class RoleForMonitorTreeAndServerTreeParameter : BaseRequest
    {
        /// <summary>
        /// 角色Code
        /// </summary>
        public string RoleCode { set; get; }
    }
    #endregion

    #endregion
}
