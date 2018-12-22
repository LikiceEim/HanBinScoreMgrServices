using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_COM
    {
        public int COMID { get; set; }
        public string COMName { get; set; }
        public int BaudRate { get; set; }
        public bool ParCheck { get; set; }
        public int DataBit { get; set; }
        public int StopBit { get; set; }
        public bool CRCCheck { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
