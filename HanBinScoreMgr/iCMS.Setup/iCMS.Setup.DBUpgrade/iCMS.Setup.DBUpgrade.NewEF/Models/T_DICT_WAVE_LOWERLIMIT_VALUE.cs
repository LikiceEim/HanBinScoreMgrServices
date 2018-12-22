using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DICT_WAVE_LOWERLIMIT_VALUE
    {
        public int ID { get; set; }
        public int VibratingSignalTypeID { get; set; }
        public int WaveLowerLimitValue { get; set; }
        public string Describe { get; set; }
        public int IsUsable { get; set; }
        public int IsDefault { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
