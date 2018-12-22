/************************************************************************************
 * Copyright (c) 2017 viLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  IsExistMonitorTreeDataByTypeParameter
 *创建人：  王颖辉
 *创建时间：2017-09-27
 *描述：对应监测树类型是否有数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 对应监测树类型是否有数据
    /// <summary>
    /// 对应监测树类型是否有数据
    /// </summary>
    public class IsExistMonitorTreeDataByTypeParameter : BaseRequest
    {
        /// <summary>
        /// 监测树类型集合
        /// </summary>
        public List<int> Type
        {
            get;
            set;
        }
    }
    #endregion
}
