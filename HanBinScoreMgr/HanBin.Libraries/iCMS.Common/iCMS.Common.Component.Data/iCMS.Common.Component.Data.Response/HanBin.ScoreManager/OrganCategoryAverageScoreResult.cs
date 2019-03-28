using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class OrganCategoryAverageScoreResult
    {
        public List<OrganCategoryAverageScoreItem> OrganCategoryAverageScoreItemList { get; set; }

        public OrganCategoryAverageScoreResult()
        {
            this.OrganCategoryAverageScoreItemList = new List<OrganCategoryAverageScoreItem>();
        }
    }

    public class OrganCategoryAverageScoreItem
    {
        public int OrganCategoryID { get; set; }

        public string OrganCategoryName { get; set; }

        public double AverageScore { get; set; }
    }
}
