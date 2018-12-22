using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_MSiteData
    {
        public int MSiteDataID { get; set; }
        public int MSiteID { get; set; }
        public int MSDType { get; set; }
        public System.DateTime MSDDate { get; set; }
        public float MSDValue { get; set; }
        public int MSDStatus { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
