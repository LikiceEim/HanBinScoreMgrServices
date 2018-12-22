/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.WirelessDevicesConfig
 *文件名：  WSStatusInfoResult
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：获取状态返回结果对象
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    #region 获取状态返回结果
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：获取状态返回结果
    /// </summary>
    public class WSStatusInfoResult
    {
        /// <summary>
        /// WS状态信息集合
        /// </summary>
        public List<WSStatusInfo> WSStatusInfos { get; set; }
    }
    #endregion

    #region 返回状态信息类
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：返回状态信息类
    /// </summary>
    public class WSStatusInfo
    {
        /// <summary>
        /// WS升级状态信息集合，0=初始无状态；1=升级成功；2=升级失败；3=操作中
        /// </summary>
        public int? UpdateStatus { get; set; }

        /// <summary>
        /// 最后一次更新操作时间，未操作为空
        /// </summary>
        public DateTime? EdateForUpdate { get; set; }

        /// <summary>
        /// WS下发测量定义状态信息集合，0=初始无状态；1=下发成功；2=下发失败；3=操作中
        /// </summary>
        public int? ConfigStatus { get; set; }

        /// <summary>
        /// 最后一次下发测量定义的操作时间，未操作为空
        /// </summary>
        public DateTime? EdateForConfig { get; set; }

        /// <summary>
        /// WS触发式上传状态信息集合，0=初始无状态；1=下发成功；2=下发失败；3=操作中4=超时状态5=网络正忙
        /// </summary>
        public int? TriggerStatus { get; set; }

        /// <summary>
        /// 最后一次更新操作时间，未操作空
        /// </summary>
        public DateTime? EdateForTrigger { get; set; }

        /// <summary>
        /// WS版本信息
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// WSID信息
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// WSMAC地址信息
        /// </summary>
        public string MAC { get; set; }

        /// <summary>
        /// WS所在位置ID
        /// </summary>
        public int? MSiteID { get; set; }

        /// <summary>
        /// WS所在位置名称
        /// </summary>
        public string MSiteName { get; set; }

        /// <summary>
        /// 测量定义下发类型：1=定时，2=临时，为空时返回定时和临时
        /// </summary>
        public int CMDType { get; set; }

        /// <summary>
        /// 连接状态0=断开  ，1=连接
        /// </summary>
        public int LinkStatus { get; set; }

        /// <summary>
        /// WS名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 使用状态0未使用，1使用
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 触式使用状态 0:停用，1启用
        /// </summary>
        public int? TriggerUseStatus { get; set; }
    }
    #endregion
}