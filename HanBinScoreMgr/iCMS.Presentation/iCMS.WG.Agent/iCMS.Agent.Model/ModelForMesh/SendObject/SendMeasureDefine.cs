using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;


using iMesh;
using iCMS.WG.Agent.Common;
using System.Runtime.Serialization;



namespace iCMS.WG.Agent.Model.Send
{
   public class SendMeasureDefine : SendObject
    {


       /// <summary>
        /// 采集方式 1:定时采集，2：临时采集
       /// </summary>
       public int DAQStyle { get; set; }

       /// <summary>
       /// 定时温度采集周期（仅定时采集时使用）时、分
       /// </summary>
       public byte[] DAQPeriodTemperature { get; set; }


       /// <summary>
       /// 特征值采集周期（该值为温度采集周期的整倍数值）
       /// </summary>
       public UInt16 DAQPeriodCharacterValue { get; set; }


       /// <summary>
       /// 波形采集周期（该值为温度采集周期的整倍数值）
       /// </summary>
       public UInt16 DAQPeriodWave { get; set; }

       /// <summary>
       /// 加速度是否订阅，0：否，1：是
       /// </summary>
       public int ACCSubscribe { get; set; }
       /// <summary>
       /// 加速度特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte ACCValue { get; set; }
       /// <summary>
       /// 加速度波长
       /// </summary>
       public UInt16 ACCWaveLength { get; set; }
       /// <summary>
       /// 加速度上限频率
       /// </summary>
       public UInt16 ACCUpLimitFreq { get; set; }
       /// <summary>
       /// 加速度下限频率
       /// </summary>
       public UInt16 ACCLowLimitFreq { get; set; }
       /// <summary>
       /// 加速度包络测量定义
       /// </summary>
       //public string ACCEnv_MDF { get; set; }
       /// <summary>
       /// 包络是否订阅，0：否，1：是
       /// </summary>
       public int ACCEnvlSubscribe { get; set; }
       /// <summary>
       /// 包络特征值，包括：峰值、地毯值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte ACCEnvlValue { get; set; }
       /// <summary>
       /// 包络波形
       /// </summary>
       public UInt16 ACCEnvlWaveLength { get; set; }
       /// <summary>
       /// 包络带宽
       /// </summary>
       public UInt16 ACCEnvlpBandWidth { get; set; }
       /// <summary>
       /// 包络滤波器
       /// </summary>
       public UInt16 ACCEnvlpFilter { get; set; }
        /// <summary>
       /// 速度波形测量定义
        /// </summary>
       //public string VelocityWave_MDF{ get; set; }
       /// <summary>
       /// 速度是否订阅，0：否，1：是
       /// </summary>
       public int VelSubscribe { get; set; }
       /// <summary>
       /// 速度特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte VelValue { get; set; }
       /// <summary>
       /// 速度波长
       /// </summary>
       public UInt16 VelWaveLength { get; set; }
       /// <summary>
       /// 速度上限频率
       /// </summary>
       public UInt16 VelUpLimitFreq { get; set; }
       /// <summary>
       /// 速度下限频率
       /// </summary>
       public UInt16 VelLowLimitFreq { get; set; }
        /// <summary>
       /// 位移波形测量定义
        /// </summary>
       //public string DispWave_MDF { get; set; }
       /// <summary>
       /// 位移是否订阅，0：否，1：是
       /// </summary>
       public int DispSubscribe { get; set; }
       /// <summary>
       /// 位移特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte DispValue { get; set; }
       /// <summary>
       /// 位移波形
       /// </summary>
       public UInt16 DispWaveLength { get; set; }
       /// <summary>
       /// 位移上限频率
       /// </summary>
       public UInt16 DispUpLimitFreq { get; set; }
       /// <summary>
       /// 位移下限频率
       /// </summary>
       public UInt16 DispLowLimitFreq { get; set; }
       //add by masu 2016年5月25日13:45:46 添加启停机阈值信息
       /// <summary>
       /// 启停机阈值是否订阅，0：否，1：是
       /// </summary>
       public int CriticalSubscribe { get; set; }
       /// <summary>
       /// 启停机阈值特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte CriticalValue { get; set; }
       /// <summary>
       /// 启停机阈值波形长度
       /// </summary>
       public UInt16 CriticalWaveLength { get; set; }
       /// <summary>
       /// 启停机阈值包络带宽
       /// </summary>
       public UInt16 CriticalBandWidth { get; set; }
       /// <summary>
       /// 启停机阈值包络滤波器
       /// </summary>
       public UInt16 CriticalFilter { get; set; }


       /// <summary>
       /// LQ值是否订阅，0：否，1：是
       /// </summary>
       public int LQSubscribe { get; set; }
       /// <summary>
       /// LQ值特征值，包括：有效值
       /// Bit0：RMS 有效值；
       /// </summary>
       public byte LQValue { get; set; }
       /// <summary>
       /// LQ波长
       /// </summary>
       public UInt16 LQWaveLength { get; set; }
       /// <summary>
       /// LQ速度上限频率
       /// </summary>
       public UInt16 LQUpLimitFreq { get; set; }
       /// <summary>
       /// LQ速度下限频率
       /// </summary>
       public UInt16 LQLowLimitFreq { get; set; }





       /// <summary>
       /// 设备总数
       /// </summary>
       public int DevTotal { get; set; }
       /// <summary>
       ///设备编号
       /// </summary>
       public int DevNum { get; set; }




       /// <summary>
       /// 记录重发次数
       /// </summary>
       public int TryAgainnum = 0;
      
       [IgnoreDataMember]
       public System.Threading.Timer time { set; get; }
    }
}
