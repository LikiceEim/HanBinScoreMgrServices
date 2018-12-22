using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_MonitorTree
    {
        public int MonitorTreeID { get; set; }
        public int PID { get; set; }
        public int OID { get; set; }
        public bool IsDefault { get; set; }
        public bool IsLeaf { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public int Type { get; set; }
        public System.DateTime AddDate { get; set; }
        public int ImageID { get; set; }
        public int MonitorTreePropertyID { get; set; }
        public int Status { get; set; }
    }
}
