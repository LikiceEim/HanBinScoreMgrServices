/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.DB.Models
 *文件名：  CloudConfig
 *创建人：  张辽阔
 *创建时间：2016-12-06
 *描述：云推送配置表
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-06
    /// 创建记录：云推送配置表
    /// </summary>
    [Table("T_SYS_CLOUDCONFIG")]
    public class CloudConfig : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 节点类型
        /// 1、基础数据，2、云平台，3、配置数据，4、日志是否记录，5、数据推送量；
        /// </summary>
        public EnumCloudConfigType Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否启用
        /// 0：未启用，1：启用
        /// </summary>
        public EnumCloudConfigIsUse IsUse { get; set; }
    }
}