using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_MONITOR_TREE
    {
        public int MonitorTreeID { get; set; }
        public int PID { get; set; }
        public int OID { get; set; }
        public int IsDefault { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public int Type { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<int> MonitorTreePropertyID { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
