using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using iCMS.Common.Component.Data.Response.HanBin.ScoreManager;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Service.Common;
using HanBin.Common.Commonent.Data.Enum;

namespace HanBin.Services.ScoreManager
{
    public class ScoreManager : IScoreManager
    {
        [Dependency]
        public IRepository<ScoreItem> scoreItemRepository { get; set; }

        [Dependency]
        public IRepository<ScoreApply> scoreApplyRepository { get; set; }

        [Dependency]
        public IRepository<ApplyUploadFile> ufRepository { get; set; }

        [Dependency]
        public IRepository<Officer> officerRepository { get; set; }

        [Dependency]
        public IRepository<ScoreChangeHistory> schRepository { get; set; }

        [Dependency]
        public IRepository<Organization> organRepository { get; set; }

        [Dependency]
        public IRepository<HBUser> userRepository { get; set; }

        [Dependency]
        public IRepository<OfficerPositionType> positionRepository { get; set; }
        [Dependency]
        public IRepository<ScoreChangeHistory> schisRepository { get; set; }
        [Dependency]
        public IRepository<OfficerLevelType> levelRepository { get; set; }

        #region 积分条目字典CRUD
        public BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var scoreItem = new ScoreItem();
                scoreItem.ItemID = parameter.ItemScore;
                scoreItem.ItemDescription = parameter.ItemDescription;
                scoreItem.Type = parameter.Type;
                var operResult = scoreItemRepository.AddNew<ScoreItem>(scoreItem);
                if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("添加积分条目发生异常");
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

        #region 编辑积分条目
        public BaseResponse<bool> EditScoreItem(EditScoreItemParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var scoreItemIndb = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted && t.ItemID == parameter.ItemID, true).FirstOrDefault();
                if (scoreItemIndb == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "所编辑积分条目不存在";
                    return response;
                }
                scoreItemIndb.ItemScore = parameter.ItemScore;
                scoreItemIndb.ItemDescription = parameter.ItemDescription;
                var operResult = scoreItemRepository.Update<ScoreItem>(scoreItemIndb);
                if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("编辑积分条目发生异常");
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

        #region 删除积分条目
        public BaseResponse<bool> DeleteScoreItem(DeleteScoreItemParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var scoreItemIndb = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted && t.ItemID == parameter.ItemID, true).FirstOrDefault();
                if (scoreItemIndb == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "将要删除积分条目不存在";
                    return response;
                }
                scoreItemIndb.IsDeleted = true;

