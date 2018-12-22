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
    /// 波形上限频率值设置表
    /// </summary>
    [Table("T_DICT_WAVE_UPPERLIMIT_VALUE")]
    public class WaveUpperLimitValues : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 外键振动信号类型ID
        /// </summary>
        public int VibratingSignalTypeID { get; set; }

        /// <summary>
        /// 波形上限频率值
        /// </summary>
        public int WaveUpperLimitValue { get; set; }

        /// <summary>
        /// 描述信息
        /// WireLessSensor_UpLimit来表示无线传感器的上限频率，
        /// WiredSensor_UpLimit来表示有线传感器的上限频率，
        /// WiredSensor_EnvlFilterUpLimit来表示有线传感器的包络滤波器上限频率，
        /// Triaxial_UpLimit来表示三轴传感器的上限频率
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 0不可用，1可用
        /// </summary>
        public int IsUsable { get; set; }

        /// <summary>
        /// 0系统初始状态，1其它状态
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? OrderNo { get; set; }

        #endregion Model
    }
}