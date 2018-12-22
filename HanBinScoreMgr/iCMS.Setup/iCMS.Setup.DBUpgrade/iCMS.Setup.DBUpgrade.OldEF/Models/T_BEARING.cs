using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_BEARING
    {
        public int BearingID { get; set; }
        public string FactoryName { get; set; }
        public string FactoryID { get; set; }
        public string BearingNum { get; set; }
        public string BearingDescribe { get; set; }
        public float BPFO { get; set; }
        public float BPFI { get; set; }
        public float BSF { get; set; }
        public float FTF { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
