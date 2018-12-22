/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  AddDeviceRecordForDeviceTreeParameter
 * 创建人：  张辽阔
 * 创建时间：2016-11-01
 * 描述：添加设备信息参数
/************************************************************************************/

using System;
using System.Collections.Generic;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 添加设备信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：添加设备信息参数
    /// </summary>
    public class AddDeviceRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 监测树节点ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevNo { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DevType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 使用类型0主用设备，1备用设备
        /// </summary>
        public int UseType { get; set; }

        /// <summary>
        /// 转速
        /// </summary>
        public float Rotate { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public float? Length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public float? Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public float? Height { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string DevManufacturer { get; set; }

        /// <summary>
        /// 厂家型号
        /// </summary>
        public string DevModel { get; set; }

        /// <summary>
        /// 车间位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public float? Power { get; set; }

        /// <summary>
        /// 额定电流
        /// </summary>
        public float? RatedCurrent { get; set; }

        /// <summary>
        /// 额定电压
        /// </summary>
        public float? RatedVoltage { get; set; }

        /// <summary>
        /// 介质
        /// </summary>
        public string Media { get; set; }

        /// <summary>
        /// 排出最大压力
        /// </summary>
        public float? OutputMaxPressure { get; set; }

        /// <summary>
        /// 轴连器形式
        /// </summary>
        public string CouplingType { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public float? OutputVolume { get; set; }

        /// <summary>
        /// 启停机阈值
        /// </summary>
        public float? StatusCriticalValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DevMark { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime? DevMadeDate { get; set; }

        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DevManager { get; set; }

        /// <summary>
        /// 扬程
        /// </summary>
        public float? HeadOfDelivery { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// 网关简单信息集合
        /// </summary>
        public List<int> WGID { get; set; }

        /// <summary>
        /// 投运时间
        /// </summary>
        public DateTime? OperationDate { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public int UserId { get; set; }
    }
    #endregion
}