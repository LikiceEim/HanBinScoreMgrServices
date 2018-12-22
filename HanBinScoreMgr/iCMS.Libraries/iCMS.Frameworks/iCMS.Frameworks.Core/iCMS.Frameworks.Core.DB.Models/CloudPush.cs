/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.DB.Models
 *文件名：  CloudPush
 *创建人：  张辽阔
 *创建时间：2016-12-06
 *描述：云推送推送数据表
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-06
    /// 创建记录：云推送推送数据表
    /// </summary>
    [Table("T_DATA_CLOUDPUSH")]
    public class CloudPush : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 来源表表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 来源表主键Id
        /// </summary>
        public int TableNameId { get; set; }

        /// <summary>
        /// 操作状态
        /// 1：新增，2：修改，3：删除,4：同步
        /// </summary>
        public EnumCloudPushOperationStatus OperationStatus { get; set; }

        /// <summary>
        /// 推送平台Id
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 数据状态
        /// 1、正常，2、正在处理中，3、失败
        /// </summary>
        public EnumCloudPushDataStatus DataStatus { get; set; }

        /// <summary>
        /// 优先级
        /// 1、优先，2、中等，3、普通
        /// </summary>
        public EnumCloudPushPriority Priority { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string ExtraMessage { get; set; }
    }
}