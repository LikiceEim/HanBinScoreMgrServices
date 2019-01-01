using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBinOrganManager
{
    public class GetAreaListResult
    {
        public List<AreaItem> AreaItemList { get; set; }

        public GetAreaListResult() 
        {
            this.AreaItemList = new List<AreaItem>();
        }
    }

    public class AreaItem
    {
        public int AreaID { get; set; }

        public string AreaName { get; set; }
    }
}
