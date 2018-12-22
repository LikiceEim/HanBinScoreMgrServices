/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Statistics
 *文件名：  DevRunningStatusDataByMTIDParameter
 *创建人：  王颖辉
 *创建时间：2016-10-19
 *描述：GetDevRunningStatusDataByMTIDParameter:调用方法
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    #region DevRunningStatusDataByMTIDParameter
    /// <summary>
    /// GetDevRunningStatusDataByMTIDParameter:调用方法
    /// </summary>
    public class DevRunningStatusDataByMTIDParameter : BaseRequest
    {
        /// <summary>
        /// 监测树id
        /// </summary>
        public int MonitorTreeId
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
        /// 排序顺序
        /// </summary>
        public string Order
        {
            get;
            set;
        }

        /// <summary>
        /// 页面索引
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// 分页数量
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
    }
    #endregion
}
