/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  ExistConfigNameResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：存在配置名称
/************************************************************************************/
namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 存在配置名称
    /// <summary>
    /// 存在配置名称
    /// </summary>
    public class ExistConfigNameResult
    {
        public bool IsExisted { get; set; }
    }
    #endregion

    #region 判断监测树节点下是否存在设备
    /// <summary>
    /// 判断监测树节点下是否存在设备
    /// </summary>
    public class ExistDeviceInMonitorTreeResult
    {
        public bool IsExisted { get; set; }
    }
    #endregion

    #region 判断系统中是否存在监测树
    /// <summary>
    /// 判断系统中是否存在监测树
    /// </summary>
    public class ExistMonitorTreeResult
    {
        public bool IsExisted { get; set; }
    }
    #endregion
}
