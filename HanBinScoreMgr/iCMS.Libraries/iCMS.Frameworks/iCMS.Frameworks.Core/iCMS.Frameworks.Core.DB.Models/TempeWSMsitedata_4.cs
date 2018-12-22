/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  张辽阔
 *创建时间：2016.07.21
 *描述：测量位置WS温度表实体
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 测量位置WS温度表4（以测量位置ID和采集时间创建索引）
    /// </summary>
    [Table("T_DATA_TEMPE_WS_MSITEDATA_4")]
    public class TempeWSMsitedata_4 : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int MsiteDataID { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        ///// <summary>
        ///// 测量位置实体集合
        ///// </summary>
        //public IList<T_SYS_MEASURESITE> MEASURESITE_List { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 采集数据
        /// </summary>
        public float MsDataValue { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        ///// <summary>
        ///// 传感器实体集合
        ///// </summary>
        //public IList<T_SYS_WS> WS_List { get; set; }

        /// <summary>
        /// 警告值
        /// </summary>
        public float? WarnValue { get; set; }

        /// <summary>
        /// 告警值
        /// </summary>
        public float? AlmValue { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int MonthDate { get; set; }

        /// <summary>
        /// 采集类型 1.有线、2.无线、3.三轴   add by lwj---2018.05.02
        /// </summary>
        public int SamplingDataType { get; set; }

        #endregion Model
    }
}