using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class AgeAverageScoreParameter
    {
        public DateTime? BirthdayFrom { get; set; }

        public DateTime? BirthdayTo { get; set; }

        public int? WorkYears { get; set; }
    }
}
