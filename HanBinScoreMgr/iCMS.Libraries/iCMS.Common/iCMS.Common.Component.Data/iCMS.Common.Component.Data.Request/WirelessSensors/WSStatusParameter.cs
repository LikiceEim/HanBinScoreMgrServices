/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  WSStatusParameter
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：Agent上传无线传感器状态信息实体类
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region Agent上传无线传感器状态信息实体类
    /// <summary>
    /// Agent上传无线传感器状态信息实体类
    /// </summary>
    public class WSStatusParameter : BaseRequest
    {
        /// <summary>
        /// WS MAC地址
        /// </summary>
        public string WSMAC { get; set; }
        /// <summary>
        /// 连接状态 0：断开； 1：连接
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 版本号 共四位x,x,x,x
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///创建时间：2017-07-20
        ///创建人:王颖辉
        ///创建内容： 添加网关地址
        /// </summary>
        /// 此字段表示WS所属于的WG MAC
        public string WGMAC { get; set; }
    }
    #endregion
}