                var operResult = scoreItemRepository.Update<ScoreItem>(scoreItemIndb);
                if (operResult.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("删除积分条目发生异常");
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

        #region 获取积分条目列表
        public BaseResponse<GetScoreItemListResult> GetScoreItemList()
        {
            BaseResponse<GetScoreItemListResult> response = new BaseResponse<GetScoreItemListResult>();
            GetScoreItemListResult result = new GetScoreItemListResult();

            try
            {
                var scoreItemArray = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted, true).Select(t => new ScoreItemInfo
                {
                    ItemID = t.ItemID,
                    ItemScore = t.ItemScore,
                    ItemDescription = t.ItemDescription,
                    Type = t.Type
                }).ToList();

                result.ScoreItemInfoList.AddRange(scoreItemArray);
                response.Result = result;
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
        #endregion

        #region 添加一条积分申请
        public BaseResponse<bool> AddScoreApply(AddScoreApplyParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                ExecuteDB.ExecuteTrans((dbContext) =>
                {
                    ScoreApply scApply = new ScoreApply();
                    scApply.OfficerID = parameter.OfficerID;
                    scApply.ItemID = parameter.ScoreItemID;
                    scApply.ItemScore = 0;
                    scApply.ApplyStatus = (int)EnumApproveStatus.Approving;
                    scApply.ProposeID = parameter.ProposeID;
                    scApply.AddUserID = parameter.ProposeID;
                    scApply.LastUpdateUserID = parameter.ProposeID;
                    scApply.LastUpdateDate = DateTime.Now;

                    scApply.ApplySummary = parameter.ApplySummary;

                    var operRes = dbContext.ScoreApplies.AddNew<ScoreApply>(dbContext, scApply);
                    if (operRes.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                    {
                        throw new Exception("保存积分申请发生异常");
                    }
                    if (parameter.UploadFileList != null && parameter.UploadFileList.Any())
                    {
                        //保存上传的文件路径
                        parameter.UploadFileList.ForEach(f =>
                        {
                            ApplyUploadFile uf = new ApplyUploadFile { ApplyID = scApply.ApplyID, FilePath = f };
                            operRes = dbContext.ApplyUploadFiles.AddNew<ApplyUploadFile>(dbContext, uf);

                            if (operRes.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                            {
                                throw new Exception("保存积分申请发生异常");
                            }
                        });
                    }

                });
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

        #region 编辑积分申请
        public BaseResponse<bool> EditScoreApply(EditScoreApplyParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                var scoreApply = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyID == parameter.ApplyID, true).FirstOrDefault();
                if (scoreApply == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "所要编辑的积分申请不存在";
                    return response;
                }
                if (scoreApply.ApplyStatus != (int)EnumApproveStatus.Approving)
                {
                    response.IsSuccessful = false;
                    response.Reason = "已审批过的积分申请不能编辑";
                    return response;
                }
                scoreApply.ApplySummary = parameter.ApplySummary;
                var operRes = scoreApplyRepository.Update<ScoreApply>(scoreApply);
                if (operRes.ResultType != iCMS.Common.Component.Data.Enum.EnumOperationResultType.Success)
                {
                    throw new Exception("编辑积分申请发生异常");
                }

                //编辑文件路径列表：先删除， 后添加，后续 优化
                ///TODO:
                ///
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

        #region 审核积分申请
        public BaseResponse<bool> CheckScoreApply(CheckScoreApplyParameter parameter)
        {
            //1. 修改积分申请状态为 通过/拒绝
            //2. 通过的话加分或者减分
            //3. 保存积分变更记录
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var apply = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyID == parameter.ApplyID, true).FirstOrDefault();
                if (apply == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "审核积分申请时候，数据异常";
                    return response;
                }
                if (apply.ApplyStatus != (int)EnumApproveStatus.Approving)
                {
                    response.IsSuccessful = false;
                    response.Reason = "此积分申请已经被审批过，或者已撤销";
                    return response;
                }
                if (parameter.ApplyStatus == (int)EnumApproveStatus.Reject && string.IsNullOrEmpty(parameter.RejectReason))
                {
                    response.IsSuccessful = false;
                    response.Reason = "请输入驳回原因";
                    return response;
                }

                apply.ApplyStatus = parameter.ApplyStatus;
                apply.RejectReason = parameter.RejectReason;
                apply.ProcessUserID = parameter.ProcessUserID;

                scoreApplyRepository.Update<ScoreApply>(apply);


                if (parameter.ApplyStatus == (int)EnumApproveStatus.Pass)
                {
                    var officer = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OfficerID == apply.OfficerID, true).FirstOrDefault();
                    if (officer != null)
                    {
                        officer.CurrentScore += apply.ItemScore;
                        officerRepository.Update<Officer>(officer);
                    }
                    //插入积分变更记录
                    ScoreChangeHistory hist = new ScoreChangeHistory();
                    hist.ApplyID = apply.ApplyID;
                    hist.OfficerID = apply.OfficerID;
                    hist.ItemID = apply.ItemID;
                    hist.ItemScore = apply.ItemScore;
                    hist.ProcessUserID = parameter.ProcessUserID;
                    hist.ProposeID = apply.ProposeID;
                    hist.AddUserID = parameter.ProcessUserID;

                    var scoreItem = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted && t.ItemID == apply.ItemID, true).FirstOrDefault();
                    string desc = scoreItem == null ? string.Empty : scoreItem.ItemDescription;

                    var off = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OfficerID == hist.OfficerID, true).FirstOrDefault();
                    if (off != null)
                    {
                        var organ = organRepository.GetDatas<Organization>(t => !t.IsDeleted && t.OrganID == off.OrganizationID, true).FirstOrDefault();
                        if (organ != null)
                        {
                            hist.Content = string.Format("{0}  {1} {2}", organ.OrganFullName, off.Name, desc);
                        }
                    }

                    schRepository.AddNew<ScoreChangeHistory>(hist);

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

        #region 首页
        #region 系统统计
        public BaseResponse<SystemStatSummaryResult> SystemStatSummary()
        {
            BaseResponse<SystemStatSummaryResult> respose = new BaseResponse<SystemStatSummaryResult>();
            SystemStatSummaryResult result = new SystemStatSummaryResult();
            try
            {
                result.OrganizatonCount = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true).Count();
                result.OfficerCount = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).Count();
                result.UserCount = userRepository.GetDatas<HBUser>(t => !t.IsDeleted, true).Count();

                result.AvarageScore = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).Select(t => t.CurrentScore).Average();

                respose.Result = result;
                return respose;
            }
            catch (Exception e)
            {
                respose.IsSuccessful = false;
                respose.Reason = e.Message;
                return respose;
            }
        }

