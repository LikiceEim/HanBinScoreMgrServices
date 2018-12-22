/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.DB.Models
 *文件名：  CloudLog
 *创建人：  张辽阔
 *创建时间：2016-12-06
 *描述：云推送日志表
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
    /// 创建记录：云推送日志表
    /// </summary>
    [Table("T_DATA_CLOUDLOG")]
    public class CloudLog : EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 平台Id
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 推送数据
        /// 波形记录路径，别的数据记录云平台推送的数据
        /// </summary>
        public string PushData { get; set; }

        /// <summary>
        /// 推送结果
        /// 带推送失败时填写失败信息，如DataError；推送成功时为空；
        /// </summary>
        public string PushResult { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushTime { get; set; }

        /// <summary>
        /// 推送状态
        /// 0、成功，1、失败
        /// </summary>
        public EnumCloudLogPushStatus PushStatus { get; set; }

        /// <summary>
        /// 数据类型
        /// 1、波形，2、其他数据
        /// </summary>
        public EnumCloudLogDataType DataType { get; set; }
    }
}