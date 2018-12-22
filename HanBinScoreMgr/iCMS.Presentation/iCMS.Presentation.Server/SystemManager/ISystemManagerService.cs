/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.SystemManager
 *文件名：  ISystemManagerService
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *=====================================================================**/
using System.ServiceModel;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemManager;

namespace iCMS.Presentation.Server.SystemManager
{
    #region 系统管理
    /// <summary>
    /// 系统管理
    /// </summary>
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISystemManagerService”。
    [ServiceContract]
    public interface ISystemManagerService
    {
        #region 用户信息
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserInfoView",
                     BodyStyle = WebMessageBodyStyle.Bare,
                     Method = "POST",
                     RequestFormat = WebMessageFormat.Json,
                     ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<UsersInfoResult> UserInfoView(QueryUserParameter Parameter);

        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserInfoAdd",
                     BodyStyle = WebMessageBodyStyle.Bare,
                     Method = "POST",
                     RequestFormat = WebMessageFormat.Json,
                     ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<AddUserResult> UserInfoAdd(UserParameter Parameter);
        #endregion

        #region 用户编辑
        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserInfoEdit",
                     BodyStyle = WebMessageBodyStyle.Bare,
                     Method = "POST",
                     RequestFormat = WebMessageFormat.Json,
                     ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserInfoEdit(UserParameter Parameter);
        #endregion

        #region 用户删除
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserInfoDelete",
                     BodyStyle = WebMessageBodyStyle.Bare,
                     Method = "POST",
                     RequestFormat = WebMessageFormat.Json,
                     ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserInfoDelete(DeleteUserParameter Parameter);
        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserPSWReset",
                    BodyStyle = WebMessageBodyStyle.Bare,
                    Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserPSWReset(RsetUserPassWordParameter Parameter);
        #endregion

        //#region 管理设备范围设置
        ///// <summary>
        /////管理设备范围设置
        ///// </summary>
        ///// <param name="Parameter"></param>
        ///// <returns></returns>
        //[WebInvoke(UriTemplate = "UserDevManagedRange",
        //            BodyStyle = WebMessageBodyStyle.Bare,
        //            Method = "POST",
        //            RequestFormat = WebMessageFormat.Json,
        //            ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResponse<bool> UserDevManagedRange(JurisdictionOfDevicesParameter Parameter);
        //#endregion

        #region 角色查看
        /// <summary>
        /// 角色查看
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "RoleInfoView",
                   BodyStyle = WebMessageBodyStyle.Bare,
                   Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<RolesDataResult> RoleInfoView(QueryRolesInfoParameter Parameter);
        #endregion

        #region 角色添加
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "RoleInfoAdd",
                   BodyStyle = WebMessageBodyStyle.Bare,
                   Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> RoleInfoAdd(AddRoleParameter Parameter);
        #endregion

        #region 角色编辑
        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "RoleInfoEdit",
                  BodyStyle = WebMessageBodyStyle.Bare,
                  Method = "POST",
                  RequestFormat = WebMessageFormat.Json,
                  ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> RoleInfoEdit(EditRoleParameter Parameter);
        #endregion

        #region 删除角色
        [WebInvoke(UriTemplate = "RoleInfoDelete",
                         BodyStyle = WebMessageBodyStyle.Bare,
                         Method = "POST",
                         RequestFormat = WebMessageFormat.Json,
                         ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> RoleInfoDelete(DeleteRoleParameter Parameter);
        #endregion

        #region 获取角色赋权接口
        /// <summary>
        /// 获取角色赋权接口
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetModuleForRole",
                         BodyStyle = WebMessageBodyStyle.Bare,
                         Method = "POST",
                         RequestFormat = WebMessageFormat.Json,
                         ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> GetModuleForRole(RoleAuthorizationParameter Parameter);
        #endregion

        #region 查看系统日志
        /// <summary>
        /// 查看系统日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "SysLogInfoView",
                 BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<SystemLogResult> SysLogInfoView(QuerySystemLogParameter Parameter);
        #endregion

        #region 查看用户日志
        /// <summary>
        /// 查看用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserLogInfoView",
                 BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<UserLogResult> UserLogInfoView(QueryUserLogParameter Parameter);
        #endregion

        #region 用户日志添加
        /// <summary>
        /// 用户日志添加
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserLogAdd",
                 BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserLogAdd(UserLogInfoParameter Parameter);
        #endregion

        #region 用户日志编辑
        /// <summary>
        /// 用户日志编辑
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserLogEdit",
                 BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserLogEdit(UserLogInfoParameter Parameter);
        #endregion

        #region 用户日志删除
        /// <summary>
        /// 用户日志删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "UserLogDelete",
                 BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResponse<bool> UserLogDelete(DeleteUserLogsParameter Parameter);
        #endregion

        #region 登陆验证
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                  Method = "POST",
                  RequestFormat = WebMessageFormat.Json,
                  ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<UserLoginResult> Login(LoginParameter loginParam);
        #endregion

        #region 用户密码设置
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> ResetPSW(ResetPSWParameter param);
        #endregion

        #region 获取用户管理设备、传感器范围
        [WebInvoke(UriTemplate = "UserDevManagedRange",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<UserDevManagedResult> UserDevManagedRange(UserIDParameter Parameter);
        #endregion

        #region 设置用户管理设备、传感器范围
        [WebInvoke(UriTemplate = "SetUserManagedRange", 
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> SetUserManagedRange(UserDevManagedParameter param);
        #endregion
    }
    #endregion
}
