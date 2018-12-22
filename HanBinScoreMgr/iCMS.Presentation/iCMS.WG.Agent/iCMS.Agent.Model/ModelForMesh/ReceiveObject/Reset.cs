using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 重启
    /// </summary>
    public class Reset : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
      
    }
}
