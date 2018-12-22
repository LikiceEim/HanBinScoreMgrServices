/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.SystemManager
 *文件名：  SystemManagerService
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *
 *修改人：张辽阔
 *修改时间：2016-11-11
 *描述：增加错误编码
 *
 *修改人：张辽阔
 *修改时间：2016-12-15
 *描述：未通过安全验证时，增加日志记录
 *=====================================================================**/

using System.Threading.Tasks;

using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Service.Web.SystemManager;
using iCMS.Presentation.Common;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Common.Component.Tool;
using System.ServiceModel;

namespace iCMS.Presentation.Server.SystemManager
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class SystemManagerService : BaseService, ISystemManagerService
    {
        #region 变量

        public IUserManager userManager { get; private set; }
        public IRoleManager roleManager { get; private set; }
        public ILogManager logManager { get; private set; }

        #endregion

        public SystemManagerService(IUserManager userManager, IRoleManager roleManager, ILogManager logManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logManager = logManager;
        }

        /// <summary>
        /// 获取用户信息数据
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UsersInfoResult> UserInfoView(QueryUserParameter Parameter)
        {
            if (ValidateData<QueryUserParameter>(Parameter))
            {
                return userManager.GetUserInfo(Parameter);
            }
            else
            {
                BaseResponse<UsersInfoResult> result = new BaseResponse<UsersInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001191";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<AddUserResult> UserInfoAdd(UserParameter Parameter)
        {
            if (ValidateData<UserParameter>(Parameter))
            {
                return userManager.AddUser(Parameter);
            }
            else
            {
                BaseResponse<AddUserResult> result = new BaseResponse<AddUserResult>();
                result.IsSuccessful = false;
                result.Code = "001201";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserInfoEdit(UserParameter Parameter)
        {
            if (ValidateData<UserParameter>(Parameter))
            {
                return userManager.EditUser(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001211";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 删除用户信息，支持批量删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserInfoDelete(DeleteUserParameter Parameter)
        {
            if (ValidateData<DeleteUserParameter>(Parameter))
            {
                return userManager.DeleteUsers(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001221";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 重置用户密码，支持批量重置
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserPSWReset(RsetUserPassWordParameter Parameter)
        {
            if (ValidateData<RsetUserPassWordParameter>(Parameter))
            {
                return userManager.ResetUserPassWord(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001231";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 用户管理设备范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        //public BaseResponse<bool> UserDevManagedRange(JurisdictionOfDevicesParameter Parameter)
        //{
        //    if (ValidateData<JurisdictionOfDevicesParameter>(Parameter))
        //    {
        //        return userManager.JurisdictionOfDevices(Parameter);
        //    }
        //    else
        //    {
        //        BaseResponse<bool> result = new BaseResponse<bool>();
        //        result.IsSuccessful = false;
        //        result.Code = "001241";
        //        Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
        //        return result;
        //    }
        //}

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<RolesDataResult> RoleInfoView(QueryRolesInfoParameter Parameter)
        {
            if (ValidateData<QueryRolesInfoParameter>(Parameter))
            {
                return roleManager.QueryRoleInfo(Parameter);
            }
            else
            {
                BaseResponse<RolesDataResult> result = new BaseResponse<RolesDataResult>();
                result.IsSuccessful = false;
                result.Code = "001251";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> RoleInfoAdd(AddRoleParameter Parameter)
        {
            if (ValidateData<AddRoleParameter>(Parameter))
            {
                return roleManager.AddRoleInfo(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001261";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> RoleInfoEdit(EditRoleParameter Parameter)
        {
            if (ValidateData<EditRoleParameter>(Parameter))
            {
                return roleManager.EditRoleInfo(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001271";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> RoleInfoDelete(DeleteRoleParameter Parameter)
        {
            if (ValidateData<DeleteRoleParameter>(Parameter))
            {
                return roleManager.DeleteRoleInfo(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001281";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 角色赋权
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> GetModuleForRole(RoleAuthorizationParameter Parameter)
        {
            if (ValidateData<RoleAuthorizationParameter>(Parameter))
            {
                return roleManager.RoleAuthorization(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001291";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 系统日志查看
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<SystemLogResult> SysLogInfoView(QuerySystemLogParameter Parameter)
        {
            if (ValidateData<QuerySystemLogParameter>(Parameter))
            {
                return logManager.QuerySystemLog(Parameter);
            }
            else
            {
                BaseResponse<SystemLogResult> result = new BaseResponse<SystemLogResult>();
                result.IsSuccessful = false;
                result.Code = "001301";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 查询用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UserLogResult> UserLogInfoView(QueryUserLogParameter Parameter)
        {
            if (ValidateData<QueryUserLogParameter>(Parameter))
            {
                return logManager.QueryUserLog(Parameter);
            }
            else
            {
                BaseResponse<UserLogResult> result = new BaseResponse<UserLogResult>();
                result.IsSuccessful = false;
                result.Code = "001311";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserLogAdd(UserLogInfoParameter Parameter)
        {
            if (ValidateData<UserLogInfoParameter>(Parameter))
            {
                return logManager.AddUserLog(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001321";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 编辑用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserLogEdit(UserLogInfoParameter Parameter)
        {
            if (ValidateData<UserLogInfoParameter>(Parameter))
            {
                return logManager.EditUserLog(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001331";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 删除用户日志
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> UserLogDelete(DeleteUserLogsParameter Parameter)
        {
            if (ValidateData<DeleteUserLogsParameter>(Parameter))
            {
                return logManager.DeleteUserLog(Parameter.UserLogsID);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001341";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginParam"></param>
        /// <returns></returns>
        public BaseResponse<UserLoginResult> Login(LoginParameter loginParam)
        {
            if (this.ValidateData<LoginParameter>(loginParam))
            {
                return userManager.Login(loginParam);
            }
            else
            {
                BaseResponse<UserLoginResult> result = new BaseResponse<UserLoginResult>();
                result.IsSuccessful = false;
                result.Code = "001351";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 用户密码设置

        public BaseResponse<bool> ResetPSW(ResetPSWParameter param)
        {
            if (this.ValidateData<ResetPSWParameter>(param))
            {
                return userManager.ResetPSW(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001361";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        /// <summary>
        /// 获取用户管理设备、传感器范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UserDevManagedResult> UserDevManagedRange(UserIDParameter Parameter)
        {
            if (this.ValidateData<UserIDParameter>(Parameter))
            {
                return userManager.UserDevManagedRange(Parameter);
            }
            else
            {
                BaseResponse<UserDevManagedResult> result = new BaseResponse<UserDevManagedResult>();
                result.IsSuccessful = false;
                result.Code = "001351";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 设置用户管理设备、传感器范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> SetUserManagedRange(UserDevManagedParameter Parameter)
        {
            if (this.ValidateData<UserDevManagedParameter>(Parameter))
            {
                return userManager.SetUserManagedRange(Parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001351";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

    }
}