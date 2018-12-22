/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  VibSignalTypeForDeviceTreeResult
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：返回振动信号报警配置数据信息的参数
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回振动信号报警配置数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回振动信号报警配置数据信息的参数
    /// </summary>
    public class SignalAlmDataForDeviceTreeResult
    {
        /// <summary>
        /// 振动信号报警配置信息集合
        /// </summary>
        public List<VibSignalInfomation> VibSignalInfomation { set; get; }
    }
    #endregion

    #region 振动信号报警配置信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：振动信号报警配置信息实体
    /// </summary>
    public class VibSignalInfomation
    {
        /// <summary>
        /// ID
        /// </summary>
        public int WID { get; set; }

        /// <summary>
        /// 外键，振动信号ID
        /// </summary>
        public int SignalID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int SignalAlmID { get; set; }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int ValueTypeId { get; set; }

        /// <summary>
        /// 特征值类型名称
        /// </summary>
        public string ValueTypeName { get; set; }

        /// <summary>
        /// 警告值
        /// </summary>
        public float WarnValue { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        public float AlmValue { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 上传触发值
        /// </summary>
        public float? UploadTrigger { get; set; }

        /// <summary>
        /// 趋势报警预值
        /// </summary>
        public float? ThrendAlarmPrvalue { get; set; }
    }
    #endregion

}