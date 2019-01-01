using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetHighLevelFeedBackSummaryResult
    {
        public List<FeedBackSummaryInfo> FeedBackList { get; set; }

        public GetHighLevelFeedBackSummaryResult()
        {
            this.FeedBackList = new List<FeedBackSummaryInfo>();
        }
    }

    public class FeedBackSummaryInfo
    {
        public int ApplyID { get; set; }

        public string FeedBackTitle { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
