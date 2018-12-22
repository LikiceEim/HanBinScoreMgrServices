/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  WSInfoResult
 *创建人：  王颖辉
 *创建时间：2016-08-01
 *描述：传感器电池电压返回值
 *
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    #region 获取无线传感器数据返回值
    /// <summary>
    /// 获取无线传感器数据
    /// </summary>
    public class WSInfoResult
    {
        /// <summary>
        /// 传感器信息集合
        /// </summary>
        public List<WSInfo> WSInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total
        {
            get;
            set;
        }

        /// <summary>
        /// 失败原因，返回结果为成功时，该值空
        /// </summary>
        public string Reason
        {
            get;
            set;
        }
    }
    #endregion
}