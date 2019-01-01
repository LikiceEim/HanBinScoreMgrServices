/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumMeasureSiteThresholdType
 *创建人：  王颖辉 
 *创建时间：2017年11月08日
 *描述：测量位置阈值类型
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 测量位置阈值类型
    /// </summary>
    public enum EnumMeasureSiteThresholdType
    {
        [Description("加速度")]
        ACC = 1,
        [Description("速度")]
        VEL = 2,
        [Description("位移")]
        DISP = 3,
        [Description("包络")]
        ENVEL = 4,
        [Description("设备状态")]
        LQ = 5,
        [Description("设备温度")]
        DeviceTemperature = 6,
        [Description("WS温度")]
        WSTemperature = 7,
        [Description("WS电池电压")]
        WSVoltage = 8,
    }
}
