/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  EigenvalueTypeResult
 *创建人：  王颖辉 
 *创建时间：2016-10-28
 *描述：获取振动信号特征值类型返回类
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 获取振动信号特征值类型返回类
    /// <summary>
    /// 获取振动信号特征值类型返回类
    /// </summary>
    public class EigenvalueTypeResult
    {
        public List<EigenvalueType> SATypeList { get; set; }

        public string ChartID { get; set; }

        public EigenvalueTypeResult()
        {
            SATypeList = new List<EigenvalueType>();
        }
    }

    public class EigenvalueType
    {
        public string text { get; set; }

        public string value { get; set; }
    }
    #endregion
}
