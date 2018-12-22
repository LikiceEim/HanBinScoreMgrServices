/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  MSiteRecordForDeviceTreeParameter
 * 创建人：  张辽阔
 * 创建时间：2016-11-01
 * 描述：添加测量位置信息参数
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 添加测量位置
    /// </summary>
    public class AddMSiteRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MeasureSiteID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public int MeasureSiteName { get; set; }

        /// <summary>
        /// 测量位置类型
        /// </summary>
        public int MSiteTypeId { get; set; }

        /// <summary>
        /// 传感器灵敏度系数A
        /// </summary>
        public float SensorCosA { get; set; }

        /// <summary>
        /// 传感器灵敏度系数B
        /// </summary>
        public float SensorCosB { get; set; }

        /// <summary>
        /// 波形采集时间间隔
        /// </summary>
        public string WaveTime { get; set; }

        /// <summary>
        /// 特征值采集时间间隔
        /// </summary>
        public string FlagTime { get; set; }

        /// <summary>
        /// 温度、电池电压采集时间间隔
        /// </summary>
        public string TemperatureTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 测点序号
        /// </summary>
        public int SerialNo { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 是否配置设备温度
        /// </summary>
        public int IfDeviceTemperature { get; set; }

        /// <summary>
        /// 设备温度中级报警
        /// </summary>
        public float DeviceTemperatureAlarmValue { get; set; }

        /// <summary>
        /// 设备温度高级报警
        /// </summary>
        public float DeviceTemperatureDangerValue { get; set; }

        /// <summary>
        /// 是否配置传感器温度
        /// </summary>
        public int IfWSTemperature { get; set; }

        /// <summary>
        /// 传感器温度中级报警
        /// </summary>
        public float WSTemperatureAlarmValue { get; set; }

        /// <summary>
        /// 传感器温度高级报警
        /// </summary>
        public float WSTemperatureDangerValue { get; set; }

        /// <summary>
        /// 是否配置电池电压
        /// </summary>
        public int IfVoltage { get; set; }

        /// <summary>
        /// 电池电压中级报警
        /// </summary>
        public float VoltageAlarmValue { get; set; }

        /// <summary>
        /// 电池电压高级报警
        /// </summary>
        public float VoltageDangerValue { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：采集温度的采集单元ID
        /// </summary>
        public int TemperatureWGID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：温度采集通道
        /// </summary>
        public int TemperatureWSID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：传感器灵敏度系数
        /// </summary>
        public float SensorCoefficient { get; set; }

        /// <summary>
        /// 振动集合
        /// </summary>
        public List<VibSignalInfoForMeasureSite> VibSignalInfo { get; set; }

        /// <summary>
        /// 轴承列表
        /// </summary>
        public List<BearingInfoForMeasureSite> BearingInfoList { get; set; }
    }

    /// <summary>
    /// 振动信号
    /// </summary>
    public class VibSignalInfoForMeasureSite
    {
        /// <summary>
        /// 振动信号Id
        /// </summary>
        public int SingalID { get; set; }

        /// <summary>
        /// 上限ID
        /// </summary>
        public int UpperLimitID { get; set; }

        /// <summary>
        /// 下限ID
        /// </summary>
        public int LowLimitID { get; set; }

        /// <summary>
        /// 波长ID
        /// </summary>
        public int WaveLengthID { get; set; }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int VibrationSignalTypeID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：采集时长
        /// </summary>
        public int SamplingTimePeriod { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：包络带宽
        /// </summary>
        public int EnvelopBandWith { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：包络滤波器上限
        /// </summary>
        public int EnvelopeFilterUpLimitFreq { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：包络滤波器下限
        /// </summary>
        public int EnvelopeFilterLowLimitFreq { get; set; }

        /// <summary>
        /// 特征值波长
        /// </summary>
        public int EigenWaveLengthID { get; set; }

        /// <summary>
        /// 特征值集合
        /// </summary>
        public List<EigenValueInfoForMeasureSite> EigenValueInfo { get; set; }
    }

    /// <summary>
    /// 添加轴承信息
    /// </summary>
    public class BearingInfoForMeasureSite
    {
        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID { get; set; }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType { get; set; }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm { get; set; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }
    }

    /// <summary>
    /// 特征值类型
    /// </summary>
    public class EigenValueInfoForMeasureSite
    {
        /// <summary>
        /// 阈值Id
        /// </summary>
        public int SingalAlmID { get; set; }

        /// <summary>
        /// 特征值ID
        /// </summary>
        public int EigenValueTypeID { get; set; }

        /// <summary>
        /// 上传触发值
        /// </summary>
        public float? UploadTrigger { get; set; }

        /// <summary>
        /// 报警阈值
        /// </summary>
        public float AlarmValue { get; set; }

        /// <summary>
        /// 危险阈值
        /// </summary>
        public float DangerValue { get; set; }

        /// <summary>
        /// 趋势报警阈值
        /// </summary>
        public float? ThrendAlarmPrvalue { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：能量值上限频率
        /// </summary>
        public int? EnergyUpLimit { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：能量值下限频率
        /// </summary>
        public int? EnergyLowLimit { get; set; }
    }
}