/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticControl
 *文件名：  WSTemperatureResult
 *创建人：  王颖辉
 *创建时间：2016-08-01
 *描述：WS温度返回值
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticControl
{
    #region 温度返回值,用于设备和传感器
    public class WSTemperatureResult
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DevID
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
        public string MsiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MsiteName
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
        public List<float> YData
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
        /// 单位
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// 阈值--警告
        /// </summary>
        public double AlertThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 阈值--告警
        /// </summary>
        public double WarnThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Result
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
        /// 标题：设备名称+测量位置名称+温度
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 温度采集时间间隔，单位：分钟
        /// </summary>
        public int TimeInterval
        {
            get;
            set;
        }
    }
    #endregion
}
