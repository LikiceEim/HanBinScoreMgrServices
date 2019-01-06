using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class GetScoreChangeHistoryParameter : BaseRequest
    {
        public int? RankNumber { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
