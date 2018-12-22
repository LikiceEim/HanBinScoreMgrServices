using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Utility
{
    public class GetAuthorizedResult
    {
        public List<bool> Result { get; set; }

        public GetAuthorizedResult()
        {
            Result = new List<bool>();
        }
    }
}
