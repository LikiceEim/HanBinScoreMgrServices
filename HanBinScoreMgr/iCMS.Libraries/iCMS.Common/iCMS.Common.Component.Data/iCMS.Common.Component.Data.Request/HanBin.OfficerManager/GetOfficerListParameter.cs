using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.HanBin.OfficerManager
{
    public class GetOfficerListParameter
    {
        public int? OrganizationID { get; set; }

        public string Keyword { get; set; }

        public int? LevelID { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
