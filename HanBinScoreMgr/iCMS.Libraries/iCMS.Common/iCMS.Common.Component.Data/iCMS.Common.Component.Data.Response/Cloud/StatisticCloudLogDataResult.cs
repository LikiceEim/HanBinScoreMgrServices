/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  StatisticCloudLogDataResult
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：统计云通讯日志返回结果
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 统计云通讯日志返回结果
    /// <summary>
    /// 统计云通讯日志返回结果
    /// </summary>
    public class StatisticCloudLogDataResult
    {
        /// <summary>
        /// 信息集合
        /// </summary>
        public List<CloudStatusInfo> CloudStatusInfo
        {
            get;
            set;
        }
    }
    #endregion

    #region 信息集合
    /// <summary>
    /// 信息集合
    /// </summary>
    public class CloudStatusInfo
    {
        /// <summary>
        /// 推送总量
        /// </summary>
        public List<int> PushNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 推送成功数
        /// </summary>
        public List<int> PushSuccessNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 推送失败数
        /// </summary>
        public List<int> PushFailureNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 可维护推送失败数（时间段不限制）
        /// </summary>
        public int PushMaintainFailNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 根据传入类型
        /// </summary>
        public List<string> PushTime
        {
            get;
            set;
        }

        /// <summary>
        /// 平台ID
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName
        {
            get;
            set;
        }
    }
    #endregion

    #region 统计日志数据
    /// <summary>
    /// 统计日志数据
    /// </summary>
    public class StatisticCloudLogData
    {
        /// <summary>
        /// 查询时间
        /// </summary>
        public string QueryTime
        {
            get;
            set;
        }

        /// <summary>
        /// 平台Id
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }

        /// <summary>
        /// 成功数量
        /// </summary>
        public int Success
        {
            get;
            set;
        }

        /// <summary>
        /// 失败数量
        /// </summary>
        public int Fail
        {
            get;
            set;
        }

        /// <summary>
        /// 数据数量
        /// </summary>
        public int DataCount
        {
            get;
            set;
        }
    }
    #endregion

    #region 统计推送错误数据
    /// <summary>
    /// 统计推送错误数据
    /// </summary>
    public class StatisticPushDataErrorData
    {
        /// <summary>
        /// 错误数据量
        /// </summary>
        public int ErrorCount
        {
            get;
            set;
        }
    }
    #endregion
}
