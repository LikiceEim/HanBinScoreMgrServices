/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  DeviceAndMSiteDataForDeviceTreeResult
 * 创建人：  张辽阔
 * 创建时间：2016-11-01
 * 描述：返回设备和测量位置数据信息的参数
/************************************************************************************/

using System;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回设备和测量位置数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回设备和测量位置数据信息的参数
    /// </summary>
    public class DeviceAndMSiteDataForDeviceTreeResult
    {
        /// <summary>
        /// 设备记录总数
        /// </summary>
        public int Total { set; get; }

        /// <summary>
        /// 设备信息集合
        /// </summary>
        public List<DeviceInfo> DeviceInfoList { set; get; }

        /// <summary>
        /// 设备信息对应测量位置信息集合
        /// </summary>
        public List<MeasureInfo> MeasureInfoList { set; get; }
    }
    #endregion

    #region 设备信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：设备信息实体
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { set; get; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int WID { set; get; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DevNO { set; get; }

        /// <summary>
        /// 转速
        /// </summary>
        public float Rotate { set; get; }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int UseType { set; get; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DevType { set; get; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName { set; get; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string DevManufacturer { set; get; }

        /// <summary>
        /// 设备负责人
        /// </summary>
        public string DevManager { set; get; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public string DevMadeDate { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DevMark { set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { set; get; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { set; get; }

        /// <summary>
        /// 设备图片
        /// </summary>
        public string DevPic { set; get; }

        /// <summary>
        /// 维度
        /// </summary>
        public string Latitude { set; get; }

        /// <summary>
        /// 设备状态 0:未采集；1：正常；2：高报；3：高高报；
        /// </summary>
        public string AlmStatus { set; get; }

        /// <summary>
        /// 设备状态更新时间
        /// </summary>
        public string DevSDate { set; get; }

        /// <summary>
        /// 长
        /// </summary>
        public string Length { set; get; }

        /// <summary>
        /// 高
        /// </summary>
        public string Height { set; get; }

        /// <summary>
        /// 宽
        /// </summary>
        public string Width { set; get; }

        /// <summary>
        /// 排量
        /// </summary>
        public string outputVolume { set; get; }

        /// <summary>
        /// 车间位置
        /// </summary>
        public string Position { set; get; }

        /// <summary>
        /// 传感器数量
        /// </summary>
        public int SensorSize { set; get; }

        /// <summary>
        /// 功率
        /// </summary>
        public string Power { set; get; }

        /// <summary>
        /// 组号
        /// </summary>
        public string GroupNo { set; get; }

        /// <summary>
        /// 额定电流
        /// </summary>
        public string RatedCurrent { set; get; }

        /// <summary>
        /// 额定电压
        /// </summary>
        public string RatedVoltage { set; get; }

        /// <summary>
        /// 介质
        /// </summary>
        public string Media { set; get; }

        /// <summary>
        /// 排出最大压力
        /// </summary>
        public string OutputMaxPressure { set; get; }

        /// <summary>
        /// 扬程
        /// </summary>
        public string HeadOfDelivery { set; get; }

        /// <summary>
        /// 轴连器形式
        /// </summary>
        public string CouplingType { set; get; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { set; get; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { set; get; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { set; get; }

        /// <summary>
        /// 厂家型号
        /// </summary>
        public string DevModel { set; get; }

        /// <summary>
        /// 启停机阀值
        /// </summary>
        public float? StatusCriticalValue { set; get; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public string AddDate { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 节点
        /// </summary>
        public string TreeNode { set; get; }

        /// <summary>
        /// 节点id
        /// </summary>
        public string TreeNodeID { set; get; }

        /// <summary>
        /// 上次检查时间
        /// </summary>
        public string LastCheckDate { set; get; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { set; get; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperationDate { get; set; }

        /// <summary>
        /// 网关简单信息集合
        /// </summary>
        public List<WGSimpleInfoResult> WGInfo { get; set; }
    }
    #endregion

    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-05-04
    /// 创建记录：网关简单信息
    /// </summary>
    public class WGSimpleInfoResult
    {
        /// <summary>
        /// 网关id
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 设备形态类型：1、单板；2、轻量级网关；3、有线
        /// </summary>
        public int DevFormType { get; set; }
    }

    #region 测量信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：测量信息实体
    /// </summary>
    public class MeasureInfo
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int WID { set; get; }

        /// <summary>
        /// 测量位置类型ID
        /// </summary>
        public int MSiteTypeId { set; get; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MStieTypeName { set; get; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { set; get; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：设备形态类型：单轴，三轴，有线等传感器
        /// </summary>
        public int DevFormType { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { set; get; }

        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName { set; get; }

        /// <summary>
        /// WS连接状态
        /// </summary>
        public int LinkStatus { set; get; }

        /// <summary>
        /// 测量位置类型
        /// </summary>
        public int MeasureSiteType { set; get; }

        /// <summary>
        /// 传感器灵敏度系数A
        /// </summary>
        public float SensorCosA { set; get; }

        /// <summary>
        /// 传感器灵敏度系数B
        /// </summary>
        public float SensorCosB { set; get; }

        /// <summary>
        /// 测量位置报警状态
        /// </summary>
        public int MSiteStatus { set; get; }

        /// <summary>
        /// 测量位置状态更新时间
        /// </summary>
        public string MSiteSDate { set; get; }

        /// <summary>
        /// 波形采集时间间隔
        /// </summary>
        public string WaveTime { set; get; }

        /// <summary>
        /// 特征值采集时间间隔
        /// </summary>
        public string FlagTime { set; get; }

        /// <summary>
        /// 温度、电池电压采集时间间隔
        /// </summary>
        public string TemperatureTime { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { set; get; }

        /// <summary>
        /// 测点序号
        /// </summary>
        public int SerialNo { set; get; }

        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID { set; get; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType { set; get; }

        /// <summary>
        /// 轴承厂商编号
        /// </summary>
        public string FactoryID { set; get; }

        /// <summary>
        /// 轴承厂商名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddDate { set; get; }

        /// <summary>
        /// 测量定义下发状态
        /// </summary>
        public int OperationStatus { set; get; }

        /// <summary>
        /// 测量定义下发状态
        /// </summary>
        public string ConfigMSDate { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 子级数量
        /// </summary>
        public int ChildrenCount { set; get; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 触发状态0:停用，1启用
        /// </summary>
        public int TriggerStatus { get; set; }

        /// <summary>
        /// 触发式测量定义下发状态
        /// </summary>
        public int TriggerOperationStatus { get; set; }

        /// <summary>
        /// 触发式测量定义下发状态
        /// </summary>
        public string TriggerConfigMSDate { get; set; }

        /// <summary>
        /// 触发式测量定义下发状态
        /// </summary>
        public int UpdateOperationStatus { get; set; }

        /// <summary>
        /// 触发式测量定义下发状态
        /// </summary>
        public string UpdateConfigMSDate { get; set; }

        /// <summary>
        /// 多轴承信息
        /// </summary>
        public List<BearingInfoForResult> BearingInfoList { get; set; }

        /// <summary>
        /// 1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器
        /// </summary>
        public int? SensorCollectType { get; set; }

        #region 转速测点相关
        /// <summary>
        /// 转速测点关联的测点ID
        /// </summary>
        public int? RelationMSiteID { get; set; }
        /// <summary>
        /// 转速测点关联的测点名称
        /// </summary>
        public string RelationMSName { get; set; }

        /// <summary>
        /// 脉冲数
        /// </summary>
        public float? PulsNumPerP { get; set; }
        #endregion
    }
    #endregion

    #region 测量信息实体
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：测量信息实体
    /// </summary>
    public class MeasureDBInfo
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int WID { set; get; }

        /// <summary>
        /// 测量位置类型ID
        /// </summary>
        public int MSiteTypeId { set; get; }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MStieTypeName { set; get; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { set; get; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：设备形态类型：单轴，三轴，有线等传感器
        /// </summary>
        public int DevFormType { get; set; }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int? WSID { set; get; }

        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName { set; get; }

        /// <summary>
        /// WS连接状态
        /// </summary>
        public int? LinkStatus { set; get; }

        /// <summary>
        /// 测量位置类型
        /// </summary>
        public int MeasureSiteType { set; get; }

        /// <summary>
        /// 传感器灵敏度系数A
        /// </summary>
        public float SensorCosA { set; get; }

        /// <summary>
        /// 传感器灵敏度系数B
        /// </summary>
        public float SensorCosB { set; get; }

        /// <summary>
        /// 测量位置报警状态
        /// </summary>
        public int MSiteStatus { set; get; }

        /// <summary>
        /// 测量位置状态更新时间
        /// </summary>
        public DateTime MSiteSDate { set; get; }

        /// <summary>
        /// 波形采集时间间隔
        /// </summary>
        public string WaveTime { set; get; }

        /// <summary>
        /// 特征值采集时间间隔
        /// </summary>
        public string FlagTime { set; get; }

        /// <summary>
        /// 温度、电池电压采集时间间隔
        /// </summary>
        public string TemperatureTime { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { set; get; }

        /// <summary>
        /// 测点序号
        /// </summary>
        public int SerialNo { set; get; }

        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID { set; get; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType { set; get; }

        /// <summary>
        /// 轴承厂商编号
        /// </summary>
        public string FactoryID { set; get; }

        /// <summary>
        /// 轴承厂商名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { set; get; }

        /// <summary>
        /// 测量定义下发状态
        /// </summary>
        public int OperationStatus { set; get; }

        /// <summary>
        /// 测量定义下发状态
        /// </summary>
        public string ConfigMSDate { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 子级数量
        /// </summary>
        public int ChildrenCount { set; get; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 触发状态0:停用，1启用
        /// </summary>
        public int TriggerStatus { get; set; }
    }
    #endregion

    #region 操作实体
    /// <summary>
    /// 创建人：王颖辉
    /// 创建时间：2017-08-10
    /// 创建记录：操作实体
    /// </summary>
    public class OperationInfo
    {
        /// <summary>
        ///  操作结果
        /// </summary>
        public string OperationResult
        {
            get;
            set;
        }

        /// <summary>
        ///  结束时间
        /// </summary>
        public DateTime? EDate
        {
            get;
            set;
        }

        /// <summary>
        ///  测量位置ID
        /// </summary>
        public int MSID
        {
            get;
            set;
        }

        /// <summary>
        /// WSID
        /// </summary>
        public int WSID
        {
            get;
            set;
        }

        /// <summary>
        /// 1：定时
        /// 2：临时
        /// </summary>
        public int DAQStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 操作结果
        /// </summary>
        public int OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 设备id
        /// </summary>
        public int DevID
        {
            get;
            set;
        }
    }
    #endregion

    /// <summary>
    /// 添加轴承信息
    /// </summary>
    public class BearingInfoForResult
    {
        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public string FactoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType
        {
            get;
            set;
        }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商
        /// </summary>
        public string FactoryName
        {
            get;
            set;
        }
    }
}