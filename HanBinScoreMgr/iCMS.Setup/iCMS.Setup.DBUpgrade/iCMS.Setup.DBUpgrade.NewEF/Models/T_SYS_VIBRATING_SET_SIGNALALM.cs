using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_VIBRATING_SET_SIGNALALM
    {
        public int SingalAlmID { get; set; }
        public int SingalID { get; set; }
        public int ValueType { get; set; }
        public float WarnValue { get; set; }
        public float AlmValue { get; set; }
        public int Status { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<float> UploadTrigger { get; set; }
        public Nullable<float> ThrendAlarmPrvalue { get; set; }
    }
}
