using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetHonourBoardResult
    {
        public List<ScoreRankInfo> RankList { get; set; }

        public GetHonourBoardResult()
        {
            RankList = new List<ScoreRankInfo>();
        }
    }

    public class ScoreRankInfo
    {
        public int OfficerID { get; set; }

        public string OfficerName { get; set; }

        public int Gender { get; set; }

        public string PositionName { get; set; }

        public int CurrentScore { get; set; }
    }

    public class GetBlackBoardResult
    {
        public List<ScoreRankInfo> RankList { get; set; }

        public GetBlackBoardResult()
        {
            RankList = new List<ScoreRankInfo>();
        }
    }
}
