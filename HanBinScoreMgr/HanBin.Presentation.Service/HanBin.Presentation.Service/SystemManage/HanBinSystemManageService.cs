using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Services.SystemManager;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.SystemManage;
using iCMS.Common.Component.Data.Response.HanBin.SystemManager;
using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
using HanBin.Services.OrganManager;
using iCMS.Common.Component.Data.Response.HanBinOrganManager;
using iCMS.Common.Component.Data.Request.HanBin.OfficerManager;
using HanBin.Services.OfficerManager;
using iCMS.Common.Component.Data.Response.HanBin.OfficerManager;

namespace HanBin.Presentation.Service.SystemManage
{
    public class HanBinSystemManageService : IHanBinSystemManageService
    {
        private IUserManager userManager;
        private IOrganManager organManager;
        private IOfficerManager officerManager;

        public HanBinSystemManageService(IUserManager userManager, IOrganManager organManager, IOfficerManager officerManager)
        {
            this.userManager = userManager;
            this.organManager = organManager;
            this.officerManager = officerManager;
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
        #endregion
    }
}
