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
    }
}
