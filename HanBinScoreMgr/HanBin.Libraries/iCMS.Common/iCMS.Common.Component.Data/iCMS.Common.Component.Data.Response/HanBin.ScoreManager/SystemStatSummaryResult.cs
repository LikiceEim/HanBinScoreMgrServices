using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class SystemStatSummaryResult
    {
        public int OrganizatonCount { get; set; }

        public int OfficerCount { get; set; }

        public int UserCount { get; set; }

        public double AvarageScore { get; set; }
    }
}
