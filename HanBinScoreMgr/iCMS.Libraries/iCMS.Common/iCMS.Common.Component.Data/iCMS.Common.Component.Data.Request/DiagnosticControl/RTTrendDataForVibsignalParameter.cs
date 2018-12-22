/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  RTTrendDataForVibsignalParameter
 *创建人：  王颖辉
 *创建时间：2016-10-21
 *描述：判断振动信号是否存在最新实时数据
/************************************************************************************/

using System;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 判断振动信号是否存在最新实时数据
    /// <summary>
    /// 判断振动信号是否存在最新实时数据
    /// </summary>
    public class RTTrendDataForVibsignalParameter : BaseRequest
    {
        /// <summary>
        /// 客户最后一条数据对应的时间
        /// </summary>
        public DateTime CheckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 测点ID
        /// </summary>
        public int MSiteID
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
        /// <summary>
        /// 特征值类型
        /// </summary>
        public int EigenvalueType
        {
            get;
            set;
        }
        /// <summary>
        /// 振动信号类型
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-15
        /// 修改记录：拼写错误
        /// </summary>
        public int SignalType
        {
            get;
            set;
        }
    }
    #endregion
}