/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetTriggerUploadDataParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：触发式上传
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 触发式上传
    /// <summary>
    /// 触发式上传
    /// </summary>
    public class GetTriggerUploadDataParameter : BaseRequest
    {
        /// <summary>
        /// MSiteIDs，以逗号隔开“,”
        /// </summary>
        public string MSiteIDs { get; set; }
    }
    #endregion
}
