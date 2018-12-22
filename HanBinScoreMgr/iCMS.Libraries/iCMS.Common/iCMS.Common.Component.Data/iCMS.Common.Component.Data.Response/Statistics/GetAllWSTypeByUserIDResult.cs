/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Statistics
 * 文件名：  GetAllWSTypeByUserIDResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：通过用户id获取关联的传感器类型个数返回结果
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
    /// 创建记录：通过用户id获取关联的传感器类型个数返回结果
    /// </summary>
    public class GetAllWSTypeByUserIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：转速数据类型个数
        /// </summary>
        public int SpeedDataTypeCount { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：振动数据类型个数
        /// </summary>
        public int VibrationDataTypeCount { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：温度数据类型个数
        /// </summary>
        public int TemperatureDataTypeCount { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：功率（过程量）数据类型个数
        /// </summary>
        public int PowerDataTypeCount { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：油液数据类型个数
        /// </summary>
        public int OilDataTypeCount { get; set; }
    }
}