using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class AgeAverageScoreResult
    {
        public List<AgeAverageScoreItem> AgeAverageScoreItemList { get; set; }

        public AgeAverageScoreResult()
        {
            this.AgeAverageScoreItemList = new List<AgeAverageScoreItem>();
        }
    }

    public class AgeAverageScoreItem
    {
        public int Year { get; set; }

        public double AverageScore { get; set; }
    }
}
