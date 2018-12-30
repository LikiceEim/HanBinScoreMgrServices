using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class GetScoreChangeHistoryResult
    {
        public List<ScoreChange> ScoreChangeHisList { get; set; }

        public int Total { get; set; }

        public GetScoreChangeHistoryResult()
        {
            ScoreChangeHisList = new List<ScoreChange>();
        }
    }

    public class ScoreChange
    {
        public int ItemScore { get; set; }

        public string Content { get; set; }

        public DateTime AddDate { get; set; }
    }
}
