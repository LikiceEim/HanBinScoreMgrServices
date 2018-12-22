/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool 
 *文件名：  SocketHelper 
 *创建人：  王颖辉
 *创建时间：2017/11/21 10:47:12
 *描述：Socket服务类
 *
 *修改人：
 *修改时间：
 *描述：
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    #region Socket服务类
    /// <summary>
    /// Socket服务类
    /// </summary>
    public class SocketHelper
    {
        #region 端口是否使用
        /// <summary>
        /// 端口是否使用
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public static bool IsUsePort(int port)
        {
            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
        #endregion
    }
    #endregion
}
