
using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class WSResult
    {
        public List<WS> WSList { get; set; }

        public WSResult()
        {
            WSList = new List<WS>();
        }
    }
}
