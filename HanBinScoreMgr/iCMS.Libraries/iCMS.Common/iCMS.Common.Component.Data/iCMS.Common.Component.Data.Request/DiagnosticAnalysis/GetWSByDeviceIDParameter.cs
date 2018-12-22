/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 * 文件名：  GetWSByDeviceIDParameter 
 * 创建人：  王颖辉
 * 创建时间：2017/10/14 19:29:36 
 * 描述：请求基类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DiagnosticAnalysis
{
    /// <summary>
    /// 通过设备ID获取传感器
    /// </summary>
    public class GetWSByDeviceIDParameter : BaseRequest
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUse
        {
            get;
            set;
        }
    }
}