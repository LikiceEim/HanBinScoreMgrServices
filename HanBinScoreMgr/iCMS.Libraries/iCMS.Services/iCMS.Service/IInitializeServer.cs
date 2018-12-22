/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Web
 *文件名：  IInitializeServer
 *创建人：  王颖辉
 *创建时间：2016-11-24
 *描述：初始化数据
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Service.Web
{
    #region 初始化数据
    /// <summary>
    /// 初始化数据
    /// </summary>
    public interface IInitializeServer:IDisposable
    {
        #region 初始化系统变量
        /// <summary>
        /// 初始化系统变量
        /// </summary>
        void InitializeEnvironmentVariables();
        #endregion
    }
    #endregion
}
