using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 温度、电池电压数据
    /// </summary>
    public class WSTemperature : ReceiveObject
    {
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public float Volatage { get; set; }

    }
   

}
