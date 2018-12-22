using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Factory
    {
        public int FactoryID { get; set; }
        public string FacName { get; set; }
        public string FacAddress { get; set; }
        public string FacMark { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int FacStatus { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
