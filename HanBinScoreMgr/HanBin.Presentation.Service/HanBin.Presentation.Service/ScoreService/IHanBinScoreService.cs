using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using iCMS.Common.Component.Data.Response.HanBin.ScoreManager;

namespace HanBin.Presentation.Service.ScoreService
{
    [ServiceContract]
    public interface IHanBinScoreService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditScoreItem(EditScoreItemParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteScoreItem(DeleteScoreItemParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetScoreItemListResult> GetScoreItemList();

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddScoreApply(AddScoreApplyParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditScoreApply(EditScoreApplyParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> CheckScoreApply(CheckScoreApplyParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<SystemStatSummaryResult> SystemStatSummary();

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetHonourBoardResult> GetHonourBoard(GetHonourBoardParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetBlackBoardResult> GetBlackBoard(GetBlackBoardParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WhatsToDoSummaryResult> GetWhatsToDoSummary(GetWhatsToDoSummaryParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWhatsToDoDetailListResult> GetWhatsToDoDetailList(GetWhatsToDoDetailListParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetHighLevelFeedBackSummaryResult> GetHighLevelFeedBackSummary(GetHighLevelFeedBackSummaryParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetHighLevelFeedBackDetailListResult> GetHighLevelFeedBackDetailList(GetHighLevelFeedBackDetailListParameter parameter);
    }
}
