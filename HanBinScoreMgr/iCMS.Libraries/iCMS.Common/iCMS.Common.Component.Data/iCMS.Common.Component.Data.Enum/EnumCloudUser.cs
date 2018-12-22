/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumCloud
 *创建人：  王颖辉  
 *创建时间：2016年9月10日
 *描述：云同步用户
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 云同步用户
    /// <summary>
    /// 云同步用户
    /// </summary>
    public enum EnumCloudUser
    {
        /// <summary>
        /// YunYI
        /// </summary>
        [Description("YunYi")]
        YunYi = 0,
        /// <summary>
        /// JiaXun
        /// </summary>
        [Description("iLine")]
        iLine = 1,
    }
    #endregion
}
