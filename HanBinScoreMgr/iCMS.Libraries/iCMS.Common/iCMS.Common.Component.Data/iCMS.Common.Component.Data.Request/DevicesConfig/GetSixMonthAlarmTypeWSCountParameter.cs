/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetSixMonthAlarmTypeWSCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/14 16:14:47 
 *描述：获取近6个月不同报警类型的设备统计
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 获取近6个月不同报警类型的设备统计
    /// </summary>
    public class GetSixMonthAlarmTypeWSCountParameter : BaseRequest
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

    }
}
