/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  DevImgDataParameter
 *创建人：  王颖辉
 *创建时间：2016-10-20
 *描述：形貌图数据展示
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    #region 形貌图数据展示
    /// <summary>
    /// 形貌图数据展示
    /// </summary>
    public class DevImgDataParameter : BaseRequest
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevID
        {
            set;
            get;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set;
            get;
        }
    }
}
#endregion

