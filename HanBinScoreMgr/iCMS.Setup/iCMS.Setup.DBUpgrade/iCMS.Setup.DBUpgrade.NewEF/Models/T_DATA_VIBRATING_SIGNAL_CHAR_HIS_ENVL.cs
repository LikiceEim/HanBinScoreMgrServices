using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL
    {
        public int HisDataID { get; set; }
        public int SingalID { get; set; }
        public int MsiteID { get; set; }
        public int DevID { get; set; }
        public System.DateTime SamplingDate { get; set; }
        public string WaveDataPath { get; set; }
        public string Rotate { get; set; }
        public float TransformCofe { get; set; }
        public float RealSamplingFrequency { get; set; }
        public int SamplingPointData { get; set; }
        public int AlmStatus { get; set; }
        public Nullable<int> DAQStyle { get; set; }
        public Nullable<float> PeakValue { get; set; }
        public Nullable<float> CarpetValue { get; set; }
        public Nullable<float> PeakWarnValue { get; set; }
        public Nullable<float> PeakAlmValue { get; set; }
        public Nullable<float> CarpetWarnValue { get; set; }
        public Nullable<float> CarpetAlmValue { get; set; }
        public int MonthDate { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
