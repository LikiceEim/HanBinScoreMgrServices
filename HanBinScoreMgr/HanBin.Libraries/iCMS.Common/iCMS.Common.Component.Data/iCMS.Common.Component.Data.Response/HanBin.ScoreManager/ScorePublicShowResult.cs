using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class ScorePublicShowResult
    {
        public List<OfficerScoreShowInfo> OfficerScoreShowList { get; set; }

        public int Total { get; set; }

        public ScorePublicShowResult()
        {
            this.OfficerScoreShowList = new List<OfficerScoreShowInfo>();
        }
    }

    public class OfficerScoreShowInfo
    {
        public int CurrentScore { get; set; }

        public int Rank { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string OrganFullName { get; set; }

        public string PositionName { get; set; }

        public string LevelName { get; set; }

        public DateTime OnOfficeDate { get; set; }
    }
}
