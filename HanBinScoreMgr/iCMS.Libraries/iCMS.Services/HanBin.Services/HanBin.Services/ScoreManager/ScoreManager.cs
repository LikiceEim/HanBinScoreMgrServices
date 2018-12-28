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
    }
}
