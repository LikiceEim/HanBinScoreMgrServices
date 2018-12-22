/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 *文件名：  ConfigByParentIDParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    #region 通过父节点id获取配置信息
    /// <summary>
    /// 通过父节点id获取配置信息
    /// </summary>
    public class ConfigByParentIDParameter : BaseRequest
    {
        public int ParentID { get; set; }
    }
    #endregion
}
