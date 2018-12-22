/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  EigenvalueTypeBySignalParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：根据振动信号类型获取对应的特征值类型
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 根据振动信号类型获取对应的特征值类型
    /// <summary>
    /// 根据振动信号类型获取对应的特征值类型
    /// </summary>
    public class EigenvalueTypeBySignalParameter: BaseRequest
    {
        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int SignalType { get; set; }
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }
    }
    #endregion
}
