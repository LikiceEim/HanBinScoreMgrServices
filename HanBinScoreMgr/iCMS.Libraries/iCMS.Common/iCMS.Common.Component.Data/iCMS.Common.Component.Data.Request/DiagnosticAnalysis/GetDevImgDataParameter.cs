/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetDevImgDataParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/13 16:43:33 
 *描述：向调用者暴露对形貌图数据展示
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    /// <summary>
    /// 向调用者暴露对形貌图数据展示
    /// </summary>
    public class GetDevImgDataParameter:BaseRequest
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DevID
        {
            get;
            set;
        }
    }
}
