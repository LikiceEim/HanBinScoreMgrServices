/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  SpectrumDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：获取频谱图数据
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 获取频谱图数据
    /// <summary>
    /// 获取频谱图数据
    /// </summary>
    public class SpectrumDataParameter: BaseRequest
    {
        public int DevID { get; set; }
        public int MSiteID { get; set; }
        public int SignalType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ChartID { get; set; }
    }

    /// <summary>
    /// 通过轴承厂商、轴承型号获取轴承详细信息
    /// </summary>
    public class BearingInfoDataParameter : BaseRequest
    {
        public string BearingNum { get; set; }
        public string FactoryID { get; set; }
        public string ChartID { get; set; }
    }

    #endregion
}
