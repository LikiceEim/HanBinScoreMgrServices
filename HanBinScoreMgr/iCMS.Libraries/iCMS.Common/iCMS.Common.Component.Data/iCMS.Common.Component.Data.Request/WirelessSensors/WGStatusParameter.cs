/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  WGStatusParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：Agent上传网关状态信息实体类
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;
using System;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region Agent上传网关状态信息实体类
    /// <summary>
    /// Agent上传网关状态信息实体类
    /// </summary>
    public class WGStatusParameter : BaseRequest
    {
        /// <summary>
        /// 网关ID，数据库ID
        /// </summary>
        public string WGID { get; set; }
        /// <summary>
        /// 连接状态 0：断开； 1：连接
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 版本号 共四位x,x,x,x
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 网关NetWorkID
        /// </summary>
        public string NetWorkID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-07-15
        /// 创建记录：供电模式类型ID
        /// </summary>
        public int PowerSupplyModeTypeID { get; set; }

        /// <summary>
        /// 网关MAC地址
        /// </summary>
        public string GateWayMAC { get; set; }

        /// <summary>
        /// 上一次休眠时间
        /// </summary>
        public DateTime? LastSleepTime { get; set; }
        /// <summary>
        /// 休眠持续时间,原定义为 Float 但由于会传输0，无法与默认值进行区分
        /// </summary>
        public string Duration { get; set; }
    }
    #endregion
}
