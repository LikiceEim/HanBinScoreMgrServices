/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  GetDeviceStatusStatisticParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/13 10:45:35 
 *描述：获取某监测树下设备状态统计
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
    /// 获取某监测树下设备状态统计
    /// </summary>
    public class GetDeviceStatusStatisticParameter:BaseRequest
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

        /// <summary>
        /// 显示数据条数
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页-1：全部
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order
        {
            get;
            set;
        }
    }
}
