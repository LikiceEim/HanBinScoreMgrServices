/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  PlatformInfoParameter
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：根据云平台ID获取平台信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 根据云平台ID获取平台信息
    /// <summary>
    /// 根据云平台ID获取平台信息
    /// </summary>
    public class PlatformInfoParameter: BaseRequest
    {
        /// <summary>
        /// 平台Id
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }
    }
    #endregion
}
