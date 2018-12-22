/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Utility
 *文件名：  NewValueExistedResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：新值已经存在，返回结果
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.Utility
{
    #region 新值已经存在，返回结果
    /// <summary>
    /// 新值已经存在，返回结果
    /// </summary>
    public class NewValueExistedResult
    {
        public bool IsExisted { get; set; }

        public NewValueExistedResult()
        {
            IsExisted = false;
        }
    }
    #endregion
}
