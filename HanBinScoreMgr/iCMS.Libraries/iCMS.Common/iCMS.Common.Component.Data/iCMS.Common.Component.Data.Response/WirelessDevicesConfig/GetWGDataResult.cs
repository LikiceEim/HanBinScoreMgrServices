/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.WirelessDevicesConfig
 * 文件名：  GetWGDataResult
 * 创建人：  张辽阔
 * 创建时间：2016-10-26
 * 描述：无线网关返回数据实体
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.WirelessDevicesConfig
{
    #region 无线网关返回数据类型
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：无线网关返回数据类型
    /// </summary>
    public class GetWGDataResult
    {
        /// <summary>
        /// 报警记录信息集合
        /// </summary>
        public List<GetWGDataInfo> WGInfo { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total { get; set; }
    }
    #endregion

    #region 无线网关返回数据实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：无线网关返回数据实体
    /// </summary>
    public class GetWGDataInfo
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// WG设备形态类型
        /// </summary>
        public int WGFormType { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string WGNO { get; set; }

        /// <summary>
        /// 网络ID
        /// </summary>
        public string NetWorkID { get; set; }

        /// <summary>
        /// 无线网关类型ID
        /// </summary>
        public int WGTypeId { get; set; }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatus { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string WGModel { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddDate { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 可连接的WS数量
        /// </summary>
        public string WGTypeName { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string MonitorTreeNames { get; set; }

        /// <summary>
        /// Agent地址
        /// </summary>
        public string AgentAddress { get; set; }

        #region 融合添加
        /// <summary>
        /// 地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string GateWayMAC { get; set; }
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string SubNetMask { get; set; }
        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway { get; set; }
        /// <summary>
        /// 串码
        /// </summary>
        public string SerizeCode { get; set; }
        /// <summary>
        /// 主板串码
        /// </summary>
        public string MainBoardSerizeCode { get; set; }
        /// <summary>
        /// 防雷板串码
        /// </summary>
        public string BESPSerizeCode { get; set; }
        /// <summary>
        /// 产品信息串码
        /// </summary>
        public string ProductInfoSerizeCode { get; set; }
        /// <summary>
        /// 电源串码
        /// </summary>
        public string PowerSupplySerizeCode { get; set; }

        /// <summary>
        /// 核心板串码
        /// </summary>
        public string CoreBoardSerizeCode { get; set; }
        /// <summary>
        /// 采集单元当前状态
        /// </summary>
        public int? CurrentDAUStates { get; set; }

        /// <summary>
        /// 一级bootloader版本
        /// </summary>
        public string MinibootVersion { get; set; }

        /// <summary>
        /// 二级bootloader版本
        /// </summary>
        public string SndbootVersion { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// FPGA版本
        /// </summary>
        public string FPGAVersion { get; set; }
        #endregion
    }
    #endregion

    #region 获取无线网关下拉列表
    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-11-08
    /// 创建记录：获取无线网关下拉列表
    /// </summary>
    public class GetWGSelectListResult
    {
        /// <summary>
        /// 报警记录信息集合
        /// </summary>
        public List<WGSelect> WGSelectList { get; set; }
    }

    public class WGSelect
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }
        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }
    }
    #endregion

    #region 获取无线传感器下拉列表
    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-11-08
    /// 创建记录：获取无线传感器下拉列表
    /// </summary>
    public class GetWSSelectListResult
    {
        public List<WSSelect> WSSelectList { get; set; }
    }

    public class WSSelect
    {
        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：传感器形态类型，1、无线 2、有线 3、三轴
        /// </summary>
        public int WSFormType { get; set; }
    }
    #endregion
}