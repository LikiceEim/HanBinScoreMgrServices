/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  SignalTypeResult
 *创建人：  王颖辉
 *创建时间：2016-07-30
 *描述：信号类型
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 信息类型返回值
    /// <summary>
    /// 创建人：王颖辉
    /// 创建时间：2016-07-27
    /// 信息类型返回值
    /// </summary>
    public class SignalTypeResult
    {
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID
        {
            get;
            set;
        }

        /// <summary>
        /// 信号类型列表
        /// </summary>
        public List<SingleTypeList> SignalTypeList
        {

            get;
            set;
        }
    }
    #endregion

    #region 信号类型
    /// <summary>
    /// 信号类型
    /// </summary>
    public class SingleTypeList
    {
        /// <summary>
        /// 振动信号类型名称
        /// </summary>
        public string text
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型编号
        /// </summary>
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值
        /// </summary>
        public List<EigenValueList> EigenValueList
        {
            get;
            set;
        }
    }
    #endregion


    public class EigenValueList
    {
        /// <summary>
        /// 振动信号类型名称
        /// </summary>
        public string text
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型编号
        /// </summary>
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME
        {
            get;
            set;
        }
    }
}
