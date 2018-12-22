/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 *文件名：  SysImagesParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：系统图片
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    #region 系统图片
    /// <summary>
    /// 系统图片
    /// </summary>
    [Obsolete]
    public class SysImagesParameter : BaseRequest
    {
        public string RootName { get; set; }
    }
    #endregion
}
