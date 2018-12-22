/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  GetMonitorTreeByParentIDParameter
 *创建人：  王颖辉
 *创建时间：2017-11-08
 *描述：获取监测树子节点，通过父子点
/************************************************************************************/

using iCMS.Common.Component.Data.Base;


namespace iCMS.Common.Component.Data.Request.Utility
{
    /// <summary>
    /// 获取监测树子节点，通过父子点
    /// </summary>
    public class GetMonitorTreeByParentIDParameter:BaseRequest
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID
        {
            get;
            set;
        }
    }
}
