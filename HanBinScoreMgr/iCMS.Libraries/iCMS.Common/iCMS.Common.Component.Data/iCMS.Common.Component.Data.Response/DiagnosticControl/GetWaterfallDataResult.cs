/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  GetWaterfallDataResult 
 *创建人：  王颖辉
 *创建时间：2017/10/11 11:22:13 
 *描述：获取瀑布图数据 返回结果
/************************************************************************************/

using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    /// <summary>
    /// 获取瀑布图数据 返回结果
    /// </summary>
    public class GetWaterfallDataResult
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
        /// 设备名称
        /// </summary>
        public string DeviceName
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
        /// 测量位置名称
        /// </summary>
        public string MeasureSiteName
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public EnumVibSignalType SignalType
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号名称
        /// </summary>
        public string SignalName
        {
            get;
            set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get;
            set;
        }
        

        /// <summary>
        /// 开始时间（秒）
        /// eg：2015-8-14 13:24:52
        /// </summary>
        public string BeginDate
        {
            get;
            set;
        }


        /// <summary>
        /// 结束时间（秒）
        /// eg：2015-8-14 13:24:52
        /// </summary>
        public string EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 波形采集时间集合（秒）
        /// [2015-8-14 13:24:52,2015-8-14 13:24:52,2015-8-14 13:24:52]
        /// </summary>
        public List<string> SamplingDate
        {
            get;
            set;
        }

        /// <summary>
        /// 采集点数
        /// </summary>
        public int SamplingPointData
        {
            get;
            set;
        }

        /// <summary>
        /// X轴数据
        /// </summary>
        public List<double> XData
        {
            get;
            set;
        }

        /// <summary>
        /// Y轴数据
        /// </summary>
        public List<YDataInfo> YResult
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

        /// <summary>
        /// 标题
        /// 设备名称+测量位置名称+振动信号名称
        /// </summary>
        public string Title
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Y轴数据
    /// </summary>
    public class YDataInfo
    {
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Y轴值
        /// </summary>
        public List<double> YData
        {
            get;
            set;
        }
    }
}
