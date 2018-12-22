/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Statistics
 *文件名：  BearResult
 *创建人：  QXM
 *创建时间：2016-10-31
 *描述：设备报警记录结果返回
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Utility
{
    #region 通过轴承厂商编号获取轴承型号信息 返回结果类
    /// <summary>
    /// 通过轴承厂商编号获取轴承型号信息 返回结果类
    /// </summary>
    public class BearResult
    {
        /// <summary>
        /// 轴承信息列表
        /// </summary>
        public List<BearingList> BearingList { get; set; }
    }
    #endregion

    #region 轴承信息
    /// <summary>
    /// 轴承信息
    /// </summary>
    public class BearingList
    {
        /// <summary>
        /// 轴承厂商名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 轴承厂商编号
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 轴承型号Id
        /// </summary>
        public int BearingID { get; set; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }
    }
    #endregion
}
