using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_VibScan
    {
        public int VibScanID { get; set; }
        public int COMID { get; set; }
        public string VibScanName { get; set; }
        public string VibScanAddress { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
