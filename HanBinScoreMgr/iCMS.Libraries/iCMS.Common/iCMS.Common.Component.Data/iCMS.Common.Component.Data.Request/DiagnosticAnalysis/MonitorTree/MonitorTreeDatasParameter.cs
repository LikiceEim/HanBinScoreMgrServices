/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree
 *文件名：  MonitorTreeDatasParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：GetMonitorTreeDatasParameter:调用方法
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree
{
    /// <summary>
    /// GetMonitorTreeDatasParameter:调用方法
    /// </summary>
    public class MonitorTreeDatasParameter: BaseRequest
    {
        public int Pid
        {
            get;
            set;
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 获取监测树
    /// </summary>
    public class GetMonitorTreeDataForNavigationParameter : BaseRequest
    {
        /// <summary>
        /// 创建时间:2017-07-20
        /// 创建人：王颖辉
        /// 创建内容:用户ID
        /// </summary>
        public int? UserID
        {
            get;
            set;
        }
    }
}
