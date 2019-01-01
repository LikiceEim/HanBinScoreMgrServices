/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：iCMS.Service.SystemManager
 * 文件名：  UserManager
 * 创建人：  LF
 * 创建时间：2016/2/15 10:10:19
 * 描述：用户管理 
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-15
 * 描述：增加错误编码
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Dynamic;
using System.Data.Entity;

using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Base;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Base.DB;
using iCMS.Frameworks.Core.Repository;

namespace iCMS.Service.Web.SystemManager
{
    #region 用户管理
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : IUserManager, IDisposable
    {
        #region 变量
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserRalationDevice> userRalationDeviceRepository;
        private readonly IRepository<UserRelationDiagnoseReport> UserRelationDiagnoseReportRepository;
        private readonly IRepository<UserRelationDeviceAlmRecord> UserRelationDeviceAlmRecordRepository;
        private readonly IRepository<UserRelationWS> UserRelationWSRepository;
        private readonly IRepository<UserRelationWSAlmRecord> UserRelationWSAlmRecordRepository;
        private readonly IRepository<UserRelationMaintainReport> UserRelationMaintainReportRepository;
        private readonly IPaging<User> userPaging;
        private readonly IRepository<Role> roleRepository;
        #endregion

        #region 构造函数
        public UserManager(IRepository<User> userRepository,
                           IRepository<UserRalationDevice> userRalationDeviceRepository,
                           IRepository<UserRelationDiagnoseReport> UserRelationDiagnoseReportRepository,
                           IRepository<UserRelationDeviceAlmRecord> UserRelationDeviceAlmRecordRepository,
                           IRepository<UserRelationWS> UserRelationWSRepository,
                           IRepository<UserRelationWSAlmRecord> UserRelationWSAlmRecordRepository,
                           IRepository<UserRelationMaintainReport> UserRelationMaintainReportRepository,
                           IPaging<User> userPaging,
                           IRepository<Role> roleRepository)
        {
            this.userRepository = userRepository;
            this.userRalationDeviceRepository = userRalationDeviceRepository;
            this.UserRelationDiagnoseReportRepository = UserRelationDiagnoseReportRepository;
            this.UserRelationDeviceAlmRecordRepository = UserRelationDeviceAlmRecordRepository;
            this.UserRelationWSRepository = UserRelationWSRepository;
            this.UserRelationWSAlmRecordRepository = UserRelationWSAlmRecordRepository;
            this.UserRelationMaintainReportRepository = UserRelationMaintainReportRepository;
            this.userPaging = userPaging;
            this.roleRepository = roleRepository;
        }
        #endregion

        #region 获取用户信息数据
        /// <summary>
        /// 获取用户信息数据
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UsersInfoResult> GetUserInfo(QueryUserParameter Parameter)
        {
            BaseResponse<UsersInfoResult> backObj = new BaseResponse<UsersInfoResult>();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);

                backObj = validate.ValidateGetUsersDataParams(Parameter.Sort, Parameter.Order);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }

                switch (Parameter.Sort)
                {
                    case "RoleName":
                        Parameter.Sort = "RoleID";
                        break;
                }

                int count = 0;
                if (Parameter.Page == 0)
                {
                    Parameter.Page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    IQueryable<User> userInfoList = dataContext.Users.Where(p => p.IsDeleted == false);
                    ListSortDirection sortOrder = Parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(Parameter.Sort, sortOrder),
                        new PropertySortCondition("UserID", sortOrder),
                    };
                    if (!Parameter.IsSuperAdmin)
                    {
                        userInfoList = userInfoList.Where(p => p.IsShow == 1).OrderBy(sortList);
                    }
                    else
                    {
                        userInfoList = userInfoList.OrderBy(sortList);
                    }
                    count = userInfoList.Count();
                    if (Parameter.Page > -1)
                    {
                        userInfoList = userInfoList
                            .Skip((Parameter.Page - 1) * Parameter.PageSize)
                            .Take(Parameter.PageSize);
                    }

