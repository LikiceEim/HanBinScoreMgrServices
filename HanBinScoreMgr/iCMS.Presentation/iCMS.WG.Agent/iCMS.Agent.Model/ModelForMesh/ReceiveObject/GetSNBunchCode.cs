using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 获取参数SN串码
    /// </summary>
    public class GetSNBunchCode : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
        /// <summary>
        /// SN串码
        /// </summary>
        public string SN { get; set; }
      
    }
}
