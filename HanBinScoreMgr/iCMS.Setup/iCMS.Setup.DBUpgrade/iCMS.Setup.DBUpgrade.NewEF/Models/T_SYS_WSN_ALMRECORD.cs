using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_WSN_ALMRECORD
    {
        public int AlmRecordID { get; set; }
        public int DevID { get; set; }
        public string DevName { get; set; }
        public string DevNO { get; set; }
        public int MSiteID { get; set; }
        public string MSiteName { get; set; }
        public int WGID { get; set; }
        public string WGName { get; set; }
        public int WSID { get; set; }
        public string WSName { get; set; }
        public string MonitorTreeID { get; set; }
        public int MSAlmID { get; set; }
        public int AlmStatus { get; set; }
        public Nullable<float> SamplingValue { get; set; }
        public Nullable<float> WarningValue { get; set; }
        public Nullable<float> DangerValue { get; set; }
        public System.DateTime BDate { get; set; }
        public System.DateTime EDate { get; set; }
        public System.DateTime LatestStartTime { get; set; }
        public string Content { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
