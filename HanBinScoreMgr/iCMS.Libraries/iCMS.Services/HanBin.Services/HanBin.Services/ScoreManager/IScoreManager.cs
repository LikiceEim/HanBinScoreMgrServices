using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.ScoreManager
{
    public interface IScoreManager
    {
        BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter);
    }
}
