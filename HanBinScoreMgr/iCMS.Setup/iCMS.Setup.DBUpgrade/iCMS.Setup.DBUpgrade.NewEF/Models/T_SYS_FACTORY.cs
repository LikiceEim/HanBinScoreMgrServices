using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_FACTORY
    {
        public int ID { get; set; }
        public string FactoryID { get; set; }
        public string FactoryName { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
