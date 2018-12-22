/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  TendencyDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：获取趋势图数据
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 获取趋势图数据
    /// <summary>
    /// 获取趋势图数据
    /// </summary>
    public class TendencyDataParameter: BaseRequest
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备位置编号
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int SignalType
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int EigenvalueType
        {
            get;
            set;
        }

        /// <summary>
        /// 趋势数据起始时间
        /// </summary>
        public DateTime BeginDate
        {
            get;
            set;
        }

        /// <summary>
        /// 趋势数据起始时间
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID
        {
            get;
            set;
        }
    }
    #endregion
}
