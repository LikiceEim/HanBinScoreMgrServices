/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.WirelessDevicesConfig
 * 文件名：  AddWGDataParameter
 * 创建人：  张辽阔
 * 创建时间：2016-10-27
 * 描述：添加无线网关信息参数
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessDevicesConfig
{
    #region 无线网关信息
    #region 添加无线网关信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-27
    /// 创建记录：添加无线网关信息参数
    /// </summary>
    public class AddWGDataParameter : BaseRequest
    {
        /// <summary>
        /// 网管名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 网管名称
        /// </summary>
        public int WGNO { get; set; }

        /// <summary>
        /// 可连接WS的数量
        /// </summary>
        public int WGTypeId { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string WGModel { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Agent地址
        /// </summary>
        public string AgentAddress { get; set; }

        #region 融合添加字段
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// WG设备形态类型
        /// </summary>
        public int WGFormType { get; set; }
        /// <summary>
        /// iWSN轻量级网关MAC
        /// </summary>
        public string GateWayMAC { get; set; }
        #endregion
    }
    #endregion

    #region 批量删除无线网关信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-27
    /// 创建记录：批量删除无线网关信息参数
    /// </summary>
    public class DeleteWGDataParameter : BaseRequest
    {
        /// <summary>
        /// 无线网关ID集合
        /// </summary>
        public List<int> WGID { get; set; }
    }
    #endregion

    #region 编辑无线网关信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-27
    /// 创建记录：编辑无线网关信息参数
    /// </summary>
    public class EditWGDataParameter : BaseRequest
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID { get; set; }

        /// <summary>
        /// 网管名称
        /// </summary>
        public string WGName { get; set; }

        /// <summary>
        /// 网管名称
        /// </summary>
        public int WGNO { get; set; }

        /// <summary>
        /// 可连接WS的数量
        /// </summary>
        public int WGTypeId { get; set; }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string WGModel { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Agent地址
        /// </summary>
        public string AgentAddress { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// iWSN MAC地址
        /// </summary>
        public string GateWayMAC { get; set; }
    }
    #endregion

    #region 获取无线网关信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：获取无线网关信息参数
    /// </summary>
    public class GetWGDataParameter : BaseRequest
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
        /// 查询网关形态类型：
        /// 1、 单板
        /// 2、 轻量级
        /// 3、 有线
        /// -1、返回所有
        /// </summary>
        public int DevFormType { get; set; }
    }

    #region 获取无线网关下拉列表
    /// <summary>
    /// 创建人：王龙杰
    /// 创建时间：2017-11-08
    /// 创建记录：获取无线网关下拉列表
    /// </summary>
    public class GetWGSelectListParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
    public class GetWSSelectListParameter : BaseRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 传感器类型， 1，无线；2，有线；0，所有
        /// </summary>
        public int Type { get; set; }
    }

    #endregion
    #endregion
    #endregion
}