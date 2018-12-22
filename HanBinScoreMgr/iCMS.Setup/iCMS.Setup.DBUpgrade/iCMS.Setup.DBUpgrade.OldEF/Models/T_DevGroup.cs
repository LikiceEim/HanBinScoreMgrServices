using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_DevGroup
    {
        public int DevGroupID { get; set; }
        public int MonitorTreeID { get; set; }
        public string DevGroupName { get; set; }
        public string Des { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
