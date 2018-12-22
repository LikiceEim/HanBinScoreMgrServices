/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  VolAndTempParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：无线传感器上传设备温度电压（监测设备：温度、电压；被监测设备：温度；）信息实体类
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 无线传感器上传设备温度电压
    /// <summary>
    /// 无线传感器上传设备温度电压
    /// </summary>
    public class VolAndTempParameter : BaseRequest
    {
        /// <summary>
        /// 采集时间
        /// </summary>
        public System.DateTime SamplingTime { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public float Temperature { get; set; }
        /// <summary>
        /// 电池电压
        /// </summary>
        public float Volatage { get; set; }
        /// <summary>
        /// WS MAC地址
        /// </summary>
        public string WSMAC { get; set; }
    }
    #endregion
}
