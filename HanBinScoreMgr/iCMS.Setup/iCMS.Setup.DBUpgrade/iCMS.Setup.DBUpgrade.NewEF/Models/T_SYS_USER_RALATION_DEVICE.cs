using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_USER_RALATION_DEVICE
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> DevId { get; set; }
        public string MTIds { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
    }
}
