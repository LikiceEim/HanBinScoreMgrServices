/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemManager
 *文件名：  UserLoginResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：用户登陆成功返回数据类
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.SystemManager
{
    #region 用户登陆成功返回数据类
    /// <summary>
    /// 用户登陆成功返回数据类
    /// </summary>
    public class UserLoginResult
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色Code
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
    }
    #endregion
}
