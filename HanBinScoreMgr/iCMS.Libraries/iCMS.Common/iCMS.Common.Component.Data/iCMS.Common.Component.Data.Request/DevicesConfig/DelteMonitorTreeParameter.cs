/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  DelteMonitorTreeParameter
 *创建人：  LF
 *创建时间：2016-10-19
 *描述：删除监测树参数类
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 删除监测树
    /// <summary>
    /// 删除监测树
    /// </summary>
    public class DelteMonitorTreeParameter : BaseRequest
    {
        /// <summary>
        ///  监测树ID	
        /// </summary>
        public int MonitorTreeID { get; set; }
    }
    #endregion
}
