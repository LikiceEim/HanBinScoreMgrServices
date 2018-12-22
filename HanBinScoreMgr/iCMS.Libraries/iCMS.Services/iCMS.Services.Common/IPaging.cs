/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Common
 *文件名：  IPaging
 *创建人：  王颖辉
 *创建时间：2016-11-23
 *描述：存储过程分页
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Service.Common
{
    #region 存储过程分页
    /// <summary>
    /// 存储过程分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPaging<T> where T : class
    {
        IList<T> GetPaging(PagingParameter parameter);
    }
    #endregion
}
