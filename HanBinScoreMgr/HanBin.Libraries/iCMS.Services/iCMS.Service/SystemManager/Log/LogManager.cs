/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Service.SystemManager
 *文件名：  LogManager
 *创建人：  
 *创建时间：
 *描述：日志管理业务处理类
 *
 *修改人：张辽阔
 *修改时间：2016-11-15
 *描述：增加错误编码
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Tool;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.Utility;
using iCMS.Frameworks.Core.Repository;

namespace iCMS.Service.Web.SystemManager
{
    #region 日志管理
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogManager : ILogManager
    {
        private readonly IRepository<SysLog> sysLogRepository = null;
        private readonly IRepository<UserLog> userLogRepository = null;

        public LogManager(IRepository<SysLog> sysLogRepository, IRepository<UserLog> userLogRepository)
        {
            this.sysLogRepository = sysLogRepository;
            this.userLogRepository = userLogRepository;
        }

        #region 查询系统日志
        /// <summary>
        /// 查询系统日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<SystemLogResult> QuerySystemLog(QuerySystemLogParameter Parameter)
        {
            BaseResponse<SystemLogResult> result = new BaseResponse<SystemLogResult>();
            var total = 0;
            var sysLogInfoList = new List<SysLogInfo>();
            IQueryable<SysLog> sysLogList = null;

            try
            {
                ListSortDirection sortDirection = Parameter.Order.ToLower().Equals("desc") ? ListSortDirection.Descending : ListSortDirection.Ascending;
                if (Parameter.IsSuperAdmin)
                {
                    sysLogList = sysLogRepository.GetDatas<SysLog>(p => true, false);
                }
                else
                {
                    sysLogList = sysLogRepository.GetDatas<SysLog>(t => t.UserID != 1011, false);
                }

                var users = new iCMSDbContext().Users.ToList();
                var linq =
                    from sysLog in sysLogList
                    join u in users
                    on sysLog.UserID equals u.UserID into sysLogGroups
                    from sl in sysLogGroups.DefaultIfEmpty(new User())
                    select new SysLogInfo
                    {
                        SysLogID = sysLog.SysLogID,
                        UserID = sysLog.UserID,
                        UserName = sl.UserName,
                        Record = sysLog.Record,
                        AddDate = sysLog.AddDate,
                        IsDeleted = sl.IsDeleted,
                        IPAddress = sysLog.IPAddress,
                    };
                sysLogInfoList = linq.AsQueryable()
                    .Where(Parameter.Page, Parameter.PageSize, out total, new PropertySortCondition(Parameter.Sort, sortDirection))
                    .ToList();

                result.Result = new SystemLogResult { Total = total, SysLogInfo = sysLogInfoList, Reason = "" };
                result.IsSuccessful = true;
                return result;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Result = new SystemLogResult { Total = 0, SysLogInfo = new List<SysLogInfo>(), Reason = "003871" };
                return result;
            }
        }
        #endregion

        #region 查询用户日志
        /// <summary>
        /// 查询用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UserLogResult> QueryUserLog(QueryUserLogParameter Parameter)
        {
            BaseResponse<UserLogResult> result = new BaseResponse<UserLogResult>();

            var total = 0;
            var sortDirection = Parameter.Order.ToLower().Equals("desc") ? ListSortDirection.Descending : ListSortDirection.Ascending;

            try
            {
                IQueryable<UserLog> userLogs = null;
                if (Parameter.Page == -1)
                {
                    if (Parameter.IsSuperAdmin)
                    {
                        userLogs = userLogRepository.GetDatas<UserLog>(p => true, false);
                    }
                    else
                    {
                        userLogs = userLogRepository.GetDatas<UserLog>(t => t.UserID != 1011, false);
                    }
                }
                else
                {
                    if (Parameter.IsSuperAdmin)
                    {
                        userLogs = userLogRepository.GetDatas<UserLog>(p => true, false);
                    }
                    else
                    {
                        userLogs = userLogRepository.GetDatas<UserLog>(t => t.UserID != 1011, false);
                    }
                }

                var users = new iCMSDbContext().Users.ToList();
                var linq = from log in userLogs
                           join user in users
                           on log.UserID equals user.UserID into userlogGroup
                           from ul in userlogGroup.DefaultIfEmpty(new User())
                           select new UserLogInfo
                           {
                               UserLogID = log.UserLogID,
                               UserID = log.UserID,
                               Record = log.Record,
                               UserName = ul.UserName,
                               AddDate = log.AddDate,
                               IsDeleted = ul.IsDeleted,
                           };

                result.Result = new UserLogResult
                {
                  
                    UserLogInfo = linq.AsQueryable()
                        .Where(Parameter.Page, Parameter.PageSize, out total, new PropertySortCondition(Parameter.Sort, sortDirection))
                        .ToList(),
                    Total = total,
                };
                result.IsSuccessful = true;
                return result;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.Code = "003881";
                result.IsSuccessful = false;
                result.Result = new UserLogResult { Total = 0, UserLogInfo = new List<UserLogInfo>() };
                return result;
            }
        }
        #endregion

        #region 添加日志
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> AddUserLog(UserLogInfoParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (Parameter != null)
            {
                UserLog userLog = new UserLog()
                {
                    AddDate = System.DateTime.Now,
                    Record = Parameter.Record,
                    UserID = Parameter.UserID
                };
                OperationResult operResu = userLogRepository.AddNew<UserLog>(userLog);
                if (operResu.ResultType == EnumOperationResultType.Success)
                {
                    result.IsSuccessful = true;
                    return result;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Code = "003891";
                    return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Code = "003902";
                return result;
            }
        }
        #endregion

        #region 编辑用户日志
        /// <summary>
        /// 编辑用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditUserLog(UserLogInfoParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                if (Parameter != null)
                {
                    var userLogInDb = userLogRepository.GetByKey(Parameter.UserLogID);
                    userLogInDb.UserID = Parameter.UserID;
                    userLogInDb.Record = Parameter.Record;

                    OperationResult operResu = userLogRepository.Update<UserLog>(userLogInDb);
                    if (operResu.ResultType == EnumOperationResultType.Success)
                    {
                        result.IsSuccessful = true;
                        return result;
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Code = "003911";
                        return result;
                    }
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Code = "003922";
                    return result;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "003911";
                return result;
            }
        }
        #endregion

        #region 删除用户日志
        /// <summary>
        /// 删除用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteUserLog(List<int> Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            try
            {
                if (Parameter == null)
                {
                    result.IsSuccessful = false;
                    result.Code = "003932";
                    return result;
                }
                foreach (var id in Parameter)
                {
                    userLogRepository.Delete<UserLog>(id);
                }
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                result.IsSuccessful = false;
                result.Code = "003941";
                return result;
            }
        }
        #endregion

        #region 系统日志写入
        /// <summary>
        /// 系统日志写入
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> LogSysMessage(AddSysLogParameter param)
        {
            try
            {
                SysLog sysLog = new SysLog();
                sysLog.UserID = param.UserID;
                sysLog.Record = param.Record;
                sysLog.IPAddress = param.IPAddress;
                sysLog.AddDate = param.AddDate;

                sysLogRepository.AddNew<SysLog>(sysLog);
                return new BaseResponse<bool>();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return new BaseResponse<bool>();
            }
        }
        #endregion
    }
    #endregion
}