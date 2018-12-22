using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Send
{
    public class SetNewWSID : SendObject
    {
        /// <summary>
        /// 新WSID
        /// </summary>
        public Byte NewWSID { get; set; }

        /// <summary>
        /// 保留字段1
        /// </summary>
        public UInt32 Reserved1 { get; set; }

        /// <summary>
        /// 保留字段2
        /// </summary>
        public UInt32 Reserved2 { get; set; }

        /// <summary>
        /// 记录重发次数
        /// </summary>
        public int TryAgainnum = 0;
    }
}
