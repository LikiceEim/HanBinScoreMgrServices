/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  StatisticCloudLogDataParameter
 *创建人：  王颖辉
 *创建时间：2016-12-012
 *描述：统计日志数据
/************************************************************************************/
using System;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 统计日志数据
    /// <summary>
    /// 统计日志数据
    /// </summary>
    public class StatisticCloudLogDataParameter: BaseRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BDate
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EDate
        {
            get;
            set;
        }

        /// <summary>
        /// 平台ID -1：表示全部平台
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }

        /// <summary>
        /// 统计类型 1日，2月，3年
        /// </summary>
        public int StatisticType
        {
            get;
            set;
        }
    }
    #endregion
}
