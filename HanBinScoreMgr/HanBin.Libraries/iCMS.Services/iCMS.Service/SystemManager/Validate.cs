/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Service.SystemManager
 *文件名：  Validate
 *创建人：  
 *创建时间：
 *描述：用户和角色参数的验证类
 *
 *修改人：张辽阔
 *修改时间：2016-11-15
 *描述：增加错误编码
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Frameworks.Core.Repository;

namespace iCMS.Service.Web.SystemManager
{
    #region 验证“获取用户信息”的参数是否有效
    /// <summary>
    /// 验证“获取用户信息”的参数是否有效
    /// </summary>
    public class Validate
    {
        #region 变量
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        #endregion

        #region 构造函数
        public Validate(IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }
        #endregion

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“获取用户信息”的参数是否有效
        /// </summary>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        internal  BaseResponse<UsersInfoResult> ValidateGetUsersDataParams(string sort, string order)
        {
            BaseResponse<UsersInfoResult> result = new BaseResponse<UsersInfoResult>();
            result.IsSuccessful = true;

            if (string.IsNullOrWhiteSpace(sort))
            {
                result.IsSuccessful = false;
                result.Code = "004142";
                result.Result = null;
                return result;
            }
            if (string.IsNullOrWhiteSpace(order))
            {
                result.IsSuccessful = false;
                result.Code = "004152";
                result.Result = null;
                return result;
            }
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“添加用户信息”参数是否有效
        /// </summary>
        /// <param name="userJSONStr">用户JSON字符串</param>
        /// <returns></returns>
        internal  BaseResponse<AddUserResult> ValidateAddUserParams(UserParameter Parameter)
        {
            BaseResponse<AddUserResult> result = new BaseResponse<AddUserResult>();
            result.IsSuccessful = true;
            if (Parameter == null)
            {
                result.IsSuccessful = true;
                result.Code = "004162";
                return result;
            }

            if (string.IsNullOrWhiteSpace(Parameter.UserName))
            {
                result.IsSuccessful = false;
                result.Code = "004172";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.AccountName))
            {
                result.IsSuccessful = false;
                result.Code = "004182";
                return result;
            }

            if (userRepository
                .GetDatas<User>(p => p.IsDeleted == false
                    && p.AccountName.Equals(Parameter.AccountName), true)
                .Any())
            {
                result.IsSuccessful = false;
                result.Code = "004192";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.PassWord))
            {
                result.IsSuccessful = false;
                result.Code = "004202";
                return result;
            }
            if (Parameter.RoleID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004212";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Email))
            {
                result.IsSuccessful = false;
                result.Code = "004222";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Phone))
            {
                result.IsSuccessful = false;
                result.Code = "004232";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“编辑用户信息”的参数是否有效
        /// </summary>
        /// <param name="userJSONStr">用户JSON字符串</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateEditUserParams(UserParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            result.IsSuccessful = true;
            if (Parameter == null)
            {
                result.IsSuccessful = false;
                result.Code = "004242";
                return result;
            }
            if (Parameter.UserID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004252";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.UserName))
            {
                result.IsSuccessful = false;
                result.Code = "004262";
                return result;
            }
            if (userRepository
                .GetDatas<User>(p => p.IsDeleted == false
                    && p.UserID != Parameter.UserID
                    && p.AccountName.Equals(Parameter.AccountName),
                    true)
                .Any())
            {
                result.IsSuccessful = false;
                result.Code = "004272";
                return result;
            }
            if (Parameter.RoleID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004282";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Email))
            {
                result.IsSuccessful = false;
                result.Code = "004292";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Phone))
            {
                result.IsSuccessful = false;
                result.Code = "004302";
                return result;
            }
            User tempUserObj = userRepository.GetDatas<User>(p => p.UserID == Parameter.UserID, true).FirstOrDefault();
            if (tempUserObj == null)
            {
                result.IsSuccessful = false;
                result.Code = "004252";
                return result;
            }
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“批量删除用户信息”的参数是否有效
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="currentUserID">当前登录用户ID</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateDeleteUserParams(List<int> userID, int currentUserID)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            result.IsSuccessful = true;
            if (userID == null || !userID.Any())
            {
                result.IsSuccessful = false;
                result.Code = "004312";
                return result;
            }
            if (currentUserID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004322";
                return result;
            }
            if (userRepository.GetByKey(currentUserID) == null)
            {
                result.IsSuccessful = false;
                result.Code = "004322";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“批量修改用户密码”的参数是否有效
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="passWord">用户密码</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateResetPassWordParams(List<int> userID, string passWord)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (userID == null || !userID.Any())
            {
                result.IsSuccessful = false;
                result.Code = "004332";
                return result;
            }
            if (string.IsNullOrWhiteSpace(passWord))
            {
                result.IsSuccessful = false;
                result.Code = "004342";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“添加用户设备关系”的参数是否有效
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateUserManagedDevRangeParams(int userID)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (userID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004352";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“获取角色信息”的参数是否有效
        /// </summary>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        internal  BaseResponse<RolesDataResult> ValidateQueryRoleInfoParams(string sort, string order)
        {
            BaseResponse<RolesDataResult> result = new BaseResponse<RolesDataResult>();
            if (string.IsNullOrWhiteSpace(sort))
            {
                result.IsSuccessful = false;
                result.Code = "004362";
                return result;
            }
            if (string.IsNullOrWhiteSpace(order))
            {
                result.IsSuccessful = false;
                result.Code = "004372";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“添加角色信息”的参数是否有效
        /// </summary>
        /// <param name="roleJSONStr">角色JSON字符串</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateAddRoleParams(AddRoleParameter role)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            if (role == null)
            {
                result.IsSuccessful = false;
                result.Code = "004382";
                return result;
            }
            if (string.IsNullOrEmpty(role.RoleName))
            {
                result.IsSuccessful = false;
                result.Code = "004392";
                return result;
            }

            if (roleRepository.GetDatas<Role>(p => p.RoleName.Equals(role.RoleName), true).Any())
            {
                result.IsSuccessful = false;
                result.Code = "004402";
                return result;
            }
            result.IsSuccessful = true;

            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-27
        /// 创建记录：验证“修改角色信息”的参数是否有效
        /// </summary>
        /// <param name="roleJSONStr">角色JSON字符串</param>
        /// <returns></returns>
        internal  BaseResponse<bool> ValidateEditRoleParams(EditRoleParameter role)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            if (role == null)
            {
                result.IsSuccessful = false;
                result.Code = "004412";
                return result;
            }
            if (role.RoleID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "004422";
                return result;
            }
            if (string.IsNullOrEmpty(role.RoleName))
            {
                result.IsSuccessful = false;
                result.Code = "004432";
                return result;
            }
            if (roleRepository
                .GetDatas<iCMS.Frameworks.Core.DB.Models.Role>(p => p.RoleID != role.RoleID
                    && p.RoleName.Equals(role.RoleName),
                    true)
                .Any())
            {
                result.IsSuccessful = false;
                result.Code = "004442";
                return result;
            }
            iCMS.Frameworks.Core.DB.Models.Role tempRoleObj = roleRepository
                .GetDatas<iCMS.Frameworks.Core.DB.Models.Role>(p => p.RoleID == role.RoleID, true)
                .FirstOrDefault();
            if (tempRoleObj == null)
            {
                result.IsSuccessful = false;
                result.Code = "004422";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-26
        /// 创建记录：验证“添加角色模块关系”的参数是否有效
        /// </summary>
        /// <param name="roleCode">角色Code</param>
        /// <returns></returns>
        /// 创建人：王龙杰
        /// 创建时间：2017-09-27
        /// 创建记录：roleId => roleCode
        internal BaseResponse<bool> ValidateRoleAuthorizationParameter(string roleCode)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (string.IsNullOrEmpty(roleCode))
            {
                result.IsSuccessful = false;
                result.Code = "004452";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-30
        /// 创建记录：验证“修改用户密码”的参数是否有效
        /// </summary>
        /// <param name="pswJSONStr">修改密码JSON字符串</param>
        /// <returns></returns>
        internal  dynamic ValidatePSWResetForUserParams(ResetPSWParameter param)
        {
            dynamic result = new ExpandoObject();

            //if (passWordDataResultObj == null)
            //{
            //    result.Result = true;
            //    result.Message = "#201030012";
            //    return result;
            //}
            if (param.UserID <= 0)
            {
                result.Result = true;
                result.Message = "004462";
                return result;
            }
            if (string.IsNullOrEmpty(param.OldPSW))
            {
                result.Result = true;
                result.Message = "004472";
                return result;
            }
            if (string.IsNullOrEmpty(param.NewPSW))
            {
                result.Result = true;
                result.Message = "004482";
                return result;
            }
            User userObj = userRepository.GetDatas<User>(p => p.UserID == param.UserID, true).FirstOrDefault();
            if (userObj == null)
            {
                result.Result = true;
                result.Message = "004462";
                return result;
            }
            if (userObj.PSW != param.OldPSW)
            {
                result.Result = true;
                result.Message = "004492";
                return result;
            }

            userObj.PSW = param.NewPSW;

            result.Result = false;
            result.UserEntity = userObj;
            return result;
        }
    }
    #endregion
}