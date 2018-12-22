using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 更新固件描述信息
    /// </summary>
    public class UpdateFirmWareDescInfo : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
       
    }
}
