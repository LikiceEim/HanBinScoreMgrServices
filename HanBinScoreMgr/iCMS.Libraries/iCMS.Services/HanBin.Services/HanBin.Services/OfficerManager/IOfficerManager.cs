using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.OfficerManager;
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

        BaseResponse<bool> GetOfficerDetailInfo(GetOfficerDetailInfoParameter parameter);
    }
}
