using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;
using HanBin.Common.Component.Data.Request.HanBin.OrganManage;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.OfficerManager;
using HanBin.Common.Component.Data.Response.HanBin.ScoreManager;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using HanBin.Common.Component.Data.Response.HanBinOrganManager;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HanBin.Presentation.Service.SystemManage
{

    [ServiceContract]
    public interface IHanBinSystemManageService
    {
        #region 用户管理
        [WebInvoke(UriTemplate = "Login",
              BodyStyle = WebMessageBodyStyle.Bare,
              Method = "POST",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<LoginResult> Login(LoginParameter parameter);

        [WebInvoke(UriTemplate = "AddUser",
             BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddUser(AddUserParameter parameter);

        [WebInvoke(UriTemplate = "EditUser",
             BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditUser(EditUserParameter parameter);

        [WebInvoke(UriTemplate = "GetUserInfo",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetUserInfoResult> GetUserInfo(GetUserInfoParameter parameter);
        #endregion

        #region 单位管理
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOrganDetailInfoResult> GetOrganDetailInfo(GetOrganDetailInfoParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditOrganizationRecord(EditOrganParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteOrganRecord(DeleteOrganParameter param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOrganListResult> GetOrganList(GetOrganInfoListParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOrganTypeResult> GetOrganTypeList(BaseRequest param);
        #endregion

        #region 干部管理
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddOfficerRecord(AddOfficerParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOfficerDetailInfoResult> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOfficerScoreDetailInfoResult> GetOfficerScoreDetailInfo(GetOfficerScoreDetailInfoParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetApplyDetailInfoResult> GetApplyDetailInfo(GetApplyDetailInfoParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> CancelScoreApply(CancelScoreApplyParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteOfficerRecord(DeleteOfficerParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> SetOfficerOffService(SetOfficerOffService parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOfficerListResult> GetOfficerList(GetOfficerListParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditOfficerRecord(EditOfficerParameter parameter);
        #endregion

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetOrganSummaryResult> GetOrganSummary(BaseRequest parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetPositionListResult> GetPositionSummary(BaseRequest param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetLevelListResult> GetLevelSummary(BaseRequest parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetRoleInfoListResult> GetRoleInfoList(BaseRequest param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> ChangeUseStatus(ChangeUseStatusParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteUser(DeleteUserParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> ResetPWD(ResetPWDParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetAreaListResult> GetAreaList(BaseRequest param);


        #region 日志
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
              Method = "POST",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<QueryLogResult> QueryLog(QueryLogParameter param);
        #endregion

        #region 数据库备份
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> BackupDB(BaseRequest param);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetBackupLogResult> GetBackupLogList(GetBackupLogParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteBackup(DeleteBackupParameter parameter);
        #endregion

    }
}
