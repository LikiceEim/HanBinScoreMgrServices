/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  AlarmStatus
 *创建人：  LF  
 *创建时间：2016年7月23日15:02:43
 *描述：告警状态枚举
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    #region 报警状态枚举
    /// <summary>
    /// 报警状态枚举
    /// </summary>
    [Description("工厂，设备，车间，测量位置，振动信号,测量位置数据，报警记录，振动实时数据报警状态")]
    public enum EnumAlarmStatus
    {
        /// <summary>
        /// 未采集
        /// </summary>
        //[Description("<div style='background-color:#afafaf;'>&nbsp;未采集</div>")]
        [Description("未采集")]
        Unused = 0,
        /// <summary>
        /// 正常
        /// </summary>
        //[Description("<div style='background-color:#1dab05;'>&nbsp;正常</div>")]
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 高报
        /// </summary>
        //[Description("<div style='background-color:#cec31a;'>&nbsp;高报</div>")]
        [Description("高报")]
        Warning = 2,
        /// <summary>
        /// 高高报
        /// </summary>
        //[Description("<div style='background-color:#b51212;'>&nbsp;高高报</div>")]
        [Description("高高报")]
        Danger = 3
    }
    #endregion
}
