/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  ConfigNameExistedParameter
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：判断云平台配置名称是否重复
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 判断云平台配置名称是否重复
    /// <summary>
    /// 判断云平台配置名称是否重复
    /// </summary>
    public class ConfigNameExistedParameter: BaseRequest
    {
        /// <summary>
        /// 新名称
        /// </summary>
        public string NewName
        {
            get;
            set;
        }

        /// <summary>
        /// 新名称
        /// </summary>
        public int? ID
        {
            get;
            set;
        }
    }
    #endregion
}
