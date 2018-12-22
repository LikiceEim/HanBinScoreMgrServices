using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class TempeDeviceSetMSiteAlmResult
    {
        public List<TempeDeviceSetMSiteAlm> TempeDeviceSetMSiteAlmList{ get; set; }

        public TempeDeviceSetMSiteAlmResult()
        {
            TempeDeviceSetMSiteAlmList = new List<TempeDeviceSetMSiteAlm>();
        }
    }
}
