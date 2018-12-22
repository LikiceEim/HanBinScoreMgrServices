/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.DB.Models
 *文件名：  RealTimeAlarmThreshold
 *创建人：  王颖辉
 *创建时间：2017-11-08
 *描述：时实表与报警阈值关系
/************************************************************************************/
using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 时实表与报警阈值关系
    /// </summary>
    /// </summary>
    [Table("T_DATA_REALTIME_ALARMTHRESHOLD")]
    public class RealTimeAlarmThreshold : EntityBase
    {
        /// <summary>
        /// id
        /// </summary>
        public int RealTimeAlarmThresholdID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置Id
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置类型
        /// 1:加速度
        ///2:速度
        ///3:位移
        ///4:包络
        ///5:设备状态
        ///6:设备温度
        ///7:WS温度
        ///8:WS电池电压
        /// </summary>
        public int MeasureSiteThresholdType
        {
            get;
            set;
        }

        /// <summary>
        /// 报警阈值
        /// </summary>
        public float AlarmThresholdValue
        {
            get;
            set;
        }

        /// <summary>
        /// 危险阈值
        /// </summary>
        public float DangerThresholdValue
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int? EigenValueType
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
