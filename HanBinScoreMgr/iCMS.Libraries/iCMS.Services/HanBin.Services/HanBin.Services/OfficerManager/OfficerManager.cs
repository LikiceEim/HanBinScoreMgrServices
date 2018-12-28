using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.OfficerManager;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.HanBin.OfficerManager;
using iCMS.Common.Component.Tool;
using HanBin.Common.Commonent.Data.Enum;
using System.ComponentModel;

namespace HanBin.Services.OfficerManager
{
    public class OfficerManager : IOfficerManager
    {
        [Dependency]
        public IRepository<Officer> officerRepository { get; set; }
        [Dependency]
        public IRepository<ScoreApply> scoreApplyRepository { get; set; }
        [Dependency]
        public IRepository<ScoreItem> scoreItemRepository { get; set; }

        public IRepository<ApplyUploadFile> applyUploadFileRepository { get; set; }

        #region 添加干部
        public BaseResponse<bool> AddOfficerRecord(AddOfficerParameter parameter)
        {
            //1. 添加干部基础信息
            //2. 添加此干部的积分申请（并设置为审批通过）
            //3. 修改当前积分
            //4. 添加积分变更记录
            BaseResponse<bool> response = new BaseResponse<bool>();
            //iCMSDbContext dbContext = new iCMSDbContext();
            OperationResult operResult = null;
            try
            {
                ExecuteDB.ExecuteTrans((dbContext) =>
                {
                    Officer officer = new Officer();
                    officer.Name = parameter.Name;
                    officer.Gender = parameter.Gender;
                    officer.IdentifyCardNumber = parameter.IdentifyNumber;
                    officer.Birthday = parameter.Birthday;
                    officer.OrganizationID = parameter.OrganizationID;
                    officer.PositionID = parameter.PositionID;
                    officer.LevelID = parameter.LevelID;
                    officer.OnOfficeDate = parameter.OnOfficeDate;
                    officer.InitialScore = parameter.InitialScore;
                    officer.CurrentScore = officer.InitialScore;
                    officer.LastUpdateDate = DateTime.Now;
                    officer.LastUpdateUserID = parameter.AddUserID;
                    operResult = dbContext.Officers.AddNew<Officer>(dbContext, officer);
                    if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                    {
                        throw new Exception("数据库操作异常");
                    }

                    int extraScore = 0;
                    if (parameter.ApplyItemList.Any())
                    {
                        parameter.ApplyItemList.ForEach(t =>
                        {
                            ScoreApply scApply = new ScoreApply();
                            scApply.OfficerID = officer.OfficerID;
                            scApply.ItemID = t.ItemID;
                            scApply.ItemScore = t.ItemScore;
                            scApply.ApplyStatus = 1;//自动设置为审批通过
                            scApply.ProposeID = officer.AddUserID;
                            scApply.AddUserID = officer.AddUserID;
                            scApply.LastUpdateDate = DateTime.Now;
                            scApply.LastUpdateUserID = officer.AddUserID;
                            scApply.ApplySummary = t.ApplySummary;
                            scApply.IsDeleted = false;

                            operResult = dbContext.ScoreApplies.AddNew<ScoreApply>(dbContext, scApply);
                            if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                            {
                                throw new Exception("数据库操作异常");
                            }
                            //保存变化的分值，最后更新CurrentScore
                            extraScore += scApply.ItemScore;

                            //保存积分更新历史记录表
                            ScoreChangeHistory his = new ScoreChangeHistory();
                            his.ApplyID = scApply.ApplyID;
                            his.OfficerID = scApply.OfficerID;
                            his.ItemID = scApply.ItemID;
                            his.ItemScore = scApply.ItemScore;
                            his.ProcessUserID = null;
                            his.ProposeID = scApply.ProposeID;
                            his.AddUserID = scApply.AddUserID;
                            var scoreItem = dbContext.ScoreItems.Where(x => x.ItemID == his.ItemID).FirstOrDefault();
                            if (scoreItem != null)
                            {
                                his.Content = string.Format("{0}  {1} {2}", his.ItemScore, his.AddDate, scoreItem.ItemDescription);
                            }

                            operResult = dbContext.ScoreChangeHistories.AddNew<ScoreChangeHistory>(dbContext, his);
                            if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                            {
                                throw new Exception("数据库操作异常");
                            }
                        });
                    }

                    var officeIndb = dbContext.Officers.Where(t => t.OfficerID == officer.OfficerID && !t.IsDeleted).FirstOrDefault();
                    if (officeIndb != null)
                    {
                        officeIndb.CurrentScore += extraScore;
                        dbContext.Officers.Update<Officer>(dbContext, officeIndb);
                    }
                });
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "添加干部发生异常！";
            }

            return response;
        }
        #endregion

