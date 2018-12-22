/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  张辽阔
 *创建时间：2016.07.21
 *描述：振动信号的特征值实时数据表实体
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 振动信号的特征值实时数据表
    /// </summary>
    [Table("T_DATA_VIBSINGAL_RT")]
    public class VibSingalRT : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int RTDataID { get; set; }

        /// <summary>
        /// 1或空为定时；2为临时。
        /// </summary>
        public int? DAQStyle { get; set; }

        /// <summary>
        /// 信号ID
        /// </summary>
        public int SingalID { get; set; }

        ///// <summary>
        ///// 振动信号信息实体集合
        ///// </summary>
        //public IList<T_SYS_VIBSINGAL> MEASURESITE_List { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSID { get; set; }

        ///// <summary>
        ///// 测量位置实体集合
        ///// </summary>
        //public IList<T_SYS_MEASURESITE> MEASURESITE_List { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        ///// <summary>
        ///// 设备信息实体集合
        ///// </summary>
        //public IList<T_SYS_DEVICE> DEVICE_List { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 转速
        /// </summary>
        public string Rotate { get; set; }

        /// <summary>
        /// 波形数据路径
        /// </summary>
        public string WaveDataPath { get; set; }

        /// <summary>
        /// 压缩因子
        /// </summary>
        public float TransformCofe { get; set; }

        /// <summary>
        /// 实际采样频率
        /// </summary>
        public float RealSamplingFrequency { get; set; }

        /// <summary>
        /// 波长
        /// </summary>
        public int SamplingPointData { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? E6 { get; set; }

        #endregion Model
    }
}