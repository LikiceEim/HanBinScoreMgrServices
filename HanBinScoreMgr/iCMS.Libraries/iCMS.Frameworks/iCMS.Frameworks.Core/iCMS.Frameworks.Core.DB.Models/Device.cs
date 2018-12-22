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
    /// 设备信息表
    /// </summary>
    [Table("T_SYS_DEVICE")]
    public class Device : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int DevID { get; set; }

        /// <summary>
        /// 监测树节点ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备编号，修改为 int 类型
        /// </summary>
        public int DevNO { get; set; }

        /// <summary>
        /// 转速
        /// </summary>
        public float Rotate { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DevType { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string DevManufacturer { get; set; }

        /// <summary>
        /// 最近检查时间
        /// </summary>
        public DateTime? LastCheckDate { get; set; }

        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DevManager { get; set; }

        /// <summary>
        /// 设备图片路径
        /// </summary>
        public string DevPic { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime? DevMadeDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DevMark { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// 设备报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 设备状态更新时间
        /// </summary>
        public DateTime DevSDate { get; set; }

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
        /// 排量
        /// </summary>
        public float? outputVolume { get; set; }

        /// <summary>
        /// 车间位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 传感器数量
        /// </summary>
        public int SensorSize { get; set; }

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
        /// 扬程
        /// </summary>
        public float? HeadOfDelivery { get; set; }

        /// <summary>
        /// 轴连器形式
        /// </summary>
        public string CouplingType { get; set; }

        /// <summary>
        /// 使用类型0主用设备，1备用设备
        /// </summary>
        public int UseType { get; set; }

        /// <summary>
        /// 运行状态3停机，1运行，2检修
        /// </summary>
        public int RunStatus { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 厂家型号
        /// </summary>
        public string DevModel { get; set; }

        /// <summary>
        /// 启停机阈值
        /// </summary>
        public float? StatusCriticalValue { get; set; }

        public DateTime? OperationDate { get; set; }

        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 设备停机时间
        /// </summary>
        public DateTime? DeviceStopDate { get; set; }

        #endregion Model
    }
}