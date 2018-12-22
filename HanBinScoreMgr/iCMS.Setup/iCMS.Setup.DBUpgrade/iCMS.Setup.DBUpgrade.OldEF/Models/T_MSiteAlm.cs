using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_MSiteAlm
    {
        public int MSiteAlmID { get; set; }
        public int MSiteID { get; set; }
        public int MSDType { get; set; }
        public float WarnValue { get; set; }
        public float AlmValue { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
