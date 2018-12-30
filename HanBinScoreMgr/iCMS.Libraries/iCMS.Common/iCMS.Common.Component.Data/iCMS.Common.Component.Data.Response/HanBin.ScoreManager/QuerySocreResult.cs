using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class QuerySocreResult
    {
        public List<QueryScoreItem> QueryScoreItemList { get; set; }

        public int Total { get; set; }

        public QuerySocreResult()
        {
            this.QueryScoreItemList = new List<QueryScoreItem>();
        }
    }

    public class QueryScoreItem
    {
        public int CurrentScore { get; set; }

        public int OfficerID { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }
    }
}
