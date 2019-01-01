using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.OfficerManager
{
    public class GetOrganSummaryResult
    {
        public List<OrganSummaryInfo> OrganList { get; set; }

        public GetOrganSummaryResult()
        {
            this.OrganList = new List<OrganSummaryInfo>();
        }
    }


    public class OrganSummaryInfo
    {
        public int OrganID { get; set; }

        public string OrganFullName { get; set; }

        public int OrganTypeID { get; set; }
    }
}
