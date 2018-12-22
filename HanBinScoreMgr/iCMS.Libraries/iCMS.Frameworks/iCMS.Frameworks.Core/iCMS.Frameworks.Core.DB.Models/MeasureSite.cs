/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：
 * 文件名：  
 * 创建人：  QXM
 * 创建时间：2016/7/21 10:10:13
 * 描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 记录测量位置信息表
    /// </summary>
    [Table("T_SYS_MEASURESITE")]
    public class MeasureSite : EntityBase
    {
        #region Model

        /// <summary>
        /// 测量位置ID
        /// </summary>
        [Key]
        public int MSiteID { get; set; }

        /// <summary>
        /// 测量位置
        /// </summary>
        public int MSiteName { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int? WSID { get; set; }

        /// <summary>
        /// 测量位置类型
        /// </summary>
        public int MeasureSiteType { get; set; }

        /// <summary>
        /// 传感器灵敏度系数A
        /// </summary>
        public float SensorCosA { get; set; }

        /// <summary>
        /// 传感器灵敏度系数B
        /// </summary>
        public float SensorCosB { get; set; }

        /// <summary>
        /// 测量位置状态
        /// </summary>
        public int MSiteStatus { get; set; }

        /// <summary>
        /// 测量位置状态更新时间
        /// </summary>
        public DateTime MSiteSDate { get; set; }

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
        /// 轴承ID
        /// </summary>
        public int? BearingID { get; set; }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType { get; set; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingModel { get; set; }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm { get; set; }

        /// <summary>
        /// 关系测点ID（该字段来表示转速挂靠的测点ID）add by lwj---2018.05.02
        /// </summary>
        public int? RelationMSiteID { get; set; }

        /// <summary> 
        /// 传感器灵敏度系数 add by lwj---2018.05.02
        /// </summary>
        public float? SensorCoefficient { get; set; }

        #endregion Model
    }
}