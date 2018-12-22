/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  TriggerUploadResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：触发式上传，返回结果
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 触发式上传，返回结果
    /// <summary>
    /// 触发式上传，返回结果
    /// </summary>
    public class TriggerUploadResult
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public List<TriggerUploadInfo> TriggerUploadList { get; set; }
    }
    #endregion

    #region 触发式上传空值信息
    /// <summary>
    /// 触发式上传空值信息
    /// </summary>
    public class TriggerUploadInfo
    {
        /// <summary>
        /// 位置id
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 位置名称
        /// </summary>
        public string MSiteName
        {
            get;
            set;
        }
    }
    #endregion
}
