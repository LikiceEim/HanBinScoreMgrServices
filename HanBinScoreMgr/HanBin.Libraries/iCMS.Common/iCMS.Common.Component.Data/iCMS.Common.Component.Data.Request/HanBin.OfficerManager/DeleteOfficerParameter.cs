using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OfficerManager
{
    public class DeleteOfficerParameter : BaseRequest
    {
        public int OfficerID { get; set; }

        public int CurrentUserID { get; set; }
    }

    public class SetOfficerOffService : BaseRequest
    {
        public int OfficerID { get; set; }

        public int CurrentUserID { get; set; }
    }
}
