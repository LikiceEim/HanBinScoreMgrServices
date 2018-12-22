using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Send
{
    public class CalibrateTime : SendObject
    {
 

        /// <summary>
        /// 校准时间（秒）
        /// </summary>
        public UInt64 Seconds { get; set; }

        /// <summary>
        /// 校准时间（微妙）
        /// </summary>
        public UInt32 Microseconds { get; set; }
        
    }
}
