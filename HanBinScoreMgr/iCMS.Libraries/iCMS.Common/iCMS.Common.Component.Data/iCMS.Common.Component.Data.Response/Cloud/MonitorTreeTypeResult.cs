
using System.Collections.Generic;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class MonitorTreeTypeResult
    {
        public List<MonitorTreeType> MonitorTreeTypeList { get; set; }

        public MonitorTreeTypeResult()
        {
            MonitorTreeTypeList = new List<MonitorTreeType>();
        }
    }
}
