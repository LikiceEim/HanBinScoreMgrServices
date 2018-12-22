/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  RTTrendDataForVibsignalResult
 *创建人：  王颖辉
 *创建时间：2016-08-01
 *描述：判断是否存在最新实时数据返回值
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 判断是否存在最新实时数据返回值
    public class RTTrendDataForVibsignalResult
    {
        /// <summary>
        /// 是否存在最新数据
        /// </summary>
        public bool HasNewData
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
