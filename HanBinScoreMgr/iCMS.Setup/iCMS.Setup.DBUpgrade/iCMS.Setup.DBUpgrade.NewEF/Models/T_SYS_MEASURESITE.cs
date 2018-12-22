using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_MEASURESITE
    {
        public int MSiteID { get; set; }
        public int MSiteName { get; set; }
        public int DevID { get; set; }
        public Nullable<int> VibScanID { get; set; }
        public Nullable<int> WSID { get; set; }
        public int ChannelID { get; set; }
        public int MeasureSiteType { get; set; }
        public float SensorCosA { get; set; }
        public float SensorCosB { get; set; }
        public int MSiteStatus { get; set; }
        public System.DateTime MSiteSDate { get; set; }
        public string WaveTime { get; set; }
        public string FlagTime { get; set; }
        public string TemperatureTime { get; set; }
        public string Remark { get; set; }
        public string Position { get; set; }
        public int SerialNo { get; set; }
        public Nullable<int> BearingID { get; set; }
        public string BearingType { get; set; }
        public string BearingModel { get; set; }
        public string LubricatingForm { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
