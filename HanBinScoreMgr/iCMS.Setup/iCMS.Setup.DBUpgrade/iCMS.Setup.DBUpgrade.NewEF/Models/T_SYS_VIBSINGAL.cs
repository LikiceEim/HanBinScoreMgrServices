using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_VIBSINGAL
    {
        public int SingalID { get; set; }
        public Nullable<int> DAQStyle { get; set; }
        public int MSiteID { get; set; }
        public Nullable<int> UpLimitFrequency { get; set; }
        public Nullable<int> LowLimitFrequency { get; set; }
        public Nullable<float> StorageTrighters { get; set; }
        public Nullable<int> EnlvpBandW { get; set; }
        public Nullable<int> EnlvpFilter { get; set; }
        public int SingalType { get; set; }
        public int SingalStatus { get; set; }
        public System.DateTime SingalSDate { get; set; }
        public int WaveDataLength { get; set; }
        public System.DateTime AddDate { get; set; }
        public string Remark { get; set; }
    }
}
