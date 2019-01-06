using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class OrganAverageScoreParameter : BaseRequest
    {
        public int? OrganID { get; set; }

        public int? LevelID { get; set; }

        public int? WorkYears { get; set; }
    }
}
