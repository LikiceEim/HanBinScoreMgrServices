using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using iCMS.Common.Component.Data.Response.HanBin.ScoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.ScoreManager
{
    public interface IScoreManager
    {
        BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter);

        BaseResponse<bool> EditScoreItem(EditScoreItemParameter parameter);

        BaseResponse<bool> DeleteScoreItem(DeleteScoreItemParameter parameter);

        BaseResponse<GetScoreItemListResult> GetScoreItemList();

        BaseResponse<bool> AddScoreApply(AddScoreApplyParameter parameter);

        BaseResponse<bool> EditScoreApply(EditScoreApplyParameter parameter);

        BaseResponse<bool> CheckScoreApply(CheckScoreApplyParameter parameter);
    }
}
