/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Cloud

 *文件名：  BaseDataListResult
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：基础数据返回列表
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    #region 基础数据返回列表
    /// <summary>
    /// 基础数据返回列表
    /// </summary>
    public class BaseDataListResult
    {
      
        /// <summary>
        /// 集合
        /// </summary>
        public List<BaseDataList> BaseDataList
        {
            get;
            set;

        }
    }

    public class BaseDataList
    {
        /// <summary>
        /// 值
        /// </summary>
        public int Value
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
    #endregion
}
