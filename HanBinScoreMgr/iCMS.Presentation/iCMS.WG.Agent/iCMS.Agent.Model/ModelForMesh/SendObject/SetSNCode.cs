using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Send
{
    public class SetSNCode : SendObject
    {
        /// <summary>
        /// SN
        /// </summary>
        public Byte[] sn { get; set; }
        /// <summary>
        /// 记录重发次数
        /// </summary>
        public int TryAgainnum = 0;
    }
}
