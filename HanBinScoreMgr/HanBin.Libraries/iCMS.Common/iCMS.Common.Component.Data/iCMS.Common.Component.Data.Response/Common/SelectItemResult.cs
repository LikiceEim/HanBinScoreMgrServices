/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Common
 * 文件名：  SelectItemResult 
 * 创建人：  王颖辉
 * 创建时间：2017/9/29 10:47:31 
 * 描述：返回选择列表
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.Common
{
    #region 返回选择列表
    /// <summary>
    /// 返回选择列表
    /// </summary>
    public class SelectItemResult
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
    #endregion
}