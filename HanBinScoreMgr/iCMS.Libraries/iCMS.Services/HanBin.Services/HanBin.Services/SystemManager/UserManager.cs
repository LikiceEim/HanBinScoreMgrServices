using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.HanBin.SystemManage;
using iCMS.Common.Component.Data.Response.HanBin.SystemManager;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using iCMS.Service.Common;

namespace HanBin.Services.SystemManager
{
    public class UserManager : IUserManager
    {
        [Dependency]
        public IRepository<HBUser> hbUserReosiory { get; set; }


        public BaseResponse<LoginResult> Login(LoginParameter parameter)
        {
            BaseResponse<LoginResult> response = new BaseResponse<LoginResult>();
            LoginResult result = new LoginResult();
            try
            {
                var user = hbUserReosiory.GetDatas<HBUser>(t => t.UserToken.Equals(parameter.UserName)
                && t.PWD.Equals(parameter.PWD)
                && t.UseStatus && !t.IsDeleted, true).FirstOrDefault();
                if (user != null)
                {
                    result.RoleID = user.RoleID;
                    response.Result = result;

                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    return response;
                }
            }
            catch (global::System.Exception e)
            {
                response.IsSuccessful = false;
                return response;
            }
        }


        public BaseResponse<bool> AddUser(AddUserParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                HBUser user = new HBUser();
                user.UserToken = parameter.UserToken;
                user.PWD = parameter.PWD;
                user.RoleID = parameter.RoleID;
                user.OrganizationID = parameter.OrganizationID;
                user.AddUserID = parameter.AddUserID;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateID = parameter.AddUserID;

                OperationResult operationResult = hbUserReosiory.AddNew<HBUser>(user);
                if (operationResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("添加用户异常");
                }

                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                return response;
            }
            throw new NotImplementedException();
        }

        public BaseResponse<bool> EditUser(EditUserParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var userInDB = hbUserReosiory.GetByKey(parameter.UserID);
                if (null == userInDB)
                {
                    response.IsSuccessful = false;
                }

                userInDB.UserToken = parameter.UserToken;
                userInDB.RoleID = parameter.RoleID;
                userInDB.OrganizationID = parameter.OrganizationID;
                userInDB.LastUpdateID = parameter.LastUpdateUserID;
                userInDB.LastUpdateDate = DateTime.Now;
                userInDB.Gender = parameter.Gender;

                OperationResult operationResult = hbUserReosiory.Update<HBUser>(userInDB);
                if (operationResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("修改用户发生异常");

                }
                return response;

            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "修改用户发生异常";
                return response;
            }
        }

        public BaseResponse<GetUserInfoResult> GetUserInfo(GetUserInfoParameter parameter)
        {

            BaseResponse<GetUserInfoResult> response = new BaseResponse<GetUserInfoResult>();
            try
            {


                switch (parameter.Sort)
                {
                    case "RoleName":
                        parameter.Sort = "RoleID";
                        break;
                }

                int count = 0;
                if (parameter.Page == 0)
                {
                    parameter.Page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    IQueryable<HBUser> userInfoList = dataContext.HBUsers.Where(p => p.IsDeleted == false);
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("UserID", sortOrder),
                    };

                    var currentUser = dataContext.HBUsers.Where(t => t.UserID == parameter.CurrentUserID).FirstOrDefault();
                    if (currentUser == null)
                    {

                        response.IsSuccessful = false;
                        return response;
                    }

                    int currentRoleID = currentUser.RoleID;
                    //根据当前登录用户角色来显示用户
                    switch (currentRoleID)
                    {
                        case 1:
                            //超级管理员
                            userInfoList.Where(t => true);
                            break;
                        case 2:
                            //一级管理员
                            userInfoList.Where(t => t.RoleID == 2 || t.RoleID == 3);
                            break;
                        case 3:
                            userInfoList.Where(t => t.RoleID == 3);

                            break;
                    }

                    userInfoList = userInfoList.OrderBy(sortList);

                    count = userInfoList.Count();
                    if (parameter.Page > -1)
                    {
                        userInfoList = userInfoList
                            .Skip((parameter.Page - 1) * parameter.PageSize)
                            .Take(parameter.PageSize);
                    }

                    var tempUserInfoList = userInfoList
                        .ToArray()
                        .Select(user =>
                        {
                            HBRole roleNameObj = dataContext.HBRoles.FirstOrDefault(role => role.RoleID == user.RoleID);

                            return new HBUserInfo
                            {
                                UserID = user.UserID,
                                RoleID = user.RoleID,
                                RoleName = roleNameObj == null ? "" : roleNameObj.RoleName,
                                UserToken = user.UserToken,
                                Gender = user.Gender,
                                LastUpdateUserID = user.LastUpdateID,
                                OrganizationID = user.OrganizationID,
                                OrganizationName = string.Empty
                            };
                        })
                        .ToList();
                    response.Result = new GetUserInfoResult
                    {
                        Total = count,
                        UserInfoList = tempUserInfoList
                    };

                    response.IsSuccessful = true;
                    response.Code = null;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = null;
                return response;
            }
        }
    }
}