using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Image
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public string ImagePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Type { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
