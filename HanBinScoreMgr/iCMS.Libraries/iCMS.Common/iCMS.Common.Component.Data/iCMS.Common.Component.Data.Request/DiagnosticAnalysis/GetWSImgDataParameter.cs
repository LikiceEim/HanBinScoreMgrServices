/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticAnalysis
 *文件名：  GetWSImgDataRequest 
 *创建人：  王颖辉
 *创建时间：2017/10/14 18:25:36 
 *描述：获取形貌图数据展示（传感器）
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
    /// 获取形貌图数据展示（传感器）
    /// </summary>
    public class GetWSImgDataParameter : BaseRequest
    {
        /// <summary>
        /// 设备
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
    }
}
