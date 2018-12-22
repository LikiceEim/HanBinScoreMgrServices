/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 设备的报警记录表
    /// </summary>
    [Table("T_SYS_DEV_ALMRECORD")]
    public class DevAlmRecord : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int AlmRecordID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DevNO { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName { get; set; }

        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int SingalID { get; set; }

        /// <summary>
        /// 振动信号名称
        /// </summary>
        public string SingalName { get; set; }

        /// <summary>
        /// 振动特征值阈值设置ID
        /// </summary>
        public int SingalAlmID { get; set; }

        /// <summary>
        /// 振动特征值类型
        /// </summary>
        public string SingalValue { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public string MonitorTreeID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int MSAlmID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 采集值
        /// </summary>
        public float? SamplingValue { get; set; }

        /// <summary>
        /// 高报阈值
        /// </summary>
        public float? WarningValue { get; set; }

        /// <summary>
        /// 高高报阈值
        /// </summary>
        public float? DangerValue { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EDate { get; set; }

        /// <summary>
        /// 最近发生时间
        /// </summary>
        public DateTime LatestStartTime { get; set; }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 采集类型 1.有线、2.无线、3.三轴   add by lwj---2018.05.02
        /// </summary>
        public int SamplingDataType { get; set; }

        #endregion Model
    }
}