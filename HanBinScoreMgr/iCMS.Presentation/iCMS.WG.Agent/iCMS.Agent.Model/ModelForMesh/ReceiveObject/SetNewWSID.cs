using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    public class SetNewWSID : ReceiveObject
    {
        /// <summary>
        /// 响应值
        /// </summary>
        public UInt16 ResponseCode { get; set; }
        /// <summary>
        /// 新WSID
        /// </summary>
        public Byte NewWSID { get; set; }
       
    }
}
