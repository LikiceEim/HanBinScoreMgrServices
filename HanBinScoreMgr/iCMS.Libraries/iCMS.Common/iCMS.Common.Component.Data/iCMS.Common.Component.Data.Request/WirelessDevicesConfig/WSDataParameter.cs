/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessDevicesConfig
 *文件名：  WSDataParameter
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：添加无线传感器信息参数
/************************************************************************************/

using System.Collections.Generic;
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessDevicesConfig
{
    #region 无线传感器
    #region 添加无线传感器信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：添加无线传感器信息参数
    /// </summary>
    public class AddWSDataParameter : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// 挂靠网关
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// SNCode代码
        /// </summary>
        public string SNCode { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string SetupTime { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        #region 解决方案融合 新增字段, Added by QXM, 2018/05/04
        /// <summary>
        /// WS关联的通道ID
        /// </summary>
        public int? ChannelID { get; set; }

        /// <summary>
        /// WS形态
        /// </summary>
        public int WSFormType { get; set; }

        /// <summary>
        /// 轴向
        /// </summary>
        public int? Axial { get; set; }

        /// <summary>
        /// 轴向自定义名称
        /// </summary>
        public string AxialName { get; set; }

        /// <summary>
        /// WS采集类型 1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器
        /// </summary>
        public int? SensorCollectType { get; set; }
        #endregion
    }

    #region WS信息类
    /// <summary>
    /// WS信息类
    /// </summary>
    public class WSData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// 挂靠网关
        /// </summary>
        public int WGID { get; set; }

        public int? ChannelID { get; set; }

        /// <summary>
        /// SNCode代码
        /// </summary>
        public string SNCode { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string SetupTime { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 传感器形态类型 1：无线传感器 2：有线传感器 3：三轴传感器
        /// </summary>
        public int WSFormType { get; set; }
        /// <summary>
        /// 轴向 1：x轴 2：y轴 3：z轴
        /// </summary>
        public int? Axial { get; set; }
        /// <summary>
        /// 轴向名称
        /// </summary>
        public string AxialName { get; set; }

        /// <summary>
        /// WS采集类型 1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器
        /// </summary>
        public int? SensorCollectType { get; set; }
    }
    #endregion

    #region 添加无线传感器集合信息参数
    /// <summary>
    /// 创建人：王颖辉
    /// 创建时间：2016-11-29
    /// 创建记录：添加无线传感器集合信息参数
    /// </summary>
    public class AddWSListDataParameter : BaseRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 无线传感器集合
        /// </summary>
        public List<WSData> WSListData
        {
            get;
            set;
        }
    }
    #endregion

    #endregion

    #region 编辑无线传感器信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：编辑无线传感器信息参数
    /// </summary>
    public class EditWSDataParameter : BaseRequest
    {
        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACADDR { get; set; }

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// 挂靠网关
        /// </summary>
        public int WGID { get; set; }

        public int? ChannelID { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public string SetupTime { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 防爆认证编号
        /// </summary>
        public string AntiExplosionSerialNo { get; set; }

        /// <summary>
        /// SNCode代码
        /// </summary>
        public string SNcode { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 传感器形态类型
        /// </summary>
        public int WSFormType { get; set; }
        /// <summary>
        /// 轴向
        /// </summary>
        public int? Axial { get; set; }
        /// <summary>
        /// 轴向名称
        /// </summary>
        public string AxialName { get; set; }

        /// <summary>
        /// 原始MAC
        /// </summary>
        public string OriginMACADDR { get; set; }

        public int? SensorCollectType { get; set; }
    }
    #endregion

    #region 获取无线传感器信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：获取无线传感器信息参数
    /// </summary>
    public class GetWSDataParameter : BaseRequest
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// 页数，从1开始,若为-1返回所有
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 页面行数，从1开始
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// null：全部、0：未使用、1：使用
        /// </summary>
        public int? isUseStatus { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 传感器类型：1，无线；2，有线；0，所有
        /// </summary>
        public int Type { get; set; }
    }
    #endregion

    #region 批量删除无线传感器信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：批量删除无线传感器信息参数
    /// </summary>
    public class DeleteWSDataParameter : BaseRequest
    {
        /// <summary>
        /// 无线传感器ID集合
        /// </summary>
        public List<int> WSID { get; set; }
    }
    #endregion
    #endregion
}
