using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 根据用户ID获取设备顺序返回结果类
    /// </summary>
    public class GetOrderDeviceByUserIDResult
    {
        public List<DeviceOrderInfo> DeviceOrderInfo { get; set; }

        public GetOrderDeviceByUserIDResult()
        {
            this.DeviceOrderInfo = new List<DeviceOrderInfo>();
        }
    }
    /// <summary>
    /// 设备根据顺序从小到大排列
    /// </summary>
    public class DeviceOrderInfo
    {
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }
    }
}
