/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumWSLinkStatus
 *创建人：  王颖辉   
 *创建时间：2017年11月26日17:12:49
 *描述：无线传感器连接状态
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Enum
{
    /// <summary>
    /// 无线传感器连接状态
    /// </summary>
    public enum EnumWSLinkStatus
    {
        [Description("断开")]
        Disconnect = 0,
        [Description("连接")]
        Connect = 1
    }
}
