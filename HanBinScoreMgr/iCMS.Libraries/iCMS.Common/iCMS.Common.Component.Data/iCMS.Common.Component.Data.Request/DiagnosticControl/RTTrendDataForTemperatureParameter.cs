/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  RTTrendDataForTemperatureParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：判断温度是否存在最新实时数据
/************************************************************************************/

using System;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 判断温度是否存在最新实时数据
    /// <summary>
    /// 判断温度是否存在最新实时数据
    /// </summary>
    public class RTTrendDataForTemperatureParameter : BaseRequest
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
        /// 测量位置ID
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }


        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType
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