using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OfficerManager
{
    public class CancelScoreApplyParameter : BaseRequest
    {
        public int ApplyID { get; set; }

        public int CurrentUserID { get; set; }
    }
}
