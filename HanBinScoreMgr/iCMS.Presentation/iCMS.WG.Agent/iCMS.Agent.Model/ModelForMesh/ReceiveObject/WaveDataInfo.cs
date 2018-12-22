using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;



namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 波形数据
    /// </summary>
    public class WaveDataInfo : ReceiveObject
    {
        /// <summary>
        /// 波形类型 - 加速度:1；速度:2；位移:3；加速度包络:4；原始波形:5；
        /// </summary>
        public EnumWaveType WaveType { get; set; }
        /// <summary>
        /// 总帧数
        /// </summary>
        public Int16 TotalFramesNum { get; set; }

        /// <summary>
        /// 当前帧数
        /// </summary>
        public Int16 CurrentFrameID { get; set; }

        /// <summary>
        /// 当前包波形数据长度
        /// </summary>
        public Byte PacketSize { get; set; }

        /// <summary>
        /// 波形数据
        /// </summary>
        public Byte[] WaveData { get; set; }
       
    }
}
