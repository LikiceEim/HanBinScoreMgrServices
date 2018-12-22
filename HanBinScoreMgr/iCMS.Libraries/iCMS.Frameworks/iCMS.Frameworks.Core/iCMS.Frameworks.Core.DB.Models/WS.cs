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
    /// 无线传感器信息表
    /// </summary>
    [Table("T_SYS_WS")]
    public class WS : EntityBase
    {
        #region Model

        /// <summary>
        /// 无线传感器ID
        /// </summary>
        [Key]
        public int WSID { get; set; }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int WSNO { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        public float BatteryVolatage { get; set; }

        /// <summary>
        /// 报警状态0未采集，1正常，2高报，3高高报
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int SensorType { get; set; }

        /// <summary>
        /// 使用状态0未使用，1使用
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 连接状态0断开，1连接
        /// </summary>
        public int LinkStatus { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 传感器型号
        /// </summary>
        public string WSModel { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? SetupTime { get; set; }

        /// <summary>
        /// 安装负责人
        /// </summary>
        public string SetupPersonInCharge { get; set; }

        /// <summary>
        /// SN码
        /// </summary>
        public string SNCode { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// 运行状态1:正常运行2:在修3:停机
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 触发状态0:停用，1启用
        /// </summary>
        public int? TriggerStatus { get; set; }

        /// <summary>
        /// 记录操作触发上传操作状态:1，启用,0为停止
        /// </summary>
        public int? TriggerTempOperationStatus { get; set; }

        /// <summary>
        /// 传感器形态类型 add by lwj---2018.05.02
        /// </summary>
        public int DevFormType { get; set; }

        /// <summary>
        /// 轴向 add by lwj---2018.05.02
        /// </summary>
        public int? Axis { get; set; }

        /// <summary>
        /// 轴向名称 add by lwj---2018.05.02
        /// </summary>
        public string AxisName { get; set; }

        /// <summary>
        /// 通道号 add by lwj---2018.05.02
        /// </summary>
        public int? ChannelId { get; set; }

        /// <summary>
        /// 传感器测量类型	否	1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器
        /// </summary>
        public int? SensorCollectType { get; set; }

        #endregion Model

        public WS()
        {
            BatteryVolatage = 0f;
            SetupTime = DateTime.Now;
        }
    }
}