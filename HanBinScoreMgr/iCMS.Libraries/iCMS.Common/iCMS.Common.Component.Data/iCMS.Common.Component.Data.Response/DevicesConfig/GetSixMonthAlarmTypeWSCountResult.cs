/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetSixMonthAlarmTypeWSCountResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 16:16:08 
 *描述：获取近6个月不同报警类型的设备统计
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取近6个月不同报警类型的设备统计
    /// </summary>
    public class GetSixMonthAlarmTypeWSCountResult
    {
        /// <summary>
        /// WS报警类型
        /// </summary>
        public List<AlarmTypeWS> AlarmTypeWS
        {
            get;
            set;
        }
    }

    public class AlarmTypeWS
    {

        /// <summary>
        /// 未采集设备总数
        /// </summary>
        public int UnCollectedData
        {
            get;
            set;
        }

        /// <summary>
        /// 正常设备总数
        /// </summary>
        public int NormalData
        {
            get;
            set;
        }

        /// <summary>
        /// 报警设备总数
        /// </summary>
        public int AlertData
        {
            get;
            set;
        }

        /// <summary>
        /// 危险设备总数
        /// </summary>
        public int DangerData
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get;
            set;
        }

        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount
        {
            get;
            set;
        }
    }
}
