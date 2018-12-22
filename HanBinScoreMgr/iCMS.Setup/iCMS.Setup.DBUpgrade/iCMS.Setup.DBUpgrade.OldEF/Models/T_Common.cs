using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Common
    {
        public int CID { get; set; }
        public string CValue { get; set; }
        public int CPID { get; set; }
        public int COID { get; set; }
        public int DataType { get; set; }
        public string CDes { get; set; }
        public bool IsDefault { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
