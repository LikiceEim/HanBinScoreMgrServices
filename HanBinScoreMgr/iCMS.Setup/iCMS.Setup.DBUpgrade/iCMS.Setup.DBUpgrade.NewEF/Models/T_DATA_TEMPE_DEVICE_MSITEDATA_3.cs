using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DATA_TEMPE_DEVICE_MSITEDATA_3
    {
        public int MsiteDataID { get; set; }
        public int MsiteID { get; set; }
        public System.DateTime SamplingDate { get; set; }
        public float MsDataValue { get; set; }
        public int Status { get; set; }
        public Nullable<float> WarnValue { get; set; }
        public Nullable<float> AlmValue { get; set; }
        public int MonthDate { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
