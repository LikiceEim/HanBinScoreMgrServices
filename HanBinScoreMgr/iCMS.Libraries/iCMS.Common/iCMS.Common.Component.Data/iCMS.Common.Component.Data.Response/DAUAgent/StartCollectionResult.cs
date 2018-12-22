using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUAgent
{
    public class StartCollectionResult
    {
        public List<StartCollectionItemResult> StartCollectionResultList { get; set; }

        public StartCollectionResult()
        {
            this.StartCollectionResultList = new List<StartCollectionItemResult>();
        }
    }

    public class StartCollectionItemResult
    {
        public int DAUID { get; set; }

        public bool IsSuccess { get; set; }
    }
}
