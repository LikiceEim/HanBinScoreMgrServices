/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  CopyMSResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 复制测点
    /// <summary>
    /// 复制测点
    /// </summary>
    public class CopyMSResult
    {
        public List<int> MSIDList { get; set; }

        public CopyMSResult()
        {
            MSIDList = new List<int>();
        }
    }
    #endregion
}
