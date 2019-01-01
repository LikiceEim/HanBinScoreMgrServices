/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Service.SystemManager
 *文件名：  IRoleManager
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：角色管理
 *
 *修改人：张辽阔
 *修改时间：2016-11-15
 *描述：增加错误编码
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;

using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Base;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Response.SystemManager;
using iCMS.Common.Component.Data.Base.DB;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;

namespace iCMS.Service.Web.SystemManager
{
    #region 角色管理
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManager : IRoleManager
    {
        private readonly IRepository<Role> roleRepository = null;
        private readonly IRepository<User> userRepository = null;
        private readonly IRepository<RoleModule> roleModuleRepository = null;

        public RoleManager(IRepository<Role> roleRepository,IRepository<User> userRepository,IRepository<RoleModule> roleModuleRepository)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.roleModuleRepository = roleModuleRepository;
        }

        #region 查询角色信息
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<RolesDataResult> QueryRoleInfo(QueryRolesInfoParameter Parameter)
        {
            BaseResponse<RolesDataResult> result = new BaseResponse<RolesDataResult>();

            try
            {
                Validate validate = new Validate(userRepository, roleRepository);
                result = validate.ValidateQueryRoleInfoParams(Parameter.Sort, Parameter.Order);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                int count = 0;
                if (Parameter.Page == 0)
                {
                    Parameter.Page = 1;
                }
                using (var dataContext = new iCMSDbContext())
                {
                    IQueryable<iCMS.Frameworks.Core.DB.Models.Role> roleInfoList = null;
                    ListSortDirection sortOrder = Parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(Parameter.Sort, sortOrder),
                        new PropertySortCondition("RoleID", sortOrder),
                    };
                    if (Parameter.IsSuperAdmin)
                    {
                        roleInfoList = dataContext.Role.OrderBy(sortList);
                    }
                    else
                    {
                        roleInfoList = dataContext.Role.Where(t => t.IsShow == 1).OrderBy(sortList);
                    }
                    count = roleInfoList.Count();
                    if (Parameter.Page > -1)
                    {
                        roleInfoList = roleInfoList
                            .Skip((Parameter.Page - 1) * Parameter.PageSize)
                            .Take(Parameter.PageSize);
                    }
                    var tempRoleInfoList = roleInfoList
                        .ToArray()
                        .Select(role =>
                        {
                            var roleModuleObj =
                                (from roleModule in dataContext.RoleModule
                                 join module in dataContext.Module on roleModule.ModuleCode equals module.Code
                                 where roleModule.RoleCode == role.RoleCode && module.IsUsed==1
                                 select new
                                 {
                                     module.Code,
                                     module.ModuleName,
                                 })
                                .ToArray();
                            return new RoleInfo
                            {
                                RoleID = role.RoleID,
                                RoleCode = role.RoleCode,
                                RoleName = role.RoleName,
                                AddDate = role.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                ModuleCode = string.Join("|", roleModuleObj.Select(p => p.Code)),
                                ModuleName = string.Join("|", roleModuleObj.Select(p => p.ModuleName)),
                                IsShow = role.IsShow,
                                IsDeault = role.IsDeault,//王颖辉添加 2016-08-25
                            };
                        })
                        .ToList();
                    result.Result = new RolesDataResult
                    {
                        Total = count,
                        RoleInfo = tempRoleInfoList,
                    };
                    result.IsSuccessful = true;
                    return result;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "003951";
                result.Result = null;
                return result;
            }
        }
        #endregion

        #region 角色添加
        /// <summary>
        /// 角色添加
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：修改IsDeault和IsShow的值
        /// 
        /// 修改人：王龙杰
        /// 修改时间：2017-10-13
        /// 修改记录：添加角色同时添加角色附权
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> AddRoleInfo(AddRoleParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            try
            {
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    Validate validate = new Validate(userRepository, roleRepository);
                    result = validate.ValidateAddRoleParams(Parameter);
                    if (!result.IsSuccessful)
                    {
                        return result;
                    }
                    Role role = new Role()
                    {
                        AddDate = DateTime.Now,
                        RoleName = Parameter.RoleName,
                        RoleCode = Parameter.RoleCode,
                        //张辽阔 2016-11-08 修改
                        IsDeault = Parameter.IsDeault,
                        IsShow = Parameter.IsShow,
                    };
                    OperationResult operationResult = dataContext.Role.AddNew<Role>(dataContext, role);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //添加角色附权 王龙杰 2017-10-13
                        dataContext.Configuration.AutoDetectChangesEnabled = false;
                        var addRoleModuleList =
                            (from p in Parameter.Module
                             select new RoleModule
                             {
                                 RoleCode = Parameter.RoleCode,
                                 ModuleCode = p,
                             });
                        foreach (var item in addRoleModuleList)
                        {
                            dataContext.Entry(item).State = EntityState.Added;
                        }
                        dataContext.SaveChanges();

                        result.IsSuccessful = true;
                        return result;
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Code = "003961";
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "003961";
                return result;
            }
        }
        #endregion

