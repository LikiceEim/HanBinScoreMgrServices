using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
