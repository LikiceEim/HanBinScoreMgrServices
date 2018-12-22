using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 恢复出厂设置
    /// </summary>
    public class SetFactoryReset : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
      
    }
}
