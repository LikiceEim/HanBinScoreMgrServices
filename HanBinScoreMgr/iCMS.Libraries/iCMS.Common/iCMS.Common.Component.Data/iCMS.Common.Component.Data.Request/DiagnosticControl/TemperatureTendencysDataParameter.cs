/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  TemperatureTendencysDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：测量位置的温度历史数据检索
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 测量位置的温度历史数据检索
    /// <summary>
    /// 测量位置的温度历史数据检索
    /// </summary>
    public class TemperatureTendencysDataParameter: BaseRequest
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
        /// 起始时间
        /// </summary>
        public DateTime BeginDate
        {
            get;
            set;
        }


        /// <summary>
        /// 截止时间
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


        /// <summary>
        /// 1：设备温度；2：传感器温度
        /// </summary>
        public int Type
        {
            get;
            set;
        }
    }
    #endregion

}
