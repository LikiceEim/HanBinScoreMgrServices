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
    /// 监测树属性表
    /// </summary>
    [Table("T_SYS_MONITOR_TREE_PROPERTY")]
    public class MonitorTreeProperty : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int MonitorTreePropertyID { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string TelphoneNO { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        public string FaxNO { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float? Longtitude { get; set; }

        /// <summary>
        /// 子节点个数
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public float? Length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public float? Width { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public float? Area { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #endregion Model
    }
}