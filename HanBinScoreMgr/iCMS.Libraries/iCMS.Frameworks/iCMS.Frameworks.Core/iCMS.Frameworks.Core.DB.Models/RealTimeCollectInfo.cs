/************************************************************************************
* Copyright (c) @ILine All Rights Reserved.
* 命名空间：
* 文件名：  
* 创建人：  张辽阔
* 创建时间：2016.07.21
* 描述：实时数据汇总表实体
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 实时数据汇总表
    /// </summary>
    [Table("T_DATA_REALTIME_COLLECT_INFO")]
    public class RealTimeCollectInfo : EntityBase
    {
        #region Model
        public RealTimeCollectInfo()
        {
            MSSpeedUnit = "mm/s";
            MSACCUnit = "m/s^2 ";
            MSEnvelopingUnit = "m/s^2";
            MSDispUnit = "μm";
            MSLQUnit = "";
            MSWSTemperatureUnit = "°C";
            MSDevTemperatureUnit = "°C";
            MSWSBatteryVolatageUnit = "V";
        }

        /// <summary>
        /// 主ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int? DevID { get; set; }

        ///// <summary>
        ///// 设备信息实体集合
        ///// </summary>
        //public IList<T_SYS_DEVICE> DEVICE_List { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int? MSID { get; set; }

        ///// <summary>
        ///// 测量位置实体集合
        ///// </summary>
        //public IList<T_SYS_MEASURESITE> MEASURESITE_List { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSName { get; set; }

        /// <summary>
        /// 报警状态0未采集，1正常，2高报，3高高报
        /// </summary>
        public int? MSStatus { get; set; }

        /// <summary>
        /// 测量位置描述信息
        /// </summary>
        public string MSDesInfo { get; set; }

        /// <summary>
        /// 测量位置当前数据更新状态
        /// </summary>
        public int? MSDataStatus { get; set; }

        /// <summary>
        /// 速度信号ID
        /// </summary>
        public int? MSSpeedSingalID { get; set; }

        /// <summary>
        /// 速度单位
        /// </summary>
        public string MSSpeedUnit { get; set; }

        /// <summary>
        /// 测量最新的速度有效值
        /// </summary>
        public float? MSSpeedVirtualValue { get; set; }

        /// <summary>
        /// 测量最新的速度峰值
        /// </summary>
        public float? MSSpeedPeakValue { get; set; }

        /// <summary>
        /// 测量最新的速度峰峰值
        /// </summary>
        public float? MSSpeedPeakPeakValue { get; set; }

        /// <summary>
        /// 测量位置最新的速度高频能量值
        /// </summary>
        public float? MSSpeedHPEValue { get; set; }

        /// <summary>
        /// 测量位置最新的速度中频能量值
        /// </summary>
        public float? MSSpeedMPEValue { get; set; }

        /// <summary>
        /// 测量位置最新的速度低频能量值
        /// </summary>
        public float? MSSpeedLPEValue { get; set; }

        /// <summary>
        /// 测量最新的速度有效值的状态
        /// </summary>
        public int? MSSpeedVirtualStatus { get; set; }

        /// <summary>
        /// 测量最新的速度峰值的状态
        /// </summary>
        public int? MSSpeedPeakStatus { get; set; }

        /// <summary>
        /// 测量最新的速度峰峰值的状态
        /// </summary>
        public int? MSSpeedPeakPeakStatus { get; set; }

        /// <summary>
        /// 测量位置最新的速度高频能量值的状态
        /// </summary>
        public int? MSSpeedHPEStatus { get; set; }

        /// <summary>
        /// 测量位置最新的速度中频能量值的状态
        /// </summary>
        public int? MSSpeedMPEStatus { get; set; }

        /// <summary>
        /// 测量位置最新的速度低频能量值的状态
        /// </summary>
        public int? MSSpeedLPEStatus { get; set; }

        /// <summary>
        /// 测量最新的速度有效值的采集时间
        /// </summary>
        public DateTime? MSSpeedVirtualTime { get; set; }

        /// <summary>
        /// 测量最新的速度峰值的采集时间
        /// </summary>
        public DateTime? MSSpeedPeakTime { get; set; }

        /// <summary>
        /// 测量最新的速度峰峰值的采集时间
        /// </summary>
        public DateTime? MSSpeedPeakPeakTime { get; set; }

        /// <summary>
        /// 测量位置最新的速度高频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedHPETime { get; set; }

        /// <summary>
        /// 测量位置最新的速度中频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedMPETime { get; set; }

        /// <summary>
        /// 测量位置最新的速度低频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedLPETime { get; set; }

        /// <summary>
        /// 加速度信号ID
        /// </summary>
        public int? MSACCSingalID { get; set; }

        /// <summary>
        /// 加速度单位
        /// </summary>
        public string MSACCUnit { get; set; }

        /// <summary>
        /// 测量最新的加速度有效值
        /// </summary>
        public float? MSACCVirtualValue { get; set; }

        /// <summary>
        /// 测量最新的加速度峰值
        /// </summary>
        public float? MSACCPeakValue { get; set; }

        /// <summary>
        /// 测量最新的加速度峰峰值
        /// </summary>
        public float? MSACCPeakPeakValue { get; set; }

        /// <summary>
        /// 测量最新的加速度有效值的状态
        /// </summary>
        public int? MSACCVirtualStatus { get; set; }

        /// <summary>
        /// 测量最新的加速度峰值的状态
        /// </summary>
        public int? MSACCPeakStatus { get; set; }

        /// <summary>
        /// 测量最新的加速度峰峰值的状态
        /// </summary>
        public int? MSACCPeakPeakStatus { get; set; }

        /// <summary>
        /// 测量最新的加速度有效值的采集时间
        /// </summary>
        public DateTime? MSACCVirtualTime { get; set; }

        /// <summary>
        /// 测量最新的加速度峰值的采集时间
        /// </summary>
        public DateTime? MSACCPeakTime { get; set; }

        /// <summary>
        /// 测量最新的加速度峰峰值的采集时间
        /// </summary>
        public DateTime? MSACCPeakPeakTime { get; set; }

        /// <summary>
        /// 位移信号ID
        /// </summary>
        public int? MSDispSingalID { get; set; }

        /// <summary>
        /// 位移单位
        /// </summary>
        public string MSDispUnit { get; set; }

        /// <summary>
        /// 测量最新的位移有效值
        /// </summary>
        public float? MSDispVirtualValue { get; set; }

        /// <summary>
        /// 测量最新的位移峰值
        /// </summary>
        public float? MSDispPeakValue { get; set; }

        /// <summary>
        /// 测量最新的位移峰峰值
        /// </summary>
        public float? MSDispPeakPeakValue { get; set; }

        /// <summary>
        /// 测量最新的位移有效值的状态
        /// </summary>
        public int? MSDispVirtualStatus { get; set; }

        /// <summary>
        /// 测量最新的位移峰值的状态
        /// </summary>
        public int? MSDispPeakStatus { get; set; }

        /// <summary>
        /// 测量最新的位移峰峰值的状态
        /// </summary>
        public int? MSDispPeakPeakStatus { get; set; }

        /// <summary>
        /// 测量最新的位移有效值的采集时间
        /// </summary>
        public DateTime? MSDispVirtualTime { get; set; }

        /// <summary>
        /// 测量最新的位移峰值的采集时间
        /// </summary>
        public DateTime? MSDispPeakTime { get; set; }

        /// <summary>
        /// 测量最新的位移峰峰值的采集时间
        /// </summary>
        public DateTime? MSDispPeakPeakTime { get; set; }

        /// <summary>
        /// 包络信号ID
        /// </summary>
        public int? MSEnvelSingalID { get; set; }

        /// <summary>
        /// 测量位置最新的包络峰值
        /// </summary>
        public float? MSEnvelopingPEAKValue { get; set; }

        /// <summary>
        /// 测量位置最新的包络地毯值
        /// </summary>
        public float? MSEnvelopingCarpetValue { get; set; }

        /// <summary>
        /// 测量位置最新的包络均值
        /// </summary>
        public float? MSEnvelopingMEANValue { get; set; }

        /// <summary>
        /// 包络单位
        /// </summary>
        public string MSEnvelopingUnit { get; set; }

        /// <summary>
        /// 测量位置最新的包络峰值的状态
        /// </summary>
        public int? MSEnvelopingPEAKStatus { get; set; }

        /// <summary>
        /// 测量位置最新的包络地毯值的状态
        /// </summary>
        public int? MSEnvelopingCarpetStatus { get; set; }

        /// <summary>
        /// 测量位置最新的包络均值的状态
        /// </summary>
        public int? MSEnvelopingMEANStatus { get; set; }

        /// <summary>
        /// 测量位置最新的包络峰值采集时间
        /// </summary>
        public DateTime? MSEnvelopingPEAKTime { get; set; }

        /// <summary>
        /// 测量位置最新的包络地毯值采集时间
        /// </summary>
        public DateTime? MSEnvelopingCarpetTime { get; set; }

        /// <summary>
        /// 测量位置最新的包络均值的采集时间
        /// </summary>
        public DateTime? MSEnvelopingMEANTime { get; set; }

        /// <summary>
        /// LQ信号ID
        /// </summary>
        public int? MSLQSingalID { get; set; }

        /// <summary>
        /// 测量位置最新的LQ值
        /// </summary>
        public float? MSLQValue { get; set; }

        /// <summary>
        /// 测量位置最新的LQ值的状态
        /// </summary>
        public int? MSLQStatus { get; set; }

        /// <summary>
        /// LQ值单位
        /// </summary>
        public string MSLQUnit { get; set; }

        /// <summary>
        /// 测量位置最新的LQ值采集时间
        /// </summary>
        public DateTime? MSLQTime { get; set; }

        /// <summary>
        /// 测量位置最新的设备温度的状态
        /// </summary>
        public int? MSDevTemperatureStatus { get; set; }

        /// <summary>
        /// 测量位置最新的设备温度值
        /// </summary>
        public float? MSDevTemperatureValue { get; set; }

        /// <summary>
        /// 设备温度单位
        /// </summary>
        public string MSDevTemperatureUnit { get; set; }

        /// <summary>
        /// 测量位置最新的设备温度值的采集时间
        /// </summary>
        public DateTime? MSDevTemperatureTime { get; set; }

        /// <summary>
        /// 测量位置最新的传感器温度的状态
        /// </summary>
        public int? MSWSTemperatureStatus { get; set; }

        /// <summary>
        /// 测量位置最新的传感器温度值
        /// </summary>
        public float? MSWSTemperatureValue { get; set; }

        /// <summary>
        /// 传感器温度单位
        /// </summary>
        public string MSWSTemperatureUnit { get; set; }

        /// <summary>
        /// 测量位置最新的传感器温度值的采集时间
        /// </summary>
        public DateTime? MSWSTemperatureTime { get; set; }

        /// <summary>
        /// 无线传感器最新的电池电压值
        /// </summary>
        public float? MSWSBatteryVolatageValue { get; set; }

        /// <summary>
        /// 电池电压单位
        /// </summary>
        public string MSWSBatteryVolatageUnit { get; set; }

        /// <summary>
        /// 无线传感器最新的电池电压状态
        /// </summary>
        public int? MSWSBatteryVolatageStatus { get; set; }

        /// <summary>
        /// 无线传感器电池电压采集时间
        /// </summary>
        public DateTime? MSWSBatteryVolatageTime { get; set; }

        /// <summary>
        /// WG连接状态
        /// </summary>
        public int? MSWGLinkStatus { get; set; }

        /// <summary>
        /// 测量位置最新的实时转速值
        /// </summary>
        public int? MSRealTimeSpeedValue { get; set; }

        /// <summary>
        /// 测量位置最新的实时转速的采集时间
        /// </summary>
        public DateTime? MSRealTimeSpeedTime { get; set; }

        #endregion Model
    }
}