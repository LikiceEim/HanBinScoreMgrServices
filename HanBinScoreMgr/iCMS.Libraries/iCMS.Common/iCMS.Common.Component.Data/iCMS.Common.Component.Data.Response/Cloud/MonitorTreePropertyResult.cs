using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class MonitorTreePropertyResult
    {
        public List<MonitorTreeProperty> MonitorTreePropertyList { get; set; }

        public MonitorTreePropertyResult()
        {
            MonitorTreePropertyList = new List<MonitorTreeProperty>();
        }
    }
}
