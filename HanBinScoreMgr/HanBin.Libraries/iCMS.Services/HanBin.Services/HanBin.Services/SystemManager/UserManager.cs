using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Core.DB.Models;
using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using HanBin.Common.Component.Tool;
using HanBin.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using HanBin.Service.Common;
using HanBin.Common.Commonent.Data.Enum;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace HanBin.Services.SystemManager
{
    public class UserManager : IUserManager
    {
        [Dependency]
        public IRepository<HBUser> hbUserReosiory { get; set; }
        [Dependency]
        public IRepository<Organization> organRepository { get; set; }
        [Dependency]
        public IRepository<HBRole> roleRepoitory { get; set; }

        [Dependency]
        public IRepository<BackupLog> backlogRepository { get; set; }

        public UserManager()
        {
            hbUserReosiory = new Repository<HBUser>();
            organRepository = new Repository<Organization>();
            roleRepoitory = new Repository<HBRole>();
            backlogRepository = new Repository<BackupLog>();
        }

        /// <summary>
        /// 登陆后返回Token 和角色ID
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<LoginResult> Login(LoginParameter parameter)
        {
            BaseResponse<LoginResult> response = new BaseResponse<LoginResult>();
            LoginResult result = new LoginResult();
            try
            {
                var encodePWD = MD5Helper.MD5Encrypt64(parameter.PWD);
                var user = hbUserReosiory.GetDatas<HBUser>(t => t.UserToken.Equals(parameter.UserName)
                && t.PWD.Equals(encodePWD)
                && t.UseStatus && !t.IsDeleted, true).FirstOrDefault();
                if (user != null)
                {
                    result.RoleID = user.RoleID;
                    result.UserID = user.UserID;
                    result.UserToken = user.UserToken;

                    var payload = new Dictionary<string, object>
                            {
                                   {"name", user.UserToken },                
                                   {"exp",1000*60*30},
                                   {"role",user.RoleID }
                                   //{"date",DateTime.Now }                                  
                            };
                    var privateKey = Utilitys.GetAppConfig("PrivateKey");
                    //var privateKey = AppConfigHelper.GetConfigValue("PrivateKey");

                    result.Token = JsonWebToken.Encode(payload, privateKey, JwtHashAlgorithm.HS512);

                    response.Result = result;

                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Reason = "用户名或者密码错误";
                    return response;
                }
            }
            catch (global::System.Exception e)
            {
                LogHelper.WriteLog(e);
                response.Reason = "用户名或者密码错误";
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
                user.PWD = MD5Helper.MD5Encrypt64(parameter.PWD);//密码MD5加密
                user.RoleID = parameter.RoleID;
                user.OrganizationID = parameter.OrganizationID;
                user.AddUserID = parameter.AddUserID;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = parameter.AddUserID;
                user.UseStatus = true;

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
                userInDB.LastUpdateUserID = parameter.LastUpdateUserID;
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
                LogHelper.WriteLog(e);
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
                if (string.IsNullOrEmpty(parameter.Sort))
                {
                    parameter.Sort = "UserToken";
                }
                if (string.IsNullOrEmpty(parameter.Order))
                {
                    parameter.Order = "asc";
                }
                if (parameter.Page == 0)
                {
                    parameter.Page = 1;
                }
                if (parameter.PageSize == 0)
                {
                    parameter.PageSize = 10;
                }

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

                    if (parameter.OrganizationID.HasValue && parameter.OrganizationID.Value > 0)
                    {
                        userInfoList = userInfoList.Where(t => t.OrganizationID == parameter.OrganizationID.Value);
                    }

                    userInfoList = userInfoList.OrderBy(sortList);

                    count = userInfoList.Count();

                    var organArray = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true).ToList();
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
                            var organ = organArray.Where(t => t.OrganID == user.OrganizationID).FirstOrDefault();
                            return new HBUserInfo
                            {
                                UserID = user.UserID,
                                RoleID = user.RoleID,
                                RoleName = roleNameObj == null ? string.Empty : roleNameObj.RoleName,
                                UserToken = user.UserToken,
                                Gender = user.Gender,
                                LastUpdateUserID = user.LastUpdateUserID,
                                OrganizationID = user.OrganizationID,
                                OrganizationName = organ == null ? string.Empty : organ.OrganFullName,
                                UseStatus = user.UseStatus,
                                AddDate = user.AddDate
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

        public BaseResponse<GetRoleInfoListResult> GetRoleInfoList()
        {
            BaseResponse<GetRoleInfoListResult> response = new BaseResponse<GetRoleInfoListResult>();
            GetRoleInfoListResult result = new GetRoleInfoListResult();

            try
            {
                var roles = roleRepoitory.GetDatas<HBRole>(t => !t.IsDeleted, true).Select(t => new RoleItem
                {
                    RoleID = t.RoleID,
                    RoleName = t.RoleName
                });

                result.RoleList.AddRange(roles);
                response.Result = result;

                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = e.Message;

                return response;
            }
        }

        public BaseResponse<bool> ChangeUseStatus(ChangeUseStatusParameter parameter)
        {

            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var userInDB = hbUserReosiory.GetDatas<HBUser>(t => !t.IsDeleted && t.UserID == parameter.UserID, true).FirstOrDefault();
                if (null == userInDB)
                {
                    response.IsSuccessful = false;
                    response.Reason = "用户不存在";

                    return response;
                }

                userInDB.UseStatus = parameter.UseStatus;

                OperationResult operationResult = hbUserReosiory.Update<HBUser>(userInDB);
                if (operationResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("修改用户启用状态发生异常");

                }
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = e.Message;
                return response;
            }
        }

        public BaseResponse<bool> DeleteUser(DeleteUserParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var user = hbUserReosiory.GetDatas<HBUser>(t => !t.IsDeleted && t.UserID == parameter.UserID, true).FirstOrDefault();
                if (user == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "待删除的用户不存在";
                    return response;
                }

                if (user.UserID == parameter.CurrentUserID)
                {
                    response.IsSuccessful = false;
                    response.Reason = "不能删除自己";
                    return response;
                }

                user.IsDeleted = true;
                var res = hbUserReosiory.Update<HBUser>(user);
                if (res.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("删除用户异常");
                }

                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = e.Message;

                return response;
            }
        }

        #region 用户管理
        #region 密码重置
        public BaseResponse<bool> ResetPWD(ResetPWDParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var user = hbUserReosiory.GetDatas<HBUser>(t => !t.IsDeleted && t.UserID == parameter.UserID, true).FirstOrDefault();
                if (user == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "用户数据异常";
                    return response;
                }

                user.PWD = MD5Helper.MD5Encrypt64("000000");

                var operRes = hbUserReosiory.Update<HBUser>(user);
                if (operRes.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("数据库操作异常");
                }
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = e.Message;
                return response;
            }
        }
        #endregion
        #endregion

        #region 数据库备份
        public BaseResponse<bool> BackupDB()
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                using (SqlCommand cmdBakRst = new SqlCommand())
                {
                    SqlConnection conn = new SqlConnection(EcanSecurity.Decode(Utilitys.GetAppConfig("iCMS")));
                    //备份文件路径
                    var dir = Utilitys.GetAppConfig("BackupPath");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    //生成备份文件名
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                    var abPath = Path.Combine(dir, fileName);

                    var cmdText = string.Format(@"backup database  HanBinDB to disk='{0}'", abPath);

                    conn.Open();
                    cmdBakRst.Connection = conn;
                    cmdBakRst.CommandType = CommandType.Text;
                    cmdBakRst.CommandText = cmdText;
                    cmdBakRst.ExecuteNonQuery();

                    FileInfo fileInfo = new FileInfo(abPath);
                    var fileSize = fileInfo.Length;

                    #region 插入备份日志
                    BackupLog backupLog = new BackupLog();
                    backupLog.BackupDate = DateTime.Now;
                    backupLog.BackupPath = abPath;

                    backupLog.BackupSize = fileSize;
                    backlogRepository.AddNew<BackupLog>(backupLog);
                    #endregion
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = e.Message;
            }
            return response;
        }

        public BaseResponse<GetBackupLogResult> GetBackupLogList(GetBackupLogParameter parameter)
        {
            BaseResponse<GetBackupLogResult> response = new BaseResponse<GetBackupLogResult>();
            GetBackupLogResult result = new GetBackupLogResult();
            try
            {
                var backuplogs = backlogRepository.GetDatas<BackupLog>(t => !t.IsDeleted, true);

                int total = backuplogs.Count();
                if (parameter.Page > 0)
                {
                    backuplogs = backuplogs
                           .Skip((parameter.Page - 1) * parameter.PageSize)
                           .Take(parameter.PageSize);
                }

                backuplogs.ToList().ForEach(t =>
                {
                    BackupInfo bi = new BackupInfo();
                    bi.BackupDate = t.BackupDate;
                    bi.BackupID = t.ID;
                    bi.BackupPath = t.BackupPath;
                    bi.BackupSize = t.BackupSize;

                    result.BackupList.Add(bi);
                });

                result.Total = total;
                response.Result = result;

                return response;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public BaseResponse<bool> DeleteBackup(DeleteBackupParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var backlog = backlogRepository.GetDatas<BackupLog>(t => !t.IsDeleted, true).FirstOrDefault();
                if (backlog == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "待删除的数据库备份不存在";
                    return response;
                }
                backlog.IsDeleted = true;
                var operRes = backlogRepository.Update<BackupLog>(backlog);
                if (operRes.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("删除备份日志发生异常");
                }

                //删除备份文件
                if (File.Exists(backlog.BackupPath))
                {
                    File.Delete(backlog.BackupPath);
                }

                response.IsSuccessful = true;
                response.Result = true;
                return response;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion
    }
}