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
    /// 测量位置温度告警设置表
    /// </summary>
    [Table("T_SYS_TEMPE_DEVICE_SET_MSITEALM")]
    public class TempeDeviceSetMSiteAlm : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int MsiteAlmID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：网关ID
        /// </summary>
        public int? WGID { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        /// <summary>
        /// 警告值
        /// </summary>
        public float WarnValue { get; set; }

        /// <summary>
        /// 告警值
        /// </summary>
        public float AlmValue { get; set; }

        /// <summary>
        /// 告警状态
        /// </summary>
        public int Status { get; set; }

        #endregion Model
    }
}