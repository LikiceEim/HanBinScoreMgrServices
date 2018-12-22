using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_SysLog
    {
        public int SysLogID { get; set; }
        public int UserID { get; set; }
        public string Record { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
