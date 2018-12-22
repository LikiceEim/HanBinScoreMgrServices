/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Common
 *文件名：  ServiceDependencyRegistrar
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：Service依赖注入
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Tool;
using iCMS;

namespace iCMS.Service.Common
{
    #region Service依赖注入
    /// <summary>
    /// Service依赖注入
    /// </summary>
    public class ServiceDependencyRegistrar
    {
        // private static ServiceDependencyRegistrar m_instance = null;
        private static readonly object m_s_lock = new object();

        public ServiceDependencyRegistrar()
        {
            //ServiceLocator.RegisterService<IMonitorTreeService, MonitorTreeService>();
        }
    }
    #endregion
}
