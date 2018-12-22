/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  DeleteSignalAlmParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：请求基类
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 删除振动信号报警
    /// <summary>
    /// 删除振动信号报警
    /// </summary>
    public class DeleteSignalAlmParameter : BaseRequest
    {
        /// <summary>
        /// 特征值ID
        /// </summary>
        public int SignalAlmID { get; set; }
    }
    #endregion
}
