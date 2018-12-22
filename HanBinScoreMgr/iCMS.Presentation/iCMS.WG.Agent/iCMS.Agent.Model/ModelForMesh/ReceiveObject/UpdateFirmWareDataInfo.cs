using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 固件数据信息
    /// </summary>
    public class UpdateFirmWareDataInfo : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
       
    }
}
