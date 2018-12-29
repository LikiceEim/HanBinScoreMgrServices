using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.ScoreManager
{
    public class WhatsToDoSummaryResult
    {
        public List<WhatToDoInfo> WhatToDoList { get; set; }

        public WhatsToDoSummaryResult()
        {
            WhatToDoList = new List<WhatToDoInfo>();
        }
    }
    public class WhatToDoInfo
    {
        public int ApplyID { get; set; }

        public string ApplyTitle { get; set; }

        public DateTime ApplyDate { get; set; }
    }
}
