/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessDevicesConfig
 *文件名：  AgentAccessParameter
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：验证Agent是否可以访问的参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessDevicesConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：验证Agent是否可以访问的参数
    /// </summary>
    public class AgentAccessParameter : BaseRequest
    {
        /// <summary>
        /// Agent地址
        /// </summary>
        public string agentUrlAddress { get; set; }
    }
}