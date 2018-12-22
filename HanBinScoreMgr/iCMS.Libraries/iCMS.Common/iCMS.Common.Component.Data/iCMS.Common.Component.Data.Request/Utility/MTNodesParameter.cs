/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  MTNodesParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：获取监测树参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 获取监测树参数
    /// <summary>
    /// 获取监测树参数
    /// </summary>
    public class MTNodesParameter : BaseRequest
    {
        public int Type { get; set; }

        /// <summary>
        /// 创建时间:2017-10-20
        /// 创建人：王颖辉
        /// 创建内容:用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
    }
    #endregion
}
