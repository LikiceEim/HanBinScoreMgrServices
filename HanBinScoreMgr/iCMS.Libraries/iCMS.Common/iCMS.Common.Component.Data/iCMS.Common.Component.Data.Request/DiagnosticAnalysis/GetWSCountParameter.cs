/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis 
 *文件名：  GetWSCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/11/27 15:31:15
 *描述：统计服务
 *
 *修改人：
 *修改时间：
 *描述：
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    #region 获取WS数量
    /// <summary>
    /// 获取WS数量
    /// </summary>
    public class GetWSCountParameter:BaseRequest
    {
        /// <summary>
        /// DeviceID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
    }
    #endregion
}
