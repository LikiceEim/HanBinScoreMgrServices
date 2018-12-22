/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Utility
 *文件名：  BearingFactoryResult
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：轴承工厂返回结果
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Utility
{
    #region 轴承工厂返回结果
    /// <summary>
    /// 轴承工厂返回结果
    /// </summary>
    public class BearingFactoryResult
    {
        public List<BearingFactoryInfo> FactoryList { get; set; }

        public string ChartID { get; set; }

        public int Total { get; set; }

        public BearingFactoryResult()
        {
            FactoryList = new List<BearingFactoryInfo>();
            ChartID = string.Empty;
        }
    }
    #endregion

    #region 工厂类
    /// <summary>
    /// 工厂类
    /// </summary>
    public class BearingFactoryInfo
    {
        public int ID { get; set; }

        public string FactoryName { get; set; }

        public string FactoryID { get; set; }
    }
    #endregion
}