using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Plant
    {
        public int PlantID { get; set; }
        public int FactoryID { get; set; }
        public string Pname { get; set; }
        public string PMark { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int PStatus { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
