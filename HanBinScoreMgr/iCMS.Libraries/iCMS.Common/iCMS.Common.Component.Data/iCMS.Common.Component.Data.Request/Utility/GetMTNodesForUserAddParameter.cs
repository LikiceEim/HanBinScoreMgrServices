/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  MTNodesForUserAddParameter
 *创建人：  周起超
 *创建时间：2017-10-19
 *描述：获取监测树参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 获取监测树参数
    /// <summary>
    /// 获取监测树参数
    /// </summary>
    public class MTNodesForUserAddParameter : BaseRequest
    {
        public int UserID { get; set; }
    }
    #endregion
}