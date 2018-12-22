using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_MONITOR_TREE_PROPERTY
    {
        public int MonitorTreePropertyID { get; set; }
        public string Address { get; set; }
        public string URL { get; set; }
        public string TelphoneNO { get; set; }
        public string FaxNO { get; set; }
        public Nullable<float> Latitude { get; set; }
        public Nullable<float> Longtitude { get; set; }
        public int ChildCount { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<float> Width { get; set; }
        public Nullable<float> Area { get; set; }
        public string PersonInCharge { get; set; }
        public string PersonInChargeTel { get; set; }
        public string Remark { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
