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

        public DateTime Birthday { get; set; }

        public int OrganizationID { get; set; }

        public int PositionID { get; set; }

        public int LevelID { get; set; }

        public DateTime OnOfficeDate { get; set; }

        public string Duty { get; set; }

        public int AddUserID { get; set; }

        public int InitialScore { get; set; }

        public List<ApplyItem> ApplyItemList { get; set; }

        public AddOfficerParameter()
        {
            ApplyItemList = new List<ApplyItem>();
        }
    }
}
