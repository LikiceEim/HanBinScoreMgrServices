/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Statistics
 * 文件名：  GetTopThreeDAUInfoByUserIDResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：通过用户id获取关联的TOP3采集单元下的传感器类型个数返回结果
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：通过用户id获取关联的TOP3采集单元下的传感器类型个数返回结果
    /// </summary>
    public class GetTopThreeDAUInfoByUserIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：采集单元统计信息返回结果集合
        /// </summary>
        public List<DAUStatisticsInfoResult> DAUInfoList { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：采集单元统计信息返回结果
    /// </summary>
    public class DAUStatisticsInfoResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：转速传感器集合
        /// </summary>
        public List<DAUDataSensorStatisticsInfo> SpeedWSInfo { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：振动传感器集合
        /// </summary>
        public List<DAUDataSensorStatisticsInfo> VibrationWSInfo { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：功率（过程量）传感器信息集合
        /// </summary>
        public List<DAUDataSensorStatisticsInfo> PowerWSInfo { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-17
        /// 创建记录：温度信息集合
        /// </summary>
        public List<DAUDataSensorStatisticsInfo> TemperatureWSInfo { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：油液传感器信息集合
        /// </summary>
        public List<DAUDataSensorStatisticsInfo> OilWSInfo { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：网关id
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：网关名称
        /// </summary>
        public string WGName { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：采集单元数据传感器统计返回结果
    /// </summary>
    public class DAUDataSensorStatisticsInfo
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器名称
        /// </summary>
        public string WSName { get; set; }
    }
}