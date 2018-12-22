/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Service.SystemManager
 *文件名：  IRoleManager
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：角色管理接口 
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemManager;

namespace iCMS.Service.Web.SystemManager
{
    #region 角色管理
    /// <summary>
    /// 角色管理
    /// </summary>
    public interface IRoleManager
    {
        #region 查询角色信息
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<RolesDataResult> QueryRoleInfo(QueryRolesInfoParameter Parameter);
        #endregion

        #region 角色添加
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> AddRoleInfo(AddRoleParameter Parameter);
        #endregion

        #region 角色编辑
        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditRoleInfo(EditRoleParameter Parameter);
        #endregion

        #region 角色删除
        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteRoleInfo(DeleteRoleParameter Parameter);
        #endregion

        #region 角色赋权
        /// <summary>
        /// 角色赋权
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> RoleAuthorization(RoleAuthorizationParameter Parameter);
        #endregion
    }
    #endregion
}
