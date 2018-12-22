using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 收到的WS自报告
    /// </summary>
    public class WSSelfReport : ReceiveObject
    {
        /// <summary>
        /// WS状态
        /// </summary>
        public EnmuWSStates WSStates { get; set; }
        /// <summary>
        /// WS 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public Int16 ErrorCode { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        public float Voltage { get; set; }

        public EnmuWakeupMode WakeupMode { get; set; }

        public EnmuMoteBoot MoteBoot { get; set; }
        /// <summary>
        /// 采集间隔时间
        /// </summary>
        public tTimingTime TimeTemp { get; set; }
        /// <summary>
        /// 特征值采集计数
        /// </summary>
        public UInt16 CharCnt { get; set; }
        /// <summary>
        /// 波形采集计数
        /// </summary>
        public UInt16 WaveCnt { get; set; }
        /// <summary>
        /// 时分秒
        /// </summary>
        public tRtcTime RTCTime { get; set; }
    }

    
}
