using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class OrganAverageScoreResult
    {
        public List<OrganAverageScoreItem> OrganAverageScoreItemList { get; set; }

        public OrganAverageScoreResult() 
        {
            this.OrganAverageScoreItemList = new List<OrganAverageScoreItem>();
        }
    }

    public class OrganAverageScoreItem
    {
        public double CurrentScore { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public string OrganName { get; set; }

        public string PositionName { get; set; }

        public string LevelName { get; set; }
    }
}
