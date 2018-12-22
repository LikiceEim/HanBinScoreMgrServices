/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud
 *文件名：  BaseDataListParameter
 *创建人：  王颖辉
 *创建时间：2016-12-19
 *描述：获取云平台推送基础数据列表参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 获取云平台推送基础数据列表参数
    /// <summary>
    /// 获取云平台推送基础数据列表参数
    /// </summary>
    public class BaseDataListParameter: BaseRequest
    {
        /// <summary>
        /// 基础数据类型
        /// </summary>
        public EnumBaseDataType Type
        {
            get;
            set;
        }
    }
    #endregion
}
