using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.SystemManager
{
    public interface ILogManager
    {
        BaseResponse<bool> QueryLog();

    }
}
