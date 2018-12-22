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
   public class SendTriggerDefine : SendObject
    {
       
       /// <summary>
       /// 功能使能 0:功能关闭，1：功能开启
       /// </summary>
       public int bEnable { get; set; }

       /// <summary>
       /// 加速度测量定义
       /// </summary>
       //public string ACCWave_MDF { get; set; }
       /// <summary>
       /// 加速度是否使能，0：否，1：是
       /// </summary>
       public int ACCEnable { get; set; }
       /// <summary>
       /// 加速度特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte ACCValue { get; set; }
       /// <summary>
       /// 加速度有效值的阈值
       /// </summary>
       public float ACCRmsThreshold { get; set; }
       /// <summary>
       /// 加速度峰值的阈值
       /// </summary>
       public float ACCPKThreshold { get; set; }
       /// <summary>
       /// 加速度峰峰值的阈值
       /// </summary>
       public float ACCPKPKThreshold { get; set; }
     
       /// <summary>
       /// 加速度包络测量定义
       /// </summary>
       //public string ACCEnvlWave_MDF { get; set; }
       /// <summary>
       /// 包络是否使能，0：否，1：是
       /// </summary>
       public byte ACCEnvlEnable { get; set; }
       /// <summary>
       /// 包络特征值，包括：峰值、地毯值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte ACCEnvlValue { get; set; }
       /// <summary>
       /// 包络峰值的阈值
       /// </summary>
       public float ACCEnvlPKThreshold { get; set; }
       /// <summary>
       /// 包络地毯值的阈值
       /// </summary>
       public float ACCEnvlPKCThreshold { get; set; }
     
        /// <summary>
       /// 速度波形测量定义
       /// </summary>
      // public string VelWave_MDF { get; set; }
       /// <summary>
       /// 速度是否使能，0：否，1：是
       /// </summary>
       public byte VelEnable { get; set; }
       /// <summary>
       /// 速度特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte VelValue { get; set; }
       /// <summary>
       /// 速度有效值的阈值
       /// </summary>
       public float VelRmsThreshold { get; set; }
       /// <summary>
       /// 速度峰值的阈值
       /// </summary>
       public float VelPKThreshold { get; set; }
       /// <summary>
       /// 速度峰峰值的阈值
       /// </summary>
       public float VelPKPKThreshold { get; set; }
      
        /// <summary>
       /// 位移波形测量定义
        /// </summary>
      // public string DispWave_MDF { get; set; }
       /// <summary>
       /// 位移是否订阅，0：否，1：是
       /// </summary>
       public byte DispEnable { get; set; }
       /// <summary>
       /// 位移特征值，包括：有效值、峰值、峰峰值
       /// Bit0：RMS 有效值；Bit1：PK 峰值；Bit2：PKPK 峰峰值；Bit3：PKC 地毯值
       /// </summary>
       public byte DispValue { get; set; }
       /// <summary>
       /// 位移有效值的阈值
       /// </summary>
       public float DispRmsThreshold { get; set; }
       /// <summary>
       /// 位移峰值的阈值
       /// </summary>
       public float DispPKThreshold { get; set; }
       /// <summary>
       /// 位移峰峰值的阈值
       /// </summary>
       public float DispPKPKThreshold { get; set; }   

       /// <summary>
       /// 记录重发次数
       /// </summary>
       public int TryAgainnum = 0;
      
       [IgnoreDataMember]
       public System.Threading.Timer time { set; get; }
    }
}
