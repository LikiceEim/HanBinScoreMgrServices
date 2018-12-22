using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_MODULE
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int ParID { get; set; }
        public int IsUsed { get; set; }
        public int IsDeault { get; set; }
        public int OID { get; set; }
        public System.DateTime AddDate { get; set; }
        public string Code { get; set; }
    }
}
