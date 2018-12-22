/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  BatteryVolatageTendencysDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：测量位置对应的传感器的电池电压历史数据的检索
/************************************************************************************/
using System;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 测量位置对应的传感器的电池电压历史数据的检索
    /// <summary>
    /// 测量位置对应的传感器的电池电压历史数据的检索
    /// </summary>
    public class BatteryVolatageTendencysDataParameter: BaseRequest
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevID
        {
            set;
            get;
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int MSiteID
        {
            set;
            get;
        }


        /// <summary>
        /// 设备编号
        /// </summary>
        public DateTime BeginDate
        {
            set;
            get;
        }


        /// <summary>
        /// 设备编号
        /// </summary>
        public DateTime EndDate
        {
            set;
            get;
        }


        /// <summary>
        /// 设备编号
        /// </summary>
        public string ChartID
        {
            set;
            get;
        }
    }
    #endregion

}
