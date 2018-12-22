using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_SYSLOG
    {
        public int SysLogID { get; set; }
        public int UserID { get; set; }
        public string Record { get; set; }
        public System.DateTime AddDate { get; set; }
        public string IPAddress { get; set; }
    }
}
