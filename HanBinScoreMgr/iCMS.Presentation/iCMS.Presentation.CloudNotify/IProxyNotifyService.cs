/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.CloudNotify
 *文件名：  IProxyNotifyService
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：云代理通知服务接口
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Request.CloudProxy;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Presentation.CloudProxy
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IProxyNotifyService”。
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：云代理通知服务接口
    /// </summary>
    [ServiceContract]
    public interface IProxyNotifyService
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-08
        /// 创建记录：接收触发源的请求和给云通讯发送数据到达通知
        /// </summary>
        /// <param name="parameter">接收触发源的请求和给云通讯发送数据到达通知的参数</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void ReceiveTriggerAndRequestCloudCommunication(ReceiveTriggerAndRequestCloudCommunicationParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-08
        /// 创建记录：云通讯请求该接口获知是否有数据
        /// </summary>
        /// <param name="parameter">云通讯请求该接口获知是否有数据的参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> ReceiveCloudCommunicationNotify(ReceiveCloudCommunicationNotifyParameter parameter);
    }
}