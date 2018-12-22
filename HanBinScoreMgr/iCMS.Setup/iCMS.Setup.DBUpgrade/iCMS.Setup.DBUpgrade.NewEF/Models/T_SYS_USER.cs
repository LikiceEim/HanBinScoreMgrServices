using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_USER
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int IsShow { get; set; }
        public string UserName { get; set; }
        public string PSW { get; set; }
        public string Email { get; set; }
        public string LockPSW { get; set; }
        public int LoginCount { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public string Phone { get; set; }
        public System.DateTime AddDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
