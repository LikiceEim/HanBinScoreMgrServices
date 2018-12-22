using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_VOLTAGE_SET_MSITEALM
    {
        public int MsiteAlmID { get; set; }
        public int MsiteID { get; set; }
        public float WarnValue { get; set; }
        public float AlmValue { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
