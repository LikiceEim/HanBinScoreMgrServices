/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.SystemManager
 *文件名：  ILogManager
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：日志接口
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Response;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Common.Component.Data.Request.Utility;

namespace iCMS.Service.Web.SystemManager
{
    #region 日志接口
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogManager
    {
        #region 查询系统日志
        /// <summary>
        /// 查询系统日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<SystemLogResult> QuerySystemLog(QuerySystemLogParameter Parameter);
        #endregion

        #region 查询用户日志
        /// <summary>
        /// 查询用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<UserLogResult> QueryUserLog(QueryUserLogParameter Parameter);
        #endregion

        #region 添加日志
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> AddUserLog(UserLogInfoParameter Parameter);
        #endregion

        #region 编辑用户日志
        /// <summary>
        /// 编辑用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditUserLog(UserLogInfoParameter Parameter);
        #endregion

        #region 删除用户日志
        /// <summary>
        /// 删除用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteUserLog(List<int> Parameter);
        #endregion

        #region 系统日志写入
        /// <summary>
        /// 系统日志写入
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> LogSysMessage(AddSysLogParameter param);
        #endregion

    }
    #endregion
}
