using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
using iCMS.Common.Component.Data.Response.HanBinOrganManager;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.OrganManager
{
    public class OrganManager : IOrganManager
    {
        [Dependency]
        public IRepository<Organization> organRepository { get; set; }

        [Dependency]
        public IRepository<Officer> officerRepository { get; set; }

        #region 添加单位
        public BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                Organization organ = new Organization();
                organ.OrganCode = param.OrganCode;
                organ.OrganFullName = param.OrganFullName;
                organ.OrganShortName = param.OrganShortName;
                organ.OrganTypeID = param.OrganTypeID;
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
                result.OrganTypeName = string.Empty;
                result.OrganCategoryID = 0;
                result.OrganCategoryName = string.Empty;

                //获取单位下的干部
                var officerInfo = officerRepository.GetDatas<Officer>(t => t.OrganizationID == organ.OrganID && !t.IsDeleted, true).ToArray().Select(t =>
                {
                    return new OfficerInfo
                    {
                        OfficerID = t.OfficerID,
                        Name = t.Name,
                        Birthday = t.Birthday,
                        PositionID = t.PositionID,
                        PositonName = string.Empty,
                        LevelID = t.LevelID,
                        IevelName = string.Empty,
                        OnOfficeDate = t.OnOfficeDate,
                        CurrentScore = t.CurrentScore
                    };
                });

                result.OfficerList.AddRange(officerInfo);
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

        #region 编辑单位
        public BaseResponse<bool> EditOrganizationRecord(EditOrganParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
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
                organInDB.LastUpdateUserID = parameter.UpdateUserID;
                organInDB.LastUpdateDate = DateTime.Now;

                var operResult = organRepository.Update<Organization>(organInDB);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位编辑发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;
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
                //逻辑删除
                organ.IsDeleted = true;

                var operResult = organRepository.Update<Organization>(organ);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位编辑发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;
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

    }
}
