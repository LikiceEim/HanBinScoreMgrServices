using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.SystemManage
{
    public class GetUserInfoParameter : BaseRequest
    {
        public int CurrentUserID { get; set; }

        public int? OrganizationID { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
