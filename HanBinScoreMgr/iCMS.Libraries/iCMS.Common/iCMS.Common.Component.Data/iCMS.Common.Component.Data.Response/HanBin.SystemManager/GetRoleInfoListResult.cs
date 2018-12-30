using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.SystemManager
{
    public class GetRoleInfoListResult
    {
        public List<RoleItem> RoleList { get; set; }

        public GetRoleInfoListResult()
        {
            this.RoleList = new List<RoleItem>();
        }
    }

    public class RoleItem
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }
    }
}
