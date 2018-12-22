using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUAgent
{
    public class StopCollectionResult
    {
        public List<StopCollectionItemResult> StopCollectionResultList { get; set; }

        public StopCollectionResult()
        {
            this.StopCollectionResultList = new List<StopCollectionItemResult>();
        }
    }

    public class StopCollectionItemResult
    {
        public int DAUID { get; set; }

        public bool IsSuccess { get; set; }
    }
}
