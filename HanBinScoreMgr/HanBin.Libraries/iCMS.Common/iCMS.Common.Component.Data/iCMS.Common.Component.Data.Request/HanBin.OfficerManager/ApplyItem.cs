using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OfficerManager
{
    public class ApplyItem
    {
        public int ItemID { get; set; }

        public string ItemDescription { get; set; }

        public int ItemScore { get; set; }

        public string ApplySummary { get; set; }
    }
}
