/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetDeviceInfoByWSIDParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/14 19:07:41 
 *描述：通过WSID获取挂靠设备信息
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
    /// 通过WSID获取挂靠设备信息
    /// </summary>
    public class GetDeviceInfoByWSIDParameter : BaseRequest
    {
        /// <summary>
        /// WSID
        /// </summary>
        public int WSID
        {
            get;
            set;
        }
    }
}
