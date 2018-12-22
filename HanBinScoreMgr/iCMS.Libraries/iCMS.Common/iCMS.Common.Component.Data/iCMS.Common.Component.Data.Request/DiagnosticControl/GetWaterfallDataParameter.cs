/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DiagnosticControl
 *文件名：  GetWaterfallDataParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/11 11:08:47 
 *描述：获取瀑布图数据
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DiagnosticControl
{
    /// <summary>
    /// 获取瀑布图数据
    /// </summary>
    public class GetWaterfallDataParameter:BaseRequest
    {

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置编号
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置编号
        /// </summary>
        public EnumVibSignalType SignalType
        {
            get;
            set;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public List<string> Date
        {
            get;
            set;
        }


        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID
        {
            get;
            set;
        }
    }
}
