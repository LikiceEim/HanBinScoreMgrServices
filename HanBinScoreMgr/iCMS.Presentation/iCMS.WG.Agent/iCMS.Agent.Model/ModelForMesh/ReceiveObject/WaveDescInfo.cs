using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Model.Receive
{
    /// <summary>
    /// 波形描述信息
    /// </summary>
    public class WaveDescInfo : ReceiveObject
    {
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }
        /// <summary>
        /// 波形类型 - 加速度:1；速度:2；位移:3；加速度包络:4；原始波形:5；
        /// </summary>
        public EnumWaveType WaveType { get; set; }
        /// <summary>
        /// 采集方式  1：定时采集；2：临时采集
        /// </summary>
        public EnumSamplingType SamplingType { get; set; }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int[] CharacterType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public Int16 WaveLength { get; set; }
        /// <summary>
        /// 下限频率
        /// </summary>
        public Int16 LowerLimit { get; set; }
        /// <summary>
        /// 上限频率
        /// </summary>
        public Int16 UpperLimit { get; set; }
        /// <summary>
        /// 幅值缩放因子
        /// </summary>
        public float AmplitueScaler { get; set; }
        /// <summary>
        /// 总帧数
        /// </summary>
        public Int16 TotalFramesNum { get; set; }
        /// <summary>
        /// 当前帧数
        /// </summary>
        public Int16 CurrentFrameID { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public float RMS { get;set; }

        /// <summary>
        /// 峰值
        /// </summary>
        public float PK { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public float PPK { get; set; }

        /// <summary>
        /// 地毯值
        /// </summary>
        public float GPKC { get; set; }

    }

}
