/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：NotifyDestroyDAUConnectionParameter
 * 创建人：张辽阔
 * 创建时间：2018-03-02
 * 描述：通知销毁采集单元的连接参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-03-02
    /// 创建记录：通知销毁采集单元的连接参数
    /// </summary>
    public class NotifyDestroyDAUConnectionParameter : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-03-02
        /// 创建记录：采集单元ID数据集合
        /// </summary>
        public List<int> DAUIDList { get; set; }
    }
}