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

namespace HanBin.Services.OfficerManager
{
    public class OfficerManager : IOfficerManager
    {
        [Dependency]
        public IRepository<Officer> officerRepository { get; set; }

        #region 添加干部
        public BaseResponse<bool> AddOfficerRecord(AddOfficerParameter parameter)
        {
            //1. 添加干部基础信息
            //2. 添加此干部的积分申请（并设置为审批通过）
            //3. 修改当前积分
            //4. 添加积分变更记录
            BaseResponse<bool> response = new BaseResponse<bool>();
            //iCMSDbContext dbContext = new iCMSDbContext();

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
                    dbContext.Officers.AddNew<Officer>(dbContext, officer);

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
                            scApply.ApplySummary = t.ApplySummary;
                            scApply.IsDeleted = false;

                            dbContext.ScoreApplies.AddNew<ScoreApply>(dbContext, scApply);
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
                            var scoreItem = dbContext.ScoreItems.Where(x => x.ItemID == his.ItemID).First();
                            his.Content = string.Format("{0}  {1} {2}", his.ItemScore, his.AddDate, scoreItem.ItemDescription);

                            dbContext.ScoreChangeHistories.AddNew<ScoreChangeHistory>(dbContext, his);

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
        public BaseResponse<bool> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter)
        {
            return null;
        }

        #endregion
    }
}
