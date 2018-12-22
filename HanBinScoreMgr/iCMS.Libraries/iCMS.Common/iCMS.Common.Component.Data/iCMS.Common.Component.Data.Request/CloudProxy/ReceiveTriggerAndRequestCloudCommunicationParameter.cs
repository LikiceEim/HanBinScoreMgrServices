/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.CloudNotify.Request
 *文件名：  ReceiveTriggerAndRequestCloudCommunicationParameter
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：接收触发源的请求和给云通讯发送数据到达通知的参数
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
    /// 创建时间：2016-12-08
    /// 创建记录：接收触发源的请求和给云通讯发送数据到达通知的参数
    /// </summary>
    public class ReceiveTriggerAndRequestCloudCommunicationParameter : BaseRequest
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int PlatformId { get; set; }
    }
}