        #endregion

        #region 获取红榜数据
        public BaseResponse<GetHonourBoardResult> GetHonourBoard(GetHonourBoardParameter parameter)
        {
            BaseResponse<GetHonourBoardResult> response = new BaseResponse<GetHonourBoardResult>();
            GetHonourBoardResult result = new GetHonourBoardResult();
            try
            {
                int rank = parameter.RankNumber;
                if (rank < 1)
                {
                    throw new Exception("参数异常");
                }

                var positionArray = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).ToList();

                var scoreRankList = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).OrderByDescending(t => t.CurrentScore).Take(rank).ToArray().Select(t =>
                {
                    int positionID = t.PositionID;
                    var position = positionArray.Where(p => p.PositionID == positionID).FirstOrDefault();
                    var positionName = position == null ? string.Empty : position.PositionName;

                    return new ScoreRankInfo
                    {
                        OfficerID = t.OfficerID,
                        OfficerName = t.Name,
                        CurrentScore = t.CurrentScore,
                        Gender = t.Gender,
                        PositionName = positionName

                    };
                });

                result.RankList.AddRange(scoreRankList);
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

        #region 获取黑榜数据
        public BaseResponse<GetBlackBoardResult> GetBlackBoard(GetBlackBoardParameter parameter)
        {
            BaseResponse<GetBlackBoardResult> response = new BaseResponse<GetBlackBoardResult>();
            GetBlackBoardResult result = new GetBlackBoardResult();
            try
            {
                int rank = parameter.RankNumber;
                if (rank < 1)
                {
                    throw new Exception("参数异常");
                }

                var positionArray = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).ToList();

                //按照分数升序排列
                var scoreRankList = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).OrderBy(t => t.CurrentScore).Take(rank).ToArray().Select(t =>
                {
                    int positionID = t.PositionID;
                    var position = positionArray.Where(p => p.PositionID == positionID).FirstOrDefault();
                    var positionName = position == null ? string.Empty : position.PositionName;

                    return new ScoreRankInfo
                    {
                        OfficerID = t.OfficerID,
                        OfficerName = t.Name,
                        CurrentScore = t.CurrentScore,
                        Gender = t.Gender,
                        PositionName = positionName

                    };
                });

                result.RankList.AddRange(scoreRankList);
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

        #region 待我审核【总览信息】
        public BaseResponse<WhatsToDoSummaryResult> GetWhatsToDoSummary(GetWhatsToDoSummaryParameter parameter)
        {
            BaseResponse<WhatsToDoSummaryResult> response = new BaseResponse<WhatsToDoSummaryResult>();
            WhatsToDoSummaryResult result = new WhatsToDoSummaryResult();

            try
            {
                var applySummaryList = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyStatus == (int)EnumApproveStatus.Approving, true).ToArray().Select(t =>
                {
                    int proposeUserID = t.ProposeID;

                    var user = userRepository.GetDatas<HBUser>(u => u.UserID == proposeUserID, true).FirstOrDefault();
                    string applyTitle = string.Format("{0}提交了一个申请", user == null ? string.Empty : user.UserToken);
                    return new WhatToDoInfo
                    {
                        ApplyID = t.ApplyID,
                        ApplyDate = t.AddDate,
                        ApplyTitle = applyTitle
                    };
                });

                result.WhatToDoList.AddRange(applySummaryList);
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

        #region 待我审批【详细信息】
        public BaseResponse<GetWhatsToDoDetailListResult> GetWhatsToDoDetailList(GetWhatsToDoDetailListParameter parameter)
        {
            BaseResponse<GetWhatsToDoDetailListResult> response = new BaseResponse<GetWhatsToDoDetailListResult>();
            GetWhatsToDoDetailListResult result = new GetWhatsToDoDetailListResult();
            try
            {
                var applyQuerable = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.ApplyStatus == (int)EnumApproveStatus.Approving, true);
                int total = applyQuerable.Count();

                var officerArray = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).ToList();
                var organArray = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true).ToList();
                var positionArray = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).ToList();

                var scoreItemArray = scoreItemRepository.GetDatas<ScoreItem>(t =>
                    !t.IsDeleted, true).ToList();

                var Linq = applyQuerable
                            .Skip((parameter.Page - 1) * parameter.PageSize)
                            .Take(parameter.PageSize).ToList();

