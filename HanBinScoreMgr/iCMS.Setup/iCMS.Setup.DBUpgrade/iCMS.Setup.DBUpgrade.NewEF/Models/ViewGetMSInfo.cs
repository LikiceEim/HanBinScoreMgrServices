using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class ViewGetMSInfo
    {
        public int WSID { get; set; }
        public string MACADDR { get; set; }
        public int LinkStatus { get; set; }
        public int MSiteID { get; set; }
        public int MSiteName { get; set; }
        public int DevID { get; set; }
        public Nullable<int> VibScanID { get; set; }
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
        public int id { get; set; }
        public string OperatorKey { get; set; }
        public int MSID { get; set; }
        public int OperationType { get; set; }
        public System.DateTime Bdate { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }
        public string OperationResult { get; set; }
        public string OperationReson { get; set; }
        public Nullable<int> DAQStyle { get; set; }
    }
}
