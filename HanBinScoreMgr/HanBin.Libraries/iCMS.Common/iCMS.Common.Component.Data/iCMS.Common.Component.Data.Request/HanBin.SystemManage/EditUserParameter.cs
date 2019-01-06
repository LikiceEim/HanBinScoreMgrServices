using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.SystemManage
{
    public class EditUserParameter : BaseRequest
    {
        public int UserID { get; set; }
        public string UserToken { get; set; }

        //public string PWD { get; set; }

        public int Gender { get; set; }

        public int OrganizationID { get; set; }

        public int RoleID { get; set; }

        public int LastUpdateUserID { get; set; }
    }
}
