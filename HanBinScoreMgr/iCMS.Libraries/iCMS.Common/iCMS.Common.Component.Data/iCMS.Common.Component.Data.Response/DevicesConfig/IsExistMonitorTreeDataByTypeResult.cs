/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  IsExistMonitorTreeDataByTypeResult 
 *创建人：  王颖辉
 *创建时间：2017/9/27 15:59:30 
 *描述：对应监测树类型是否有数据
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 对应监测树类型是否有数据
    /// </summary>
    public class IsExistMonitorTreeDataByTypeResult
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        public List<bool> IsExist
        {
            get;
            set;
        }
    }
}
