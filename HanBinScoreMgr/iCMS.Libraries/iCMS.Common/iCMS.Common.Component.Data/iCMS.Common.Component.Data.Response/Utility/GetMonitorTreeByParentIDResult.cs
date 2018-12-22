/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Utility
 *文件名：  GetMonitorTreeByParentIDResult
 *创建人：  王颖辉
 *创建时间：2016-11-08
 *描述：获取监测树子节点，通过父子点
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Utility
{
    /// <summary>
    /// 获取监测树子节点，通过父子点
    /// </summary>
    public class GetMonitorTreeByParentIDResult
    {
        /// <summary>
        /// 监测树列表
        /// </summary>
        public List<MonitorTreeInfoForMonitorTree> MonitorTree
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 监测树
    /// </summary>
    public class MonitorTreeInfoForMonitorTree
    {
        /// <summary>
        /// 监测树Id
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 名称 
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
