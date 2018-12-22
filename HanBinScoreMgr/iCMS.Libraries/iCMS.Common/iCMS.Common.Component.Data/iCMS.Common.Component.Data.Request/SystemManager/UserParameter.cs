/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemManager
 *文件名：  UserParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：添加、编辑用户参数
 *=====================================================================**/
using System.Collections.Generic;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemManager
{
    #region 用户管理

    #region 用户ID
    /// <summary>
    /// 用户ID
    /// </summary>
    public class UserIDParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号，添加为null
        /// </summary>
        public int UserID { get; set; }
       
    }
    #endregion

    #region 设置用户管理设备、传感器范围
    /// <summary>
    /// 设置用户管理设备、传感器范围
    /// </summary>
    public class UserDevManagedParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号，添加为null
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 设备ID集合
        /// </summary>
        public List<int> DeviceID { get; set; }

        /// <summary>
        /// 传感器ID集合
        /// </summary>
        public List<int> WSID { get; set; }

    }
    #endregion

    #region 用户
    /// <summary>
    /// 用户
    /// </summary>
    public class UserParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号，添加为null
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 角色ID（外键）
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否显示在前台0否，1是
        /// </summary>
        public int IsShow { get; set; }
    }
    #endregion

    #region 删除用户
    /// <summary>
    /// 删除用户
    /// </summary>
    public class DeleteUserParameter : BaseRequest
    {
        /// <summary>
        /// 待删除用户ID集合
        /// </summary>
        public List<int> UsersID { get; set; }

        /// <summary>
        /// 当前操作用户ID
        /// </summary>
        public int CurrentUserID { get; set; }
    }
    #endregion

    #region 重置用户密码
    /// <summary>
    /// 重置用户密码
    /// </summary>
    public class RsetUserPassWordParameter : BaseRequest
    {
        /// <summary>
        /// 待重置密码用户ID集合
        /// </summary>
        public List<int> UsersID { get; set; }

        /// <summary>
        /// 重置新密码ID
        /// </summary>
        public string NewPassWord { get; set; }
    }
    #endregion

    #region 重置用户密码
    /// <summary>
    /// 重置用户密码
    /// </summary>
    public class ResetPSWParameter : BaseRequest
    {
        public int UserID { get; set; }

        public string OldPSW { get; set; }

        public string NewPSW { get; set; }
    }
    #endregion

    #region 查询用户信息
    /// <summary>
    /// 查询用户信息
    /// </summary>
    public class QueryUserParameter : BaseRequest
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

    #region 用户登录
    /// <summary>
    /// 用户登录 
    /// </summary>
    public class LoginParameter : BaseRequest
    {
        public string AccountName { get; set; }

        public string PSW { get; set; }
    }
    #endregion

    #region 用户登录信息
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class UserLogInfoParameter : BaseRequest
    {
        public int UserLogID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Record { get; set; }

        public System.DateTime AddDate { get; set; }

        public string IPAddress { get; set; }

        /// <summary>
        /// 是否删除false否，true是
        /// </summary>
        public bool IsDeleted { get; set; }
    }
    #endregion

    #endregion
}
