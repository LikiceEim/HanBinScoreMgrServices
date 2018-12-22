using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Module
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleValue { get; set; }
        public int ParID { get; set; }
        public int OID { get; set; }
        public System.DateTime AddDate { get; set; }
        public string Code { get; set; }
    }
}
