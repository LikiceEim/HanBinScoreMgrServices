/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloudOperationType
 *创建人：  
 *创建时间：
 *描述：工厂枚举
 *
 *修改人：张辽阔
 *修改时间：2016-12-16
 *修改内容：迁移至该命名空间下
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    #region 佳讯云平台数据同步类型
    /// <summary>
    /// 佳讯云平台数据同步类型
    /// </summary>
    public enum EnumCloudOperationType
    {
        /// <summary>
        /// 链路数据
        /// </summary>
        [Description("链路数据")]
        Link = 1,
        /// <summary>
        /// 工厂数据
        /// </summary>
        [Description("工厂数据")]
        Enterprise = 2,
        /// <summary>
        /// 工厂图片数据
        /// </summary>
        [Description("工厂图片数据")]
        EnterpriseImage = 3,
        /// <summary>
        /// 车间数据
        /// </summary>
        [Description("车间数据")]
        Workshop = 4,
        /// <summary>
        /// 车间图片数据
        /// </summary>
        [Description("车间图片数据")]
        WorkshopImage = 5,
        /// <summary>
        /// 设备数据
        /// </summary>
        [Description("设备数据")]
        Device = 6,
        /// <summary>
        /// 设备图片数据
        /// </summary>
        [Description("设备图片数据")]
        DeviceImage = 7,
        /// <summary>
        /// 设备数据
        /// </summary>
        [Description("无线网关数据")]
        WirelessGateway = 8,
        /// <summary>
        /// 测量位置数据
        /// </summary>
        [Description("测量位置数据")]
        Measuresite = 9,
        /// <summary>
        /// 振动信号数据
        /// </summary>
        [Description("振动信号数据")]
        VibSignal = 10,
        /// <summary>
        /// 传感器数据
        /// </summary>
        [Description("传感器数据")]
        Sensor = 11,
        /// <summary>
        /// 传感器图片数据
        /// </summary>
        [Description("传感器图片数据")]
        SensorImage = 12,
        /// <summary>
        /// 传感器电池电压数据
        /// </summary>
        [Description("传感器电池电压数据")]
        SensorPerf = 13,
        /// <summary>
        /// 测量位置温度数据
        /// </summary>
        [Description("设备温度数据")]
        MeasuresitePerf = 14,
        /// <summary>
        /// 振动特征值数据
        /// </summary>
        [Description("振动特征值数据")]
        VibValuePerf = 15,
        /// <summary>
        /// 波形数据
        /// </summary>
        [Description("波形数据")]
        VibWavePerf = 16,
        /// <summary>
        /// 测量位置报警配置数据
        /// </summary>
        [Description("测量位置报警配置数据")]
        MeasuresiteAlarmSet = 17,
        /// <summary>
        /// 振动信号报警配置数据
        /// </summary>
        [Description("振动信号报警配置数据")]
        Alarm = 18,
        /// <summary>
        /// 振动信号报警配置数据
        /// </summary>
        [Description("振动信号报警配置数据")]
        SignalAlarmSet = 19,
        /// <summary>
        /// 测量位置报警配置数据
        /// </summary>
        [Description("设备温度报警配置")]
        DeviceTemperatureAlarmSet = 20,
        /// <summary>
        /// 振动信号报警配置数据
        /// </summary>
        [Description("无线传感器温度报警配置")]
        WSTemperatureAlarmSet = 21,
        /// <summary>
        /// 振动信号报警配置数据
        /// </summary>
        [Description("电池电压报警配置")]
        BatteryVoltageAlarmSet = 22,

        #region  Added by QXM 2016/9/19

        [Description("传感器电池电压1")]
        VoltageWSMSiteData_1 = 23,

        [Description("传感器电池电压2")]
        VoltageWSMSiteData_2 = 24,

        [Description("传感器电池电压3")]
        VoltageWSMSiteData_3 = 25,

        [Description("传感器电池电压4")]
        VoltageWSMSiteData_4 = 26,

        [Description("测点温度1")]
        TempeDeviceMsitedata_1 = 27,

        [Description("测点温度2")]
        TempeDeviceMsitedata_2 = 28,

        [Description("测点温度3")]
        TempeDeviceMsitedata_3 = 29,

        [Description("测点温度4")]
        TempeDeviceMsitedata_4 = 30,

        [Description("加速度")]
        VibratingSingalCharHisAccForEngin = 31,

        [Description("速度")]
        VibratingSingalCharHisVelForEngin = 32,

        [Description("包络")]
        VibratingSingalCharHisEnvlForEngin = 33,

        [Description("位移")]
        VibratingSingalCharHisDispForEngin = 34,

        [Description("LQ")]
        VibratingSingalCharHisLQForEngin = 35,
        #endregion

        /// <summary>
        /// 传感器报警记录
        /// </summary>
        [Description("传感器报警记录")]
        WSAlarmRecord = 36,
        /// <summary>
        /// 设备报警记录
        /// </summary>
        [Description("设备报警记录")]
        DeviceArarmRecord = 37,

        [Description("加速度")]
        VibratingSingalCharHisAccForWave = 38,

        [Description("速度")]
        VibratingSingalCharHisVelForWave = 39,

        [Description("包络")]
        VibratingSingalCharHisEnvlForWave = 40,

        [Description("位移")]
        VibratingSingalCharHisDispForWave = 41,

        [Description("LQ")]
        VibratingSingalCharHisLQForWave = 42,

        #region 张辽阔 2016-12-14 添加

        /// <summary>
        /// 监测树
        /// </summary>
        [Description("监测树")]
        MonitorTree = 43,

        /// <summary>
        /// 加速度
        /// </summary>
        [Description("加速度")]
        VibratingSingalCharHisAcc = 44,

        /// <summary>
        /// 速度
        /// </summary>
        [Description("速度")]
        VibratingSingalCharHisVel = 45,

        /// <summary>
        /// 包络
        /// </summary>
        [Description("包络")]
        VibratingSingalCharHisEnvl = 46,

        /// <summary>
        /// 位移
        /// </summary>
        [Description("位移")]
        VibratingSingalCharHisDisp = 47,

        /// <summary>
        /// LQ
        /// </summary>
        [Description("设备状态")]
        VibratingSingalCharHisLQ = 48,

        /// <summary>
        /// 无线传感器温度采集数据1
        /// </summary>
        [Description("无线传感器温度采集数据1")]
        WSTempeMsitedata_1 = 49,

        /// <summary>
        /// 无线传感器温度采集数据2
        /// </summary>
        [Description("无线传感器温度采集数据2")]
        WSTempeMsitedata_2 = 50,

        /// <summary>
        /// 无线传感器温度采集数据3
        /// </summary>
        [Description("无线传感器温度采集数据3")]
        WSTempeMsitedata_3 = 51,

        /// <summary>
        /// 无线传感器温度采集数据4
        /// </summary>
        [Description("无线传感器温度采集数据4")]
        WSTempeMsitedata_4 = 52,

        /// <summary>
        /// 操作表
        /// </summary>
        [Description("操作表")]
        Operation = 53,

        #region 给前台页面专用的枚举

        /// <summary>
        /// 设备温度数据
        /// </summary>
        [Description("设备温度数据")]
        TempeDeviceMsitedata = 54,

        /// <summary>
        /// 无线传感器温度数据
        /// </summary>
        [Description("无线传感器温度数据")]
        WSTempeMsitedata = 55,

        /// <summary>
        /// 无线传感器电池电压数据
        /// </summary>
        [Description("无线传感器电池电压数据")]
        VoltageWSMSiteData = 56,

        #endregion

        #endregion

        /// <summary>
        /// 云平台数据周期同步
        /// ADDED BY QXM, ACCORDING TO VERSION 0.6 DOC
        /// </summary>
        [Description("报警状态")]
        AlarmStatus = 57,

        [Description("告警门限")]
        AlarmThreshold = 58
    }
    #endregion
}