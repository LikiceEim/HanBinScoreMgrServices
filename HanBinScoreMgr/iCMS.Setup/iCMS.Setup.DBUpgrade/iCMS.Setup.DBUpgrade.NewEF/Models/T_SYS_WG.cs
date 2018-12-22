using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_WG
    {
        public int WGID { get; set; }
        public string WGName { get; set; }
        public int WGNO { get; set; }
        public Nullable<int> NetWorkID { get; set; }
        public int WGType { get; set; }
        public int LinkStatus { get; set; }
        public System.DateTime AddDate { get; set; }
        public string WGModel { get; set; }
        public string SoftwareVersion { get; set; }
        public int RunStatus { get; set; }
        public int ImageID { get; set; }
        public string Remark { get; set; }
        public string PersonInCharge { get; set; }
        public string PersonInChargeTel { get; set; }
        public Nullable<int> MonitorTreeID { get; set; }
        public string AgentAddress { get; set; }
    }
}
