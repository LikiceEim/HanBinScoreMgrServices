/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  IsAuthorizedParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：授权请求参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 授权请求参数
    /// <summary>
    /// 授权请求参数
    /// </summary>
    public class IsAuthorizedParameter : BaseRequest
    {
        public string RoleCode { get; set; }

        public string Code { get; set; }

        public int RoleID { get; set; }
    }
    #endregion
}
