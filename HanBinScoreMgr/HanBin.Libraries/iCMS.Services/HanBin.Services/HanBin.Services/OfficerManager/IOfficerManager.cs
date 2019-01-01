using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;
using HanBin.Common.Component.Data.Response.HanBin.OfficerManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.OfficerManager
{
    public interface IOfficerManager
    {
        BaseResponse<bool> AddOfficerRecord(AddOfficerParameter parameter);

        BaseResponse<GetOfficerDetailInfoResult> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter);

        BaseResponse<GetOfficerScoreDetailInfoResult> GetOfficerScoreDetailInfo(GetOfficerScoreDetailInfoParameter parameter);

        BaseResponse<GetApplyDetailInfoResult> GetApplyDetailInfo(GetApplyDetailInfoParameter parameter);

        BaseResponse<bool> CancelScoreApply(CancelScoreApplyParameter parameter);

        BaseResponse<bool> DeleteOfficerRecord(DeleteOfficerParameter parameter);

        BaseResponse<GetOfficerListResult> GetOfficerList(GetOfficerListParameter parameter);

        BaseResponse<GetOrganSummaryResult> GetOrganSummary();

        BaseResponse<GetPositionListResult> GetPositionSummary();

        BaseResponse<GetLevelListResult> GetLevelSummary();
    }
}