                    var tempUserInfoList = userInfoList
                        .ToArray()
                        .Select(user =>
                        {
                            Role roleNameObj = dataContext.Role.FirstOrDefault(role => role.RoleID == user.RoleID);
                            var userManageObj =
                                (from userDevice in dataContext.UserRalationDevice
                                 join device in dataContext.Device on userDevice.DevId equals device.DevID
                                 where userDevice.UserID == user.UserID
                                 select new
                                 {
                                     device.DevID,
                                     device.DevName,
                                 })
                                .ToArray();
                            return new UserInfo
                            {
                                UserID = user.UserID,
                                RoleID = user.RoleID,
                                RoleName = roleNameObj == null ? "" : roleNameObj.RoleName,
                                UserName = user.UserName,
                                AccountName = user.AccountName,
                                Email = user.Email,
                                LoginCount = user.LoginCount,
                                LastLoginDate = user.LastLoginDate.HasValue ? user.LastLoginDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                                Phone = user.Phone,
                                AddDate = user.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                IsShow = user.IsShow,
                                UserManagedDevs = string.Join("|", userManageObj.Select(p => p.DevID)),
                                UserManagedDevsName = string.Join("|", userManageObj.Select(p => p.DevName)),
                            };
                        })
                        .ToList();
                    backObj.Result = new UsersInfoResult
                    {
                        Total = count,
                        UserInfo = tempUserInfoList
                    };
                    backObj.IsSuccessful = true;
                    backObj.Code = null;
                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.Code = "004031";
                backObj.IsSuccessful = false;
                backObj.Result = null;

