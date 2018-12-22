/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 无线网络报警记录表
    /// </summary>
    [Table("T_SYS_WSN_ALMRECORD")]
    public class WSnAlmRecord : EntityBase
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
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName { get; set; }

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

        #region Navigation Properties

        //public virtual IList<T_SYS_DEVICE> Devices { get; set; }

        //public virtual IList<T_SYS_MEASURESITE> MeasureSites { get; set; }

        #endregion
    }
}