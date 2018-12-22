/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  WaveDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：获取波形数据
/************************************************************************************/

using System;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    #region 获取波形数据
    /// <summary>
    /// 获取波形数据
    /// </summary>
    public class WaveDataParameter : BaseRequest
    {
        public int DevID { get; set; }
        public int MSiteID { get; set; }
        public int SignalType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ChartID { get; set; }
    }
    #endregion

}
