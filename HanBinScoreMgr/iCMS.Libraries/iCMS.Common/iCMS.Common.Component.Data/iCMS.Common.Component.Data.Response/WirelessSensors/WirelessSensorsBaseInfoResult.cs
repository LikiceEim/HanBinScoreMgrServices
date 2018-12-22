/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessSensors
 *文件名：  WirelessSensorsBaseInfoResult
 *创建人：  LF
 *创建时间：2016-09-05
 *描述：无线传感器信息结果类
 *
/************************************************************************************/
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.WirelessSensors
{
    #region 无线传感器返回基类
    /// <summary>
    /// 无线传感器返回基类
    /// </summary>
    public class WirelessSensorsBaseInfoResult
    {
        //返回基本状态信息结果集合
        public List<StatuInfo> DevicesInfo { get; set; }
        //WGID
        public string WGID { get; set; }
    }
    #endregion

    #region 状态信息
    /// <summary>
    /// 状态信息
    /// </summary>
    public class StatuInfo
    {
        //WG ID
        public string WGID { get; set; }
        //WG连接状态（1=连接，0断开）
        public string WGLinkstatu { get; set; }
        //WS ID
        public string WSID { get; set; }
        //WS的MAC地址
        public string WSMAC { get; set; }
        //WS连接状态（1=连接，0断开）
        public string WSLinkstatu { get; set; }
    }
    #endregion
}
