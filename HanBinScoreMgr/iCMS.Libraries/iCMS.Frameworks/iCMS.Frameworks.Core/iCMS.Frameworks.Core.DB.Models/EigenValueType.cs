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
    /// 特征值类型表
    /// </summary>
    [Table("T_DICT_EIGEN_VALUE_TYPE")]
    public class EigenValueType : EntityBase
    {
        #region Model

        /// <summary>
        /// 特征值类型ID
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
        /// 特征值类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述信息
        /// WireLessSensor_EigenValue来表示无线传感器的特征值，
        /// WiredSensor_EigenValue来表示有线传感器的特征值，
        /// Triaxial_EigenValue来表示三轴传感器的特征值
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