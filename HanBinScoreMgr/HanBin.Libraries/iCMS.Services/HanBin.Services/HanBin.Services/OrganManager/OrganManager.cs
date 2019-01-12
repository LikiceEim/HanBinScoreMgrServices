using HanBin.Core.DB.Models;
using HanBin.Common.Component.Data.Base;

using HanBin.Common.Component.Data.Request.HanBin.OrganManage;
using HanBin.Common.Component.Data.Response.HanBinOrganManager;
using HanBin.Common.Component.Tool;
using HanBin.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Service.Common;
using HanBin.Common.Commonent.Data.Enum;

namespace HanBin.Services.OrganManager
{
    public class OrganManager : IOrganManager
    {
        [Dependency]
        public IRepository<Organization> organRepository { get; set; }

        [Dependency]
        public IRepository<Officer> officerRepository { get; set; }

        [Dependency]
        public IRepository<OrganCategory> organCategoryRepository { get; set; }

        [Dependency]
        public IRepository<OrganType> organTypeRepository { get; set; }

        [Dependency]
        public IRepository<OfficerPositionType> officerPositionRepository { get; set; }
        [Dependency]
        public IRepository<OfficerLevelType> officerLevelRepository { get; set; }

        [Dependency]
        public IRepository<Area> areaRepository { get; set; }

        public OrganManager()
        {
            organRepository = new Repository<Organization>();
            officerRepository = new Repository<Officer>();
            organCategoryRepository = new Repository<OrganCategory>();
            organTypeRepository = new Repository<OrganType>();
            officerPositionRepository = new Repository<OfficerPositionType>();
            officerLevelRepository = new Repository<OfficerLevelType>();
            areaRepository = new Repository<Area>();
        }

