/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  CopyAllSpeedMSParameter 
 * 创建人：  张辽阔
 * 创建时间：2018-05-08
 * 描述：复制转速测量位置参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-08
    /// 创建记录：复制转速测量位置参数
    /// </summary>
    public class CopyAllSpeedMSParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：设备ID
        /// </summary>
        public int TargetDevId { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：源ms与新WS与挂靠的测点的匹配字符串（1#4#2，4##2）
        /// </summary>
        public string MSAndWSStr { get; set; }
    }
}