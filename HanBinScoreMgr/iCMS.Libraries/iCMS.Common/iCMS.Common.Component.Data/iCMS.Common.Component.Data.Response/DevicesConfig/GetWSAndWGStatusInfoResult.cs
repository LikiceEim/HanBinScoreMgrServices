/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  GetWSAndWGStatusInfoResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 17:04:43 
 *描述：获取用户管理的WS和网关信息
/************************************************************************************/

using iCMS.Common.Component.Data.Base.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取用户管理的WS和网关信息
    /// </summary>
    public class GetWSAndWGStatusInfoResult
    {
        /// <summary>
        /// 网关信息
        /// </summary>
        public List<Sensorlist> Sensorlist
        {
            get;
            set;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 网关信息
    /// </summary>
    public class Sensorlist: EntityBase
    {
        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string WGName
        {
            get;
            set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int WGNO
        {
            get;
            set;
        }

        /// <summary>
        /// NetWorkID
        /// </summary>
        public int? NetWorkID
        {
            get;
            set;
        }

        /// <summary>
        /// 可连接的WS数量
        /// </summary>
        public int WGTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public int LinkStatus
        {
            get;
            set;
        }

     
        /// <summary>
        /// 型号
        /// </summary>
        public string WGModel
        {
            get;
            set;
        }


        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftwareVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树ID
        /// </summary>
        public int? MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge
        {
            get;
            set;
        }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string PersonInChargeTel
        {
            get;
            set;
        }

        /// <summary>
        /// 网关类型名称 
        /// </summary>
        public string WGTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 节点名称 
        /// </summary>
        public string MonitorTreeNames
        {
            get;
            set;
        }

        /// <summary>
        /// Agent地址 
        /// </summary>
        public string AgentAddress
        {
            get;
            set;
        }

        /// <summary>
        /// WS相关信息
        /// </summary>
        public List<WSWGStatusInfo> Children
        {
            get;
            set;
        }
    }

    /// <summary>
    /// WS信息
    /// </summary>
    public class WSWGStatusInfo
    {
        /// <summary>
        /// 无线传感器ID 
        /// </summary>
        public int WSID
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器编号 
        /// </summary>
        public int WSNO
        {
            get;
            set;
        }


        /// <summary>
        /// SN 
        /// </summary>
        public string WSName
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压 
        /// </summary>
        public int BatteryVolatage
        {
            get;
            set;
        }


        /// <summary>
        /// 状态 
        /// </summary>
        public int UseStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态 
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 地址 
        /// </summary>
        public string MACADDR
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器类型 
        /// </summary>
        public int SensorTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 升级状态 
        /// </summary>
        public int OperationStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 升级状态 
        /// </summary>
        public string OperationStatusName
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器类型 
        /// </summary>
        public string SensorTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 添加日期 
        /// </summary>
        public DateTime AddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 厂商 
        /// </summary>
        public string Vendor
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器型号 
        /// </summary>
        public string WSModel
        {
            get;
            set;
        }

        /// <summary>
        /// 安装时间 
        /// </summary>
        public DateTime? SetupTime
        {
            get;
            set;
        }

        /// <summary>
        /// 安装负责人 
        /// </summary>
        public string SetupPersonInCharge
        {
            get;
            set;
        }


        /// <summary>
        /// SN码 
        /// </summary>
        public string SNCode
        {
            get;
            set;
        }


        /// <summary>
        /// 固件版本 
        /// </summary>
        public string FirmwareVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 防爆认证编号 
        /// </summary>
        public string AntiExplosionSerialNo
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态 
        /// </summary>
        public int RunStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 图片ID 
        /// </summary>
        public int ImageID
        {
            get;
            set;
        }

        /// <summary>
        /// 负责人 
        /// </summary>
        public string PersonInCharge
        {
            get;
            set;
        }

        /// <summary>
        /// 负责人电话 
        /// </summary>
        public string PersonInChargeTel
        {
            get;
            set;
        }

        /// <summary>
        /// 备注 
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 联接状态 
        /// </summary>
        public int LinkStatus
        {
            get;
            set;
        }
    }
}
