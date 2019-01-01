using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class AreaAverageScoreResult
    {
        public List<AreaAverageScoreItem> AreaAverageScoreItemList { get; set; }

        public AreaAverageScoreResult()
        {
            this.AreaAverageScoreItemList = new List<AreaAverageScoreItem>();
        }
    }

    public class AreaAverageScoreItem
    {
        public int AreaID { get; set; }

        public string AreaName { get; set; }

        public double AverageScore { get; set; }
    }
}
