using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetScoreItemListResult
    {
        public List<ScoreItemInfo> ScoreItemInfoList { get; set; }

        public GetScoreItemListResult()
        {
            this.ScoreItemInfoList = new List<ScoreItemInfo>();
        }
    }

    public class ScoreItemInfo
    {
        public int ItemID { get; set; }

        public int ItemScore { get; set; }

        public string ItemDescription { get; set; }

        public int Type { get; set; }
    }
}
