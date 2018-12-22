using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.SystemManager
{
    public class GetUserInfoResult
    {
        public List<HBUserInfo> UserInfoList { get; set; }


        public int Total { get; set; }

        public GetUserInfoResult()
        {
            UserInfoList = new List<HBUserInfo>();
        }
    }

    public class HBUserInfo
    {
        public int UserID { get; set; }
        public string UserToken { get; set; }

        //public string PWD { get; set; }

        public int Gender { get; set; }

        public int OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }
        public int LastUpdateUserID { get; set; }
    }
}
