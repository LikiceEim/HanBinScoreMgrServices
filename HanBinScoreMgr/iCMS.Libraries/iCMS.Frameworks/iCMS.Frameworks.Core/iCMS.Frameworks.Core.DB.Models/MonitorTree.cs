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
    /// 监测树表
    /// </summary>
    [Table("T_SYS_MONITOR_TREE")]
    public class MonitorTree : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 源ID
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// 是否系统默认 0=false 1=true
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 树类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 对应图片ID
        /// </summary>
        public int? ImageID { get; set; }

        /// <summary>
        /// 属性ID
        /// </summary>
        public int? MonitorTreePropertyID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        #endregion Model
    }
}