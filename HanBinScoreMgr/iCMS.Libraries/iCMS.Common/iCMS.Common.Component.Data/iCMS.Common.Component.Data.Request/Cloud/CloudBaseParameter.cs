/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  CloudBaseParameter
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：云参数基类
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 云平台参数基类
    /// <summary>
    /// 云平台参数基类
    /// </summary>
    public class CloudBaseParameter: BaseRequest
    {
        /// <summary>
        /// id集合
        /// </summary>
        public List<int> IdList
        {
            get;
            set;
        }
    }
    #endregion
}
