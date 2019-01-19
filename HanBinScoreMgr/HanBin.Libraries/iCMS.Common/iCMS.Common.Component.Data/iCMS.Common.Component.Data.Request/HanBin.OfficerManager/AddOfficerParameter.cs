using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OfficerManager
{
    public class AddOfficerParameter : BaseRequest
    {
        public string Name { get; set; }

        public int Gender { get; set; }

        public string IdentifyNumber { get; set; }

        public string Birthday { get; set; }

        public int OrganizationID { get; set; }

        public int PositionID { get; set; }

        public int LevelID { get; set; }

        public string OnOfficeDate { get; set; }

        public string Duty { get; set; }

        public int AddUserID { get; set; }

        public int InitialScore { get; set; }

        public List<ApplyItem> ApplyItemList { get; set; }

        public AddOfficerParameter()
        {
            ApplyItemList = new List<ApplyItem>();
        }
    }

    public class EditOfficerParameter : BaseRequest
    {
        public int OfficerID { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public string IdentifyNumber { get; set; }

        public string Birthday { get; set; }

        public int OrganizationID { get; set; }

        public int PositionID { get; set; }

        public int LevelID { get; set; }

        public string OnOfficeDate { get; set; }

        public string Duty { get; set; }

        public int InitialScore { get; set; }

        public int UpdateUserID { get; set; }
    }
}