                Linq.ForEach(t =>
                {
                    ApplyDetail applyDetail = new ApplyDetail();
                    applyDetail.ApplyID = t.ApplyID;
                    var officer = officerArray.Where(o => o.OfficerID == t.OfficerID).FirstOrDefault();
                    if (officer != null)
                    {
                        applyDetail.OfficerName = officer.Name;
                        applyDetail.IdentifyCardNumber = officer.IdentifyCardNumber;
                        applyDetail.OrganID = officer.OrganizationID;

                        var organ = organArray.Where(g => g.OrganID == officer.OrganizationID).FirstOrDefault();
                        if (organ != null)
                        {
                            applyDetail.OrganFullName = organ.OrganFullName;
                        }
                    }

                    applyDetail.PositionID = officer.PositionID;
                    var position = positionArray.Where(p => p.PositionID == officer.PositionID).FirstOrDefault();
                    if (position != null)
                    {
                        applyDetail.PositionName = position.PositionName;
                    }
                    applyDetail.ItemScore = t.ItemScore;

                    var scoreItem = scoreItemArray.Where(s => s.ItemID == t.ItemID).FirstOrDefault();
                    if (scoreItem != null)
                    {
                        applyDetail.ItemDescription = scoreItem.ItemDescription;
                    }

                    var files = ufRepository.GetDatas<ApplyUploadFile>(f => !f.IsDeleted && f.ApplyID == t.ApplyID, true).Select(f => f.FilePath).ToList();
                    applyDetail.UploadFileList.AddRange(files);

                    result.ApplyDetailList.Add(applyDetail);
                    result.Total = total;
                });

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