        #region 角色编辑
        /// <summary>
        /// 角色编辑
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：修改IsDeault和IsShow的值
        /// 
        /// 修改人：王龙杰
        /// 修改时间：2017-10-13
        /// 修改记录：编辑角色同时添加角色附权
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditRoleInfo(EditRoleParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            try
            {

                #region 验证角色是否已经被使用，如果已经被使用，则不能进行角色隐藏  王颖辉 2018-01-18
                var role = roleRepository.GetByKey(Parameter.RoleID);
                if (role == null)
                {
                    result.IsSuccessful = false;
                    result.Code = "010092";
                    return result;
                }

                //如果本次修改为隐藏，而且角色被使用，则不能进行隐藏
                if (role.IsShow != Parameter.IsShow && role.IsShow == 1)
                {
                    var isExist = userRepository.GetDatas<User>(item => item.RoleID == role.RoleID, true).Any();
                    if (isExist)
                    {
                        result.IsSuccessful = false;
                        result.Code = "010102";
                        return result;
                    }
                }
              
                #endregion
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    Validate validate = new Validate(userRepository, roleRepository);

                    result = validate.ValidateEditRoleParams(Parameter);
                    if (!result.IsSuccessful)
                    {
                        return result;
                    }
                    Role tempRoleObj = dataContext.Role
                        .GetDatas<Role>(dataContext, p => p.RoleID == Parameter.RoleID)
                        .FirstOrDefault();
                    tempRoleObj.RoleName = Parameter.RoleName;
                    //张辽阔 2016-11-08 添加
                    tempRoleObj.IsDeault = Parameter.IsDeault;
                    tempRoleObj.IsShow = Parameter.IsShow;
                    OperationResult operationResult = dataContext.Role.Update<Role>(dataContext, tempRoleObj);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //修改角色附权 王龙杰 2017-10-13
                        dataContext.Configuration.AutoDetectChangesEnabled = false;
                        var roleModuleList =
                            (from p in dataContext.RoleModule
                             where p.RoleCode == tempRoleObj.RoleCode
                             select p);
                        foreach (var item in roleModuleList)
                        {
                            dataContext.Entry(item).State = EntityState.Deleted;
                        }
                        var addRoleModuleList =
                            (from p in Parameter.Module
                             select new RoleModule
                             {
                                 RoleCode = tempRoleObj.RoleCode,
                                 ModuleCode = p,
                             });
                        foreach (var item in addRoleModuleList)
                        {
                            dataContext.Entry(item).State = EntityState.Added;
                        }
                        dataContext.SaveChanges();

                        result.IsSuccessful = true;
                        return result;
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Code = "003971";
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "003971";
                return result;
            }
        }
        #endregion

        #region 角色删除
        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteRoleInfo(DeleteRoleParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            Dictionary<EntityBase, EntityState> dicOperator = new Dictionary<EntityBase, EntityState>();

            try
            {
                if (Parameter.RoleID.Count==0)
                {
                    result.IsSuccessful = false;
                    result.Code = "003982";
                    return result;
                }

                if (roleRepository
                    .GetDatas<Role>(p => Parameter.RoleID.Contains(p.RoleID)
                        && p.IsDeault == 1, true)
                    .Any())
                {
                    result.IsSuccessful = false;
                    result.Code = "003992";
                    return result;
                }
                if (userRepository
                    .GetDatas<User>(p => p.IsDeleted == false
                        && Parameter.RoleID.Contains(p.RoleID), true)
                    .Any())
                {
                    result.IsSuccessful = false;
                    result.Code = "004002";
                    return result;
                }

                List<Role> roleList = roleRepository
                    .GetDatas<Role>(p => Parameter.RoleID.Contains(p.RoleID), true).ToList();

                foreach (var role in roleList)
                {
                    dicOperator.Add(role, EntityState.Deleted);
                    //修改：王龙杰 2017-09-27 根据RoleCode关联RoleModule 
                    var list = roleModuleRepository
                        .GetDatas<RoleModule>(p => p.RoleCode == role.RoleCode, false);
                    foreach (var item in list)
                    {
                        dicOperator.Add(item, EntityState.Deleted);
                    }
                }

                OperationResult operationResult = roleRepository.TranMethod(dicOperator);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    result.IsSuccessful = true;
                    return result;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Code = "004011";
                    return result;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "004011";
                return result;
            }
        }
        #endregion

        #region 角色赋权
        /// <summary>
        /// 角色赋权
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> RoleAuthorization(RoleAuthorizationParameter Parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();

            try
            {
                if (Parameter.Module == null)
                {
                    Parameter.Module = new List<string>();
                }

                Validate validate = new Validate(userRepository, roleRepository);

                result = validate.ValidateRoleAuthorizationParameter(Parameter.RoleCode);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                using (var dataContext = new iCMSDbContext())
                {
                    dataContext.Configuration.AutoDetectChangesEnabled = false;
                    var roleModuleList =
                        (from p in dataContext.RoleModule
                         where p.RoleCode == Parameter.RoleCode
                         select p);
                    foreach (var item in roleModuleList)
                    {
                        dataContext.Entry(item).State = EntityState.Deleted;
                    }
                    var addRoleModuleList =
                        (from p in Parameter.Module
                         select new RoleModule
                         {
                             RoleCode = Parameter.RoleCode,
                             ModuleCode = p,
                         });
                    foreach (var item in addRoleModuleList)
                    {
                        dataContext.Entry(item).State = EntityState.Added;
                    }
                    dataContext.SaveChanges();
                }

                result.IsSuccessful = true;
                return result;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.IsSuccessful = false;
                result.Code = "004021";
                return result;
            }
        }
        #endregion
    }
    #endregion
}