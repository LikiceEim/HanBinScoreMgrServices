/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud
 *文件名：  GetCloudLogDataParameter
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：获取云推送日志表的参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：获取云推送日志表的参数
    /// </summary>
    public class GetCloudLogDataParameter : BaseRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BDate
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EDate
        {
            get;
            set;
        }

        /// <summary>
        /// 推送平台Id -1：表示全部平台
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型-1：表示全部数据类型1、波形，2、其他数据
        /// </summary>
        public int DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 推送状态-1:全部0：成功1：失败
        /// </summary>
        public int PushStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 页数，从1开始,若为-1返回所有
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页面行数，从1开始
        /// </summary>
        public int PageSize { get; set; }
    }
}