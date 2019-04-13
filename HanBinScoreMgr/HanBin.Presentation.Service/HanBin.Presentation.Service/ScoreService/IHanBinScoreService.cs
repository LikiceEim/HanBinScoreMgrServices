using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.ScoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using HanBin.Common.Component.Data.Response.HanBin.ScoreManager;
using System.IO;
using System.Runtime.Serialization;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;

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
        BaseResponse<GetScoreItemListResult> GetScoreItemList(BaseRequest param);

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
        BaseResponse<bool> CancelScoreApply(CancelScoreApplyParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<SystemStatSummaryResult> SystemStatSummary(SystemStatSummaryParameter param);

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

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetScoreChangeHistoryResult> GetScoreChangeHistory(GetScoreChangeHistoryParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ScorePublicShowResult> ScorePublicShow(ScorePublicShowParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<QuerySocreResult> QuerySocre(QuerySocreParameter parameter);


        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<AreaAverageScoreResult> AreaAverageScore(BaseRequest param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<AgeAverageScoreResult> AgeAverageScore(AgeAverageScoreParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<OrganAverageScoreResult> OrganAverageScore(OrganAverageScoreParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<OrganCategoryAverageScoreResult> OrganCategoryAverageScore(BaseRequest parameter);

        //[WebInvoke(Method = "POST", UriTemplate = "UpLoad/{fileName}",
        //ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //[System.ComponentModel.Description("上传文件")]
        //bool UpLoad(System.IO.Stream stream, string fileName);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteFile(DeleteFileParameter parameter);

        [WebInvoke(UriTemplate = "UploadFile/{filename}", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BaseResponse<UpFileResult> UploadFile(string filename, Stream FileStream);

        [WebGet(UriTemplate = "DownloadFile/{fileName}")]
        Stream DownLoadFile(string fileName);
    }

    [DataContract]
    public class UpFile : BaseRequest
    {
        [DataMember]
        public long FileSize { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public Stream FileStream { get; set; }

    }


    public class DownLoadFileParameter : BaseRequest
    {
        public string FilePath { get; set; }
    }

    public class UpFileResult
    {
        public string FilePath { get; set; }
    }
}
