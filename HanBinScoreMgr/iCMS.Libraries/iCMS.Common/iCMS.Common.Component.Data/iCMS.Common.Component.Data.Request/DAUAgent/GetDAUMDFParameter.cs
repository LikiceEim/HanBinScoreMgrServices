using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class GetDAUMDFParameter : BaseRequest
    {
        public int DAUID { get; set; }
    }
}