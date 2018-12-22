/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  DevImgDataResult
 *创建人：  王颖辉
 *创建时间：2016年10月28日14:05:41
 *描述：形貌图返回结果参数
/************************************************************************************/
using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    #region 形貌图返回结果参数
    /// <summary>
    /// 形貌图返回结果参数
    /// </summary>
    public class DevImgDataResult
    {
        //测量位置状态信息集合
        public List<MSStatusInfo> MSStatusInfos { get; set; }
        //设备ID
        public string DevID { get; set; }
        //设备名称
        public string DevName { get; set; }
        //设备转速
        public string DevRoatate { get; set; }
        //设备当前状态 0：正常，1：高报，2：高高报
        public string DevStatus { get; set; }
        //运行状态3停机，1运行，2检修
        public string DevRunningStatus { get; set; }

        #region 新增设备类型ID, 设备类型名称 Modified by QXM 2016/09/01

        /// <summary>
        /// 设备类型ID
        /// </summary>
        public string DeviceTypeID { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName { get; set; }
        #endregion

        /// <summary>
        /// 投运时间
        /// </summary>
        public string OperationDate
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型:0主用,1备用
        /// </summary>
        public int UseType
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 测量位置状态信息
    /// </summary>
    public class MSStatusInfo
    {
        #region Model
        /// <summary>
        /// 设备ID
        /// </summary>
        public int? DevID { get; set; }
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int? MSiteID { get; set; }
        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName { get; set; }
        /// <summary>
        /// 报警状态0未采集，1正常，2高报，3高高报
        /// </summary>
        public int? MSStatus { get; set; }
        /// <summary>
        /// 测量位置描述信息
        /// </summary>
        public string MSDesInfo { get; set; }
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

        #region 添加速度报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量最新的速度有效值报警值
        /// </summary>
        public float? MSSpeedVirtualAlarmValue { get; set; }

        /// <summary>
        /// 测量最新的速度有效值危险值
        /// </summary>
        public float? MSSpeedVirtualDangerValue { get; set; }


        /// <summary>
        /// 测量最新的速度峰值报警值
        /// </summary>
        public float? MSSpeedPeakAlarmValue { get; set; }

        /// <summary>
        /// 测量最新的速度峰值危险值
        /// </summary>
        public float? MSSpeedPeakDangerValue { get; set; }


        /// <summary>
        /// 测量最新的速度峰峰值报警值
        /// </summary>
        public float? MSSpeedPeakPeakAlarmValue { get; set; }

        /// <summary>
        /// 测量最新的速度峰峰值危险值
        /// </summary>
        public float? MSSpeedPeakPeakDangerValue { get; set; }
        #endregion

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

        #region 添加加速度报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量最新的加速度有效值报警值
        /// </summary>
        public float? MSACCVirtualAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的加速度有效值危险值
        /// </summary>
        public float? MSACCVirtualDangerValue { get; set; }

        /// <summary>
        /// 测量最新的加速度峰值报警值
        /// </summary>
        public float? MSACCPeakAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的加速度峰值危险值
        /// </summary>
        public float? MSACCPeakDangerValue { get; set; }

        /// <summary>
        /// 测量最新的加速度峰峰值报警值
        /// </summary>
        public float? MSACCPeakPeakAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的加速度峰峰值危险值
        /// </summary>
        public float? MSACCPeakPeakDangerValue { get; set; }

        #endregion


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

        #region 添加位移报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量最新的位移有效值报警
        /// </summary>
        public float? MSDispVirtualAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的位移有效值危险
        /// </summary>
        public float? MSDispVirtualDangerValue { get; set; }
        /// <summary>
        /// 测量最新的位移峰值报警
        /// </summary>
        public float? MSDispPeakAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的位移峰值危险
        /// </summary>
        public float? MSDispPeakDangerValue { get; set; }
        /// <summary>
        /// 测量最新的位移峰峰值报警
        /// </summary>
        public float? MSDispPeakPeakAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的位移峰峰值危险
        /// </summary>
        public float? MSDispPeakPeakDangerValue { get; set; }
        #endregion

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
        /// 测量位置最新的包络峰值采集时间
        /// </summary>
        public DateTime? MSEnvelopingPEAKTime { get; set; }
        /// <summary>
        /// 测量位置最新的包络地毯值采集时间
        /// </summary>
        public DateTime? MSEnvelopingCarpetTime { get; set; }

        #region 添加包络报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量位置最新的包络峰值报警
        /// </summary>
        public float? MSEnvelopingPEAKAlarmValue { get; set; }
        /// <summary>
        /// 测量位置最新的包络峰值危险
        /// </summary>
        public float? MSEnvelopingPEAKDangerValue { get; set; }
        /// <summary>
        /// 测量位置最新的包络地毯值报警
        /// </summary>
        public float? MSEnvelopingCarpetAlarmValue { get; set; }
        /// <summary>
        /// 测量位置最新的包络地毯值危险
        /// </summary>
        public float? MSEnvelopingCarpetDangerValue { get; set; }
        #endregion

        /// <summary>
        /// LQ信号ID
        /// </summary>
        public int? MSLQSingalID { get; set; }
        /// <summary>
        /// 测量位置最新的轴承状态采集值
        /// </summary>
        public float? MSLQValue { get; set; }
        /// <summary>
        /// 测量位置最新的轴承状态采集值的状态
        /// </summary>
        public int? MSLQStatus { get; set; }
        /// <summary>
        /// 轴承状态采集值单位
        /// </summary>
        public string MSLQUnit { get; set; }
        /// <summary>
        /// 测量位置最新的轴承状态采集值采集时间
        /// </summary>
        public DateTime? MSLQTime { get; set; }

        #region 添加LQ报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量位置最新的轴承状态采集值报警
        /// </summary>
        public float? MSLQAlarmValue { get; set; }
        /// <summary>
        /// 测量位置最新的轴承状态采集值危险
        /// </summary>
        public float? MSLQDangerValue { get; set; }
        #endregion


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

        #region 添加设备温度报警阈值 王颖辉  2017-10-27
        /// <summary>
        /// 测量位置最新的设备温度值报警
        /// </summary>
        public float? MSDevTemperatureAlarmValue { get; set; }
        /// <summary>
        /// 测量位置最新的设备温度值危险
        /// </summary>
        public float? MSDevTemperatureDangerValue { get; set; }
        #endregion

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
        /// WS连接状态
        /// </summary>
        public int? MSWSLinkStatus { get; set; }

        /// <summary>
        /// 测量位置类型Code
        /// </summary>
        public string MeasureSiteTypeCode
        {
            get;
            set;
        }

        #endregion Model

        #region 解决方案融合添加
        /// <summary>
        /// 测量最新的速度-低频能量值
        /// </summary>
        public float? MSSpeedLPEValue { get; set; }
        /// <summary>
        /// 测量最新的速度-低频能量值的状态 1：正常，2：警告，3：报警
        /// </summary>
        public float? MSSpeedLPEStatus { get; set; }
        /// <summary>
        /// 测量最新的速度-低频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedLPETime { get; set; }
        /// <summary>
        /// 测量最新的速度-低频能量值高报阈值
        /// </summary>
        public float? MSSpeedLPEAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的速度-低频能量值高高报阈值
        /// </summary>
        public float? MSSpeedLPEDangerValue { get; set; }
        /// <summary>
        /// 测量最新的速度-低频能量值单位
        /// </summary>
        //public string MSSpeedLowFrequencyUnit { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值
        /// </summary>
        public float? MSSpeedMPEValue { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值的状态 1：正常，2：警告，3：报警
        /// </summary>
        public int? MSSpeedMPEStatus { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedMPETime { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值高报阈值
        /// </summary>
        public float? MSSpeedMPEAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值高高报阈值
        /// </summary>
        public float? MSSpeedMPEDangerValue { get; set; }
        /// <summary>
        /// 测量最新的速度-中频能量值单位
        /// </summary>
        //public string MSSpeedMiddleFrequencyUnit { get; set; }
        /// <summary>
        /// 测量最新的速度-高频能量值
        /// </summary>
        public float? MSSpeedHPEValue { get; set; }
        /// <summary>
        /// 测量最新的速度-高频能量值的状态 1：正常，2：警告，3：报警
        /// </summary>
        public int? MSSpeedHPEStatus { get; set; }
        /// <summary>
        /// 测量最新的速度-高频能量值的采集时间
        /// </summary>
        public DateTime? MSSpeedHPETime { get; set; }
        /// <summary>
        /// 测量最新的速度-高频能量值高报阈值
        /// </summary>
        public float? MSSpeedHPEAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的速度-高频能量值高高报阈值
        /// </summary>
        public float? MSSpeedHPEDangerValue { get; set; }
        /// <summary>
        /// 	string	测量最新的速度-高频能量值单位
        /// </summary>
        //public string MSSpeedUpFrequencyUnit { get; set; }
        /// <summary>
        /// 测量最新的包络-均值
        /// </summary>
        public float? MSEnvelopeMeanValue { get; set; }
        /// <summary>
        /// 测量最新的包络-均值的状态 1：正常，2：警告，3：报警
        /// </summary>
        public int? MSEnvelopeMeanStatus { get; set; }
        /// <summary>
        /// 测量最新的包络-均值的采集时间
        /// </summary>
        public DateTime? MSEnvelopeMeanTime { get; set; }
        /// <summary>
        /// 测量最新的包络-均值高报阈值
        /// </summary>
        public float? MSEnvelopeMeanAlarmValue { get; set; }
        /// <summary>
        /// 测量最新的包络-均值高高报阈值
        /// </summary>
        public float? MSEnvelopeMeanDangerValue { get; set; }
        /// <summary>
        /// 测量最新的包络-均值单位
        /// </summary>
        //public string MSEnvelopeMeanUnit { get; set; }
        #endregion
    }
    #endregion
}
