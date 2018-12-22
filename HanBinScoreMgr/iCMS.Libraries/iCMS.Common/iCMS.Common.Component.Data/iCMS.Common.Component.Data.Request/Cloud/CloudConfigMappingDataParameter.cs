/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  CloudConfigMappingDataParameter
 *创建人：  王颖辉
 *创建时间：2016-12-09
 *描述：关系数据页面关联
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 关系数据页面关联
    /// <summary>
    /// 关系数据页面关联
    /// </summary>
    public class CloudConfigMappingDataParameter : BaseRequest
    {
        /// <summary>
        /// 平台Id
        /// </summary>
        public int PlatformId
        {
            get;
            set;
        }

        /// <summary>
        /// 选中ID
        /// </summary>
        public List<int> SelectID
        {
            get;
            set;
        }
    }
    #endregion
}
