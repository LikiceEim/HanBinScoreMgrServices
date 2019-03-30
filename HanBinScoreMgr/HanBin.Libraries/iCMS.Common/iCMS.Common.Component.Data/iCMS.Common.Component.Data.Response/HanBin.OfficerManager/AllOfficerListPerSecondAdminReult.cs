using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.OfficerManager
{
    public class AllOfficerListPerSecondAdminReult
    {
        public List<OfficerInfoItem> OfficerInfoItemList { get; set; }

        public AllOfficerListPerSecondAdminReult()
        {
            this.OfficerInfoItemList = new List<OfficerInfoItem>();
        }
    }

    public class OfficerInfoItem
    {
        public int OfficerID { get; set; }

        public string OfficerName { get; set; }

        public double CurrentScore { get; set; }
    }
}
