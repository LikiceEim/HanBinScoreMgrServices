using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_ROLEMODULE
    {
        public int RMID { get; set; }
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
