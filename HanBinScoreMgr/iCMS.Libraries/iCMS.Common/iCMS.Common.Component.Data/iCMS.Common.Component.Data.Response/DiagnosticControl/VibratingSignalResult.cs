/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  VibratingSignalResult
 *创建人：  王颖辉
 *创建时间：2016-07-30
 *描述：振动信号返回值
 *
/************************************************************************************/
using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 振动信号返回值
    /// <summary>
    /// 振动信号返回值
    /// </summary>
    public class VibratingSignalResult
    {
        /// <summary>
        /// 趋势图数据
        /// </summary>
       public List<TendencyData> TendencyData
        {
            get;
            set;
        }

        /// <summary>
        /// 控件id
        /// </summary>
        public string ChartID
        {
            get;
            set;
        }
    }
    #endregion

    /// <summary>
    /// 趋势图数据
    /// </summary>
    public class TendencyData
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置编号
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSiteName
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型
        /// </summary>
        public int SignalID
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
        /// 特征值类型(振动信号报警配置)
        /// </summary>
        public int SAID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值类型名称
        /// </summary>
        public string SAName
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间(秒) eg：2015-8-10 13:23:50
        /// </summary>
        public DateTime BeginDate
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间(秒) eg：2015-8-10 13:23:50
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置数据
        /// </summary>
        public List<string> YData
        {
            get;
            set;
        }

        /// <summary>
        /// 时间轴数据
        /// </summary>
        public List<DateTime> XData
        {
            get;
            set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public List<string> AlarmType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有波形数据（0表示有，1表示没有）
        /// </summary>
        public List<string> WaveType
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
        /// 阈值—高报
        /// </summary>
        public float AlertThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 阈值—高高报
        /// </summary>
        public float WarnThreshold
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
        /// 标题：设备名称+测量位置名称+振动信号名称+特征值名称
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 振动数据采集时间间隔，单位：分钟
        /// </summary>
        public int TimeInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate
        {
            get;
            set;
        }
    }
}