        #region 上级反馈【总览信息】
        public BaseResponse<GetHighLevelFeedBackSummaryResult> GetHighLevelFeedBackSummary(GetHighLevelFeedBackSummaryParameter parameter)
        {
            BaseResponse<GetHighLevelFeedBackSummaryResult> response = new BaseResponse<GetHighLevelFeedBackSummaryResult>();
            GetHighLevelFeedBackSummaryResult result = new GetHighLevelFeedBackSummaryResult();

            try
            {
                var approvedApplyList = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && (t.ApplyStatus == (int)EnumApproveStatus.Pass || t.ApplyStatus == (int)EnumApproveStatus.Reject), true).ToArray().Select(t =>
                {
                    int proposeUserID = t.ProposeID;

                    var user = userRepository.GetDatas<HBUser>(u => u.UserID == proposeUserID, true).FirstOrDefault();
                    string feedBackTitle = string.Format("{0}提交的申请已{1}", user == null ? string.Empty : user.UserToken, t.ApplyStatus == (int)EnumApproveStatus.Pass ? "通过审核" : "被驳回");
                    return new FeedBackSummaryInfo
                    {
                        ApplyID = t.ApplyID,
                        LastUpdateDate = t.LastUpdateDate,
                        FeedBackTitle = feedBackTitle
                    };
                });

                result.FeedBackList.AddRange(approvedApplyList);
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

        #region 上级反馈【详细信息】
        public BaseResponse<GetHighLevelFeedBackDetailListResult> GetHighLevelFeedBackDetailList(GetHighLevelFeedBackDetailListParameter parameter)
        {
            BaseResponse<GetHighLevelFeedBackDetailListResult> response = new BaseResponse<GetHighLevelFeedBackDetailListResult>();
            GetHighLevelFeedBackDetailListResult result = new GetHighLevelFeedBackDetailListResult();
            try
            {
                var approvedApplyQuerable = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && (t.ApplyStatus == (int)EnumApproveStatus.Pass || t.ApplyStatus == (int)EnumApproveStatus.Reject), true);
                int total = approvedApplyQuerable.Count();

                var officerArray = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).ToList();
                var organArray = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true).ToList();
                var positionArray = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).ToList();

                var scoreItemArray = scoreItemRepository.GetDatas<ScoreItem>(t =>
                    !t.IsDeleted, true).ToList();

                var Linq = approvedApplyQuerable
                            .Skip((parameter.Page - 1) * parameter.PageSize)
                            .Take(parameter.PageSize).ToList();

                Linq.ForEach(t =>
                {
                    ApprovedApplyDetail approvedApply = new ApprovedApplyDetail();
                    approvedApply.ApplyID = t.ApplyID;
                    var officer = officerArray.Where(o => o.OfficerID == t.OfficerID).FirstOrDefault();
                    if (officer != null)
                    {
                        approvedApply.OfficerName = officer.Name;
                        approvedApply.IdentifyCardNumber = officer.IdentifyCardNumber;
                        approvedApply.OrganID = officer.OrganizationID;

                        var organ = organArray.Where(g => g.OrganID == officer.OrganizationID).FirstOrDefault();
                        if (organ != null)
                        {
                            approvedApply.OrganFullName = organ.OrganFullName;
                        }
                    }

                    approvedApply.PositionID = officer.PositionID;
                    var position = positionArray.Where(p => p.PositionID == officer.PositionID).FirstOrDefault();
                    if (position != null)
                    {
                        approvedApply.PositionName = position.PositionName;
                    }
                    approvedApply.ItemScore = t.ItemScore;

                    var scoreItem = scoreItemArray.Where(s => s.ItemID == t.ItemID).FirstOrDefault();
                    if (scoreItem != null)
                    {
                        approvedApply.ItemDescription = scoreItem.ItemDescription;
                    }

                    var files = ufRepository.GetDatas<ApplyUploadFile>(f => !f.IsDeleted && f.ApplyID == t.ApplyID, true).Select(f => f.FilePath).ToList();
                    approvedApply.UploadFileList.AddRange(files);
                    approvedApply.ApproveStatus = t.ApplyStatus;
                    approvedApply.RejectReason = t.RejectReason;

                    result.ApplovedApplyDetailList.Add(approvedApply);

                });

                response.Result = result;
                result.Total = total;
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

        #region 变更公示
        public BaseResponse<GetScoreChangeHistoryResult> GetScoreChangeHistory(GetScoreChangeHistoryParameter parameter)
        {
            BaseResponse<GetScoreChangeHistoryResult> response = new BaseResponse<GetScoreChangeHistoryResult>();
            GetScoreChangeHistoryResult result = new GetScoreChangeHistoryResult();

            try
            {
                IQueryable<ScoreChangeHistory> scHisQuerable = schisRepository.GetDatas<ScoreChangeHistory>(t => true, true).OrderByDescending(t => t.AddDate);
                if (parameter.RankNumber.HasValue && parameter.RankNumber.Value > 0)
                {
                    //取前N条
                    scHisQuerable = scHisQuerable.Take(parameter.RankNumber.Value);
                }

                int total = scHisQuerable.Count();

                int? page = parameter.Page;
                int? pageSize = parameter.PageSize;
                if (page.HasValue && page.Value > 0 && pageSize.HasValue && pageSize.Value > 0)
                {
                    //分页
                    scHisQuerable = scHisQuerable
                           .Skip((page.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);

                }
                scHisQuerable.ToList().ForEach(t =>
                {
                    ScoreChange sc = new ScoreChange();
                    sc.ItemScore = t.ItemScore;
                    sc.Content = t.Content;
                    sc.AddDate = t.AddDate;

                    result.ScoreChangeHisList.Add(sc);
                });

                result.Total = total;
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

        #region 积分公示【首页及跳转页】
        public BaseResponse<ScorePublicShowResult> ScorePublicShow(ScorePublicShowParameter parameter)
        {
            BaseResponse<ScorePublicShowResult> response = new BaseResponse<ScorePublicShowResult>();
            ScorePublicShowResult result = new ScorePublicShowResult();
            try
            {
                var organArray = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true).ToList();
                var positionArray = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).ToList();
                var levelArray = levelRepository.GetDatas<OfficerLevelType>(t => !t.IsDeleted, true).ToList();

                var officers = officerRepository.GetDatas<Officer>(t => !t.IsDeleted, true).OrderByDescending(t => t.CurrentScore);
                int total = officers.Count();

                officers = officers
                           .Skip((parameter.Page - 1) * parameter.PageSize)
                           .Take(parameter.PageSize).OrderByDescending(t => t.CurrentScore);
                int rank = 1;
                officers.ToList().ForEach(t =>
                       {
                           //计算一个积分公示
                           OfficerScoreShowInfo offPubShow = new OfficerScoreShowInfo();
                           offPubShow.CurrentScore = t.CurrentScore;
                           offPubShow.Rank = rank++;
                           offPubShow.Name = t.Name;
                           offPubShow.Gender = t.Gender;
                           offPubShow.Birthday = t.Birthday;
                           var organ = organArray.Where(o => o.OrganID == t.OrganizationID).FirstOrDefault();
                           if (organ != null)
                           {
                               offPubShow.OrganFullName = organ.OrganFullName;
                           }

                           var position = positionArray.Where(p => p.PositionID == t.PositionID).FirstOrDefault();
                           if (position != null)
                           {
                               offPubShow.PositionName = position.PositionName;
                           }
                           var level = levelArray.Where(l => l.LevelID == t.LevelID).FirstOrDefault();
                           if (level != null)
                           {
                               offPubShow.LevelName = level.LevelName;
                           }

                           offPubShow.OnOfficeDate = t.OnOfficeDate;
                           result.OfficerScoreShowList.Add(offPubShow);
                       });

                result.Total = total;
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
        #endregion
    }
}
