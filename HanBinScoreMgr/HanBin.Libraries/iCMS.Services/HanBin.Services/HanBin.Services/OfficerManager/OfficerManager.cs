using HanBin.Core.DB.Models;
using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;
using HanBin.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Service.Common;
using HanBin.Common.Component.Data.Response.HanBin.OfficerManager;
using HanBin.Common.Component.Tool;
using HanBin.Common.Commonent.Data.Enum;
using System.ComponentModel;
using HanBin.Common.Component.Data.Base.DB;
using System.Data.Entity;

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
        [Dependency]
        public IRepository<ApplyUploadFile> applyUploadFileRepository { get; set; }
        [Dependency]
        public IRepository<Organization> organRepository { get; set; }

        [Dependency]
        public IRepository<OfficerPositionType> positionRepository { get; set; }
        [Dependency]
        public IRepository<OfficerLevelType> levelRepository { get; set; }

        [Dependency]
        public IRepository<OrganType> organTypeRepository { get; set; }

        [Dependency]
        public IRepository<ScoreChangeHistory> schRepository { get; set; }

        public OfficerManager()
        {
            //解决部署IIS 依赖注入的问题
            if (officerRepository == null)
            {
                officerRepository = new Repository<Officer>();
            }
            if (scoreApplyRepository == null)
            {
                scoreApplyRepository = new Repository<ScoreApply>();
            }
            if (scoreItemRepository == null)
            {
                scoreItemRepository = new Repository<ScoreItem>();
            }
            if (applyUploadFileRepository == null)
            {
                applyUploadFileRepository = new Repository<ApplyUploadFile>();
            }
            if (organRepository == null)
            {
                organRepository = new Repository<Organization>();
            }
            if (positionRepository == null)
            {
                positionRepository = new Repository<OfficerPositionType>();
            }
            if (levelRepository == null)
            {
                levelRepository = new Repository<OfficerLevelType>();
            }

            organTypeRepository = new Repository<OrganType>();
            schRepository = new Repository<ScoreChangeHistory>();
        }

        #region 添加干部
        private void ValidateAddOfficer(iCMSDbContext dbContext, AddOfficerParameter parameter)
        {
            #region 输入验证
            if (string.IsNullOrEmpty(parameter.Name))
            {
                throw new Exception("请输入用户名");
            }
            if (string.IsNullOrEmpty(parameter.IdentifyNumber))
            {
                throw new Exception("请输入身份证号码");
            }
            if (string.IsNullOrEmpty(parameter.OnOfficeDate))
            {
                throw new Exception("请输入任职时间");
            }

            if (parameter.OrganizationID < 1)
            {
                throw new Exception("请选择单位");
            }

            //var isExisted = dbContext.Officers.Where(t => !t.IsDeleted && !string.IsNullOrEmpty(t.Name) && t.Name.Equals(parameter.Name)).Any();
            //if (isExisted)
            //{
            //    throw new Exception("干部名称已重复");
            //}

            if (!Utilitys.CheckIDCard(parameter.IdentifyNumber))
            {
                throw new Exception("请输入合法的身份证号码");
            }

            var isExisted = dbContext.Officers.Where(t => !t.IsDeleted && !string.IsNullOrEmpty(t.IdentifyCardNumber) && t.IdentifyCardNumber.Equals(parameter.IdentifyNumber)).Any();
            if (isExisted)
            {
                throw new Exception("身份证号码重复");
            }

            var addUser = dbContext.HBUsers.Where(t => !t.IsDeleted && t.UserID == parameter.AddUserID).FirstOrDefault();
            if (addUser == null)
            {
                throw new Exception("数据异常");
            }

            var organ = dbContext.Organizations.Where(t => !t.IsDeleted && t.OrganID == addUser.OrganizationID).FirstOrDefault();
            if (organ == null)
            {
                throw new Exception("请选择干部所在单位");
            }
            int roleID = addUser.RoleID;
            switch (roleID)
            {
                case (int)EnumRoleType.SuperAdmin:
                    //超级管理员可以任意单位的干部
                    break;
                case (int)EnumRoleType.FirstLevelAdmin:
                    //一级管理员不能够添加干部
                    throw new Exception("请使用二级管理员登陆，然后添加干部");
                    break;
                case (int)EnumRoleType.SecondLevelAdmin:
                    //二级管理员只能够添加本单位的干部
                    if (addUser.OrganizationID != parameter.OrganizationID)
                    {
                        string msg = string.Format("只能添加本单位({0})的干部", organ.OrganFullName);
                        throw new Exception(msg);
                    }
                    break;
            }

            #endregion
        }

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
                    ValidateAddOfficer(dbContext, parameter);

                    Officer officer = new Officer();
                    officer.Name = parameter.Name;
                    officer.Gender = parameter.Gender;
                    officer.IdentifyCardNumber = parameter.IdentifyNumber;
                    var birth = DateTime.MinValue;
                    var birthdayStr = Utilitys.GetBrithdayFromIdCard(parameter.IdentifyNumber);
                    if (DateTime.TryParse(birthdayStr, out birth))
                    {
                        officer.Birthday = birth;
                    }

                    officer.OrganizationID = parameter.OrganizationID;
                    officer.PositionID = parameter.PositionID;
                    officer.LevelID = parameter.LevelID;

                    DateTime onOfficerDate = DateTime.MinValue;
                    if (DateTime.TryParse(parameter.OnOfficeDate, out onOfficerDate))
                    {
                        officer.OnOfficeDate = onOfficerDate;
                    }
                    officer.InitialScore = parameter.InitialScore;
                    officer.CurrentScore = officer.InitialScore;
                    officer.LastUpdateDate = DateTime.Now;
                    officer.LastUpdateUserID = parameter.AddUserID;
                    officer.AddUserID = parameter.AddUserID;

                    operResult = dbContext.Officers.AddNew<Officer>(dbContext, officer);
                    if (operResult.ResultType != EnumOperationResultType.Success)
                    {
                        throw new Exception("数据库操作异常");
                    }

                    #region 操作日志
                    new LogManager().AddOperationLog(parameter.AddUserID, "添加干部", parameter.RequestIP);
                    #endregion

                    var scoreItemArray = scoreItemRepository.GetDatas<ScoreItem>(t => !t.IsDeleted, true).ToList();

                    int extraScore = 0;
                    if (parameter.ApplyItemList.Any())
                    {
                        parameter.ApplyItemList.ForEach(t =>
                        {
                            ScoreApply scApply = new ScoreApply();
                            scApply.OfficerID = officer.OfficerID;
                            scApply.ItemID = t.ItemID;
                            var tempscoreItem = scoreItemArray.Where(s => s.ItemID == t.ItemID).FirstOrDefault();
                            if (tempscoreItem == null)
                            {
                                throw new Exception("积分条目已被删除");
                            }

                            scApply.ItemScore = tempscoreItem.ItemScore;

                            scApply.ApplyStatus = 1;//自动设置为审批通过
                            scApply.ProposeID = officer.AddUserID;
                            scApply.AddUserID = officer.AddUserID;
                            scApply.LastUpdateDate = DateTime.Now;
                            scApply.LastUpdateUserID = officer.AddUserID;
                            scApply.ApplySummary = t.ApplySummary;
                            scApply.IsDeleted = false;

                            operResult = dbContext.ScoreApplies.AddNew<ScoreApply>(dbContext, scApply);
                            if (operResult.ResultType != EnumOperationResultType.Success)
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

                                var off = dbContext.Officers.Where(o => !o.IsDeleted && o.OfficerID == his.OfficerID).FirstOrDefault();
                                if (off != null)
                                {
                                    var organ = dbContext.Organizations.Where(or => !or.IsDeleted && or.OrganID == off.OrganizationID).FirstOrDefault();
                                    if (organ != null)
                                    {
                                        his.Content = string.Format("{0}  {1} {2}", organ.OrganFullName, off.Name, scoreItem.ItemDescription);
                                        operResult = dbContext.ScoreChangeHistories.AddNew<ScoreChangeHistory>(dbContext, his);
                                        if (operResult.ResultType != EnumOperationResultType.Success)
                                        {
                                            throw new Exception("数据库操作异常");
                                        }
                                    }
                                }
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
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = e.Message;
            }

            return response;
        }
        #endregion

        #region 编辑干部
        public BaseResponse<bool> EditOfficerRecord(EditOfficerParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                #region 输入验证
                if (string.IsNullOrEmpty(parameter.Name))
                {
                    response.IsSuccessful = false;
                    response.Reason = "干部名称不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(parameter.IdentifyNumber))
                {
                    response.IsSuccessful = false;
                    response.Reason = "身份证号不能为空";
                    return response;
                }
                if (string.IsNullOrEmpty(parameter.OnOfficeDate))
                {
                    response.IsSuccessful = false;
                    response.Reason = "请输入任职时间";
                    return response;
                }

                if (parameter.OrganizationID < 1)
                {
                    response.IsSuccessful = false;
                    response.Reason = "请选择单位";
                    return response;
                }

                //var isExisted = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.Name) && t.OfficerID != parameter.OfficerID && t.Name.Equals(parameter.Name), true).Any();
                //if (isExisted)
                //{
                //    response.IsSuccessful = false;
                //    response.Reason = "干部名称已存在";
                //    return response;
                //}

                var isExisted = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && !string.IsNullOrEmpty(t.IdentifyCardNumber) && t.IdentifyCardNumber.Equals(parameter.IdentifyNumber) && t.OfficerID != parameter.OfficerID, true).Any();
                if (isExisted)
                {
                    throw new Exception("身份证号码重复");
                }

                if (!Utilitys.CheckIDCard(parameter.IdentifyNumber))
                {
                    throw new Exception("请输入合法的身份证号码");
                }
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var updateUser = dbContext.HBUsers.Where(t => !t.IsDeleted && t.UserID == parameter.UpdateUserID).FirstOrDefault();
                    if (updateUser == null)
                    {
                        throw new Exception("数据异常");
                    }

                    var organ = dbContext.Organizations.Where(t => !t.IsDeleted && t.OrganID == updateUser.OrganizationID).FirstOrDefault();
                    if (organ == null)
                    {
                        throw new Exception("数据异常");
                    }

                    int roleID = updateUser.RoleID;
                    switch (roleID)
                    {
                        case (int)EnumRoleType.SuperAdmin:
                            //超级管理员可以编辑任意单位的干部
                            break;
                        case (int)EnumRoleType.FirstLevelAdmin:
                            //一级管理员不能够编辑干部
                            throw new Exception("请使用二级管理员登陆，然后编辑干部");
                            break;
                        case (int)EnumRoleType.SecondLevelAdmin:
                            //二级管理员只能够添加本单位的干部
                            if (updateUser.OrganizationID != parameter.OrganizationID)
                            {
                                string msg = string.Format("只能修改本单位({0})的干部", organ.OrganFullName);
                                throw new Exception(msg);
                            }
                            break;
                    }

                    //if (updateUser.OrganizationID != parameter.OrganizationID)
                    //{
                    //    string msg = string.Format("只能修改本单位({0})的干部", organ.OrganFullName);
                    //    throw new Exception(msg);
                    //}
                }

                #endregion

                var offIndb = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID, true).FirstOrDefault();
                if (offIndb == null)
                {
                    response.IsSuccessful = false;
                    response.Reason = "编辑干部数据异常";
                    return response;
                }
                offIndb.Name = parameter.Name;
                offIndb.Gender = parameter.Gender;
                offIndb.IdentifyCardNumber = parameter.IdentifyNumber;

                var birth = Utilitys.GetBrithdayFromIdCard(parameter.IdentifyNumber);
                var birthDay = DateTime.MinValue;
                if (DateTime.TryParse(birth, out birthDay))
                {
                    offIndb.Birthday = birthDay;
                }

                offIndb.OrganizationID = parameter.OrganizationID;
                offIndb.PositionID = parameter.PositionID;
                offIndb.LevelID = parameter.LevelID;

                var onOfficeDate = DateTime.MinValue;
                if (DateTime.TryParse(parameter.OnOfficeDate, out onOfficeDate))
                {
                    offIndb.OnOfficeDate = onOfficeDate;
                }

                offIndb.Duty = parameter.Duty;
                //offIndb.InitialScore = parameter.InitialScore;

                var res = officerRepository.Update<Officer>(offIndb);
                if (res.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("编辑干部发生异常");
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

                var organ = organRepository.GetDatas<Organization>(t => !t.IsDeleted && t.OrganID == result.OrganizationID, true).FirstOrDefault();
                if (organ == null)
                {
                    throw new Exception();
                }
                result.OrganFullName = organ.OrganFullName;
                result.OrganShortName = organ.OrganShortName;
                result.OrganTypeID = organ.OrganTypeID;
                var organType = organTypeRepository.GetDatas<OrganType>(t => !t.IsDeleted && t.OrganTypeID == result.OrganTypeID, true).FirstOrDefault();
                if (organType == null)
                {
                    throw new Exception();
                }
                result.OrganTypeName = organType.OrganTypeName;
                var position = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted && t.PositionID == result.PositionID, true).FirstOrDefault();
                if (position != null)
                {
                    result.PositionName = position.PositionName;
                }
                var level = levelRepository.GetDatas<OfficerLevelType>(t => !t.IsDeleted && t.LevelID == result.LevelID, true).FirstOrDefault();
                if (level != null)
                {
                    result.LevelName = level.LevelName;
                }

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "获取干部详细信息", parameter.RequestIP);
                #endregion

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
                var applovedScoreApplyList = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID && t.ApplyStatus == (int)EnumApproveStatus.Pass, true).ToList();
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
                            applyItenInfo.ItemScore = t.ItemScore;//scoreItem.ItemScore;
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
                            applyItenInfo.ItemScore = t.ItemScore; //scoreItem.ItemScore;
                            applyItenInfo.ItemDescription = scoreItem.ItemDescription;
                        }

                        result.ApprovingApplyItemList.Add(applyItenInfo);
                    });
                }

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "获取干部积分信息", parameter.RequestIP);
                #endregion

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

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "获取积分申请详细信息", parameter.RequestIP);
                #endregion

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
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("撤销积分申请时，数据库操作发生异常");
                }

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "获取积分申请详细信息", parameter.RequestIP);
                #endregion

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
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("删除干部时，数据库操作发生异常");
                }

                Dictionary<EntityBase, EntityState> opers = new Dictionary<EntityBase, EntityState>();

                #region 删除干部时候，同时删除此干部的积分申请信息（包含审批过与未审批过的） 以及积分变更记录
                var relatedScoreApplies = scoreApplyRepository.GetDatas<ScoreApply>(t => !t.IsDeleted && t.OfficerID == officer.OfficerID, true).ToList();
                relatedScoreApplies.ForEach(p =>
                {
                    p.IsDeleted = true;
                    opers.Add(p, EntityState.Modified);
                });

                var schList = schRepository.GetDatas<ScoreChangeHistory>(t => !t.IsDeleted && t.OfficerID == officer.OfficerID, true).ToList();
                schList.ForEach(t =>
                {
                    t.IsDeleted = true;
                    opers.Add(t, EntityState.Modified);
                });

                operResult = officerRepository.TranMethod(opers);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("删除干部相关数据发生异常");
                }
                #endregion

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, "删除干部", parameter.RequestIP);
                #endregion

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

        #region 设置干部退休
        public BaseResponse<bool> SetOfficerOffService(SetOfficerOffService parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var officer = officerRepository.GetDatas<Officer>(t => !t.IsDeleted && t.OfficerID == parameter.OfficerID, true).FirstOrDefault();
                if (officer == null)
                {
                    throw new Exception("数据异常");
                }
                officer.IsOnService = false;
                var operResult = officerRepository.Update<Officer>(officer);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("设置干部退休时，数据库操作发生异常");
                }

                #region 操作日志
                new LogManager().AddOperationLog(parameter.CurrentUserID, string.Format("设置干部:{0}退休", officer.Name), parameter.RequestIP);
                #endregion

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
            if (string.IsNullOrEmpty(parameter.Sort))
            {
                parameter.Sort = "OfficerID";
                parameter.Order = "asc";
            }
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var officerQuerable = dbContext.Officers.Where(t => !t.IsDeleted && t.IsOnService);
                    var currentUser = dbContext.HBUsers.Where(t => !t.IsDeleted && t.UserID == parameter.CurrentUserID).FirstOrDefault();
                    if (currentUser == null)
                    {
                        throw new Exception("数据异常");
                    }
                    if (currentUser.RoleID == 4)//如果是二级管理员，则只返回本单位的干部
                    {
                        officerQuerable = officerQuerable.Where(t => t.OrganizationID == currentUser.OrganizationID);
                    }

                    if (parameter.OrganizationID.HasValue && parameter.OrganizationID.Value > 0)
                    {
                        officerQuerable = officerQuerable.Where(t => t.OrganizationID == parameter.OrganizationID);
                    }
                    if (parameter.LevelID.HasValue && parameter.LevelID.Value > 0)
                    {
                        officerQuerable = officerQuerable.Where(t => t.LevelID == parameter.LevelID.Value);

                    }

                    var officerLinq = from off in officerQuerable
                                      join org in dbContext.Organizations
                                      on off.OrganizationID equals org.OrganID
                                      into group1
                                      from g1 in group1
                                      join pos in dbContext.OfficerPositionTypes.Where(t => !t.IsDeleted) on off.PositionID equals pos.PositionID into group2
                                      from g2 in group2
                                      join lev in dbContext.OfficerLevelTypes.Where(t => !t.IsDeleted) on off.LevelID equals lev.LevelID into group3
                                      from g3 in group3
                                      //where org.OrganFullName.ToUpper().Contains(parameter.Keyword.ToUpper()) || org.OrganCode.ToUpper().Contains(parameter.Keyword.ToUpper())
                                      select new OfficerDetailInfo
                                      {
                                          OfficerID = off.OfficerID,
                                          Name = off.Name,
                                          Gender = off.Gender,
                                          Birthday = off.Birthday,
                                          OrganizationName = g1.OrganFullName,
                                          PositionID = g2.PositionID,
                                          PositionName = g2.PositionName,
                                          LevelID = g3.LevelID,
                                          LevelName = g3.LevelName,
                                          OnOfficeDate = off.OnOfficeDate,
                                          OrganizationID = off.OrganizationID,
                                          CurrentScore = off.CurrentScore,
                                          IdentifyNumber = off.IdentifyCardNumber
                                      };


                    if (!string.IsNullOrEmpty(parameter.Keyword))
                    {
                        officerLinq = officerLinq.Where(t => t.Name.Contains(parameter.Keyword) || t.IdentifyNumber.Contains(parameter.Keyword) || t.OrganizationName.Contains(parameter.Keyword));
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

                    #region 操作日志
                    new LogManager().AddOperationLog(parameter.CurrentUserID, "获取积分申请详细信息", parameter.RequestIP);
                    #endregion

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

        #region 添加干部时候，获取单位信息
        public BaseResponse<GetOrganSummaryResult> GetOrganSummary(GetOrganSummaryParameter parameter)
        {
            BaseResponse<GetOrganSummaryResult> response = new BaseResponse<GetOrganSummaryResult>();
            GetOrganSummaryResult result = new GetOrganSummaryResult();
            try
            {
                var organQuerable = organRepository.GetDatas<Organization>(t => !t.IsDeleted, true);
                if (parameter.OrganTypeID.HasValue && parameter.OrganTypeID.Value > 0)
                {
                    organQuerable = organQuerable.Where(t => t.OrganTypeID == parameter.OrganTypeID.Value);
                }

                var organList = organQuerable.Select(t => new OrganSummaryInfo
                {
                    OrganID = t.OrganID,
                    OrganFullName = t.OrganFullName,
                    OrganTypeID = t.OrganTypeID
                }).ToList();

                result.OrganList.AddRange(organList);
                response.Result = result;

                return response;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion

        #region 添加干部时候，获取职位信息
        public BaseResponse<GetPositionListResult> GetPositionSummary()
        {
            BaseResponse<GetPositionListResult> response = new BaseResponse<GetPositionListResult>();
            GetPositionListResult result = new GetPositionListResult();
            try
            {
                var positionList = positionRepository.GetDatas<OfficerPositionType>(t => !t.IsDeleted, true).Select(t => new PositionSummaryInfo
                {
                    PositionID = t.PositionID,
                    PositionName = t.PositionName
                }).ToList();

                result.PositionList.AddRange(positionList);
                response.Result = result;

                return response;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion

        #region 添加干部时候，获取级别信息
        public BaseResponse<GetLevelListResult> GetLevelSummary()
        {
            BaseResponse<GetLevelListResult> response = new BaseResponse<GetLevelListResult>();
            GetLevelListResult result = new GetLevelListResult();
            try
            {
                var positionList = levelRepository.GetDatas<OfficerLevelType>(t => !t.IsDeleted, true).Select(t => new LevelSummaryInfo
                {
                    LevelID = t.LevelID,
                    LevelName = t.LevelName
                }).ToList();

                result.LevelList.AddRange(positionList);
                response.Result = result;

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
