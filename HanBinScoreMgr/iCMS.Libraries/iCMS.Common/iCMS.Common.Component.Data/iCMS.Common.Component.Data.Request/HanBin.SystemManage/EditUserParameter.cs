using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.SystemManage
{
   public class EditUserParameter
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