        #region 获取干部基础信息（不包含积分信息）
        public BaseResponse<GetOfficerDetailInfoResult> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter)
        {
            BaseResponse<GetOfficerDetailInfoResult> response = new BaseResponse<GetOfficerDetailInfoResult>();
            GetOfficerDetailInfoResult result = new GetOfficerDetailInfoResult();
            try
            {
                var officer = officerRepository.GetDatas<Officer>(t => t.OfficerID == parameter.OfficerID && !t.IsDeleted, true).FirstOrDefault();
                if (officer == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "获取干部详细信息数据异常";
                    return response;
                }
                result.OfficerID = officer.OfficerID;
                result.Name = officer.Name;
                result.Gender = officer.Gender;
                result.IdentifyNumber = officer.IdentifyCardNumber;
                result.Birthday = officer.Birthday;
                result.OrganizationID = officer.OrganizationID;
                result.PositionID = officer.PositionID;
                result.LevelID = officer.LevelID;
                result.OnOfficeDate = officer.OnOfficeDate;
                result.Duty = officer.Duty;
                result.AddUserID = officer.AddUserID;
                result.AddDate = officer.AddDate;

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = "获取干部详细信息发生异常";
                return response;
            }
        }

        #endregion

        #region 获取干部积分信息（用于编辑干部页面的右边部分）
        public BaseResponse<GetOfficerScoreDetailInfoResult> GetOfficerScoreDetailInfo(GetOfficerScoreDetailInfoParameter parameter)
        {
            BaseResponse<GetOfficerScoreDetailInfoResult> response = new BaseResponse<GetOfficerScoreDetailInfoResult>();
            GetOfficerScoreDetailInfoResult result = new GetOfficerScoreDetailInfoResult();
            try
            {
                //字典表缓存
                var scoreItemArray = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted, true).ToList();
                var officer = officerRepository.GetDatas<Officer>(t => t.OfficerID == parameter.OfficerID && !t.IsDeleted, true).FirstOrDefault();
                if (officer == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "获取干部积分信息数据异常";
                    return response;
                }

                result.OfficerID = officer.OfficerID;
                result.InitialScore = officer.InitialScore;
                result.CurrentScore = officer.CurrentScore;

                //获取此干部 已通过审批（审批通过或者驳回）的积分申请项
                var applovedScoreApplyList = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID && (t.ApplyStatus == (int)EnumApproveStatus.Pass || t.ApplyStatus == (int)EnumApproveStatus.Reject), true).ToList();
                if (applovedScoreApplyList != null && applovedScoreApplyList.Any())
                {
                    applovedScoreApplyList.ForEach(t =>
                    {
                        ApplyItemInfo applyItenInfo = new ApplyItemInfo();
                        applyItenInfo.ApplyID = t.ApplyID;
                        applyItenInfo.ApplyStatus = t.ApplyStatus;

                        var scoreItem = scoreItemArray.Where(tt => tt.ItemID == t.ItemID).FirstOrDefault();
                        if (scoreItem != null)
                        {
                            applyItenInfo.ItemID = scoreItem.ItemID;
                            applyItenInfo.ItemScore = scoreItem.ItemScore;
                            applyItenInfo.ItemDescription = scoreItem.ItemDescription;
                        }

                        result.ApprovedApplyItemList.Add(applyItenInfo);
                    });
                }
                //获取此干部 通过未进行审批（审批中）的积分申请项
                var applovingScoreApplyList = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID && t.ApplyStatus == (int)EnumApproveStatus.Approving, true).ToList();
                if (applovingScoreApplyList != null && applovingScoreApplyList.Any())
                {
                    applovingScoreApplyList.ForEach(t =>
                    {
                        ApplyItemInfo applyItenInfo = new ApplyItemInfo();
                        applyItenInfo.ApplyID = t.ApplyID;
                        applyItenInfo.ApplyStatus = t.ApplyStatus;

                        var scoreItem = scoreItemArray.Where(tt => tt.ItemID == t.ItemID).FirstOrDefault();
                        if (scoreItem != null)
                        {
                            applyItenInfo.ItemID = scoreItem.ItemID;
                            applyItenInfo.ItemScore = scoreItem.ItemScore;
                            applyItenInfo.ItemDescription = scoreItem.ItemDescription;
                        }

                        result.ApprovingApplyItemList.Add(applyItenInfo);
                    });
                }

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "获取干部积分信息发生异常";
                return response;
            }
        }
        #endregion

        #region 编辑积分申请时候，获取积分申请详细信息
        public BaseResponse<GetApplyDetailInfoResult> GetApplyDetailInfo(GetApplyDetailInfoParameter parameter)
        {
            BaseResponse<GetApplyDetailInfoResult> response = new BaseResponse<GetApplyDetailInfoResult>();
            GetApplyDetailInfoResult result = new GetApplyDetailInfoResult();
            try
            {
                ScoreApply scApply = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyID == parameter.ApplyID, true).FirstOrDefault();
                if (scApply == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "获取积分申请信息数据异常";
                    return response;
                }
                result.ApplyID = scApply.ApplyID;
                result.ItemID = scApply.ItemID;
                result.ApplySummary = scApply.ApplySummary;
                var scoreItem = scoreApplyRepository.GetDatas<ScoreItem>(t => !t.IsDeleted && t.ItemID == result.ItemID, true).FirstOrDefault();
                if (scoreItem == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "获取积分申请信息数据异常";
                    return response;
                }
                result.ItemScore = scoreItem.ItemScore;
                result.ItemDescription = scoreItem.ItemDescription;
                result.Type = scoreItem.Type;

                var apyFiles = applyUploadFileRepository.GetDatas<ApplyUploadFile>(t => !t.IsDeleted && t.ApplyID == result.ApplyID, true).Select(t => t.FilePath).ToList();
                result.UploadFileList.AddRange(apyFiles);

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "获取积分申请信息数据异常";
                return response;
            }
        }
        #endregion

        #region 撤销积分申请
        public BaseResponse<bool> CancelScoreApply(CancelScoreApplyParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var apply = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyID == parameter.ApplyID && t.ApplyStatus == (int)EnumApproveStatus.Approving, true).FirstOrDefault();
                if (apply == null)
                {
                    throw new Exception("数据异常");
                }
                apply.ApplyStatus = (int)EnumApproveStatus.Revoke;
                var operResult = scoreApplyRepository.Update<ScoreApply>(apply);
                if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("撤销积分申请时，数据库操作发生异常");
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
        #endregion

        #region 删除干部
        public BaseResponse<bool> DeleteOfficerRecord(DeleteOfficerParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var officer = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID, true).FirstOrDefault();
                if (officer == null)
                {
                    throw new Exception("数据异常");
                }
                officer.IsDeleted = true;
                var operResult = officerRepository.Update<Officer>(officer);
                if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("删除干部时，数据库操作发生异常");
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
        #endregion

        #region 获取干部列表
        public BaseResponse<GetOfficerListResult> GetOfficerList(GetOfficerListParameter parameter)
        {
            BaseResponse<GetOfficerListResult> response = new BaseResponse<GetOfficerListResult>();
            GetOfficerListResult result = new GetOfficerListResult();
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var officerQuerable = dbContext.Officers.Where(t => !t.IsDeleted);
                    if (parameter.OrganizationID.HasValue && parameter.OrganizationID.Value > 0)
                    {
                        officerQuerable = officerQuerable.Where(t => t.OrganizationID == parameter.OrganizationID);
                    }

                    var officerLinq = from off in officerQuerable
                                      join org in dbContext.Organizations
                                      on off.OrganizationID equals org.OrganID
                                      join pos in dbContext.OfficerPositionTypes on off.PositionID equals pos.PositionID
                                      join lev in dbContext.OfficerLevelTypes on off.OfficerID equals lev.LevelID
                                      where org.OrganFullName.ToUpper().Contains(parameter.Keyword.ToUpper()) || org.OrganCode.ToUpper().Contains(parameter.Keyword.ToUpper())
                                      select new OfficerDetailInfo
                                      {
                                          OfficerID = off.OfficerID,
                                          Name = off.Name,
                                          Gender = off.Gender,
                                          Birthday = off.Birthday,
                                          OrganizationName = org.OrganFullName,
                                          PositionID = pos.PositionID,
                                          PositionName = pos.PositionName,
                                          LevelID = lev.LevelID,
                                          LevelName = lev.LevelName,
                                          OnOfficeDate = off.OnOfficeDate,
                                          OrganizationID = org.OrganID,
                                          CurrentScore = off.CurrentScore,
                                          IdentifyNumber = off.IdentifyCardNumber
                                      };


                    if (!string.IsNullOrEmpty(parameter.Keyword))
                    {
                        officerLinq = officerLinq.Where(t => t.Name.Contains(parameter.Keyword) || t.IdentifyNumber.Contains(parameter.Keyword));
                    }
                    ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(parameter.Sort, sortOrder),
                        new PropertySortCondition("OfficerID", sortOrder),
                    };

                    officerLinq = officerLinq.OrderBy(sortList);
                    int count = officerLinq.Count();
                    if (parameter.Page > -1)
                    {
                        officerLinq = officerLinq
                            .Skip((parameter.Page - 1) * parameter.PageSize)
                            .Take(parameter.PageSize);
                    }

                    result.OfficerInfoList.AddRange(officerLinq.ToList());
                    result.Total = count;
                    response.Result = result;

                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Reason = "获取单位列表发生异常";

                return response;
            }
        }
        #endregion
    }
}
