/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumConfig
 *创建人：  王颖辉  
 *创建时间：2016年09月06日
 *描述：通用配置枚
/************************************************************************************/
using System.ComponentModel;
namespace iCMS.Common.Component.Data.Enum
{
    #region 通用配置枚
    /// <summary>
    /// 通用配置枚
    /// </summary>
    public enum EnumConfig
    {  /// <summary>
       /// 形貌图配置
       /// </summary>
        [Description("形貌图配置")]
        TopographicMapConfig = 1,
        /// <summary>
        /// 形貌图显示配置
        /// </summary>
        [Description("形貌图显示配置")]
        TopographicMapShowConfig = 2,


        /// <summary>
        /// 设备报警相关设置
        /// </summary>
        [Description("设备报警相关设置")]
        AlarmConfig=3,

        /// <summary>
        /// 设备报警相关设置
        /// </summary>
        [Description("报警方式选择")]
        AlarmPatternConfig=4,
        
        /// <summary>
        /// 趋势报警开关
        /// </summary>
        [Description("趋势报警开关")]
        TrendAlarmConfig=5,

        /// <summary>
        /// 数据推送
        /// </summary>
        [Description("数据推送")]
        Cloud = 6,
        /// <summary>
        /// 历史数据显示相关
        /// </summary>
        [Description("历史数据显示相关")]
        HistoryDataShow = 7,
        
    }
    #endregion
}
