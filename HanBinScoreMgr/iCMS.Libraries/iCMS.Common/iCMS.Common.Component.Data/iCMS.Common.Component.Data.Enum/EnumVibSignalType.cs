/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  VibratingSignalType
 *创建人：  LF  
 *创建时间：2016年7月23日11:44:41
 *描述：振动类型数据枚举
/************************************************************************************/

using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 振动类型数据枚举
    /// <summary>
    /// 振动类型数据枚举
    /// </summary>
    [Description("振动类型数据枚举")]
    public enum EnumVibSignalType
    {
        /// <summary>
        ///  加速度
        /// </summary>
        [Description("加速度")]
        Accelerated = 1,
        /// <summary>
        ///  速度
        /// </summary>
        [Description("速度")]
        Velocity = 2,
        /// <summary>
        /// 位移
        /// </summary>
        [Description("位移")]
        Displacement = 3,
        /// <summary>
        /// 包络
        /// </summary>
        [Description("包络")]
        Envelope = 4,
        /// <summary>
        /// LQ
        /// </summary>
        [Description("设备状态")]
        LQ = 5
    }
    #endregion
}