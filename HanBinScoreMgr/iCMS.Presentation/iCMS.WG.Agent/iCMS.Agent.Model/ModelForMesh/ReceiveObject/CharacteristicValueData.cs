using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;


namespace iCMS.WG.Agent.Model.Receive
{
    public class CharacteristicValueData : ReceiveObject
    {
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SampleTime { get; set; }
        /// <summary>
        /// 采集方式  1：定时采集；2：临时采集
        /// </summary>
        public EnumSamplingType SamplingType { get; set; }

        /// <summary>
        /// 类型 - 加速度:1；速度:2；位移:3；加速度包络:4；原始波形:5；
        /// </summary>
        public EnumWaveType WaveType { get; set; }
        /// <summary>
        /// 加速度有效值、峰值、峰峰值
        /// </summary>
        public Byte[] ACCCharcPara { get; set; }
        /// <summary>
        /// 速度有效值、峰值、峰峰值
        /// </summary>
        public Byte[] VELCharcPara { get; set; }
        /// <summary>
        /// 位移有效值、峰值、峰峰值
        /// </summary>
        public Byte[] DISCharcPara { get; set; }
        /// <summary>
        /// 加速度包络峰峰值、地毯值
        /// </summary>
        public Byte[] ACCEnvCharcPara { get; set; }
        
    }

   

}
