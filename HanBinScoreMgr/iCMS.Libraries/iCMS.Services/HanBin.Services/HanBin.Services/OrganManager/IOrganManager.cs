using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
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
    }
}
