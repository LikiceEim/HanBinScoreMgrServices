using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Services.SystemManager;
using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using HanBin.Common.Component.Data.Request.HanBin.OrganManage;
using HanBin.Services.OrganManager;
using HanBin.Common.Component.Data.Response.HanBinOrganManager;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;
using HanBin.Services.OfficerManager;
using HanBin.Common.Component.Data.Response.HanBin.OfficerManager;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace HanBin.Presentation.Service.SystemManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    public class HanBinSystemManageService : IHanBinSystemManageService
    {
        private IUserManager userManager;
        private IOrganManager organManager;
        private IOfficerManager officerManager;
        private ILogManager logManager;

        public HanBinSystemManageService()
        {
            this.userManager = new UserManager();
            this.organManager = new OrganManager();
            this.officerManager = new OfficerManager();
            this.logManager = new LogManager();
        }

        #region 用户管理
        public BaseResponse<LoginResult> Login(LoginParameter parameter)
        {
            return userManager.Login(parameter);
        }

        public BaseResponse<bool> AddUser(AddUserParameter parameter)
        {
            return userManager.AddUser(parameter);
        }

        public BaseResponse<bool> EditUser(EditUserParameter parameter)
        {
            return userManager.EditUser(parameter);
        }

        public BaseResponse<GetUserInfoResult> GetUserInfo(GetUserInfoParameter parameter)
        {
            return userManager.GetUserInfo(parameter);
        }
        #endregion

        #region 单位管理
        public BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param)
        {
            return organManager.AddOrganizationRecord(param);
        }

        #region 获取单位详细信息
        public BaseResponse<GetOrganDetailInfoResult> GetOrganDetailInfo(GetOrganDetailInfoParameter parameter)
        {
            return organManager.GetOrganDetailInfo(parameter);
        }
        #endregion

        #region 编辑单位
        public BaseResponse<bool> EditOrganizationRecord(EditOrganParameter parameter)
        {
            return organManager.EditOrganizationRecord(parameter);
        }
        #endregion

        #region 删除单位
        public BaseResponse<bool> DeleteOrganRecord(DeleteOrganParameter param)
        {
            return organManager.DeleteOrganRecord(param);
        }
        #endregion

        #region 获取单位列表
        public BaseResponse<GetOrganListResult> GetOrganList(GetOrganInfoListParameter parameter)
        {
            return organManager.GetOrganList(parameter);
        }
        #endregion
        #endregion

        #region 干部管理
        #region 添加干部
        public BaseResponse<bool> AddOfficerRecord(AddOfficerParameter parameter)
        {
            return officerManager.AddOfficerRecord(parameter);
        }
        #endregion

        #region 获取干部详细信息（不包含积分）
        public BaseResponse<GetOfficerDetailInfoResult> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter)
        {
            return officerManager.GetOfficerDetailInfo(parameter);
        }
        #endregion

        #region 获取干部积分信息
        public BaseResponse<GetOfficerScoreDetailInfoResult> GetOfficerScoreDetailInfo(GetOfficerScoreDetailInfoParameter parameter)
        {
            return officerManager.GetOfficerScoreDetailInfo(parameter);
        }
        #endregion

        #region 编辑积分申请时候，获取积分申请详细信息
        public BaseResponse<GetApplyDetailInfoResult> GetApplyDetailInfo(GetApplyDetailInfoParameter parameter)
        {
            return officerManager.GetApplyDetailInfo(parameter);
        }
        #endregion

        #region 撤销积分申请
        public BaseResponse<bool> CancelScoreApply(CancelScoreApplyParameter parameter)
        {
            return officerManager.CancelScoreApply(parameter);
        }
        #endregion

        #region 删除干部
        public BaseResponse<bool> DeleteOfficerRecord(DeleteOfficerParameter parameter)
        {
            return officerManager.DeleteOfficerRecord(parameter);
        }
        #endregion

        #region 获取单位列表
        public BaseResponse<GetOfficerListResult> GetOfficerList(GetOfficerListParameter parameter)
        {
            return officerManager.GetOfficerList(parameter);
        }
        #endregion

        public BaseResponse<GetOrganTypeResult> GetOrganTypeList()
        {
            return organManager.GetOrganTypeList();
        }
        #endregion


        public BaseResponse<GetOrganSummaryResult> GetOrganSummary()
        {
            return officerManager.GetOrganSummary();
        }

        public BaseResponse<GetPositionListResult> GetPositionSummary()
        {
            return officerManager.GetPositionSummary();
        }

        public BaseResponse<GetLevelListResult> GetLevelSummary()
        {
            return officerManager.GetLevelSummary();
        }

        public BaseResponse<GetRoleInfoListResult> GetRoleInfoList()
        {
            var headers = WebOperationContext.Current.IncomingRequest.Headers;
            return userManager.GetRoleInfoList();
        }

        public BaseResponse<bool> ChangeUseStatus(ChangeUseStatusParameter parameter)
        {
            return userManager.ChangeUseStatus(parameter);
        }

        public BaseResponse<bool> DeleteUser(DeleteUserParameter parameter)
        {
            return userManager.DeleteUser(parameter);
        }

        public BaseResponse<bool> ResetPWD(ResetPWDParameter parameter)
        {
            return userManager.ResetPWD(parameter);
        }

        public BaseResponse<GetAreaListResult> GetAreaList()
        {
            return organManager.GetAreaList();
        }

        public BaseResponse<QueryLogResult> QueryLog(QueryLogParameter param)
        {
            return logManager.QueryLog(param);
        }

        public BaseResponse<bool> BackupDB(BackupDBParameter param)
        {
            return userManager.BackupDB(param);
        }
    }
}
