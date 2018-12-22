/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud
 *文件名：  AddCloudLogDataParameter
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：添加云推送日志表的参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：添加云推送日志表的参数
    /// </summary>
    public class AddCloudLogDataParameter : BaseRequest
    {
        public AddCloudLogDataParameter()
        {
            PushTime = DateTime.Now;
        }

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
        public string PushResult { get; set; }   //

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushTime { get; set; } //

        /// <summary>
        /// 推送状态
        /// </summary>
        public EnumCloudLogPushStatus PushStatus { get; set; } //

        /// <summary>
        /// 数据类型
        /// </summary>
        public EnumCloudLogDataType DataType { get; set; }
    }
}