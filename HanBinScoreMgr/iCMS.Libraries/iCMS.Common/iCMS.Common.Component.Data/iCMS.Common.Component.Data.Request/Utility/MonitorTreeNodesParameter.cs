/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  MonitorTreeNodesParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：监测树信息
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 监测树信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class MonitorTreeNodesParameter : BaseRequest
    {
        /// <summary>
        /// 监测树ID
        /// </summary>
        public int RecordID { get; set; }

        /// <summary>
        /// 监测树记录类型
        /// </summary>
        public int Type { get; set; }
    }
    #endregion
}
