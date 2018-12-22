using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    public class GetWGSimpleInfoResult
    {
        public List<WGSimpleInfo> WGSimpleInfo { get; set; }

        public GetWGSimpleInfoResult()
        {
            this.WGSimpleInfo  = new List<WGSimpleInfo>();
        }
    }
    public class WGSimpleInfo 
    {
        public int WGID { get; set; }

        public string WGName { get; set; }

        public int DevFormType { get; set; }

        public List<int> RelatedDeviceIDList { get; set; }

        public WGSimpleInfo() 
        {
            this.RelatedDeviceIDList = new List<int>();
        }
    }
}
