using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.HanBin.OfficerManager
{
    public class GetOfficerListResult
    {
        public List<OfficerDetailInfo> OfficerInfoList { get; set; }

        public int Total { get; set; }

        public GetOfficerListResult()
        {
            this.OfficerInfoList = new List<OfficerDetailInfo>();
        }
    }

    public class OfficerDetailInfo
    {
        public int OfficerID { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public int OrganizationID { get; set; }

        public string OrganizationName { get; set; }

        public int PositionID { get; set; }

        public string PositionName { get; set; }

        public int LevelID { get; set; }

        public string LevelName { get; set; }

        public DateTime OnOfficeDate { get; set; }

        public int CurrentScore { get; set; }

        public string IdentifyNumber { get; set; }
    }

    public class GetLevelListResult
    {
        public List<LevelSummaryInfo> LevelList { get; set; }

        public GetLevelListResult()
        {
            LevelList = new List<LevelSummaryInfo>();
        }
    }

    public class LevelSummaryInfo
    {
        public int LevelID { get; set; }

        public string LevelName { get; set; }
    }
    public class GetPositionListResult
    {
        public List<PositionSummaryInfo> PositionList { get; set; }

        public GetPositionListResult()
        {
            PositionList = new List<PositionSummaryInfo>();
        }
    }

    public class PositionSummaryInfo
    {
        public int PositionID { get; set; }

        public string PositionName { get; set; }
    }

}
