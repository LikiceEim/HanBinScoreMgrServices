/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  GetCloudConfigDataParameter
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：云通讯基础数据页面查看接口参数
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 云通讯基础数据页面查看接口参数
    /// <summary>
    /// 云通讯基础数据页面查看接口参数
    /// </summary>
    public class GetCloudConfigDataParameter: BaseRequest
    {
        /// <summary>
        /// 查看类型 -1:全部 0：type：1,2,4,5 1：基础数据 2：云平台 4：日志是否记录 5：数据推送量
        /// </summary>
        public int Type
        {
            set;
            get;
        }
    }
    #endregion
}
