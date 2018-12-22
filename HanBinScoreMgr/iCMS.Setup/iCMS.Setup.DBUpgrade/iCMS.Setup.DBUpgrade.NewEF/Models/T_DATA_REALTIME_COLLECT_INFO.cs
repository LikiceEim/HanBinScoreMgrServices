using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DATA_REALTIME_COLLECT_INFO
    {
        public int ID { get; set; }
        public Nullable<int> DevID { get; set; }
        public Nullable<int> MSID { get; set; }
        public string MSName { get; set; }
        public Nullable<int> MSStatus { get; set; }
        public string MSDesInfo { get; set; }
        public Nullable<int> MSDataStatus { get; set; }
        public Nullable<int> MSSpeedSingalID { get; set; }
        public string MSSpeedUnit { get; set; }
        public Nullable<float> MSSpeedVirtualValue { get; set; }
        public Nullable<float> MSSpeedPeakValue { get; set; }
        public Nullable<float> MSSpeedPeakPeakValue { get; set; }
        public Nullable<int> MSSpeedVirtualStatus { get; set; }
        public Nullable<int> MSSpeedPeakStatus { get; set; }
        public Nullable<int> MSSpeedPeakPeakStatus { get; set; }
        public Nullable<System.DateTime> MSSpeedVirtualTime { get; set; }
        public Nullable<System.DateTime> MSSpeedPeakTime { get; set; }
        public Nullable<System.DateTime> MSSpeedPeakPeakTime { get; set; }
        public Nullable<int> MSACCSingalID { get; set; }
        public string MSACCUnit { get; set; }
        public Nullable<float> MSACCVirtualValue { get; set; }
        public Nullable<float> MSACCPeakValue { get; set; }
        public Nullable<float> MSACCPeakPeakValue { get; set; }
        public Nullable<int> MSACCVirtualStatus { get; set; }
        public Nullable<int> MSACCPeakStatus { get; set; }
        public Nullable<int> MSACCPeakPeakStatus { get; set; }
        public Nullable<System.DateTime> MSACCVirtualTime { get; set; }
        public Nullable<System.DateTime> MSACCPeakTime { get; set; }
        public Nullable<System.DateTime> MSACCPeakPeakTime { get; set; }
        public Nullable<int> MSDispSingalID { get; set; }
        public string MSDispUnit { get; set; }
        public Nullable<float> MSDispVirtualValue { get; set; }
        public Nullable<float> MSDispPeakValue { get; set; }
        public Nullable<float> MSDispPeakPeakValue { get; set; }
        public Nullable<int> MSDispVirtualStatus { get; set; }
        public Nullable<int> MSDispPeakStatus { get; set; }
        public Nullable<int> MSDispPeakPeakStatus { get; set; }
        public Nullable<System.DateTime> MSDispVirtualTime { get; set; }
        public Nullable<System.DateTime> MSDispPeakTime { get; set; }
        public Nullable<System.DateTime> MSDispPeakPeakTime { get; set; }
        public Nullable<int> MSEnvelSingalID { get; set; }
        public Nullable<float> MSEnvelopingPEAKValue { get; set; }
        public Nullable<float> MSEnvelopingCarpetValue { get; set; }
        public string MSEnvelopingUnit { get; set; }
        public Nullable<int> MSEnvelopingPEAKStatus { get; set; }
        public Nullable<int> MSEnvelopingCarpetStatus { get; set; }
        public Nullable<System.DateTime> MSEnvelopingPEAKTime { get; set; }
        public Nullable<System.DateTime> MSEnvelopingCarpetTime { get; set; }
        public Nullable<int> MSLQSingalID { get; set; }
        public Nullable<float> MSLQValue { get; set; }
        public Nullable<int> MSLQStatus { get; set; }
        public string MSLQUnit { get; set; }
        public Nullable<System.DateTime> MSLQTime { get; set; }
        public Nullable<int> MSDevTemperatureStatus { get; set; }
        public Nullable<float> MSDevTemperatureValue { get; set; }
        public string MSDevTemperatureUnit { get; set; }
        public Nullable<System.DateTime> MSDevTemperatureTime { get; set; }
        public Nullable<int> MSWSTemperatureStatus { get; set; }
        public Nullable<float> MSWSTemperatureValue { get; set; }
        public string MSWSTemperatureUnit { get; set; }
        public Nullable<System.DateTime> MSWSTemperatureTime { get; set; }
        public Nullable<float> MSWSBatteryVolatageValue { get; set; }
        public string MSWSBatteryVolatageUnit { get; set; }
        public Nullable<int> MSWSBatteryVolatageStatus { get; set; }
        public Nullable<System.DateTime> MSWSBatteryVolatageTime { get; set; }
        public Nullable<int> MSWGLinkStatus { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
