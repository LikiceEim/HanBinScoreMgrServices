using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    public class GetDeviceStatisticSummaryParam : BaseRequest
    {
        public int MonitorTreeID { get; set; }

        public int UserID { get; set; }
    }
}
