using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBinOrganManager
{
    public class GetMainOrganTypeResult
    {
        public List<MainOrganTypeItem> MainOrganTypeItemList { get; set; }

        public GetMainOrganTypeResult()
        {
            this.MainOrganTypeItemList = new List<MainOrganTypeItem>();
        }
    }

    public class MainOrganTypeItem
    {
        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }
    }
}
