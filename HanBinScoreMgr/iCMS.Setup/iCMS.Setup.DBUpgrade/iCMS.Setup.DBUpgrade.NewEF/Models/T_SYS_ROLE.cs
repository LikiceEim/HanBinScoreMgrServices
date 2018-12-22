using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_ROLE
    {
        public int RoleID { get; set; }
        public int IsShow { get; set; }
        public int IsDeault { get; set; }
        public string RoleName { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
