using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_IMAGE
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
