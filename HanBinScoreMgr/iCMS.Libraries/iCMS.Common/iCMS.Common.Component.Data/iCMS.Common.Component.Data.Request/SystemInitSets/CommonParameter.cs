/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 * 文件名：  AddModuleDataParameter
 * 创建人：  王颖辉
 * 创建时间：2016-11-15
 * 描述：添加模块数据
/************************************************************************************/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    #region 通用数据设置参数

    /// <summary>
    /// 获取通用数据类型
    /// </summary>
    public class GetComSetDataParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        /// </summary>
        public int table { get; set; }

        /// <summary>
        /// 各类型父ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：设备形态，Null/1: 无线，2：有线，3：三轴
        /// </summary>
        public int? DevFormType { get; set; }
    }

    /// <summary>
    /// 新增振动信号类型
    /// </summary>
    public class AddComSetDataParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        ///  29 特征值波长
        /// </summary>
        public int table { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 通用数据值
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 可用状态 1：可用 0：不可用
        /// </summary>
        public int IsUsable { get; set; }
        /// <summary>
        /// 初始化状态 1：用户添加 0：系统默认
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// WS形态类型， null/1: 无线，2:有线，3:三轴
        /// </summary>
        public int? DevFormType { get; set; }
    }

    /// <summary>
    /// 编辑振动信号类型
    /// </summary>
    public class EditComSetDataParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        /// </summary>
        public int table { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 通用数据值
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 可用状态 1：可用 0：不可用
        /// </summary>
        public int IsUsable { get; set; }
        /// <summary>
        /// 初始化状态 1：用户添加 0：系统默认
        /// </summary>
        public int IsDefault { get; set; }
    }

    /// <summary>
    /// 获取通用数据类型
    /// </summary>
    public class DeleteComSetDataParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        /// </summary>
        public int table { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 通用数据是否有相同Code
    /// </summary>
    public class IsExistComSetCodeParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        /// </summary>
        public int table { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 通用数据是否有相同Name
    /// </summary>
    public class IsExistComSetNameParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        ///  12 系统参数 
        ///  13 系统功能
        /// </summary>
        public int table { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 设置通用数据显示顺序
    /// </summary>
    public class SetComSetOrderParameter : BaseRequest
    {
        /// <summary>
        ///  1 监测树类型
        ///  2 设备类型
        ///  3 测量位置类型
        ///  4 测量位置监测类型
        ///  5 振动信号类型
        ///  6 特征值
        ///  7 波长
        ///  8 上限频率
        ///  9 下限频率
        ///  10 传感器类型
        ///  11 传感器挂靠个数
        ///  12 系统参数
        ///  13 系统功能
        /// </summary>
        public int table { get; set; }

        public int ID1 { get; set; }

        public int ID2 { get; set; }

        public int Order1 { get; set; }

        public int Order2 { get; set; }
    }

    #endregion

    #region ConnectType request parameter
    public class ViewConnectTypeParameter : BaseRequest
    {
    }

    public class AddConnectTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditConnectTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteConnectTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    /// <summary>
    /// 监测树类型级别是否重复
    /// </summary>
    public class IsExistDescribeInMonitorTreeParameter : BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Describe
        /// </summary>
        public string Describe { get; set; }
    }

    #region 操作类型
    #region 监测树类型操作

    #region 监测树类型
    /// <summary>
    /// 监测树类型
    /// </summary>
    public class MonitorTreeTypeParameter : BaseRequest
    {
    }
    #endregion

    #region 添加监测树类型
    /// <summary>
    /// 添加监测树类型
    /// </summary>
    public class AddMonitorTreeTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 编辑监测树类型
    /// <summary>
    /// 编辑监测树类型
    /// </summary>

    public class EditMonitorTreeTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 删除监测树类型
    /// <summary>
    /// 删除监测树类型
    /// </summary>

    public class DeleteMonitorTreeTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #endregion

    #region 设备类型操作
    public class DeviceTypeParameter : BaseRequest
    {

    }
    #region 设备类型添加
    /// <summary>
    /// 设备类型添加
    /// </summary>
    public class AddDeviceTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 设备类型编辑
    /// <summary>
    /// 设备类型编辑
    /// </summary>

    public class EditDeviceTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 删除设备类型
    /// <summary>
    /// 删除设备类型
    /// </summary>
    public class DeleteDeviceTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #endregion

    #region 测点类型操作

    #region 测点类型
    /// <summary>
    /// 测点类型
    /// </summary>
    public class MSiteTypeParameter : BaseRequest
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int ID { get; set; }
    }
    #endregion

    #region 添加测点类型
    /// <summary>
    /// 添加测点类型
    /// </summary>

    public class AddMSiteTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public int PID { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 编辑测点类型
    /// <summary>
    /// 编辑测点类型
    /// </summary>
    public class EditMSiteTypeParamete : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 删除测点类型
    /// <summary>
    /// 删除测点类型
    /// </summary>
    public class DeleteMSiteTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #endregion

    #region 测点对应监测类型操作

    #region 测点对应监测类型
    /// <summary>
    /// 测点对应监测类型
    /// </summary>
    public class MSiteMonitorTypeParameter : BaseRequest { }
    #endregion

    #region 添加测点对应监测类型
    /// <summary>
    /// 添加测点对应监测类型
    /// </summary>

    public class AddMSiteMonitorTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 编辑测点对应监测类型
    /// <summary>
    /// 编辑测点对应监测类型
    /// </summary>
    public class EditMSiteMonitorTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion
    #region 删除测点对应的监测类型
    /// <summary>
    /// 删除测点对应的监测类型
    /// </summary>
    public class DeleteMSiteMonitorTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #endregion

    #region 振动信号类型操作

    #region 振动信号类型
    /// <summary>
    /// 振动信号类型
    /// </summary>
    public class VibSignalTypeParameter : BaseRequest
    {
    }
    #endregion
    #region 添加振动信号类型
    /// <summary>
    /// 添加振动信号类型
    /// </summary>

    public class AddVibSignalTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 编辑振动信号类型

    #endregion

    #region 编辑振动信号类型
    /// <summary>
    /// 编辑振动信号类型
    /// </summary>
    public class EditVibSignalTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }
    #endregion

    #region 删除振动信号类型
    /// <summary>
    /// 删除振动信号类型
    /// </summary>
    public class DeleteVibSignalTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #endregion

    #region EigenValue type request parameter
    public class ViewEigenTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }

    public class AddEigenTypeParameter : BaseRequest
    {
        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditEigenTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteEigenTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region WaveLength request parameter
    public class ViewWaveLengthTypeParameter : BaseRequest
    {
        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int ID { get; set; }
    }

    public class AddWaveLengthTypeParameter : BaseRequest
    {
        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditWaveLengthTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteWaveLengthTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region  Wave UpperLimit
    public class ViewWaveUpperLimitTypeParameter : BaseRequest
    {
        public int SignalTypeID { get; set; }
    }

    public class AddWaveUpperLimitTypeParameter : BaseRequest
    {
        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditUpperLimitTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteUpperLimitTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region Wave LowerUpperLimit
    public class ViewWaveLowerLimitTypeParameter : BaseRequest
    {
        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int ID { get; set; }
    }

    public class AddWaveLowerLimitTypeParameter : BaseRequest
    {
        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditWaveLowerLimitTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteWaveLowerLimitTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region WS Type
    public class ViewWSTypeParameter : BaseRequest
    {

    }

    public class AddWSTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditWSTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteWSTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region WG Type
    public class ViewWGTypeParameter : BaseRequest
    {

    }

    public class AddWGTypeParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class EditWGTypeParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }
    }

    public class DeleteWGTypeParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion


    #endregion
}