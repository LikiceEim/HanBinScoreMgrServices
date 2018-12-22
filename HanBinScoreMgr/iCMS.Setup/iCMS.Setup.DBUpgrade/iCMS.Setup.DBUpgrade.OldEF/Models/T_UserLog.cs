using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_UserLog
    {
        public int UserLogID { get; set; }
        public int UserID { get; set; }
        public string Record { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
