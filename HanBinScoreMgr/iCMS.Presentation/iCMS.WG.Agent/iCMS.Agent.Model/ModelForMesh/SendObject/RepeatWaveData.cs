using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Send
{
    public class RepeatWaveData:SendObject
    {
   

        /// <summary>
        /// 重发报文编号集合
        /// </summary>
        public IList<UInt16> RepeatNum { get; set; }
    }
}
