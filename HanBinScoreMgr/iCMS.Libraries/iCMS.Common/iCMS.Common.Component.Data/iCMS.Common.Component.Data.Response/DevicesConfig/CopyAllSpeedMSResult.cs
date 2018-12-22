/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  CopyAllSpeedMSResult 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：复制转速测量位置返回参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-08
    /// 创建记录：复制转速测量位置返回参数
    /// </summary>
    public class CopyAllSpeedMSResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：新增加的测量位置ID，以列表的形式返回
        /// </summary>
        public List<int> MSIDList { get; set; }
    }
}