using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    /// <summary>
    /// 对用户管理的设备顺序进行排序请求参数
    /// </summary>
    public class OrderDeviceByUserIDAndDevcieIDParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 排序后设备ID顺序，按顺序从小到大
        /// </summary>
        public List<int> DeviceIDList { get; set; }

        public OrderDeviceByUserIDAndDevcieIDParameter()
        {
            DeviceIDList = new List<int>();
        }
    }
}
