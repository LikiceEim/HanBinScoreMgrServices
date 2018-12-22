/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  MonitorReslut
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：监测树返回结果
/************************************************************************************/


using iCMS.Frameworks.Core.DB.Models;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 监测树返回结果
    /// <summary>
    /// 监测树返回结果
    /// </summary>
    public class MonitorTreeResult
    {
        /// <summary>
        /// 返回集合
        /// </summary>
        public List<MonitorTree> MonitorTreeList { get; set; }

        public MonitorTreeResult()
        {
            MonitorTreeList = new List<MonitorTree>();
        }
    }
    #endregion
}
