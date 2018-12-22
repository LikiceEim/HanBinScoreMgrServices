using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    public class GetDevInfoByGroupIDResult
    {
        public List<DeviceInfo> DeviceInfoList { get; set; }

        public GetDevInfoByGroupIDResult()
        {
            DeviceInfoList = new List<DeviceInfo>();
        }
    }

    public class DeviceInfo
    {
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }
    }
}
