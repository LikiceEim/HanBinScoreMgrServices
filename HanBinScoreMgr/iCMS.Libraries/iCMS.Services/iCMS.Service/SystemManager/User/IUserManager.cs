/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：CMS.Service.SystemManager
 *文件名：  IUserManager
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：用户管理接口 
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Request;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Common.Component.Data.Response;

namespace iCMS.Service.Web.SystemManager
{
    #region 用户管理
    /// <summary>
    /// 用户管理
    /// </summary>
    public interface IUserManager
    {
        #region 获取用户信息数据
        /// <summary>
        /// 获取用户信息数据
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<UsersInfoResult> GetUserInfo(QueryUserParameter Parameter);
        #endregion

        #region 添加用户信息
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<AddUserResult> AddUser(UserParameter Parameter);
        #endregion

        #region 编辑用户信息
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditUser(UserParameter Parameter);
        #endregion

        #region  删除用户信息，支持批量删除
        /// <summary>
        /// 删除用户信息，支持批量删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteUsers(DeleteUserParameter Parameter);
        #endregion

        #region 重置用户密码，支持批量重置
        /// <summary>
        /// 重置用户密码，支持批量重置
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> ResetUserPassWord(RsetUserPassWordParameter Parameter);
        #endregion

        #region 用户管理设备范围
        /// <summary>
        /// 用户管理设备范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> JurisdictionOfDevices(JurisdictionOfDevicesParameter Parameter);
        #endregion

        #region 用户登陆
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<UserLoginResult> Login(LoginParameter param);
        #endregion

        #region 用户密码设置
        BaseResponse<bool> ResetPSW(ResetPSWParameter param);
        #endregion

        #region 获取用户管理设备、传感器范围
        BaseResponse<UserDevManagedResult> UserDevManagedRange(UserIDParameter Parameter);
        #endregion

        #region 设置用户管理设备、传感器范围
        BaseResponse<bool> SetUserManagedRange(UserDevManagedParameter Parameter);
        #endregion
    }
    #endregion
}