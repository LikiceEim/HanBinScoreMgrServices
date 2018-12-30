using HanBin.Services.ScoreManager;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using iCMS.Common.Component.Data.Response.HanBin.ScoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Presentation.Service.ScoreService
{
    public class HanBinScoreService : IHanBinScoreService
    {
        public IScoreManager scoreManager { get; set; }

        public HanBinScoreService(IScoreManager scoreManager)
        {
            this.scoreManager = scoreManager;
        }

        public BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter)
        {
            return scoreManager.AddScoreItem(parameter);
        }

        public BaseResponse<bool> EditScoreItem(EditScoreItemParameter parameter)
        {
            return scoreManager.EditScoreItem(parameter);
        }

        public BaseResponse<bool> DeleteScoreItem(DeleteScoreItemParameter parameter)
        {
            return scoreManager.DeleteScoreItem(parameter);
        }
        public BaseResponse<GetScoreItemListResult> GetScoreItemList()
        {
            return scoreManager.GetScoreItemList();
        }

        public BaseResponse<bool> AddScoreApply(AddScoreApplyParameter parameter)
        {
            return scoreManager.AddScoreApply(parameter);
        }

        public BaseResponse<bool> EditScoreApply(EditScoreApplyParameter parameter)
        {
            return scoreManager.EditScoreApply(parameter);
        }

        public BaseResponse<bool> CheckScoreApply(CheckScoreApplyParameter parameter)
        {
            return scoreManager.CheckScoreApply(parameter);
        }

        public BaseResponse<SystemStatSummaryResult> SystemStatSummary()
        {
            return scoreManager.SystemStatSummary();
        }

        public BaseResponse<GetHonourBoardResult> GetHonourBoard(GetHonourBoardParameter parameter)
        {
            return scoreManager.GetHonourBoard(parameter);
        }

        public BaseResponse<GetBlackBoardResult> GetBlackBoard(GetBlackBoardParameter parameter)
        {
            return scoreManager.GetBlackBoard(parameter);
        }

        public BaseResponse<WhatsToDoSummaryResult> GetWhatsToDoSummary(GetWhatsToDoSummaryParameter parameter)
        {
            return scoreManager.GetWhatsToDoSummary(parameter);
        }

        public BaseResponse<GetWhatsToDoDetailListResult> GetWhatsToDoDetailList(GetWhatsToDoDetailListParameter parameter)
        {
            return scoreManager.GetWhatsToDoDetailList(parameter);
        }

        public BaseResponse<GetHighLevelFeedBackSummaryResult> GetHighLevelFeedBackSummary(GetHighLevelFeedBackSummaryParameter parameter)
        {
            return scoreManager.GetHighLevelFeedBackSummary(parameter);
        }

        public BaseResponse<GetHighLevelFeedBackDetailListResult> GetHighLevelFeedBackDetailList(GetHighLevelFeedBackDetailListParameter parameter)
        {
            return scoreManager.GetHighLevelFeedBackDetailList(parameter);
        }

        public BaseResponse<GetScoreChangeHistoryResult> GetScoreChangeHistory(GetScoreChangeHistoryParameter parameter)
        {
            return scoreManager.GetScoreChangeHistory(parameter);
        }

        public BaseResponse<ScorePublicShowResult> ScorePublicShow(ScorePublicShowParameter parameter)
        {
            return scoreManager.ScorePublicShow(parameter);
        }

        public BaseResponse<QuerySocreResult> QuerySocre(QuerySocreParameter parameter)
        {
            return scoreManager.QuerySocre(parameter);
        }

        public BaseResponse<AreaAverageScoreResult> AreaAverageScore()
        {
            return scoreManager.AreaAverageScore();
        }

        public BaseResponse<AgeAverageScoreResult> AreaAverageScore(AgeAverageScoreParameter parameter)
        {
            return scoreManager.AreaAverageScore(parameter);
        }

        public BaseResponse<OrganAverageScoreResult> OrganAverageScore(OrganAverageScoreParameter parameter)
        {
            return scoreManager.OrganAverageScore(parameter);
        }
    }
}
