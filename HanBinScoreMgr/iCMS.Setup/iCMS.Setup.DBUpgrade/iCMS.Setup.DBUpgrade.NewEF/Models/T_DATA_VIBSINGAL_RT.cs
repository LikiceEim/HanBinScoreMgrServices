using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DATA_VIBSINGAL_RT
    {
        public int RTDataID { get; set; }
        public Nullable<int> DAQStyle { get; set; }
        public int SingalID { get; set; }
        public int MSID { get; set; }
        public int DevID { get; set; }
        public System.DateTime SamplingDate { get; set; }
        public string Rotate { get; set; }
        public string WaveDataPath { get; set; }
        public float TransformCofe { get; set; }
        public float RealSamplingFrequency { get; set; }
        public int SamplingPointData { get; set; }
        public int AlmStatus { get; set; }
        public Nullable<float> E1 { get; set; }
        public Nullable<float> E2 { get; set; }
        public Nullable<float> E3 { get; set; }
        public Nullable<float> E4 { get; set; }
        public Nullable<float> E5 { get; set; }
        public Nullable<float> E6 { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
