/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Statistics
 * 文件名：  GetAllWSInfoByUserIDResult
 * 创建人：  张辽阔
 * 创建时间：2018-05-09
 * 描述：查询用户管理的所有传感器的详细信息返回结果
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
    /// 创建记录：查询用户管理的所有传感器的详细信息返回结果
    /// </summary>
    public class GetAllWSInfoByUserIDResult
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：简单的传感器信息返回结果集合
        /// </summary>
        public List<WSSimpleInfo> WSInfoList { get; set; }
    }

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-09
    /// 创建记录：简单的传感器信息返回结果
    /// </summary>
    public class WSSimpleInfo
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器id
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：传感器使用状态：true，使用；false，未使用
        /// </summary>
        public bool WSState { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：设备形态类型：单轴，三轴，有线等传感器
        /// </summary>
        public int DevFormType { get; set; }
    }
}