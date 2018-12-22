using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class View_DevHistoryData
    {
        public int MSiteID { get; set; }
        public string MSiteName { get; set; }
        public Nullable<int> DevID { get; set; }
        public string DevName { get; set; }
        public Nullable<float> TempValue { get; set; }
        public Nullable<float> TempWarnSet { get; set; }
        public Nullable<float> TempAlarmSet { get; set; }
        public Nullable<int> TempStat { get; set; }
        public Nullable<float> SpeedVirtualValue { get; set; }
        public Nullable<float> SpeedVirtualValueWarnSet { get; set; }
        public Nullable<float> SpeedVirtualValueAlarmSet { get; set; }
        public Nullable<int> SpeedVirtualValueStat { get; set; }
        public Nullable<float> ACCPEAKValue { get; set; }
        public Nullable<float> ACCPEAKValueWarnSet { get; set; }
        public Nullable<float> ACCPEAKValueAlarmSet { get; set; }
        public Nullable<int> ACCPEAKValueStat { get; set; }
        public Nullable<float> LQValue { get; set; }
        public Nullable<float> LQWarnSet { get; set; }
        public Nullable<float> LQAlarmSet { get; set; }
        public Nullable<int> LQStat { get; set; }
        public Nullable<float> DisplacementDPEAKValue { get; set; }
        public Nullable<float> DisplacementDPEAKValueWarnSet { get; set; }
        public Nullable<float> DisplacementDPEAKValueAlarmSet { get; set; }
        public Nullable<int> DisplacementDPEAKValueStat { get; set; }
        public Nullable<float> EnvelopPEAKValue { get; set; }
        public Nullable<float> EnvelopPEAKValueWarnSet { get; set; }
        public Nullable<float> EnvelopPEAKValueAlmSet { get; set; }
        public Nullable<int> EnvelopPEAKValueStat { get; set; }
        public System.DateTime CollectitTime { get; set; }
        public int DataType { get; set; }
    }
}
