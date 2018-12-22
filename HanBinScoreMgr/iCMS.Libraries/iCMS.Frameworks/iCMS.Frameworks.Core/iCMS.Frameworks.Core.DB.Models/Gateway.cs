/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：
 * 文件名：  
 * 创建人：  QXM
 * 创建时间：2016/7/21 10:10:13
 * 描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 无线网关信息表
    /// </summary>
    [Table("T_SYS_WG")]
    public class Gateway : EntityBase
    {
        #region Model

        /// <summary>
        /// 网关ID
        /// </summary>
        [Key]
        public int WGID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int WGNO { get; set; }

        /// <summary>
        /// NetWorkID
        /// </summary>
        public int? NetWorkID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int WGType { get; set; }

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
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int? MonitorTreeID { get; set; }

        /// <summary>
        /// Agent地址
        /// </summary>
        public string AgentAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PowerSupplyModeTypeID { get; set; }

        /// <summary>
        /// 网关MAC地址
        /// </summary>
        public string GateWayMAC { get; set; }

        /// <summary>
        /// 上一次睡眠时间
        /// </summary>
        public DateTime? LastSleepTime { get; set; }

        /// <summary>
        /// 睡眠持续秒
        /// </summary>
        public float? Duration { get; set; }

        /// <summary>
        /// 是否在线：1在线，0离线
        /// </summary>
        public bool? IsOnLine { get; set; }

        /// <summary>
        /// 1：单板，2：轻量级，3：有线
        /// </summary>
        public int DevFormType { get; set; }

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

        /// <summary>
        /// 采集单元IP地址
        /// </summary>
        public string WGIP { get; set; }

        /// <summary>
        /// 采集单元端口号
        /// </summary>
        public int? WGPort { get; set; }

        /// <summary>
        /// 子网掩码
        /// </summary>
        public string SubNetMask { get; set; }

        /// <summary>
        /// 网关
        /// </summary>
        public string GateWay { get; set; }

        #endregion Model
    }
}