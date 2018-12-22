/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 *文件名：  EditConfigListParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/21 11:36:10 
 *描述：批量编辑
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    /// <summary>
    /// 批量编辑
    /// </summary>
    public class EditConfigListParameter : BaseRequest
    {
        /// <summary>
        /// 批量编辑
        /// </summary>
        public List<EditConfigList> ConfigList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 批量编辑
    /// </summary>
    public class EditConfigList
    {

        /// <summary>
        /// id
        /// </summary>
        public int ID
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


    }

}
