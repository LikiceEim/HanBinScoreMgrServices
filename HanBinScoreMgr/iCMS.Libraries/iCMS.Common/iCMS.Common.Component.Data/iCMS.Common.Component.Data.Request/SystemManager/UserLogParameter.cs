/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemManager
 *文件名：  UserLog
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：用户日志
/************************************************************************************/
using System.Collections.Generic;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.SystemManager
{
    #region 日志
    #region 删除日志
    /// <summary>
    /// 删除日志
    /// </summary>
    public class DeleteUserLogsParameter : BaseRequest
    {
        public List<int> UserLogsID { get; set; }
    }
    #endregion

    #region 查询日志
    /// <summary>
    /// 查询日志
    /// </summary>
    public class QuerySystemLogParameter : BaseRequest
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
        /// </summary>
        public bool IsSuperAdmin { set; get; }
    }
    #endregion

    #region 查询用户日志
    /// <summary>
    /// 查询用户日志
    /// </summary>
    public class QueryUserLogParameter : QuerySystemLogParameter
    {
    }
    #endregion
    #endregion
}
