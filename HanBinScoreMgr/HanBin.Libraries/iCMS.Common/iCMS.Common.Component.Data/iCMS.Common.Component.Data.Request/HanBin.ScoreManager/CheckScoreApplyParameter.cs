using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    public class CheckScoreApplyParameter : BaseRequest
    {
        public int ProcessUserID { get; set; }

        public int ApplyID { get; set; }

        public int ApplyStatus { get; set; }

        public string RejectReason { get; set; }

        public int CurrentUserID { get; set; }
    }

    public class CancelScoreApplyParameter : BaseRequest
    {
        public int ApplyID { get; set; }

        public int CurrentUserID { get; set; }
    }
}
