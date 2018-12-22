using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class StopCollectionParameter : BaseRequest
    {
        public List<int> DAUIDList { get; set; }

        public StopCollectionParameter()
        {
            this.DAUIDList = new List<int>();
        }
    }
}