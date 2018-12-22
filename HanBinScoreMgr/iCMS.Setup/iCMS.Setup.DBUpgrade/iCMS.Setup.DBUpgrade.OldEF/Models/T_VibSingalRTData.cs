using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_VibSingalRTData
    {
        public int RTDataID { get; set; }
        public int SingalID { get; set; }
        public int MSID { get; set; }
        public int DevID { get; set; }
        public System.DateTime SamplingDate { get; set; }
        public float Rotate { get; set; }
        public string WaveData { get; set; }
        public float TransformCofe { get; set; }
        public float RealSamplingFrequency { get; set; }
        public int SamplingPointData { get; set; }
        public int AlmStatus { get; set; }
        public float E1 { get; set; }
        public float E2 { get; set; }
        public float E3 { get; set; }
        public float E4 { get; set; }
        public float E5 { get; set; }
        public float E6 { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
