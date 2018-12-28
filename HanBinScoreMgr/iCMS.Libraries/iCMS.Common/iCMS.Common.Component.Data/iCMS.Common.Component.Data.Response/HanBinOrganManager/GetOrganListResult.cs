using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBinOrganManager
{
    public class GetOrganListResult
    {
        public List<OrganInfo> OrganInfoList { get; set; }

        public int Total { get; set; }

        public GetOrganListResult()
        {
            OrganInfoList = new List<OrganInfo>();
        }
    }

    public class OrganInfo
    {
        public int OrganID { get; set; }

        public string OrganCode { get; set; }

        public string OrganFullName { get; set; }

        public string OrganShortName { get; set; }

        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }

        public int OrganCategoryID { get; set; }

        public string OrganCategoryName { get; set; }

        public int OfficerQuanlity { get; set; }

        public DateTime AddDate { get; set; }
    }
}