        #region 添加单位
        public BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                #region 输入验证
                if (string.IsNullOrEmpty(param.OrganCode))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位编码不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(param.OrganFullName))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位全称不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(param.OrganShortName))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位简称不能为空";
                    return response;
                }

                var isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganCode) && t.OrganCode.Equals(param.OrganCode), true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位编码已存在";
                    return response;
                }

                isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganFullName) && t.OrganFullName.Equals(param.OrganFullName), true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位全称已存在";
                    return response;
                }

                isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganShortName) && t.OrganShortName.Equals(param.OrganShortName), true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位简称已存在";
                    return response;
                }
                #endregion

                Organization organ = new Organization();
                organ.OrganCode = param.OrganCode;
                organ.OrganFullName = param.OrganFullName;
                organ.OrganShortName = param.OrganShortName;
                organ.OrganTypeID = param.OrganTypeID;

                organ.AreaID = param.AreaID;
                organ.AddUserID = param.AddUserID;
                organ.LastUpdateDate = DateTime.Now;
                organ.LastUpdateUserID = param.AddUserID;

                var operResult = organRepository.AddNew<Organization>(organ);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位添加发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;

                #region 操作日志
                new LogManager().AddOperationLog(param.CurrentUserID, "添加部门");
                #endregion

                return response;

            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                response.Reason = e.Message;

                return response;
            }
        }
        #endregion

        #region 获取单位详细信息
        public BaseResponse<GetOrganDetailInfoResult> GetOrganDetailInfo(GetOrganDetailInfoParameter parameter)
        {
            BaseResponse<GetOrganDetailInfoResult> response = new BaseResponse<GetOrganDetailInfoResult>();
            GetOrganDetailInfoResult result = new GetOrganDetailInfoResult();
            try
            {
                var organ = organRepository.GetDatas<Organization>(t => t.OrganID == parameter.OrganID && !t.IsDeleted, true).FirstOrDefault();
                if (organ == null)
                {
                    throw new Exception("获取单位信息数据异常！");
                }
                result.OrganID = organ.OrganID;
                result.OrganCode = organ.OrganCode;
                result.OrganFullName = organ.OrganFullName;
                result.OrganShortName = organ.OrganShortName;
                result.OrganTypeID = organ.OrganTypeID;
                var organType = organTypeRepository.GetDatas<OrganType>(t => t.OrganTypeID == organ.OrganTypeID && !t.IsDeleted, true).FirstOrDefault();
                if (organType != null)
                {
                    result.OrganTypeName = organType.OrganTypeName;
                    var organCategory = organCategoryRepository.GetDatas<OrganCategory>(t => t.CategoryID == organType.CategoryID && !t.IsDeleted, true).FirstOrDefault();
                    if (organCategory != null)
                    {
                        result.OrganCategoryID = organCategory.CategoryID;
                        result.OrganCategoryName = organCategory.CategoryName;
                    }
                }

                //获取单位下的干部
                var officerInfo = officerRepository.GetDatas<Officer>(t => t.OrganizationID == organ.OrganID && !t.IsDeleted, true).ToArray().Select(t =>
                {
                    var level = officerLevelRepository.GetDatas<OfficerLevelType>(l => !l.IsDeleted && l.LevelID == t.LevelID, true).FirstOrDefault();
                    var position = officerPositionRepository.GetDatas<OfficerPositionType>(p => !p.IsDeleted && p.PositionID == t.PositionID, true).FirstOrDefault();
                    string levelName = level == null ? string.Empty : level.LevelName;
                    string positionName = position == null ? string.Empty : position.PositionName;
                    return new OfficerInfo
                    {
                        OfficerID = t.OfficerID,
                        Name = t.Name,
                        Birthday = t.Birthday,
                        PositionID = t.PositionID,
                        PositionName = positionName,
                        LevelID = t.LevelID,
                        LevelName = levelName,
                        OnOfficeDate = t.OnOfficeDate,
                        CurrentScore = t.CurrentScore,
                        Gender = t.Gender
                    };
                });

                result.OfficerList.AddRange(officerInfo);
                response.Result = result;

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "获取单位详细信息");
                #endregion

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

        #region 编辑单位
        public BaseResponse<bool> EditOrganizationRecord(EditOrganParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                #region 输入验证
                if (string.IsNullOrEmpty(parameter.OrganCode))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位编码不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(parameter.OrganFullName))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位全称不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(parameter.OrganShortName))
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位简称不能为空";
                    return response;
                }

                var isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganCode) && t.OrganCode.Equals(parameter.OrganCode) && t.OrganID != parameter.OrganID, true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位编码已存在";
                    return response;
                }

                isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganFullName) && t.OrganFullName.Equals(parameter.OrganFullName) && t.OrganID != parameter.OrganID, true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位全称已存在";
                    return response;
                }

                isExisted = organRepository.GetDatas<Organization>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.OrganShortName) && t.OrganShortName.Equals(parameter.OrganShortName) && t.OrganID != parameter.OrganID, true).Any();
                if (isExisted)
                {
                    response.IsSuccessful = false;
                    response.Reason = "单位简称已存在";
                    return response;
                }
                #endregion

                //var organInDB = organRepository.GetByKey(parameter.OrganID);
                var organInDB = organRepository.GetDatas<Organization>(t => t.OrganID == parameter.OrganID && !t.IsDeleted, true).FirstOrDefault();
                if (organInDB == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "编辑单位时候数据异常";
                    return response;
                }

                organInDB.OrganCode = parameter.OrganCode;
                organInDB.OrganFullName = parameter.OrganFullName;
                organInDB.OrganShortName = parameter.OrganShortName;
                organInDB.OrganTypeID = parameter.OrganTypeID;

                organInDB.AreaID = parameter.AreaID;
                organInDB.LastUpdateUserID = parameter.UpdateUserID;
                organInDB.LastUpdateDate = DateTime.Now;

                var operResult = organRepository.Update<Organization>(organInDB);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位编辑发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "编辑部门");
                #endregion

                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                response.Reason = e.Message;

                return response;
            }
        }

        #endregion

        #region 删除单位
        public BaseResponse<bool> DeleteOrganRecord(DeleteOrganParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var organ = organRepository.GetDatas<Organization>(t => t.OrganID == param.OrganID && !t.IsDeleted, true).FirstOrDefault();
                if (organ == null)
                {
                    throw new Exception("删除单位时候，数据异常！");
                }

                var hasOfficers = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OrganizationID == organ.OrganID, true).Any();
                if (hasOfficers)
                {
                    throw new Exception("单位下有干部，不能删除");
                }
                //逻辑删除
                organ.IsDeleted = true;

                var operResult = organRepository.Update<Organization>(organ);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位编辑发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;

                #region 操作日志
                new LogManager().AddOperationLog(param.CurrentUserID, "删除部门");
                #endregion
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                response.Reason = e.Message;

                return response;
            }

        }
        #endregion

        #region 获取单位类型
        public BaseResponse<GetOrganTypeResult> GetOrganTypeList()
        {
            BaseResponse<GetOrganTypeResult> response = new BaseResponse<GetOrganTypeResult>();
            GetOrganTypeResult result = new GetOrganTypeResult();
            try
            {
                var categories = organCategoryRepository.GetDatas<OrganCategory>(t => !t.IsDeleted, true).ToArray().Select(t =>
                {
                    CategoryInfo category = new CategoryInfo();
                    category.CategoryID = t.CategoryID;
                    category.CategoryName = t.CategoryName;

                    var organTypeList = organTypeRepository.GetDatas<OrganType>(n => !n.IsDeleted && n.CategoryID == t.CategoryID, true).ToArray().Select(n =>
                    {
                        OrganTypeInfo info = new OrganTypeInfo();
                        info.OrganTypeID = n.OrganTypeID;
                        info.OrganTypeName = n.OrganTypeName;

                        return info;
                    });
                    category.OrganTypeList.AddRange(organTypeList);
                    return category;
                }).ToList();
                result.CategoryList = categories;
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "获取单位类型信息异常";
                return response;
            }
        }
        #endregion

        #region 获取单位列表
        public BaseResponse<GetOrganListResult> GetOrganList(GetOrganInfoListParameter parameter)
        {
            BaseResponse<GetOrganListResult> response = new BaseResponse<GetOrganListResult>();
            GetOrganListResult result = new GetOrganListResult();

            if (string.IsNullOrEmpty(parameter.Sort))
            {
                parameter.Sort = "OrganCode";
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
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var organQuerable = dbContext.Organizations.Where(t => !t.IsDeleted);
                    if (parameter.OrganTypeID.HasValue && parameter.OrganTypeID.Value > 0)
                    {
                        organQuerable = organQuerable.Where(t => t.OrganTypeID == parameter.OrganTypeID.Value);
                    }
                    if (!string.IsNullOrEmpty(parameter.Keyword))
                    {
                        string matchedWord = parameter.Keyword.ToUpper();
                        organQuerable = organQuerable.Where(t => t.OrganCode.ToUpper().Contains(matchedWord) || t.OrganFullName.ToUpper().Contains(matchedWord) || t.OrganShortName.ToUpper().Contains(matchedWord));
                    }

                    var organListLinq = from organ in organQuerable
                                        join organType in dbContext.OrganTypes.Where(t => !t.IsDeleted) on organ.OrganTypeID equals organType.OrganTypeID
                                        into group1
                                        from g1 in group1
                                        join category in dbContext.OrganCategories.Where(t => !t.IsDeleted) on g1.CategoryID equals category.CategoryID
                                        into group2
                                        from g2 in group2


                                        join item in dbContext.Officers.Where(t => !t.IsDeleted).GroupBy(t => t.OrganizationID) on organ.OrganID equals item.Key

                                        into group3
                                        from g3 in group3.DefaultIfEmpty()


                                        select new OrganInfo
                                        {
                                            OrganID = organ.OrganID,
                                            OrganCode = organ.OrganCode,
                                            OrganFullName = organ.OrganFullName,
                                            OrganShortName = organ.OrganShortName,
                                            OrganTypeID = organ.OrganTypeID,
                                            OrganTypeName = g1.OrganTypeName,
                                            OrganCategoryID = g2.CategoryID,
                                            OrganCategoryName = g2.CategoryName,
                                            OfficerQuanlity = g3.Count(),
                                            AddDate = organ.AddDate,
                                            AreaID = organ.AreaID
                                        };

                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("OrganID", sortOrder),
                    };

                    organListLinq = organListLinq.OrderBy(sortList);

                    int count = organListLinq.Count();
                    if (parameter.Page > -1)
                    {
                        organListLinq = organListLinq
                            .Skip((parameter.Page - 1) * parameter.PageSize)
                            .Take(parameter.PageSize);
                    }

                    result.OrganInfoList.AddRange(organListLinq.ToList());
                    result.Total = count;
                    response.Result = result;

                    #region 操作日志
                    new LogManager().AddOperationLog(parameter.CurrentUserID, "获取部门列表");
                    #endregion

                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = e.Message;
                return response;
            }
        }
        #endregion

        #region 添加单位时候，获取地区列表
        public BaseResponse<GetAreaListResult> GetAreaList()
        {
            BaseResponse<GetAreaListResult> response = new BaseResponse<GetAreaListResult>();
            GetAreaListResult result = new GetAreaListResult();
            try
            {
                var areas = areaRepository.GetDatas<Area>(t => !t.IsDeleted, true).Select(t =>
                    new AreaItem
                        {
                            AreaID = t.AreaID,
                            AreaName = t.AreaName
                        });

                result.AreaItemList.AddRange(areas);
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
        #endregion
    }
}
