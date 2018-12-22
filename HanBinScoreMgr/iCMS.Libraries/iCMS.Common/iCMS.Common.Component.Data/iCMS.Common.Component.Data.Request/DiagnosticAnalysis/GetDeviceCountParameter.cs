/************************************************************************************
 *Copyright (c) 2017iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis 
 *文件名：  GetDeviceCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/11/27 14:58:37
 *描述：获取设备数量
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
    /// <summary>
    /// 获取设备数量
    /// </summary>
    public class GetDeviceCountParameter : BaseRequest
    {
        /// <summary>
        /// WSID
        /// </summary>
        public int WSID
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
}
