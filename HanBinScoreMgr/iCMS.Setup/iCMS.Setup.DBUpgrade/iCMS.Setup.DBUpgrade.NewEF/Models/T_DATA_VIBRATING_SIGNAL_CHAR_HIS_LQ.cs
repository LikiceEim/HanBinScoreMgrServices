using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ
    {
        public int HisDataID { get; set; }
        public int SingalID { get; set; }
        public int MSITEID { get; set; }
        public int DevID { get; set; }
        public System.DateTime SamplingDate { get; set; }
        public string WaveDataPath { get; set; }
        public string Rotate { get; set; }
        public float TransformCofe { get; set; }
        public float RealSamplingFrequency { get; set; }
        public int SamplingPointData { get; set; }
        public int AlmStatus { get; set; }
        public Nullable<int> DAQStyle { get; set; }
        public Nullable<float> LQValue { get; set; }
        public Nullable<float> LQWarnValue { get; set; }
        public Nullable<float> LQAlmValue { get; set; }
        public int MonthDate { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