                return backObj;
            }
        }
        #endregion

        #region 添加用户信息
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<AddUserResult> AddUser(UserParameter Parameter)
        {
            BaseResponse<AddUserResult> backObj = new BaseResponse<AddUserResult>();
            AddUserResult result = new AddUserResult();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);
                backObj = validate.ValidateAddUserParams(Parameter);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }

                User newUser = new User
                {
                    AddDate = System.DateTime.Now,
                    Email = Parameter.Email,
                    IsDeleted = false,
                    IsShow = Parameter.IsShow,
                    Phone = Parameter.Phone,
                    PSW = Parameter.PassWord,
                    RoleID = Parameter.RoleID,
                    AccountName = Parameter.AccountName,
                    UserName = Parameter.UserName
                };
                OperationResult operationresult = userRepository.AddNew<User>(newUser);
                if (operationresult.ResultType == EnumOperationResultType.Success)
                {
                    result.UserID = newUser.UserID;
                    backObj.Code = null;
                    backObj.IsSuccessful = true;
                    backObj.Result = result;

                    return backObj;
                }
                else
                {
                    backObj.Code = "004041";
                    backObj.IsSuccessful = false;

                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.Code = "004041";
                backObj.IsSuccessful = false;

                return backObj;
            }
        }
        #endregion

        #region 编辑用户信息
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditUser(UserParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);

                backObj = validate.ValidateEditUserParams(Parameter);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }
                User tempUserObj = userRepository.GetDatas<User>(p => p.UserID == Parameter.UserID, true).FirstOrDefault();
                tempUserObj.UserName = Parameter.UserName;
                tempUserObj.RoleID = Parameter.RoleID;
                tempUserObj.Email = Parameter.Email;
                tempUserObj.Phone = Parameter.Phone;
                tempUserObj.IsShow = Parameter.IsShow;
                tempUserObj.AccountName = Parameter.AccountName;
                OperationResult result = userRepository.Update<User>(tempUserObj);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    backObj.Code = null;
                    backObj.IsSuccessful = true;
                    backObj.Result = true;

                    return backObj;
                }
                else
                {
                    backObj.Code = "004051";
                    backObj.IsSuccessful = false;
                    backObj.Result = false;
                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.Code = "004051";
                backObj.IsSuccessful = false;
                backObj.Result = false;
                return backObj;
            }
        }
        #endregion

        #region 删除用户信息，支持批量删除
        /// <summary>
        /// 删除用户信息，支持批量删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteUsers(DeleteUserParameter Parameter)
        {
            Dictionary<EntityBase, EntityState> dicOperator = new Dictionary<EntityBase, EntityState>();
            BaseResponse<bool> backObj = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);

                backObj = validate.ValidateDeleteUserParams(Parameter.UsersID, Parameter.CurrentUserID);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }

                Parameter.UsersID.RemoveAll(p => p == Parameter.CurrentUserID);
                if (Parameter.UsersID.Any())
                {
                    var userList = userRepository
                        .GetDatas<User>(p => Parameter.UsersID.Contains(p.UserID), true)
                        .ToArray();
                    foreach (var item in userList)
                    {
                        item.IsDeleted = true;
                        dicOperator.Add(item, EntityState.Modified);
                    }
                    //删除 用户未读管理设备权限
                    var ralationDeviceList = userRalationDeviceRepository
                        .GetDatas<UserRalationDevice>(p => Parameter.UsersID.Contains(p.UserID.HasValue ? p.UserID.Value : 0), false);
                    foreach (var item in ralationDeviceList)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                    //删除 用户未确认管理传感器权限
                    var userRelationWS = UserRelationWSRepository.
                        GetDatas<UserRelationWS>(p => Parameter.UsersID.Contains(p.UserID), false);
                    foreach (var item in userRelationWS)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                    //删除 用户未确认设备报警记录
                    var userRelationDeviceAlmRecord = UserRelationDeviceAlmRecordRepository.
                        GetDatas<UserRelationDeviceAlmRecord>(p => Parameter.UsersID.Contains(p.UserID), false);
                    foreach (var item in userRelationDeviceAlmRecord)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                    //删除 用户未确认传感器、网关报警记录
                    var userRelationWSAlmRecord = UserRelationWSAlmRecordRepository.
                        GetDatas<UserRelationWSAlmRecord>(p => Parameter.UsersID.Contains(p.UserID), false);
                    foreach (var item in userRelationWSAlmRecord)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                    //删除 用户未读诊断报告记录
                    var userRelationDiagnoseReport = UserRelationDiagnoseReportRepository.
                        GetDatas<UserRelationDiagnoseReport>(p => Parameter.UsersID.Contains(p.UserID), false);
                    foreach (var item in userRelationDiagnoseReport)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                    //删除 用户未读维修日志记录
                    var userRelationMaintainReport = UserRelationMaintainReportRepository.
                        GetDatas<UserRelationMaintainReport>(p => Parameter.UsersID.Contains(p.UserID), false);
                    foreach (var item in userRelationMaintainReport)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }

                    OperationResult result = userRepository.TranMethod(dicOperator);
                    if (result.ResultType == EnumOperationResultType.Success)
                    {
                        backObj.Code = null;
                        backObj.IsSuccessful = true;
                        backObj.Result = true;

                        return backObj;
                    }
                    else
                    {
                        backObj.Code = "004061";
                        backObj.IsSuccessful = false;
                        backObj.Result = false;
                        return backObj;
                    }
                }
                else
                {
                    backObj.Code = null;
                    backObj.IsSuccessful = true;
                    backObj.Result = false;
                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.Code = "004061";
                backObj.IsSuccessful = true;
                backObj.Result = false;
                return backObj;
            }
        }
        #endregion

        #region 重置用户密码，支持批量重置
        /// <summary>
        /// 重置用户密码，支持批量重置
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> ResetUserPassWord(RsetUserPassWordParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);

                backObj = validate.ValidateResetPassWordParams(Parameter.UsersID, Parameter.NewPassWord);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }

                using (var dataContext = new iCMSDbContext())
                {
                    dataContext.Configuration.AutoDetectChangesEnabled = false;
                    var updateUser =
                        (from p in dataContext.Users
                         where Parameter.UsersID.Contains(p.UserID)
                         select p)
                        .ToArray();
                    foreach (var item in updateUser)
                    {
                        item.PSW = Parameter.NewPassWord;
                        dataContext.Entry(item).State = EntityState.Modified;
                    }
                    dataContext.SaveChanges();
                }
                backObj.IsSuccessful = true;
                return backObj;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.IsSuccessful = false;
                backObj.Code = "004071";
                return backObj;
            }
        }
        #endregion

        #region 用户管理设备范围（不再使用）
        /// <summary>
        /// 用户管理设备范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> JurisdictionOfDevices(JurisdictionOfDevicesParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();

            try
            {
                if (Parameter.DevicesID == null)
                {
                    Parameter.DevicesID = new List<int>();
                }

                Validate validate = new Validate(userRepository, roleRepository);

                backObj = validate.ValidateUserManagedDevRangeParams(Parameter.UserID);
                if (!backObj.IsSuccessful)
                {
                    return backObj;
                }

                using (var dataContext = new iCMSDbContext())
                {
                    dataContext.Configuration.AutoDetectChangesEnabled = false;
                    var userRalationDeviceList =
                        (from p in dataContext.UserRalationDevice
                         where p.UserID == Parameter.UserID
                         select p);
                    foreach (var item in userRalationDeviceList)
                    {
                        dataContext.Entry(item).State = EntityState.Deleted;
                    }
                    var addUserRalationDeviceList =
                        from p in Parameter.DevicesID
                        select new UserRalationDevice
                        {
                            UserID = Parameter.UserID,
                            DevId = p,
                        };
                    foreach (var item in addUserRalationDeviceList)
                    {
                        dataContext.Entry(item).State = EntityState.Added;
                    }
                    dataContext.SaveChanges();
                }
                backObj.IsSuccessful = true;
                return backObj;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.IsSuccessful = false;
                backObj.Code = "004081";
                return backObj;
            }
        }
        #endregion

        #region 获取用户管理设备、传感器范围
        /// <summary>
        /// 获取用户管理设备、传感器范围
        /// 修改人：QXM
        /// 修改时间：2018/05/10
        /// 修改记录：关联范围的SERVER根节点变为 WIRELESS 和 WIRED
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<UserDevManagedResult> UserDevManagedRange(UserIDParameter Parameter)
        {
            BaseResponse<UserDevManagedResult> backObj = new BaseResponse<UserDevManagedResult>();
            List<UserRelated> deviceTree = new List<UserRelated>();

            //无线监测树集合
            List<UserRelated> wirelessWSTree = new List<UserRelated>();

            //有线监测树集合
            List<UserRelated> wiredWSTree = new List<UserRelated>();

            int UserID = Parameter.UserID;

            try
            {
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    #region 监测树
                    int? maxMTID = dataContext.MonitorTree.Select(s => s.MonitorTreeID).Max();
                    if (maxMTID.HasValue)
                    {
                        UserRelated rootMT = new UserRelated()
                        {
                            ID = 0,
                            PID = 0
                        };
                        deviceTree.Add(rootMT);

                        var DeviceList =
                            (from d in dataContext.Device
                             select new UserRelated
                             {
                                 ID = maxMTID.Value + d.DevID,
                                 PID = d.MonitorTreeID,
                                 Name = d.DevName,
                                 Type = "DEVICE",
                                 RecordID = d.DevID,
                                 isRelated = (from urd in dataContext.UserRalationDevice
                                              where urd.UserID == Parameter.UserID && d.DevID == urd.DevId
                                              select urd.DevId).Any()
                             })
                            .ToList();

                        deviceTree.AddRange(DeviceList);
                        List<MonitorTreeType> mtType = dataContext.MonitorTreeType
                            .Where(w => w.IsUsable == 1)
                            .OrderByDescending(o => o.Describe)
                            .ToList();
                        if (mtType.Count > 0)
                        {
                            List<int> DevicePID = DeviceList.Select(s => s.PID).ToList();
                            int leafMTID = mtType.First().ID;
                            var leafMTList =
                                (from mt in dataContext.MonitorTree
                                 where mt.Type == leafMTID && DevicePID.Contains(mt.MonitorTreeID)
                                 select new
                                 {
                                     ID = mt.MonitorTreeID,
                                     Pid = mt.PID,
                                     Name = mt.Name,
                                     Type = mt.Type,
                                     RecordID = mt.MonitorTreeID,
                                     isRelated = false
                                 }).ToList().Select(s => new UserRelated
                                 {
                                     ID = s.ID,
                                     PID = s.Pid,
                                     Name = s.Name,
                                     Type = s.Type.ToString(),
                                     RecordID = s.RecordID,
                                     isRelated = false
                                 }).ToList();
                            deviceTree.AddRange(leafMTList);
                            mtType.RemoveAt(0);
                            foreach (var item in mtType)
                            {
                                int tempTypeID = item.ID;
                                List<int> tempMTPID = leafMTList.Select(s => s.PID).ToList();
                                List<UserRelated> tempMTList =
                                    (from mt in dataContext.MonitorTree
                                     where mt.Type == tempTypeID && tempMTPID.Contains(mt.MonitorTreeID)
                                     select new
                                     {
                                         ID = mt.MonitorTreeID,
                                         Pid = mt.PID,
                                         Name = mt.Name,
                                         Type = mt.Type,
                                         RecordID = mt.MonitorTreeID,
                                         isRelated = false
                                     })
                                     .ToList()
                                     .Select(s => new UserRelated
                                    {
                                        ID = s.ID,
                                        PID = s.Pid,
                                        Name = s.Name,
                                        Type = s.Type.ToString(),
                                        RecordID = s.RecordID,
                                        isRelated = false
                                    })
                                    .ToList();
                                deviceTree.AddRange(tempMTList);
                                leafMTList.Clear();
                                leafMTList.AddRange(tempMTList);
                            }
                        }
                    }
                    #endregion

                    #region WirelessWS树
                    UserRelated wirelessNode = new UserRelated()
                    {
                        ID = 10000,
                        PID = 0,
                        Name = "无线",
                        Type = "WIRELESS",
                        RecordID = 0,
                        isRelated = false
                    };
                    wirelessWSTree.Add(wirelessNode);

                    //找到无线网关最大ID
                    int? maxWirelessWGID = dataContext.WG
                        .Where(s => s.DevFormType == (int)EnumDevFormType.SingleBoard
                            || s.DevFormType == (int)EnumDevFormType.iWSN)
                        .Select(s => s.WGID)
                        .Max();
                    if (maxWirelessWGID.HasValue)
                    {
                        var WSList =
                            (from ws in dataContext.WS
                             where ws.DevFormType == (int)EnumWSFormType.WireLessSensor
                                || ws.DevFormType == (int)EnumWSFormType.Triaxial
                             select ws)
                             .DistinctBy(t => t.MACADDR)
                             .Select(t => new UserRelated
                             {
                                 ID = maxWirelessWGID.Value + t.WSID,
                                 PID = t.WGID,
                                 Name = t.WSName,
                                 Type = "WIRELESS_SENSOR",
                                 RecordID = t.WSID,
                                 isRelated = (from urws in dataContext.UserRelationWS
                                              where urws.UserID == Parameter.UserID && t.WSID == urws.WSID
                                              select urws.WSID).Any()
                             })
                             .ToList();
                        wirelessWSTree.AddRange(WSList);
                        List<int> tempWGID = WSList.Select(s => s.PID).Distinct().ToList();
                        var wgList =
                            (from wg in dataContext.WG
                             where tempWGID.Contains(wg.WGID)
                             select new UserRelated
                             {
                                 ID = wg.WGID,
                                 PID = wirelessNode.ID,
                                 Name = wg.WGName,
                                 Type = "WIRELESS_GATE",
                                 RecordID = wg.WGID,
                                 isRelated = false
                             })
                             .ToList();
                        wirelessWSTree.AddRange(wgList);
                    }

                    #endregion

                    #region WiredWS树 Added by QXM, 2018/05/10
                    UserRelated wiredNode = new UserRelated()
                    {
                        ID = 10001,
                        PID = 0,
                        Name = "有线",
                        Type = "WIRED",
                        RecordID = 0,
                        isRelated = false
                    };
                    wiredWSTree.Add(wiredNode);

                    if (dataContext.WG.Where(t => t.DevFormType == (int)EnumDevFormType.Wired).Any())
                    {
                        int maxWiredWGID = dataContext.WG
                            .Where(t => t.DevFormType == (int)EnumDevFormType.Wired)
                            .Select(t => t.WGID)
                            .Max();
                        var wiredWSList =
                            (from ws in dataContext.WS
                             where ws.DevFormType == (int)EnumWSFormType.WiredSensor
                             select new UserRelated
                             {
                                 ID = maxWiredWGID + ws.WSID,
                                 PID = ws.WGID,
                                 Name = ws.WSName,
                                 Type = "WIRED_SENSOR",
                                 RecordID = ws.WSID,
                                 isRelated = (from urws in dataContext.UserRelationWS
                                              where urws.UserID == Parameter.UserID && ws.WSID == urws.WSID
                                              select urws.WSID).Any()
                             })
                            .ToList();
                        wiredWSTree.AddRange(wiredWSList);

                        List<int> tempWiredWGID = wiredWSList
                            .Select(s => s.PID).Distinct()
                            .ToList();
                        var wiredWGList =
                            (from wg in dataContext.WG
                             where tempWiredWGID.Contains(wg.WGID)
                             select new UserRelated
                             {
                                 ID = wg.WGID,
                                 PID = wiredNode.ID,
                                 Name = wg.WGName,
                                 Type = "WIRED_GATE",
                                 RecordID = wg.WGID,
                                 isRelated = false
                             })
                            .ToList();
                        wiredWSTree.AddRange(wiredWGList);
                    }
                    #endregion

                    backObj.Result = new UserDevManagedResult
                    {
                        DeviceTree = deviceTree,
                        WireLessWSTree = wirelessWSTree,
                        WiredWSTree = wiredWSTree
                    };
                    backObj.IsSuccessful = true;
                    backObj.Code = null;
                    return backObj;
                }
            }
            catch (Exception ex)
            {
                //获取用户管理设备、传感器范围出错
                LogHelper.WriteLog(ex);
                backObj.IsSuccessful = false;
                backObj.Code = "009291";
                return backObj;
            }
        }

        #endregion

        #region 设置用户管理设备、传感器范围
        /// <summary>
        /// 设置用户管理设备、传感器范围
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> SetUserManagedRange(UserDevManagedParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();
            List<int> newDeviceID = Parameter.DeviceID == null ? new List<int>() : Parameter.DeviceID;
            List<int> newWSID = Parameter.WSID == null ? new List<int>() : Parameter.WSID;
            List<int> newWGID = new List<int>();

            try
            {
                return ExecuteDB.ExecuteTrans((dataContext) =>
                {
                    dataContext.Configuration.AutoDetectChangesEnabled = false;
                    //验证UserID
                    if (!dataContext.Users.Where(w => w.UserID == Parameter.UserID).Any())
                    {
                        backObj.IsSuccessful = false;
                        backObj.Code = "009462";
                        return backObj;
                    }

                    //原设备、传感器管理权限
                    List<int> oldDeviceID = (from urd in dataContext.UserRalationDevice
                                             where urd.UserID == Parameter.UserID
                                             select urd.DevId.Value).ToList();
                    List<int> oldWSID = (from urws in dataContext.UserRelationWS
                                         where urws.UserID == Parameter.UserID
                                         select urws.WSID).ToList();
                    List<int> oldWGID = (from urws in dataContext.UserRelationWS
                                         join ws in dataContext.WS on urws.WSID equals ws.WSID
                                         join wg in dataContext.WG on ws.WGID equals wg.WGID
                                         where urws.UserID == Parameter.UserID
                                         select wg.WGID).ToList();

                    #region 设置设备权限
                    dataContext.UserRalationDevice.Delete(dataContext, d => d.UserID == Parameter.UserID);
                    if (newDeviceID.Count > 0)
                    {
                        var addUserRalationDeviceList =
                        (from p in newDeviceID
                         select new UserRalationDevice
                         {
                             UserID = Parameter.UserID,
                             DevId = p,
                         }).ToList();
                        dataContext.UserRalationDevice.AddNew(dataContext, addUserRalationDeviceList);
                    }
                    #endregion

                    #region 设置WS权限
                    var userRalationWSList =
                        (from p in dataContext.UserRelationWS
                         where p.UserID == Parameter.UserID
                         select p);
                    dataContext.UserRelationWS.Delete(dataContext, d => d.UserID == Parameter.UserID);
                    if (newWSID.Count > 0)
                    {
                        var addUserRalationWSList =
                        (from p in newWSID
                         select new UserRelationWS
                         {
                             UserID = Parameter.UserID,
                             WSID = p,
                         }).ToList();
                        dataContext.UserRelationWS.AddNew(dataContext, addUserRalationWSList);
                    }
                    #endregion

                    #region 刷新用户未读诊断报告/维修日志关联记录、用户未确认报警提醒关联
                    List<int> deleteDeviceID = new List<int>(); //删除的管理设备
                    List<int> addDeviceID = new List<int>();    //新增的管理设备
                    List<int> deleteWSID = new List<int>();     //删除的管理传感器
                    List<int> addWSID = new List<int>();        //新增的管理传感器
                    List<int> deleteWGID = new List<int>();     //删除的管理网关
                    List<int> addWGID = new List<int>();        //新增的管理网关

                    newWGID = dataContext.WS.Where(w => newWSID.Contains(w.WSID)).Select(s => s.WGID).Distinct().ToList();
                    oldWGID = dataContext.WS.Where(w => oldWSID.Contains(w.WSID)).Select(s => s.WGID).Distinct().ToList();

                    #region 计算变化的管理权限

                    deleteDeviceID.AddRange((from d in oldDeviceID where !newDeviceID.Contains(d) select d).ToList());
                    addDeviceID.AddRange((from d in newDeviceID where !oldDeviceID.Contains(d) select d).ToList());
                    deleteWSID.AddRange((from d in oldWSID where !newWSID.Contains(d) select d).ToList());
                    addWSID.AddRange((from d in newWSID where !oldWSID.Contains(d) select d).ToList());
                    deleteWGID.AddRange((from d in oldWGID where !newWGID.Contains(d) select d).ToList());
                    addWGID.AddRange((from d in newWGID where !oldWGID.Contains(d) select d).ToList());

                    #endregion

                    if (deleteDeviceID.Count > 0)
                    {
                        //删除诊断报告
                        dataContext.UserRelationDiagnoseReport.Delete(dataContext,
                                d => d.UserID == Parameter.UserID && deleteDeviceID.Contains(d.DeviceID));
                        //删除设备维修日志
                        dataContext.UserRelationMaintainReport.Delete(dataContext,
                                d => d.ReportType == 1 && d.UserID == Parameter.UserID && deleteDeviceID.Contains(d.DeviceID));
                        //删除设备报警记录
                        var e = dataContext.UserRelationDeviceAlmRecord.Delete(dataContext,
                                d => d.UserID == Parameter.UserID && deleteDeviceID.Contains(d.DeviceID));
                    }
                    if (deleteWSID.Count > 0)
                    {
                        //删除传感器维修日志
                        dataContext.UserRelationMaintainReport.Delete(dataContext,
                                d => d.ReportType == 2 && d.UserID == Parameter.UserID && deleteWSID.Contains(d.DeviceID));
                        //删除传感器报警记录
                        var e = dataContext.UserRelationWSAlmRecord.Delete(dataContext,
                                d => d.MonitorDeviceType == 2 && d.UserID == Parameter.UserID && deleteWSID.Contains(d.MonitorDeviceID));
                    }
                    if (deleteWGID.Count > 0)
                    {
                        //删除网关维修日志
                        dataContext.UserRelationMaintainReport.Delete(dataContext,
                                d => d.ReportType == 3 && d.UserID == Parameter.UserID && deleteWGID.Contains(d.DeviceID));
                        //删除网关报警记录
                        var e = dataContext.UserRelationWSAlmRecord.Delete(dataContext,
                                d => d.MonitorDeviceType == 1 && d.UserID == Parameter.UserID && deleteWGID.Contains(d.MonitorDeviceID));
                    }
                    if (addDeviceID.Count > 0)
                    {
                        List<UserRelationDiagnoseReport> list = new List<UserRelationDiagnoseReport>();
                        var tempDiagnoseReport = dataContext.DiagnoseReport.Where(w => addDeviceID.Contains(w.DeviceID)
                                                                                  && w.IsDeleted == false && w.IsTemplate == false).
                            Select(s => new
                            {
                                DeviceID = s.DeviceID,
                                UpdateDate = s.UpdateDate,
                                DiagnoseReportID = s.DiagnoseReportID
                            }).ToList();
                        foreach (var item in tempDiagnoseReport)
                        {
                            UserRelationDiagnoseReport userRelationDiagnoseReport = new UserRelationDiagnoseReport();
                            userRelationDiagnoseReport.DeviceID = item.DeviceID;
                            userRelationDiagnoseReport.DiangoseReportAddDate = item.UpdateDate;
                            userRelationDiagnoseReport.DiangoseReportID = item.DiagnoseReportID;
                            userRelationDiagnoseReport.UserID = Parameter.UserID;

                            list.Add(userRelationDiagnoseReport);
                        }
                        dataContext.UserRelationDiagnoseReport.AddNew(dataContext, list);
                    }
                    if (addDeviceID.Count > 0 || addWSID.Count > 0 || addWGID.Count > 0)
                    {
                        List<UserRelationMaintainReport> list = new List<UserRelationMaintainReport>();
                        var tempMaintainReport = dataContext.MaintainReport.Where(w =>
                                             w.IsDeleted == false && w.IsTemplate == false &&
                                            ((w.ReportType == 1 && addDeviceID.Contains(w.DeviceID)) ||
                                             (w.ReportType == 2 && addWSID.Contains(w.DeviceID)) ||
                                             (w.ReportType == 3 && addWGID.Contains(w.DeviceID)))
                                            ).Select(s => new
                                            {
                                                ReportType = s.ReportType,
                                                DeviceID = s.DeviceID,
                                                UpdateDate = s.UpdateDate,
                                                MaintainReportID = s.MaintainReportID
                                            }).ToList();
                        foreach (var item in tempMaintainReport)
                        {
                            UserRelationMaintainReport userRelationMaintainReport = new UserRelationMaintainReport();
                            userRelationMaintainReport.ReportType = item.ReportType;
                            userRelationMaintainReport.DeviceID = item.DeviceID;
                            userRelationMaintainReport.MaintainReportAddDate = item.UpdateDate;
                            userRelationMaintainReport.MaintainReportID = item.MaintainReportID;
                            userRelationMaintainReport.UserID = Parameter.UserID;

                            list.Add(userRelationMaintainReport);
                        }
                        dataContext.UserRelationMaintainReport.AddNew(dataContext, list);
                    }
                    #endregion

                    backObj.IsSuccessful = true;
                    return backObj;
                });
            }
            catch (Exception ex)
            {
                //设置用户管理设备、传感器范围出错
                LogHelper.WriteLog(ex);
                backObj.IsSuccessful = false;
                backObj.Code = "009301";
                return backObj;
            }
        }

        #endregion

        #region 登陆验证
        /// <summary>
        /// 登陆验证
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：用户登录成功后修改最后登录时间和登陆次数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<UserLoginResult> Login(LoginParameter param)
        {
            BaseResponse<UserLoginResult> response = new BaseResponse<UserLoginResult>();
            try
            {
                var user = userRepository
                    .GetDatas<User>(t => t.AccountName.Trim().Equals(param.AccountName)
                        && t.PSW.Trim().Equals(param.PSW) && t.IsDeleted == false, true)
                    .SingleOrDefault();
                if (user != null)
                {
                    response.IsSuccessful = true;
                    response.Code = string.Empty;
                    UserLoginResult userLoginResult = new UserLoginResult
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        RoleID = user.RoleID,
                        RoleCode = roleRepository.GetByKey(user.RoleID).RoleCode,
                        LoginCount = user.LoginCount,
                        Email = user.Email
                    };

                    //存储登陆成功后返回数据
                    response.Result = userLoginResult;

                    //张辽阔 2016-11-08 添加
                    user.LastLoginDate = DateTime.Now;
                    user.LoginCount += 1;
                    OperationResult result = userRepository.Update(user);
                    if (result.ResultType != EnumOperationResultType.Success)
                    {
                        response.IsSuccessful = false;
                        response.Code = string.Empty;
                        response.Result = null;
                    }
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = string.Empty;
                    response.Result = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = string.Empty;
                response.Result = null;

                return response;
            }
        }
        #endregion

        #region 用户密码设置
        public BaseResponse<bool> ResetPSW(ResetPSWParameter param)
        {
            BaseResponse<bool> respose = new BaseResponse<bool>();
            try
            {
                Validate validate = new Validate(userRepository, roleRepository);

                dynamic validateResult = validate.ValidatePSWResetForUserParams(param);
                if (validateResult.Result)
                {
                    respose.IsSuccessful = false;
                    respose.Code = validateResult.Message;
                    return respose;
                    //return Json.JsonSerializer<UserOperationResult>(new UserOperationResult { Result = false, Reason = validateResult.Message });
                }

                OperationResult result = userRepository.Update<User>(validateResult.UserEntity as User);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    respose.IsSuccessful = true;
                    respose.Code = "";
                    respose.Result = true;
                    return respose;
                    //return Json.JsonSerializer<UserOperationResult>(new UserOperationResult { Result = true, Reason = "" });
                }
                else
                {
                    respose.IsSuccessful = false;
                    respose.Code = "004091";
                    respose.Result = false;
                    return respose;
                    //return Json.JsonSerializer<UserOperationResult>(new UserOperationResult { Result = false, Reason = "#201030075001" });
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                respose.IsSuccessful = false;
                respose.Code = "004091";
                respose.Result = false;
                return respose;
                //return Json.JsonSerializer<UserOperationResult>(new UserOperationResult { Result = false, Reason = "#201030075001" });
            }
        }
        #endregion

        #region 释放
        public void Dispose()
        {

        }
        #endregion

        #region 验证“修改用户密码”的参数是否有效
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-30
        /// 创建记录：验证“修改用户密码”的参数是否有效
        /// </summary>
        /// <param name="pswJSONStr">修改密码JSON字符串</param>
        /// <returns></returns>
        internal dynamic ValidatePSWResetForUserParams(ResetPSWParameter param)
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
                result.Message = "004102";
                return result;
            }
            if (string.IsNullOrEmpty(param.OldPSW))
            {
                result.Result = true;
                result.Message = "004112";
                return result;
            }
            if (string.IsNullOrEmpty(param.NewPSW))
            {
                result.Result = true;
                result.Message = "004122";
                return result;
            }
            User userObj = userRepository.GetDatas<User>(p => p.UserID == param.UserID, true).FirstOrDefault();
            if (userObj == null)
            {
                result.Result = true;
                result.Message = "004102";
                return result;
            }
            if (userObj.PSW != param.OldPSW)
            {
                result.Result = true;
                result.Message = "004132";
                return result;
            }

            userObj.PSW = param.NewPSW;

            result.Result = false;
            result.UserEntity = userObj;
            return result;
        }
        #endregion
    }
    #endregion
}