using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
using iCMS.Common.Component.Data.Response.HanBinOrganManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.OrganManager
{
    public interface IOrganManager
    {
        BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param);

        BaseResponse<GetOrganDetailInfoResult> GetOrganDetailInfo(GetOrganDetailInfoParameter parameter);

        BaseResponse<bool> EditOrganizationRecord(EditOrganParameter parameter);

        BaseResponse<bool> DeleteOrganRecord(DeleteOrganParameter param);

        BaseResponse<GetOrganListResult> GetOrganList(GetOrganInfoListParameter parameter);
    }
}
