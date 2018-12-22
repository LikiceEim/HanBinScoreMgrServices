using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_AlmRecord
    {
        public int AlmRecordID { get; set; }
        public int DevID { get; set; }
        public int MSiteID { get; set; }
        public int SingalID { get; set; }
        public int MSAlmID { get; set; }
        public int AlmStatus { get; set; }
        public System.DateTime BDate { get; set; }
        public System.DateTime EDate { get; set; }
        public System.DateTime AddDate { get; set; }
        public System.DateTime LatestStartTime { get; set; }
        public string Content { get; set; }
    }
}
