using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.SystemManager
{
    public interface ILogManager
    {
        BaseResponse<QueryLogResult> QueryLog(QueryLogParameter parameter);
    }
}
