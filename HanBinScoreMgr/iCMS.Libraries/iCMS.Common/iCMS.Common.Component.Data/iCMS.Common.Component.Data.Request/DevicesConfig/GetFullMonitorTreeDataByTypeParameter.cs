/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetFullMonitorTreeDataByTypeParameter 
 *创建人：  王颖辉
 *创建时间：2017/9/28 10:57:10 
 *描述：根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
    /// <summary>
    /// 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
    /// </summary>
    public class GetFullMonitorTreeDataByTypeParameter:BaseRequest
    {
        /// <summary>
        /// 监测树类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }
    }
    #endregion
}
