using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 传感器校准
    /// </summary>
    public class TransducerAdjust : ReceiveObject
    {
        /// <summary>
        /// 校准是否成功1: 成功0: 失败
        /// </summary>
        public UInt16 IsSuccessful { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public UInt16 ErrorCode { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain { get; set; }
        /// <summary>
        /// 偏移
        /// </summary>
        public float Offset { get; set; }
       
    }
}
