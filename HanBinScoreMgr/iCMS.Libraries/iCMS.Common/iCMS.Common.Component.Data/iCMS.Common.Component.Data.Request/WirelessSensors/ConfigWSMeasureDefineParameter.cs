/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  ConfigWSMeasureDefineParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：配置无线传感器测量定义
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 配置无线传感器测量定义
    /// <summary>
    /// 配置无线传感器测量定义
    /// </summary>
    public class ConfigWSMeasureDefineParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置ID，以逗号隔开“,”
        /// </summary>
        public string MSiteIDs { get; set; }

        /// <summary>
        /// 采集方式 1：定时；2：临时，以逗号隔开“,”,与测量位置ID一一对应
        /// </summary>
        public string DAQStyle { get; set; }
    }
    #endregion
}
