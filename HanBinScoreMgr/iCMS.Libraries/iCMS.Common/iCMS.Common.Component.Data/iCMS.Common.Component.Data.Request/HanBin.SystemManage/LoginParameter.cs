using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.SystemManage
{
  public  class LoginParameter:BaseRequest
    {
        public string UserName { get; set; }

        public string PWD { get; set; }
    }

    public class AddUserParameter
    {
        public string UserToken { get; set; }

        public string PWD { get; set; }

        public int Gender { get; set; }

        public int OrganizationID { get; set; }

        public int RoleID { get; set; }

        public int AddUserID { get; set; }

  
    } 


}
