/***********************************************************************
 * Copyright (c) 2017@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  IProxyNotifyService
 *创建人：  王颖辉
 *创建时间：2017-09-18
 *描述：自定义异常处理
 ************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    /// <summary> 
    /// 自定义异常处理 
    /// </summary> 
    public class CustomExceptionHandler : IErrorHandler
    {
        #region IErrorHandler Members

        /// <summary> 
        /// HandleError 
        /// </summary> 
        /// <param name="ex">ex</param> 
        /// <returns>true</returns> 
        public bool HandleError(Exception ex)
        {
            return true;
        }

        /// <summary> 
        /// ProvideFault 
        /// </summary> 
        /// <param name="ex">ex</param> 
        /// <param name="version">version</param> 
        /// <param name="msg">msg</param> 
        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            // 
            //在这里处理服务端的消息，将消息写入服务端的日志 
            // 
            //string err = string.Format("调用WCF接口 '{0}' 出错", ex.TargetSite.Name + "，详情：\r\n" + ex.Message);
            //var newEx = new FaultException(err);

            //MessageFault msgFault = newEx.CreateMessageFault();
            //msg = Message.CreateMessage(version, msgFault, newEx.Action);
            LogHelper.WriteLog(ex);
        }

        #endregion
    }




    /// <summary> 
    /// WCF服务类的特性 
    /// </summary> 
    public class CustomExceptionBehaviourAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        public CustomExceptionBehaviourAttribute(Type errorHandlerType)
        {
            _errorHandlerType = errorHandlerType;
        }

        #region IServiceBehavior Members

        public void Validate(ServiceDescription description,
        ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription description,
        ServiceHostBase serviceHostBase,
        Collection<ServiceEndpoint> endpoints,
        BindingParameterCollection parameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription description,
        ServiceHostBase serviceHostBase)
        {
            var handler =
            (IErrorHandler)Activator.CreateInstance(_errorHandlerType);

            foreach (ChannelDispatcherBase dispatcherBase in
            serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(handler);
            }
        }

        #endregion
    }
}
