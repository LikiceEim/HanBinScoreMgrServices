/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  Get6MonthAlarmTypeDeviceCountParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/12 17:38:40 
 *描述：获取近6个月不同报警类型的设备统计
/************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 获取近6个月不同报警类型的设备统计
    /// <summary>
    /// 获取近6个月不同报警类型的设备统计
    /// </summary>
    public class Get6MonthAlarmTypeDeviceCountParameter : BaseRequest
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
        /// 监测树ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

    }
    #endregion
}
