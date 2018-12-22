/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessDevicesConfig
 *文件名：  WSStatusInfoParameter
 *创建人：  张辽阔
 *创建时间：2016-10-31
 *描述：获取1+个无线传感器的状态信息的参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessDevicesConfig
{
    #region 无线传感器状态
    #region 获取1+个无线传感器的状态信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：获取1+个无线传感器的状态信息的参数
    /// </summary>
    public class WSStatusInfoParameter : BaseRequest
    {
        /// <summary>
        /// 1+个WS的MAC地址或WSID集合串，以英文逗号分隔
        /// </summary>
        public string WSMACList { get; set; }

        /// <summary>
        /// 1=MAC地址，2=WSID
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 0:自动刷新，1：手动刷新
        /// </summary>
        public int OpType { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时
        /// </summary>
        public int CMDType { get; set; }
    }
    #endregion

    #region 获取多个设备下的无线传感器的状态信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：获取多个设备下的无线传感器的状态信息的参数
    /// </summary>
    public class WSStatusInfoByDeviceParameter : BaseRequest
    {
        /// <summary>
        /// 设备编号串
        /// </summary>
        public string DeviceIDs { get; set; }

        /// <summary>
        /// 0:自动刷新，1：手动刷新
        /// </summary>
        public int OpType { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时
        /// </summary>
        public int CMDType { get; set; }
    }
    #endregion

    #region 获取同一操作标识Key值下的无线传感器的状态信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：获取同一操作标识Key值下的无线传感器的状态信息的参数
    /// </summary>
    public class WSStatusInfoByKeyParameter : BaseRequest
    {
        /// <summary>
        /// 根据进行升级或下发测量定义后Service返回Key值获取
        /// </summary>
        public string WSKey { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时
        /// </summary>
        public int CMDType { get; set; }
    }
    #endregion

    #region 获取某一测点下的无线传感器的状态信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-31
    /// 创建记录：获取某一测点下的无线传感器的状态信息的参数
    /// </summary>
    public class WSStatusInfoByMSiteParameter : BaseRequest
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public string MSiteID { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时
        /// </summary>
        public int CMDType { get; set; }
    }
    #endregion

    #region 获取某一网关下的无线传感器的状态信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：获取某一网关下的无线传感器的状态信息的参数
    /// </summary>
    public class WSStatusInfoByWGNOParameter : BaseRequest
    {
        /// <summary>
        /// 网关编号
        /// </summary>
        public string WGNO { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时
        /// </summary>
        public int CMDType { get; set; }
    }
    #endregion
    #endregion
}