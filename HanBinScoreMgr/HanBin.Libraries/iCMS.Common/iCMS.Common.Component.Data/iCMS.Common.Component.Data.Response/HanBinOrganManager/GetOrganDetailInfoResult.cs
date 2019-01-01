using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBinOrganManager
{
    public class GetOrganDetailInfoResult
    {
        public int OrganID { get; set; }

        public string OrganCode { get; set; }

        public string OrganFullName { get; set; }

        public string OrganShortName { get; set; }

        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }

        public int OrganCategoryID { get; set; }

        public string OrganCategoryName { get; set; }

        public List<OfficerInfo> OfficerList { get; set; }

        public GetOrganDetailInfoResult()
        {
            OfficerList = new List<OfficerInfo>();
        }
    }

    public class OfficerInfo
    {
        public int OfficerID { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public int PositionID { get; set; }

        public string PositonName { get; set; }

        public int LevelID { get; set; }

        public string IevelName { get; set; }

        public DateTime OnOfficeDate { get; set; }

        public int CurrentScore { get; set; }
    }
}
