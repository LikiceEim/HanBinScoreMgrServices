using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Commonent.Data.Enum
{
    public enum EnumApproveStatus
    {
        [Description("待审批")]
        Approving = 0,

        [Description("审批通过")]
        Pass = 1,

        [Description("审批驳回")]
        Reject = 2,

        [Description("撤销")]
        Revoke = 3
    }
}
