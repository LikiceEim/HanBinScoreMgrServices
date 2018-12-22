/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.CloudNotify.Request
 *文件名：  ReceiveCloudCommunicationNotifyParameter
 *创建人：  张辽阔
 *创建时间：2016-12-12
 *描述：云通讯请求该接口获知是否有数据的参数
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.CloudProxy
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-12
    /// 创建记录：云通讯请求该接口获知是否有数据的参数
    /// </summary>
    public class ReceiveCloudCommunicationNotifyParameter : BaseRequest
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 云通讯本次处理的数据量
        /// </summary>
        public int ProcessCount { get; set; }
    }
}