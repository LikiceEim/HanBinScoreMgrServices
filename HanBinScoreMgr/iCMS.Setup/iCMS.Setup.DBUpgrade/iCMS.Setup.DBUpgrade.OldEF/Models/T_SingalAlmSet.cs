using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_SingalAlmSet
    {
        public int SingalAlmID { get; set; }
        public int SingalID { get; set; }
        public int ValueType { get; set; }
        public float WarnValue { get; set; }
        public float AlmValue { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
