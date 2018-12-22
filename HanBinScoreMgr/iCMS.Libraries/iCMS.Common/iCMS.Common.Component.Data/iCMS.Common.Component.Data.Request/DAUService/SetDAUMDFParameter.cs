using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DAUService
{
    public class SetDAUMDFParameter : BaseRequest
    {
        public List<int> DAUID { get; set; }
    }
}
