/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.CloudNotify.Common
 *文件名：  PlatformCacheData
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：保存多个平台的状态
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace iCMS.Presentation.CloudNotify.Common
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：保存多个平台的状态
    /// </summary>
    public static class PlatformCacheData
    {
        /// <summary>
        /// 保存多个平台的状态
        /// </summary>
        public static Dictionary<int, PlatformDataStatus> MuiltPlatformCacheData = new Dictionary<int, PlatformDataStatus>();
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：保存单个平台的状态
    /// </summary>
    public class PlatformDataStatus
    {
        public PlatformDataStatus()
        {
            PlatformTimer = new Timer();
            DataCount = 1;
        }

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer PlatformTimer { get; private set; }

        /// <summary>
        /// 数据需要处理的数据总数
        /// </summary>
        public int DataCount { get; set; }
    }
}