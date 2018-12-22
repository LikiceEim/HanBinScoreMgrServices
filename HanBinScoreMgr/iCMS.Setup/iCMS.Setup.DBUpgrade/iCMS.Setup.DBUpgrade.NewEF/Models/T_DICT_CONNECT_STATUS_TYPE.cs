using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_DICT_CONNECT_STATUS_TYPE
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public int IsUsable { get; set; }
        public int IsDefault { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
