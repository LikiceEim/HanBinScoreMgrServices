/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DAUAgent
 * 文件名：  UploadDAUInfoParameter
 * 创建人：  QXM
 * 创建时间：2018/01/11
 * 描述：    DAUAgent属性数据上传请求参数
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DAUAgent
{
    public class UploadDAUInfoParameter : BaseRequest
    {
        /// <summary>
        /// 采集单元ID
        /// </summary>
        public int DAUID { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 	端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACAddress { get; set; }

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
        public int CurrentDAUStates { get; set; }

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
    }
}