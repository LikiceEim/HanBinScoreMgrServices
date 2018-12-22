/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Enum
 * 文件名：  AlarmRecordType
 * 创建人：  LF  
 * 创建时间：2016年7月26日10:19:22
 * 描述：告警记录类型枚举
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    #region 报警结果类型枚举
    /// <summary>
    /// 报警结果类型枚举
    /// </summary>
    public enum EnumAlarmRecordType
    {
        /// <summary>
        /// 振动
        /// </summary>
        DeviceVibration = 1,
        /// <summary>
        /// 设备温度
        /// </summary>
        DeviceTemperature = 2,
        /// <summary>
        /// 传感器温度
        /// </summary>
        WSTemperature = 3,
        /// <summary>
        /// 传感器电池电压
        /// </summary>
        WSVoltage = 4,
        /// <summary>
        /// 传感器连接
        /// </summary>
        WSLinked = 5,
        /// <summary>
        /// 网关连接
        /// </summary>
        WGLinked = 6,
        /// <summary>
        /// 停机
        /// </summary>
        DeviceShutDown = 7,
        /// <summary>
        /// 趋势报警
        /// </summary>
        TrendAlarm = 8,
    }
    #endregion
}